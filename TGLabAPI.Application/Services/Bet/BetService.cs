using TgLabApi.Domain.Entities.Player;
using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Application.DTOs.Common;
using TGLabAPI.Application.DTOs.Transaction.Request;
using TGLabAPI.Application.DTOs.Transaction.Response;
using TGLabAPI.Application.Interfaces.Repositories.Transaction;
using TGLabAPI.Application.Interfaces.Services.Player;
using TGLabAPI.Application.Interfaces.Services.Transaction;
using TGLabAPI.Domain.Entities;

namespace TGLabAPI.Application.Services.Transaction
{
    public class BetService : IBetService
    {
        private readonly IBetRepository _betRepository;
        private readonly IPlayerService _playerService;
        private readonly ITransactionService _transactionService;
        private readonly IWalletService _walletService;

        public BetService(
            IBetRepository betRepository, 
            IPlayerService playerService, 
            ITransactionService transactionService, 
            IWalletService walletService
            )
        {
            _betRepository = betRepository;
            _playerService = playerService;
            _transactionService = transactionService;
            _walletService = walletService;
        }

        public async Task<GetBetResponse> CreateBet(CreateBetRequest commandCreateBet)
        {
            WalletEntity? walletHistory = null;
            BetEntity? betHistory = null;

            try
            {
                var player = await _playerService.Me();
                if (player == null) throw new Exception("Usuário não encontrado.");

                if (!player.Wallet.IsSufficient(commandCreateBet.Value)) throw new Exception("Saldo insuficiente.");

                walletHistory = player.Wallet;

                BetEntity newBet = new BetEntity(player.Id, commandCreateBet.Value, Domain.Entities.BetStatus.Pending, commandCreateBet.Color);
                var bet = await _betRepository.Insert(newBet);

                var walletAmount = await _walletService.RemoveAmout(player.Wallet.Id, commandCreateBet.Value);
                await _transactionService.CreateTransaction(player.Wallet.Id, bet.Id, bet.Value, TransactionType.Bet);

                betHistory = bet;

                bet = await this.ProcessBet(bet, player, player.Wallet.Id);

                var wallet = await _walletService.GetByPlayerId(player.Id);

                return new GetBetResponse(bet.Id, bet.Value, bet.Status.ToString(), bet.Color, bet.ValueReward, bet.CreatedAt, wallet.Amount);
            }
            catch (Exception ex)
            {
                if (walletHistory != null && betHistory != null && !betHistory.IsCanceled)
                {
                    await _walletService.UpdateAmout(walletHistory.Id, walletHistory.Amount);
                    await _transactionService.CreateTransaction(walletHistory.Id, betHistory.Id, betHistory.Value, TransactionType.Refund);
                    betHistory.Cancel();
                    await _betRepository.Update(betHistory);
                }
                throw new ApplicationException($"Erro ao criar uma nova aposta: {ex.Message}", ex);
            }
        }

        public async Task<GetBetResponse> CancelBet(Guid id)
        {
            try
            {
                var player = await _playerService.Me();
                if (player == null) throw new Exception("Usuário não encontrado.");

                BetEntity? bet = await _betRepository.Get(id);
                if (bet == null) throw new Exception("Aposta não encontrada.");

                if (!bet.IsCanceled)
                {
                    double value = player.Wallet.Amount;
                    if (bet.Status == BetStatus.Win) value = player.Wallet.Amount - bet.ValueReward!.Value;
                    if (bet.Status == BetStatus.Lose) value = player.Wallet.Amount + bet.Value;

                    bet.Cancel();
                    await _betRepository.Update(bet);

                    await _transactionService.CreateTransaction(player.Wallet.Id, bet.Id, bet.Value, TransactionType.Cancelled);
                    player.Wallet.UpdateAmount(value);
                    await _walletService.UpdateAmout(player.Wallet.Id, player.Wallet.Amount);
                }

                return new GetBetResponse(bet.Id, bet.Value, bet.Status.ToString(), bet.Color, bet.ValueReward, bet.CreatedAt, player.Wallet.Amount);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao cancelar aposta: {ex.Message}", ex);
            }
        }
        public async Task<BetEntity> ProcessBet(BetEntity bet, PlayerEntity player, Guid walletId)
        {
            try
            {
                Random random = new Random();
                int numberResult = random.Next(0, 37);

                Color? result = null;
                if (numberResult == 0)
                    result = Color.Green;
                if (numberResult % 2 == 0)
                    result = Color.Black;
                if (numberResult % 2 != 0)
                    result = Color.Red;

                if (result == null) throw new ApplicationException("Erro ao Processar Aposta.");

                if (bet.Color == result)
                {
                    bet.Win();
                    await _betRepository.Update(bet);
                    var walletAmount = await _walletService.AddAmout(walletId, bet.ValueReward!.Value);
                    await _transactionService.CreateTransaction(walletId, bet.Id, bet.ValueReward!.Value, TransactionType.Reward);
                }
                else
                {
                    bet.Lose();
                    await _betRepository.Update(bet);

                    var lastFiveBets = await _betRepository.ListLastFiveBets(player.Id);

                    if (lastFiveBets.Count == 5 && lastFiveBets.All(b => b.Status == BetStatus.Lose))
                    {
                        var lastBetDate = lastFiveBets.Last().CreatedAt;
                        if (player.LastBonusDate == null || player.LastBonusDate < lastBetDate)
                        {
                            double bonus = lastFiveBets.Sum(e => e.Value) * 0.1;
                            var walletAmount = await _walletService.AddAmout(walletId, bonus);
                            await _transactionService.CreateTransaction(walletId, bet.Id, bonus, TransactionType.Bonus);

                            player.SetBonus(DateTime.UtcNow);
                            await _playerService.Update(player);
                        }
                    }
                }

                return bet;
            }
            catch (Exception) {
                throw;
            }
        }

        public async Task<PageableResponse<GetBetListDetailResponse>> List(PageableRequest request)
        {
            try
            {
                var player = await _playerService.Me();
                if (player == null) throw new Exception("Usuário não encontrado.");

                var result = await _betRepository.PageableListByUser(player.Id, request.PageNumber, request.PageSize);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao listar apostas: {ex.Message}", ex);
            }
        }
    }
}

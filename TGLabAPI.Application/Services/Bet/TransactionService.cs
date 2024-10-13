using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Application.DTOs.Bet.Response;
using TGLabAPI.Application.DTOs.Common;
using TGLabAPI.Application.Interfaces.Repositories.Transaction;
using TGLabAPI.Application.Interfaces.Services.Player;
using TGLabAPI.Application.Interfaces.Services.Transaction;
using TGLabAPI.Domain.Entities;

namespace TGLabAPI.Application.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPlayerService _playerService;

        public TransactionService(ITransactionRepository transactionRepository, IPlayerService playerService)
        {
            _transactionRepository = transactionRepository;
            _playerService = playerService;
        }

        public async Task<TransactionEntity> CreateTransaction(Guid walletId, Guid betId, double value, TransactionType type)
        {
            try
            {
                TransactionEntity transaction = new TransactionEntity(walletId, betId, value, type);
                return await _transactionRepository.Insert(transaction);
            }
            catch(Exception ex)
            {
                throw new ApplicationException($"Erro ao criar uma nova transação: {ex.Message}", ex);
            }
        }

        public async Task<PageableResponse<GetTransactionResponse>> List(PageableRequest request)
        {
            try
            {
                var player = await _playerService.Me();
                if (player == null) throw new Exception("Usuário não encontrado.");

                var result = await _transactionRepository.PageableListByUser(player.Id, request.PageNumber, request.PageSize);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao listar transações: {ex.Message}", ex);
            }
        }
    }
}

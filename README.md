# TgLab - Sistema de Carteira Digital para Apostas

## Desafio

Este projeto tem como objetivo implementar uma solução de carteira digital simplificada para uma plataforma de apostas esportivas e cassino online. O sistema permite aos jogadores realizar apostas, consultar transações, ganhar prêmios e cancelar apostas, com saldo atualizado a cada operação. O jogo segue as regras de uma **roleta europeia**, onde o sistema decide as apostas com base em uma probabilidade de 37 números. As regras da roleta são:
- O número **0** refere-se à cor **verde**.
- Números **pares** referem-se à cor **preta**.
- Números **ímpares** referem-se à cor **vermelha**.

## Como Rodar o Projeto

### Pré-requisitos:
- .NET 6 SDK
- PostgreSQL
- Docker

### Passo a Passo:

1. **Clone o repositório**:

    ```bash
    git clone https://github.com/VitorDanielAdams/TgLab.git
    cd TgLab
    ```

2. **Crie o banco de dados e configure o ambiente**:

    - Acesse o prompt de comando na raiz do projeto.
    - Caso já tenha o `.env` criado, rode o comando:

        ```bash
        docker-compose up
        ```

    - Caso não tenha o `.env` criado, siga as etapas abaixo:

        - **Windows**:
            ```bash
            setup_env.bat
            ```

        - **Linux**:
            ```bash
            chmod +x setup_env.sh
            ./setup_env.sh
            ```

    - Após executar o script, forneça as credenciais de acesso no terminal e o container com o banco de dados será criado, juntamente com o arquivo `.env`.

3. **Configure a string de conexão do banco de dados** no arquivo `appsettings.json`:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Database=TgLabDb;Username=seu_usuario;Password=sua_senha"
    }
    ```

4. **Rode as migrações** para configurar o banco de dados:

    ```bash
    dotnet ef database update
    ```

5. **Compile e rode a aplicação**:

    ```bash
    dotnet run
    ```

6. **Acesse a documentação do Swagger** para testar a API:

    - Acesse [http://localhost:10618/index.html](http://localhost:10618/index.html) no seu navegador.

## Testes Unitários

Execute os testes unitários com o seguinte comando:

```bash
dotnet test
```

### Modelo Entidade Relacional (MER)
**O modelo ER (Entidade Relacional) encontra-se disponível dentro do diretório /TGLabAPI.Infrastructure/Resources do projeto. Ele descreve as tabelas de Jogadores, Carteiras, Apostas e Transações.**

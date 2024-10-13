*Desafio*

* Como Rodar o Projeto
- Pré-requisitos:
    .NET 6 SDK
    PostgreSQL
    Docker

- Passo a Passo:
    Clone o repositório:

    git clone https://github.com/VitorDanielAdams/TgLab.git
    cd TgLab


Crie o banco de dados e env:
    Acesse o prompt de comando na raiz do projeto:
    Caso já tenha o .env criado, rode o comando:
        docker-compose up
    Caso não tenha o .env criado, rode o comando:
        (Windows):
        - setup_env.bat

        (Linux):
        - chmod +x setup_env.sh
        - ./setup_env.sh
    Após rodar informe do terminal as credenciais de acesso e ele irá criar o
    container com o banco de dados e o .env para ele.

Configure a string de conexão do banco de dados no arquivo appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=TgLabDb;Username=seu_usuario;Password=sua_senha"
}

Rode as migrações para configurar o banco de dados:
    dotnet ef database update

Compile e rode a aplicação:
    dotnet run

Acesse a documentação do Swagger para testar a API:
    Acesse http://localhost:10618/index.html no seu navegador.

Testes Unitários
    Execute os testes unitários com o seguinte comando:
    dotnet test

Modelo Entidade Relacional (MER)

    O modelo ER (Entidade Relacional) encontra-se disponível dentro do diretório /TGLabAPI.Infrastructure/Resources do projeto. Ele descreve as tabelas de Jogadores, Carteiras, Apostas e Transações.

Observações

    O jogo segue as regras de uma roleta europeia, onde o sistema decide as apostas com base em probabilidade de 37 numeros, sendo o número 0 referente a cor verde, os números pares referentes a cor preta e os números impares referentes a cor vermelha.
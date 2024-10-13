read -p "Informe o usuário do PostgreSQL [admin]: " POSTGRES_USER
POSTGRES_USER=${POSTGRES_USER:-admin}

read -p "Informe a senha do PostgreSQL [admin]: " POSTGRES_PASSWORD
POSTGRES_PASSWORD=${POSTGRES_PASSWORD:-admin}

read -p "Informe o nome do banco de dados [seu_banco_de_dados]: " POSTGRES_DB
POSTGRES_DB=${POSTGRES_DB:-seu_banco_de_dados}

# read -p "Informe o email do pgAdmin [admin@admin.com]: " PGADMIN_DEFAULT_EMAIL
# PGADMIN_DEFAULT_EMAIL=${PGADMIN_DEFAULT_EMAIL:-admin@admin.com}

# read -p "Informe a senha do pgAdmin [admin]: " PGADMIN_DEFAULT_PASSWORD
# PGADMIN_DEFAULT_PASSWORD=${PGADMIN_DEFAULT_PASSWORD:-admin}

echo "POSTGRES_USER=${POSTGRES_USER}" > .env
echo "POSTGRES_PASSWORD=${POSTGRES_PASSWORD}" >> .env
echo "POSTGRES_DB=${POSTGRES_DB}" >> .env
# echo "PGADMIN_DEFAULT_EMAIL=${PGADMIN_DEFAULT_EMAIL}" >> .env
# echo "PGADMIN_DEFAULT_PASSWORD=${PGADMIN_DEFAULT_PASSWORD}" >> .env

echo ".env criado com sucesso!"

if ! [ -x "$(command -v docker-compose)" ]; then
  echo "Erro: docker-compose não está instalado." >&2
  exit 1
fi

echo "Subindo o ambiente Docker..."
docker-compose up -d

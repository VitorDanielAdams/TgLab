@echo off
set /p POSTGRES_USER=Informe o usuario do PostgreSQL [admin]: 
if "%POSTGRES_USER%"=="" set POSTGRES_USER=admin

set /p POSTGRES_PASSWORD=Informe a senha do PostgreSQL [admin]: 
if "%POSTGRES_PASSWORD%"=="" set POSTGRES_PASSWORD=admin

set /p POSTGRES_DB=Informe o nome do banco de dados [seu_banco_de_dados]: 
if "%POSTGRES_DB%"=="" set POSTGRES_DB=seu_banco_de_dados

@REM set /p PGADMIN_DEFAULT_EMAIL=Informe o email do pgAdmin [admin@admin.com]: 
@REM if "%PGADMIN_DEFAULT_EMAIL%"=="" set PGADMIN_DEFAULT_EMAIL=admin@admin.com

@REM set /p PGADMIN_DEFAULT_PASSWORD=Informe a senha do pgAdmin [admin]: 
@REM if "%PGADMIN_DEFAULT_PASSWORD%"=="" set PGADMIN_DEFAULT_PASSWORD=admin

echo POSTGRES_USER=%POSTGRES_USER% > .env
echo POSTGRES_PASSWORD=%POSTGRES_PASSWORD% >> .env
echo POSTGRES_DB=%POSTGRES_DB% >> .env
@REM echo PGADMIN_DEFAULT_EMAIL=%PGADMIN_DEFAULT_EMAIL% >> .env
@REM echo PGADMIN_DEFAULT_PASSWORD=%PGADMIN_DEFAULT_PASSWORD% >> .env

echo .env criado com sucesso!

where docker-compose >nul 2>nul
if %ERRORLEVEL% neq 0 (
  echo Erro: docker-compose não está instalado.
  exit /b 1
)

echo Subindo o ambiente Docker...
docker-compose up -d

docker-compose ps
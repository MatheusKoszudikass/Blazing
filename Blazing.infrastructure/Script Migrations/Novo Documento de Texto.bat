@echo off

rem Definir variaveis
set STARTUP_PROJECT=../../Blazing.Api
set CONTEXT_PROJECT_ECOMMERCE=BlazingDbContext
set CONTEXT_PROJECT_IDENTITY=BlazingIdentityDbContext

rem Adicionar migracao para o contexto Ecommerce
echo Adicionando migracao CreateTable para o contexto %CONTEXT_PROJECT_ECOMMERCE%.
echo Pressione qualquer tecla para prosseguir ou Ctrl+C para cancelar.
pause >nul
dotnet ef migrations add CreateTable -c %CONTEXT_PROJECT_ECOMMERCE% --startup-project %STARTUP_PROJECT%
echo Migracao %CONTEXT_PROJECT_ECOMMERCE% criada com sucesso!

echo.


rem Atualizar o banco de dados para o contexto Ecommerce
echo Deseja atualizar o banco de dados para o contexto %CONTEXT_PROJECT_ECOMMERCE%?
echo Pressione qualquer tecla para prosseguir ou Ctrl+C para cancelar.
pause >nul
dotnet ef database update -c %CONTEXT_PROJECT_ECOMMERCE% --startup-project %STARTUP_PROJECT%
echo Banco de dados atualizado para o contexto %CONTEXT_PROJECT_ECOMMERCE%.


echo.


rem Adicionar migracao para o contexto Identity
echo Adicionando migracao CreateColumn para o contexto %CONTEXT_PROJECT_IDENTITY%.
echo Pressione qualquer tecla para prosseguir ou Ctrl+C para cancelar.
pause >nul
dotnet ef migrations add CreateColumn -c %CONTEXT_PROJECT_IDENTITY% --startup-project %STARTUP_PROJECT%
echo Migracao %CONTEXT_PROJECT_IDENTITY% criada com sucesso!


echo.


rem Atualizar o banco de dados para o contexto Identity
echo Deseja atualizar o banco de dados para o contexto %CONTEXT_PROJECT_IDENTITY%?
echo Pressione qualquer tecla para prosseguir ou Ctrl+C para cancelar.
pause >nul
dotnet ef database update -c %CONTEXT_PROJECT_IDENTITY% --startup-project %STARTUP_PROJECT%
echo Banco de dados atualizado para o contexto %CONTEXT_PROJECT_IDENTITY%.
pause >nul

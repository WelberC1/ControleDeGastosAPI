# Controle de Gastos

Bem-vindo ao **Controle de Gastos**, um projeto desenvolvido para gerenciar contas e realizar controle financeiro de maneira prática e eficiente. Este repositório inclui uma API desenvolvida em ASP.NET Core com suporte a um front-end integrado para operações básicas de autenticação, criação de conta e gerenciamento de transações.

## Recursos do Projeto

- **Funções do Software:**
  - Login com validação de credenciais;
  - Registro de novos usuários com verificação de senha;
  - Registro de Transações financeiras (Entrada ou Saída);
  - Registro das Transações por tipo (Alimentação, Transporte, Entretenimento, Saúde, Contas, Salários e Diversos);
  - Controle de Saldo dos usuários;

- **Frontend Integrado:**
  - Interface responsiva desenvolvida com Bootstrap 5;
  - Páginas de login e criação de conta;
  - Pagina de Visualização das Transações, com saldo atual;
  - Página de Adicionar Transações;

- **API REST:**
  - Suporte a endpoints para autenticação e registro de usuários.
  - JSON como formato padrão para comunicação.

## Tecnologias Utilizadas

- **Back-End:**
  - ASP.NET Core 8.0
  - C#

- **Front-End:**
  - HTML5
  - CSS3
  - Bootstrap 5
  - JavaScript (ES6+)

- **Banco de Dados:**
  - SQL Server

## Estrutura do Projeto

O projeto consiste em duas camadas, sendo o front-end, responsável por toda a interface visual dos usuários, e o back-end, presente na forma de uma API REST.
A comunicação dessas camadas é feita através do Javascript, utilizando o método fetch.

## Configuração e Execução

### Requisitos

- .NET SDK 8.0 ou superior.
- SQL Server instalado e configurado.

### Configuração do Banco de Dados

1. Crie um banco de dados chamado `controle_gastos` (ou o nome desejado).
2. Configure a string de conexão no arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "Database": "Server=./;Database= ControleGastosDB;User Id=root;Password=@Password123;TrustServerCertificate=True"
},
```

### Executando o Projeto

1. Clone este repositório:

   ```bash
   git clone https://github.com/WelberC1/ControleDeGastosAPI.git
   ```

2. Acesse o diretório do projeto:

   ```bash
   cd controle-de-gastos
   ```

3. Restaure as dependências:

   ```bash
   dotnet restore
   ```

4. Execute o projeto:

   ```bash
   dotnet run
   ```

5. Acesse a aplicação no navegador em: `https://localhost:7227/Pages/index`.

## Licença

Este projeto está licenciado sob a Licença MIT. Consulte o arquivo `LICENSE` para mais informações.

# Projeto em Desenvolvimento...

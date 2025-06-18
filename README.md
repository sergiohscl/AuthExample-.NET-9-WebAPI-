# AuthExample (.NET 9 WebAPI)

API de autenticação com JWT + Refresh Token, validação de senha customizada, tratamento de exceções e arquitetura organizada por camadas.

---

## 🚀 Tecnologias

- .NET 9
- ASP.NET Core WebAPI
- Entity Framework Core
- SQLite
- JWT (Json Web Token)
- BCrypt (hash de senhas)

---

## 📁 Estrutura de Pastas

```
AuthExample/
├── Controllers/            # Controllers da API
├── Data/                   # DbContext e configuração do EF
├── DTO/                   # Objetos de transferência (input/output)
├── Exceptions/            # Exceções personalizadas
├── Middlewares/           # Tratamento global de erros
├── Models/                # Models principais (entidades)
├── Services/
│   ├── Interfaces/        # Interfaces dos serviços
│   └── ...                # Lógica de negócios
└── Program.cs             # Configuração da aplicação
```

---

## 🧱 Como criar o projeto

```bash
dotnet new webapi -n AuthExample
cd AuthExample
```

---

## 📦 Pacotes utilizados

Instale os pacotes:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Swashbuckle.AspNetCore
dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt

dotnet add package BCrypt.Net-Next
```

---

## 🧰 Gerar o banco de dados

1. Criar a primeira migration:

```bash
dotnet ef migrations add InitialCreate
```

2. Atualizar o banco:

```bash
dotnet ef database update
```

---

## ⚙️ Configurar e rodar a aplicação

```bash
dotnet run
```

A API estará disponível em:

```
https://localhost:5083
```

---

## 🔐 Endpoints principais

| Método | Rota                     | Descrição                          |
|--------|--------------------------|------------------------------------|
| POST   | `/api/user/register`     | Registra novo usuário              |
| POST   | `/api/user/login`        | Autentica e retorna JWT + refresh  |
| POST   | `/api/user/refresh`      | Gera novo JWT usando refresh token |
| GET    | `/api/user`              | Lista todos os usuários (Admin)    |

---

## ✅ Validação de senha

- Deve conter exatamente 6 dígitos
- Não pode ser sequência (ex: `123456`)
- Não pode conter todos os números iguais (ex: `111111`)

---

## 🧱 Tratamento global de erros

Middleware para capturar exceções e retornar os status apropriados:

- `InvalidPasswordException` → 400
- `UserAlreadyExistsException` → 409
- `NotFoundException` → 404
- `UnauthorizedAccessException` → 401
- Outros → 500

---

## ✨ Exemplos de corpo de requisição

### Registro (`POST /api/user/register`)

```json
{
  "username": "admin",
  "password": "987654",
  "role": "Admin"
}
```

### Login (`POST /api/user/login`)

```json
{
  "username": "admin",
  "password": "987654"
}
```

### Refresh Token (`POST /api/user/refresh`)

```json
{
  "username": "admin",
  "refreshToken": "Base64Gerado..."
}
```

---

## 👤 Autor

Desenvolvido por Sergio Henrique - Projeto de autenticação com boas práticas e arquitetura limpa.
# 📦 Product NCM CEST API

API REST em .NET 9 para consulta de produtos com códigos **NCM**, **CEST** e respectivos valores de **MVA**, com importação de dados via JSON e persistência em **PostgreSQL**. Desenvolvido com arquitetura modular e foco em clareza, escalabilidade e portfólio profissional.

---

## 🚀 Tecnologias

- [.NET 9](https://dotnet.microsoft.com)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [PostgreSQL](https://www.postgresql.org/)
- [System.Text.Json](https://learn.microsoft.com/dotnet/api/system.text.json)
- [JWT Bearer Authentication](https://learn.microsoft.com/aspnet/core/security/authentication/jwt)
- [Swagger](https://swagger.io/)
- [Arquitetura por camadas](#-estrutura-do-projeto)

---

## 🗂️ Estrutura do projeto

ProductNcmCestAPI/  
   ├── CestNcm.API/ → API REST com endpoints HTTP  
   ├── CestNcm.Domain/ → Entidades e regras de domínio  
   ├── CestNcm.Infrastructure/ → DbContext + persistência (PostgreSQL + EF Core)  
   ├── CestNcm.DataImporter/ → Console App para importar JSON  
      ├── dados_cest.json → Base de dados oficial  
   └── README.md → Você está aqui :)
  
---

## 📥 Importador JSON

Importa centenas de registros de forma automatizada com validação básica:

```bash
dotnet run --project CestNcm.DataImporter
```
✅ Lê o arquivo dados_cest.json 🛠️ Preenche os campos CEST, NCM, Descrição, e MVAs 🧼 Ignora automaticamente registros inválidos ou incompletos 📊 Exibe contagem final de inserções

---

🐘 Banco de dados PostgreSQL
Configure a string de conexão no appsettings.json do projeto CestNcm.DataImporter:

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=cestncm;Username=seu_usuario;Password=sua_senha"
  }
}

Certifique-se de que o banco cestncm esteja criado e acessível localmente.

---

🔐 Autenticação JWT
A API exige token JWT válido para acessar qualquer endpoint.

🔓 Como obter um token
Use o endpoint POST /api/auth/login com o corpo:

{
  "username": "admin",
  "password": "123456"
}

Retorno:

{
  "token": "<seu_token_jwt_aqui>"
}


🔒 Como usar
Inclua o token no cabeçalho Authorization:

Authorization: Bearer <seu_token_jwt>

No Swagger, clique em Authorize e cole o token.

Todos os endpoints em api/produtos requerem autenticação.

---

🔍 Endpoints disponíveis
/api/produtos
Consulta todos os produtos cadastrados.

/api/produtos/{cest}
Busca por código CEST (com ou sem pontuação).

/api/produtos/ncm/{ncm}
Busca produtos por NCM — aceita 22011000 ou 2201.10.00.

/api/produtos/secao/{secao}
Busca por seções — aceita termos parciais como cerveja.

/api/produtos/search
Busca com múltiplos filtros:

GET /api/produtos/search?descricao=água&ncm=2201.10.00&cest=0300100

Todos os filtros são opcionais, mas pelo menos um deve ser informado.

---

📐 Validações
❌ Retornos 400 para campos obrigatórios ausentes

❌ Retornos 401 para acesso sem autenticação

✅ Uso de ProblemDetails para mensagens de erro padronizadas

✅ Campos de login validados com [Required]

🧠 Entidade principal
Representa o produto CEST com seus campos fiscais:

Secao

Cest

Ncm

Descricao

MvaOriginal

MvaSubstituto

MvaAjustada12

MvaAjustada4

---

📌 Próximos passos
[x] Criar endpoints REST com filtros flexíveis

[x] Implementar autenticação JWT

[x] Proteger rotas com [Authorize]

[x] Validar entradas com DataAnnotations

[x] Integração com Swagger para login e teste de token

[ ] Criar DTOs e filtros avançados

[ ] Criar controller de alíquotas

[ ] Adicionar testes unitários e integração

[ ] Suporte Docker e CI/CD com GitHub Actions + Azure

---

💼 Objetivo do projeto
Criado com foco em:

🚧 Organização e estrutura limpa

✅ Cobertura fiscal confiável (CEST/NCM/MVA)

🔐 Segurança por autenticação JWT

💼 Portfólio técnico demonstrável com deploy futuro em nuvem

🧩 Base para futuras integrações com sistemas fiscais, ERPs ou dashboards

---

🤝 Contribuições
Sugestões são sempre bem-vindas! Abra uma issue ou envie um pull request com melhorias.

---

📄 Licença
Distribuído sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

---


🧾 Desenvolvedor
Feito com 💡 e foco por @dangelofoliveira 🐘 PostgreSQL • ⚙️ .NET 9 • 🔐 JWT • ☁️ Azure-ready

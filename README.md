# 📦 Product NCM CEST API

API REST em .NET 9 para consulta de produtos com códigos **NCM**, **CEST** e respectivos valores de **MVA**, com importação de dados via JSON e persistência em **PostgreSQL**. Desenvolvido com arquitetura modular e foco em clareza, escalabilidade e portfólio profissional.

---

## 🚀 Tecnologias

- [.NET 9](https://dotnet.microsoft.com)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [PostgreSQL](https://www.postgresql.org/)
- [System.Text.Json](https://learn.microsoft.com/dotnet/api/system.text.json)
- [Arquitetura por camadas](#-estrutura-do-projeto)

---

## 🗂️ Estrutura do projeto

ProductNcmCestAPI/ ├── CestNcm.API/ → [em breve] API REST com endpoints HTTP ├── CestNcm.Domain/ → Entidades e regras de domínio ├── CestNcm.Infrastructure/ → DbContext + persistência (PostgreSQL + EF Core) ├── CestNcm.DataImporter/ → Console App para importar JSON ├── dados_cest.json → Base de dados oficial └── README.md → Você está aqui :)


---

## 📥 Importador JSON

Importa centenas de registros de forma automatizada com validação básica:

```bash
dotnet run --project CestNcm.DataImporter
```
✅ Lê o arquivo dados_cest.json

🛠️ Preenche os campos CEST, NCM, Descrição, e MVAs

🧼 Ignora automaticamente registros inválidos ou incompletos

📊 Exibe contagem final de inserções

---

## 🐘 Banco de dados PostgreSQL
Configure a string de conexão no appsettings.json do projeto CestNcm.DataImporter:

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=cestncm;Username=seu_usuario;Password=sua_senha"
  }
}

> Certifique-se de que o banco cestncm esteja criado e acessível localmente.

---

## 🧠 Entidade principal

A entidade ProdutoCest reflete com fidelidade os campos do JSON:

Secao

Cest

Ncm

Descricao

MvaOriginal

MvaSubstituto

MvaAjustada12

MvaAjustada4

> O importador mapeia os valores condicionalmente com base na presença de campos como mva_substituto ou mva_ajustada_X.

---

## 📌 Próximos passos
[ ] Criar os primeiros endpoints REST (/produtos)

[ ] Adicionar DTOs e FluentValidation

[ ] Gerar documentação Swagger

[ ] Adicionar testes unitários e integração

[ ] Suporte Docker e CI/CD

💡 Objetivo do projeto
Criado com foco em:

🚧 Organização e estrutura limpa

✅ Cobertura fiscal confiável (CEST/NCM)

💼 Portfólio técnico demonstrável

🧩 Base para futuras integrações com sistemas fiscais ou ERP

🤝 Contribuições
Se quiser sugerir melhorias, correções ou evoluções (como filtros por NCM/Descrição), fique à vontade para abrir uma issue ou PR.

📄 Licença
Distribuído sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

Desenvolvido por @dangelofoliveira 🧾🐘🚀

# Frameworks Dev Web I - API

Este projeto é uma API ASP.NET Core para agendamento de pedidos, conectada ao banco Supabase.

## Como rodar localmente

1. Instale o .NET SDK 10 (preview) ou superior.

2. **Configure a senha do banco de dados:**
   - Copie o arquivo `.env.example` para `.env`
   - Edite o arquivo `.env` e configure sua senha do banco:
   ```
   DB_PASSWORD=sua_senha_real_aqui
   ```
   
3. Execute o comando:

```
dotnet run
```

### Onde fica minha declaração de senha?

A **senha do banco de dados** deve ser declarada no arquivo `.env` na raiz do projeto:

- 📁 **Para desenvolvimento local**: Crie um arquivo `.env` (baseado no `.env.example`)
- 🌐 **Para produção**: Configure a variável de ambiente `DB_PASSWORD` no seu provedor de hospedagem (Render, etc.)
- 🚫 **Nunca**: Coloque senhas reais no `appsettings.json` ou commit no Git

A aplicação carrega automaticamente as variáveis do arquivo `.env` e insere a senha na connection string do PostgreSQL.

A API estará disponível em http://localhost:5030 (ou na porta configurada).

## Projeto online

Acesse a API online em:

[https://frameworks-dev-web-i-1.onrender.com](https://frameworks-dev-web-i-1.onrender.com)

---

Para dúvidas ou sugestões, abra uma issue no repositório.

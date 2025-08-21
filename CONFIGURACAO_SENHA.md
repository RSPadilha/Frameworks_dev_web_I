# Configuração de Senha do Banco de Dados

## Onde fica minha declaração de senha?

### 🏠 Desenvolvimento Local
1. Copie o arquivo `.env.example` para `.env`
2. Edite o arquivo `.env` e configure:
   ```
   DB_PASSWORD=sua_senha_do_supabase
   ```

### 🌐 Produção (Render/Heroku/etc)
Configure a variável de ambiente `DB_PASSWORD` nas configurações do seu provedor:
- **Render**: Environment Variables
- **Heroku**: Config Vars
- **Azure**: Application Settings

### 🔒 Segurança
- ✅ **FAÇA**: Use arquivo `.env` para desenvolvimento
- ✅ **FAÇA**: Configure variáveis de ambiente em produção
- ❌ **NÃO FAÇA**: Coloque senhas no `appsettings.json`
- ❌ **NÃO FAÇA**: Commit arquivos `.env` com senhas reais

### 🔧 Como funciona?
1. O arquivo `Program.cs` carrega variáveis do `.env` via `DotNetEnv`
2. A senha é inserida na connection string automaticamente
3. Em produção, usa variáveis de ambiente do sistema

### 📁 Estrutura de arquivos
```
Frameworks_dev_web_I/
├── .env.example    ← Template para configuração
├── .env           ← Suas senhas locais (não commitado)
├── appsettings.json ← Connection string sem senha
└── Program.cs     ← Carrega DB_PASSWORD
```
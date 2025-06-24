# Barbearia Pro TESTE

Projeto de sistema SaaS para agendamento e gerenciamento de barbearias.

---

## 🛠️ Tecnologias Utilizadas

- **Backend:** ASP.NET Core Web API (.NET 6) com Clean Architecture
- **Frontend:** Angular 16
- **Banco de Dados:** PostgreSQL
- **Infraestrutura:** Docker, AWS
- **CI/CD:** GitHub Actions

---

## 🌿 Fluxo de Git

Este repositório utiliza um fluxo de versionamento controlado para garantir estabilidade e qualidade nas entregas.

### Branches principais:

| Branch | Descrição |
|--------|-----------|
| `main` | Produção |
| `development` | Homologação |
| `feature/*` | Desenvolvimento de funcionalidades |
| `hotfix/*` | Correções emergenciais |

---

## 🚀 Fluxo de Desenvolvimento

1. Sempre criar novas funcionalidades a partir da branch `development`.
2. Criar uma nova branch com o padrão `feature/nome-da-feature`.
3. Após desenvolvimento, abrir Pull Request para `development`.
4. Quando a `development` estiver estável, abrir Pull Request para `main` (liberação em produção).
5. O merge na `main` gera uma release automática via GitHub Actions.

---

## ✅ Proteção de Branches

- `main`: Proteção máxima (PR obrigatório, reviewers, bloqueio de push direto)
- `development`: Proteção forte (PR obrigatório, reviewers, bloqueio de push direto)

---

## 🎯 Modelo de Pull Request

Sempre utilize o template padrão ao abrir um Pull Request:

```markdown
## Descrição

Descreva resumidamente a funcionalidade ou correção.

## Tipo de mudança

- [ ] Nova feature
- [ ] Correção de bug
- [ ] Melhoria
- [ ] Refatoração

## Checklist

- [ ] Código testado localmente
- [ ] Review solicitado
- [ ] Testes automatizados (se aplicável)
- [ ] Não há código comentado ou morto

## Observações

Adicione observações adicionais se necessário.

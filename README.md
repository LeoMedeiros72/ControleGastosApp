![GitHub release (latest by date)](https://img.shields.io/github/v/release/LeoMedeiros72/ControleGastosApp)
![GitHub last commit](https://img.shields.io/github/last-commit/LeoMedeiros72/ControleGastosApp)
![GitHub repo size](https://img.shields.io/github/repo-size/LeoMedeiros72/ControleGastosApp)
![GitHub issues](https://img.shields.io/github/issues/LeoMedeiros72/ControleGastosApp)

# ControleGastosApp

Aplicativo simples de **controle de gastos pessoais**, desenvolvido com **.NET MAUI**.  
O objetivo principal é oferecer uma forma **rápida, offline e prática** de registrar despesas no dia a dia, com futura integração a **PostgreSQL** e **inteligência artificial** para sugerir categorias automaticamente, comparar seus gastos e fazer perguntas estratégicas como:
> Quanto eu gastei com Mercado nesse ano?
> Quanto eu gastei no crédito esse ano?
> Compare meus gastos de energia desse mês com o mesmo mês do ano passado

## Funcionalidades Atuais

- ✅ Inserção de novos gastos com:
  - Valor
  - Local onde foi gasto
  - Forma de pagamento (Dinheiro, Crédito, Débito, Pix)
- ✅ Exibição da lista de gastos cadastrados
- ✅ Cálculo automático do **total gasto**
- ✅ Remoção e edição de gastos
- ✅ Tema escuro para maior conforto visual
- ✅ Sincronização opcional com banco **PostgreSQL** (via botão)
- ✅ Prevenção contra duplicidade de gastos no banco


## Interface

- Tema escuro com fundo preto  
- Título em **amarelo**  
- Botão **Adicionar Gasto** em verde escuro  
- Entradas e texto em **branco**  

## Tecnologias Utilizadas

- [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/) — framework multiplataforma
- [C#](https://learn.microsoft.com/pt-br/dotnet/csharp/)
- [XAML](https://learn.microsoft.com/en-us/dotnet/maui/xaml/)
- [SQLite](https://www.sqlite.org/index.html) — armazenamento local
- [PostgreSQL](https://www.postgresql.org/) — sincronização de dados via container Docker

## Captura de tela

![Imagem do WhatsApp de 2025-07-12 à(s) 13 19 12_77cbea2b](https://github.com/user-attachments/assets/0b63fe90-bf97-49c0-ab3d-4791f27c9a69)
<img width="538" height="428" alt="image" src="https://github.com/user-attachments/assets/495f1367-533d-4dad-aaa3-e843a540c9b3" />
<img width="527" height="566" alt="image" src="https://github.com/user-attachments/assets/f7aee6c5-33ab-41c2-a91c-592b90d65018" />

## Histórico de versões

### v1.0.0 

Cadastro de gastos com:

- Valor

- Local onde foi gasto

- Forma de pagamento (Dinheiro, Crédito, Débito, Pix)

- Exibição da lista de gastos cadastrados.

- Cálculo automático do total gasto.

- Exclusão de gastos individualmente.

- Interface inicial com tema escuro.

- Distribuição do primeiro .apk para Android.

### v1.1.1 — (2025-06-01)

- Integração com PostgreSQL para sincronização dos dados cadastrados offline no SQLite.

- Implementada verificação de duplicidade antes de enviar os gastos para o Postgres.

- Adicionadas categorias e subcategorias automáticas e personalizadas (com fallback para “Outros”).

- Melhorias na interface de listagem de gastos (mostrando local e forma de pagamento).


## 🚀 Como executar

1. Clone este repositório:
```
git clone https://github.com/LeoMedeiros72/ControleGastosApp.git
cd ControleGastosApp
```
2. Abra o projeto no Visual Studio 2022 ou superior.

3. Selecione a plataforma desejada (Android, Windows, etc).
   
4. Execute o projeto (Ctrl + F5 ou botão “Executar”).


## Instale o .APK no seu Celular Android (versão 1.0)

Versão compatível com **Android 5.0 (API 21)** ou superior.

👉 [📲 Baixar APK (Release v1.1.0)](https://github.com/LeoMedeiros72/ControleGastosApp/releases/tag/v1.1.0)

Ou escaneie o QR Code abaixo para abrir a página da Release:

<img src="https://api.qrserver.com/v1/create-qr-code/?size=250x250&data=https://github.com/LeoMedeiros72/ControleGastosApp/releases/download/v1.1.0/com.companyname.controlegastosapp-Signed.apk" width="250" alt="QR Code para download do APK" />

Como instalar:

1. Baixe o .apk clicando no link ou escaneando o QR Code acima.

2. Copie para seu celular (se necessário).

3. Ative a opção “Permitir instalação de fontes desconhecidas” nas configurações do Android.

Toque no arquivo e siga os passos para concluir a instalação.

⚠️ Esta versão não está disponível na Google Play Store.

## Futuras Melhorias

1. Inteligência Artificial para sugerir categorias/subcategorias automaticamente

2. Gráficos e relatórios de gastos por período

3. Sincronização em nuvem para multiplataforma

4. Categorias e Subcategorias dinâmicas (ex.: Supermercado → Compra mensal / Compra extra)

5. Alertas inteligentes quando os gastos ultrapassarem o planejado

6. Pergunte a IA: Faça perguntas pertinentes e a IA vai responder de acordo com seus gastos

## Contribuição

### Contribuições são bem-vindas!

1. Faça um fork do projeto

2. Crie sua branch (git checkout -b feature/minha-feature)

3. Commit suas alterações (git commit -m 'Minha nova feature')

4. Faça push (git push origin feature/minha-feature)

5. Abra um Pull Request 

## Licença

Este projeto está sob a licença MIT.
Consulte o arquivo LICENSE



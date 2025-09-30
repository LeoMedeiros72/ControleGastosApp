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


## 🚀 Como executar

1. Clone este repositório:
```
git clone https://github.com/LeoMedeiros72/ControleGastosApp.git
cd ControleGastosApp
```
2. Abra o projeto no Visual Studio 2022 ou superior.

3. Selecione a plataforma desejada (Android, Windows, etc).
   
4. Execute o projeto (Ctrl + F5 ou botão “Executar”).


## Instale o .APK no seu Celular Android

Versão compatível com Android 5.0 (API 21) ou superior.

[Download direto do APK](https://github.com/LeoMedeiros72/ControleGastosApp/releases/download/v1.0.0/ContoleGastosApp-v1.0.0-arm64.apk)

Ou escaneie o QR Code:

<img src="https://github.com/user-attachments/assets/542bfe8b-f5a2-4699-90f3-b918480a3937" width="250" alt="QR Code para download do APK" />

Como instalar:
Baixe o .apk clicando no link ou escaneando o QR Code acima.

Copie para seu celular (se necessário).

Ative a opção “Permitir instalação de fontes desconhecidas” nas configurações do Android.

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



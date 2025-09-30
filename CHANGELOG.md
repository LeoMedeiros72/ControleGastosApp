# 📌 Changelog

Todas as mudanças notáveis neste projeto serão documentadas neste arquivo.

O formato segue o padrão [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/), 
e este projeto adota o versionamento [SemVer](https://semver.org/lang/pt-BR/).

---

## [1.2.0] - Em desenvolvimento
### Adicionado
- Suporte a **categorias e subcategorias** personalizadas.
- Gráfico de pizza simples para exibir a distribuição dos gastos por categoria.
- Sincronização mais robusta entre **SQLite** e **PostgreSQL**.

### Corrigido
- Erro de duplicação ao sincronizar gastos no Postgres.
- Ajustes visuais na exibição da lista de gastos.

---

## [1.1.0] - 2025-09-30
### Adicionado
- Integração com **PostgreSQL** para sincronização dos dados cadastrados offline no SQLite.  
- Verificação para evitar **duplicidade de gastos**.  
- Categorias e subcategorias automáticas (fallback para "Outros").  
- Interface de listagem de gastos exibindo **local** e **forma de pagamento**.  
- Suporte a execução no **Windows Desktop**.  

---

## [1.0.0] - 2025-07-12
### Adicionado
- Cadastro de gastos com:
  - Valor  
  - Local  
  - Forma de pagamento (Dinheiro, Crédito, Débito, Pix)  
- Exibição da lista de gastos cadastrados.  
- Cálculo automático do total gasto.  
- Exclusão individual de gastos.  
- Interface inicial com **tema escuro**.  
- Primeiro `.apk` distribuído via GitHub Release.  

---

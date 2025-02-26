# Equipamento Eletrônico

Este projeto é uma aplicação ASP.NET Core que combina MVC e API para gerenciar equipamentos eletrônicos. Ele inclui uma interface web para interação com os usuários e uma API documentada com Swagger para facilitar a integração com outros sistemas.

## Tecnologias Utilizadas

- **ASP.NET Core** (MVC e API)
- **Entity Framework Core** (com banco de dados em memória para testes)
- **FluentValidation** (validação de dados)
- **Swagger** (documentação da API)

## Funcionalidades

- **Gerenciamento de Equipamentos:** Criar, editar, listar e excluir equipamentos.
- **Interface Web:** Páginas para interação com os dados.
- **API REST:** Endpoints para integração com outros sistemas.
- **Tratamento de Exceções:** Middleware para capturar e padronizar erros.
- **Versionamento de API:** Controle de versões para manter compatibilidade.

## Configuração e Execução

1. **Clone o repositório:**
   ```sh
   git clone https://github.com/MitchelBierman/equipamento-eletronico.git
   ```
2. **Acesse a pasta do projeto:**
   ```sh
   cd equipamento-eletronico
   ```
3. **Instale as dependências:**
   ```sh
   dotnet restore
   ```
4. **Execute o projeto:**
   ```sh
   dotnet run
   ```
5. **Acesse a aplicação:**
   - Interface web: `http://localhost:5000`
   - Documentação Swagger: `http://localhost:5000/swagger`

## Endpoints Principais

- `GET /api/equipamentos` – Lista todos os equipamentos
- `GET /api/equipamentos/{id}` – Obtém um equipamento pelo ID
- `POST /api/equipamentos` – Adiciona um novo equipamento
- `PUT /api/equipamentos/{id}` – Atualiza um equipamento existente
- `DELETE /api/equipamentos/{id}` – Remove um equipamento

## Estrutura do Projeto

```
├── EquipamentoEletronico.Application
├── EquipamentoEletronico.Domain
├── EquipamentoEletronico.Infrastructure
├── EquipamentoEletronico.Controllers
│   ├── EquipamentoController.cs
│   ├── Api
│   │   ├── EquipamentoApiController.cs
├── Views
│   ├── Equipamento
│   │   ├── Index.cshtml
│   │   ├── Criar.cshtml
│   │   ├── Editar.cshtml
│   │   ├── Detalhes.cshtml
│   │   ├── Error.cshtml
├── Middleware
│   ├── ExceptionHandlingMiddleware.cs
```

## Contribuição

Se quiser contribuir, sinta-se à vontade para abrir um Pull Request ou relatar problemas na aba de Issues.

## Aviso

Este é um projeto pessoal e pode ser utilizado e modificado conforme necessário.
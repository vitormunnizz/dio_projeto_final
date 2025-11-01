# 🛒 E-commerce Simples: Evolução para Microservices

Este repositório contém a implementação de um sistema de gerenciamento de **Estoque e Vendas** para e-commerce, construído com **.NET Core Minimal APIs**.

## 🧠 Tecnologias Utilizadas (Implementação Atual)

  * **.NET Core Minimal APIs (C\#)** – Para a construção dos endpoints HTTP leves.
  * **Entity Framework Core** – ORM.
  * **SQLite** – Banco de dados leve e local (`ecommerce.db`).
  * **Swagger/OpenAPI** – Para documentação e teste dos endpoints.

## 🎯 Objetivo do Desafio (Próximos Passos)

O objetivo final é a **divisão** do código atual em componentes independentes, introduzindo resiliência e escalabilidade:

1.  **Microservice.Estoque:** Responsável apenas por `/products` e lógica de quantidade.
2.  **Microservice.Vendas:** Responsável apenas por `/orders` e a lógica de pedidos.
3.  **API Gateway:** Para rotear as chamadas externas.
4.  **RabbitMQ:** Para comunicação **assíncrona** (Venda notifica Estoque).
5.  **JWT:** Para autenticação em todos os serviços.

## 📂 Estrutura do Projeto (Atual)

O projeto está configurado como um **monolito simples** que gerencia todas as entidades.

```
ecommerce-simples/
├── bin/
├── Data/                # AppDb.cs (DbContext) e Migrations
├── Models/              # Product.cs e Order.cs (Entidades)
├── appsettings.json
├── Program.cs           # Configuração e todos os Endpoints (Minimal APIs)
└── dio.sln              # Arquivo de Solução
```

## ⚙️ Como Executar o Projeto

O projeto utiliza um banco de dados SQLite local, o que facilita a execução.

**Pré-requisitos:** Certifique-se de ter o **.NET Core SDK (6.0+)** instalado.

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/ecommerce-simples.git

# Acessar o diretório
cd ecommerce-simples

# Restaurar dependências
dotnet restore

# Executar o projeto (cria ou usa o arquivo ecommerce.db)
dotnet run
```

*Após a execução, acesse `http://localhost:[Porta]/swagger` para testar os endpoints.*

## 💻 Funcionalidades Implementadas

O **`Program.cs`** centraliza a lógica síncrona de Estoque e Vendas:

| Funcionalidade | Método / Endpoint | Detalhe da Lógica |
| :--- | :--- | :--- |
| **Estoque** | `POST /products` | Cadastro de novos produtos. |
| **Estoque** | `GET /products` | Listagem e Consulta individual de produtos. |
| **Vendas** | `POST /orders` | **Validação de Estoque:** Verifica se `Product.Quantity >= Order.Quantity`. |
| **Vendas** | `POST /orders` | **Redução de Estoque:** Atualiza `Product.Quantity -= Order.Quantity` no mesmo `Db.SaveChangesAsync()`. |
| **Vendas** | `GET /orders` | Listagem de pedidos. |

## 🧱 Próximos Passos do Desafio

As seguintes melhorias são necessárias para cumprir o requisito de Microserviços:

1.  **Refatoração para Microserviços:** Separar o código em dois projetos distintos: **Estoque** e **Vendas**.
2.  **Implementar RabbitMQ:** Substituir a validação/redução de estoque síncrona por uma comunicação **assíncrona** via mensageria.
3.  **Implementar Autenticação JWT:** Proteger todos os endpoints.
4.  **Configurar API Gateway (Ocelot):** Para rotear o tráfego externo para os novos serviços.

## 👨‍💻 Autor

**Vitor Hugo Muniz de Sousa Santos**

💼 Engenheiro da Computação | Desenvolvedor Front-end

📧 [vitormunnizzdev@gmail.com](mailto:vitormunnizzdev@gmail.com)
🌐 [www.linkedin.com/in/vitormunnizz](https://www.linkedin.com/in/vitormunnizz)

## 📝 Licença

Este projeto está licenciado sob a **MIT License**.
Sinta-se livre para usar e modificar conforme necessário, mantendo os créditos ao autor.

⭐ **Se este projeto te ajudou, deixe uma estrela no repositório\!**
O `README` agora está 100% alinhado com o seu código atual, ao mesmo tempo que enquadra o projeto dentro do contexto de **evolução para Microserviços**.

Gostaria que eu detalhasse a primeira etapa do desafio, que é **separar a lógica de Estoque da lógica de Vendas** em projetos .NET Core distintos?

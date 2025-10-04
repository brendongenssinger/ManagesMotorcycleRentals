# 🐳 Docker & Docker Compose
Os arquivos encontra-se no projeto, ManagesMotorcycleRentals.API .

Executando o comando :
`docker-compose up` é criado os container.

# 🏍️ Manages Motorcycle Rentals API

API desenvolvida em **.NET 8** para gerenciar o aluguel de motocicletas, com autenticação via **JWT**, mensageria com **RabbitMQ** e consumo assíncrono via **Worker Service**.

---

## 🔐 Autenticação e Roles

A autenticação é feita via **JWT Bearer Token**, e cada token é gerado com uma **role** específica:

| Role  | Permissão | Descrição |
|-------|------------|-----------|
| `admin` | Acesso ao controller de **motorcycle** | Pode cadastrar, listar, atualizar e deletar motocicletas |
| `user`  | Acesso ao controller de **customers (entregadores)** | Pode cadastrar entregadores e criar alocações de motos |

Para gerar o token, utilize o **UserController** (rota `/api/v1/user/login`), enviando:

```json
{
  "id": 1,
  "role": "admin"
}
```
OU para o usuário : 
```json
{
  "id": 2,
  "role": "user"
}
```

# 🐇 Worker & RabbitMQ
📦 Projeto: ManagesMotorcycleRentals.Worker

Atua como consumer do RabbitMQ.

Sempre que uma moto é cadastrada no sistema:

Um evento é publicado no RabbitMQ (mensagem com dados da moto).

O Worker consome essa mensagem.

Se o ano da moto for 2024, ele efetua o cadastro da moto no banco.

As mensagens são persistidas para consulta posterior.

# 🧱 Arquitetura DDD ( Domain - Driven - Design )

A arquitetura do projeto foi desenvolvida seguindo o modelo DDD (Domain-Driven Design)

Com uma separação clara entre services (responsáveis pela lógica de aplicação) e repositories (responsáveis pelo acesso e persistência de dados). 

Essa abordagem garante melhor organização, manutenção e testabilidade do código, além de favorecer a escalabilidade da aplicação.

# 🧩 Testes Unitários

Está no projeto ManagesMotorcycleRentals.Tests .

# 🌐 Postman

Anexado o arquivo do postman para ajudar [Link Aqui](https://github.com/brendongenssinger/ManagesMotorcycleRentals/tree/master/Postman).

Ou dentro da pasta Postman


____
✍️ Autor: Brendon Mascarenhas
📅 Versão: 1.0.0


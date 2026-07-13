# Digital Wallet & Remittance Platform

A production-grade Digital Wallet and Remittance Platform built with .NET 10, ASP.NET Core, Clean Architecture, Domain-Driven Design, PostgreSQL, EF Core, and modern distributed-systems principles.

This project is being built step by step as an enterprise-grade backend platform similar in architectural direction to systems used by Wise, Stripe, PayPal, Revolut, Remitly, and other financial technology companies.

## Project Goal

The goal of this project is to build a secure, scalable, testable, and production-ready backend platform for digital wallet and remittance operations.

The platform will eventually support:

- Customer onboarding
- KYC and compliance checks
- Multi-currency wallets
- Wallet lifecycle management
- Double-entry ledger
- Wallet-to-wallet transfers
- Pay-ins
- Payouts
- Cross-border remittance
- FX quotation and pricing
- Provider integrations
- Event-driven communication
- Notifications
- Reconciliation
- Auditing
- Observability
- Secure deployment to cloud infrastructure

---

## Current Implementation Status

The project currently includes:

- .NET 10 solution setup
- Modular monolith architecture
- Clean Architecture boundaries
- Wallet bounded context
- Shared domain building blocks
- Wallet aggregate
- Wallet lifecycle model
- Strongly typed identifiers
- Currency value object
- Wallet suspension reason value object
- Wallet lifecycle domain events
- PostgreSQL local container
- EF Core persistence for Wallet aggregate
- PostgreSQL schema for Wallets module
- Unique customer/currency wallet constraint
- Optimistic concurrency using PostgreSQL `xmin`
- Domain unit tests
- Architecture tests
- API health checks
- API integration tests
- PostgreSQL integration tests using Testcontainers

CI/CD, authentication, APIs, ledger, Kafka, Redis, and Kubernetes will be added in later lessons.

---

## Technology Stack

### Backend

- .NET 10
- ASP.NET Core
- C#
- Clean Architecture
- Domain-Driven Design
- CQRS planned
- Vertical Slice Architecture planned
- MediatR planned
- FluentValidation planned

### Database

- PostgreSQL
- EF Core
- Dapper planned for optimized read queries where appropriate

### Messaging

Planned:

- Kafka
- RabbitMQ

### Caching

Planned:

- Redis

### Communication

Planned:

- REST APIs
- gRPC

### Observability

Planned:

- OpenTelemetry
- Serilog
- Prometheus
- Grafana

### Security

Planned:

- JWT
- OAuth2
- mTLS

### DevOps

Currently:

- Docker Compose for local PostgreSQL

Planned:

- GitHub Actions
- Azure DevOps
- Docker
- Kubernetes
- Azure

### Testing

Currently:

- xUnit
- Architecture tests
- Unit tests
- Integration tests
- Testcontainers

Planned:

- Performance testing
- Contract testing
- Security testing

---

## Architecture Style

The project currently uses a Modular Monolith architecture.

A modular monolith means the application is deployed as one system, but internally it is divided into clear business modules.

This gives us:

- Simple deployment at the beginning
- Strong module boundaries
- Easier debugging
- Local database transactions
- Lower operational complexity
- A safe path to microservices later

We are not starting with microservices immediately because the domain boundaries are still evolving. Starting with microservices too early would introduce unnecessary distributed-systems complexity.

---

## Solution Structure

```text
DigitalWallet
├── src
│   ├── DigitalWallet.Api
│   │   ├── Health
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── BuildingBlocks
│   │   ├── DigitalWallet.BuildingBlocks.Domain
│   │   └── DigitalWallet.BuildingBlocks.Application
│   │
│   └── Modules
│       └── Wallets
│           ├── DigitalWallet.Modules.Wallets.Domain
│           ├── DigitalWallet.Modules.Wallets.Application
│           ├── DigitalWallet.Modules.Wallets.Infrastructure
│           └── DigitalWallet.Modules.Wallets.Presentation
│
├── tests
│   ├── DigitalWallet.ArchitectureTests
│   ├── DigitalWallet.Api.UnitTests
│   ├── DigitalWallet.Api.IntegrationTests
│   ├── DigitalWallet.Modules.Wallets.Domain.UnitTests
│   └── DigitalWallet.Modules.Wallets.Infrastructure.IntegrationTests
│
├── docs
│   └── architecture
│
├── compose.yaml
├── global.json
├── Directory.Build.props
└── README.md
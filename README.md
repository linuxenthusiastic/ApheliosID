# 🔗 ApheliosID - Sistema de Identidad Descentralizada

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=.net)
![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=c-sharp)
![License](https://img.shields.io/badge/License-MIT-green)

Sistema de identidades descentralizadas (DIDs) basado en blockchain, desarrollado con arquitectura limpia y principios SOLID.

---

## 📋 Tabla de Contenidos

- [Descripción](#-descripción)
- [Características](#-características)
- [Arquitectura](#-arquitectura)
- [Tecnologías](#-tecnologías)
- [Instalación](#-instalación)
- [Uso](#-uso)
- [Endpoints API](#-endpoints-api)
- [Principios SOLID Aplicados](#-principios-solid-aplicados)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Autor](#-autor)

---

## 🎯 Descripción

**ApheliosID** es un sistema de identidad descentralizada que utiliza blockchain para garantizar:

- ✅ **Inmutabilidad**: Las identidades no pueden ser alteradas
- ✅ **Descentralización**: No hay autoridad central que controle las identidades
- ✅ **Transparencia**: Todas las operaciones son auditables
- ✅ **Seguridad**: Criptografía RSA para autenticación

### ¿Qué es un DID (Decentralized Identifier)?

Un DID es un identificador único que **tú controlas**, a diferencia de:
- Email (controlado por Gmail/Outlook)
- Username (controlado por Twitter/Instagram)
- CI (controlado por el gobierno)

**Ejemplo de DID:** `did:aphelios:a1b2c3d4e5f6g7h8`

---

## ✨ Características

### Blockchain
- 🔗 Bloques enlazados mediante hash SHA256
- 🔒 Validación de integridad automática
- 📦 Auto-cierre de bloques (configurable)
- 🚫 Sin minería (blockchain privada)

### API REST
- 📡 7 endpoints documentados
- 📚 Swagger UI integrado
- ✔️ Validación automática de datos
- 🔍 Logging completo

---

## 🏗️ Arquitectura

El proyecto sigue **Arquitectura en Capas** con **Principios SOLID**:
```
┌─────────────────────────────────────────┐
│         PRESENTATION LAYER              │
│      (Controllers + DTOs)               │
└──────────────┬──────────────────────────┘
               │
               ▼
┌─────────────────────────────────────────┐
│         SERVICE LAYER                   │
│    (IBlockchainService → Impl)          │
└──────────────┬──────────────────────────┘
               │
               ▼
┌─────────────────────────────────────────┐
│         MANAGER LAYER                   │
│  (BlockchainManager, TransactionPool,   │
│   BlockFactory, BlockValidator)         │
└──────────────┬──────────────────────────┘
               │
               ▼
┌─────────────────────────────────────────┐
│         MODEL LAYER                     │
│    (Block, Transaction)                 │
└─────────────────────────────────────────┘
```

### Separación de Responsabilidades

| Componente | Responsabilidad |
|------------|----------------|
| **BlockchainManager** | Gestionar la lista de bloques |
| **TransactionPool** | Gestionar transacciones pendientes |
| **BlockFactory** | Crear bloques nuevos |
| **BlockValidator** | Validar bloques y cadena |
| **BlockchainService** | Orquestar todos los componentes |

---

## 🛠️ Tecnologías

- **Lenguaje:** C# 12.0
- **Framework:** .NET 9.0
- **API:** ASP.NET Core Web API
- **Documentación:** Swagger/OpenAPI
- **Criptografía:** RSA 2048 bits, SHA256
- **Arquitectura:** Clean Architecture + SOLID

---

## 📥 Instalación

### Prerrequisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Editor: Visual Studio Code, Visual Studio 2022, o Rider

### Pasos

1. **Clonar el repositorio**
```bash
git clone https://github.com/tu-usuario/ApheliosID.git
cd ApheliosID
```

2. **Restaurar dependencias**
```bash
dotnet restore
```

3. **Compilar el proyecto**
```bash
dotnet build
```

4. **Ejecutar la API**
```bash
cd ApheliosID.API
dotnet run
```

5. **Abrir Swagger UI**

Navega a: `http://localhost:5141`

---

### Ver la Blockchain
```bash
curl http://localhost:5141/api/blockchain
```

### Validar la Cadena
```bash
curl http://localhost:5141/api/blockchain/validate
```

---

## 📡 Endpoints API

### Blockchain

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/blockchain` | Obtener toda la cadena |
| GET | `/api/blockchain/stats` | Estadísticas generales |
| GET | `/api/blockchain/block/{index}` | Obtener bloque específico |
| GET | `/api/blockchain/latest` | Obtener último bloque |
| GET | `/api/blockchain/validate` | Validar integridad |
| POST | `/api/blockchain/force-block` | Forzar creación de bloque |

### Transacciones

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/transaction` | Crear transacción |

---

## 🎓 Principios SOLID Aplicados

### 1️⃣ Single Responsibility Principle (SRP)

Cada clase tiene **una sola responsabilidad**:
```csharp
// ✅ BIEN: Una responsabilidad
public class BlockValidator
{
    public bool ValidateChain(IReadOnlyList<Block> chain) { }
}

// ❌ MAL: Múltiples responsabilidades
public class Blockchain
{
    public void AddBlock() { }
    public void ValidateChain() { }
    public void AddTransaction() { }
    public void CreateBlock() { }
}
```

### 2️⃣ Open/Closed Principle (OCP)

Abierto para extensión, cerrado para modificación:
```csharp
// Se puede extender BlockValidator sin modificar el código existente
public class CustomBlockValidator : BlockValidator
{
    public override bool ValidateBlock(Block block)
    {
        // Lógica personalizada
    }
}
```

### 3️⃣ Liskov Substitution Principle (LSP)

Las implementaciones pueden substituirse:
```csharp
IBlockchainService service = new BlockchainService();
// O en tests:
IBlockchainService service = new MockBlockchainService();
```

### 4️⃣ Interface Segregation Principle (ISP)

Interfaces específicas, no "gordas":
```csharp
// ✅ BIEN: Interfaz específica
public interface IBlockchainService
{
    Block AddTransaction(Transaction tx);
    bool IsChainValid();
}

// ❌ MAL: Interfaz con métodos no relacionados
public interface IGodService
{
    Block AddTransaction();
    User LoginUser();
    Email SendEmail();
}
```

### 5️⃣ Dependency Inversion Principle (DIP)

Depender de abstracciones, no implementaciones:
```csharp
// ✅ BIEN: Depende de interfaz
public class BlockchainController
{
    private readonly IBlockchainService _service;
}

// ❌ MAL: Depende de implementación concreta
public class BlockchainController
{
    private readonly BlockchainService _service;
}
```

---

## 📁 Estructura del Proyecto
```
ApheliosID/
├── ApheliosID.Core/              # Lógica de negocio
│   ├── Models/                   # Entidades del dominio
│   │   ├── Block.cs
│   │   ├── Transaction.cs
│   │   └── HashCalculator.cs
│   ├── Managers/                 # Gestores especializados
│   │   ├── BlockchainManager.cs
│   │   ├── TransactionPool.cs
│   │   ├── BlockFactory.cs
│   │   └── BlockValidator.cs
│   ├── Services/                 # Servicios orquestadores
│   │   └── BlockchainService.cs
│   └── Interfaces/               # Contratos
│       └── IBlockchainService.cs
│
├── ApheliosID.API/               # Capa de presentación
│   ├── Controllers/              # Endpoints HTTP
│   │   ├── BlockchainController.cs
│   │   ├── TransactionController.cs
│   │   └── IdentityController.cs
│   ├── DTOs/                     # Objetos de transferencia
│   │   ├── CreateTransactionDto.cs
│   │   ├── TransactionResponseDto.cs
│   │   └── BlockResponseDto.cs
│   └── Program.cs                # Configuración
│
└── ApheliosID.ConsoleTest/       # Proyecto de pruebas
    └── Program.cs
```

---


---

## 📊 Buenas Prácticas Implementadas

### 1. Encapsulamiento
```csharp
// Campos privados, acceso controlado
private readonly List<Block> _chain;
public IReadOnlyList<Block> GetChain() => _chain.AsReadOnly();
```

### 2. Inmutabilidad
```csharp
// Propiedades de solo lectura
public string Hash { get; }
public DateTime Timestamp { get; }
```

### 3. Validaciones
```csharp
if (transaction == null)
    throw new ArgumentNullException(nameof(transaction));
```

### 4. Comentarios XML
```csharp
/// <summary>
/// Valida la integridad del bloque
/// </summary>
/// <returns>True si el bloque es válido</returns>
public bool IsValid() { }
```

### 5. Inyección de Dependencias
```csharp
public BlockchainController(IBlockchainService service)
{
    _service = service;
}
```

---

## 👨‍💻 Autor

**Tu Nombre**
- GitHub: [@linuxenthusiastic](https://github.com/tu-usuario)
- Email: abuawadsantiago@gmail.com
- Universidad: UCB - Ingeniería de Software

---

## 📄 Licencia

Este proyecto es parte de un trabajo académico para el curso de Programación Orientada a Objetos.

---

## 📝 Notas del Proyecto

### Decisiones de Diseño

1. **Sin minería**: Blockchain privada, no requiere proof-of-work
2. **Auto-cierre**: Bloques se crean automáticamente al alcanzar N transacciones
4. **SHA256**: Algoritmo estándar para hashing

### Limitaciones Actuales

- Sin persistencia en base de datos (se reinicia al apagar)
- Sin autenticación de usuarios en la API
- Sin red P2P (blockchain centralizada)

### Mejoras Futuras

- [ ] Persistencia con PostgreSQL
- [ ] Sistema de autenticación JWT
- [ ] Credenciales verificables
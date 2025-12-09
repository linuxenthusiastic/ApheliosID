# 🔐 ApheliosID - Blockchain de Identidades Descentralizadas

> Sistema de identidades descentralizadas (DIDs) y credenciales verificables con blockchain inmutable

[![.NET 9.0](https://img.shields.io/badge/.NET-9.0-512BD4)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

---

## 📋 Tabla de Contenidos

- [Características](#características)
- [Arquitectura](#arquitectura)
- [Tecnologías](#tecnologías)
- [Instalación](#instalación)
- [Uso](#uso)
- [Endpoints API](#endpoints-api)
- [Seguridad](#seguridad)
- [Ejemplos](#ejemplos)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Conceptos OOP](#conceptos-oop)
- [Principios SOLID](#principios-solid)

---

## ✨ Características

### 🔗 Blockchain Inmutable
- Cadena de bloques con proof-of-work
- Transacciones agrupadas en bloques
- Historial completo y auditable
- Validación de integridad

### 🆔 Identidades Descentralizadas (DIDs)
- Generación de DIDs basados en claves públicas
- Criptografía asimétrica RSA 2048
- Zero-Knowledge: servidor nunca ve claves privadas
- Registro inmutable en blockchain

### 🎓 Credenciales Verificables
- Emisión de credenciales firmadas digitalmente
- Verificación sin intermediarios
- Revocación de credenciales
- Jerarquía de tipos (Academic, Professional, Certification)

### 🔐 Autenticación JWT
- Challenge-response sin contraseñas
- Firma digital con clave privada
- Tokens con expiración configurable
- Herramienta de firmado externa

---

## 🏗️ Arquitectura
```
┌─────────────────────────────────────────────────┐
│                  API REST                       │
│         (Controllers + Swagger UI)              │
└────────────────┬────────────────────────────────┘
                 │
┌────────────────▼────────────────────────────────┐
│              SERVICES LAYER                     │
│  ┌──────────┐ ┌──────────┐ ┌──────────────┐   │
│  │Identity  │ │Credential│ │  Blockchain  │   │
│  │Service   │ │Service   │ │   Service    │   │
│  └──────────┘ └──────────┘ └──────────────┘   │
│  ┌──────────┐ ┌──────────┐                     │
│  │  Crypto  │ │   Auth   │                     │
│  │ Service  │ │ Service  │                     │
│  └──────────┘ └──────────┘                     │
└────────────────┬────────────────────────────────┘
                 │
┌────────────────▼────────────────────────────────┐
│              DATA LAYER                         │
│  ┌────────────────┐  ┌──────────────────────┐  │
│  │  Dictionary    │  │     Blockchain       │  │
│  │ (búsqueda O(1))│  │   (inmutabilidad)    │  │
│  └────────────────┘  └──────────────────────┘  │
└─────────────────────────────────────────────────┘
```

### Patrones de Diseño

- **Singleton**: Services registrados una sola vez
- **Dependency Injection**: Inyección en constructores
- **Repository Pattern**: Services actúan como repositorios
- **Facade Pattern**: Controllers exponen API simplificada
- **Strategy Pattern**: Diferentes algoritmos de validación

---

## 🛠️ Tecnologías

| Categoría | Tecnología |
|-----------|-----------|
| **Framework** | .NET 9.0 |
| **Lenguaje** | C# 12.0 |
| **Criptografía** | RSA 2048, SHA256 |
| **API** | ASP.NET Core Web API |
| **Documentación** | Swagger/OpenAPI |
| **Autenticación** | JWT Bearer |
| **Serialización** | System.Text.Json |

---

## 📦 Instalación

### Requisitos Previos

- .NET 9.0 SDK
- Git
- Editor de código

### Pasos
```bash
# 1. Clonar repositorio
git clone https://github.com/tu-usuario/ApheliosID.git
cd ApheliosID

# 2. Restaurar dependencias
dotnet restore

# 3. Compilar
dotnet build

# 4. Ejecutar
dotnet run --project ApheliosID.API

# 5. Abrir navegador
# http://localhost:5000
```

---

## 🚀 Uso

### 1. Crear Identidad
```bash
POST /api/identity/create-with-keys
Content-Type: application/json

{
  "metadata": {
    "name": "Alice Smith",
    "email": "alice@example.com"
  }
}
```

**Respuesta:**
```json
{
  "did": "did:aphelios:abc123...",
  "publicKey": "MIIBIjANBg...",
  "privateKey": "MIIEvQIBAD...",
  "warning": "⚠️ SAVE YOUR PRIVATE KEY"
}
```

### 2. Emitir Credencial
```bash
POST /api/credential/issue

{
  "issuerDid": "did:aphelios:mit",
  "issuerPrivateKey": "MIIEvQIB...",
  "subjectDid": "did:aphelios:alice",
  "type": "AcademicCredential",
  "claims": {
    "degree": "Bachelor of Science",
    "fieldOfStudy": "Computer Science"
  }
}
```

### 3. Verificar Credencial
```bash
POST /api/credential/verify/cred_abc123
```

### 4. Autenticación JWT
```bash
# Paso 1: Solicitar challenge
POST /api/auth/challenge
{"did": "did:aphelios:alice"}

# Paso 2: Firmar con herramienta externa
cd ApheliosID.Signer
dotnet run

# Paso 3: Verificar
POST /api/auth/verify
{
  "did": "did:aphelios:alice",
  "challenge": "xyz...",
  "signature": "abc..."
}
```

---

## 📡 Endpoints API

### Identity (8 endpoints)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/identity/create-with-keys` | Crear identidad completa |
| POST | `/api/identity/register` | Registrar identidad |
| POST | `/api/identity/generate-keys` | Generar claves |
| GET | `/api/identity/{did}` | Obtener identidad |
| GET | `/api/identity` | Listar todas |
| POST | `/api/identity/{did}/deactivate` | Desactivar |
| POST | `/api/identity/{did}/activate` | Activar |

### Auth (2 endpoints)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/auth/challenge` | Solicitar challenge |
| POST | `/api/auth/verify` | Verificar y obtener JWT |

### Credential (8 endpoints)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/credential/issue` | Emitir credencial |
| POST | `/api/credential/verify/{id}` | Verificar |
| POST | `/api/credential/revoke/{id}` | Revocar |
| GET | `/api/credential/{id}` | Obtener |
| GET | `/api/credential/subject/{did}` | Por sujeto |
| GET | `/api/credential/issuer/{did}` | Por emisor |
| GET | `/api/credential` | Listar todas |
| GET | `/api/credential/demo-inheritance` | Demo herencia |

### Blockchain (6 endpoints)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/blockchain` | Ver cadena |
| GET | `/api/blockchain/block/{index}` | Ver bloque |
| POST | `/api/blockchain/mine` | Minar |
| GET | `/api/blockchain/validate` | Validar |
| GET | `/api/blockchain/pending` | Pendientes |
| GET | `/api/blockchain/stats` | Estadísticas |

**Total: 26 endpoints funcionales**

---

## 🔐 Seguridad

### Arquitectura Zero-Knowledge
```
┌─────────────────────────────────────────────────┐
│      EL SERVIDOR NUNCA VE CLAVES PRIVADAS      │
└─────────────────────────────────────────────────┘

1. Usuario genera claves LOCALMENTE
2. Usuario firma challenges LOCALMENTE
3. Servidor solo verifica con clave pública
4. Clave privada NUNCA toca la red
```

### Criptografía

- **RSA 2048 bits**: Generación de pares de claves
- **SHA-256**: Hash de firmas digitales
- **PKCS#1**: Padding para firmas
- **Base64**: Codificación

### Autenticación

- **JWT**: Tokens con expiración de 1 hora
- **Challenge**: Expira en 5 minutos, un solo uso
- **Firma digital**: Prueba de identidad

---

## 💡 Ejemplos

### Ejemplo Completo: Alice se Gradúa
```bash
# 1. MIT crea su identidad
POST /api/identity/create-with-keys
{"metadata": {"name": "MIT"}}

# 2. Alice crea su identidad
POST /api/identity/create-with-keys
{"metadata": {"name": "Alice Smith"}}

# 3. MIT emite diploma a Alice
POST /api/credential/issue
{
  "issuerDid": "did:aphelios:mit",
  "subjectDid": "did:aphelios:alice",
  "type": "AcademicCredential",
  "claims": {"degree": "Bachelor of Science"}
}

# 4. Empleador verifica
POST /api/credential/verify/cred_001

# 5. Ver credenciales de Alice
GET /api/credential/subject/did:aphelios:alice
```

---

## 📁 Estructura del Proyecto
```
ApheliosID/
├── ApheliosID.Core/
│   ├── Models/
│   │   ├── Block.cs
│   │   ├── Transaction.cs
│   │   ├── Identity.cs
│   │   ├── Credential.cs
│   │   ├── VerifiableCredential.cs
│   │   ├── AcademicCredential.cs
│   │   ├── ProfessionalCredential.cs
│   │   └── CertificationCredential.cs
│   ├── Services/
│   │   ├── BlockchainService.cs
│   │   ├── CryptoService.cs
│   │   ├── IdentityService.cs
│   │   ├── CredentialService.cs
│   │   └── AuthService.cs
│   └── Interfaces/
│
├── ApheliosID.API/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Program.cs
│   └── appsettings.json
│
├── ApheliosID.Signer/
└── ApheliosID.SecurityTests/
```

---

## 🎓 Conceptos OOP

### Herencia
```
VerifiableCredential (abstracta)
├── AcademicCredential
├── ProfessionalCredential
└── CertificationCredential
```

### Polimorfismo

- `GetCredentialType()` - Implementación específica
- `ValidateSpecificClaims()` - Reglas por tipo
- `IsValid()` - Método virtual

### Encapsulación

- Properties privadas con getters públicos
- Métodos protected en clases base

### Abstracción

- Clases abstractas con métodos abstractos
- Interfaces que definen contratos

---

## 📚 Principios SOLID

| Principio | Implementación |
|-----------|----------------|
| **S** - Single Responsibility | Cada Service una responsabilidad |
| **O** - Open/Closed | Fácil agregar tipos de credenciales |
| **L** - Liskov Substitution | Credenciales intercambiables |
| **I** - Interface Segregation | Interfaces específicas |
| **D** - Dependency Inversion | Dependency Injection |

---

## 🧪 Testing
```bash
# Terminal 1: API
dotnet run --project ApheliosID.API

# Terminal 2: Tests
cd ApheliosID.SecurityTests
dotnet run
```

**Tests:**
- SQL Injection
- JWT Tampering
- Challenge Replay
- Invalid Signature
- XSS Protection

---

## 📄 Licencia

MIT License

---

## 👨‍💻 Autor
Oscar Santiago Abuawad

**Tu Nombre**
- GitHub: @linuxenthusiastic

---

## 📚 Referencias

- [W3C DID Specification](https://www.w3.org/TR/did-core/)
- [W3C Verifiable Credentials](https://www.w3.org/TR/vc-data-model/)
- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [JWT Introduction](https://jwt.io/introduction)

---

⭐ **Si este proyecto te ayudó, dale una estrella** ⭐

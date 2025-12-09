

🔐 ApheliosID
=============

Blockchain de Identidades Descentralizadas y Credenciales Verificables

.NET 9.0 C# 12.0 RSA 2048 JWT Auth Blockchain

📋 Tabla de Contenidos
----------------------

*   [✨ Características](#caracteristicas)
*   [🏗️ Arquitectura](#arquitectura)
*   [🛠️ Tecnologías](#tecnologias)
*   [📦 Instalación](#instalacion)
*   [🚀 Uso](#uso)
*   [📡 Endpoints API](#endpoints)
*   [🔐 Seguridad](#seguridad)
*   [💡 Ejemplos](#ejemplos)
*   [📁 Estructura del Proyecto](#estructura)

✨ Características
-----------------

#### 🔗 Blockchain Inmutable

*   Proof-of-work
*   Bloques enlazados
*   Historial auditable
*   Validación de integridad

#### 🆔 Identidades DIDs

*   Generación basada en RSA
*   Zero-Knowledge
*   Descentralizado
*   Sin intermediarios

#### 🎓 Credenciales

*   Firma digital
*   Verificación offline
*   Revocación
*   Tipos especializados

#### 🔐 Autenticación JWT

*   Sin contraseñas
*   Challenge-response
*   Expiración configurable
*   Firmado local

🏗️ Arquitectura
----------------

┌─────────────────────────────────────────────────┐ │ API REST │ │ (Controllers + Swagger UI) │ └────────────────┬────────────────────────────────┘ │ ┌────────────────▼────────────────────────────────┐ │ SERVICES LAYER │ │ ┌──────────┐ ┌──────────┐ ┌──────────────┐ │ │ │Identity │ │Credential│ │ Blockchain │ │ │ │Service │ │Service │ │ Service │ │ │ └──────────┘ └──────────┘ └──────────────┘ │ │ ┌──────────┐ ┌──────────┐ │ │ │ Crypto │ │ Auth │ │ │ │ Service │ │ Service │ │ │ └──────────┘ └──────────┘ │ └────────────────┬────────────────────────────────┘ │ ┌────────────────▼────────────────────────────────┐ │ DATA LAYER │ │ ┌────────────────┐ ┌──────────────────────┐ │ │ │ Dictionary │ │ Blockchain │ │ │ │ (búsqueda O(1))│ │ (inmutabilidad) │ │ │ └────────────────┘ └──────────────────────┘ │ └─────────────────────────────────────────────────┘

### Patrones de Diseño Implementados

*   **Singleton:** Services registrados una sola vez en la aplicación
*   **Dependency Injection:** Inyección de dependencias en constructores
*   **Repository Pattern:** Services actúan como repositorios de datos
*   **Facade Pattern:** Controllers exponen una API simplificada
*   **Strategy Pattern:** Diferentes estrategias de validación por tipo

🛠️ Tecnologías
---------------

Categoría

Tecnología

**Framework**

.NET 9.0

**Lenguaje**

C# 12.0

**Criptografía**

RSA 2048, SHA256

**API**

ASP.NET Core Web API

**Documentación**

Swagger/OpenAPI

**Autenticación**

JWT Bearer

**Serialización**

System.Text.Json

📦 Instalación
--------------

### Requisitos Previos

*   .NET 9.0 SDK
*   Git
*   Editor de código (VS Code, Visual Studio, Rider)

### Pasos de Instalación

\# 1. Clonar repositorio git clone https://github.com/tu-usuario/ApheliosID.git cd ApheliosID # 2. Restaurar dependencias dotnet restore # 3. Compilar proyecto dotnet build # 4. Ejecutar aplicación dotnet run --project ApheliosID.API # 5. Abrir navegador en: http://localhost:5000

**✅ Listo!** El Swagger UI se abrirá automáticamente mostrando todos los endpoints disponibles.

🚀 Uso Rápido
-------------

### 1\. Crear una Identidad

POST /api/identity/create-with-keys Content-Type: application/json { "metadata": { "name": "Alice Smith", "email": "alice@example.com" } }

**Respuesta:**

{ "did": "did:aphelios:abc123...", "publicKey": "MIIBIjANBg...", "privateKey": "MIIEvQIBAD...", "warning": "⚠️ SAVE YOUR PRIVATE KEY!" }

### 2\. Emitir una Credencial

POST /api/credential/issue Content-Type: application/json { "issuerDid": "did:aphelios:mit", "issuerPrivateKey": "MIIEvQIB...", "subjectDid": "did:aphelios:alice", "type": "AcademicCredential", "claims": { "degree": "Bachelor of Science", "fieldOfStudy": "Computer Science", "graduationDate": "2024-06-15", "gpa": 3.8 } }

### 3\. Verificar Credencial

POST /api/credential/verify/cred\_abc123

**Respuesta:**

{ "credentialId": "cred\_abc123", "isValid": true, "isRevoked": false, "verifiedAt": "2024-12-09T12:00:00Z" }

### 4\. Autenticación JWT (Challenge-Response)

\# Paso 1: Solicitar challenge POST /api/auth/challenge {"did": "did:aphelios:alice"} # Respuesta: {"challenge": "xyz789..."} # Paso 2: Firmar con herramienta externa cd ApheliosID.Signer dotnet run > Challenge: xyz789... > Private Key: MIIEvQIB... > Output: signature\_abc123... # Paso 3: Verificar y obtener JWT POST /api/auth/verify { "did": "did:aphelios:alice", "challenge": "xyz789...", "signature": "signature\_abc123..." } # Respuesta: { "token": "eyJhbGciOiJIUzI1NiIs...", "tokenType": "Bearer", "expiresIn": 3600, "did": "did:aphelios:alice" }

📡 Endpoints API
----------------

### Identity (8 endpoints)

Método

Endpoint

Descripción

POST

/api/identity/create-with-keys

Crear identidad completa con claves

POST

/api/identity/register

Registrar identidad existente

POST

/api/identity/generate-keys

Generar par de claves RSA

GET

/api/identity/{did}

Obtener identidad por DID

GET

/api/identity

Listar todas las identidades

POST

/api/identity/{did}/deactivate

Desactivar identidad

POST

/api/identity/{did}/activate

Activar identidad

### Auth (2 endpoints)

Método

Endpoint

Descripción

POST

/api/auth/challenge

Solicitar challenge aleatorio

POST

/api/auth/verify

Verificar firma y obtener JWT

### Credential (8 endpoints)

Método

Endpoint

Descripción

POST

/api/credential/issue

Emitir nueva credencial

POST

/api/credential/verify/{id}

Verificar validez de credencial

POST

/api/credential/revoke/{id}

Revocar credencial

GET

/api/credential/{id}

Obtener credencial por ID

GET

/api/credential/subject/{did}

Credenciales de una persona

GET

/api/credential/issuer/{did}

Credenciales emitidas por organización

GET

/api/credential

Listar todas las credenciales

GET

/api/credential/demo-inheritance

Demostración de herencia OOP

### Blockchain (6 endpoints)

Método

Endpoint

Descripción

GET

/api/blockchain

Ver cadena completa

GET

/api/blockchain/block/{index}

Ver bloque específico

POST

/api/blockchain/mine

Minar bloque pendiente

GET

/api/blockchain/validate

Validar integridad

GET

/api/blockchain/pending

Ver transacciones pendientes

GET

/api/blockchain/stats

Estadísticas de la blockchain

**Total: 26 endpoints REST funcionales**

🔐 Seguridad
------------

### Arquitectura Zero-Knowledge

**⚠️ CRÍTICO: EL SERVIDOR NUNCA VE CLAVES PRIVADAS**

*   Usuario genera claves LOCALMENTE
*   Usuario firma challenges LOCALMENTE
*   Servidor solo verifica con clave pública
*   Clave privada NUNCA toca la red

### Criptografía Implementada

*   **RSA 2048 bits:** Generación de pares de claves asimétricas
*   **SHA-256:** Algoritmo de hash para firmas digitales
*   **PKCS#1:** Padding estándar para firmas RSA
*   **Base64:** Codificación de claves y firmas para transporte

### Autenticación JWT

*   **Tokens JWT:** Expiración de 1 hora
*   **Challenge:** Expira en 5 minutos, un solo uso
*   **Firma digital:** Prueba criptográfica de identidad
*   **Sin contraseñas:** Sistema passwordless completo

### Tests de Seguridad Incluidos

*   SQL Injection attempts
*   JWT Token Tampering
*   Challenge Replay Attack
*   Invalid Signature detection
*   XSS in Metadata

💡 Ejemplo Completo: Alice se Gradúa
------------------------------------

#### Escenario: Alice obtiene su diploma de MIT y busca trabajo en TechCorp

### Paso 1: MIT crea su identidad

POST /api/identity/create-with-keys {"metadata": {"name": "MIT", "type": "university"}} → Response: did:aphelios:mit

### Paso 2: Alice crea su identidad

POST /api/identity/create-with-keys {"metadata": {"name": "Alice Smith"}} → Response: did:aphelios:alice

### Paso 3: MIT emite diploma a Alice

POST /api/credential/issue { "issuerDid": "did:aphelios:mit", "issuerPrivateKey": "...", "subjectDid": "did:aphelios:alice", "type": "AcademicCredential", "claims": { "degree": "Bachelor of Science", "fieldOfStudy": "Computer Science", "graduationDate": "2024-06-15" } } → Response: cred\_001 (diploma creado)

### Paso 4: Empleador verifica diploma

POST /api/credential/verify/cred\_001 → Response: isValid: true ✅

### Paso 5: TechCorp emite credencial de empleo

POST /api/credential/issue (TechCorp emite credencial) → Response: cred\_002 (empleo creado)

### Paso 6: Ver portafolio completo de Alice

GET /api/credential/subject/did:aphelios:alice → Response: \[diploma MIT, empleo TechCorp, ...\]

**✅ Resultado:** Alice tiene un portafolio digital verificable sin intermediarios

📁 Estructura del Proyecto
--------------------------

ApheliosID/ ├── ApheliosID.Core/ # Lógica de negocio │ ├── Models/ │ │ ├── Block.cs │ │ ├── Transaction.cs │ │ ├── Identity.cs │ │ ├── Credential.cs │ │ ├── VerifiableCredential.cs │ │ ├── AcademicCredential.cs │ │ ├── ProfessionalCredential.cs │ │ ├── CertificationCredential.cs │ │ └── AuthChallenge.cs │ ├── Services/ │ │ ├── BlockchainService.cs │ │ ├── CryptoService.cs │ │ ├── IdentityService.cs │ │ ├── CredentialService.cs │ │ └── AuthService.cs │ └── Interfaces/ │ └── IBlockchainService.cs │ ├── ApheliosID.API/ # REST API │ ├── Controllers/ │ │ ├── BlockchainController.cs │ │ ├── IdentityController.cs │ │ ├── CredentialController.cs │ │ ├── TransactionController.cs │ │ └── AuthController.cs │ ├── DTOs/ │ │ ├── IdentityRequestDto.cs │ │ ├── CredentialRequestDto.cs │ │ └── AuthRequestDto.cs │ ├── Program.cs │ └── appsettings.json │ ├── ApheliosID.Signer/ # Herramienta de firmado │ └── Program.cs │ ├── ApheliosID.SecurityTests/ # Tests de seguridad │ └── Program.cs │ └── README.md

🎓 Conceptos OOP Implementados
------------------------------

### Herencia

VerifiableCredential (abstracta) ├── AcademicCredential (concreta) ├── ProfessionalCredential (concreta) └── CertificationCredential (concreta)

### Polimorfismo

*   `GetCredentialType()` - Cada tipo implementa diferente
*   `ValidateSpecificClaims()` - Reglas específicas por tipo
*   `IsValid()` - Método virtual que usa polimorfismo

### Encapsulación

*   Properties privadas con getters públicos
*   Métodos protected en clases base
*   Ocultación de implementación interna

### Abstracción

*   Clases abstractas que definen contratos
*   Métodos abstractos que deben implementarse
*   Interfaces que definen comportamiento

📚 Principios SOLID
-------------------

Principio

Implementación

**S** - Single Responsibility

Cada Service tiene una única responsabilidad clara

**O** - Open/Closed

Fácil agregar nuevos tipos de credenciales sin modificar existentes

**L** - Liskov Substitution

Cualquier credencial puede usarse donde se espera VerifiableCredential

**I** - Interface Segregation

IBlockchainService específica sin métodos innecesarios

**D** - Dependency Inversion

Dependency Injection en todos los constructores

🧪 Testing
----------

### Ejecutar Tests de Seguridad

\# Terminal 1: Iniciar API dotnet run --project ApheliosID.API # Terminal 2: Ejecutar tests cd ApheliosID.SecurityTests dotnet run

**Tests Incluidos:**

*   ✅ SQL Injection Protection
*   ✅ JWT Tampering Detection
*   ✅ Challenge Replay Prevention
*   ✅ Invalid Signature Rejection
*   ✅ XSS in Metadata Protection

📄 Licencia
-----------

MIT License - Este proyecto es de código abierto y puede ser usado libremente.

👨‍💻 Autor
-----------

**Tu Nombre**

*   GitHub: @tu-usuario
*   Email: tu-email@example.com
*   Universidad: Tu Universidad

📚 Referencias
--------------

*   [W3C DID Specification](https://www.w3.org/TR/did-core/)
*   [W3C Verifiable Credentials](https://www.w3.org/TR/vc-data-model/)
*   [.NET Documentation](https://docs.microsoft.com/dotnet/)
*   [JWT Introduction](https://jwt.io/introduction)

⭐ Si este proyecto te ayudó, dale una estrella en GitHub ⭐
----------------------------------------------------------
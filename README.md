<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>ApheliosID - README</title>
    <style>
        body {
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
            line-height: 1.6;
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
            background: #f6f8fa;
            color: #24292e;
        }
        .header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 40px;
            border-radius: 10px;
            margin-bottom: 30px;
            text-align: center;
        }
        .header h1 {
            margin: 0;
            font-size: 3em;
        }
        .header p {
            margin: 10px 0 0 0;
            font-size: 1.2em;
            opacity: 0.9;
        }
        .badges {
            margin: 20px 0;
        }
        .badge {
            display: inline-block;
            padding: 5px 10px;
            background: rgba(255,255,255,0.2);
            border-radius: 5px;
            margin: 5px;
            font-size: 0.9em;
        }
        .section {
            background: white;
            padding: 30px;
            margin-bottom: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }
        .section h2 {
            color: #667eea;
            border-bottom: 3px solid #667eea;
            padding-bottom: 10px;
            margin-top: 0;
        }
        .section h3 {
            color: #764ba2;
            margin-top: 25px;
        }
        .code-block {
            background: #f6f8fa;
            border: 1px solid #e1e4e8;
            border-radius: 6px;
            padding: 16px;
            overflow-x: auto;
            margin: 15px 0;
            font-family: 'Courier New', monospace;
            font-size: 0.9em;
        }
        .architecture-diagram {
            background: #2d3748;
            color: #68d391;
            padding: 20px;
            border-radius: 8px;
            font-family: monospace;
            white-space: pre;
            overflow-x: auto;
            margin: 20px 0;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }
        th {
            background: #667eea;
            color: white;
            padding: 12px;
            text-align: left;
        }
        td {
            padding: 12px;
            border-bottom: 1px solid #e1e4e8;
        }
        tr:hover {
            background: #f6f8fa;
        }
        .feature-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 20px;
            margin: 20px 0;
        }
        .feature-card {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 20px;
            border-radius: 10px;
        }
        .feature-card h4 {
            margin-top: 0;
            font-size: 1.3em;
        }
        .example-box {
            background: #fff3cd;
            border-left: 4px solid #ffc107;
            padding: 15px;
            margin: 15px 0;
        }
        .success-box {
            background: #d4edda;
            border-left: 4px solid #28a745;
            padding: 15px;
            margin: 15px 0;
        }
        .warning-box {
            background: #f8d7da;
            border-left: 4px solid #dc3545;
            padding: 15px;
            margin: 15px 0;
        }
        ul {
            line-height: 1.8;
        }
        .toc {
            background: #f6f8fa;
            padding: 20px;
            border-radius: 8px;
            margin: 20px 0;
        }
        .toc ul {
            list-style: none;
            padding-left: 0;
        }
        .toc li {
            padding: 5px 0;
        }
        .toc a {
            color: #667eea;
            text-decoration: none;
        }
        .toc a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>

<div class="header">
    <h1>🔐 ApheliosID</h1>
    <p>Blockchain de Identidades Descentralizadas y Credenciales Verificables</p>
    <div class="badges">
        <span class="badge">.NET 9.0</span>
        <span class="badge">C# 12.0</span>
        <span class="badge">RSA 2048</span>
        <span class="badge">JWT Auth</span>
        <span class="badge">Blockchain</span>
    </div>
</div>

<div class="section toc">
    <h2>📋 Tabla de Contenidos</h2>
    <ul>
        <li><a href="#caracteristicas">✨ Características</a></li>
        <li><a href="#arquitectura">🏗️ Arquitectura</a></li>
        <li><a href="#tecnologias">🛠️ Tecnologías</a></li>
        <li><a href="#instalacion">📦 Instalación</a></li>
        <li><a href="#uso">🚀 Uso</a></li>
        <li><a href="#endpoints">📡 Endpoints API</a></li>
        <li><a href="#seguridad">🔐 Seguridad</a></li>
        <li><a href="#ejemplos">💡 Ejemplos</a></li>
        <li><a href="#estructura">📁 Estructura del Proyecto</a></li>
    </ul>
</div>

<div class="section" id="caracteristicas">
    <h2>✨ Características</h2>
    
    <div class="feature-grid">
        <div class="feature-card">
            <h4>🔗 Blockchain Inmutable</h4>
            <ul>
                <li>Proof-of-work</li>
                <li>Bloques enlazados</li>
                <li>Historial auditable</li>
                <li>Validación de integridad</li>
            </ul>
        </div>
        
        <div class="feature-card">
            <h4>🆔 Identidades DIDs</h4>
            <ul>
                <li>Generación basada en RSA</li>
                <li>Zero-Knowledge</li>
                <li>Descentralizado</li>
                <li>Sin intermediarios</li>
            </ul>
        </div>
        
        <div class="feature-card">
            <h4>🎓 Credenciales</h4>
            <ul>
                <li>Firma digital</li>
                <li>Verificación offline</li>
                <li>Revocación</li>
                <li>Tipos especializados</li>
            </ul>
        </div>
        
        <div class="feature-card">
            <h4>🔐 Autenticación JWT</h4>
            <ul>
                <li>Sin contraseñas</li>
                <li>Challenge-response</li>
                <li>Expiración configurable</li>
                <li>Firmado local</li>
            </ul>
        </div>
    </div>
</div>

<div class="section" id="arquitectura">
    <h2>🏗️ Arquitectura</h2>
    
    <div class="architecture-diagram">
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
    </div>
    
    <h3>Patrones de Diseño Implementados</h3>
    <ul>
        <li><strong>Singleton:</strong> Services registrados una sola vez en la aplicación</li>
        <li><strong>Dependency Injection:</strong> Inyección de dependencias en constructores</li>
        <li><strong>Repository Pattern:</strong> Services actúan como repositorios de datos</li>
        <li><strong>Facade Pattern:</strong> Controllers exponen una API simplificada</li>
        <li><strong>Strategy Pattern:</strong> Diferentes estrategias de validación por tipo</li>
    </ul>
</div>

<div class="section" id="tecnologias">
    <h2>🛠️ Tecnologías</h2>
    
    <table>
        <tr>
            <th>Categoría</th>
            <th>Tecnología</th>
        </tr>
        <tr>
            <td><strong>Framework</strong></td>
            <td>.NET 9.0</td>
        </tr>
        <tr>
            <td><strong>Lenguaje</strong></td>
            <td>C# 12.0</td>
        </tr>
        <tr>
            <td><strong>Criptografía</strong></td>
            <td>RSA 2048, SHA256</td>
        </tr>
        <tr>
            <td><strong>API</strong></td>
            <td>ASP.NET Core Web API</td>
        </tr>
        <tr>
            <td><strong>Documentación</strong></td>
            <td>Swagger/OpenAPI</td>
        </tr>
        <tr>
            <td><strong>Autenticación</strong></td>
            <td>JWT Bearer</td>
        </tr>
        <tr>
            <td><strong>Serialización</strong></td>
            <td>System.Text.Json</td>
        </tr>
    </table>
</div>

<div class="section" id="instalacion">
    <h2>📦 Instalación</h2>
    
    <h3>Requisitos Previos</h3>
    <ul>
        <li>.NET 9.0 SDK</li>
        <li>Git</li>
        <li>Editor de código (VS Code, Visual Studio, Rider)</li>
    </ul>
    
    <h3>Pasos de Instalación</h3>
    
    <div class="code-block">
# 1. Clonar repositorio
git clone https://github.com/tu-usuario/ApheliosID.git
cd ApheliosID

# 2. Restaurar dependencias
dotnet restore

# 3. Compilar proyecto
dotnet build

# 4. Ejecutar aplicación
dotnet run --project ApheliosID.API

# 5. Abrir navegador en:
http://localhost:5000
    </div>
    
    <div class="success-box">
        <strong>✅ Listo!</strong> El Swagger UI se abrirá automáticamente mostrando todos los endpoints disponibles.
    </div>
</div>

<div class="section" id="uso">
    <h2>🚀 Uso Rápido</h2>
    
    <h3>1. Crear una Identidad</h3>
    <div class="code-block">
POST /api/identity/create-with-keys
Content-Type: application/json

{
  "metadata": {
    "name": "Alice Smith",
    "email": "alice@example.com"
  }
}
    </div>
    
    <div class="example-box">
        <strong>Respuesta:</strong>
        <div class="code-block">
{
  "did": "did:aphelios:abc123...",
  "publicKey": "MIIBIjANBg...",
  "privateKey": "MIIEvQIBAD...",
  "warning": "⚠️ SAVE YOUR PRIVATE KEY!"
}
        </div>
    </div>
    
    <h3>2. Emitir una Credencial</h3>
    <div class="code-block">
POST /api/credential/issue
Content-Type: application/json

{
  "issuerDid": "did:aphelios:mit",
  "issuerPrivateKey": "MIIEvQIB...",
  "subjectDid": "did:aphelios:alice",
  "type": "AcademicCredential",
  "claims": {
    "degree": "Bachelor of Science",
    "fieldOfStudy": "Computer Science",
    "graduationDate": "2024-06-15",
    "gpa": 3.8
  }
}
    </div>
    
    <h3>3. Verificar Credencial</h3>
    <div class="code-block">
POST /api/credential/verify/cred_abc123
    </div>
    
    <div class="success-box">
        <strong>Respuesta:</strong>
        <div class="code-block">
{
  "credentialId": "cred_abc123",
  "isValid": true,
  "isRevoked": false,
  "verifiedAt": "2024-12-09T12:00:00Z"
}
        </div>
    </div>
    
    <h3>4. Autenticación JWT (Challenge-Response)</h3>
    <div class="code-block">
# Paso 1: Solicitar challenge
POST /api/auth/challenge
{"did": "did:aphelios:alice"}

# Respuesta: {"challenge": "xyz789..."}

# Paso 2: Firmar con herramienta externa
cd ApheliosID.Signer
dotnet run
> Challenge: xyz789...
> Private Key: MIIEvQIB...
> Output: signature_abc123...

# Paso 3: Verificar y obtener JWT
POST /api/auth/verify
{
  "did": "did:aphelios:alice",
  "challenge": "xyz789...",
  "signature": "signature_abc123..."
}

# Respuesta:
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "tokenType": "Bearer",
  "expiresIn": 3600,
  "did": "did:aphelios:alice"
}
    </div>
</div>

<div class="section" id="endpoints">
    <h2>📡 Endpoints API</h2>
    
    <h3>Identity (8 endpoints)</h3>
    <table>
        <tr>
            <th>Método</th>
            <th>Endpoint</th>
            <th>Descripción</th>
        </tr>
        <tr>
            <td>POST</td>
            <td>/api/identity/create-with-keys</td>
            <td>Crear identidad completa con claves</td>
        </tr>
        <tr>
            <td>POST</td>
            <td>/api/identity/register</td>
            <td>Registrar identidad existente</td>
        </tr>
        <tr>
            <td>POST</td>
            <td>/api/identity/generate-keys</td>
            <td>Generar par de claves RSA</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/identity/{did}</td>
            <td>Obtener identidad por DID</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/identity</td>
            <td>Listar todas las identidades</td>
        </tr>
        <tr>
            <td>POST</td>
            <td>/api/identity/{did}/deactivate</td>
            <td>Desactivar identidad</td>
        </tr>
        <tr>
            <td>POST</td>
            <td>/api/identity/{did}/activate</td>
            <td>Activar identidad</td>
        </tr>
    </table>
    
    <h3>Auth (2 endpoints)</h3>
    <table>
        <tr>
            <th>Método</th>
            <th>Endpoint</th>
            <th>Descripción</th>
        </tr>
        <tr>
            <td>POST</td>
            <td>/api/auth/challenge</td>
            <td>Solicitar challenge aleatorio</td>
        </tr>
        <tr>
            <td>POST</td>
            <td>/api/auth/verify</td>
            <td>Verificar firma y obtener JWT</td>
        </tr>
    </table>
    
    <h3>Credential (8 endpoints)</h3>
    <table>
        <tr>
            <th>Método</th>
            <th>Endpoint</th>
            <th>Descripción</th>
        </tr>
        <tr>
            <td>POST</td>
            <td>/api/credential/issue</td>
            <td>Emitir nueva credencial</td>
        </tr>
        <tr>
            <td>POST</td>
            <td>/api/credential/verify/{id}</td>
            <td>Verificar validez de credencial</td>
        </tr>
        <tr>
            <td>POST</td>
            <td>/api/credential/revoke/{id}</td>
            <td>Revocar credencial</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/credential/{id}</td>
            <td>Obtener credencial por ID</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/credential/subject/{did}</td>
            <td>Credenciales de una persona</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/credential/issuer/{did}</td>
            <td>Credenciales emitidas por organización</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/credential</td>
            <td>Listar todas las credenciales</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/credential/demo-inheritance</td>
            <td>Demostración de herencia OOP</td>
        </tr>
    </table>
    
    <h3>Blockchain (6 endpoints)</h3>
    <table>
        <tr>
            <th>Método</th>
            <th>Endpoint</th>
            <th>Descripción</th>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/blockchain</td>
            <td>Ver cadena completa</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/blockchain/block/{index}</td>
            <td>Ver bloque específico</td>
        </tr>
        <tr>
            <td>POST</td>
            <td>/api/blockchain/mine</td>
            <td>Minar bloque pendiente</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/blockchain/validate</td>
            <td>Validar integridad</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/blockchain/pending</td>
            <td>Ver transacciones pendientes</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>/api/blockchain/stats</td>
            <td>Estadísticas de la blockchain</td>
        </tr>
    </table>
    
    <div class="success-box">
        <strong>Total: 26 endpoints REST funcionales</strong>
    </div>
</div>

<div class="section" id="seguridad">
    <h2>🔐 Seguridad</h2>
    
    <h3>Arquitectura Zero-Knowledge</h3>
    <div class="warning-box">
        <strong>⚠️ CRÍTICO: EL SERVIDOR NUNCA VE CLAVES PRIVADAS</strong>
        <ul>
            <li>Usuario genera claves LOCALMENTE</li>
            <li>Usuario firma challenges LOCALMENTE</li>
            <li>Servidor solo verifica con clave pública</li>
            <li>Clave privada NUNCA toca la red</li>
        </ul>
    </div>
    
    <h3>Criptografía Implementada</h3>
    <ul>
        <li><strong>RSA 2048 bits:</strong> Generación de pares de claves asimétricas</li>
        <li><strong>SHA-256:</strong> Algoritmo de hash para firmas digitales</li>
        <li><strong>PKCS#1:</strong> Padding estándar para firmas RSA</li>
        <li><strong>Base64:</strong> Codificación de claves y firmas para transporte</li>
    </ul>
    
    <h3>Autenticación JWT</h3>
    <ul>
        <li><strong>Tokens JWT:</strong> Expiración de 1 hora</li>
        <li><strong>Challenge:</strong> Expira en 5 minutos, un solo uso</li>
        <li><strong>Firma digital:</strong> Prueba criptográfica de identidad</li>
        <li><strong>Sin contraseñas:</strong> Sistema passwordless completo</li>
    </ul>
    
    <h3>Tests de Seguridad Incluidos</h3>
    <ul>
        <li>SQL Injection attempts</li>
        <li>JWT Token Tampering</li>
        <li>Challenge Replay Attack</li>
        <li>Invalid Signature detection</li>
        <li>XSS in Metadata</li>
    </ul>
</div>

<div class="section" id="ejemplos">
    <h2>💡 Ejemplo Completo: Alice se Gradúa</h2>
    
    <div class="example-box">
        <h4>Escenario: Alice obtiene su diploma de MIT y busca trabajo en TechCorp</h4>
    </div>
    
    <h3>Paso 1: MIT crea su identidad</h3>
    <div class="code-block">
POST /api/identity/create-with-keys
{"metadata": {"name": "MIT", "type": "university"}}

→ Response: did:aphelios:mit
    </div>
    
    <h3>Paso 2: Alice crea su identidad</h3>
    <div class="code-block">
POST /api/identity/create-with-keys
{"metadata": {"name": "Alice Smith"}}

→ Response: did:aphelios:alice
    </div>
    
    <h3>Paso 3: MIT emite diploma a Alice</h3>
    <div class="code-block">
POST /api/credential/issue
{
  "issuerDid": "did:aphelios:mit",
  "issuerPrivateKey": "...",
  "subjectDid": "did:aphelios:alice",
  "type": "AcademicCredential",
  "claims": {
    "degree": "Bachelor of Science",
    "fieldOfStudy": "Computer Science",
    "graduationDate": "2024-06-15"
  }
}

→ Response: cred_001 (diploma creado)
    </div>
    
    <h3>Paso 4: Empleador verifica diploma</h3>
    <div class="code-block">
POST /api/credential/verify/cred_001

→ Response: isValid: true ✅
    </div>
    
    <h3>Paso 5: TechCorp emite credencial de empleo</h3>
    <div class="code-block">
POST /api/credential/issue (TechCorp emite credencial)

→ Response: cred_002 (empleo creado)
    </div>
    
    <h3>Paso 6: Ver portafolio completo de Alice</h3>
    <div class="code-block">
GET /api/credential/subject/did:aphelios:alice

→ Response: [diploma MIT, empleo TechCorp, ...]
    </div>
    
    <div class="success-box">
        <strong>✅ Resultado:</strong> Alice tiene un portafolio digital verificable sin intermediarios
    </div>
</div>

<div class="section" id="estructura">
    <h2>📁 Estructura del Proyecto</h2>
    
    <div class="code-block">
ApheliosID/
├── ApheliosID.Core/              # Lógica de negocio
│   ├── Models/
│   │   ├── Block.cs
│   │   ├── Transaction.cs
│   │   ├── Identity.cs
│   │   ├── Credential.cs
│   │   ├── VerifiableCredential.cs
│   │   ├── AcademicCredential.cs
│   │   ├── ProfessionalCredential.cs
│   │   ├── CertificationCredential.cs
│   │   └── AuthChallenge.cs
│   ├── Services/
│   │   ├── BlockchainService.cs
│   │   ├── CryptoService.cs
│   │   ├── IdentityService.cs
│   │   ├── CredentialService.cs
│   │   └── AuthService.cs
│   └── Interfaces/
│       └── IBlockchainService.cs
│
├── ApheliosID.API/               # REST API
│   ├── Controllers/
│   │   ├── BlockchainController.cs
│   │   ├── IdentityController.cs
│   │   ├── CredentialController.cs
│   │   ├── TransactionController.cs
│   │   └── AuthController.cs
│   ├── DTOs/
│   │   ├── IdentityRequestDto.cs
│   │   ├── CredentialRequestDto.cs
│   │   └── AuthRequestDto.cs
│   ├── Program.cs
│   └── appsettings.json
│
├── ApheliosID.Signer/            # Herramienta de firmado
│   └── Program.cs
│
├── ApheliosID.SecurityTests/     # Tests de seguridad
│   └── Program.cs
│
└── README.md
    </div>
</div>

<div class="section">
    <h2>🎓 Conceptos OOP Implementados</h2>
    
    <h3>Herencia</h3>
    <div class="code-block">
VerifiableCredential (abstracta)
├── AcademicCredential (concreta)
├── ProfessionalCredential (concreta)
└── CertificationCredential (concreta)
    </div>
    
    <h3>Polimorfismo</h3>
    <ul>
        <li><code>GetCredentialType()</code> - Cada tipo implementa diferente</li>
        <li><code>ValidateSpecificClaims()</code> - Reglas específicas por tipo</li>
        <li><code>IsValid()</code> - Método virtual que usa polimorfismo</li>
    </ul>
    
    <h3>Encapsulación</h3>
    <ul>
        <li>Properties privadas con getters públicos</li>
        <li>Métodos protected en clases base</li>
        <li>Ocultación de implementación interna</li>
    </ul>
    
    <h3>Abstracción</h3>
    <ul>
        <li>Clases abstractas que definen contratos</li>
        <li>Métodos abstractos que deben implementarse</li>
        <li>Interfaces que definen comportamiento</li>
    </ul>
</div>

<div class="section">
    <h2>📚 Principios SOLID</h2>
    
    <table>
        <tr>
            <th>Principio</th>
            <th>Implementación</th>
        </tr>
        <tr>
            <td><strong>S</strong> - Single Responsibility</td>
            <td>Cada Service tiene una única responsabilidad clara</td>
        </tr>
        <tr>
            <td><strong>O</strong> - Open/Closed</td>
            <td>Fácil agregar nuevos tipos de credenciales sin modificar existentes</td>
        </tr>
        <tr>
            <td><strong>L</strong> - Liskov Substitution</td>
            <td>Cualquier credencial puede usarse donde se espera VerifiableCredential</td>
        </tr>
        <tr>
            <td><strong>I</strong> - Interface Segregation</td>
            <td>IBlockchainService específica sin métodos innecesarios</td>
        </tr>
        <tr>
            <td><strong>D</strong> - Dependency Inversion</td>
            <td>Dependency Injection en todos los constructores</td>
        </tr>
    </table>
</div>

<div class="section">
    <h2>🧪 Testing</h2>
    
    <h3>Ejecutar Tests de Seguridad</h3>
    <div class="code-block">
# Terminal 1: Iniciar API
dotnet run --project ApheliosID.API

# Terminal 2: Ejecutar tests
cd ApheliosID.SecurityTests
dotnet run
    </div>
    
    <div class="success-box">
        <strong>Tests Incluidos:</strong>
        <ul>
            <li>✅ SQL Injection Protection</li>
            <li>✅ JWT Tampering Detection</li>
            <li>✅ Challenge Replay Prevention</li>
            <li>✅ Invalid Signature Rejection</li>
            <li>✅ XSS in Metadata Protection</li>
        </ul>
    </div>
</div>

<div class="section">
    <h2>📄 Licencia</h2>
    <p>MIT License - Este proyecto es de código abierto y puede ser usado libremente.</p>
</div>

<div class="section">
    <h2>👨‍💻 Autor</h2>
    <p><strong>Tu Nombre</strong></p>
    <ul>
        <li>GitHub: @tu-usuario</li>
        <li>Email: tu-email@example.com</li>
        <li>Universidad: Tu Universidad</li>
    </ul>
</div>

<div class="section">
    <h2>📚 Referencias</h2>
    <ul>
        <li><a href="https://www.w3.org/TR/did-core/" target="_blank">W3C DID Specification</a></li>
        <li><a href="https://www.w3.org/TR/vc-data-model/" target="_blank">W3C Verifiable Credentials</a></li>
        <li><a href="https://docs.microsoft.com/dotnet/" target="_blank">.NET Documentation</a></li>
        <li><a href="https://jwt.io/introduction" target="_blank">JWT Introduction</a></li>
    </ul>
</div>

<div class="header" style="margin-top: 40px;">
    <h2>⭐ Si este proyecto te ayudó, dale una estrella en GitHub ⭐</h2>
</div>

</body>
</html>
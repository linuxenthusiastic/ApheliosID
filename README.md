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
- [Guía de Ejecución](#guía-de-ejecución)
- [Uso](#uso)
- [Endpoints API](#endpoints-api)
- [Seguridad](#seguridad)
- [Ejemplos](#ejemplos)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Conceptos OOP](#conceptos-oop)
- [Principios SOLID](#principios-solid)
- [Deploy y Producción](#deploy-y-producción)

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

#### Windows
1. Descargar e instalar [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
2. Descargar e instalar [Git](https://git-scm.com/download/win)
3. (Opcional) Instalar [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)

#### macOS
```bash
# Instalar Homebrew (si no lo tienes)
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Instalar .NET SDK
brew install dotnet

# Instalar Git
brew install git
```

#### Linux (Ubuntu/Debian)
```bash
# Agregar repositorio de Microsoft
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Instalar .NET SDK
sudo apt-get update
sudo apt-get install -y dotnet-sdk-9.0

# Instalar Git
sudo apt-get install git
```

#### Verificar Instalación
```bash
# Verificar .NET
dotnet --version
# Debe mostrar: 9.0.x

# Verificar Git
git --version
```

---

## 🚀 Guía de Ejecución

### Opción 1: Clonar desde GitHub
```bash
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
# http://localhost:5000
# o
# https://localhost:5001
```

---

### Opción 2: Descargar ZIP

1. **Descargar proyecto**
   - Ve a GitHub → Click en "Code" → "Download ZIP"
   - Extrae el archivo en tu carpeta preferida

2. **Abrir terminal en la carpeta**
   - **Windows**: Click derecho → "Abrir en Terminal" o "Git Bash Here"
   - **macOS/Linux**: Click derecho → "New Terminal at Folder"

3. **Ejecutar comandos**
```bash
cd ApheliosID
dotnet restore
dotnet build
dotnet run --project ApheliosID.API
```

---

### Opción 3: Usar Visual Studio

1. Abrir **Visual Studio 2022**
2. **File** → **Open** → **Project/Solution**
3. Seleccionar `ApheliosID.sln`
4. Presionar **F5** o click en **▶ Start**
5. El navegador se abrirá automáticamente con Swagger UI

---

### Opción 4: Usar VS Code
```bash
# 1. Abrir proyecto
code ApheliosID

# 2. Instalar extensión de C# (si no la tienes)
# Extensions → Buscar "C# Dev Kit"

# 3. Abrir terminal integrada (Ctrl+`)
dotnet restore
dotnet build

# 4. Ejecutar
dotnet run --project ApheliosID.API

# 5. Ctrl+Click en el link que aparece
# http://localhost:5000
```

---

### Opción 5: Ejecutar con Docker

#### Crear Dockerfile
```dockerfile
# En la raíz del proyecto: ApheliosID/Dockerfile

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar archivos del proyecto
COPY ["ApheliosID.API/ApheliosID.API.csproj", "ApheliosID.API/"]
COPY ["ApheliosID.Core/ApheliosID.Core.csproj", "ApheliosID.Core/"]

# Restaurar dependencias
RUN dotnet restore "ApheliosID.API/ApheliosID.API.csproj"

# Copiar todo el código
COPY . .

# Compilar
WORKDIR "/src/ApheliosID.API"
RUN dotnet build "ApheliosID.API.csproj" -c Release -o /app/build

# Publicar
FROM build AS publish
RUN dotnet publish "ApheliosID.API.csproj" -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "ApheliosID.API.dll"]
```

#### Comandos Docker
```bash
# 1. Construir imagen
docker build -t apheliosid .

# 2. Ejecutar contenedor
docker run -d -p 5000:80 --name apheliosid-api apheliosid

# 3. Ver logs
docker logs apheliosid-api

# 4. Detener
docker stop apheliosid-api

# 5. Eliminar
docker rm apheliosid-api
```

---

### Opción 6: Deploy en Render.com (Gratis)

1. **Crear cuenta en [Render.com](https://render.com)**

2. **Conectar GitHub**
   - Dashboard → New → Web Service
   - Conectar tu repositorio de GitHub

3. **Configurar**
   - **Name**: apheliosid
   - **Environment**: Docker
   - **Branch**: main
   - **Plan**: Free

4. **Variables de entorno** (opcional)
```
JWT_KEY=tu-super-secret-key-para-produccion
ASPNETCORE_ENVIRONMENT=Production
```

5. **Deploy**
   - Click "Create Web Service"
   - Render compilará y desplegará automáticamente

6. **URL pública**
   - Tu API estará en: `https://apheliosid.onrender.com`

---

### Opción 7: Publicar Ejecutable Standalone

#### Windows
```bash
# Compilar ejecutable para Windows
dotnet publish ApheliosID.API -c Release -r win-x64 --self-contained -o ./publish/windows

# Resultado: ./publish/windows/ApheliosID.API.exe
# Doble click para ejecutar
```

#### macOS
```bash
# Compilar ejecutable para macOS
dotnet publish ApheliosID.API -c Release -r osx-x64 --self-contained -o ./publish/macos

# Ejecutar
cd publish/macos
./ApheliosID.API
```

#### Linux
```bash
# Compilar ejecutable para Linux
dotnet publish ApheliosID.API -c Release -r linux-x64 --self-contained -o ./publish/linux

# Ejecutar
cd publish/linux
./ApheliosID.API
```

---

## 🌐 Acceder a la Aplicación

### Swagger UI (Documentación Interactiva)
```
http://localhost:5000
o
https://localhost:5001
```

### Endpoints Base
```
API Base URL: http://localhost:5000/api
```

### Puertos Personalizados

Si el puerto 5000 está ocupado, editar `appsettings.json`:
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:8080"
      }
    }
  }
}
```

O ejecutar con:
```bash
dotnet run --project ApheliosID.API --urls "http://localhost:8080"
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

## 🌍 Deploy y Producción

### Variables de Entorno (Producción)
```bash
# Linux/macOS
export ASPNETCORE_ENVIRONMENT=Production
export JWT_KEY=tu-super-secret-key-min-64-chars

# Windows CMD
set ASPNETCORE_ENVIRONMENT=Production
set JWT_KEY=tu-super-secret-key-min-64-chars

# Windows PowerShell
$env:ASPNETCORE_ENVIRONMENT="Production"
$env:JWT_KEY="tu-super-secret-key-min-64-chars"
```

### Configuración HTTPS
```bash
# Generar certificado de desarrollo
dotnet dev-certs https --trust
```

### Performance
```bash
# Compilar en modo Release
dotnet build -c Release

# Ejecutar optimizado
dotnet run -c Release --project ApheliosID.API
```

---

## 🛠️ Troubleshooting

### Puerto en uso
```bash
# Ver qué proceso usa el puerto 5000
# Linux/macOS
lsof -i :5000

# Windows
netstat -ano | findstr :5000

# Matar proceso
# Linux/macOS
kill -9 <PID>

# Windows
taskkill /PID <PID> /F
```

### Limpiar y recompilar
```bash
dotnet clean
dotnet restore
dotnet build
```

### Errores de compilación
```bash
# Verificar versión de .NET
dotnet --version

# Listar SDKs instalados
dotnet --list-sdks

# Reinstalar dependencias
rm -rf obj bin
dotnet restore
```

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
- [Docker Documentation](https://docs.docker.com/)
- [Render Deploy Guide](https://render.com/docs)

---

⭐ **Si este proyecto te ayudó, dale una estrella** ⭐
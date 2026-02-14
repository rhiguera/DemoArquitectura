# DemoArquitectura

Proyecto de demostración de arquitectura limpia con .NET 8, ASP.NET Core y Clean Architecture.

## Estructura del Proyecto

```
DemoArquitectura/
├── src/
│   ├── DemoArquitectura.Api/          # Capa de presentación (ASP.NET Core)
│   ├── DemoArquitectura.Application/  # Lógica de aplicación (servicios)
│   ├── DemoArquitectura.Domain/       # Entidades y especificaciones
│   └── DemoArquitectura.Infrastructure/ # Implementaciones de repositorios
└── tests/
    └── DemoArquitectura.Tests/        # Pruebas unitarias
```

## Tecnologías Utilizadas

- **.NET 8**: Framework para aplicaciones de escritorio y web
- **ASP.NET Core Web API**: Framework para crear APIs REST
- **Clean Architecture**: Patrón de arquitectura de software
- **Repositorio en Memoria**: Almacenamiento temporal de datos
- **Swagger/OpenAPI**: Documentación automática de API
- **xUnit**: Framework de pruebas unitarias

## Estructura de Capas

### 1. Domain Layer (DemoArquitectura.Domain)
- **Entities**: Clases de dominio (Product)
- **Repositories**: Interfaces de repositorios (IProductRepository)

### 2. Application Layer (DemoArquitectura.Application)
- **DTOs**: Data Transfer Objects (ProductDto, CreateProductDto, UpdateProductDto)
- **Services**: Servicios de aplicación (IProductService, ProductService)
- **Dependency Injection**: Configuración de inyección de dependencias

### 3. Infrastructure Layer (DemoArquitectura.Infrastructure)
- **Repositories**: Implementaciones de repositorios (InMemoryProductRepository)
- **Dependency Injection**: Registro de servicios de infraestructura

### 4. API Layer (DemoArquitectura.Api)
- **Controllers**: Controladores de API (ProductsController)
- **Program.cs**: Configuración de la aplicación
- **appsettings.json**: Configuración de la aplicación

### 5. Tests Layer (DemoArquitectura.Tests)
- **Unit Tests**: Pruebas unitarias con xUnit

## Cómo Ejecutar

### Requisitos Previos

- .NET 8 SDK instalado
- Visual Studio Code o Visual Studio

### Pasos

1. Navegar al directorio del proyecto:
   ```bash
   cd /home/rhm/Desarrollo/DemoArquitectura
   ```

2. Restaurar dependencias:
   ```bash
   dotnet restore
   ```

3. Ejecutar la aplicación:
   ```bash
   dotnet run --project src/DemoArquitectura.Api
   ```

4. Acceder a Swagger:
   ```
   https://localhost:7001/swagger
   ```

## Endpoints de la API

- **GET /api/products** - Obtener todos los productos
- **GET /api/products/{id}** - Obtener producto por ID
- **POST /api/products** - Crear nuevo producto
- **PUT /api/products/{id}** - Actualizar producto
- **DELETE /api/products/{id}** - Eliminar producto

## Ejecutar Pruebas

```bash
dotnet test
```

## Patrones y Principios Utilizados

- **Repository Pattern**: Abstracción del acceso a datos
- **Dependency Injection**: Inyección de dependencias
- **SOLID Principles**: Principios de diseño de software
- **Clean Architecture**: Separación de responsabilidades
- **DTOs**: Transferencia de datos entre capas

## Autor

Arquitecto Senior .NET

## Licencia

MIT
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
├── tests/
│   └── DemoArquitectura.Tests/        # Pruebas unitarias
├── web/
│   ├── index.html                     # Página principal
│   ├── css/styles.css                 # Estilos
│   ├── js/api.js                      # Cliente API (vanilla JS)
│   ├── js/app.js                      # Lógica de la aplicación
│   └── README.md                      # Documentación del proyecto web
└── DemoArquitectura.sln               # Solución Visual Studio
```

## Descripción de Carpetas

- **src/** - Proyectos .NET (Backend)
- **tests/** - Pruebas unitarias
- **web/** - Interfaz web (Frontend vanilla JavaScript)

## Dependencias entre Proyectos

### Diagrama de Dependencias

```
                    DemoArquitectura.Api
                           ↓
              (usa servicios y ext. DI)
                           ↓
        DemoArquitectura.Application ◄─────┐
                    ↓                       │
            (implementa interfaces)   (registra servicios)
                    ↓                       │
        DemoArquitectura.Domain       Infrastructure
                                       (implementa)
                                           ↓
                            DemoArquitectura.Tests
                            (prueba todos los anteriores)
```

### Tabla de Dependencias

| Proyecto | Dependencias | Descripción |
|----------|--------------|-------------|
| **Api** | Application, Infrastructure | Expone servicios como endpoints REST |
| **Application** | Domain | Define servicios e interfaces usadas por la API |
| **Infrastructure** | Domain | Implementa repositorios e inyección de dependencias |
| **Domain** | (ninguna) | No depende de otros proyectos (núcleo puro) |
| **Tests** | Application, Infrastructure, Domain | Prueba todas las capas |

### Referencias de Proyectos (.csproj)

**DemoArquitectura.Api.csproj**:
```xml
<ProjectReference Include="..\DemoArquitectura.Application\DemoArquitectura.Application.csproj" />
<ProjectReference Include="..\DemoArquitectura.Infrastructure\DemoArquitectura.Infrastructure.csproj" />
```

**DemoArquitectura.Application.csproj**:
```xml
<ProjectReference Include="..\DemoArquitectura.Domain\DemoArquitectura.Domain.csproj" />
```

**DemoArquitectura.Infrastructure.csproj**:
```xml
<ProjectReference Include="..\DemoArquitectura.Domain\DemoArquitectura.Domain.csproj" />
```

**DemoArquitectura.Tests.csproj**:
```xml
<ProjectReference Include="..\..\src\DemoArquitectura.Application\DemoArquitectura.Application.csproj" />
<ProjectReference Include="..\..\src\DemoArquitectura.Domain\DemoArquitectura.Domain.csproj" />
<ProjectReference Include="..\..\src\DemoArquitectura.Infrastructure\DemoArquitectura.Infrastructure.csproj" />
```

### Flujo de Dependencias

1. **Domain** → Punto de partida, sin dependencias externas
2. **Application** → Depende de Domain para usar entidades e interfaces
3. **Infrastructure** → Depende de Domain para implementar repositorios
4. **Api** → Depende de Application e Infrastructure
5. **Tests** → Verifica todos los anteriores

### Paquetes NuGet por Proyecto

| Proyecto | NuGet Packages | Versión |
|----------|--------|---------|
| Domain | (ninguno) | - |
| Application | Microsoft.Extensions.DependencyInjection.Abstractions | 8.0.0 |
| Infrastructure | Microsoft.Extensions.DependencyInjection.Abstractions | 8.0.0 |
| Api | Swashbuckle.AspNetCore | 6.4.0 |
| Tests | xunit, Moq, Microsoft.Extensions.DependencyInjection | 2.6.6, 4.20.70, 8.0.0 |

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

### Cobertura de Pruebas

El proyecto incluye **20 pruebas unitarias** que cubren:

#### ProductServiceTests (8 pruebas)
- Creación de productos con datos válidos
- Obtención de todos los productos
- Búsqueda de producto por ID
- Actualización de productos
- Eliminación de productos
- Manejo de casos de error (ID inválido, producto no encontrado)

#### InMemoryProductRepositoryTests (9 pruebas)
- Agregar productos al repositorio
- Obtener todos los productos
- Búsqueda por ID
- Actualización de productos existentes y no existentes
- Eliminación de productos
- Persistencia de datos entre llamadas
- Manejo de excepciones

#### DependencyInjectionTests (3 pruebas)
- Registro correcto de ProductService
- Registro correcto de ProductRepository
- Verificación de Singleton para el repositorio
- Configuración completa del contenedor de DI

**Resultado**: ✅ 20/20 pruebas pasadas exitosamente

## Patrones y Principios Utilizados

- **Repository Pattern**: Abstracción del acceso a datos
- **Dependency Injection**: Inyección de dependencias
- **SOLID Principles**: Principios de diseño de software
- **Clean Architecture**: Separación de responsabilidades
- **DTOs**: Transferencia de datos entre capas
- **Unit Testing**: Pruebas automatizadas con xUnit y Moq

## Autor

Arquitecto Senior .NET

## Licencia

MIT
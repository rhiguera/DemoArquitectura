# Arquitectura de DemoArquitectura

## Clean Architecture - Principios Aplicados

Este proyecto implementa los principios de **Clean Architecture** con una separación clara de responsabilidades en capas independientes.

## Capas de la Arquitectura

### 1. Domain Layer (Núcleo)
**Ubicación**: `src/DemoArquitectura.Domain/`

**Responsabilidad**: Define las entidades de negocio e interfaces de repositorio.

**Contenido**:
```
Domain/
├── Entities/
│   └── Product.cs           # Entidad de negocio
├── Repositories/
│   └── IProductRepository.cs # Contrato de repositorio (sin implementación)
└── GlobalUsings.cs          # Global using statements
```

**Características**:
- ✅ Sin dependencias de otros proyectos
- ✅ Sin referencias a frameworks externos
- ✅ Contiene solo la lógica de dominio pura
- ✅ Define contratos (interfaces) para la infraestructura

**Ejemplo de Entidad**:
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
}
```

---

### 2. Application Layer (Casos de Uso)
**Ubicación**: `src/DemoArquitectura.Application/`

**Responsabilidad**: Implementa la lógica de aplicación y servicios que orquestan el negocio.

**Contenido**:
```
Application/
├── DTOs/
│   ├── ProductDto.cs         # DTO para lectura
│   ├── CreateProductDto.cs   # DTO para creación
│   └── UpdateProductDto.cs   # DTO para actualización
├── Services/
│   ├── IProductService.cs    # Interfaz del servicio
│   └── ProductService.cs     # Implementación del servicio
├── DependencyInjectionExtensions.cs
└── GlobalUsings.cs
```

**Características**:
- ✅ Depende de Domain
- ✅ Define servicios de aplicación
- ✅ Mapea DTOs a entidades de Domain
- ✅ No depende de Infrastructure (inyección de dependencias)
- ✅ Implementa la lógica de negocio

**Ejemplo de Servicio**:
```csharp
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        var product = new Product { ... };
        var created = await _productRepository.AddAsync(product);
        return MapToDto(created);
    }
}
```

**DTOs**:
- `ProductDto` - Para leer productos
- `CreateProductDto` - Para crear productos
- `UpdateProductDto` - Para actualizar productos

---

### 3. Infrastructure Layer (Datos y Externos)
**Ubicación**: `src/DemoArquitectura.Infrastructure/`

**Responsabilidad**: Implementa los contratos del Domain y maneja el acceso a datos.

**Contenido**:
```
Infrastructure/
├── Repositories/
│   └── InMemoryProductRepository.cs  # Implementación en memoria
├── DependencyInjectionExtensions.cs  # Registro de servicios
└── GlobalUsings.cs
```

**Características**:
- ✅ Depende de Domain
- ✅ Implementa interfaces de IProductRepository
- ✅ Maneja persistencia (en memoria para esta demo)
- ✅ Registra servicios para inyección de dependencias
- ✅ Aislado del resto de la aplicación

**Ejemplo de Repositorio**:
```csharp
public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();
    
    public Task<Product> AddAsync(Product product)
    {
        product.Id = ++_nextId;
        _products.Add(product);
        return Task.FromResult(product);
    }
}
```

**Patrón Singleton**:
- El repositorio está registrado como `Singleton` para mantener datos en memoria
- En producción, usaría un `DbContext` como `Scoped`

---

### 4. API Layer (Presentación)
**Ubicación**: `src/DemoArquitectura.Api/`

**Responsabilidad**: Expone servicios como endpoints REST y maneja la configuración.

**Contenido**:
```
Api/
├── Controllers/
│   └── ProductsController.cs  # Endpoints REST
├── Properties/
│   └── launchSettings.json    # Configuración de ejecución
├── Program.cs                 # Configuración de la aplicación
├── appsettings.json           # Configuración
├── appsettings.Development.json
└── GlobalUsings.cs
```

**Características**:
- ✅ Capa más externa
- ✅ Depende de Application e Infrastructure
- ✅ Inyecta servicios en controladores
- ✅ Expone endpoints REST con Swagger
- ✅ Maneja solicitudes HTTP

**Ejemplo de Controlador**:
```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
    {
        var product = await _productService.CreateProductAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
}
```

**Configuración en Program.cs**:
```csharp
builder.Services.AddApplication();      // Registro de servicios Application
builder.Services.AddInfrastructure();    // Registro de repositorios
```

---

### 5. Tests Layer (Verificación)
**Ubicación**: `tests/DemoArquitectura.Tests/`

**Responsabilidad**: Valida la arquitectura y funcionalidad de todas las capas.

**Contenido**:
```
Tests/
├── ProductTests.cs                      # Pruebas de ProductService
├── InMemoryProductRepositoryTests.cs    # Pruebas de repositorio
├── DependencyInjectionTests.cs          # Pruebas de DI
└── GlobalUsings.cs
```

**Herramientas**:
- **xUnit** - Framework de pruebas
- **Moq** - Mocking de dependencias
- **Microsoft.Extensions.DependencyInjection** - Validación de DI

---

## Diagrama de Flujo de Datos

```
Cliente HTTP
     ↓
┌─────────────────────────────┐
│   Api Controller            │
│   (ProductsController)      │
└──────────┬──────────────────┘
           ↓
┌─────────────────────────────┐
│   Application Service       │
│   (ProductService)          │
│   - Valida DTOs             │
│   - Mapea a Entidades       │
└──────────┬──────────────────┘
           ↓
┌─────────────────────────────┐
│   Infrastructure Repository │
│   (InMemoryProductRepository)
│   - Accede a datos          │
│   - Retorna Entidades       │
└──────────┬──────────────────┘
           ↓
┌─────────────────────────────┐
│   Domain Entity             │
│   (Product)                 │
│   - Lógica de negocio       │
└─────────────────────────────┘
```

---

## Matriz de Dependencias

```
┌───────────────┬─────────┬──────────────┬───────────────┐
│ Capa          │ Domain  │ Application  │ Infrastructure│
├───────────────┼─────────┼──────────────┼───────────────┤
│ Domain        │ ✓       │              │               │
│ Application   │ ✓       │ ✓            │               │
│ Infrastructure│ ✓       │              │ ✓             │
│ Api           │ (DTOs)  │ ✓            │ ✓             │
│ Tests         │ ✓       │ ✓            │ ✓             │
└───────────────┴─────────┴──────────────┴───────────────┘

✓ = Depende de esta capa
```

---

## Principios SOLID Aplicados

### S - Single Responsibility Principle
- Cada capa tiene una única responsabilidad
- ProductService solo orquesta lógica de aplicación
- ProductRepository solo accede a datos

### O - Open/Closed Principle
- Las capas están abiertas para extensión
- Cerradas para modificación (interfaces)

### L - Liskov Substitution Principle
- IProductRepository puede ser reemplazado por otras implementaciones
- ProductService no conoce detalles de implementación

### I - Interface Segregation Principle
- Interfaces pequeñas y específicas
- IProductRepository define solo operaciones de producto

### D - Dependency Inversion Principle
- Las capas dependen de abstracciones (interfaces)
- No de implementaciones concretas
- Inyección de dependencias via constructor

---

## Ciclos de Vida de Servicios

### Singleton (Repositorio)
```csharp
services.AddSingleton<IProductRepository, InMemoryProductRepository>();
```
- **Una única instancia** para toda la aplicación
- Ideal para datos en memoria que deben persistir
- En producción: Usar Scoped con DbContext

### Scoped (En Producción Típica)
- Nueva instancia por solicitud HTTP
- Ideal para DbContext

### Transient
- Nueva instancia cada vez que se solicita
- Ideal para objetos sin estado

---

## Flujo de una Solicitud HTTP

### 1. Cliente envía POST /api/products
```json
{
  "name": "Laptop",
  "description": "High-performance laptop",
  "price": 999.99
}
```

### 2. ProductsController recibe la solicitud
```csharp
[HttpPost]
public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
{
    var product = await _productService.CreateProductAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
}
```

### 3. ProductService procesa la lógica
```csharp
public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
{
    var product = new Product
    {
        Name = dto.Name,
        Description = dto.Description,
        Price = dto.Price,
        CreatedDate = DateTime.UtcNow
    };
    var created = await _productRepository.AddAsync(product);
    return MapToDto(created);
}
```

### 4. Repositorio guarda los datos
```csharp
public Task<Product> AddAsync(Product product)
{
    product.Id = ++_nextId;
    _products.Add(product);
    return Task.FromResult(product);
}
```

### 5. Respuesta al cliente
```json
{
  "id": 1,
  "name": "Laptop",
  "description": "High-performance laptop",
  "price": 999.99,
  "createdDate": "2026-02-15T10:30:00Z"
}
```

---

## Ventajas de esta Arquitectura

✅ **Mantenibilidad**: Capas claramente separadas y responsabilidades definidas
✅ **Testabilidad**: Fácil de mockear dependencias
✅ **Escalabilidad**: Agregar nuevas funcionalidades sin afectar capas existentes
✅ **Reutilización**: Servicios pueden usarse en diferentes contextos
✅ **Independencia de Frameworks**: Domain layer no depende de ASP.NET Core
✅ **Independencia de Bases de Datos**: Implementación de repositorio intercambiable

---

## Próximos Pasos para Producción

1. **Base de Datos**: Reemplazar InMemoryProductRepository con Entity Framework Core
2. **Autenticación**: Agregar JWT tokens en ProductsController
3. **Validación**: Implementar FluentValidation para DTOs
4. **Logging**: Agregar Serilog para logging distribuido
5. **Caching**: Redis para mejorar performance
6. **Documentación**: OpenAPI/Swagger más detallado
7. **CI/CD**: Pipelines de GitHub Actions o Azure DevOps

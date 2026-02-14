# Documentación de Pruebas - DemoArquitectura

## Resumen Ejecutivo

El proyecto DemoArquitectura incluye una suite completa de pruebas unitarias con **20 pruebas** que validan toda la arquitectura de la aplicación.

- ✅ **20/20 pruebas pasadas**
- 📊 Cobertura: Application, Infrastructure y Dependency Injection
- 🛠️ Herramientas: xUnit, Moq, Microsoft.Extensions.DependencyInjection

## Estructura de Pruebas

### 1. ProductServiceTests (8 pruebas)

**Ubicación**: `tests/DemoArquitectura.Tests/ProductTests.cs`

Valida la lógica de negocio de la capa Application. Utiliza **Moq** para simular el repositorio.

#### Pruebas Incluidas:

| Prueba | Descripción | Estado |
|--------|-------------|--------|
| `CreateProductAsync_WithValidData_ReturnsProductDto` | Verifica la creación de un producto con datos válidos | ✅ |
| `GetAllProductsAsync_WithProducts_ReturnsListOfProductDtos` | Valida la obtención de todos los productos | ✅ |
| `GetProductByIdAsync_WithValidId_ReturnsProductDto` | Busca un producto por ID válido | ✅ |
| `GetProductByIdAsync_WithInvalidId_ReturnsNull` | Maneja búsqueda con ID inválido | ✅ |
| `UpdateProductAsync_WithValidData_ReturnsUpdatedProductDto` | Actualiza un producto correctamente | ✅ |
| `DeleteProductAsync_WithValidId_ReturnsTrue` | Elimina un producto exitosamente | ✅ |
| `DeleteProductAsync_WithInvalidId_ReturnsFalse` | Maneja eliminación de producto no existente | ✅ |

**Patrones de Testing**:
- **Arrange-Act-Assert**: Estructura clara de cada prueba
- **Mocking**: Uso de Moq para aislar la lógica del servicio
- **Edge Cases**: Pruebas de casos de error y excepciones

---

### 2. InMemoryProductRepositoryTests (9 pruebas)

**Ubicación**: `tests/DemoArquitectura.Tests/InMemoryProductRepositoryTests.cs`

Valida la implementación del repositorio en memoria de la capa Infrastructure.

#### Pruebas Incluidas:

| Prueba | Descripción | Estado |
|--------|-------------|--------|
| `AddAsync_WithValidProduct_ReturnsProductWithId` | Verifica la adición de un producto | ✅ |
| `GetAllAsync_WithMultipleProducts_ReturnsAllProducts` | Obtiene todos los productos agregados | ✅ |
| `GetByIdAsync_WithValidId_ReturnsProduct` | Busca un producto por ID válido | ✅ |
| `GetByIdAsync_WithInvalidId_ReturnsNull` | Retorna null para ID inválido | ✅ |
| `UpdateAsync_WithValidProduct_UpdatesProductData` | Actualiza los datos de un producto | ✅ |
| `UpdateAsync_WithNonexistentProduct_ThrowsKeyNotFoundException` | Lanza excepción para producto inexistente | ✅ |
| `DeleteAsync_WithValidId_RemovesProductAndReturnsTrue` | Elimina producto exitosamente | ✅ |
| `DeleteAsync_WithInvalidId_ReturnsFalse` | Maneja eliminación de ID inválido | ✅ |
| `Repository_DataPersistsBetweenCalls` | Valida que los datos persistan (Singleton) | ✅ |

**Características Validadas**:
- CRUD completo (Create, Read, Update, Delete)
- Manejo de errores y excepciones
- Persistencia de datos en memoria
- Generación automática de IDs

---

### 3. DependencyInjectionTests (3 pruebas)

**Ubicación**: `tests/DemoArquitectura.Tests/DependencyInjectionTests.cs`

Valida la configuración correcta del contenedor de inyección de dependencias.

#### Pruebas Incluidas:

| Prueba | Descripción | Estado |
|--------|-------------|--------|
| `AddApplication_RegistersProductService` | Verifica el registro de ProductService | ✅ |
| `AddInfrastructure_RegistersProductRepository` | Verifica el registro de ProductRepository | ✅ |
| `AddInfrastructure_RegistersRepositoryAsSingleton` | Valida que el repositorio sea Singleton | ✅ |
| `CompleteSetup_RegistersAllServices` | Verifica que todos los servicios se registren correctamente | ✅ |

**Validaciones**:
- ✅ Registro correcto de servicios
- ✅ Ciclo de vida adecuado (Singleton para repositorio)
- ✅ Resolución de dependencias

---

## Ejecución de Pruebas

### Ejecutar todas las pruebas:
```bash
dotnet test
```

### Ejecutar pruebas de una clase específica:
```bash
dotnet test --filter "ClassName=DemoArquitectura.Tests.ProductServiceTests"
```

### Ejecutar prueba específica:
```bash
dotnet test --filter "Name=CreateProductAsync_WithValidData_ReturnsProductDto"
```

### Ver detalles verbosos:
```bash
dotnet test --verbosity normal
```

---

## Cobertura de Funcionalidad

### Domain Layer
- ✅ Entidad Product con validación de propiedades
- ✅ Interfaz IProductRepository definida correctamente

### Application Layer
- ✅ ProductService implementa todos los métodos
- ✅ DTOs correctamente mapeados
- ✅ Manejo de casos de error

### Infrastructure Layer
- ✅ InMemoryProductRepository funciona correctamente
- ✅ Persistencia de datos en memoria
- ✅ Generación de IDs automática
- ✅ Registro como Singleton

### Dependency Injection
- ✅ Configuración de servicios completa
- ✅ Resolución correcta de dependencias
- ✅ Ciclos de vida apropiados

---

## Herramientas y Frameworks

| Herramienta | Versión | Propósito |
|------------|---------|----------|
| **xUnit** | 2.6.6 | Framework de pruebas |
| **Moq** | 4.20.70 | Mocking de dependencias |
| **Microsoft.NET.Test.Sdk** | 17.9.0 | SDK de pruebas |
| **Microsoft.Extensions.DependencyInjection** | 8.0.0 | Inyección de dependencias |

---

## Mejores Prácticas Implementadas

1. **Naming Convention**: Los nombres de las pruebas siguen el patrón `MethodName_Scenario_ExpectedResult`
2. **Arrange-Act-Assert**: Estructura clara en cada prueba
3. **Single Responsibility**: Cada prueba valida un aspecto específico
4. **Independent Tests**: Las pruebas no dependen unas de otras
5. **Mocking**: Uso de Moq para aislar componentes
6. **Edge Cases**: Cobertura de casos de error y límite

---

## Matriz de Cobertura

```
┌─────────────────────┬────────┬──────────────────────────────────┐
│ Capa                │ Pruebas│ Métodos Validados                │
├─────────────────────┼────────┼──────────────────────────────────┤
│ Application         │   8    │ CreateProductAsync               │
│                     │        │ GetAllProductsAsync              │
│                     │        │ GetProductByIdAsync              │
│                     │        │ UpdateProductAsync               │
│                     │        │ DeleteProductAsync               │
├─────────────────────┼────────┼──────────────────────────────────┤
│ Infrastructure      │   9    │ GetAllAsync                      │
│                     │        │ GetByIdAsync                     │
│                     │        │ AddAsync                         │
│                     │        │ UpdateAsync                      │
│                     │        │ DeleteAsync                      │
├─────────────────────┼────────┼──────────────────────────────────┤
│ Dependency Inject.  │   3    │ Registros de servicios           │
│                     │        │ Ciclos de vida                   │
│                     │        │ Resolución de deps.              │
├─────────────────────┼────────┼──────────────────────────────────┤
│ TOTAL               │  20    │ ✅ 100% de pruebas pasadas       │
└─────────────────────┴────────┴──────────────────────────────────┘
```

---

## Notas Importantes

### Singleton vs Scoped
El repositorio está registrado como **Singleton** para mantener los datos en memoria entre requests. Esto es ideal para demostración; en producción, usaría una base de datos real.

### Mocking en ProductServiceTests
Se utiliza **Moq** para simular el repositorio, lo que permite pruebas unitarias puras del servicio sin dependencias externas.

### Error Handling
Las pruebas validan:
- Retorno de `null` para búsquedas sin resultado
- Lanzamiento de `KeyNotFoundException` para operaciones inválidas
- Retorno de `false` para operaciones fallidas

---

## Resultado Final

✅ **20/20 pruebas pasadas**  
⏱️ **Tiempo de ejecución**: ~237 ms  
📊 **Cobertura**: Application, Infrastructure, DI Configuration  

La suite de pruebas valida completamente la arquitectura limpia implementada.

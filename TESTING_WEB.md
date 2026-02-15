# Pruebas Manual - Interfaz Web

Guía para probar manualmente la interfaz web de DemoArquitectura.

## Requisitos Previos

1. **API ejecutándose**:
   ```bash
   dotnet run --project src/DemoArquitectura.Api
   ```

2. **Live Server en VS Code**:
   - Click derecho en `web/index.html` → "Open with Live Server"
   - O cualquier servidor HTTP que sirva archivos estáticos

## Escenarios de Prueba

### 1. Cargar lista de productos
- [ ] La página carga con mensaje "Cargando productos..."
- [ ] Desaparece el loading y se muestra la tabla
- [ ] Si no hay productos, se muestra "No hay productos disponibles"
- [ ] La tabla muestra todas las columnas correctas (ID, Nombre, Descripción, Precio)

### 2. Crear un nuevo producto
- [ ] Rellenar el formulario:
  - Nombre: "Laptop Dell XPS 13"
  - Descripción: "Portátil profesional de alta gama"
  - Precio: "1299.99"
- [ ] Click en "Crear Producto"
- [ ] Aparece mensaje verde de éxito
- [ ] El formulario se limpia automáticamente
- [ ] Después de 1 segundo, aparece el nuevo producto en la tabla
- [ ] La tabla se actualiza sin recargar la página

### 3. Validación del formulario
- [ ] Intentar crear sin rellenar campos → Error de validación HTML
- [ ] Precio negativo → No se permite enviar
- [ ] Dejar campos vacíos → Error de validación
- [ ] Click en "Limpiar" → Formulario se resetea

### 4. Manejo de errores
- [ ] Si la API no está disponible → Mensaje de error clara
- [ ] Si hay error en la creación → Mensaje de error con detalles
- [ ] Si hay error al cargar → Se muestra error en rojo

### 5. Formateo de datos
- [ ] Los precios se muestran con 2 decimales (ej: $1299.99)
- [ ] Los nombres y descripciones se muestran correctamente (escape de HTML)
- [ ] Caracteres especiales se escapan correctamente

## Flujo de Prueba Completo

```
1. Abre el navegador en Live Server
   ↓
2. Verifica que se cargan los productos existentes
   ↓
3. Rellena el formulario con un nuevo producto
   ↓
4. Click en "Crear Producto"
   ↓
5. Verifica el mensaje de éxito
   ↓
6. Verifica que el nuevo producto aparece en la tabla
   ↓
7. Intenta crear otro producto
   ↓
8. Verifica que ahora hay 2+ productos en la lista
```

## Datos de Prueba Sugeridos

```javascript
// Producto 1
{
    "name": "Monitor LG 27 pulgadas",
    "description": "Monitor 4K UltraWide para estación de trabajo",
    "price": 549.99
}

// Producto 2
{
    "name": "Teclado Mecánico RGB",
    "description": "Teclado gaming con switches Cherry MX",
    "price": 149.99
}

// Producto 3
{
    "name": "Mouse Logitech MX Master",
    "description": "Mouse inalámbrico de precisión para profesionales",
    "price": 99.99
}
```

## Herramientas de Desarrollo

Para debugging más avanzado:

### 1. Consola del Navegador (F12 → Console):
- Verificar llamadas API
- Ver logs de la aplicación
- Inspeccionar variables

### 2. Network Tab:
- Verificar solicitudes GET a `/api/products`
- Verificar solicitudes POST a `/api/products`
- Ver códigos de estado HTTP
- Inspeccionar headers y body

### 3. Ejemplo en Consola:
```javascript
// Obtener todos los productos
API.getAllProducts().then(products => console.log(products));

// Crear un nuevo producto
API.createProduct({
    name: "Test",
    description: "Test product",
    price: 99.99
}).then(result => console.log(result));
```

## Checklist de Validación

- [ ] **GET /products**: Carga la lista correctamente
- [ ] **POST /products**: Crea nuevo producto y retorna datos
- [ ] **Validación HTML**: El navegador valida el formulario
- [ ] **Mensajes**: Éxito y error se muestran correctamente
- [ ] **Formateo**: Precios y datos se formatean correctamente
- [ ] **Seguridad**: No hay vulnerabilidades XSS
- [ ] **Performance**: Sin lag al crear o cargar
- [ ] **Responsive**: La interfaz se ve bien en móvil

## Funcionalidades Implementadas

✅ **Listar Productos**
- GET /api/products
- Visualización en tabla
- Formateo de precios
- Estados: Loading, Error, Vacío, Datos

✅ **Crear Productos**
- POST /api/products
- Formulario con validación
- Campos: Nombre, Descripción, Precio
- Mensajes de éxito/error
- Recarga automática de lista

## Próximas Características

- [ ] Editar producto (PUT)
- [ ] Eliminar producto (DELETE)
- [ ] Búsqueda y filtrado
- [ ] Paginación
- [ ] Modo oscuro

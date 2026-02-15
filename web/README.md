# Proyecto Web - DemoArquitectura

Interfaz web para consumir la API REST de DemoArquitectura.

## Características

- 📱 Interfaz responsive
- 🚀 JavaScript vanilla (sin frameworks)
- 🎨 Estilos minimalistas
- 🔒 Protección contra XSS
- ⚡ Carga rápida
- ✅ Operaciones CRUD básicas

## Estructura del Proyecto

```
web/
├── index.html        # Página principal
├── css/
│   └── styles.css    # Estilos
├── js/
│   ├── api.js       # Cliente API
│   └── app.js       # Lógica de la aplicación
└── README.md        # Este archivo
```

## Cómo Usar

### Requisitos

- Navegador moderno (Chrome, Firefox, Safari, Edge)
- API de DemoArquitectura ejecutándose en `https://localhost:7001`

### Pasos

1. Asegúrate de que la API está corriendo:
   ```bash
   dotnet run --project src/DemoArquitectura.Api
   ```

2. Abre `index.html` en tu navegador:
   - Usando Live Server en VS Code
   - O directamente: `open index.html`

3. La página cargará automáticamente la lista de productos

## Funcionalidades Actuales

### Listar Productos ✅
- Carga automática de productos al abrir la página
- Tabla con columnas: ID, Nombre, Descripción, Precio
- Formateo de precios a 2 decimales
- Estado de carga con animación
- Manejo de errores
- Estado vacío cuando no hay productos

### Crear Producto ✅
- Formulario con validación del lado del cliente
- Campos requeridos: Nombre, Descripción, Precio
- Validación de precio (debe ser mayor a 0)
- Mensajes de éxito y error
- Recarga automática de la lista después de crear
- Limpieza automática del formulario
- Escapado de HTML para prevenir XSS

## Implementación Técnica

### Módulo API (`js/api.js`)

El módulo `API` proporciona métodos para consumir la API REST:

```javascript
// Obtener todos los productos
const products = await API.getAllProducts();

// Obtener un producto por ID
const product = await API.getProductById(1);

// Crear un nuevo producto
const newProduct = await API.createProduct({
    name: "Producto",
    description: "Descripción",
    price: 99.99
});
```

### Módulo Aplicación (`js/app.js`)

Maneja la lógica de la interfaz:

- `initApp()` - Inicialización de la aplicación
- `handleProductFormSubmit()` - Procesamiento del formulario
- `loadProducts()` - Carga de productos
- `renderProducts()` - Renderizado de tabla
- `showFormSuccess()` / `showFormError()` - Mensajes
- Utilidades: `escapeHtml()`, `formatPrice()`

## Próximas Características

- [ ] Editar producto (PUT)
- [ ] Eliminar producto (DELETE)
- [ ] Búsqueda y filtrado
- [ ] Paginación
- [ ] Modo oscuro
- [ ] Confirmación en eliminación
- [ ] Indicador de carga en botón de envío

## Notas de Desarrollo

### CORS
Si obtienes error de CORS, asegúrate de que la API permite solicitudes desde `localhost`.

### Seguridad
- Los datos se escapan para prevenir XSS
- Las solicitudes usan HTTPS
- Sin almacenamiento de datos sensibles en el navegador
- Validación del lado del cliente (también hacer en el servidor)

### Performance
- Carga mínima de recursos
- Sin dependencias externas
- Solicitudes optimizadas a la API
- Recarga selectiva después de crear producto

## Commits Progresivos

Este proyecto se desarrolla incrementalmente con commits específicos para cada funcionalidad:

1. ✅ `feat: estructura base del proyecto web`
2. ✅ `feat: interfaz para listar productos`
3. ✅ `feat: consumir API GetAll de productos`
4. ✅ `feat: formulario para crear productos`
5. ✅ `feat: consumir API POST para crear productos`
6. [ ] `feat: interfaz para actualizar productos`
7. [ ] `feat: interfaz para eliminar productos`

## Licencia

MIT

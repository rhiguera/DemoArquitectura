# Proyecto Web - DemoArquitectura

Interfaz web para consumir la API REST de DemoArquitectura.

## Características

- 📱 Interfaz responsive
- 🚀 JavaScript vanilla (sin frameworks)
- 🎨 Estilos minimalistas
- 🔒 Protección contra XSS
- ⚡ Carga rápida

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

- ✅ Listar todos los productos
- ✅ Mostrar información completa (ID, Nombre, Descripción, Precio, Fecha)
- ✅ Formateo de precios y fechas
- ✅ Manejo de errores
- ✅ Estado de carga
- ✅ Estado vacío

## Próximas Características

- [ ] Agregar producto
- [ ] Editar producto
- [ ] Eliminar producto
- [ ] Búsqueda y filtrado
- [ ] Paginación
- [ ] Modo oscuro

## Notas de Desarrollo

### CORS
Si obtienes error de CORS, asegúrate de que la API permite solicitudes desde `localhost`.

### Seguridad
- Los datos se escapan para prevenir XSS
- Las solicitudes usan HTTPS
- Sin almacenamiento de datos sensibles en el navegador

### Performance
- Carga mínima de recursos
- Sin dependencias externas
- Solicitudes optimizadas a la API

## Commits Progresivos

Este proyecto se desarrolla incrementalmente con commits específicos para cada funcionalidad:

1. `feat: estructura base del proyecto web`
2. `feat: interfaz para listar productos`
3. `feat: consumir API GetAll de productos`
4. Y más...

## Licencia

MIT

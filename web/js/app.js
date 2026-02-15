/**
 * Aplicación Principal
 * 
 * Maneja la lógica de la interfaz web
 */

// Elementos del DOM
const DOM = {
    // Formulario
    productForm: document.getElementById('productForm'),
    formError: document.getElementById('formError'),
    formSuccess: document.getElementById('formSuccess'),
    
    // Tabla de productos
    loading: document.getElementById('loading'),
    error: document.getElementById('error'),
    table: document.getElementById('productsTable'),
    tableBody: document.getElementById('productsBody'),
    emptyState: document.getElementById('emptyState')
};

/**
 * Inicializa la aplicación
 */
function initApp() {
    // Cargar productos
    loadProducts();
    
    // Configurar event listener del formulario
    DOM.productForm.addEventListener('submit', handleProductFormSubmit);
}

/**
 * Maneja el envío del formulario de creación de producto
 * @param {Event} e - Evento del formulario
 */
async function handleProductFormSubmit(e) {
    e.preventDefault();
    
    try {
        hideFormMessages();
        
        // Obtener datos del formulario
        const formData = new FormData(DOM.productForm);
        const productData = {
            name: formData.get('name').trim(),
            description: formData.get('description').trim(),
            price: parseFloat(formData.get('price'))
        };
        
        // Validaciones básicas
        if (!productData.name || !productData.description || productData.price < 0) {
            showFormError('Por favor completa todos los campos correctamente');
            return;
        }
        
        // Crear el producto
        const createdProduct = await API.createProduct(productData);
        
        // Mostrar mensaje de éxito
        showFormSuccess(`Producto "${productData.name}" creado exitosamente`);
        
        // Limpiar formulario
        DOM.productForm.reset();
        
        // Recargar lista de productos
        setTimeout(() => {
            loadProducts();
        }, 1000);
        
    } catch (error) {
        console.error('Error al crear producto:', error);
        showFormError(`Error al crear el producto: ${error.message}`);
    }
}

/**
 * Carga los productos desde la API
 */
async function loadProducts() {
    try {
        showLoading();
        const products = await API.getAllProducts();
        
        if (Array.isArray(products) && products.length > 0) {
            renderProducts(products);
        } else {
            showEmptyState();
        }
    } catch (error) {
        showError(`No se pudieron cargar los productos: ${error.message}`);
    }
}

/**
 * Renderiza los productos en la tabla
 * @param {Array} products - Array de productos
 */
function renderProducts(products) {
    hideLoading();
    hideError();

    // Limpiar tabla anterior
    DOM.tableBody.innerHTML = '';

    // Agregar filas
    products.forEach(product => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${product.id}</td>
            <td>${escapeHtml(product.name)}</td>
            <td>${escapeHtml(product.description)}</td>
            <td class="price">$${formatPrice(product.price)}</td>
        `;
        DOM.tableBody.appendChild(row);
    });

    // Mostrar tabla
    DOM.table.style.display = 'table';
    DOM.emptyState.style.display = 'none';
}

/**
 * Muestra el indicador de carga
 */
function showLoading() {
    DOM.loading.style.display = 'block';
    DOM.error.style.display = 'none';
    DOM.table.style.display = 'none';
    DOM.emptyState.style.display = 'none';
}

/**
 * Oculta el indicador de carga
 */
function hideLoading() {
    DOM.loading.style.display = 'none';
}

/**
 * Muestra un mensaje de error
 * @param {string} message - Mensaje de error
 */
function showError(message) {
    hideLoading();
    DOM.error.textContent = message;
    DOM.error.style.display = 'block';
    DOM.table.style.display = 'none';
    DOM.emptyState.style.display = 'none';
}

/**
 * Oculta el mensaje de error
 */
function hideError() {
    DOM.error.style.display = 'none';
}

/**
 * Muestra un mensaje de error del formulario
 * @param {string} message - Mensaje de error
 */
function showFormError(message) {
    DOM.formError.textContent = message;
    DOM.formError.style.display = 'block';
    DOM.formSuccess.style.display = 'none';
}

/**
 * Muestra un mensaje de éxito del formulario
 * @param {string} message - Mensaje de éxito
 */
function showFormSuccess(message) {
    DOM.formSuccess.textContent = message;
    DOM.formSuccess.style.display = 'block';
    DOM.formError.style.display = 'none';
}

/**
 * Oculta todos los mensajes del formulario
 */
function hideFormMessages() {
    DOM.formError.style.display = 'none';
    DOM.formSuccess.style.display = 'none';
}

/**
 * Muestra el estado vacío
 */
function showEmptyState() {
    hideLoading();
    hideError();
    DOM.table.style.display = 'none';
    DOM.emptyState.style.display = 'block';
}

/**
 * Formatea un precio a dos decimales
 * @param {number} price - Precio
 * @returns {string} - Precio formateado
 */
function formatPrice(price) {
    return parseFloat(price).toFixed(2);
}

/**
 * Escapa caracteres HTML para prevenir XSS
 * @param {string} text - Texto a escapar
 * @returns {string} - Texto escapado
 */
function escapeHtml(text) {
    const map = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#039;'
    };
    return text.replace(/[&<>"']/g, char => map[char]);
}

/**
 * Inicia la aplicación cuando el DOM esté listo
 */
document.addEventListener('DOMContentLoaded', initApp);

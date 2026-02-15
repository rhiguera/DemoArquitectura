/**
 * Aplicación Principal
 * 
 * Maneja la lógica de la interfaz web
 */

// Elementos del DOM
const DOM = {
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
    loadProducts();
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
            <td class="date">${formatDate(product.createdDate)}</td>
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
 * Formatea una fecha ISO al formato local
 * @param {string} dateString - Fecha en formato ISO
 * @returns {string} - Fecha formateada
 */
function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString('es-ES', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });
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

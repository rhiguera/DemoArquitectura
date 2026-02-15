/**
 * API Service
 * 
 * Módulo para consumir la API REST de DemoArquitectura
 */

const API = {
    // Configuración
    baseUrl: 'https://localhost:7001/api',
    
    /**
     * Realiza una solicitud GET a la API
     * @param {string} endpoint - Ruta del endpoint
     * @returns {Promise} - Promesa que resuelve con los datos
     */
    async get(endpoint) {
        try {
            const response = await fetch(`${this.baseUrl}${endpoint}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();
            return data;
        } catch (error) {
            console.error('Error en la solicitud API:', error);
            throw error;
        }
    },

    /**
     * Obtiene todos los productos
     * @returns {Promise<Array>} - Array de productos
     */
    async getAllProducts() {
        return this.get('/products');
    },

    /**
     * Obtiene un producto por ID
     * @param {number} id - ID del producto
     * @returns {Promise<Object>} - Objeto producto
     */
    async getProductById(id) {
        return this.get(`/products/${id}`);
    }
};

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
     * Realiza una solicitud POST a la API
     * @param {string} endpoint - Ruta del endpoint
     * @param {Object} data - Datos a enviar
     * @returns {Promise} - Promesa que resuelve con los datos de respuesta
     */
    async post(endpoint, data) {
        try {
            const response = await fetch(`${this.baseUrl}${endpoint}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify(data)
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || `HTTP error! status: ${response.status}`);
            }

            const result = await response.json();
            return result;
        } catch (error) {
            console.error('Error en la solicitud POST:', error);
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
    },

    /**
     * Crea un nuevo producto
     * @param {Object} productData - Datos del producto
     * @param {string} productData.name - Nombre del producto
     * @param {string} productData.description - Descripción del producto
     * @param {number} productData.price - Precio del producto
     * @returns {Promise<Object>} - Objeto del producto creado
     */
    async createProduct(productData) {
        return this.post('/products', productData);
    }
};

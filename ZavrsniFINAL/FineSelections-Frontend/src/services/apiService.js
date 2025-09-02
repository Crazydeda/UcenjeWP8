// For local development
// const API_BASE_URL = 'http://localhost:5138/api';
// For production deployment where frontend and backend are on the same domain
const API_BASE_URL = '/api';

class ApiService {
  async makeRequest(endpoint, options = {}) {
    const url = `${API_BASE_URL}${endpoint}`;
    const config = {
      headers: {
        'Content-Type': 'application/json',
        ...options.headers,
      },
      ...options,
    };

    try {
      const response = await fetch(url, config);
      
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      // Check if response has content before trying to parse JSON
      const contentLength = response.headers.get('content-length');
      const contentType = response.headers.get('content-type');
      
      // If no content or content-length is 0, return null
      if (response.status === 204 || contentLength === '0') {
        return null;
      }
      
      // If content-type indicates JSON, parse it
      if (contentType && contentType.includes('application/json')) {
        return await response.json();
      }
      
      // For other content types, return the response text
      const text = await response.text();
      return text || null;
    } catch (error) {
      console.error('API request failed:', error);
      throw error;
    }
  }

  // Products
  async getProducts(type = null) {
    const endpoint = type ? `/proizvodi?vrsta=${type}` : '/proizvodi';
    return this.makeRequest(endpoint);
  }

  async getProduct(id) {
    return this.makeRequest(`/proizvodi/${id}`);
  }

  async createProduct(productData) {
    return this.makeRequest('/proizvodi', {
      method: 'POST',
      body: JSON.stringify(productData),
    });
  }

  async updateProduct(id, productData) {
    return this.makeRequest(`/proizvodi/${id}`, {
      method: 'PUT',
      body: JSON.stringify(productData),
    });
  }

  async deleteProduct(id) {
    return this.makeRequest(`/proizvodi/${id}`, {
      method: 'DELETE',
    });
  }

  async getProductTypes() {
    return this.makeRequest('/proizvodi/vrste');
  }

  // Users
  async getUsers() {
    return this.makeRequest('/korisnici');
  }

  async getUser(id) {
    return this.makeRequest(`/korisnici/${id}`);
  }

  async createUser(userData) {
    return this.makeRequest('/korisnici', {
      method: 'POST',
      body: JSON.stringify(userData),
    });
  }

  async updateUser(id, userData) {
    return this.makeRequest(`/korisnici/${id}`, {
      method: 'PUT',
      body: JSON.stringify(userData),
    });
  }

  // Shopping Carts
  async getCarts() {
    return this.makeRequest('/kosarice');
  }

  async getCart(id) {
    return this.makeRequest(`/kosarice/${id}`);
  }

  async getCartsByUser(userId) {
    return this.makeRequest(`/kosarice/korisnik/${userId}`);
  }

  async createCart(cartData) {
    return this.makeRequest('/kosarice', {
      method: 'POST',
      body: JSON.stringify(cartData),
    });
  }

  async updateCart(id, cartData) {
    return this.makeRequest(`/kosarice/${id}`, {
      method: 'PUT',
      body: JSON.stringify(cartData),
    });
  }

  async deleteCart(id) {
    return this.makeRequest(`/kosarice/${id}`, {
      method: 'DELETE',
    });
  }

  // Cart Items
  async getCartItems() {
    return this.makeRequest('/stavkekosarice');
  }

  async getCartItem(id) {
    return this.makeRequest(`/stavkekosarice/${id}`);
  }

  async getCartItemsByCart(cartId) {
    return this.makeRequest(`/stavkekosarice/kosarica/${cartId}`);
  }

  async addCartItem(itemData) {
    return this.makeRequest('/stavkekosarice', {
      method: 'POST',
      body: JSON.stringify(itemData),
    });
  }

  async updateCartItem(id, itemData) {
    return this.makeRequest(`/stavkekosarice/${id}`, {
      method: 'PUT',
      body: JSON.stringify(itemData),
    });
  }

  async deleteCartItem(id) {
    return this.makeRequest(`/stavkekosarice/${id}`, {
      method: 'DELETE',
    });
  }

  // Orders
  async getOrders() {
    return this.makeRequest('/narudzbe');
  }

  async getOrder(id) {
    return this.makeRequest(`/narudzbe/${id}`);
  }

  async getOrdersByUser(userId) {
    return this.makeRequest(`/narudzbe/korisnik/${userId}`);
  }

  async createOrder(orderData) {
    return this.makeRequest('/narudzbe', {
      method: 'POST',
      body: JSON.stringify(orderData),
    });
  }

  async updateOrder(id, orderData) {
    return this.makeRequest(`/narudzbe/${id}`, {
      method: 'PUT',
      body: JSON.stringify(orderData),
    });
  }

  async deleteOrder(id) {
    return this.makeRequest(`/narudzbe/${id}`, {
      method: 'DELETE',
    });
  }

  // Order Items
  async getOrderItems() {
    return this.makeRequest('/stavkenarudzbe');
  }

  async getOrderItem(id) {
    return this.makeRequest(`/stavkenarudzbe/${id}`);
  }

  async getOrderItemsByOrder(orderId) {
    return this.makeRequest(`/stavkenarudzbe/narudzba/${orderId}`);
  }

  async addOrderItem(itemData) {
    return this.makeRequest('/stavkenarudzbe', {
      method: 'POST',
      body: JSON.stringify(itemData),
    });
  }

  async updateOrderItem(id, itemData) {
    return this.makeRequest(`/stavkenarudzbe/${id}`, {
      method: 'PUT',
      body: JSON.stringify(itemData),
    });
  }

  async deleteOrderItem(id) {
    return this.makeRequest(`/stavkenarudzbe/${id}`, {
      method: 'DELETE',
    });
  }
}

export default new ApiService();

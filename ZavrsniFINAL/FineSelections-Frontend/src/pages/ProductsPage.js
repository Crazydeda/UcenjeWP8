import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Form, Button } from 'react-bootstrap';
import { useSearchParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import ProductCard from '../components/ProductCard';
import ProductEditModal from '../components/ProductEditModal';
import LoadingSpinner from '../components/LoadingSpinner';
import apiService from '../services/apiService';

function ProductsPage({ cart, updateCart, user }) {
  const [products, setProducts] = useState([]);
  const [filteredProducts, setFilteredProducts] = useState([]);
  const [productTypes, setProductTypes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedType, setSelectedType] = useState('');
  const [sortBy, setSortBy] = useState('name');
  const [showEditModal, setShowEditModal] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [searchParams] = useSearchParams();

  useEffect(() => {
    loadProducts();
    loadProductTypes();
    
    // Check for type parameter in URL
    const typeParam = searchParams.get('type');
    if (typeParam) {
      setSelectedType(typeParam);
    }
  }, [searchParams]);

  useEffect(() => {
    filterProducts();
  }, [products, searchTerm, selectedType, sortBy]);

  const loadProducts = async () => {
    try {
      setLoading(true);
      const data = await apiService.getProducts();
      setProducts(data);
    } catch (error) {
      console.error('Error loading products:', error);
      toast.error('Failed to load products');
    } finally {
      setLoading(false);
    }
  };

  const loadProductTypes = async () => {
    try {
      const types = await apiService.getProductTypes();
      setProductTypes(types);
    } catch (error) {
      console.error('Error loading product types:', error);
    }
  };

  const filterProducts = () => {
    let filtered = [...products];

    // Filter by search term
    if (searchTerm) {
      filtered = filtered.filter(product =>
        product.naziv?.toLowerCase().includes(searchTerm.toLowerCase()) ||
        product.opis?.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }

    // Filter by type
    if (selectedType) {
      filtered = filtered.filter(product => product.vrsta === selectedType);
    }

    // Sort products
    filtered.sort((a, b) => {
      switch (sortBy) {
        case 'price-asc':
          return (a.cijena || 0) - (b.cijena || 0);
        case 'price-desc':
          return (b.cijena || 0) - (a.cijena || 0);
        case 'name':
        default:
          return (a.naziv || '').localeCompare(b.naziv || '');
      }
    });

    setFilteredProducts(filtered);
  };

  const handleAddToCart = async (product) => {
    if (!user) {
      toast.error('Please log in to add items to cart');
      return;
    }

    try {
      // Create cart if user doesn't have one
      let currentCart = cart;
      if (!currentCart) {
        const newCart = await apiService.createCart({
          idKorisnika: user.idKorisnika,
          status: 'aktivna'
        });
        currentCart = newCart;
        updateCart(currentCart);
      }

      // Check if product already in cart
      const existingItem = currentCart.stavkeKosarice?.find(
        item => item.idProizvoda === product.idProizvoda
      );

      if (existingItem) {
        // Update quantity
        await apiService.updateCartItem(existingItem.idStavke, {
          ...existingItem,
          kolicina: existingItem.kolicina + 1
        });
      } else {
        // Add new item
        await apiService.addCartItem({
          idKosarice: currentCart.idKosarice,
          idProizvoda: product.idProizvoda,
          kolicina: 1,
          cijenaKom: product.cijena
        });
      }

      // Reload cart
      const updatedCart = await apiService.getCart(currentCart.idKosarice);
      updateCart(updatedCart);
      
      toast.success(`${product.naziv} added to cart!`);
    } catch (error) {
      console.error('Error adding to cart:', error);
      toast.error('Failed to add item to cart');
    }
  };

  const handleEditProduct = (product) => {
    setSelectedProduct(product);
    setShowEditModal(true);
  };

  const handleProductUpdated = async (updatedProduct) => {
    await apiService.updateProduct(updatedProduct.idProizvoda, updatedProduct);
    
    // Update the product in the local state
    setProducts(prevProducts => 
      prevProducts.map(p => 
        p.idProizvoda === updatedProduct.idProizvoda ? updatedProduct : p
      )
    );
  };

  const handleCloseEditModal = () => {
    setShowEditModal(false);
    setSelectedProduct(null);
  };

  if (loading) {
    return (
      <div style={{ paddingTop: '100px' }}>
        <LoadingSpinner message="Loading products..." />
      </div>
    );
  }

  return (
    <div style={{ paddingTop: '100px' }}>
      <Container>
        <Row>
          <Col lg={3}>
            {/* Filters Sidebar */}
            <div className="filter-sidebar">
              <h5 className="filter-title">Filters</h5>
              
              {/* Search */}
              <Form.Group className="mb-3">
                <Form.Label>Search</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Search products..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                />
              </Form.Group>

              {/* Product Type */}
              <Form.Group className="mb-3">
                <Form.Label>Category</Form.Label>
                <Form.Select
                  value={selectedType}
                  onChange={(e) => setSelectedType(e.target.value)}
                >
                  <option value="">All Categories</option>
                  {productTypes.map(type => (
                    <option key={type} value={type}>
                      {type.charAt(0).toUpperCase() + type.slice(1)}
                    </option>
                  ))}
                </Form.Select>
              </Form.Group>

              {/* Sort */}
              <Form.Group className="mb-3">
                <Form.Label>Sort By</Form.Label>
                <Form.Select
                  value={sortBy}
                  onChange={(e) => setSortBy(e.target.value)}
                >
                  <option value="name">Name</option>
                  <option value="price-asc">Price: Low to High</option>
                  <option value="price-desc">Price: High to Low</option>
                </Form.Select>
              </Form.Group>

              {/* Clear Filters */}
              <Button 
                variant="outline-secondary" 
                className="w-100"
                onClick={() => {
                  setSearchTerm('');
                  setSelectedType('');
                  setSortBy('name');
                }}
              >
                Clear Filters
              </Button>
            </div>
          </Col>

          <Col lg={9}>
            {/* Results Header */}
            <div className="d-flex justify-content-between align-items-center mb-4">
              <h2>
                Products 
                {selectedType && ` - ${selectedType.charAt(0).toUpperCase() + selectedType.slice(1)}`}
              </h2>
              <span className="text-muted">
                {filteredProducts.length} product{filteredProducts.length !== 1 ? 's' : ''} found
              </span>
            </div>

            {/* Products Grid */}
            {filteredProducts.length === 0 ? (
              <div className="text-center py-5">
                <h4>No products found</h4>
                <p className="text-muted">Try adjusting your filters</p>
              </div>
            ) : (
              <Row>
                {filteredProducts.map((product) => (
                  <Col key={product.idProizvoda} lg={4} md={6} className="mb-4">
                    <ProductCard 
                      product={product}
                      onAddToCart={handleAddToCart}
                      onEditProduct={handleEditProduct}
                      cart={cart}
                      user={user}
                    />
                  </Col>
                ))}
              </Row>
            )}
          </Col>
        </Row>
      </Container>

      {/* Edit Product Modal */}
      <ProductEditModal
        show={showEditModal}
        onHide={handleCloseEditModal}
        product={selectedProduct}
        onProductUpdated={handleProductUpdated}
      />
    </div>
  );
}

export default ProductsPage;

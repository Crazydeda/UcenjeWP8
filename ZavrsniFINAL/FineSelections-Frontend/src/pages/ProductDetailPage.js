import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Row, Col, Card, Button, Badge } from 'react-bootstrap';
import { toast } from 'react-toastify';
import LoadingSpinner from '../components/LoadingSpinner';
import ProductEditModal from '../components/ProductEditModal';
import apiService from '../services/apiService';

function ProductDetailPage({ cart, updateCart, user }) {
  const { id } = useParams();
  const navigate = useNavigate();
  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);
  const [imageError, setImageError] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);

  useEffect(() => {
    loadProduct();
  }, [id]);

  const loadProduct = async () => {
    try {
      setLoading(true);
      const productData = await apiService.getProduct(id);
      setProduct(productData);
    } catch (error) {
      console.error('Error loading product:', error);
      toast.error('Failed to load product');
      navigate('/products');
    } finally {
      setLoading(false);
    }
  };

  const formatPrice = (price) => {
    return new Intl.NumberFormat('hr-HR', {
      style: 'currency',
      currency: 'EUR',
      minimumFractionDigits: 2,
    }).format(price);
  };

  const getTypeColor = (type) => {
    const colors = {
      viski: 'warning',
      rum: 'info',
      gin: 'success',
      konjak: 'primary'
    };
    return colors[type] || 'secondary';
  };

  const handleAddToCart = async () => {
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

  const handleEditProduct = () => {
    setShowEditModal(true);
  };

  const handleProductUpdated = async (updatedProduct) => {
    await apiService.updateProduct(updatedProduct.idProizvoda, updatedProduct);
    setProduct(updatedProduct);
  };

  const handleCloseEditModal = () => {
    setShowEditModal(false);
  };

  if (loading) {
    return (
      <div style={{ paddingTop: '100px' }}>
        <LoadingSpinner message="Loading product..." />
      </div>
    );
  }

  if (!product) {
    return (
      <div style={{ paddingTop: '100px' }}>
        <Container>
          <Row>
            <Col className="text-center">
              <h2>Product not found</h2>
              <Button onClick={() => navigate('/products')} variant="primary">
                Back to Products
              </Button>
            </Col>
          </Row>
        </Container>
      </div>
    );
  }

  return (
    <div style={{ paddingTop: '100px' }}>
      <Container>
        <Row className="mb-3">
          <Col>
            <Button 
              variant="outline-secondary" 
              onClick={() => navigate('/products')}
            >
              ← Back to Products
            </Button>
          </Col>
        </Row>

        <Row>
          <Col lg={6}>
            {/* Product Image */}
            <Card>
              <Card.Body className="text-center">
                {!imageError ? (
                  <img
                    src={product.slika}
                    alt={product.naziv}
                    className="img-fluid"
                    style={{ maxHeight: '500px', objectFit: 'contain' }}
                    onError={() => setImageError(true)}
                  />
                ) : (
                  <div 
                    className="d-flex align-items-center justify-content-center bg-light"
                    style={{ height: '500px' }}
                  >
                    <div className="text-center">
                      <span style={{ fontSize: '4rem' }}>🍾</span>
                      <p className="text-muted mt-2">Image not available</p>
                    </div>
                  </div>
                )}
              </Card.Body>
            </Card>
          </Col>

          <Col lg={6}>
            {/* Product Details */}
            <div>
              <div className="mb-3">
                {product.vrsta && (
                  <Badge bg={getTypeColor(product.vrsta)} className="mb-2">
                    {product.vrsta.charAt(0).toUpperCase() + product.vrsta.slice(1)}
                  </Badge>
                )}
                <h1>{product.naziv}</h1>
              </div>

              <div className="mb-4">
                <h2 className="product-price">{formatPrice(product.cijena)}</h2>
              </div>

              <div className="mb-4">
                <h5>Description</h5>
                <p>{product.opis}</p>
              </div>

              <div className="mb-4">
                <h6>Availability</h6>
                <p>
                  {product.zaliha > 0 ? (
                    <span className="text-success">
                      In Stock ({product.zaliha} available)
                    </span>
                  ) : (
                    <span className="text-danger">Out of Stock</span>
                  )}
                </p>
              </div>

              <div className="mb-4">
                <Button 
                  variant="primary" 
                  size="lg"
                  onClick={handleAddToCart}
                  disabled={!product.zaliha || product.zaliha === 0}
                  className="me-3"
                >
                  {product.zaliha > 0 ? 'Add to Cart' : 'Out of Stock'}
                </Button>
                
                {user && (
                  <Button 
                    variant="outline-secondary" 
                    size="lg"
                    onClick={handleEditProduct}
                    className="me-3"
                  >
                    Edit Product
                  </Button>
                )}
                
                <Button 
                  variant="outline-primary" 
                  size="lg"
                  onClick={() => navigate('/products')}
                >
                  Continue Shopping
                </Button>
              </div>

              {/* Product Details */}
              <Card>
                <Card.Header>
                  <h6>Product Information</h6>
                </Card.Header>
                <Card.Body>
                  <Row>
                    <Col sm={4}><strong>Category:</strong></Col>
                    <Col sm={8}>{product.vrsta}</Col>
                  </Row>
                  <Row>
                    <Col sm={4}><strong>Product ID:</strong></Col>
                    <Col sm={8}>{product.idProizvoda}</Col>
                  </Row>
                  <Row>
                    <Col sm={4}><strong>Stock:</strong></Col>
                    <Col sm={8}>{product.zaliha} units</Col>
                  </Row>
                </Card.Body>
              </Card>
            </div>
          </Col>
        </Row>
      </Container>

      {/* Edit Product Modal */}
      <ProductEditModal
        show={showEditModal}
        onHide={handleCloseEditModal}
        product={product}
        onProductUpdated={handleProductUpdated}
      />
    </div>
  );
}

export default ProductDetailPage;

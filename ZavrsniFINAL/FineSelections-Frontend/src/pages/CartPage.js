import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Card, Button, Form } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import LoadingSpinner from '../components/LoadingSpinner';
import apiService from '../services/apiService';

function CartPage({ cart, updateCart, user }) {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    if (user && !cart) {
      loadUserCart();
    }
  }, [user, cart]);

  const loadUserCart = async () => {
    if (!user) return;
    
    try {
      setLoading(true);
      const userCarts = await apiService.getCartsByUser(user.idKorisnika);
      const activeCart = userCarts.find(c => c.status === 'aktivna');
      if (activeCart) {
        const cartWithItems = await apiService.getCart(activeCart.idKosarice);
        updateCart(cartWithItems);
      }
    } catch (error) {
      console.error('Error loading cart:', error);
    } finally {
      setLoading(false);
    }
  };

  const updateItemQuantity = async (item, newQuantity) => {
    if (newQuantity < 1) {
      removeItem(item);
      return;
    }

    try {
      await apiService.updateCartItem(item.idStavke, {
        ...item,
        kolicina: newQuantity
      });

      // Reload cart
      const updatedCart = await apiService.getCart(cart.idKosarice);
      updateCart(updatedCart);
      
    } catch (error) {
      console.error('Error updating quantity:', error);
      toast.error('Failed to update quantity');
    }
  };

  const removeItem = async (item) => {
    try {
      await apiService.deleteCartItem(item.idStavke);
      
      // Reload cart
      const updatedCart = await apiService.getCart(cart.idKosarice);
      updateCart(updatedCart);
      
      toast.success('Item removed from cart');
    } catch (error) {
      console.error('Error removing item:', error);
      toast.error('Failed to remove item');
    }
  };

  const formatPrice = (price) => {
    return new Intl.NumberFormat('hr-HR', {
      style: 'currency',
      currency: 'EUR',
      minimumFractionDigits: 2,
    }).format(price);
  };

  const calculateTotal = () => {
    if (!cart || !cart.stavkeKosarice) return 0;
    return cart.stavkeKosarice.reduce(
      (total, item) => total + (item.cijenaKom * item.kolicina), 
      0
    );
  };

  const handleCheckout = () => {
    if (!user) {
      toast.error('Please log in to proceed with checkout');
      return;
    }
    
    if (!cart || !cart.stavkeKosarice || cart.stavkeKosarice.length === 0) {
      toast.error('Your cart is empty');
      return;
    }
    
    navigate('/checkout');
  };

  if (!user) {
    return (
      <div style={{ paddingTop: '100px' }}>
        <Container>
          <Row>
            <Col lg={8} className="mx-auto text-center">
              <h2>Please Log In</h2>
              <p>You need to log in to view your cart.</p>
              <Button as={Link} to="/login" variant="primary">
                Log In
              </Button>
            </Col>
          </Row>
        </Container>
      </div>
    );
  }

  if (loading) {
    return (
      <div style={{ paddingTop: '100px' }}>
        <LoadingSpinner message="Loading your cart..." />
      </div>
    );
  }

  return (
    <div style={{ paddingTop: '100px' }}>
      <Container>
        <Row>
          <Col>
            <h2>Shopping Cart</h2>
          </Col>
        </Row>

        {!cart || !cart.stavkeKosarice || cart.stavkeKosarice.length === 0 ? (
          <Row className="mt-4">
            <Col lg={8} className="mx-auto text-center">
              <h4>Your cart is empty</h4>
              <p className="text-muted">Start shopping to add items to your cart.</p>
              <Button as={Link} to="/products" variant="primary">
                Continue Shopping
              </Button>
            </Col>
          </Row>
        ) : (
          <Row className="mt-4">
            <Col lg={8}>
              {/* Cart Items */}
              <Card>
                <Card.Body>
                  {cart.stavkeKosarice.map((item, index) => (
                    <div key={item.idStavke} className="cart-item">
                      <Row className="align-items-center">
                        <Col md={2}>
                          <img
                            src={item.proizvod?.slika || '/placeholder-product.png'}
                            alt={item.proizvod?.naziv}
                            className="img-fluid"
                            style={{ maxHeight: '80px', objectFit: 'contain' }}
                            onError={(e) => {
                              e.target.src = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iODAiIGhlaWdodD0iODAiIHZpZXdCb3g9IjAgMCA4MCA4MCIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KPHJlY3Qgd2lkdGg9IjgwIiBoZWlnaHQ9IjgwIiBmaWxsPSIjRjhGOUZBIi8+CjxwYXRoIGQ9Ik00MCA0NUM0My4zMTM3IDQ1IDQ2IDQyLjMxMzcgNDYgMzlDNDYgMzUuNjg2MyA0My4zMTM3IDMzIDQwIDMzQzM2LjY4NjMgMzMgMzQgMzUuNjg2MyAzNCAzOUMzNCA0Mi4zMTM3IDM2LjY4NjMgNDUgNDAgNDVaIiBmaWxsPSIjNkM3NTdEIi8+Cjwvc3ZnPgo=';
                            }}
                          />
                        </Col>
                        <Col md={4}>
                          <h6>{item.proizvod?.naziv}</h6>
                          <small className="text-muted">{item.proizvod?.vrsta}</small>
                        </Col>
                        <Col md={2}>
                          <span>{formatPrice(item.cijenaKom)}</span>
                        </Col>
                        <Col md={2}>
                          <div className="quantity-controls">
                            <button
                              className="quantity-btn"
                              onClick={() => updateItemQuantity(item, item.kolicina - 1)}
                            >
                              -
                            </button>
                            <Form.Control
                              type="number"
                              min="1"
                              value={item.kolicina}
                              onChange={(e) => updateItemQuantity(item, parseInt(e.target.value) || 1)}
                              className="quantity-input"
                            />
                            <button
                              className="quantity-btn"
                              onClick={() => updateItemQuantity(item, item.kolicina + 1)}
                            >
                              +
                            </button>
                          </div>
                        </Col>
                        <Col md={1}>
                          <strong>{formatPrice(item.cijenaKom * item.kolicina)}</strong>
                        </Col>
                        <Col md={1}>
                          <Button
                            variant="outline-danger"
                            size="sm"
                            onClick={() => removeItem(item)}
                          >
                            ×
                          </Button>
                        </Col>
                      </Row>
                    </div>
                  ))}
                </Card.Body>
              </Card>

              <div className="mt-3">
                <Button as={Link} to="/products" variant="outline-primary">
                  Continue Shopping
                </Button>
              </div>
            </Col>

            <Col lg={4}>
              {/* Cart Summary */}
              <div className="cart-summary">
                <h5>Order Summary</h5>
                <hr />
                
                <div className="d-flex justify-content-between mb-2">
                  <span>Subtotal:</span>
                  <span>{formatPrice(calculateTotal())}</span>
                </div>
                
                <div className="d-flex justify-content-between mb-2">
                  <span>Shipping:</span>
                  <span>Free</span>
                </div>
                
                <hr />
                
                <div className="d-flex justify-content-between mb-3">
                  <strong>Total:</strong>
                  <strong>{formatPrice(calculateTotal())}</strong>
                </div>
                
                <Button 
                  variant="primary" 
                  size="lg" 
                  className="w-100"
                  onClick={handleCheckout}
                >
                  Proceed to Checkout
                </Button>
              </div>
            </Col>
          </Row>
        )}
      </Container>
    </div>
  );
}

export default CartPage;

import React, { useState } from 'react';
import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import apiService from '../services/apiService';

function CheckoutPage({ cart, updateCart, user }) {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

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

  const handlePlaceOrder = async () => {
    if (!user) {
      toast.error('Please log in to place an order');
      return;
    }

    if (!cart || !cart.stavkeKosarice || cart.stavkeKosarice.length === 0) {
      toast.error('Your cart is empty');
      return;
    }

    try {
      setLoading(true);

      // Create order
      const orderData = {
        idKorisnika: user.idKorisnika,
        ukupnaCijena: calculateTotal(),
        status: 'u obradi'
      };

      const newOrder = await apiService.createOrder(orderData);

      // Add order items
      for (const cartItem of cart.stavkeKosarice) {
        await apiService.addOrderItem({
          idNarudzbe: newOrder.idNarudzbe,
          idProizvoda: cartItem.idProizvoda,
          kolicina: cartItem.kolicina,
          cijenaKom: cartItem.cijenaKom
        });
      }

      // Update cart status to ordered
      await apiService.updateCart(cart.idKosarice, {
        ...cart,
        status: 'naručena'
      });

      // Clear cart from state
      updateCart(null);

      toast.success('Order placed successfully!');
      navigate('/orders');

    } catch (error) {
      console.error('Error placing order:', error);
      toast.error('Failed to place order. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  if (!user) {
    return (
      <div style={{ paddingTop: '100px' }}>
        <Container>
          <Row>
            <Col lg={8} className="mx-auto text-center">
              <h2>Please Log In</h2>
              <p>You need to log in to proceed with checkout.</p>
              <Button onClick={() => navigate('/login')} variant="primary">
                Log In
              </Button>
            </Col>
          </Row>
        </Container>
      </div>
    );
  }

  if (!cart || !cart.stavkeKosarice || cart.stavkeKosarice.length === 0) {
    return (
      <div style={{ paddingTop: '100px' }}>
        <Container>
          <Row>
            <Col lg={8} className="mx-auto text-center">
              <h2>Your Cart is Empty</h2>
              <p>Add some products to your cart before checking out.</p>
              <Button onClick={() => navigate('/products')} variant="primary">
                Continue Shopping
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
        <Row>
          <Col>
            <h2>Checkout</h2>
          </Col>
        </Row>

        <Row className="mt-4">
          <Col lg={8}>
            {/* Order Summary */}
            <Card className="mb-4">
              <Card.Header>
                <h5>Order Summary</h5>
              </Card.Header>
              <Card.Body>
                {cart.stavkeKosarice.map((item) => (
                  <div key={item.idStavke} className="d-flex justify-content-between align-items-center py-2 border-bottom">
                    <div className="d-flex align-items-center">
                      <img
                        src={item.proizvod?.slika || '/placeholder-product.png'}
                        alt={item.proizvod?.naziv}
                        className="me-3"
                        style={{ width: '50px', height: '50px', objectFit: 'contain' }}
                        onError={(e) => {
                          e.target.src = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNTAiIGhlaWdodD0iNTAiIHZpZXdCb3g9IjAgMCA1MCA1MCIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KPHJlY3Qgd2lkdGg9IjUwIiBoZWlnaHQ9IjUwIiBmaWxsPSIjRjhGOUZBIi8+CjxwYXRoIGQ9Ik0yNSAzMEMyNy43NjE0IDMwIDMwIDI3Ljc2MTQgMzAgMjVDMzAgMjIuMjM4NiAyNy43NjE0IDIwIDI1IDIwQzIyLjIzODYgMjAgMjAgMjIuMjM4NiAyMCAyNUMyMCAyNy43NjE0IDIyLjIzODYgMzAgMjUgMzBaIiBmaWxsPSIjNkM3NTdEIi8+Cjwvc3ZnPgo=';
                        }}
                      />
                      <div>
                        <h6>{item.proizvod?.naziv}</h6>
                        <small className="text-muted">Quantity: {item.kolicina}</small>
                      </div>
                    </div>
                    <div className="text-end">
                      <div>{formatPrice(item.cijenaKom * item.kolicina)}</div>
                      <small className="text-muted">{formatPrice(item.cijenaKom)} each</small>
                    </div>
                  </div>
                ))}
              </Card.Body>
            </Card>

            {/* Customer Information */}
            <Card>
              <Card.Header>
                <h5>Customer Information</h5>
              </Card.Header>
              <Card.Body>
                <Row>
                  <Col md={6}>
                    <p><strong>Name:</strong> {user.ime} {user.prezime}</p>
                    <p><strong>Email:</strong> {user.email}</p>
                  </Col>
                  <Col md={6}>
                    <p><strong>Address:</strong> {user.adresa || 'Not provided'}</p>
                    <p><strong>Phone:</strong> {user.telefon || 'Not provided'}</p>
                  </Col>
                </Row>
              </Card.Body>
            </Card>
          </Col>

          <Col lg={4}>
            {/* Payment Summary */}
            <Card>
              <Card.Header>
                <h5>Payment Summary</h5>
              </Card.Header>
              <Card.Body>
                <div className="d-flex justify-content-between mb-2">
                  <span>Subtotal:</span>
                  <span>{formatPrice(calculateTotal())}</span>
                </div>
                
                <div className="d-flex justify-content-between mb-2">
                  <span>Shipping:</span>
                  <span>Free</span>
                </div>
                
                <div className="d-flex justify-content-between mb-2">
                  <span>Tax:</span>
                  <span>Included</span>
                </div>
                
                <hr />
                
                <div className="d-flex justify-content-between mb-3">
                  <strong>Total:</strong>
                  <strong>{formatPrice(calculateTotal())}</strong>
                </div>

                <div className="mb-3 p-3 bg-light rounded">
                  <small>
                    <strong>Payment Method:</strong><br />
                    Cash on Delivery<br />
                    <em>This is a demo checkout process.</em>
                  </small>
                </div>
                
                <Button 
                  variant="primary" 
                  size="lg" 
                  className="w-100"
                  onClick={handlePlaceOrder}
                  disabled={loading}
                >
                  {loading ? 'Placing Order...' : 'Place Order'}
                </Button>

                <div className="mt-3">
                  <Button 
                    variant="outline-secondary" 
                    className="w-100"
                    onClick={() => navigate('/cart')}
                  >
                    Back to Cart
                  </Button>
                </div>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default CheckoutPage;

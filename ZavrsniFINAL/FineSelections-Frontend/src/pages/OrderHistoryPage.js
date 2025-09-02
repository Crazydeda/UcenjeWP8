import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Card, Table, Badge, Alert } from 'react-bootstrap';
import { toast } from 'react-toastify';
import LoadingSpinner from '../components/LoadingSpinner';
import apiService from '../services/apiService';

function OrderHistoryPage({ user }) {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (user) {
      loadOrders();
    } else {
      setLoading(false);
    }
  }, [user]);

  const loadOrders = async () => {
    try {
      setLoading(true);
      const allOrders = await apiService.getOrders();
      const userOrders = allOrders.filter(order => order.idKorisnika === user.idKorisnika);
      setOrders(userOrders);
    } catch (error) {
      console.error('Error loading orders:', error);
      toast.error('Failed to load order history');
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

  const formatDate = (dateString) => {
    return new Date(dateString).toLocaleDateString('hr-HR', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  };

  const getStatusColor = (status) => {
    const colors = {
      pending: 'warning',
      processing: 'info',
      shipped: 'primary',
      delivered: 'success',
      cancelled: 'danger'
    };
    return colors[status] || 'secondary';
  };

  if (!user) {
    return (
      <div style={{ paddingTop: '100px' }}>
        <Container>
          <Row>
            <Col className="text-center">
              <Alert variant="warning">
                <h4>Login Required</h4>
                <p>Please log in to view your order history.</p>
              </Alert>
            </Col>
          </Row>
        </Container>
      </div>
    );
  }

  if (loading) {
    return (
      <div style={{ paddingTop: '100px' }}>
        <LoadingSpinner message="Loading order history..." />
      </div>
    );
  }

  return (
    <div style={{ paddingTop: '100px' }}>
      <Container>
        <Row>
          <Col>
            <h1 className="mb-4">Order History</h1>
            
            {orders.length === 0 ? (
              <Alert variant="info">
                <h5>No Orders Yet</h5>
                <p>You haven't placed any orders yet. Browse our premium spirits collection to make your first purchase!</p>
              </Alert>
            ) : (
              <div>
                <p className="text-muted mb-4">
                  Total orders: {orders.length}
                </p>
                
                {orders.map(order => (
                  <Card key={order.idNarudzbe} className="mb-4">
                    <Card.Header>
                      <Row>
                        <Col md={6}>
                          <h6 className="mb-1">Order #{order.idNarudzbe}</h6>
                          <small className="text-muted">
                            Placed on {formatDate(order.datumNarudzbe)}
                          </small>
                        </Col>
                        <Col md={3} className="text-md-center">
                          <Badge bg={getStatusColor(order.status || 'pending')}>
                            {(order.status || 'pending').charAt(0).toUpperCase() + 
                             (order.status || 'pending').slice(1)}
                          </Badge>
                        </Col>
                        <Col md={3} className="text-md-end">
                          <strong>{formatPrice(order.ukupnaCijena)}</strong>
                        </Col>
                      </Row>
                    </Card.Header>
                    
                    <Card.Body>
                      {order.stavkeNarudzbe && order.stavkeNarudzbe.length > 0 ? (
                        <Table responsive>
                          <thead>
                            <tr>
                              <th>Product</th>
                              <th>Quantity</th>
                              <th>Unit Price</th>
                              <th>Total</th>
                            </tr>
                          </thead>
                          <tbody>
                            {order.stavkeNarudzbe.map(item => (
                              <tr key={item.idStavke}>
                                <td>
                                  <div>
                                    <strong>{item.proizvod?.naziv || 'Product'}</strong>
                                    {item.proizvod?.vrsta && (
                                      <div>
                                        <small className="text-muted">
                                          {item.proizvod.vrsta.charAt(0).toUpperCase() + 
                                           item.proizvod.vrsta.slice(1)}
                                        </small>
                                      </div>
                                    )}
                                  </div>
                                </td>
                                <td>{item.kolicina}</td>
                                <td>{formatPrice(item.cijenaKom)}</td>
                                <td>
                                  <strong>
                                    {formatPrice(item.kolicina * item.cijenaKom)}
                                  </strong>
                                </td>
                              </tr>
                            ))}
                          </tbody>
                        </Table>
                      ) : (
                        <Alert variant="secondary">
                          Order details not available
                        </Alert>
                      )}
                      
                      {/* Delivery Information */}
                      <Row className="mt-3">
                        <Col md={6}>
                          <h6>Delivery Address</h6>
                          <p className="mb-1">
                            <strong>{user.ime} {user.prezime}</strong>
                          </p>
                          <p className="mb-1">{user.adresa}</p>
                          <p className="mb-1">{user.email}</p>
                          {user.telefon && (
                            <p className="mb-0">{user.telefon}</p>
                          )}
                        </Col>
                        <Col md={6}>
                          <h6>Order Summary</h6>
                          <Row>
                            <Col xs={8}>Subtotal:</Col>
                            <Col xs={4} className="text-end">
                              {formatPrice(order.ukupnaCijena)}
                            </Col>
                          </Row>
                          <Row>
                            <Col xs={8}>Shipping:</Col>
                            <Col xs={4} className="text-end">Free</Col>
                          </Row>
                          <hr />
                          <Row>
                            <Col xs={8}><strong>Total:</strong></Col>
                            <Col xs={4} className="text-end">
                              <strong>{formatPrice(order.ukupnaCijena)}</strong>
                            </Col>
                          </Row>
                        </Col>
                      </Row>
                    </Card.Body>
                  </Card>
                ))}
              </div>
            )}
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default OrderHistoryPage;

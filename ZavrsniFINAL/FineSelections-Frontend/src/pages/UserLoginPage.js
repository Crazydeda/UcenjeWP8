import React, { useState } from 'react';
import { Container, Row, Col, Card, Form, Button } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import apiService from '../services/apiService';

function UserLoginPage({ updateUser }) {
  const [email, setEmail] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!email) {
      toast.error('Please enter your email');
      return;
    }

    try {
      setLoading(true);
      
      // Simple login - find user by email
      const users = await apiService.getUsers();
      const user = users.find(u => u.email?.toLowerCase() === email.toLowerCase());
      
      if (user) {
        updateUser(user);
        toast.success(`Welcome back, ${user.ime}!`);
        navigate('/');
      } else {
        toast.error('User not found. Please check your email or register.');
      }
      
    } catch (error) {
      console.error('Login error:', error);
      toast.error('Login failed. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ paddingTop: '100px' }}>
      <Container>
        <Row>
          <Col lg={4} className="mx-auto">
            <Card>
              <Card.Body>
                <h3 className="text-center mb-4">Sign In</h3>
                
                <Form onSubmit={handleSubmit}>
                  <Form.Group className="mb-3">
                    <Form.Label>Email</Form.Label>
                    <Form.Control
                      type="email"
                      value={email}
                      onChange={(e) => setEmail(e.target.value)}
                      placeholder="Enter your email"
                      required
                    />
                  </Form.Group>

                  <Button 
                    type="submit" 
                    variant="primary" 
                    size="lg" 
                    className="w-100"
                    disabled={loading}
                  >
                    {loading ? 'Signing In...' : 'Sign In'}
                  </Button>
                </Form>

                <div className="text-center mt-3">
                  <p>
                    Don't have an account?{' '}
                    <Link to="/register">Create one here</Link>
                  </p>
                </div>

                <div className="mt-4 p-3 bg-light rounded">
                  <small className="text-muted">
                    <strong>Demo Users:</strong><br />
                    For testing, you can create a new account or use any email that you register with.
                    This is a simplified login system for demonstration purposes.
                  </small>
                </div>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default UserLoginPage;

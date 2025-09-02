import React from 'react';
import { Navbar, Nav, Container, Badge } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';

function Navigation({ cart, user, updateUser }) {
  const navigate = useNavigate();

  const handleLogout = () => {
    updateUser(null);
    navigate('/');
  };

  const getCartItemCount = () => {
    if (!cart || !cart.stavkeKosarice) return 0;
    return cart.stavkeKosarice.reduce((total, item) => total + (item.kolicina || 0), 0);
  };

  return (
    <Navbar expand="lg" className="navbar" fixed="top">
      <Container>
        <Navbar.Brand as={Link} to="/">
          🥃 FineSelections
        </Navbar.Brand>
        
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link as={Link} to="/">Home</Nav.Link>
            <Nav.Link as={Link} to="/products">Products</Nav.Link>
            <Nav.Link as={Link} to="/products?type=viski">Whisky</Nav.Link>
            <Nav.Link as={Link} to="/products?type=rum">Rum</Nav.Link>
            <Nav.Link as={Link} to="/products?type=gin">Gin</Nav.Link>
            <Nav.Link as={Link} to="/products?type=konjak">Cognac</Nav.Link>
          </Nav>
          
          <Nav>
            {user ? (
              <>
                <Nav.Link as={Link} to="/orders">
                  My Orders
                </Nav.Link>
                <Nav.Link onClick={handleLogout}>
                  Logout ({user.ime})
                </Nav.Link>
              </>
            ) : (
              <>
                <Nav.Link as={Link} to="/login">Login</Nav.Link>
                <Nav.Link as={Link} to="/register">Register</Nav.Link>
              </>
            )}
            
            <Nav.Link as={Link} to="/cart" className="position-relative">
              🛒 Cart
              {getCartItemCount() > 0 && (
                <Badge 
                  bg="danger" 
                  className="position-absolute top-0 start-100 translate-middle"
                  style={{ fontSize: '0.7rem' }}
                >
                  {getCartItemCount()}
                </Badge>
              )}
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default Navigation;

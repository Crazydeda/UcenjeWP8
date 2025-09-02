import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';
import ProductCard from '../components/ProductCard';
import LoadingSpinner from '../components/LoadingSpinner';
import apiService from '../services/apiService';

function HomePage() {
  const [featuredProducts, setFeaturedProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadFeaturedProducts();
  }, []);

  const loadFeaturedProducts = async () => {
    try {
      setLoading(true);
      const products = await apiService.getProducts();
      // Get first 8 products as featured
      setFeaturedProducts(products.slice(0, 8));
    } catch (error) {
      console.error('Error loading featured products:', error);
      toast.error('Failed to load featured products');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ paddingTop: '80px' }}>
      {/* Hero Section */}
      <section className="hero-section">
        <Container>
          <Row>
            <Col lg={8} className="mx-auto text-center">
              <h1 className="hero-title">FineSelections</h1>
              <p className="hero-subtitle">
                Discover the world's finest spirits and liquors. 
                From premium whiskies to rare cognacs, elevate your collection with our curated selection.
              </p>
              <Button as={Link} to="/products" variant="outline-light" size="lg" className="mt-3">
                Explore Collection
              </Button>
            </Col>
          </Row>
        </Container>
      </section>

      {/* Featured Products */}
      <Container className="my-5">
        <Row>
          <Col>
            <h2 className="text-center mb-4">Featured Products</h2>
          </Col>
        </Row>

        {loading ? (
          <LoadingSpinner message="Loading featured products..." />
        ) : (
          <Row>
            {featuredProducts.map((product) => (
              <Col key={product.idProizvoda} lg={3} md={4} sm={6} className="mb-4">
                <ProductCard 
                  product={product}
                  onAddToCart={() => toast.info('Please visit the product page to add to cart')}
                />
              </Col>
            ))}
          </Row>
        )}

        <Row className="mt-4">
          <Col className="text-center">
            <Button as={Link} to="/products" variant="primary" size="lg">
              View All Products
            </Button>
          </Col>
        </Row>
      </Container>

      {/* Categories Section */}
      <Container className="my-5">
        <Row>
          <Col>
            <h2 className="text-center mb-4">Shop by Category</h2>
          </Col>
        </Row>
        <Row>
          <Col md={3} sm={6} className="mb-3">
            <Button 
              as={Link} 
              to="/products?type=viski" 
              variant="outline-primary" 
              className="w-100 h-100 d-flex flex-column align-items-center justify-content-center p-4"
              style={{ minHeight: '120px' }}
            >
              <span style={{ fontSize: '2rem' }}>🥃</span>
              <span className="mt-2">Whisky</span>
            </Button>
          </Col>
          <Col md={3} sm={6} className="mb-3">
            <Button 
              as={Link} 
              to="/products?type=rum" 
              variant="outline-primary" 
              className="w-100 h-100 d-flex flex-column align-items-center justify-content-center p-4"
              style={{ minHeight: '120px' }}
            >
              <span style={{ fontSize: '2rem' }}>🍹</span>
              <span className="mt-2">Rum</span>
            </Button>
          </Col>
          <Col md={3} sm={6} className="mb-3">
            <Button 
              as={Link} 
              to="/products?type=gin" 
              variant="outline-primary" 
              className="w-100 h-100 d-flex flex-column align-items-center justify-content-center p-4"
              style={{ minHeight: '120px' }}
            >
              <span style={{ fontSize: '2rem' }}>🍸</span>
              <span className="mt-2">Gin</span>
            </Button>
          </Col>
          <Col md={3} sm={6} className="mb-3">
            <Button 
              as={Link} 
              to="/products?type=konjak" 
              variant="outline-primary" 
              className="w-100 h-100 d-flex flex-column align-items-center justify-content-center p-4"
              style={{ minHeight: '120px' }}
            >
              <span style={{ fontSize: '2rem' }}>🥂</span>
              <span className="mt-2">Cognac</span>
            </Button>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default HomePage;

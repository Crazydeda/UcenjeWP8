import React, { useState } from 'react';
import { Card, Button, Badge, ButtonGroup } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

function ProductCard({ product, onAddToCart, onEditProduct, cart, user }) {
  const [imageError, setImageError] = useState(false);
  const navigate = useNavigate();

  const handleImageError = () => {
    setImageError(true);
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

  const handleAddToCart = (e) => {
    e.stopPropagation();
    
    if (onAddToCart) {
      onAddToCart(product);
    }
  };

  const handleEditProduct = (e) => {
    e.stopPropagation();
    
    if (onEditProduct) {
      onEditProduct(product);
    }
  };

  const handleCardClick = () => {
    if (product?.idProizvoda) {
      navigate(`/products/${product.idProizvoda}`);
    }
  };  

  return (
    <Card className="product-card h-100" onClick={handleCardClick}>
      <div className="position-relative">
        {!imageError ? (
          <Card.Img
            variant="top"
            src={product.slika}
            alt={product.naziv}
            className="product-image"
            onError={handleImageError}
          />
        ) : (
          <div 
            className="product-image d-flex align-items-center justify-content-center bg-light"
            style={{ height: '250px' }}
          >
            <span className="text-muted">🍾 No Image</span>
          </div>
        )}
        
        {product.vrsta && (
          <Badge 
            bg={getTypeColor(product.vrsta)} 
            className="position-absolute top-0 end-0 m-2 product-type"
          >
            {product.vrsta}
          </Badge>
        )}
      </div>

      <Card.Body className="d-flex flex-column">
        <Card.Title className="product-title">
          {product.naziv}
        </Card.Title>
        
        <Card.Text className="text-muted flex-grow-1">
          {product.opis}
        </Card.Text>

        <div className="mt-auto">
          <div className="d-flex justify-content-between align-items-center mb-2">
            <span className="product-price">
              {formatPrice(product.cijena)}
            </span>
            <small className="text-muted">
              Stock: {product.zaliha || 0}
            </small>
          </div>
          
          <Button 
            variant="primary" 
            className="w-100"
            onClick={handleAddToCart}
            disabled={!product.zaliha || product.zaliha === 0}
          >
            {product.zaliha > 0 ? 'Add to Cart' : 'Out of Stock'}
          </Button>
        </div>
      </Card.Body>
    </Card>
  );
}

export default ProductCard;

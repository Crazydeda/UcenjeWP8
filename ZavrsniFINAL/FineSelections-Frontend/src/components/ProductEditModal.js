import React, { useState, useEffect } from 'react';
import { Modal, Button, Form, Row, Col, Alert } from 'react-bootstrap';
import { toast } from 'react-toastify';

function ProductEditModal({ show, onHide, product, onProductUpdated }) {
  const [formData, setFormData] = useState({
    naziv: '',
    vrsta: '',
    opis: '',
    cijena: '',
    zaliha: '',
    slika: ''
  });
  const [loading, setLoading] = useState(false);
  const [errors, setErrors] = useState({});

  const productTypes = ['viski', 'rum', 'gin', 'konjak'];

  useEffect(() => {
    if (product) {
      setFormData({
        naziv: product.naziv || '',
        vrsta: product.vrsta || '',
        opis: product.opis || '',
        cijena: product.cijena || '',
        zaliha: product.zaliha || '',
        slika: product.slika || ''
      });
    }
  }, [product]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
    
    // Clear error when user starts typing
    if (errors[name]) {
      setErrors(prev => ({
        ...prev,
        [name]: ''
      }));
    }
  };

  const validateForm = () => {
    const newErrors = {};

    if (!formData.naziv.trim()) {
      newErrors.naziv = 'Product name is required';
    }

    if (!formData.vrsta) {
      newErrors.vrsta = 'Product type is required';
    }

    if (!formData.opis.trim()) {
      newErrors.opis = 'Description is required';
    }

    if (!formData.cijena || formData.cijena <= 0) {
      newErrors.cijena = 'Valid price is required';
    }

    if (!formData.zaliha || formData.zaliha < 0) {
      newErrors.zaliha = 'Valid stock quantity is required';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!validateForm()) {
      return;
    }

    setLoading(true);
    try {
      const updatedProduct = {
        ...product,
        naziv: formData.naziv.trim(),
        vrsta: formData.vrsta,
        opis: formData.opis.trim(),
        cijena: parseFloat(formData.cijena),
        zaliha: parseInt(formData.zaliha),
        slika: formData.slika.trim() || null
      };

      await onProductUpdated(updatedProduct);
      toast.success('Product updated successfully!');
      onHide();
    } catch (error) {
      console.error('Error updating product:', error);
      toast.error('Failed to update product');
    } finally {
      setLoading(false);
    }
  };

  const handleClose = () => {
    setFormData({
      naziv: '',
      vrsta: '',
      opis: '',
      cijena: '',
      zaliha: '',
      slika: ''
    });
    setErrors({});
    onHide();
  };

  return (
    <Modal show={show} onHide={handleClose} size="lg">
      <Modal.Header closeButton>
        <Modal.Title>Edit Product</Modal.Title>
      </Modal.Header>
      
      <Form onSubmit={handleSubmit}>
        <Modal.Body>
          <Row>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Product Name *</Form.Label>
                <Form.Control
                  type="text"
                  name="naziv"
                  value={formData.naziv}
                  onChange={handleInputChange}
                  isInvalid={!!errors.naziv}
                  placeholder="Enter product name"
                />
                <Form.Control.Feedback type="invalid">
                  {errors.naziv}
                </Form.Control.Feedback>
              </Form.Group>
            </Col>
            
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Product Type *</Form.Label>
                <Form.Select
                  name="vrsta"
                  value={formData.vrsta}
                  onChange={handleInputChange}
                  isInvalid={!!errors.vrsta}
                >
                  <option value="">Select type...</option>
                  {productTypes.map(type => (
                    <option key={type} value={type}>
                      {type.charAt(0).toUpperCase() + type.slice(1)}
                    </option>
                  ))}
                </Form.Select>
                <Form.Control.Feedback type="invalid">
                  {errors.vrsta}
                </Form.Control.Feedback>
              </Form.Group>
            </Col>
          </Row>

          <Form.Group className="mb-3">
            <Form.Label>Description *</Form.Label>
            <Form.Control
              as="textarea"
              rows={3}
              name="opis"
              value={formData.opis}
              onChange={handleInputChange}
              isInvalid={!!errors.opis}
              placeholder="Enter product description"
            />
            <Form.Control.Feedback type="invalid">
              {errors.opis}
            </Form.Control.Feedback>
          </Form.Group>

          <Row>
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Price (EUR) *</Form.Label>
                <Form.Control
                  type="number"
                  step="0.01"
                  min="0"
                  name="cijena"
                  value={formData.cijena}
                  onChange={handleInputChange}
                  isInvalid={!!errors.cijena}
                  placeholder="0.00"
                />
                <Form.Control.Feedback type="invalid">
                  {errors.cijena}
                </Form.Control.Feedback>
              </Form.Group>
            </Col>
            
            <Col md={6}>
              <Form.Group className="mb-3">
                <Form.Label>Stock Quantity *</Form.Label>
                <Form.Control
                  type="number"
                  min="0"
                  name="zaliha"
                  value={formData.zaliha}
                  onChange={handleInputChange}
                  isInvalid={!!errors.zaliha}
                  placeholder="0"
                />
                <Form.Control.Feedback type="invalid">
                  {errors.zaliha}
                </Form.Control.Feedback>
              </Form.Group>
            </Col>
          </Row>

          <Form.Group className="mb-3">
            <Form.Label>Image URL</Form.Label>
            <Form.Control
              type="url"
              name="slika"
              value={formData.slika}
              onChange={handleInputChange}
              placeholder="https://example.com/image.jpg"
            />
            <Form.Text className="text-muted">
              Enter a valid URL for the product image (optional)
            </Form.Text>
          </Form.Group>

          {Object.keys(errors).length > 0 && (
            <Alert variant="danger">
              Please fix the errors above before submitting.
            </Alert>
          )}
        </Modal.Body>

        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose} disabled={loading}>
            Cancel
          </Button>
          <Button variant="primary" type="submit" disabled={loading}>
            {loading ? 'Updating...' : 'Update Product'}
          </Button>
        </Modal.Footer>
      </Form>
    </Modal>
  );
}

export default ProductEditModal;

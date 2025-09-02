import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';

function Footer() {
  return (
    <footer className="footer">
      <Container>
        <Row>
          <Col md={4}>
            <h5 className="footer-title">FineSelections</h5>
            <p>Premium spirits and liquors for the discerning connoisseur.</p>
          </Col>
          <Col md={4}>
            <h5 className="footer-title">Categories</h5>
            <ul className="list-unstyled">
              <li>Whisky</li>
              <li>Rum</li>
              <li>Gin</li>
              <li>Cognac</li>
            </ul>
          </Col>
          <Col md={4}>
            <h5 className="footer-title">Contact</h5>
            <p>
              Email: info@fineselections.com<br />
              Phone: +385 1 234 5678
            </p>
          </Col>
        </Row>
        <hr className="my-4" />
        <Row>
          <Col className="text-center">
            <p>&copy; 2025 FineSelections. All rights reserved.</p>
          </Col>
        </Row>
      </Container>
    </footer>
  );
}

export default Footer;

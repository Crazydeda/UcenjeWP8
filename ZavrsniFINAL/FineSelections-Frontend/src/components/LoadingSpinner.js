import React from 'react';
import { Spinner } from 'react-bootstrap';

function LoadingSpinner({ message = 'Loading...' }) {
  return (
    <div className="loading-spinner">
      <div className="text-center">
        <Spinner animation="border" className="spinner-border-custom" />
        <div className="mt-2">{message}</div>
      </div>
    </div>
  );
}

export default LoadingSpinner;

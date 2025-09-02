import React, { useState, useEffect } from 'react';
import { Routes, Route } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import Navigation from './components/Navigation';
import Footer from './components/Footer';
import HomePage from './pages/HomePage';
import ProductsPage from './pages/ProductsPage';
import ProductDetailPage from './pages/ProductDetailPage';
import CartPage from './pages/CartPage';
import CheckoutPage from './pages/CheckoutPage';
import UserRegistrationPage from './pages/UserRegistrationPage';
import UserLoginPage from './pages/UserLoginPage';
import OrderHistoryPage from './pages/OrderHistoryPage';

function App() {
  const [cart, setCart] = useState(null);
  const [user, setUser] = useState(null);

  useEffect(() => {
    // Load user from localStorage if exists
    const savedUser = localStorage.getItem('fineselections_user');
    if (savedUser) {
      setUser(JSON.parse(savedUser));
    }
  }, []);

  const updateCart = (newCart) => {
    setCart(newCart);
  };

  const updateUser = (newUser) => {
    setUser(newUser);
    if (newUser) {
      localStorage.setItem('fineselections_user', JSON.stringify(newUser));
    } else {
      localStorage.removeItem('fineselections_user');
    }
  };

  return (
    <div className="App">
      <Navigation cart={cart} user={user} updateUser={updateUser} />
      
      <main>
        <Routes>
          <Route 
            path="/" 
            element={<HomePage />} 
          />
          <Route 
            path="/products" 
            element={<ProductsPage cart={cart} updateCart={updateCart} user={user} />} 
          />
          <Route 
            path="/products/:id" 
            element={<ProductDetailPage cart={cart} updateCart={updateCart} user={user} />} 
          />
          <Route 
            path="/cart" 
            element={<CartPage cart={cart} updateCart={updateCart} user={user} />} 
          />
          <Route 
            path="/checkout" 
            element={<CheckoutPage cart={cart} updateCart={updateCart} user={user} />} 
          />
          <Route 
            path="/register" 
            element={<UserRegistrationPage updateUser={updateUser} />} 
          />
          <Route 
            path="/login" 
            element={<UserLoginPage updateUser={updateUser} />} 
          />
          <Route 
            path="/orders" 
            element={<OrderHistoryPage user={user} />} 
          />
        </Routes>
      </main>

      <Footer />
      
      <ToastContainer
        position="top-right"
        autoClose={3000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </div>
  );
}

export default App;

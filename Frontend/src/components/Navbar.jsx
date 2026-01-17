import React from 'react';
import '../assets/css/Navbar.css';
import logo from '../assets/img/logo-hf-lateral.svg';

const Navbar = () => {
    return (
        <nav className="navbar">
            <div className="navbar-logo-container">
                <img src={logo} alt="HoneyFlow Logo" className="navbar-logo" />
            </div>
        </nav>
    );
};

export default Navbar;

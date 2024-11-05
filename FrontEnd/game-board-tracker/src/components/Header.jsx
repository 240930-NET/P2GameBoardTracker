// src/components/Header.jsx
import React from 'react';
import { Link } from 'react-router-dom';
import '../styles/Header.css'; // Import CSS for styling

const Header = () => {
    return (
        <header className="header">
            <div className="logo">Game Board Tracker</div>
            <nav>

                <ul>
                    {/* <div className='group 1'> */}
                        <li>
                            <Link to="/">Home</Link>
                        </li>
                        <li>
                            <Link to="/GameDetails">GameDetails</Link>
                        </li>

                        <li>
                            <Link to="/search">Search</Link>
                        </li>
                    {/* </div> */}
                    <li>
                        <Link to="/profile">Profile</Link>
                    </li>

                    {/* Add more links as needed */}
                </ul>
            </nav>
        </header>
    );
};

export default Header;
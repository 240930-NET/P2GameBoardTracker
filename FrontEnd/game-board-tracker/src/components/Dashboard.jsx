// src/components/Dashboard.jsx

import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const Dashboard = () => {
    const [username, setUsername] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        const user = localStorage.getItem('user');
        if (user) {
            setUsername(JSON.parse(user));
        } else {
            // If no user is found in localStorage, redirect to login
            navigate('/');
        }
    }, [navigate]);

    const handleLogout = () => {
        localStorage.removeItem('user');
        navigate('/');
    };

    return (
        <div className="min-h-screen bg-gray-100 flex flex-col justify-center items-center">
            <div className="bg-white p-8 rounded-lg shadow-md max-w-md w-full">
                <h1 className="text-3xl font-bold text-center text-indigo-600 mb-6">
                    Welcome, {username}!
                </h1>
                <p className="text-gray-600 text-center mb-6">
                    You have successfully logged in to your Game Board Tracker account.
                </p>
                <div className="flex justify-center">
                    <button
                        onClick={handleLogout}
                        className="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                    >
                        Logout
                    </button>
                </div>
            </div>
        </div>
    );
};

export default Dashboard;
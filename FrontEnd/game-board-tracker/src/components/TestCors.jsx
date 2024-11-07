// src/components/TestCORS.jsx
import React, { useEffect } from 'react';

const TestCORS = () => {
    useEffect(() => {
        const testCORS = async () => {
            try {
                const response = await fetch('https://localhost:5014/api/game', {
                    method: 'GET',
                    credentials: 'include', // Include credentials if needed
                });

                if (response.ok) {
                    const data = await response.json();
                    console.log('CORS test successful:', data);
                } else {
                    console.error('CORS test failed:', response.status, response.statusText);
                }
            } catch (error) {
                console.error('Error during CORS test:', error);
            }
        };

        testCORS();
    }, []);

    return <div>Testing CORS...</div>;
};

export default TestCORS;
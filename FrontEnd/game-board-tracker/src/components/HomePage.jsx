import React, { useState } from 'react';
import { FaGamepad } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';
import '../styles/HomePage.css';

export default function HomePage() {
    return <AuthForm />;
}

function AuthForm() {
    const [isLogin, setIsLogin] = useState(true);
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const navigate = useNavigate();

    async function handleSubmit(e) {
        e.preventDefault();
        setError('');
        setIsLoading(true);

        if (!isLogin && password !== confirmPassword) {
            setError("Passwords don't match");
            setIsLoading(false);
            return;
        }

        const endpoint = isLogin ? 'login' : 'register';

        try {
            const response = await fetch(`http://localhost:5013/api/User/${endpoint}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ UserName: username, Password: password }),
                credentials: 'include',
            });

            const data = await response.json();

            if (response.ok) {
                console.log(`${isLogin ? 'Login' : 'Registration'} successful:`, data);
                localStorage.setItem('user', JSON.stringify(data.userName));
                navigate('/dashboard');
            } else {
                setError(data.message || `${isLogin ? 'Login' : 'Registration'} failed`);
            }
        } catch (err) {
            setError('An error occurred. Please try again.');
            console.error(`${isLogin ? 'Login' : 'Registration'} error`, err);
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="Home">
            <div className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8">
                <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                    <center>
                        <FaGamepad size={100} color="#FF00FF" />
                    </center>
                    <h2 className="mt-10 text-center text-2xl font-bold tracking-tight text-yellow-300">
                        {isLogin ? 'Sign in to your account' : 'Create a new account'}
                    </h2>
                </div>

                <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
                    {error && <p className="text-red-500 text-center mb-4">{error}</p>}
                    <form className="space-y-6" onSubmit={handleSubmit}>
                        <div>
                            <label htmlFor="username" className="block text-sm font-medium text-yellow-300">
                                Username
                            </label>
                            <div className="mt-2">
                                <input
                                    id="username"
                                    name="username"
                                    type="text"
                                    required
                                    value={username}
                                    onChange={(e) => setUsername(e.target.value)}
                                    className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm"
                                />
                            </div>
                        </div>

                        <div>
                            <label htmlFor="password" className="block text-sm font-medium text-yellow-300">
                                Password
                            </label>
                            <div className="mt-2">
                                <input
                                    id="password"
                                    name="password"
                                    type="password"
                                    autoComplete="current-password"
                                    required
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm"
                                />
                            </div>
                        </div>

                        {!isLogin && (
                            <div>
                                <label htmlFor="confirmPassword" className="block text-sm font-medium text-yellow-300">
                                    Confirm Password
                                </label>
                                <div className="mt-2">
                                    <input
                                        id="confirmPassword"
                                        name="confirmPassword"
                                        type="password"
                                        required
                                        value={confirmPassword}
                                        onChange={(e) => setConfirmPassword(e.target.value)}
                                        className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm"
                                    />
                                </div>
                            </div>
                        )}

                        <div>
                            <button
                                type="submit"
                                disabled={isLoading}
                                className={`flex w-full justify-center rounded-md px-3 py-1.5 text-sm font-semibold text-white shadow-sm focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600 ${
                                    isLoading ? 'bg-indigo-400 cursor-not-allowed' : 'bg-indigo-600 hover:bg-indigo-500'
                                }`}
                            >
                                {isLoading ? 'Processing...' : (isLogin ? 'Sign in' : 'Register')}
                            </button>
                        </div>
                    </form>

                    <p className="mt-10 text-center text-sm text-gray-500">
                        {isLogin ? "Don't have an account?" : "Already have an account?"}
                        <button onClick={() => setIsLogin(!isLogin)} className="font-semibold text-indigo-600 hover:text-indigo-500 ml-1">
                            {isLogin ? 'Register here' : 'Login here'}
                        </button>
                    </p>
                </div>
            </div>
        </div>
    );
}
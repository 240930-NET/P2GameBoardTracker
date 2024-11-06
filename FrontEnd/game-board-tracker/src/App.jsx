import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './styles/App.css';
import Header from './components/Header';
import HomePage from './components/HomePage';
import Profile from './components/Profile';
import GameDetails from './components/GameDetails';
import SearchFilter from './components/SearchFilter';
import Dashboard from './components/Dashboard'; // Import the Dashboard component

function App() {
  return (
    <>
      <Router>
        <Header />
        <Routes>
          <Route path="/" element={<HomePage />} /> {/*the home page */}
          <Route path='/GameDetails' element={<GameDetails />} />
          <Route path='/search' element={<SearchFilter />} />
          <Route path='/profile' element={<Profile />} />
          <Route path="/dashboard" element={<Dashboard />} /> {/* Add this line for the Dashboard route */}
        </Routes>
      </Router>
    </>
  )
}

export default App
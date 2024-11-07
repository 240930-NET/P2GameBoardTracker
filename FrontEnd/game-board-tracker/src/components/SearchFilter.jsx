import React, { useState } from 'react';
import { FaSearch } from 'react-icons/fa';
import '../styles/SearchFilter.css';  

function SearchBar({ onSearch }) {
  const [searchTerm, setSearchTerm] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    onSearch(searchTerm);
  };

  return (
    <form onSubmit={handleSubmit} className="relative mx-auto w-11/12 md:w-7/12 lg:w-7/12">
      <input
        className="peer h-10 w-full rounded-full border bg-white pl-4 pr-10 text-sm outline-none focus:border-indigo-600"
        type="search"
        name="search"
        placeholder="Search"
        onChange={(e) => setSearchTerm(e.target.value)}
      />
      <button type="submit" className="absolute inset-y-0 right-0 flex items-center pr-3">
        <FaSearch className="text-gray-400 h-5 w-5" />
      </button>
    </form>
  );
}

export default SearchBar;
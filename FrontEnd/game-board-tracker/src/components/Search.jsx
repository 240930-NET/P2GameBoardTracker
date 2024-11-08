// Search.js
import { useState } from 'react';
import SearchBar from './SearchBar';

function Search() {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchResults, setSearchResults] = useState([]);

  const handleSearch = async (term) => {
    setSearchTerm(term);
    try {
      const response = await fetch(`https://localhost:5014/GetGamesByName/${encodeURIComponent(term)}`)
      console.log('response', response);
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const games = await response.json();
      console.log('games', games);
      setSearchResults(games); // Update the state with search results
    } catch (error) {
      console.error('Error fetching games:', error);
    }
  };

  return (
    <div className="container mx-auto px-4">
      <SearchBar onSearch={handleSearch} />
      {/* Display current search term */}
      {searchTerm && <p className="mt-4 text-sm text-gray-600">Current search: {searchTerm}</p>}
      {/* Render search results */}
      <div className="mt-6">
        {searchResults.length > 0 ? (
          <ul className="space-y-2">
            {searchResults.map((game) => (
              <li key={game.id} className="bg-white shadow rounded-lg p-4">
                {game.name}
              </li>
            ))}
          </ul>
        ) : (
          <p className="text-gray-500">No results found.</p>
        )}
      </div>
    </div>
  );
}

export default Search;
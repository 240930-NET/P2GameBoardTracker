// SearchBar.js
import { useState } from "react";
import { FaSearch } from "react-icons/fa";
import PropTypes from 'prop-types';


function SearchBar({ onSearch }) {
  const [searchInput, setSearchInput] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    onSearch(searchInput);
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="relative mx-auto w-11/12 md:w-7/12 lg:w-7/12"
    >
      <input
        className="peer h-10 w-full rounded-full border bg-white pl-4 pr-10 text-sm outline-none focus:border-indigo-600"
        type="search"
        name="search"
        placeholder="Search"
        value={searchInput}
        onChange={(e) => setSearchInput(e.target.value)}
      />
      <button
        type="submit"
        className="absolute inset-y-0 right-0 flex items-center pr-3"
      >
        <FaSearch className="text-gray-400 h-5 w-5" />
      </button>
    </form>
  );
}
SearchBar.propTypes = {
  onSearch: PropTypes.func.isRequired,
};
export default SearchBar;

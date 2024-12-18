import React, { useState, useRef } from "react";

const GameCard = ({ game, addGameToBacklog }) => {
  const [isFlipped, setIsFlipped] = useState(false);
  const [isConfirming, setIsConfirming] = useState(false);
  const buttonRef = useRef();

  // Toggle the flipped state on click
  const handleFlip = () => {
    setIsFlipped(!isFlipped);
  };

  // Handle add button click
  const handleAddClick = (e) => {
    e.stopPropagation(); // Prevents flip when add button is clicked
    addGameToBacklog(game.gameId); // Call the function to add the game
  };

  // Reset confirmation state when mouse leaves the card
  const handleMouseLeave = () => {
    setIsConfirming(false);
  };

  return (
    <div
      className="game-card"
      onClick={handleFlip}
      onMouseLeave={handleMouseLeave}
    >
      <div className={`card-inner ${isFlipped ? "flipped" : ""}`}>
        {/* Front side of the card */}
        <div className="card-front">
          <img
            src={game.imageURL}
            alt={`${game.name} cover`}
            className="game-image"
          />
          <button
            ref={buttonRef}
            className={`delete-button ${isConfirming ? "confirming" : ""}`}
            onClick={handleAddClick} // Call the add function on click
          >
            {isConfirming ? "Add?" : "+"}
          </button>
        </div>

        {/* Back side of the card with scrollable description */}
        <div className="card-back">
          <h3>{game.name}</h3>
          <div className="scrollable-content">
            <p>{game.description}</p>{" "}
            {/* Full description without truncation */}
          </div>
          <p>
            <strong>Rating:</strong> {Math.round(game.rating * 10) / 10}
          </p>
        </div>
      </div>
    </div>
  );
};

export default GameCard;

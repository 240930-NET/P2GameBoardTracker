import React, { useState, useRef } from "react";

const GameCard = ({ game, onDelete }) => {
  const [isFlipped, setIsFlipped] = useState(false);
  const [isConfirming, setIsConfirming] = useState(false);
  const buttonRef = useRef();

  // Toggle the flipped state on click
  const handleFlip = () => {
    setIsFlipped(!isFlipped);
  };

  // Handle delete button click with confirmation step
  const handleDeleteClick = (e) => {
    e.stopPropagation(); // Prevents flip when delete button is clicked
    if (isConfirming) {
      onDelete(game.GameId); // Confirm deletion on second click
      setIsConfirming(false); // Reset to initial state
    } else {
      setIsConfirming(true); // Expand button on first click
    }
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
            src={game.ImageUrl}
            alt={`${game.Name} cover`}
            className="game-image"
          />
          <button
            ref={buttonRef}
            className={`delete-button ${isConfirming ? "confirming" : ""}`}
            onClick={handleDeleteClick}
          >
            {isConfirming ? "Delete?" : "−"}
          </button>
        </div>

        {/* Back side of the card with scrollable description */}
        <div className="card-back">
          <h3>{game.Name}</h3>
          <div className="scrollable-content">
            <p>{game.Description}</p>{" "}
            {/* Full description without truncation */}
          </div>
          <p>
            <strong>Rating:</strong> {game.Rating}
          </p>
        </div>
      </div>
    </div>
  );
};

export default GameCard;

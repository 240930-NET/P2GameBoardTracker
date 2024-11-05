import React, { useState, useEffect, useRef } from "react";

const GameCard = ({ game, onDelete }) => {
  const [isFlipped, setIsFlipped] = useState(false);
  const [isConfirming, setIsConfirming] = useState(false);
  const buttonRef = useRef();

  // Toggle the flipped state on click
  const handleFlip = () => {
    setIsFlipped(!isFlipped);
  };

  // Truncate description to 100 characters
  const truncateDescription = (text, limit) => {
    return text.length > limit ? text.substring(0, limit) + "..." : text;
  };

  return (
    <div className="card-wrapper">
      <div className="game-card" onClick={handleFlip}>
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
              onClick={(e) => {
                e.stopPropagation();
                if (isConfirming) {
                  onDelete(game.GameId);
                  setIsConfirming(false);
                } else {
                  setIsConfirming(true);
                }
              }}
            >
              {isConfirming ? "Delete?" : "−"}
            </button>
          </div>

          {/* Back side of the card with name, truncated description, and rating */}
          <div className="card-back">
            <h3>{game.Name}</h3>
            <p>{truncateDescription(game.Description, 200)}</p>
            <p>
              <strong>Rating:</strong> {game.Rating}
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default GameCard;

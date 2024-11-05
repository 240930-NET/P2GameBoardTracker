import React from 'react';

const GameCard = ({ game }) => (
    <div className="game-card">
        <img src={game.ImageUrl} alt={`${game.Name} cover`} className="game-image" />
        <h3>{game.Name}</h3>
        <p>{game.Description}</p>
        <p><strong>Rating:</strong> {game.Rating}</p>
    </div>
);

export default GameCard;

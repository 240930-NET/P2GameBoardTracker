import React, { useEffect, useState } from 'react';
import GameCard from './GameCard';
import '../styles/GameBoard.css'; // Importing all relevant CSS here

const UserGameBoard = () => {
    const [games, setGames] = useState([]);

    useEffect(() => {
        const fetchGames = async () => {
            try {
                const response = await fetch(`https://localhost:5014/api/backlog/1`);
                const data = await response.json();
                setGames(data);
            } catch (error) {
                console.error('Error fetching games:', error);
            }
        };

        fetchGames();
    }, []);

    return (
        <div className="game-board">
            <h2>Your Game Board</h2>
            <div className="game-grid">
                {games.map(game => (
                    <GameCard key={game.gameId} game={game} />
                ))}
            </div>
        </div>
    );
};

export default UserGameBoard;

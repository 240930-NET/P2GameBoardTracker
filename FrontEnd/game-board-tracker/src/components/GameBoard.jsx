import React, { useEffect, useState } from 'react';
import GameCard from './GameCard';
import '../styles/GameBoard.css'; // Importing all relevant CSS here

const GameBoard = () => {
    const [games, setGames] = useState([]);

    useEffect(() => {
        const fetchGames = async () => {
            try {
                //Note: if this is supposed to return the user's list of games, it should call backlog{userid}
                const response = await fetch('/api/user/games');
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
                    <GameCard key={game.GameId} game={game} />
                ))}
            </div>
        </div>
    );
};

export default GameBoard;

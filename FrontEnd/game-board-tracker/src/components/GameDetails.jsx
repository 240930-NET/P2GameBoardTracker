import React, { useEffect, useState } from 'react';
import GameCard from './GameCard'; // Assuming you have a GameCard component to display each game
import '../styles/GameDetails.css'; // Import your CSS styles

const GameDetails = () => {
    const [games, setGames] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchAllGames = async () => {
            try {
                const response = await fetch('https://localhost:5014/api/game'); // Adjust the URL as needed
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                const data = await response.json();
                setGames(data);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchAllGames();
    }, []);

    const addGameToBacklog = async (gameId) => {
        try {
            // Get today's date in ISO format
            const today = new Date().toISOString();

            const response = await fetch(`https://localhost:5014/api/Backlog`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    userId: 1,
                    gameId: gameId,
                    completed: true,
                    completionDate: today
                }),
            });

            console.log('Response Status:', response.status);

            if (!response.ok) {
                const errorText = await response.text();
                console.error('Error details:', errorText);
                throw new Error(`Failed to add game to backlog: ${errorText}`);
            }

            const result = await response.json();
            console.log('Game added to backlog:', result);
            alert(`Game with has been added to your backlog!`);
        } catch (error) {
            console.error('Error adding game to backlog:', error);
        }
    };

    if (loading) {
        return <div>Loading games...</div>;
    }

    if (error) {
        return <div>Error fetching games: {error}</div>;
    }

    return (
        <div>
            <h1>All Games</h1>
            <div className="game-grid">
                {games.length > 0 ? (
                    games.map(game => (
                        <GameCard key={game.GameId} game={game} addGameToBacklog={addGameToBacklog} />
                    ))
                ) : (
                    <div>No games found.</div>
                )}
            </div>
        </div>
    );
};

export default GameDetails;
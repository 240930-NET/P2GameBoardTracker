# Possible Frontend Directory Structure

```text
game-board-tracker/
├── public/
│   ├── index.html        # Main HTML file
├── src/
│   ├── components/
│   │   ├── GameGrid.jsx  # Component to render the grid of games
│   │   └── GameCard.jsx  # Component for each game card with an image
│   ├── services/
│   │   └── api.js        # API calls to the IGDB API
│   ├── App.js            # Main application component
│   ├── index.js          # Entry point for React
│   └── styles/
│       ├── GameGrid.css  # CSS for the game grid layout
│       └── App.css       # Global styles
└── package.json          # Project dependencies and scripts
```
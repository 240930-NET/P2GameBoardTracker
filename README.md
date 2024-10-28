# Project Proposal - Game Board Tracker

## Overview
**Game Board Tracker** is a full-stack web application that serves as an interactive platform where users can track the games they’ve played and those they haven’t. This project aims to create a unique, engaging way for gamers to log their gaming experiences, explore new titles, and revisit games they might want to try in the future.

## MVP Features
1. **Game Tracking Board**: A visual game board where users can mark each game as "Played" or "Not Played."
2. **User Profiles**: A profile section that shows each user’s tracked games.
3. **Game Details Page**: Provides details for each game, such as a description, genre, release date, and cover art.
4. **Search and Filter**: Users can search by game title and filter games by categories, such as genre or status (Played/Not Played).
5. **External API Integration**: Connects to a game data API (e.g., IGDB or BoardGameGeek) to pull in detailed information about each game.

## Stretch Ideas
1. **Personalized Recommendations**: Based on games marked as "Played," suggest similar titles the user may enjoy.
2. **User Reviews and Ratings**: Allow users to add reviews or ratings for games, creating a shared experience and helping others decide what to play.
3. **Social Sharing Features**: Enable users to share their game board with friends on social media.
4. **Popular Games Leaderboard**: Show a leaderboard of the most-played games among users to highlight trending or popular titles.
5. **Achievements and Badges**: Introduce badges for completing certain milestones, like tracking a specific number of games or playing across multiple genres.

## ERD (Entity-Relationship Diagram)
The main entities will include:
- **User**: Stores user data such as username and password.
- **Game**: Holds game details, including title, genre, and release date.
- **UserGameStatus**: Tracks whether a user has marked each game as “Played” or “Not Played.”
- **Review** (stretch idea): Allows users to add comments or ratings to games.
<span style="color:orange"><a href="https://miro.com/welcomeonboard/RXFlYkVJQWVlTnZ5a2NhMUU3NWR3N0xkU05ONlpnM0NtY291S2dOb3dqY0ZUUjlBOHBTQW5BNzVRVFJCN25aaXwzNDU4NzY0NTE2MzY1MDY3NTMyfDI=?share_link_id=840662358072">Interactive link of our Erd</a></span>

## External API
We will use an external API for game data:
- **IGDB (Internet Game Database)**: This API offers comprehensive information on video games, including details, images, and genre categorization.
- **Alternative**: **BoardGameGeek API** if we decide to add support for board games, which would provide data for tabletop games.

## Technology Stack
- **Backend**:
  - **C#** and **ASP.NET Core** as the backend framework.
  - **Entity Framework (EF) Core** as the ORM.
  - **SQL Server** on **Azure** for data storage.
  - **xUnit/Moq** for backend testing.
  - **Azure App Service** for deployment.
- **Frontend**:
  - **React** for the interactive user interface.
  - **CSS Framework**: Tailwind or Bootstrap for styling flexibility.

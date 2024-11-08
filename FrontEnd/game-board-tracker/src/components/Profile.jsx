import React from "react"
import "../styles/Profile.css"
import UserGameBoard from "./UserGameBoard"

export default function Profile() {
    return (
        <div className="Profile">
            <center>
                <h1>This page  is the Profile!</h1>
                <UserGameBoard/>
            </center>
        </div>


    )
}
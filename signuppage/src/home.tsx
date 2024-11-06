"use client";

import React, { useEffect, useState } from 'react';
import { apiGetRequest } from 'src/GetLogin/routeGetLogin';
import { apiPostRequest } from 'src/SignUpFunctions/signupfunctions';

const Home = () => {
    const [user, setUser] = useState([]);
    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [userId, setUserId] = useState(''); // New state for User ID
    const [password, setPassword] = useState(''); // New state for Password

    useEffect(() => {
        // Use an IIFE to handle async logic
        (async () => {
            try {
                const userData = await apiGetRequest('/GetSignUp');
                setUser(userData || []);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        })();
    }, []);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        console.log('Form submitted');

        const newAccount = {
            Id: parseInt(userId),  // Parse to integer if necessary
            Email: email,
            Username: username,
            password: password,
        };

        const result = await apiPostRequest('/SignUpFunction', newAccount);
        console.log(result);
        
    };

    return (
        <div className="bodyuser">
          <div className="container">
              <h2>Sign up</h2>
              <form onSubmit={handleSubmit}>
                  <label className="label">User ID:</label>
                  <input
                      className="input"
                      type="number"
                      placeholder="User ID"
                      value={userId}
                      onChange={(e) => setUserId(e.target.value)}
                  />
                  
                  <label className="label">Email:</label>
                  <input
                      className="input"
                      type="text"
                      placeholder="email"
                      value={email}
                      onChange={(e) => setEmail(e.target.value)}
                  />
                  
                  <label className="label">Username:</label>
                  <input
                      className="input"
                      type="text"
                      placeholder="Username "
                      value={username}
                      onChange={(e) => setUsername(e.target.value)}
                  />

                  <label className="label">Password :</label>
                  <input
                      className="input"
                      type="text"
                      placeholder="Password "
                      value={password}
                      onChange={(e) => setPassword(e.target.value)}
                  />
                  <button type="submit" className="buttonAddLoan">Sign Up</button>
              </form>
          </div>
    
        
        <div className="body">
            <div className="title">
                <h1>Welcome to Loan Tracker</h1>
            </div>
            <div className="tableTitle">
                <h4>Login</h4>
            </div>
            <div className="loanTableContainer">
                <table className="loanTable">
                    <thead>
                    <tr>
                        <th>ID</th>
                        <th>Email</th>
                        <th>Username</th>
                        <th>Password</th>
                    </tr>
                    </thead>
                    <tbody>
                    {user.length > 0 ? (
                        user.map(({ Id, email, Username, Password }) => (
                            <tr key={Id}>
                                <td>{Id}</td>
                                <td>{email}</td>
                                <td>{Username}</td>
                                <td>{Password}</td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            
                        </tr>
                    )}
                    </tbody>
                </table>
            </div>
            
        </div>
    </div>
    );
};

export default Home;
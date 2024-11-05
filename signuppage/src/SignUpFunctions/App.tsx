"use client";

import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { apiPostRequest } from 'src/SignUpFunctions/signupfunctions';

const addUser = () => {
    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [userId, setUserId] = useState(''); // New state for User ID
    const [password, setPassword] = useState(''); // New state for Password

    const router = useRouter();

        const handleSubmit = async (e:any) => {
            e.preventDefault();

        const newAccount = {
            Id: parseInt(userId),  // Parse to integer if necessary
            username: username,
            password: password,
        };

        const result = await apiPostRequest('/SignUpFunction', newAccount);

        if (result) {
            router.push('/');
        }
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

                  <button type="submit" className="buttonAddLoan">ADD</button>
              </form>
          </div>
      </div>
  );
};

export default addUser;
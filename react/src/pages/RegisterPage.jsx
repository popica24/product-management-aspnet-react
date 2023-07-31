import React from "react";
import { useState } from "react";
import userService from "../services/userService";

function RegisterPage() {
  const [userName, setUserName] = useState("");

  const [email, setEmail] = useState("");

  const [password, setPassword] = useState("");

  const [isSuccessful, setIsSuccessful] = useState(false);

  const handleRegister = (e) => {
    e.preventDefault();
    setIsSuccessful(false);
    userService.register(email, userName, password).then(
      (response) => {
        setIsSuccessful(true);
      },
      (error) => {
        console.log("failed with " + error);
        setIsSuccessful(false);
      }
    );
  };

  return (
    <div className="d-flex flex-column align-items-center justify-content-center">
      <h1 className="headline border-bottom my-3">Hello There</h1>
      <form onSubmit={handleRegister}>
        {!isSuccessful && (
          <>
            <div className="mb-3 row">
              <label className="col-sm-2 col-form-label">Email</label>
              <input
                type="text"
                className="form-control"
                id="staticEmail"
                placeholder="email@example.com"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
              />
            </div>
            <div className="mb-3 row">
              <label className="col-sm-2 col-form-label">Username</label>
              <input
                type="text"
                className="form-control"
                value={userName}
                onChange={(e) => setUserName(e.target.value)}
                required
              />
            </div>
            <div className="mb-3 row">
              <label for="inputPassword" className="col-sm-2 col-form-label">
                Password
              </label>
              <input
                type="password"
                className="form-control"
                id="inputPassword"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
            </div>
            <button className="btn btn-primary w-100">Create Account</button>
          </>
        )}
        {isSuccessful && <>Register successful</>}
      </form>
    </div>
  );
}

export default RegisterPage;

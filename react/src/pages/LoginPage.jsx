import React, { useState } from "react";
import userService from "../services/userService";
import { Link, useNavigate } from "react-router-dom";

function LoginPage() {
  let navigate = useNavigate();

  const [email, setEmail] = useState("");

  const [password, setPassword] = useState("");

  const [loading, setLoading] = useState(false);

  const handleLogin = (e) => {
    e.preventDefault();
    setLoading(true);
    userService.login(email, password).then(
      () => {
        navigate("/");
        window.location.reload();
      },
      (error) => {
        console.log("Failed login with " + error);
      }
    );
    setLoading(false);
  };

  return (
    <>
      <div className="d-flex flex-column align-items-center justify-content-center">
        <h1 className="headline border-bottom my-3">Welcome Back</h1>

        <form onSubmit={handleLogin}>
          <div className="mb-3">
            <label htmlFor="emailBox" className="form-label">
              Email address
            </label>
            <input
              type="email"
              className="form-control"
              id="emailBox"
              placeholder="Enter email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>
          <div className="mb-3">
            <label htmlFor="emailBox" className="form-label">
              Password
            </label>
            <input
              type="password"
              className="form-control"
              id="emailBox"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>
          <button className="btn btn-primary w-100">Log in</button>
        </form>
        <span>
          Don't have an account?{" "}
          <Link as={Link} to="/register">
            Sign Up here
          </Link>
        </span>
      </div>
    </>
  );
}

export default LoginPage;

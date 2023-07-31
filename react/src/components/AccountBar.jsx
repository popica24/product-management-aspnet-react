import React from "react";
import { Link } from "react-router-dom";
import userService from "../services/userService";

function AccountBar({ user, setIsAuthenticated, isAuthenticated }) {
  const logout = () => {
    userService.logout();
    setIsAuthenticated(false);
  };

  return (
    <nav className="navbar bg-body-tertiary">
      <div className="container-fluid">
        {isAuthenticated ? (
          <div className="d-flex flex-row justify-content-between align-items-center w-100">
            <Link to="/profile" className="headline fs-4">
              Hello {user.username}
            </Link>
            <button className="btn btn-danger" onClick={logout}>
              Log Out
            </button>
          </div>
        ) : (
          <div className="d-flex flex-row justify-content-between align-items-center w-100">
            <div className="headline fs-4">Hello Guest!</div>
            <span className="headline fs-5">
              Browse in readonly or&nbsp;
              <Link to="/login">Log In here</Link>
            </span>
          </div>
        )}
      </div>
    </nav>
  );
}

export default AccountBar;

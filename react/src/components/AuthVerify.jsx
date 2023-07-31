import jwtDecode from "jwt-decode";
import userService from "../services/userService";
import React from "react";
import { useLocation } from "react-router-dom";
import { useEffect } from "react";

function AuthVerify({ logout, setIsAuthenticated }) {
  let location = useLocation();

  useEffect(() => {
    const user = userService.getCurrentUser();
    if (user) {
      const decoded = jwtDecode(user.token);
      console.log(" not expired");
      if (decoded.exp * 1000 < Date.now()) {
        console.log("expired");
        setIsAuthenticated(false);
        logout();
      }
    }
  }, [location]);

  return <></>;
}

export default AuthVerify;

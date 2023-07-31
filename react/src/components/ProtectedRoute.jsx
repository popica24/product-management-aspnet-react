import React from "react";
//https://www.robinwieruch.de/react-router-private-routes/
import { Navigate, Outlet, useNavigate } from "react-router-dom";

const ProtectedRoute = ({ user, children }) => {
  let navigate = useNavigate();
  if (!user) {
    navigate("/");
    window.location.reload();
  }
  return children ? children : <Outlet />;
};

export default ProtectedRoute;

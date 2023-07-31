import React from "react";
import { Link } from "react-router-dom";
import { Nav } from "react-bootstrap";

function Sidebar({ user, isAuthenticated }) {
  return (
    <Nav className="col-md-12 d-md-block bg-light sidebar">
      <div className="sidebar-sticky">
        <Nav.Item>
          <Nav.Link as={Link} to="/catalogue">
            Catalogue
          </Nav.Link>
        </Nav.Item>
        {user && isAuthenticated && (
          <>
            {" "}
            <Nav.Item>
              <Nav.Link as={Link} to="/admin/categories">
                Admin Categories
              </Nav.Link>
            </Nav.Item>
            <Nav.Item>
              <Nav.Link as={Link} to="/admin/products">
                Admin Products
              </Nav.Link>
            </Nav.Item>
          </>
        )}
      </div>
    </Nav>
  );
}

export default Sidebar;

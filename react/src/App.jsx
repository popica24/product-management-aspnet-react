import "./App.css";
import { BrowserRouter, Route, Routes, Navigate } from "react-router-dom";
import { Container, Row, Col } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import CategoryList from "./pages/CategoryList";
import ProductList from "./pages/ProductList";
import Sidebar from "./components/Sidebar";
import AdminPage from "./pages/AdminPage";
import CategoryDetails from "./pages/CategoryDetails";
import CategoryCreate from "./pages/CategoryCreate";
import LoginPage from "./pages/LoginPage";
import ProtectedRoute from "./components/ProtectedRoute";
import { useEffect, useState } from "react";
import RegisterPage from "./pages/RegisterPage";
import Catalogue from "./pages/Catalogue";
import AccountBar from "./components/AccountBar";
import userService from "./services/userService";
import Profile from "./pages/Profile";
import AuthVerify from "./components/AuthVerify";
import ProductCreate from "./pages/ProductCreate";
import ProductDetails from "./pages/ProductDetails";
import CategoryUpdate from "./pages/CategoryUpdate";
function App() {
  const [user, setUser] = useState({});
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  useEffect(() => {
    const user = userService.getCurrentUser();

    if (user) {
      setUser(user);
      setIsAuthenticated(true);
    } else {
      setUser(null);
      setIsAuthenticated(false);
    }
  }, []);

  return (
    <BrowserRouter>
      <Container fluid>
        <Row>
          <Col lg={2}>
            <Sidebar user={user} isAuthenticated={isAuthenticated} />
          </Col>
          <Col lg={10} xs={12}>
            <Row>
              <AccountBar
                user={user}
                isAuthenticated={isAuthenticated}
                setIsAuthenticated={setIsAuthenticated}
              />
            </Row>
            <Routes>
              <Route element={<ProtectedRoute user={user} />}>
                <Route
                  path="/admin/categories/:categoryId"
                  element={<CategoryDetails />}
                />
                <Route
                  path="/admin/categories/create"
                  element={<CategoryCreate />}
                />
                <Route
                  path="/admin/categories/:categoryId/update/"
                  element={<CategoryUpdate />}
                />
                <Route path="/admin" element={<AdminPage />} />
                <Route path="/admin/categories" element={<CategoryList />} />
                <Route path="/admin/products" element={<ProductList />} />
                <Route
                  path="/admin/products/create"
                  element={<ProductCreate />}
                />
                <Route
                  path="/admin/products/:productId"
                  element={<ProductDetails />}
                />
                <Route path="/profile" element={<Profile user={user} />} />
              </Route>
              <Route path="/catalogue" element={<Catalogue />} />
              <Route path="/login" element={<LoginPage />} />
              <Route path="/" element={<Catalogue />} />
              <Route path="/register" element={<RegisterPage />} />
            </Routes>
            <AuthVerify
              logout={userService.logout}
              setIsAuthenticated={setIsAuthenticated}
            />
          </Col>
        </Row>
      </Container>
    </BrowserRouter>
  );
}

export default App;

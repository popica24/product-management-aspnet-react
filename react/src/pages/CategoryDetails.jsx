import React, { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import categoryService from "../services/categoryService";
import { Button } from "react-bootstrap";
import ProductPage from "../components/ProductPage";

function CategoryDetails() {
  const { categoryId } = useParams();
  const [category, setCategory] = useState({});
  const [showProducts, setShowProducts] = useState(false);
  let navigate = useNavigate();
  useEffect(() => {
    categoryService
      .GetById(categoryId)
      .then((response) => {
        console.log(response);
        setCategory(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);
  const handleDelete = () => {
    categoryService.Delete(categoryId);
    navigate("/admin/categories");
    window.location.reload();
  };
  const handleSeeProducts = () => {
    setShowProducts(true);
  };
  return (
    <div className="details-wrapper" key={category.categoryId}>
      <div className="row">
        <div className="col-md-12">
          <div className="specs-wrapper d-flex flex-column">
            <span className="spec">Name : {category.name}</span>
            <span className="spec">Description : {category.description}</span>
            <span className="spec">
              Products : {category.productCount}{" "}
              <Button onClick={handleSeeProducts}>See products</Button>
            </span>
          </div>
        </div>
      </div>
      <div className="row">
        <div className="col-md-12">
          <div className="actions-wrapper">
            <span className="action">
              <button
                onClick={() => {
                  handleDelete();
                }}
                className="btn btn-danger"
              >
                Delete
              </button>
              <Link
                as={Link}
                to={`/admin/categories/${categoryId}/update`}
                className="btn btn-warning"
              >
                Update
              </Link>
            </span>
          </div>
        </div>
      </div>
      {showProducts && (
        <ProductPage
          categoryId={categoryId}
          setShowProducts={setShowProducts}
        />
      )}
    </div>
  );
}

export default CategoryDetails;

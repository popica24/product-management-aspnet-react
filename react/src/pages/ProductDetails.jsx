import React from "react";
import { useEffect } from "react";
import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import productService from "../services/productService";

function ProductDetails() {
  const { productId } = useParams();
  const [product, setProduct] = useState({});
  const [categories, setCategories] = useState([]);
  let navigate = useNavigate();

  useEffect(() => {
    productService
      .GetById(productId)
      .then((response) => {
        setProduct(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
    productService
      .LoadCategories(productId)
      .then((response) => {
        setCategories(response.data);
        console.log(categories);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  const handleDelete = () => {
    productService.Delete(productId);
    navigate("/admin/products");
    window.location.reload();
  };
  return (
    <div className="details-wrapper" key={productId}>
      <div className="row">
        <div className="col-md-12">
          <div className="specs-wrapper d-flex flex-column">
            <span className="spec">Name : {product.name}</span>
            <span className="spec">Description : {product.description}</span>
            <span className="spec">Quantity : {product.quantity}</span>
            <span className="spec">
              Categories :
              {categories.length === 0 && <>Not assigned to a category</>}
              {categories.map((c) => {
                return <> {c}</>;
              })}
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
            </span>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ProductDetails;

import React from "react";
import { Link } from "react-router-dom";
import categoryService from "../services/categoryService";

export default function ProductCard({ cardData }) {
  return (
    <div className="card">
      <div className="card-body">
        <h5 className="card-title">{cardData.name}</h5>
        <p className="card-text">{cardData.description}</p>
        <p className="card-text">Quantity : {cardData.quantity}</p>
        <div className="d-flex align-items-center justify-content-between">
          <Link
            to={`/admin/products/${cardData.productId}`}
            className="btn btn-secondary"
          >
            See More
          </Link>
        </div>
      </div>
    </div>
  );
}

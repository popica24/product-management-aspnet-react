import React from "react";
import { Link } from "react-router-dom";

export function CategoryCard({ cardData }) {
  return (
    <div className="card">
      <div className="card-body">
        <h5 className="card-title">{cardData.name}</h5>
        <p className="card-text">{cardData.description}</p>
        <Link to={`/admin/categories/${cardData.categoryId}`}>See More</Link>
      </div>
    </div>
  );
}

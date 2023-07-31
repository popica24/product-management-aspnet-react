import React from "react";
import { Link } from "react-router-dom";

export default function ProductCardAnonymous({ cardData }) {
  return (
    <div className="card">
      <div className="card-body">
        <h5 className="card-title">{cardData.name}</h5>
        <p className="card-text">{cardData.description}</p>
      </div>
    </div>
  );
}

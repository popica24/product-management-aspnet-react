import React from "react";
import { useState } from "react";
import productService from "../services/productService";

function ProductCreate() {
  const [productName, setProductName] = useState("");
  const [productDescription, setproductDescription] = useState("");
  const [quantity, setQuantity] = useState(0);

  const handleSubmit = async (e) => {
    e.preventDefault();
    productService.Post(productName, productDescription, quantity);
  };
  return (
    <>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Product Name</label>
          <input
            type="text"
            className="form-control"
            id="Name"
            placeholder="Smartphone"
            required
            onChange={(e) => setProductName(e.target.value)}
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Product Description</label>
          <textarea
            className="form-control"
            id="Description"
            rows="3"
            required
            onChange={(e) => setproductDescription(e.target.value)}
          ></textarea>
        </div>
        <div className="mb-3">
          <label className="form-label">Quantity</label>
          <input
            className="form-control"
            id="Quantity"
            type="number"
            required
            onChange={(e) => setQuantity(e.target.value)}
          ></input>
        </div>
        <div className="mb-3">
          <button className="btn btn-primary">Create</button>
        </div>
      </form>
    </>
  );
}

export default ProductCreate;

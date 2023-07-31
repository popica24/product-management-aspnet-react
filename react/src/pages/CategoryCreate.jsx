import React, { useState } from "react";
import categoryService from "../services/categoryService";

function CategoryCreate() {
  const [categoryName, setCategoryName] = useState("");
  const [categoryDescription, setCategoryDescription] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    categoryService.Post(categoryName, categoryDescription);
  };

  return (
    <>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Category Title</label>
          <input
            type="text"
            className="form-control"
            id="Name"
            placeholder="Gardening"
            required
            onChange={(e) => setCategoryName(e.target.value)}
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Category Description</label>
          <textarea
            className="form-control"
            id="Description"
            rows="3"
            required
            onChange={(e) => setCategoryDescription(e.target.value)}
          ></textarea>
        </div>
        <div className="mb-3">
          <button className="btn btn-primary">Create</button>
        </div>
      </form>
    </>
  );
}

export default CategoryCreate;

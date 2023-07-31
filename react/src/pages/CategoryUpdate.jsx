import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import categoryService from "../services/categoryService";
import productService from "../services/productService";
import { useRef } from "react";
function CategoryUpdate() {
  const { categoryId } = useParams();
  const [category, setCategory] = useState(null);
  const [categoryName, setCategoryName] = useState("");
  const [categoryDescription, setCategoryDescription] = useState("");
  const [hasMore, setHasMore] = useState(false);
  const [addedProducts, setAddedProducts] = useState([]);
  const limit = 10;
  const offset = useRef(0);
  const [products, setProducts] = useState([]);
  useEffect(() => {
    categoryService.GetById(categoryId).then((response) => {
      setCategory(response.data);
    });
    productService.GetAll(offset.current, limit).then((response) => {
      console.log(response);
      setHasMore(response.data.hasMore);
      setProducts(response.data.data);
    });
  }, []);

  useEffect(() => {
    if (category) {
      setCategoryName(category.name.trim());
      setCategoryDescription(category.description.trim());
    }
  }, [category]);

  const handleSubmit = (e) => {
    e.preventDefault();

    categoryService.Put(
      addedProducts,
      categoryName,
      categoryDescription,
      categoryId
    );
  };
  const fetchNext = () => {
    setProducts([]);
    offset.current += limit;

    productService.GetAll(offset.current, limit).then((response) => {
      console.log(response);
      setProducts(response.data.data);
      setHasMore(response.data.hasMore);
    });
  };
  const fetchPrev = () => {
    setProducts([]);
    offset.current -= limit;

    productService.GetAll(offset.current, limit).then((response) => {
      setProducts(response.data.data);
      setHasMore(response.data.hasMore);
    });
  };
  const handleAddProduct = (productId) => {
    setAddedProducts((prev) => [...prev, productId]);
    console.log(addedProducts);
  };
  return (
    <div>
      {category ? (
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label className="form-label">Category Title</label>
            <input
              type="text"
              className="form-control"
              id="Name"
              placeholder="Gardening"
              value={categoryName}
              onChange={(e) => setCategoryName(e.target.value)}
            />
          </div>
          <div className="mb-3">
            <label className="form-label">Category Description</label>
            <textarea
              className="form-control"
              id="Description"
              rows="3"
              value={categoryDescription}
              onChange={(e) => setCategoryDescription(e.target.value)}
            ></textarea>
          </div>
          <div className="mb-3">
            {products.length !== 0 &&
              products.map((x) => (
                <div className="w-25 py-2">
                  <span>
                    <button
                      type="button"
                      onClick={() => handleAddProduct(x.productId)}
                      className="btn btn-primary mx-2"
                    >
                      Add
                    </button>
                    {x.name}
                  </span>
                </div>
              ))}
            <div className="d-flex flex-row justify-content-center align-items-center">
              {offset.current !== 0 && (
                <button
                  type="button"
                  className="btn btn-primary mx-1"
                  onClick={() => fetchPrev()}
                >
                  Prev
                </button>
              )}
              {hasMore && products.length !== 0 && (
                <button
                  type="button"
                  className="btn btn-primary mx-1"
                  onClick={() => fetchNext()}
                >
                  Next
                </button>
              )}
            </div>
          </div>
          <div className="mb-3 text-center border-top py-3">
            <button type="submit" className="btn btn-primary">
              Update
            </button>
          </div>
        </form>
      ) : (
        <div>Loading...</div>
      )}
    </div>
  );
}

export default CategoryUpdate;

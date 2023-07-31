import React from "react";
import categoryService from "../services/categoryService";
import { useEffect } from "react";
import { useState } from "react";
import { useRef } from "react";
import ProductCard from "./ProductCard";

function ProductPage({ categoryId, setShowProducts }) {
  const [products, setProducts] = useState([]);
  const [hasMore, setHasMore] = useState(false);
  const offset = useRef(0);
  const limit = 12;
  const [isLoadingFetch, setIsLoadingFetch] = useState(false);

  useEffect(() => {
    setIsLoadingFetch(true);
    categoryService
      .LoadProducts(categoryId, offset.current, limit)
      .then((response) => {
        response.data !== ""
          ? (setProducts(response.data.data), setHasMore(response.data.hasMore))
          : setProducts([]);
      });
  }, []);

  const fetchNext = () => {
    setProducts([]);
    offset.current += limit;

    categoryService
      .LoadProducts(categoryId, offset.current, limit)
      .then((response) => {
        setProducts(response.data.data);
        setHasMore(response.data.hasMore);
      });
  };
  const fetchPrev = () => {
    setProducts([]);
    offset.current -= limit;

    categoryService
      .LoadProducts(categoryId, offset.current, limit)
      .then((response) => {
        setProducts(response.data.data);
        setHasMore(response.data.hasMore);
      });
  };
  return (
    <div className="w-100">
      <div className="d-flex flex-row align-items-center justify-content-between">
        <span>Products for category with ID : {categoryId}</span>
        <button
          className="btn btn-danger ms-auto"
          onClick={() => {
            setShowProducts(false);
          }}
        >
          X
        </button>
      </div>
      <div className="row">
        {products.length === 0 && <>No Products</>}
        {products.length !== 0 &&
          products.map((c) => {
            return (
              <div className="w-25 py-2">
                <ProductCard
                  cardData={c}
                  categoryId={categoryId}
                  key={categoryId}
                />
              </div>
            );
          })}
      </div>
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
  );
}

export default ProductPage;

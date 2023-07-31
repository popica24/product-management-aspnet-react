import React, { useRef } from "react";
import { useEffect } from "react";
import { useState } from "react";
import productService from "../services/productService";
import categoryService from "../services/categoryService";
import SearchBar from "../components/SearchBar";
import ProductCardAnonymous from "../components/ProductCardAnonymous";

function Catalogue() {
  const limit = 20;
  const offset = useRef(0);
  const [data, setData] = useState([]);
  const [hasMore, setHasMore] = useState(false);
  const [searchName, setSearchName] = useState("");
  const [areFiltered, setAreFiltered] = useState(false);
  const [categories, setCategories] = useState([]);
  const [categoryfilter, setCategoryFilter] = useState(0);
  useEffect(() => {
    setAreFiltered(false);
    productService.GetAll(offset.current, limit).then((response) => {
      setData(response.data.data);
      setHasMore(response.data.hasMore);
    });
    categoryService.GetAll().then((response) => {
      setCategories(response.data.data);
    });
  }, []);
  useEffect(() => {
    if (searchName === "" && categoryfilter === 0) return;
    offset.current = 0;
    setAreFiltered(true);
    productService
      .Filter(
        searchName,
        0,
        0,
        categoryfilter,
        offset.current,
        limit,
        "Asc",
        "ProductId"
      )
      .then((response) => {
        response.data
          ? (setData(response.data.data), setHasMore(response.data.hasMore))
          : setData([]);
      });
  }, [searchName, categoryfilter]);
  useEffect(() => {
    if (areFiltered) return;
    productService.GetAll(offset.current, limit).then((response) => {
      setData(response.data.data);
      setHasMore(response.data.hasMore);
    });
  }, [areFiltered]);
  const fetchNext = () => {
    setData([]);
    offset.current += limit;
    !areFiltered
      ? productService.GetAll(offset.current, limit).then((response) => {
          setData(response.data.data);
          setHasMore(response.data.hasMore);
        })
      : productService
          .Filter(
            searchName,
            0,
            0,
            categoryfilter,
            offset.current,
            limit,
            "Asc",
            "ProductId"
          )
          .then((response) => {
            setData(response.data.data);
            setHasMore(response.data.hasMore);
          });
  };
  const fetchPrev = () => {
    setData([]);
    offset.current -= limit;
    !areFiltered
      ? productService.GetAll(offset.current, limit).then((response) => {
          setData(response.data.data);
          setHasMore(response.data.hasMore);
        })
      : productService
          .Filter(
            searchName,
            0,
            0,
            categoryfilter,
            offset.current,
            limit,
            "Asc",
            "ProductId"
          )
          .then((response) => {
            setData(response.data.data);
            setHasMore(response.data.hasMore);
          });
    console.log("filtered : " + areFiltered);
  };

  const filteredData = data ? data.filter((x) => x.quantity !== 0) : [];

  const handleCategorySelect = (categoryId) => {
    setCategoryFilter(categoryId);
  };
  return (
    <div className="container">
      <div className="row my-3">
        <SearchBar setSearchName={setSearchName} />
        <div className="dropdown">
          <button
            className="btn btn-secondary dropdown-toggle"
            type="button"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            Category
          </button>
          <ul className="dropdown-menu scrollable-menu">
            {categories.map((c) => {
              return (
                <button
                  className="dropdown-item"
                  onClick={() => handleCategorySelect(c.categoryId)}
                >
                  {c.name}
                </button>
              );
            })}
            <li></li>
          </ul>
        </div>
        {areFiltered && (
          <>
            <button
              onClick={() => {
                setSearchName("");
                setCategoryFilter(0);
                setAreFiltered(false);
              }}
              className="btn btn-danger me-3"
            >
              Delete
            </button>
          </>
        )}
      </div>
      <div className="row">
        {data.length === 0 && "No results for " + searchName}
        {data.length !== 0 &&
          filteredData.map((x) => (
            <div className="w-25 py-2">
              <ProductCardAnonymous key={x.id} cardData={x} />
            </div>
          ))}
      </div>
      <div className="d-flex flex-row justify-content-center align-items-center">
        {offset.current !== 0 && (
          <button className="btn btn-primary mx-1" onClick={() => fetchPrev()}>
            Prev
          </button>
        )}
        {hasMore && data.length !== 0 && (
          <button className="btn btn-primary mx-1" onClick={() => fetchNext()}>
            Next
          </button>
        )}
      </div>
    </div>
  );
}

export default Catalogue;

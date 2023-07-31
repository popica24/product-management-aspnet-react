import React from "react";
import productService from "../services/productService";
import { useRef } from "react";
import { useState } from "react";
import { useEffect } from "react";
import SearchBar from "../components/SearchBar";
import { Link } from "react-router-dom";
import { Nav, Row } from "react-bootstrap";
import ProductCard from "../components/ProductCard";
function ProductList() {
  const [products, setProducts] = useState([]);
  // *Sorting* //
  const orderBy = "Asc";
  const groupBy = "Name";
  const [hasMore, setHasMore] = useState(false);
  const [searchName, setSearchName] = useState("");
  // *End of Sorting* //
  // *Paging* //
  const [records, setRecords] = useState(0);
  const offset = useRef(0);
  const limit = 12;
  // *End of Paging* //
  //*Filtering logic*//
  const [areFiltered, setAreFiltered] = useState(false);
  //*End of Filtering logic*//

  useEffect(() => {
    productService.GetAll(offset.current, limit).then((response) => {
      setAreFiltered(false);
      setRecords(response.data.totalRecords);
      setHasMore(response.data.hasMore);
      setProducts(response.data.data);
    });
  }, []);
  useEffect(() => {
    offset.current = 0;
    setAreFiltered(true);
    productService
      .Filter(searchName, 0, 0, 0, offset.current, limit, "Asc", "CategoryId")
      .then((response) => {
        response.data !== ""
          ? (console.log("called"),
            setProducts(response.data.data),
            setHasMore(response.data.hasMore),
            setRecords(response.data.totalRecords))
          : setProducts([]);
      });
  }, [searchName]);
  useEffect(() => {
    if (areFiltered) return;
    productService.GetAll(offset.current, limit).then((response) => {
      offset.current = 0;
      setProducts(response.data.data);
      setHasMore(response.data.hasMore);
      setRecords(response.data.totalRecords);
    });
  }, [areFiltered]);
  const fetchNext = () => {
    setProducts([]);
    offset.current += limit;
    !areFiltered
      ? productService.GetAll(offset.current, limit).then((response) => {
          setProducts(response.data.data);
          setHasMore(response.data.hasMore);
        })
      : productService
          .Filter(
            searchName,
            0,
            0,
            0,
            offset.current,
            limit,
            "Asc",
            "CategoryId"
          )
          .then((response) => {
            setProducts(response.data.data);
            setHasMore(response.data.hasMore);
          });
  };
  const fetchPrev = () => {
    setProducts([]);
    offset.current -= limit;
    !areFiltered
      ? productService.GetAll(offset.current, limit).then((response) => {
          setProducts(response.data.data);
          setHasMore(response.data.hasMore);
        })
      : productService
          .Filter(
            searchName,
            0,
            0,
            0,
            offset.current,
            limit,
            "Asc",
            "CategoryId"
          )
          .then((response) => {
            setProducts(response.data.data);
            setHasMore(response.data.hasMore);
          });
  };
  return (
    <>
      <div className="container">
        <Row className="my-3">
          <SearchBar setSearchName={setSearchName} />
          <Nav.Item>
            <Link as={Link} to="/admin/products/create" className="d-flex">
              Create New
            </Link>
          </Nav.Item>
        </Row>
        {!areFiltered && (
          <>
            <span>{records} products</span>
          </>
        )}
        {areFiltered && (
          <>
            <button
              onClick={() => {
                setAreFiltered(false), setSearchName("");
              }}
              className="btn btn-danger me-3"
            >
              Delete
            </button>
            <span>
              {records} results for {searchName}
            </span>
          </>
        )}
        <div className="row">
          {" "}
          {products.length === 0 && "No results for " + searchName}
          {products.length !== 0 &&
            products.map((c) => {
              return (
                <div className="w-25 p-3">
                  <ProductCard cardData={c} key={c.id} />
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
    </>
  );
}

export default ProductList;

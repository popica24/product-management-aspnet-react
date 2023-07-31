//Fix pagination and rewrite categorylist
import React, { useEffect, useRef, useState } from "react";
import categoryService from "../services/categoryService";
import { CategoryCard } from "../components/CategoryCard";
import SearchBar from "../components/SearchBar";
import { Nav, Row } from "react-bootstrap";
import { Link } from "react-router-dom";
function CategoryList() {
  const [categories, setCategories] = useState([]);

  // *Sorting* //
  const orderBy = "Asc";
  const groupBy = "Name";
  const [searchName, setSearchName] = useState("");
  // *End of Sorting* //

  // *Paging* //
  const [records, setRecords] = useState(0);
  // *End of Paging* //

  //*Filtering logic*//
  const [areFiltered, setAreFiltered] = useState(false);
  const [hasMore, setHasMore] = useState(false);
  //*End of Filtering logic*//

  const offset = useRef(0);
  const limit = 12;

  useEffect(() => {
    categoryService.GetAll(offset.current, limit).then((response) => {
      setAreFiltered(false);
      setRecords(response.data.totalRecords);
      setHasMore(response.data.hasMore);
      setCategories(response.data.data);
    });
  }, []);

  useEffect(() => {
    offset.current = 0;
    setAreFiltered(true);
    categoryService
      .Filter(searchName, offset.current, limit, "Asc", "CategoryId")
      .then((response) => {
        console.log(response);
        response.data !== ""
          ? (console.log("called"),
            setCategories(response.data.data),
            setHasMore(response.data.hasMore),
            setRecords(response.data.totalRecords))
          : setCategories([]);
      });
  }, [searchName]);
  useEffect(() => {
    if (areFiltered) return;
    categoryService.GetAll(offset.current, limit).then((response) => {
      offset.current = 0;
      setCategories(response.data.data);
      setHasMore(response.data.hasMore);
      setRecords(response.data.totalRecords);
    });
  }, [areFiltered]);
  const fetchNext = () => {
    setCategories([]);
    offset.current += limit;
    !areFiltered
      ? categoryService.GetAll(offset.current, limit).then((response) => {
          setCategories(response.data.data);
          setHasMore(response.data.hasMore);
        })
      : categoryService
          .Filter(searchName, offset.current, limit, "Asc", "CategoryId")
          .then((response) => {
            setCategories(response.data.data);
            setHasMore(response.data.hasMore);
          });
  };
  const fetchPrev = () => {
    setCategories([]);
    offset.current -= limit;
    !areFiltered
      ? categoryService.GetAll(offset.current, limit).then((response) => {
          setCategories(response.data.data);
          setHasMore(response.data.hasMore);
        })
      : categoryService
          .Filter(searchName, offset.current, limit, "Asc", "CategoryId")
          .then((response) => {
            setCategories(response.data.data);
            setHasMore(response.data.hasMore);
          });
  };

  return (
    <div className="container">
      <Row className="my-3">
        <SearchBar setSearchName={setSearchName} />
        <Nav.Item>
          <Link as={Link} to="/admin/categories/create" className="d-flex">
            Create New
          </Link>
        </Nav.Item>
      </Row>
      {!areFiltered && (
        <>
          <span>{records} categories</span>
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
        {categories.length === 0 && "No results for " + searchName}
        {categories.length !== 0 &&
          categories.map((c) => {
            return (
              <div className="w-25 p-3">
                <CategoryCard cardData={c} key={c.id} />
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
        {hasMore && categories.length !== 0 && (
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

export default CategoryList;

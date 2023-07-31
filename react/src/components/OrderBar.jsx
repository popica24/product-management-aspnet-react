import React from "react";

function OrderBar({ setOrderBy, setGroupBy }) {
  const handleGroupByChange = (e) => {
    e.preventDefault();
    setGroupBy(e.currentTarget.getAttribute("data-value"));
  };

  const handleOrderByChange = (e) => {
    e.preventDefault();
    setOrderBy(e.currentTarget.getAttribute("data-value"));
  };

  return (
    <>
      <div className="d-flex flex-row justify-content-between align-items-center">
        <div className="dropdown">
          <button
            className="btn btn-secondary dropdown-toggle"
            type="button"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            Order By
          </button>
          <ul className="dropdown-menu">
            <li>
              <button
                className="dropdown-item"
                onClick={handleGroupByChange}
                data-value="Name"
              >
                Name
              </button>
            </li>
          </ul>
        </div>
        <div className="dropdown">
          <button
            className="btn btn-secondary dropdown-toggle"
            type="button"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            Sort By
          </button>
          <ul className="dropdown-menu">
            <li>
              <button
                className="dropdown-item"
                onClick={handleOrderByChange}
                data-value="Asc"
              >
                Asc
              </button>
            </li>
            <li>
              <button
                className="dropdown-item"
                onClick={handleOrderByChange}
                data-value="Desc"
              >
                Desc
              </button>
            </li>
          </ul>
        </div>
      </div>
    </>
  );
}

export default OrderBar;

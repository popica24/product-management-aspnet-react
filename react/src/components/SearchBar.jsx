import { useState } from "react";

function SearchBar({ setSearchName }) {
  const [input, setInput] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    setSearchName(input);
    setInput("");
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="input-group input-group-md mb-3">
        <span className="input-group-text" id="inputGroup-sizing-sm">
          Search
        </span>
        <input
          type="text"
          className="form-control"
          aria-label="Sizing example input"
          aria-describedby="inputGroup-sizing-sm"
          value={input}
          onChange={(e) => setInput(e.target.value)}
        />
      </div>
    </form>
  );
}

export default SearchBar;

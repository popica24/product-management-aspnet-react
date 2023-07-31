import React from "react";

function Profile({ user }) {
  const token = `${user.token.substring(0, 10)}...${user.token.substring(
    user.token.length - 10
  )}`;

  return (
    <div>
      <p>Email : {user.email}</p>
      <p>Username : {user.username}</p>
      <p>Token : {token}</p>
    </div>
  );
}

export default Profile;

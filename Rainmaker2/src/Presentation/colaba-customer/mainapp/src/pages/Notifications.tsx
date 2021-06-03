import React from "react";
import { useAuth } from "../components/AuthContext";

const Notifications = () => {
  const auth: any = useAuth();

  return auth.user ? (
    <div>
      <h1>Notifications</h1>
    </div>
  ) : (
    <div>
      <h1>Login Now</h1>
    </div>
  );
};

export default Notifications;

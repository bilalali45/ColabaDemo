import React from "react";
import { useAuth } from "./AuthContext";

export default function Footer() {
  const auth: any = useAuth();

  return auth.user ? (
    <div>
      <h1>Footer</h1>
    </div>
  ) : null;
}

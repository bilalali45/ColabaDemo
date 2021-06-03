import React from "react";
import { useAuth } from "./AuthContext";

export default function Header() {
  const auth: any = useAuth();

  return auth.user ? (
    <div>
      <h1>Header</h1>
    </div>
  ) : null;
}

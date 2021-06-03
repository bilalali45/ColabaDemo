import React, { useContext } from "react";
import { Store } from "../Store/Store";

interface HeaderAuthProps {
  className?: string;
}

const HeaderAuth: React.FC<HeaderAuthProps> = ({ className }) => {
  const { state } = useContext(Store);

  const user: any = state.user;
  const tenantInfo = user.tenantInfo;

  return (
    <header className={`colaba__header-auth ${className ? className : ""}`}>
      <div className="container">
        <div className="main-logo">
          <img src={tenantInfo.logo} alt="" />
        </div>
      </div>
    </header>
  );
};

export default HeaderAuth;

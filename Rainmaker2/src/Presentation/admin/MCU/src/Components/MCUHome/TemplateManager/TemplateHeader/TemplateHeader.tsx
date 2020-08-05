import React, { useEffect } from "react";
import { Link, useHistory } from "react-router-dom";
import { LocalDB } from "../../../../Utils/LocalDB";

export const TemplateHeader = () => {
  const history = useHistory();

  useEffect(() => {
    const closeTemplateManager = (e: any) => {
      console.log("key", e.keyCode);
      if (e.keyCode === 27) {
        history.push(`/needList/${LocalDB.getLoanAppliationId()}`);
      }
    };

    document.addEventListener("keydown", closeTemplateManager);

    return () => {
      document.removeEventListener("keydown", closeTemplateManager);
    };
  }, []);

  return (
    <section className="MTheader">
      <h2>Manage Templates</h2>

      <Link
        title="Close"
        to={`/needList/${LocalDB.getLoanAppliationId()}`}
        className="close-ManageTemplate"
      >
        <i className="zmdi zmdi-close"></i>
      </Link>
    </section>
  );
};

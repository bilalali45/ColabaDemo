import React from "react";

export const DocumentSnipet = ({
  clientName,
  active,
}: {
  clientName: string;
  active?: string;
}) => {
  return (
    <div className={`document-snipet ${!!active ? "active" : ""}`}>
      <div className="document-snipet--left">
        <div className="document-snipet--input-group">
          <input
            type="text"
            size={38}
            value="Bank-statement-Jan-to-Mar-2020-1.jpg"
          />
          <button className="document-snipet-btn-ok">
            <em className="zmdi zmdi-check"></em>
          </button>
        </div>
        <small className="document-snipet--detail">
          By Richard Glenn Randall on Apr 17, 2020 at 4:31 AM
        </small>
      </div>
      <div className="document-snipet--right">
        <button className="document-snipet-btn-cancel">
          <em className="zmdi zmdi-close"></em>
        </button>
        <button className="document-snipet-btn-edit">
          <em className="icon-edit2"></em>
        </button>
      </div>
    </div>
  );
};

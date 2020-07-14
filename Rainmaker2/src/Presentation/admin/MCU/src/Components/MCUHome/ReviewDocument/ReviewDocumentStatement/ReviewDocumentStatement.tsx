import React from "react";
import { DocumentSnipet } from "./DocumentSnipet/DocumentSnipet";
import { NeedListDocumentFileType } from "../../../../Entities/Types/Types";

export const ReviewDocumentStatement = ({
  documentName,
  files,
}: {
  files: NeedListDocumentFileType[];
  documentName: string;
}) => {
  return (
    <div
      id="ReviewDocumentStatement"
      data-component="ReviewDocumentStatement"
      className="document-statement"
    >
      <header className="document-statement--header">
        <h2>{documentName}</h2>
      </header>
      <section className="document-statement--body">
        <h3>Documents</h3>

        {/* <DocumentSnipet status="active" /> */}
        {!!files.length &&
          files.map((file) => <DocumentSnipet clientName={file.clientName} />)}

        <hr />

        <div className="clearfix">
          <span className="float-right activity-logs-icon">
            <em className="icon-letter"></em>
          </span>
          <h3>Activity Logs</h3>
        </div>
      </section>
    </div>
  );
};

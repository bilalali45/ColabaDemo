import React, {  useContext } from "react";
import { UploadedDocumentsTable } from "./UploadedDocumentsTable/UploadedDocumentsTable";
import { Store } from "../../../store/store";
export const UploadedDocuments = () => {
  const { state, dispatch } = useContext(Store);
  const loan: any = state.loan;
  const { isMobile } = loan;
  return (
    <div data-testid="uploaded-documents" className={`UploadedDocuments box-wrap ${isMobile?.value? "box-mobilewrap":""}`}>
      <div className="box-wrap--header">
        <h2>Uploaded Documents</h2>
      </div>
      <div className="box-wrap--body clearfix">
        <UploadedDocumentsTable />
      </div>
    </div>
  );
};

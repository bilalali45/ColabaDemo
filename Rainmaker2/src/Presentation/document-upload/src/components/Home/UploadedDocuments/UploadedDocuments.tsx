import React, {  useContext ,useEffect} from "react";
import { UploadedDocumentsTable } from "./UploadedDocumentsTable/UploadedDocumentsTable";
import { Store } from "../../../store/store";
import { Auth } from "../../../services/auth/Auth";
import { DocumentActions } from "../../../store/actions/DocumentActions";
import { DocumentsActionType } from "../../../store/reducers/documentReducer";
export const UploadedDocuments = () => {
  const { state, dispatch } = useContext(Store);
  const { submittedDocs }: any = state.documents;
  const loan: any = state.loan;
  const { isMobile } = loan;

  useEffect(() => {
    fetchUploadedDocuments();
  }, []);

  const fetchUploadedDocuments = async () => {
    if (!submittedDocs) {
      let uploadedDocs = await DocumentActions.getSubmittedDocuments(
        Auth.getLoanAppliationId()
      );
      if (uploadedDocs) {
        dispatch({
          type: DocumentsActionType.FetchSubmittedDocs,
          payload: uploadedDocs,
        });
      }
    }
  };

  return (
    <div data-testid="uploaded-documents" className={`UploadedDocuments box-wrap ${submittedDocs?.length>0?"havedoc":""} ${isMobile?.value? "box-mobilewrap":""}`}>
      <div className="box-wrap--header">
        <h2>Uploaded Documents</h2>
      </div>
      <div className="box-wrap--body clearfix">
        <UploadedDocumentsTable />
      </div>
    </div>
  );
};

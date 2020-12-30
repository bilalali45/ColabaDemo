import React, { useContext, useEffect, useState, useRef } from "react";
import _ from "lodash";
import { UploadedDocumentsTable } from "./UploadedDocumentsTable/UploadedDocumentsTable";
import { Store } from "../../../store/store";
import { Auth } from "../../../services/auth/Auth";
import { DocumentActions } from "../../../store/actions/DocumentActions";
import { DocumentsActionType } from "../../../store/reducers/documentReducer";
import { SVGSearch, SVGUploadDoc, SVGDragFilesToUpload, SVGUploadedDoc, SVGUploadDocumentStikyIcon } from "../../../shared/Components/SVGs";
import { UploadDocumentWithoutRequest } from "../UploadDocumentWithoutRequest/UploadDocumentWithoutRequest";

export const UploadedDocuments = () => {
  const { state, dispatch } = useContext(Store);
  const { submittedDocs }: any = state.documents;
  const loan: any = state.loan;
  const { isMobile } = loan;
  const [popupDocumentUpload, setPopupDocumentUpload] = useState<boolean>(false);

  useEffect(() => {
    fetchUploadedDocuments();
    fetchCategoryDocuments();
    
  }, []);

  useEffect(() => {
    const checkPopupOpen = () => {
      if (popupDocumentUpload == true) {
        document.body.classList.add('overflow-hidden');
      } else {
        document.body.classList.remove('overflow-hidden');
        document.body.classList.remove('lockbody');
      }
    }
    checkPopupOpen();

    const closeDocUploadPopup = () => {
      setPopupDocumentUpload(false);
      document.body.classList.remove('lockbody');
    }
    document.getElementById('mobileNavigation')?.addEventListener('click',closeDocUploadPopup);
    return () => {
      checkPopupOpen();
      document.getElementById('mobileNavigation')?.addEventListener('click',closeDocUploadPopup);
    }
  }, [popupDocumentUpload])

  const fetchUploadedDocuments = async () => {
    if (!submittedDocs) {
      let uploadedDocs = await DocumentActions.getSubmittedDocuments(
        Auth.getLoanAppliationId()
      );
      if (uploadedDocs) {
        const sortedUploadedDocuments = _.orderBy(uploadedDocs, (item) => item.docName, ["asc",]);
        dispatch({
          type: DocumentsActionType.FetchSubmittedDocs,
          payload: sortedUploadedDocuments,
        });
      }
    }
  };

  const fetchCategoryDocuments = async () => {
    let categoryDocuments = await DocumentActions.fetchCategoryDocuments();
    if (categoryDocuments) {
      dispatch({
        type: DocumentsActionType.SetCategoryDocuments,
        payload: categoryDocuments
      });
      dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: {} });
    }
  };

  return (
    <div data-testid="uploaded-documents" className={`UploadedDocuments box-wrap ${submittedDocs?.length > 0 ? "havedoc" : ""} ${isMobile?.value ? "box-mobilewrap" : ""}`}>
      <div className="box-wrap--header">
        <h2>My Documents</h2>
        {!isMobile?.value &&
          <div className="options">
            <button className={`btn btn-icon`} onClick={() => { setPopupDocumentUpload(true) }}><SVGUploadDoc /> Upload Document</button>
          </div>
        }        
      </div>
      <div className="box-wrap--body clearfix">
        <UploadedDocumentsTable />
        {popupDocumentUpload && 
        <UploadDocumentWithoutRequest 
          handlerClose={() => {         
            setPopupDocumentUpload(false);           
            fetchCategoryDocuments();
             }} 
        />}
      </div>

      {isMobile?.value &&
        <div className="mobile-uploaded-document-option">          
            <button className={`btn-mudo`} onClick={() => { setPopupDocumentUpload(true) }} id="btnPopupDocumentUpload">
              <span className="mudo-text">Upload Document</span>
              <span className="mudo-icon"><SVGUploadDocumentStikyIcon/></span>
            </button>
        </div>
      } 

    </div>
  );
};

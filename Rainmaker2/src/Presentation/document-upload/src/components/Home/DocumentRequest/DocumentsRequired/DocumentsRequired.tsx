import React, { useEffect, useContext, useRef, useState } from "react";

import { DocumentActions } from "../../../../store/actions/DocumentActions";
import { Store } from "../../../../store/store";
import { isArray } from "util";
import { Auth } from "../../../../services/auth/Auth";
import { DocumentsActionType } from "../../../../store/reducers/documentReducer";
import { DocumentRequest } from "../../../../entities/Models/DocumentRequest";
import { Redirect } from "react-router-dom";
import { AlertBox } from "../../../../shared/Components/AlertBox/AlertBox";

type DocumentsRequiredType = {
  setCurrentInview?: any
}

export const DocumentsRequired = ({setCurrentInview} : DocumentsRequiredType) => {
  const [showAlert, setshowAlert] = useState<boolean>(false);
  const [triedSelected, setTriedSelected] = useState();
  const { state, dispatch } = useContext(Store);
  const { pendingDocs }: any = state.documents;
  const { currentDoc }: any = state.documents;
  const loan: any = state.loan;
  const {isMobile} = loan; 
  const selectedFiles = currentDoc?.files || [];

  const sideBarNav = useRef<HTMLDivElement>(null);

  const stopBrowerTabFromClosing = () => {
    window.onbeforeunload = confirmExit;
    function confirmExit() {
      return "show message";
    }
  };

  const allowBrowserClosing = () => {
    window.onbeforeunload = confirmExit;
    function confirmExit() {
      return null;
    }
  };

  useEffect(() => {
    if (pendingDocs?.length) {
      setCurrentDoc(pendingDocs[0]);
    }
    fetchPendingDocs();
  }, []);

  useEffect(() => {
    const files =
      selectedFiles.filter((f) => f.uploadStatus === "pending").length > 0;

    if (sideBarNav.current) {
      if (files) {
        stopBrowerTabFromClosing();
        sideBarNav.current.onclick = showAlertPopup;
      } else {
        allowBrowserClosing();
        sideBarNav.current.onclick = null;
      }
    }
  }, [selectedFiles]);

  const showAlertPopup = () => {
    setshowAlert(true);
  };

  const setCurrentDoc = (doc) => {
    dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: doc });
  };

  const fetchPendingDocs = async () => {
    if (!pendingDocs) {
      let docs = await DocumentActions.getPendingDocuments(
        Auth.getLoanAppliationId()
      );
      if (docs) {
        dispatch({ type: DocumentsActionType.FetchPendingDocs, payload: docs });
        setCurrentDoc(docs[0]);
      }
    }
  };

  const changeCurrentDoc = (curDoc: DocumentRequest) => {
    let uploadInProgress = currentDoc?.files?.find(
      (f) => f.uploadProgress > 0 && f.uploadStatus !== "done"
    );
    if (!uploadInProgress) {
      dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: curDoc });
    } else {
      alert("please wait for the files to finish before nevavigating!");
    }
  };

  const renderRequiredDocs = () => {
    if (pendingDocs) {
      return (
        <ul>
          {pendingDocs.map((pd: any, i: number) => {
            return (
              <li
                data-testid={`pending-doc-${i}`}
                key={pd?.docId}
                onClick={() => {
                  setCurrentInview('documentUploadView')
                  if (currentDoc && pd?.docId === currentDoc?.docId) {
                    setshowAlert(false);
                    return;
                  }
                  if (showAlert) {
                    if (pd) {
                      setTriedSelected(pd);
                    }
                    return;
                  }
                  changeCurrentDoc(pd);
                }}
              >
                <a title={pd.docName}
                  className={
                    (currentDoc && !isMobile.value && pd?.docId === currentDoc?.docId)
                      ? "active"
                      : ""
                  }
                >
                  <span> {pd.docName}</span>
                 
                </a>
                {isMobile.value && <div className="pd-m-arrow-icon"><i className="zmdi zmdi-chevron-right"></i></div>}
              </li>
            );
          })}
        </ul>
      );
    }
    return "";
  };
  console.log('in here  ------------------------- ----------------------- ', pendingDocs?.length);
  if (pendingDocs?.length === 0) {
    console.log('in here yyoiu adsfj 9u, 149-04--=====================4234 234=24=23=4=    =', pendingDocs);
    return <Redirect to={`/activity/${Auth.getLoanAppliationId()}`} />;
  }

  return (
    <div ref={sideBarNav} className="dr-list-wrap">
      <nav>{pendingDocs && renderRequiredDocs()}</nav>
      {showAlert && (
        <AlertBox
          triedSelected={triedSelected}
          hideAlert={() => setshowAlert(false)}
        />
      )}
    </div>
  );
};

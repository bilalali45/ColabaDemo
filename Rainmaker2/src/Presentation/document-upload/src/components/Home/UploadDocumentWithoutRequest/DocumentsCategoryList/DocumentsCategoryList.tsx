import React, { useEffect, useContext, useRef, useState } from "react";

import { DocumentActions } from "../../../../store/actions/DocumentActions";
import { Store } from "../../../../store/store";
import { isArray } from "util";
import { Auth } from "../../../../services/auth/Auth";
import { DocumentsActionType } from "../../../../store/reducers/documentReducer";
import { DocumentRequest } from "../../../../entities/Models/DocumentRequest";
import { Redirect } from "react-router-dom";
import { AlertBox } from "../../../../shared/Components/AlertBox/AlertBox";
import doneTaskListIcon from "../../../../assets/images/doneTasklist-icon.svg";
import noTaskListIcon from "../../../../assets/images/empty-doc-req-icon-mobile.svg";
type DocumentsRequiredType = {
  setCurrentInview?: any,
  handlerClickItem?:any,
  documentTypeItems: any
}

export const DocumentsCategoryList = ({setCurrentInview, handlerClickItem, documentTypeItems} : DocumentsRequiredType) => {
  
  const { state, dispatch } = useContext(Store);
  const { categoryDocuments }: any = state.documents;
  const { currentDoc }: any = state.documents;
  const loan: any = state.loan;
  const {isMobile} = loan; 
  const selectedFiles = currentDoc?.files || [];
  const [showAlert, setshowAlert] = useState<boolean>(false);
  const [triedSelected, setTriedSelected] = useState();
  

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
    const files = selectedFiles.filter((f) => f.uploadStatus === "pending").length > 0;

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

  
  const changeCurrentDoc = (item: any) => {
    let uploadInProgress = currentDoc?.files?.find(
      (f) => f.uploadProgress > 0 && f.uploadStatus !== "done"
    );
    if (!uploadInProgress) {
      let curDoc = {
        id:'',
        requestId: '',
        docId: item.docTypeId,
        docName:item.docType,
        docMessage:item.docMessage,
        files: [],
        isRejected: false,
        resubmittedNewFiles: false
      }
      dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: curDoc });
    } else {
      alert("please wait for the files to finish before nevavigating!");
    }
  };

  const renderCategoryitems = (items: any) => {
    if (items) {
      return (
        <div className="popup-doc-upload--list">
         <ul>
         {items.map((item: any, i: number) => {
            return (
           <>
           <li className={ isMobile?.value ? "" : (currentDoc && item?.docTypeId === currentDoc?.docId) ? "current" : ""} 
                onClick= {() => {
                  if (currentDoc && item?.docTypeId === currentDoc?.docId) {
                        setshowAlert(false);
                        if(handlerClickItem){
                          handlerClickItem();
                        }
                          return;
                    }
                  if (showAlert) {
                    if (item) {
                      setTriedSelected(item);
                    }
                    return;
                  }
                  changeCurrentDoc(item);
                   }} >
                     <a draggable="false" href="javascript:;" title={item.docType}><span className="list-text">{item.docType}</span></a></li>
           </>
            );
         })}
         </ul>
        </div>
      )
    }
    return "";
  }

  const renderCategoryDocs = () => {
    if (documentTypeItems) {
           return (
             <>             
                 {documentTypeItems.map((pd: any, i: number) => {  
                    if(pd.documents.length > 0){
                      return (
                        <section className="popup-doc-upload--list-group">
                        <h4 className="h4">{pd.catName}</h4>
                        {renderCategoryitems(pd.documents)}
                        </section>
                      );
                    }                                                  
                 })}            
             </>
           );
    }
    return "";
  };
 
  return (
    <div data-testid="requiredDocsList" ref={sideBarNav} className="dr-list-wrap">
      <nav>{categoryDocuments && renderCategoryDocs()}</nav>
      {showAlert && (
        <AlertBox
          triedSelected={triedSelected}
          hideAlert={() => setshowAlert(false)}
          type= {`WithoutReq`}
        />
      )}
    </div>
  );
};

import React, { useContext, useEffect, useState, useRef } from "react";
import Overlay from "react-bootstrap/Overlay";
import Popover from "react-bootstrap/Popover";
import { CategoryDocument } from "../../../../../../Models/CategoryDocument";
import { DocumentRequest } from "../../../../../../Models/DocumentRequest";
import DocumentActions from "../../../../../../Store/actions/DocumentActions";
import { DocumentActionsType } from "../../../../../../Store/reducers/documentsReducer";
import { ViewerActionsType } from "../../../../../../Store/reducers/ViewerReducer";
import { Store } from "../../../../../../Store/Store";
import { LocalDB } from "../../../../../../Utilities/LocalDB";
import {AddDocIcon} from "../../../../../../shared/Components/Assets/SVG";

export const ReassignDropdown = ({
  target,
  container,
  hide,
  visible,
  selectedFile,
  isFromWorkbench,
  getDocswithfailedFiles,
  placement,
  refReassignPopover,

}: any) => {
  const { state, dispatch } = useContext(Store);
  let { currentFile, isFileChanged,fileToChangeWhenUnSaved, performNextAction }: any = state.viewer;
  const { currentDoc, documentItems, importedFileIds }: any = state.documents;
  const [docCategories, setDocCategories] = useState<DocumentRequest[]>();
  let loanApplicationId = LocalDB.getLoanAppliationId();


  //const combinedRef = useRef<any>(ref);

  useEffect(() => {
    fetchDocCategories();
  }, []);

  useEffect(()=>{
    
    if(fileToChangeWhenUnSaved && performNextAction && fileToChangeWhenUnSaved.action === "reassign"){
      performNextActionFn()
    }

  },[performNextAction])


  const performNextActionFn= async () =>{

    selectedFile = fileToChangeWhenUnSaved.selectedFile;
    isFromWorkbench = currentFile.isWorkBenchFile;
    isFileChanged = false
    await ReassignCategory(fileToChangeWhenUnSaved.document)
    
  }

  const ReassignCategory = async (document: DocumentRequest) => {
    dispatch({ type: ViewerActionsType.SetPerformNextAction, payload: false });
    
    if (isFromWorkbench) {
      if (isFileChanged && selectedFile?.fileId === currentFile?.fileId) {
        dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });
        dispatch({ type: ViewerActionsType.SetFileToChangeWhenUnSaved, payload: { selectedFile, document, action:"reassign", isWorkbenchFile:isFromWorkbench } });
        return;
      }

      let res = await DocumentActions.moveFromWorkBenchToCategory(
        document.id,
        document.requestId,
        document.docId,
        selectedFile.fileId
      );
      if (res) {
        await DocumentActions.getDocumentItems(dispatch, importedFileIds);
        await DocumentActions.getWorkBenchItems(dispatch, importedFileIds);
      }

    } else {

      if (isFileChanged && selectedFile?.id === currentFile?.fileId) {
        dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });
        dispatch({ type: ViewerActionsType.SetFileToChangeWhenUnSaved, payload: { selectedFile, document, action:"reassign" } });
        return;
      }
      if (selectedFile && selectedFile.id) {
        let res = await DocumentActions.reassignDoc(
          currentDoc.id,
          currentDoc.requestId,
          currentDoc.docId,
          selectedFile.id,
          document.requestId,
          document.docId
        );
        if (res) {
          
          getDocswithfailedFiles()
        }
      }
    }
    dispatch({ type: ViewerActionsType.SetFileToChangeWhenUnSaved, payload: null });
    dispatch({ type: ViewerActionsType.SetPerformNextAction, payload: false });
    hide();

  };

  const fetchDocCategories = () => {
    if (documentItems && currentDoc) {
      if (isFromWorkbench) {
        setDocCategories(documentItems);
      } else {
        setDocCategories(
          documentItems.filter((item: any) => item.docId !== currentDoc.docId)
        );
      }
    }
  };
  const openAddDocPopover = () => {
    let addDocLInk = window.document.getElementById("dm-h-linkAddDoc");
    addDocLInk.click(); 
  };
  return (
    <Overlay
      show={visible}
      target={target}
      placement={placement ? placement : "bottom-end"}
      container={container}
      containerPadding={20}
      onHide={hide}
      rootClose={true}
    >
      <Popover id="popover-contained" className="ReassignOverlay">
        <div ref={refReassignPopover}>
          <Popover.Title as="h3">Select Document Type</Popover.Title>
          <Popover.Content >
            {docCategories && docCategories.length > 0 ? (
              <div className="wrap-doc-type">
                <ol className="dm-dt-docList" >
                  {docCategories &&
                    docCategories.length > 0 &&
                    docCategories.map((doc: any, i: number) => (
                      <li title={doc.docName} key={i} onClick={() => ReassignCategory(doc)}>
                        {doc.docName}
                      </li>
                    ))}
                </ol>
              </div>
            ) : (
                <div className="emptyList">
                  <p>There is no item in List</p>
                </div>
              )}
          </Popover.Content>
          <Popover.Title as="div" bsPrefix="popover-footer">
                                            <div className="dh-actions-lbl-wrap" onClick={openAddDocPopover}>
                                                <div className="dm-h-icon"><AddDocIcon /></div>
                                                <div className="dm-h-lbl">
                                                    <span>Add Document</span>
                                                </div>
                                            </div>
                                        </Popover.Title>
        </div>
      </Popover>
    </Overlay>
  );
};

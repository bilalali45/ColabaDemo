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
  const { currentFile, isFileChanged }: any = state.viewer;
  const { currentDoc, documentItems }: any = state.documents;
  const [docCategories, setDocCategories] = useState<DocumentRequest[]>();
  let loanApplicationId = LocalDB.getLoanAppliationId();


  //const combinedRef = useRef<any>(ref);

  useEffect(() => {
    fetchDocCategories();
  }, []);

  const ReassignCategory = async (document: DocumentRequest) => {


    if (isFromWorkbench) {

      console.log('in here!!!!', currentFile, selectedFile)
      if (isFileChanged && selectedFile?.fileId === currentFile?.fileId) {
        dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });

        return;
      }

      let res = await DocumentActions.moveFromWorkBenchToCategory(
        document.id,
        document.requestId,
        document.docId,
        selectedFile.fileId
      );
      if (res) {
        await DocumentActions.getDocumentItems(dispatch);
        await DocumentActions.getWorkBenchItems(dispatch);
      }

    } else {

      console.log('in here!!!!', currentFile, selectedFile)
      if (isFileChanged && selectedFile?.id === currentFile?.fileId) {
        dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });

        return;
      }
      if (selectedFile.id) {
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

  return (
    <Overlay
      show={visible}
      target={target}
      placement={placement ? placement : "bottom-end"}
      container={container}
      containerPadding={20}
      onHide={hide}
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
        </div>
      </Popover>
    </Overlay>
  );
};

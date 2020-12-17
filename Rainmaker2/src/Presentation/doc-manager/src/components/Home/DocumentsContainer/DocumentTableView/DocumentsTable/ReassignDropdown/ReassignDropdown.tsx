import React, { useContext, useEffect, useState } from "react";
import Overlay from "react-bootstrap/Overlay";
import Popover from "react-bootstrap/Popover";
import { CategoryDocument } from "../../../../../../Models/CategoryDocument";
import { DocumentRequest } from "../../../../../../Models/DocumentRequest";
import DocumentActions from "../../../../../../Store/actions/DocumentActions";
import { DocumentActionsType } from "../../../../../../Store/reducers/documentsReducer";
import { Store } from "../../../../../../Store/Store";
import { LocalDB } from "../../../../../../Utilities/LocalDB";

export const ReassignDropdown = ({
  target,
  container,
  hide,
  visible,
  selectedFile,
  isFromWorkbench,
  getDocswithfailedFiles
}: any) => {
  const { state, dispatch } = useContext(Store);
  const { currentFile }: any = state.viewer;
  const { currentDoc, documentItems }: any = state.documents;
  const [docCategories, setDocCategories] = useState<DocumentRequest[]>();
  let loanApplicationId = LocalDB.getLoanAppliationId();

  useEffect(() => {
    fetchDocCategories();
  }, []);

  const ReassignCategory = async (document: DocumentRequest )=> {
    if (isFromWorkbench ) {
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
      placement="bottom-end"
      container={container}
      containerPadding={20}
      onHide={hide}
      rootClose={true}>
      <Popover id="popover-contained" className="ReassignOverlay">
        <Popover.Title as="h3">Select Document Type</Popover.Title>
        <Popover.Content>
          {docCategories && docCategories.length > 0 ? (
          <div className="wrap-doc-type">
            <ol className="dm-dt-docList">
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
      </Popover>
    </Overlay>
  );
};

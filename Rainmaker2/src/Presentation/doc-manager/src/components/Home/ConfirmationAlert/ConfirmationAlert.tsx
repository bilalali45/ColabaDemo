import React, { useContext } from "react";
import { Button, Modal } from "react-bootstrap";
import { ViewerActionsType } from "../../../Store/reducers/ViewerReducer";
import { Store } from "../../../Store/Store";
import { ViewerTools } from "../../../Utilities/ViewerTools";

export const ConfirmationAlert = ({ viewFile }: any) => {
  const { state, dispatch } = useContext(Store);
  const { currentFile, selectedFileData, isLoading, isFileChanged, showingConfirmationAlert, fileToChangeWhenUnSaved }: any = state.viewer;
  const { currentDoc, workbenchItems, importedFileIds }: any = state.documents;
  
  const DiscardChanges = async() => {
    ViewerTools.discardChanges(dispatch, currentFile)
    await dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: false });
    if(fileToChangeWhenUnSaved.action !== "dragged")
    dispatch({ type: ViewerActionsType.SetPerformNextAction, payload: true });
  };

   
  const SaveChanges = async() => {

    dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: false });
    let fileObj:any =  {}
    if(currentFile.isWorkBenchFile){
      let{id, fileId, name } = currentFile
      fileObj = {
        id, 
        requestId:"000000000000000000000000", 
        docId:"000000000000000000000000", 
        fileId, 
        isFromWorkbench:true,
        name
      }
      await ViewerTools.saveViewerFileWithAnnotations(fileObj, isFileChanged, dispatch, workbenchItems, currentFile, importedFileIds)
    }
    else{
      let{fileId, name } = currentFile
      let {id, requestId, docId} = currentDoc;
      fileObj = {
        id, 
        requestId, 
        docId, 
        fileId, 
        isFromCategory:true,
        name
      }
      await ViewerTools.saveViewerFileWithAnnotations(fileObj, isFileChanged, dispatch, currentDoc, currentFile, importedFileIds)
     }
     if(fileToChangeWhenUnSaved.action !== "dragged")
      dispatch({ type: ViewerActionsType.SetPerformNextAction, payload: true });
    
  };

  return (
    <Modal
      backdrop="static"
      keyboard={true}
      className="modal-alert"
      //   onHide={}
      centered
      show={true}>
      {/* <div className="close-wrap">
        <button
          onClick={() => {
            dispatch({
              type: ViewerActionsType.SetShowingConfirmationAlert,
              payload: false,
            });
          }}
          type="button"
          className="close">
          <span aria-hidden="true">×</span>
          <span className="sr-only">Close</span>
        </button>
      </div> */}
      <Modal.Header> </Modal.Header>
      <Modal.Body>
        <h3 className="text-center">
          Do you want to leave without saving changes?
        </h3>
      </Modal.Body>
      <Modal.Footer>
        <p className="text-center">
          {/* <Button onClick={saveAndContinue} variant="primary">
                        {"Save & Continue"}
                    </Button> {" "} */}
          
            <button
              onClick={ SaveChanges}
              className="btn btn-sm btn-secondry">
              Save
            </button>
            
            <button
              onClick={DiscardChanges}
              className="btn btn-sm btn-primary">
              Don’t Save
            </button>
          
        </p>
      </Modal.Footer>
    </Modal>
  );
};

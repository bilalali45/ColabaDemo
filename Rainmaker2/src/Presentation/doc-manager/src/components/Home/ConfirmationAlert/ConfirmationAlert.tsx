import React, { useContext } from 'react'
import { Button, Modal } from 'react-bootstrap'
import { ViewerActionsType } from '../../../Store/reducers/ViewerReducer';
import { Store } from '../../../Store/Store';
import { ViewerTools } from '../../../Utilities/ViewerTools';

export const ConfirmationAlert = ({ viewFile }: any) => {

    const { state, dispatch } = useContext(Store);

    const { isFileChanged, showingConfirmationAlert, fileToChangeWhenUnSaved, currentFile }: any = state.viewer;
    const { currentDoc, workbenchItems }: any = state.documents;

    const saveAndContinue = async () => {
        dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: false });

        let { file, document }: any = fileToChangeWhenUnSaved;


        if (file.isWorkBenchFile) {
            let { id, fileId, isWorkBenchFile, name } = currentFile
            let annotationObj = {
                id,
                requestId: "000000000000000000000000",
                docId: "000000000000000000000000",
                fileId,
                isFromWorkbench: true,
                name
            }
            await ViewerTools.saveViewerFileWithAnnotations(annotationObj, isFileChanged, dispatch, workbenchItems);

        }
        else {
            let { fileId, name } = currentFile
            let { id, requestId, docId } = currentDoc;
            let annotationObj = {
                id,
                requestId,
                docId,
                fileId,
                isFromCategory: true,
                name
            }
            await ViewerTools.saveViewerFileWithAnnotations(annotationObj, isFileChanged, dispatch, currentDoc);
        }

        viewFile(file, document, dispatch);
        dispatch({ type: ViewerActionsType.SetFileToChangeWhenUnSaved, payload: {} });

    }

    const cancelAndContinue = () => {
        console.log(state);
        let { file, document }: any = fileToChangeWhenUnSaved;
        viewFile(file, document, dispatch);
    }

    return (

        //  <Modal
        //     backdrop="static"
        //     keyboard={true}
        //     className="modal-alert sync-error-alert"
        //     centered
        //     show={true}
        // >
        //     <Modal.Header closeButton onClick={() => { dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: false }) }}>
        //         <div className="h-wrap" data-testid="sync-alert-Header">
        //             <div className="e-content">
        //                 <h4>Document Synchronization Failed</h4>
        //                 <p>Synchronization was not successfully completed</p>
        //             </div>
        //         </div>

        //     </Modal.Header>
        //     <Modal.Body>
        //         <div className="sync-modal-alert-body">

        //             <h4>Documents that have failed to sync</h4>

        //             <p className="text-center">
        //                 <Button onClick={saveAndContinue} variant="primary">
        //                     {"Save & Continue"}
        //                 </Button>
        //             </p>
        //             <p className="text-center">
        //                 <Button onClick={cancelAndContinue} variant="primary">
        //                     {"Cancel & Continue"}
        //                 </Button>
        //             </p>
        //         </div>
        //     </Modal.Body>
        // </Modal>
        
        <Modal
            backdrop="static"
            keyboard={true}
            className="modal-alert"
         //   onHide={}
            centered
            show={true}
        >
            <div className="close-wrap">
<button 
onClick={() => { dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: false }) }}
type="button" className="close"><span aria-hidden="true">×</span><span className="sr-only">Close</span></button>
</div>
            <Modal.Header> </Modal.Header>
            <Modal.Body>
  <h3 className="text-center">
    Are you sure you want to close this request 
    <br /> without saving document?
  </h3>
</Modal.Body>
<Modal.Footer>
<p className="text-center">
<Button onClick={saveAndContinue} variant="primary">
                            {"Save & Continue"}
                        </Button> {" "}
                        <Button onClick={cancelAndContinue} variant="primary">
                            {"Cancel & Continue"}
                        </Button>
  </p>
</Modal.Footer>
        </Modal>

      
    )
}

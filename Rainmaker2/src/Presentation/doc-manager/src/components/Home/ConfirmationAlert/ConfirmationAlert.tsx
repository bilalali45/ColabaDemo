import React, { useContext } from 'react'
import { Button, Modal } from 'react-bootstrap'
import { ViewerActionsType } from '../../../Store/reducers/ViewerReducer';
import { Store } from '../../../Store/Store';
import { ViewerTools } from '../../../Utilities/ViewerTools';

export const ConfirmationAlert = ({ viewFile }: any) => {

    const { state, dispatch } = useContext(Store);

    const cancelAndContinue = () => {
        dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: false });
    }

    return (

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
                <h3 className="text-center">Save or Discard your changes before proceeding.</h3>
            </Modal.Body>
            <Modal.Footer>
                <p className="text-center">
                    {/* <Button onClick={saveAndContinue} variant="primary">
                        {"Save & Continue"}
                    </Button> {" "} */}
                    <Button onClick={cancelAndContinue} variant="primary">
                        {"OK"}
                    </Button>
                </p>
            </Modal.Footer>
        </Modal>


    )
}

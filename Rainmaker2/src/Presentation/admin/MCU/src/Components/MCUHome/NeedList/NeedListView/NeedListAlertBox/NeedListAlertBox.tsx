import React, {useState} from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from "react-bootstrap/Button";
import { NeedList } from '../../../../../Entities/Models/NeedList';

type NeedListAlertProps = {
    showFailedToSyncBox: boolean;
    needList: NeedList;
}

export const NeedListAlertBox = ({showFailedToSyncBox, needList} : NeedListAlertProps) => {
    return (
        <Modal
        show={showFailedToSyncBox}
        // onHide={handleClose}
        backdrop="static"
        keyboard={true}
        className="modal-alert"
        centered
    >
        <Modal.Header closeButton>
        Document Synchronization Failed
        </Modal.Header>

        <Modal.Body>
        
            <p className="text-center">
  Lorem ipsum dolor sit amet consectetur adipisicing elit. Repudiandae dignissimos ducimus aspernatur earum beatae mollitia ex nisi nobis soluta maiores dicta quae deserunt, ut non tenetur accusantium veritatis atque perferendis.
            </p>
        </Modal.Body>
        <Modal.Footer>
        <p className="text-center">
      
              <Button  variant="primary">
                {"Sync again"} 
              </Button>
            </p>
        </Modal.Footer>
    </Modal>
    )
}
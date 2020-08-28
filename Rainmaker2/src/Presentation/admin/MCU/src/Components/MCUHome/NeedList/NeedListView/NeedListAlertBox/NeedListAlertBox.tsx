import React, {useState} from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from "react-bootstrap/Button";
import { NeedList } from '../../../../../Entities/Models/NeedList';
import errorIcon from '../../../../../Assets/images/error-icon.svg';
type NeedListAlertProps = {
    showFailedToSyncBox: boolean;
    needList: NeedList;
}

export const NeedListAlertBox = ({showFailedToSyncBox, needList} : NeedListAlertProps) => {
    return (
        <Modal
        show={showFailedToSyncBox}
        //show={true} 
        // onHide={handleClose}
        backdrop="static"
        keyboard={true}
        className="modal-alert sync-error-alert"

        centered
    >
        <Modal.Header closeButton>
            <div className="h-wrap">
                <div className="e-icon">
                    <img src={errorIcon} alt="" />
                </div>
                <div className="e-content">
                    <h4>Document Synchronization Failed</h4>
                    <p>Synchronization was not successfully completed</p>
                </div>
            </div>
        
        </Modal.Header>

        <Modal.Body>
  
  <div className="sync-modal-alert-body">

      <h4>Documents that have failed to sync</h4>
      <ul>
          <li>
              <h5>Bank Statement</h5>
              <p>Bank-statement-Jan-to-Mar-2020-1.jpg</p>
              <p>Bank-statement-Jan-to-Mar-2020-1.jpg</p>
              <p>Bank-statement-Jan-to-Mar-2020-1.jpg</p>
          </li>

          <li>
              <h5>Personal Tax Return</h5>
              <p>Personal Tax Returns.pdf</p>
          </li>

          <li>
              <h5>Alimony Income Verification</h5>
              <p>Income Verification.pdf</p>
          </li>

          <li>
              <h5>Alimony Income Verification</h5>
              <p>Income Verification.pdf</p>
          </li>

          <li>
              <h5>Alimony Income Verification</h5>
              <p>Income Verification.pdf</p>
          </li>


          <li>
              <h5>Alimony Income Verification</h5>
              <p>Income Verification.pdf</p>
          </li>


          <li>
              <h5>Alimony Income Verification</h5>
              <p>Income Verification.pdf</p>
          </li>


          <li>
              <h5>Alimony Income Verification</h5>
              <p>Income Verification.pdf</p>
          </li>
      </ul>

  </div>
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
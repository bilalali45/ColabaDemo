import React, {useState, Fragment} from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from "react-bootstrap/Button";
import { NeedList } from '../../../../../Entities/Models/NeedList';
import errorIcon from '../../../../../Assets/images/error-icon.svg';
type NeedListAlertProps = {
    showFailedToSyncBox: boolean;
    needList: any;
    syncAgain: Function;
    handleClose: Function;
}

export const NeedListAlertBox = ({showFailedToSyncBox, needList, syncAgain, handleClose} : NeedListAlertProps) => {

   const renderFailedSyncList = () => {
     let arr =  getFailedList();
    if(arr.length > 0){
        return (
            <Fragment>
           {arr.map((item: any) => {
            return (
           <li>
            <h5>{item.docName}</h5>
            {item.fileList.map((item2: any) => {
                return <p>{item2}</p>
            })}
            
           </li>
            )
           })}

            </Fragment>
            
            )
    }
    }

  const getFailedList = () => {
      let newListArray = [];
      let docName = '';
      for(let i = 0; i < needList?.length; i++){
        docName = needList[i].docName;
        let files = [];
        for(let k = 0; k < needList[i].files.length; k++){
          if( needList[i].files[k].byteProStatus === "sync failed"){
            files.push(needList[i].files[k].clientName)         
          }
        }
        if(files.length > 0){
            newListArray.push({ docName: docName, fileList:  files } )
        }
     }
   return newListArray;
  }
    return (
        <Modal
        show={showFailedToSyncBox}
        onHide={handleClose}
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
        {renderFailedSyncList()}
      </ul>
      <p className="text-center"> 
              <Button onClick={() => syncAgain()}  variant="primary">
                {"Sync again"} 
              </Button>
            </p>
  </div>
        </Modal.Body>
    </Modal>
    )
}
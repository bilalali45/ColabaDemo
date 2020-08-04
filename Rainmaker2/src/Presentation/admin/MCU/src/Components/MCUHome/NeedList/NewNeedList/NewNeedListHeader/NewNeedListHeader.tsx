import React, { useEffect, useCallback, useState } from "react";
import { Http } from "rainsoft-js";
import Spinner from "react-bootstrap/Spinner";
import Modal from 'react-bootstrap/Modal';
import ModalDialog from 'react-bootstrap/ModalDialog';
import ModalHeader from 'react-bootstrap/ModalHeader';
import ModalTitle from 'react-bootstrap/ModalTitle';
import ModalBody from 'react-bootstrap/ModalBody';
import ModalFooter from 'react-bootstrap/ModalFooter';
import Button from "react-bootstrap/Button";


type NewNeedListHeaderType = {
    saveAsDraft: Function
}

export const NewNeedListHeader = ({saveAsDraft} : NewNeedListHeaderType) => {
    const [show, setShow] = useState(true);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return (
        <>
        <section className="MTheader">
            <div className="addneedlist-actions">
                <button className="btn btn-sm btn-secondary">Close</button>
                <button onClick={() => saveAsDraft()} className="btn btn-sm btn-primary">Save as Close</button>
            </div>
        </section>
        <Modal
            show={show}
            onHide={handleClose}
            backdrop="static"
            keyboard={true}
            className="modal-alert"
            centered
        >
            <Modal.Header closeButton>
                {/* <Modal.Title></Modal.Title> */}
            </Modal.Header>

            <Modal.Body>
                <h3 className="text-center">Are you sure you want to close this<br /> request without saving?</h3>
                <p className="text-center">
                    <Button variant="secondary">Close</Button>
                    {" "}
                    <Button variant="primary">Save & Close</Button>
                </p>
            </Modal.Body>
            <Modal.Footer>
                
            </Modal.Footer>
        </Modal>

        </>
    )
}
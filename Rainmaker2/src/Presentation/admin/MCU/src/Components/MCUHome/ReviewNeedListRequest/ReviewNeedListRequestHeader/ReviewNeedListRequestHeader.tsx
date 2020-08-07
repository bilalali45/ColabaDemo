import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import { TemplateDocument } from "../../../../Entities/Models/TemplateDocument";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import { LocalDB } from "../../../../Utils/LocalDB";

type NewNeedListHeaderType = {
  saveAsDraft: Function;
  showReview: boolean;
  toggleShowReview: Function;
  documentList: TemplateDocument[]
};

export const ReviewNeedListRequestHeader = ({
  saveAsDraft,
  showReview,
  toggleShowReview,
  documentList
}: NewNeedListHeaderType) => {
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const history = useHistory();

  const closeHandler = () => {
    history.push(`/needList/${LocalDB.getLoanAppliationId()}`);
  };

  return (
    <>
      <div
        id="ReviewNeedListRequestHeader"
        data-component="ReviewNeedListRequestHeader"
        className="mcu-panel-header"
      >
        <div className="row">
          <div className="mcu-panel-header--left col-md-8">
            {showReview && (
              <button
                onClick={(e) => toggleShowReview(e)}
                className="btn btn-sm btn-back"
              >
                <em className="zmdi zmdi-arrow-left"></em> Back
              </button>
            )}
          </div>

          <div className="mcu-panel-header--right col-md-4">
            <button
              onClick={() => setShow(true)}
              className="btn btn-sm btn-secondary"
            >
              Close
            </button>{" "}
            <button
              disabled={!documentList?.length}
              onClick={() => saveAsDraft(true)}
              className="btn btn-sm btn-primary"
            >
              Save & Close
            </button>
          </div>
        </div>
      </div>
      {
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
            <h3 className="text-center">
              Are you sure you want to close this
              <br /> request without saving?
            </h3>
            <p className="text-center">
              <Button onClick={closeHandler} variant="secondary">
                Close
              </Button>{" "}
              <Button onClick={() => saveAsDraft(true)} variant="primary">
                Save & Close
              </Button>
            </p>
          </Modal.Body>
          <Modal.Footer></Modal.Footer>
        </Modal>
      }
    </>
  );
};

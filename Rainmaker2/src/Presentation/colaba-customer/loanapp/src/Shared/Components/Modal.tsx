import React, { MouseEventHandler } from "react";
import Modal from "react-bootstrap/Modal";


interface PopupModalProps {
  size?: string | any;
  modalHeading?: string | any;
  modalHeaderData?: React.ReactNode | any;
  modalBodyData?: React.ReactNode | any;
  modalFooterData?: React.ReactNode | any;
  show: boolean;
  handlerShow: MouseEventHandler;
  dialogClassName?: string;
  keyboard?: boolean;
  noHeader?: boolean;
  backDrop?: any;
}

export const PopupModal: React.FC<PopupModalProps> = ({ size, modalHeading, modalHeaderData, modalBodyData, modalFooterData, show, handlerShow, dialogClassName, keyboard, noHeader, backDrop }) => {

  return (
    <>
      {show &&
        <>
          <Modal className={`loanapp-c-modal ${noHeader && 'no-header'}`} centered size={size} show={show} onHide={handlerShow} dialogClassName={dialogClassName} 
            //backdrop={backdrop ? backdrop : 'static'} 
            backdrop={backDrop}
            keyboard={keyboard ? keyboard : true}>
            {!noHeader &&
                <Modal.Header>
                <div data-testid="modal">
                    <Modal.Title>{modalHeading}</Modal.Title>
                    {modalHeaderData && <span dangerouslySetInnerHTML={{ __html: modalHeaderData }}></span>}
                </div>
                <button data-testid="modal-close" type="button" className="close" onClick={handlerShow}><em className="zmdi zmdi-close"></em></button>
                </Modal.Header>
            }
            <Modal.Body>
                {noHeader &&
                    <button data-testid="modal-close" type="button" className="close" onClick={handlerShow}><em className="zmdi zmdi-close"></em></button>
                }
              {/* <div dangerouslySetInnerHTML={{ __html: modalBodyData }}></div> */}
              {modalBodyData}
            </Modal.Body>
            {modalFooterData &&
              <Modal.Footer>
                {/* <div dangerouslySetInnerHTML={{ __html: modalFooterData }}></div> */}
                {modalFooterData}
              </Modal.Footer>
            }
          </Modal>        
        </>
      }
    </>
  );
};

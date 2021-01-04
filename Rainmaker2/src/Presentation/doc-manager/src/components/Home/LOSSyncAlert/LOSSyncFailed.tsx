import React, { useContext } from 'react'
import { Button, Modal } from "react-bootstrap";
import { ErrorIcon } from "../../../shared/Components/Assets/SVG";
import { DocumentActionsType } from '../../../Store/reducers/documentsReducer';
import { Store } from '../../../Store/Store';


export const LOSSyncFailed = ({ retrySync, hideFailedAlert }: any) => {

    const { state, dispatch } = useContext(Store);

    const documents: any = state?.documents;
    const filesToSync: any = documents?.filesToSync.filter((f: any) => f.syncStatus === 'failed');

    let getDocCatFiles: any = () => {
        let docCats: any = [];
        for (const d of filesToSync) {
            if (!docCats.find((dc: any) => dc.docId === d.document.docId)) {
                docCats.push(d.document);
            }
        }

        for (const d of docCats) {
            d.filesFailed = [];
            for (const f of filesToSync) {
                if (f.document.docId === d.docId) {
                    d.filesFailed.push(f.file);
                }
            }

        }
        return docCats;
    }

    getDocCatFiles();

    return (
        <Modal
            backdrop="static"
            keyboard={true}
            className="modal-alert sync-error-alert"
            centered
            show={true}
        >
            <Modal.Header closeButton onClick={hideFailedAlert}>
                <div className="h-wrap" data-testid="sync-alert-Header">
                    <div className="e-icon">
                        {/*<img src={errorIcon} alt="ErrorIcon" />*/}
                        <ErrorIcon />
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
                        {
                            getDocCatFiles()?.map((d: any) => {
                                return (
                                    <li>
                                        <h5>{d.docName}</h5>
                                        {
                                            <ul>
                                                {
                                                    d?.filesFailed?.map((f: any) => {
                                                        return (
                                                            <li>{f.mcuName}</li>
                                                        )
                                                    })
                                                }
                                            </ul>
                                        }
                                    </li>
                                )
                            })
                        }
                        {/*{renderFailedSyncList()}*/}
                    </ul>
                    <p className="text-center">
                        <Button onClick={() => retrySync(filesToSync)} variant="primary">
                            {"Sync again"}
                        </Button>
                    </p>
                </div>
            </Modal.Body>
        </Modal>
    )
}

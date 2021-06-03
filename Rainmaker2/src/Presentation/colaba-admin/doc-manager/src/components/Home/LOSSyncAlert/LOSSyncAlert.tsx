import React, { useContext, useState } from 'react'
import { Store } from '../../../Store/Store';
import sycLOSIcon from '../../../Assets/images/sync-los-icon.svg';
import loadingIcon from '../../../Assets/images/loading.svg';
import { DocumentActionsType } from '../../../Store/reducers/documentsReducer';
import DocumentActions from '../../../Store/actions/DocumentActions';
import { LocalDB } from '../../../Utilities/LocalDB';
import { async } from 'q';
import { FileUpload } from '../../../Utilities/helpers/FileUpload';
import { LOSSyncFailed } from './LOSSyncFailed';

export const LOSSyncAlert = () => {

    const [synching, setSynching] = useState(false);
    const [syncSuccessful, setSyncSuccessful] = useState<boolean | undefined>();
    const { state, dispatch } = useContext(Store);

    const documents: any = state?.documents;
    const uploadFailedDocs: any = documents?.uploadFailedDocs;
    const filesToSync: any = documents?.filesToSync;
    const isSynching: any = documents?.isSynching;
    const syncStatus: any = documents?.syncStatus;
    const importedFileIds:any = documents?.importedFileIds


    const updateFilesBeingSynched = (filesToSync: any, fileInSyncProcess: any, status: any) => {
        let filesUpdated = filesToSync?.map((fs: any) => {
            if (fs.file.id === fileInSyncProcess.file.id) {
                fs.syncStatus = status;
                return fs;
            }
            return fs;
        });
        dispatch({ type: DocumentActionsType.SetFilesToSync, payload: filesUpdated });
    }

    const getFilesFailed = () => filesToSync.filter((f: any) => f.syncStatus === 'failed');

    const startSyncProcess = async (filesToSync: any) => {

        setSynching(true);
        dispatch({ type: DocumentActionsType.SetSyncStarted, payload: true });

        for (const file of filesToSync) {
            let fileToSync: any = {
                LoanApplicationId: Number(LocalDB.getLoanAppliationId()),
                DocumentLoanApplicationId: file?.document?.id,
                RequestId: file?.document?.requestId,
                DocumentId: file?.document?.docId,
                FileId: file?.file.id
            }

            try {
                const res = await DocumentActions.syncFileToLos(fileToSync);
                if (res) {
                    updateFilesBeingSynched(filesToSync, file, 'successful');
                }


            } catch (error) {
                await updateFilesBeingSynched(filesToSync, file, 'failed');
            }

        }

        if (!getFilesFailed()?.length) {
            setSyncSuccessful(true);
            getDocumentItems();
            setTimeout(() => {
                dispatch({ type: DocumentActionsType.SetFilesToSync, payload: [] });
                setSynching(false);
            }, 2000);

            dispatch({ type: DocumentActionsType.SetSyncStarted, payload: false });
        } else {
            setSyncSuccessful(false);
        }

    };


    const hideCompletedSync = () => {
        setSyncSuccessful(false)
    };

    const renderSyncInProcess = () => {
        if (syncSuccessful && !getFilesFailed().length) {
            return renderSyncCompleted();
        }
        return (
            <div className="sync-alert"  >
                <div className="sync-alert-wrap" data-testid="sync-alert">
                    <div className="icon">
                        <img src={sycLOSIcon} alt="syncLosIcon" />
                    </div>
                    <div className="msg">
                        {!synching ? "Are you ready to sync the selected documents?" : "Synchronization in process..."}

                    </div>
                    <div className="btn-wrap">
                        {!synching ?
                            <button
                                data-testid="sync-button"
                                disabled={false}
                                className="btn btn-primary btn-sm"
                                onClick={() => startSyncProcess(filesToSync)}
                            >
                                SYNC
                            </button> :

                            <button
                                disabled={true}
                                className="btn btn-primary btn-sm">
                                <div className="spinning-loader"><img src={loadingIcon} /></div>

                            </button>}
                    </div>
                </div>
            </div>
        )
    }

    const retrySync = async (failedFilesToSync: any) => {
        setSyncSuccessful(undefined);

        let onlyFailedFiles = filesToSync.filter((fs: any) => {
            if (fs.syncStatus = 'failed') {
                fs.syncStatus = 'started';
                return fs;
            }
        });
        await dispatch({
            type: DocumentActionsType.SetFilesToSync, payload: onlyFailedFiles
        });
        await startSyncProcess(failedFilesToSync)
    }


    const renderSyncCompleted = () => {
        return (
            <div className="sync-alert">
                <div className="sync-alert-wrap success">
                    <div className="msg">Synchronization completed</div>
                    <div className="close-wrap" onClick={hideCompletedSync}>
                        <span className="close-btn">
                            <em className="zmdi zmdi-close"></em>
                        </span>
                    </div>
                </div>
            </div>
        )
    }

    const getDocumentItems = async () => {
        let docs = await DocumentActions.getDocumentItems(dispatch, importedFileIds);
        let allDocs: any;
        for (let index = 0; index < uploadFailedDocs.length; index++) {
            allDocs = docs?.map((doc: any) => {
                if (doc.docId === uploadFailedDocs[index].docCategoryId) {
                    doc.files = [...doc.files, uploadFailedDocs[index]]
                }
                return doc
            })
        }
        if (allDocs && allDocs.length) {
            dispatch({ type: DocumentActionsType.SetDocumentItems, payload: allDocs });
        }
    }

    const hideFailedAlert = (e: any) => {
        if (e.target.tagName === 'BUTTON') {
            dispatch({ type: DocumentActionsType.SetFilesToSync, payload: [] });
            dispatch({ type: DocumentActionsType.SetSyncStarted, payload: false });
            getDocumentItems()
        }
    }

    return (
        <div data-testid="los-sync-alert">
            {(getFilesFailed()?.length && syncSuccessful === false) ? <LOSSyncFailed
                fileToSync={getFilesFailed()}
                retrySync={retrySync}
                hideFailedAlert={hideFailedAlert} /> : renderSyncInProcess()}
        </div>
    )
}

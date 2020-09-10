import React, { useState, useEffect, FunctionComponent } from 'react';
import { useHistory } from 'react-router-dom';
import Spinner from 'react-bootstrap/Spinner';

import { NeedList } from '../../../../../Entities/Models/NeedList';
import { NeedListDocuments } from '../../../../../Entities/Models/NeedListDocuments';
import { truncate } from '../../../../../Utils/helpers/TruncateString';
import { LocalDB } from '../../../../../Utils/LocalDB';
import { DocumentStatus } from '../../../../../Entities/Types/Types';
import sycLOSIcon from '../../../../../Assets/images/sync-los-icon.svg';
import syncedIcon from '../../../../../Assets/images/check-icon.svg';
import loadingIcon from '../../../../../Assets/images/loading.svg';
import emptyIcon from './../../../../../Assets/images/empty-icon.svg';

type NeedListProps = {
  needList: NeedList | null | undefined | any;
  deleteDocument: Function;
  sortDocumentTitle: Function;
  documentTitleArrow: string;
  statusTitleArrow: string;
  sortStatusTitle: Function;
  documentSortClick: boolean;
  statusSortClick: boolean;
  deleteRequestSent: boolean;
  isByteProAuto: boolean;
  FilesSyncToLos: Function;
  showConfirmBox: boolean;
  FileSyncToLos: Function;
  postToBytePro: Function;
  synchronizing: boolean;
  syncSuccess: boolean;
  closeSyncCompletedBox: Function;
  syncTitleClass: string;
};

export const NeedListTable: FunctionComponent<NeedListProps> = (props) => {
 const  {
    needList,
    deleteDocument,
    sortDocumentTitle,
    documentTitleArrow,
    statusTitleArrow,
    sortStatusTitle,
    documentSortClick,
    statusSortClick,
    deleteRequestSent,
    isByteProAuto,
    FilesSyncToLos,
    FileSyncToLos,
    showConfirmBox,
    postToBytePro,
    synchronizing,
    syncSuccess,
    closeSyncCompletedBox,
    syncTitleClass
  } = props

  const [confirmDelete, setConfirmDelete] = useState<boolean>(false);
  const [currentItem, setCurrentItem] = useState<NeedList | null>(null);
  const [syncButtonEnabled, setsyncButtonEnabled] = useState(false)
  const history = useHistory();
  
  useEffect(() => {
    if(!needList || needList.length === 0) return 
    
    const fileNotSyncedOrSyncFailed = needList.some((document:NeedList) => document.files.some(uploadedFile => ['sync_error','not_Synced'].includes(uploadedFile.byteProStatusClassName)))

    //Only enable sync button if there is a file with sync failed or it never synced.
    if(fileNotSyncedOrSyncFailed===true){
      setsyncButtonEnabled(true)
    }
  },[syncButtonEnabled,needList])
  
  const renderNeedList = (data: any) => {   
    return data.map((item: NeedList, index: number) => {
      return (
        <div key={index} className="tr row-shadow">
          {renderDocName(item.docName, item.files)}
          {renderStatus(item.status)}
          {renderFile(item.files, item.status, index)}
          {!isByteProAuto && renderSyncToLos(item.files, item.index, )}
          {renderButton(item, index)}
          <div className="td td-options">
            {confirmDelete &&
              currentItem === item &&
              deleteDocAlert(item, index)}
          </div>
        </div>
      );
    });
  };

  const toTitleCase = (str: string) => {
    return str.toLowerCase().replace(/([^a-z])([a-z])(?=[a-z]{2})|^([a-z])/g, function (_, g1, g2, g3) {
      return (typeof g1 === 'undefined') ? g3.toUpperCase() : g1 + g2.toUpperCase();
    });
  }

  const renderDocName = (name: string, data: NeedListDocuments[] | null) => {
    let count = 0;
    if (data) {
      for (let i = 0; i < data?.length; i++) {
        if (data[i].isRead === false) {
          count++;
          break;
        }
      }
    }

    if (count > 0)
      return (
        <div className="td">
          <span className="f-normal" title={toTitleCase(name)}>
            <i className="far fa-file text-primary"></i> <strong>{toTitleCase(name)}</strong>
          </span>
        </div>
      );
    else
      return (
        <div className="td">
          <span className="f-normal" title={toTitleCase(name)}>
            <i className="far fa-file"></i> {toTitleCase(name)}
          </span>
        </div>
      );
  };

  const renderStatus = (status: string) => {
    let cssClass = '';
    switch (status) {
      case 'Pending review':
        cssClass = 'status-bullet pending';
        break;
      case 'Started':
        cssClass = 'status-bullet started';
        break;
      case 'Borrower to do':
        cssClass = 'status-bullet borrower';
        break;
      case 'Completed':
        cssClass = 'status-bullet completed';
        break;
      case 'In draft':
        cssClass = 'status-bullet indraft';
        break;
      default:
        cssClass = 'status-bullet pending';
    }
    return (
      <div className="td">
        <span className={cssClass}></span> {toTitleCase(status)}
      </div>
    );
  };

  const deleteDocAlert = (data: NeedList, index: number) => {
    return (
      <>
        <div>
          <div className="list-remove-alert">
            <span className="list-remove-text">
              Remove this document from Needs List?
            </span>
            <div className="list-remove-options">
              <button
                onClick={() => {
                  deleteDocument(data.id, data.requestId, data.docId);
                  setConfirmDelete(false);
                }}
                className="btn btn-sm btn-secondry"
              >
                Yes
              </button>
              <button
                onClick={() => setConfirmDelete(false)}
                className="btn btn-sm btn-primary"
              >
                No
              </button>
            </div>
          </div>
        </div>
      </>
    );
  };

  const renderButton = (data: NeedList, index: number) => {
    let count = data.files != null ? data.files.length : data.files;
    if (data.status === 'Pending review') {
      return (
        <div className="td options">
          <button
            onClick={() => reviewClickHandler(index)}
            className="btn btn-primary btn-sm"
          >
            Review
          </button>
        </div>
      );
    } else {
      return (
        <div className="td options">
          <button
            onClick={() => detailClickHandler(index)}
            className="btn btn-secondry btn-sm"
          >
            Details
          </button>
          {data.status === 'Borrower to do' && !count ? (
            deleteRequestSent && currentItem === data ? (
              <span className="btnloader">
                <Spinner size="sm" animation="border" role="status">
                  <span className="sr-only">Loading...</span>
                </Spinner>
              </span>
            ) : deleteRequestSent && currentItem != data ? (
              <button
                onClick={() => {
                  setCurrentItem(data);
                  setConfirmDelete(true);
                }}
                className="btn btn-delete btn-sm"
              >
                <em className="zmdi zmdi-close"></em>
              </button>
            ) : (
                  <button
                    onClick={() => {
                      setCurrentItem(data);
                      setConfirmDelete(true);
                    }}
                    className="btn btn-delete btn-sm"
                  >
                    <em className="zmdi zmdi-close"></em>
                  </button>
                )
          ) : (
              ''
            )}
        </div>
      );
    }
  };

  const renderFile = (
    data: NeedListDocuments[] | null,
    status: string,
    documentIndex: number
  ) => {
    if (data === null || data.length === 0) {
      return (
        <div className="td">
          <span className="block-element">No file submitted yet</span>
        </div>
      );
    } else {
      return (
        <div className="td ">
          {data.map((file: NeedListDocuments, index) => {
            const pendingReview = status === DocumentStatus.PENDING_REVIEW;
            const { mcuName, clientName, isRead } = file;

            return (
              <span key={index} className="block-element c-filename">
                {mcuName ? (
                  <React.Fragment>
                    <span
                      title={mcuName}
                      className={
                        isRead === false
                          ? 'block-element-child td-filename filename-by-mcu filename-p'
                          : 'block-element-child td-filename filename-by-mcu'
                      }
                    >
                      <a
                        href="#"
                        onClick={() =>
                          pendingReview
                            ? reviewClickHandler(documentIndex, index)
                            : detailClickHandler(documentIndex, index)
                        }
                      >
                        {truncate(mcuName, 47)}
                      </a>
                    </span>
                  </React.Fragment>
                ) : (
                    <span
                      title={clientName}
                      className={
                        isRead === false
                          ? 'block-element-child td-filename filename-by-mcu filename-p'
                          : 'block-element-child td-filename filename-by-mcu'
                      }
                    >
                      <a
                        href="#"
                        onClick={() =>
                          pendingReview
                            ? reviewClickHandler(documentIndex, index)
                            : detailClickHandler(documentIndex, index)
                        }
                      >
                        {truncate(clientName, 47)}
                      </a>
                    </span>
                  )}
              </span>
            );
          })}
        </div>
      );
    }
  };

  const renderSyncToLos = (data: NeedListDocuments[], index: number) => {

    if (data === null || data.length === 0) {
      return <div className="td"></div>

    } else {
      return (
        <div id={String(index)} className="td">
          {data.map((item: NeedListDocuments) => {

            return (
              <span id={String(item.index)} key={item.index} className="block-element c-filename">
                <a onClick={() => FileSyncToLos(item)}>
                  {item.byteProStatusClassName == "synced" ? <img src={syncedIcon} className={item.byteProStatusClassName} alt="" /> : <em className={"icon-refresh " + item.byteProStatusClassName}></em>
                  }
                </a>{' '}
                {item.byteProStatusClassName == "synced" ? item.byteProStatusText : <span className="txt-stl" onClick={() => FileSyncToLos(item)}>{' '} {item.byteProStatusText}</span>}
              </span>
            );
          })}
        </div>
      );
    }
  };

  const reviewClickHandler = (index: number, fileIndex?: number) => {
    history.push(`/ReviewDocument/${LocalDB.getLoanAppliationId()}`, {
      currentDocumentIndex: index,
      fileIndex: fileIndex ? fileIndex : null,
      documentDetail: false
    });
  };

  const detailClickHandler = (index: number, fileIndex?: number) => {
    history.push(`/ReviewDocument/${LocalDB.getLoanAppliationId()}`, {
      currentDocumentIndex: index,
      fileIndex: fileIndex ? fileIndex : null,
      documentDetail: true
    });
  };

  const renderDocumentTitle = () => {
    if (documentSortClick)
      return (
        <div className="th">
          <a onClick={() => sortDocumentTitle()} href="javascript:;">
            Document{' '}
            <em
              className={
                documentTitleArrow === 'asc'
                  ? 'zmdi zmdi-long-arrow-down table-th-arrow'
                  : 'zmdi zmdi-long-arrow-up table-th-arrow'
              }
            ></em>
          </a>
        </div>
      );
    else
      return (
        <div className="th">
          <a onClick={() => sortDocumentTitle()} href="javascript:;">
            Document
          </a>
        </div>
      );
  };

  const renderStatusTitle = () => {
    if (statusSortClick)
      return (
        <div className="th">
          <a onClick={() => sortStatusTitle()} href="javascript:;">
            Status{' '}
            <em
              className={
                statusTitleArrow === 'asc'
                  ? 'zmdi zmdi-long-arrow-down table-th-arrow'
                  : 'zmdi zmdi-long-arrow-up table-th-arrow'
              }
            ></em>
          </a>
        </div>
      );
    else
      return (
        <div className="th">
          <a onClick={() => sortStatusTitle()} href="javascript:;">
            Status{' '}
          </a>
        </div>
      );
  };

  const renderSyncToLosTitle = () => {
    if (isByteProAuto) {
      return <></>
    } else {
      return (
        <div className="th">
          <a onClick={() => syncButtonEnabled===true ? FilesSyncToLos(syncTitleClass) : {}} >
            <em className={"icon-refresh " + syncTitleClass}></em>
          </a>{' '}
          <span className="txt-stl" onClick={() => syncButtonEnabled===true ? FilesSyncToLos(syncTitleClass) : {}}>sync to LOS</span>
        </div>
      )
    }
  }

  const renderSyncToLosConfirmationBox = () => {
    if (!showConfirmBox && !syncSuccess) {
      return '';
    } else if (showConfirmBox && !syncSuccess) {
      return (
        <div className="sync-alert">
          <div className="sync-alert-wrap">
            <div className="icon"><img src={sycLOSIcon} alt="" /></div>
            <div className="msg">{synchronizing != true ? "Are you ready to sync the selected documents?" : "Synchronization in process..."}</div>
            <div className="btn-wrap">
              <button disabled={synchronizing} onClick={() => synchronizing=== false ? postToBytePro(false) : {}} className="btn btn-primary btn-sm">
                {synchronizing != true
                  ? <>
                    Sync
                    </>
                  :
                  <div className="spinning-loader"><img src={loadingIcon} /></div>
                }
              </button>

            </div>
          </div>


        </div>
      )
    } else if (!showConfirmBox && syncSuccess) {
      return (
        <div className="sync-alert">
          <div className="sync-alert-wrap success">

            <div className="msg">Synchronization completed</div>
            <div onClick={() => closeSyncCompletedBox()} className="close-wrap">
              <span className="close-btn"><em className="zmdi zmdi-close"></em></span>
            </div>
          </div>
        </div>
      )
    }
  }


  if (!needList) {
    return (
      <div className="loader-widget">
        <Spinner animation="border" role="status">
          <span className="sr-only">Loading...</span>
        </Spinner>
      </div>
    );
  }

  return (
    <div className="need-list-table" id="NeedListTable">
      <div className="table-responsive">
        <div className="need-list-table table">
          <div className="tr">
            {renderDocumentTitle()}
            {renderStatusTitle()}
            <div className="th">File Name</div>
            {renderSyncToLosTitle()}

            <div className="th options">&nbsp;</div>
            <div className="th th-options">&nbsp;</div>
          </div>

          {needList && renderNeedList(needList)}

        </div>

        {needList.length === 0 &&
          <div className="no-preview">
            <div>
              <div className="icon-wrap">
                <img src={emptyIcon} alt="" />
              </div>
              <h2>Nothing In Need List</h2>
              <p>No document Request yet</p>
            </div>
          </div>
        }


        {renderSyncToLosConfirmationBox()}
      </div>
    </div>
  );
};

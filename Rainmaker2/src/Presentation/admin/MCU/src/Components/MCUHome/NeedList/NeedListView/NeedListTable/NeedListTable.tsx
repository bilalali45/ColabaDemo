import React, {useState} from 'react';
import {useHistory} from 'react-router-dom';
import {NeedList} from '../../../../../Entities/Models/NeedList';
import {NeedListDocuments} from '../../../../../Entities/Models/NeedListDocuments';
import Spinner from 'react-bootstrap/Spinner';
import {truncate} from '../../../../../Utils/helpers/TruncateString';
import {toTitleCase} from 'rainsoft-js';
import {LocalDB} from '../../../../../Utils/LocalDB';
import {DocumentStatus} from '../../../../../Entities/Types/Types';

type NeedListProps = {
  needList: NeedList | null | undefined;
  deleteDocument: Function;
  sortDocumentTitle: Function;
  documentTitleArrow: string;
  statusTitleArrow: string;
  sortStatusTitle: Function;
  documentSortClick: boolean;
  statusSortClick: boolean;
  deleteRequestSent: boolean;
};

export const NeedListTable = ({
  needList,
  deleteDocument,
  sortDocumentTitle,
  documentTitleArrow,
  statusTitleArrow,
  sortStatusTitle,
  documentSortClick,
  statusSortClick,
  deleteRequestSent
}: NeedListProps) => {
  const [confirmDelete, setConfirmDelete] = useState<boolean>(false);
  const [currentItem, setCurrentItem] = useState<NeedList | null>(null);

  const history = useHistory();
  const renderNeedList = (data: any) => {
    return data.map((item: NeedList, index: number) => {
      return (
        <div key={index} className="tr row-shadow">
          {renderDocName(item.docName, item.files)}
          {renderStatus(item.status)}
          {renderFile(item.files, item.status, index)}
          {renderSyncToLos(item.files)}
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
  const renderDocName = (name: string, data: NeedListDocuments[] | null) => {
    let count = 0;
    console.log('data', data);
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
            <strong>{toTitleCase(name)}</strong>
          </span>
        </div>
      );
    else
      return (
        <div className="td">
          <span className="f-normal" title={toTitleCase(name)}>{toTitleCase(name)}</span>
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
              Are you sure want to delete this Document?
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
            className="btn btn-secondry btn-sm"
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
            className="btn btn-default btn-sm"
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
            const {mcuName, clientName, isRead} = file;

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
                        href="javascript:void"
                        onClick={() =>
                          pendingReview
                            ? reviewClickHandler(documentIndex, index)
                            : detailClickHandler(documentIndex, index)
                        }
                      >
                        {truncate(mcuName, 47)}
                      </a>
                    </span>
                    <small
                      title={clientName}
                      className="block-element-child td-filename filename-by-b"
                    >
                      {truncate(clientName, 47)}
                    </small>
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
                      href="javascript:void"
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

  const renderSyncToLos = (data: NeedListDocuments[]) => {
    if (data === null || data.length === 0) {
      return (
        <div className="td">
          <span className="block-element">
            <a>
              <em className="icon-refresh default"></em>
            </a>
          </span>{' '}
        </div>
      );
    } else {
      return (
        <div className="td">
          {data.map((item: NeedListDocuments) => {
            return (
              <span key={item.id} className="block-element c-filename">
                <a>
                  <em className="icon-refresh default"></em>
                </a>
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
            <div className="th">
              <a href="javascript:;">
                <em className="icon-refresh"></em>
              </a>{' '}
              sync to LOS
            </div>
            <div className="th options">&nbsp;</div>
            <div className="th th-options">&nbsp;</div>
          </div>
          {needList && renderNeedList(needList)}
        </div>
      </div>
    </div>
  );
};

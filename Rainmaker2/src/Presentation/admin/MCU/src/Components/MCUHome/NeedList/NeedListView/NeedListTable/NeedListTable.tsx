import React from 'react'
import { useHistory } from 'react-router-dom'
import { NeedList } from '../../../../../Entities/Models/NeedList'
import { NeedListDocuments } from '../../../../../Entities/Models/NeedListDocuments'
import Spinner from 'react-bootstrap/Spinner';
import { truncate } from '../../../../../Utils/helpers/TruncateString';
import { toTitleCase } from 'rainsoft-js';


type NeedListProps = {
    needList: NeedList | null | undefined;
    deleteDocument: Function;
    sortDocumentTitle: Function;
    documentTitleArrow: string;
    statusTitleArrow: string;
    sortStatusTitle: Function;
    documentSortClick: boolean;
    statusSortClick: boolean;
}

export const NeedListTable = ({ needList, deleteDocument, sortDocumentTitle, documentTitleArrow, statusTitleArrow, sortStatusTitle, documentSortClick, statusSortClick }: NeedListProps) => {
    const history = useHistory()
    const renderNeedList = (data: any) => {
        return data.map((item: NeedList, index: number) => {
            return (
                <div key={index} className="tr row-shadow">
                    {renderDocName(item.docName, item.status)}
                    {renderStatus(item.status)}
                    {renderFile(item.files, item.status)}
                    {renderSyncToLos(item.files)}
                    {renderButton(item, index)}
                </div>
            )


        })
    };
    const renderDocName = (name: string, status: string) => {
        if (status === 'Pending review')
            return <div className="td"><span className="f-normal"><strong>{toTitleCase(name)}</strong></span></div>
        else
            return <div className="td"><span className="f-normal">{toTitleCase(name)}</span></div>
    }
    const renderStatus = (status: string) => {
        let cssClass = ''
        switch (status) {
            case 'Pending review':
                cssClass = 'status-bullet pending'
                break;
            case 'Started':
                cssClass = 'status-bullet started'
                break;
            case 'Borrower to do':
                cssClass = 'status-bullet borrower'
                break;
            case 'Completed':
                cssClass = 'status-bullet completed'
                break;
            default:
                cssClass = 'status-bullet pending'
        }
        return <div className="td"><span className={cssClass}></span> {status}</div>
    }
    const renderButton = (data: NeedList, index: number) => {
        let count = data.files != null ? data.files.length : data.files;
        if (data.status === 'Pending review') {
            return (
                <div className="td">
                    <button onClick={() => reviewClickHandler(index)} className="btn btn-secondry btn-sm">Review</button>
                </div>
            )
        } else {
            return (
                <div className="td">
                    <button onClick={() => detailClickHandler(index)} className="btn btn-default btn-sm">Details</button>
                    {count === 0 || count === null
                        ?
                        <button onClick={() => deleteDocument(data.id, data.requestId, data.docId)} className="btn btn-delete btn-sm"><em className="zmdi zmdi-close"></em></button>
                        :
                        ''
                    }
                </div>
            )
        }
    }

    const renderFile = (data: NeedListDocuments[] | null, status: string) => {
        if (data === null || data.length === 0) {
            return (
                <div className="td">
                    <span className="block-element">No file submitted yet</span>
                </div>
            )
        } else {
            return (
                <div className="td ">
                    {data.map((item: NeedListDocuments) => {
                        return (
                            <span className="block-element c-filename">
                                {item.mcuName ?
                                    <React.Fragment>
                                        <span title={item.clientName} key={item.id} className={ status === 'Pending review' ? "block-element-child td-filename filename-by-mcu filename-p" : "block-element-child td-filename filename-by-mcu"}>{truncate(item.mcuName, 47)}</span>
                                        <small title={item.clientName} key={item.id} className="block-element-child td-filename filename-by-b">{truncate(item.clientName, 47)}</small>
                                    </React.Fragment>
                                    :
                                    <span title={item.clientName} key={item.id} className={ status === 'Pending review' ? "block-element-child td-filename filename-by-mcu filename-p" : "block-element-child td-filename filename-by-mcu"}>{truncate(item.clientName, 47)}</span>
                                }
                            </span>
                        )
                    })}
                </div>
            )
        }
    }

    const renderSyncToLos = (data: NeedListDocuments[]) => {
        if (data === null || data.length === 0) {
            return (
                <div className="td"><span className="block-element"><a><em className="icon-refresh default"></em></a></span> </div>
            )

        } else {
            return (
                <div className="td">
                    {data.map((item: NeedListDocuments) => {
                        return <span key={item.id} className="block-element"><a ><em className="icon-refresh default"></em></a></span>
                    })
                    }
                </div>
            )
        }
    }
    const reviewClickHandler = (index: number) => {
        history.push('/ReviewDocument', {
            documentList: needList,
            currentDocumentIndex: index,
            documentDetail: false
        })
    }

    const detailClickHandler = (index: number) => {
        history.push('/ReviewDocument', {
            documentList: needList,
            currentDocumentIndex: index,
            documentDetail: true
        })
    }

    const renderDocumentTitle = () => {
        if (documentSortClick)
            return <div className="th"><a onClick={() => sortDocumentTitle()} href="javascript:;">Document <em className={documentTitleArrow === 'asc' ? 'zmdi zmdi-long-arrow-down table-th-arrow' : 'zmdi zmdi-long-arrow-up table-th-arrow'}></em></a></div>
        else
            return <div className="th"><a onClick={() => sortDocumentTitle()} href="javascript:;">Document</a></div>
    }
    const renderStatusTitle = () => {
        if (statusSortClick)
            return <div className="th"><a onClick={() => sortStatusTitle()} href="javascript:;">Status <em className={statusTitleArrow === 'asc' ? 'zmdi zmdi-long-arrow-down table-th-arrow' : 'zmdi zmdi-long-arrow-up table-th-arrow'}></em></a></div>
        else
            return <div className="th"><a onClick={() => sortStatusTitle()} href="javascript:;">Status </a></div>
    }


    if (!needList) {
        return (
            <div className="loader-widget">
                <Spinner animation="border" role="status">
                    <span className="sr-only">Loading...</span>
                </Spinner>
            </div>
        )
    }

    return (
        <div className="need-list-table" id="NeedListTable">

            <div className="table-responsive">

                <div className="need-list-table table">

                    <div className="tr">
                        {renderDocumentTitle()}
                        {renderStatusTitle()}
                        <div className="th">File Name</div>
                        <div className="th"><a href="javascript:;"><em className="icon-refresh"></em></a> sync to LOS</div>
                        <div className="th">&nbsp;</div>
                    </div>
                    {needList &&
                        renderNeedList(needList)
                    }
                </div>


            </div>
        </div>
    )
}

import React from 'react'
import { useHistory } from 'react-router-dom'
import { NeedList } from '../../../../../Entities/Models/NeedList'
import { NeedListDocuments } from '../../../../../Entities/Models/NeedListDocuments'
import Spinner from 'react-bootstrap/Spinner';

type NeedListProps = {
    needList: NeedList | null | undefined;
    deleteDocument: Function;
}
export const NeedListTable = ({ needList, deleteDocument }: NeedListProps) => {
    const history = useHistory()

    const renderNeedList = (data: any) => {
        return data.map((item: NeedList, index: number) => {
            return (
                <div className="tr row-shadow">
                    <div className="td"><span className="f-normal"><strong>{item.docName}</strong></span></div>
                    {renderStatus(item.status)}
                    {renderFile(item.files)}
                    {renderSyncToLos(item.files)}
                    {renderButton(item, index)}
                </div>
            )


        })
    };
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
                    <button onClick={() => detailClickHandler(data.docId)} className="btn btn-default btn-sm">Details</button>
                    {count === 0 || count === null
                        ?
                        <button onClick={() => deleteDocument(data.id, data.docId)} className="btn btn-delete btn-sm"><em className="zmdi zmdi-close"></em></button>
                        :
                        ''
                    }
                </div>
            )
        }
    }

    const renderFile = (data: NeedListDocuments[] | null) => {
        if (data === null || data.length === 0) {
            return (
                <div className="td">
                    <span className="block-element">No file submitted yet</span>
                </div>
            )
        } else {
            return (
                <div className="td">
                    {
                        data.map((item: NeedListDocuments) => {
                            return <span className="block-element">{item.clientName}</span>
                        })
                    }
                </div>
            )
        }
    }

    const renderSyncToLos = (data: NeedListDocuments[]) => {
        if (data === null || data.length === 0) {
            return(
                <div className="td"><span className="block-element"><a href=""><em className="icon-refresh default"></em></a></span> </div>
            )

        }else{
            return (
                <div className="td">
                    {data.map((item: NeedListDocuments) => {
                        return <span className="block-element"><a href=""><em className="icon-refresh default"></em></a></span>
                    })
                    }
                </div>
            )
        }    
    }
    const reviewClickHandler = (index: number) => {
        history.push('/ReviewDocument', {
            documentList: needList,
            currentDocumentIndex: index
        })
    }

    const detailClickHandler = (id: string) => {
        console.log('Click on detail Id:', id)
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
                        <div className="th"><a href="javascript:;">Document <em className="zmdi zmdi-long-arrow-down table-th-arrow"></em></a></div>
                        <div className="th"><a href="javascript:;">Status <em className="zmdi zmdi-long-arrow-down table-th-arrow"></em></a></div>
                        <div className="th">File Name</div>
                        <div className="th"><a href=""><em className="icon-refresh"></em></a> sync to LOS</div>
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

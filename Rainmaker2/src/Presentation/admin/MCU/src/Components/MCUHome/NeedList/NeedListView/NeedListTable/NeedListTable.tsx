import React from 'react'
import { NeedListItem } from '../NeedListItem/NeedListItem'
import { NeedList } from '../../../../../Entities/Models/NeedList'
import { NeedListDocuments } from '../../../../../Entities/Models/NeedListDocuments'

type needListProps = {
    needList: NeedList | null | undefined;
    deleteDocument: Function;
}
export const NeedListTable = ({needList, deleteDocument}: needListProps) => {
console.log('needList',needList)

const renderNeedList = (data: any) => {
    return data.map((item:NeedList)=> {
        return (
        <div className="tr row-shadow">
            <div className="td"><span className="f-normal"><strong>{item.docName}</strong></span></div>
                 {renderStatus(item.status)}
                 {renderFile(item.files)}
                 {renderSyncToLos(item.files)}
                 {renderButton(item)}
        </div>
        )
        
       
    })
};
const renderStatus = (status: string) => {
    let cssClass =''
    switch(status){
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
const renderButton = (data: NeedList) => {
    let count = data.files.length;
    if(data.status === 'Pending review'){
        return (
              <div className="td">
                <a onClick={() =>reviewClickHandler(data.docId)} className="btn btn-secondry btn-sm">Review</a>
              </div>
              )         
    }else{
        return (
                <div className="td">
                 <a  onClick={()=>detailClickHandler(data.docId)} className="btn btn-default btn-sm">Details</a>
                   {count === 0 
                   ? 
                   <a  onClick={()=>deleteDocument(data.id, data.docId)} className="btn btn-delete btn-sm"><em className="zmdi zmdi-close"></em></a>
                   :
                   ''
                }         
                </div>
        )                       
    }
}

const renderFile = (data: NeedListDocuments[]) => {
    if(data.length === 0){
      return(
        <div className="td">
            <span className="block-element">No file submitted yet</span> 
        </div>
      )
    }else{
        return(
            <div className="td">
                {
                    data.map((item:NeedListDocuments) => {
                    return <span className="block-element">{item.clientName}</span>  
                                                         })
                }  
            </div>
            )}
   }

const renderSyncToLos = (data: NeedListDocuments[]) => {
    return (
<div className="td">
    {data.map((item:NeedListDocuments) => {
              return <span className="block-element"><a href=""><em className="icon-refresh default"></em></a></span>})
    }
</div>
    )}
const reviewClickHandler = (id: string) => {
console.log('Click on review Id:', id)
} 

const detailClickHandler = (id: string) => {
    console.log('Click on detail Id:', id)
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
                    { needList &&
                    renderNeedList(needList)
                    }
                    {/* <div className="tr row-shadow">
                        <div className="td"><span className="f-normal"><strong>Bank Statement</strong></span></div>
                        <div className="td"><span className="status-bullet pending"></span> Pending</div>
                        <div className="td">
                            <span className="block-element">Bank-statement-Jan-to-Mar-2020-1.jpg</span>
                            <span className="block-element">Bank-statement-Jan-to-Mar-2020-2.jpg</span>
                            <span className="block-element">Bank-statement-Jan-to-Mar-2020-3.jpg</span>
                        </td>
                        <td>
                            <span className="block-element"><a href=""><em className="icon-refresh success"></em></a></span>
                            <span className="block-element"><a href=""><em className="icon-refresh failed"></em></a></span>
                            <span className="block-element"><a href=""><em className="icon-refresh success"></em></a></span>
                        </td>
                        <td>
                            <a href="" className="btn btn-secondry btn-sm">Review</a>
                        </div>
                    </div> */}

                   
                 
                    {/* <div className="tr row-shadow">
                        <div className="td"><span className="f-normal">Bank Deposit Slip</span></div>
                        <div className="td"><span className="status-bullet completed"></span> Completed</div>
                        <div className="td">
                            <span className="block-element">
                                Verification Slip.jpg <br/>
                                <small>Screenshot 2020-06-11 at 8.32.14 PM.pdf</small>
                            </span>
                        </td>
                        <td>
                            <span className="block-element"><a href=""><em className="icon-refresh"></em></a></span>
                        </td>
                        <td>
                            <a href="" className="btn btn-default btn-sm">Details</a>
                            <a href="" className="btn btn-delete btn-sm"><em className="zmdi zmdi-close"></em></a>
                        </div>
                    </div> */}
                    
                </div>

            
            </div>
        </div>
    )
}

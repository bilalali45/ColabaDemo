import React from 'react'

export const NeedListItem = () => {
    return (
        <div className="tr row-shadow">
            <div className="td"><span className="f-normal"><strong>Bank Statement</strong></span></div>
            <div className="td"><span className="status-bullet pending"></span> Pending</div>
            <div className="td">
                <span className="block-element">Bank-statement-Jan-to-Mar-2020-1.jpg</span>
                <span className="block-element">Bank-statement-Jan-to-Mar-2020-2.jpg</span>
                <span className="block-element">Bank-statement-Jan-to-Mar-2020-3.jpg</span>
            </div>
            <div className="td">
                <span className="block-element"><a href=""><em className="icon-refresh success"></em></a></span>
                <span className="block-element"><a href=""><em className="icon-refresh failed"></em></a></span>
                <span className="block-element"><a href=""><em className="icon-refresh success"></em></a></span>
            </div>
            <div className="td">
                <a href="" className="btn btn-secondry btn-sm">Review</a>
            </div>
        </div>
    )
}

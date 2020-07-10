import React from 'react'
import { NeedListItem } from '../NeedListItem/NeedListItem'

export const NeedListTable = () => {
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

                    <NeedListItem/>
                    
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

                    <div className="tr row-shadow">
                        <div className="td"><span className="f-normal"><strong>W-2s</strong></span></div>
                        <div className="td"><span className="status-bullet pending"></span> Pending</div>
                        <div className="td">
                            <span className="block-element">2020-06-11 at 8.32.14 PM.pdf</span>
                        </div>
                        <div className="td">
                            <span className="block-element"><a href=""><em className="icon-refresh success"></em></a></span>
                        </div>
                        <div className="td">
                            <a href="" className="btn btn-secondry btn-sm">Review</a>
                        </div>
                    </div>

                    <div className="tr row-shadow">
                        <div className="td"><span className="f-normal">Personal Tax Returns</span></div>
                        <div className="td"><span className="status-bullet started"></span> Started</div>
                        <div className="td">
                            <span className="block-element">2020-06-11 at 8.32.14 PM.pdf</span>
                        </div>
                        <div className="td">
                            <span className="block-element"><a href=""><em className="icon-refresh success"></em></a></span>
                        </div>
                        <div className="td">
                            <a href="" className="btn btn-default btn-sm">Details</a>
                            <a href="" className="btn btn-delete btn-sm"><em className="zmdi zmdi-close"></em></a>
                        </div>
                    </div>

                    <div className="tr row-shadow">
                        <div className="td"><span className="f-normal">Tax divanscripts</span></div>
                        <div className="td"><span className="status-bullet borrower"></span> Borrower to do</div>
                        <div className="td">
                            <span className="block-element">No file submitted yet</span>
                        </div>
                        <div className="td">
                            <span className="block-element"><a href=""><em className="icon-refresh"></em></a></span>
                        </div>
                        <div className="td">
                            <a href="" className="btn btn-default btn-sm">Details</a>
                            <a href="" className="btn btn-delete btn-sm"><em className="zmdi zmdi-close"></em></a>
                        </div>
                    </div>

                    <div className="tr row-shadow">
                        <div className="td"><span className="f-normal">Tax divanscripts</span></div>
                        <div className="td"><span className="status-bullet borrower"></span> Borrower to do</div>
                        <div className="td">
                            <span className="block-element">No file submitted yet</span>
                        </div>
                        <div className="td">
                            <span className="block-element"><a href=""><em className="icon-refresh"></em></a></span>
                        </div>
                        <div className="td">
                            <a href="" className="btn btn-default btn-sm">Details</a>
                            <a href="" className="btn btn-delete btn-sm"><em className="zmdi zmdi-close"></em></a>
                        </div>
                    </div>

                    <div className="tr row-shadow">
                        <div className="td"><span className="f-normal">Bank Deposit Slip</span></div>
                        <div className="td"><span className="status-bullet completed"></span> Completed</div>
                        <div className="td">
                            <span className="block-element">
                                Verification Slip.jpg <br/>
                                <small>Screenshot 2020-06-11 at 8.32.14 PM.pdf</small>
                            </span>
                        </div>
                        <div className="td">
                            <span className="block-element"><a href=""><em className="icon-refresh"></em></a></span>
                        </div>
                        <div className="td">
                            <a href="" className="btn btn-default btn-sm">Details</a>
                            <a href="" className="btn btn-delete btn-sm"><em className="zmdi zmdi-close"></em></a>
                        </div>
                    </div>
                    
                </div>

            {/* <table className="need-list-table table">
                <thead>
                    <tr>
                        <th><a href="javascript:;">Document <em className="zmdi zmdi-long-arrow-down table-th-arrow"></em></a></th>
                        <th><a href="javascript:;">Status <em className="zmdi zmdi-long-arrow-down table-th-arrow"></em></a></th>
                        <th>File Name</th>
                        <th><a href=""><em className="icon-refresh"></em></a> sync to LOS</th>
                        <th>&nbsp;</th>
                    </tr>
                </thead>
                <tbody>
                    <NeedListItem/>
                    <tr>
                        <td><span className="f-normal"><strong>Bank Statement</strong></span></td>
                        <td><span className="status-bullet pending"></span> Pending</td>
                        <td>
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
                        </td>
                    </tr>
                    <tr>
                        <td><span className="f-normal"><strong>W-2s</strong></span></td>
                        <td><span className="status-bullet pending"></span> Pending</td>
                        <td>
                            <span className="block-element">2020-06-11 at 8.32.14 PM.pdf</span>
                        </td>
                        <td>
                            <span className="block-element"><a href=""><em className="icon-refresh success"></em></a></span>
                        </td>
                        <td>
                            <a href="" className="btn btn-secondry btn-sm">Review</a>
                        </td>
                    </tr>
                    <tr>
                        <td><span className="f-normal">Personal Tax Returns</span></td>
                        <td><span className="status-bullet started"></span> Pending</td>
                        <td>
                            <span className="block-element">2020-06-11 at 8.32.14 PM.pdf</span>
                        </td>
                        <td>
                            <span className="block-element"><a href=""><em className="icon-refresh success"></em></a></span>
                        </td>
                        <td>
                            <a href="" className="btn btn-default btn-sm">Details</a>
                            <a href="" className="btn btn-delete"><em className="zmdi zmdi-close"></em></a>
                        </td>
                    </tr>
                    <tr>
                        <td><span className="f-normal">Tax Transcripts</span></td>
                        <td><span className="status-bullet borrower"></span> Borrower to do</td>
                        <td>
                            <span className="block-element">No file submitted yet</span>
                        </td>
                        <td>
                            <span className="block-element"><a href=""><em className="icon-refresh"></em></a></span>
                        </td>
                        <td>
                            <a href="" className="btn btn-default btn-sm">Details</a>
                            <a href="" className="btn btn-delete"><em className="zmdi zmdi-close"></em></a>
                        </td>
                    </tr>
                    <tr>
                        <td><span className="f-normal">Tax Transcripts</span></td>
                        <td><span className="status-bullet borrower"></span> Borrower to do</td>
                        <td>
                            <span className="block-element">No file submitted yet</span>
                        </td>
                        <td>
                            <span className="block-element"><a href=""><em className="icon-refresh"></em></a></span>
                        </td>
                        <td>
                            <a href="" className="btn btn-default btn-sm">Details</a>
                            <a href="" className="btn btn-delete"><em className="zmdi zmdi-close"></em></a>
                        </td>
                    </tr>
                    <tr>
                        <td><span className="f-normal">Bank Deposit Slip</span></td>
                        <td><span className="status-bullet completed"></span> Completed</td>
                        <td>
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
                            <a href="" className="btn btn-delete"><em className="zmdi zmdi-close"></em></a>
                        </td>
                    </tr>
                </tbody>
            </table> */}
            </div>
        </div>
    )
}

import React from 'react'; 

 export const DocumentSnipet = () => {
     return (
            <div className="document-statement--doc">
                <div className="document-statement--doc---left">
                    <div className="document-statement--doc---input-group">
                        <input type="text" value="Bank-statement-Jan-to-Mar-2020-1.jpg"/>
                        <button className="document-statement--doc-btn-ok"><em className="zmdi zmdi-check"></em></button>
                    </div>
                    <small className="document-statement--doc---detail">By Richard Glenn Randall on Apr 17, 2020 at 4:31 AM</small>
                </div>
                <div className="document-statement--doc--right">
                    <button className="document-statement--doc-btn-cancel"><em className="zmdi zmdi-close"></em></button>
                    <button className="document-statement--doc-btn-edit"><em className="icon-edit"></em></button>
                </div>
            </div>
     )
 }
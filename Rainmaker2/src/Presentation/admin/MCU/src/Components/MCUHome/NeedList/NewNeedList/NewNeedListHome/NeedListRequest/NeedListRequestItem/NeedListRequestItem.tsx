import React, { useState, useContext, useEffect } from 'react'

export const NeedListRequestItem = () => {
    const [toRemoveList, setRemoveList] = useState<boolean>(false);
    return (
         <li>
            <div className="l-wrap">
                {!toRemoveList ?
                    <div className="c-list">
                    Test test 
                            <span className="BTNclose" onClick={() => {}}><i className="zmdi zmdi-close"></i></span>
                    </div>
                    : <>
                        <div className="alert-cancel">
                            <span>Remove this template?</span>
                            <div className="l-remove-actions">
                                <button className="lbtn btn-no" onClick={() => {}}> No</button>
                                <button className="lbtn btn-yes" onClick={() => {}}>Yes</button></div>
                        </div>
                    </>
                }


            </div>
         </li>
    )
}

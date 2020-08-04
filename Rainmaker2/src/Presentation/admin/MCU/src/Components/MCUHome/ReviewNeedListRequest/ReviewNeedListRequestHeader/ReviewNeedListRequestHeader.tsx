import React from 'react'
import { useHistory } from 'react-router-dom'
import { TemplateDocument } from '../../../../Entities/Models/TemplateDocument';

type NewNeedListHeaderType = {
    saveAsDraft: Function;
    showReview: boolean,
    toggleShowReview: Function
}


export const ReviewNeedListRequestHeader = ({saveAsDraft, showReview, toggleShowReview} : NewNeedListHeaderType) => {

    const history = useHistory();

    const closeHandler = () => {
        history.push('/needList');
    }

    return (
        <div id="ReviewNeedListRequestHeader" data-component="ReviewNeedListRequestHeader" className="mcu-panel-header">
           <div className="row">
                <div className="mcu-panel-header--left col-md-8">
                    {showReview && <button onClick={(e) => toggleShowReview(e)} className="btn btn-sm btn-back"><em className="zmdi zmdi-arrow-left"></em> Back</button>}
                </div>

                <div className="mcu-panel-header--right col-md-4">
                    <button onClick={closeHandler} className="btn btn-sm btn-secondary">Close</button>
                    {" "}
                    <button onClick={() => saveAsDraft(true)} className="btn btn-sm btn-primary">Save as Close</button>
                </div>
           </div>
        </div>
    )
}

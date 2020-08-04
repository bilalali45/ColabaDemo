import React from 'react'
import { useHistory } from 'react-router-dom'
import { TemplateDocument } from '../../../../Entities/Models/TemplateDocument';

type NewNeedListHeaderType = {
    saveAsDraft: Function;
}


export const ReviewNeedListRequestHeader = ({saveAsDraft} : NewNeedListHeaderType) => {

    const history = useHistory();

    const backHandler = () => {
        history.push('/newNeedList');
    }

    const closeHandler = () => {
        history.push('/needList');
    }

    return (
        <div id="ReviewNeedListRequestHeader" data-component="ReviewNeedListRequestHeader" className="mcu-panel-header">
           <div className="row">
                <div className="mcu-panel-header--left col-md-8">
                    {!window.location.pathname.includes('/newNeedList') && <button onClick={backHandler} className="btn btn-sm btn-back"><em className="zmdi zmdi-arrow-left"></em> Back</button>}
                </div>

                <div className="mcu-panel-header--right col-md-4">
                    <button onClick={closeHandler} className="btn btn-sm btn-secondary">Close</button>
                    {" "}
                    <button onClick={() => saveAsDraft()} className="btn btn-sm btn-primary">Save as Close</button>
                </div>
           </div>
        </div>
    )
}

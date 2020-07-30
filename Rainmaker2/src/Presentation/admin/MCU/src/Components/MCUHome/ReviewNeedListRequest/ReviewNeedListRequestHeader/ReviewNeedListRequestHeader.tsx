import React from 'react'

export const ReviewNeedListRequestHeader = () => {
    return (
        <div id="ReviewNeedListRequestHeader" data-component="ReviewNeedListRequestHeader" className="mcu-panel-header">
           <div className="row">
                <div className="mcu-panel-header--left col-md-8">
                    <button className="btn btn-sm btn-back"><em className="zmdi zmdi-arrow-left"></em> Back</button>
                </div>

                <div className="mcu-panel-header--right col-md-4">
                    <button className="btn btn-sm btn-secondary">Close</button>
                    {" "}
                    <button className="btn btn-sm btn-primary">Save as Close</button>
                </div>
           </div>
        </div>
    )
}

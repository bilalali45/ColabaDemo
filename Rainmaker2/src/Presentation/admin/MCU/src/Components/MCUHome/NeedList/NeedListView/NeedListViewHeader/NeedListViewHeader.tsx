import React from 'react'

export const NeedListViewHeader = () => {
    return (
        <div className="need-list-view-header" id="NeedListViewHeader" data-component="NeedListViewHeader">
            <div className="need-list-view-header--left">
                <span className="h2">Needs List</span> 
                <div className="btn-group">
                    <button className="btn btn-primary btn-sm dropdown-toggle">Add <span className="rotate-plus"></span></button>
                    <div className="dropdown-menu">
                        <a className="dropdown-item" href="#">Action</a>
                        <a className="dropdown-item" href="#">Another action</a>
                        <a className="dropdown-item" href="#">Something else here</a>
                        <div className="dropdown-divider"></div>
                        <a className="dropdown-item" href="#">Separated link</a>
                    </div> 
                </div>
            </div>
            <div className="need-list-view-header--right">
                <label><strong>All</strong></label>
                &nbsp;&nbsp;&nbsp;
                <label className="switch">
                    <input type="checkbox" id="toggle" />
                    <span className="slider round"></span>
                </label>
                &nbsp;&nbsp;&nbsp;
                <label><strong>Pending</strong></label>
            </div>            
        </div>
    )
}

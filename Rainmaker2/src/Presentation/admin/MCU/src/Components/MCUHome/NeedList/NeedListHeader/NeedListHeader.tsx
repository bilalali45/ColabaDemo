import React from 'react'

export const NeedListHeader = () => {
    return (
        <div className="need-list-header">
            <div className="need-list-header--left">
                <a href="#" className="btn btn-back"><em className="zmdi zmdi-arrow-left"></em> Back</a>
            </div>
            <div className="need-list-header--right">
                <a href="" className="btn btn-secondry"><em className="icon-record"></em> Manage Template</a> <a href="" className="btn btn-primary"><em className="icon-edit"></em> Post to Byte Pro</a>
            </div>
        </div>
    )
}

import React from 'react'
import { useHistory } from 'react-router-dom'

export const NeedListHeader = () => {
    const history = useHistory();

const redirectToTemplate= () => {
    
    history.push('/templateManager')
}


    return (
        <div className="need-list-header">
            <div className="need-list-header--left">
                <a href="#" className="btn btn-back"><em className="zmdi zmdi-arrow-left"></em> Back</a>
            </div>
            <div className="need-list-header--right">
                <button onClick={redirectToTemplate} className="btn btn-secondry"><em className="icon-record"></em> Manage Template</button> 
                <button className="btn btn-primary" style={{pointerEvents:'none'}}><em className="icon-edit"></em> Post to Byte Pro</button>
            </div>
        </div>
    )
}

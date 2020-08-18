import React from 'react';
import { SVGNoBell } from '../SVGIcons';

interface  propsData{
    handleClearVerification: Function
}

export const AlertForRemove = ({handleClearVerification}:propsData) => {
    return (
        <div className="notify-alert-msg">
            <div className="notify-alert-msg--wrap">
                <SVGNoBell/>
                <p>Are you sure you want to remove all notifications?</p>
                <p>
                    <button onClick={(e)=> handleClearVerification(false)} className="btn-notify secondry">No</button>
                    <button onClick={(e)=> handleClearVerification(true)} className="btn-notify primary">Yes</button>
                </p>
            </div>
        </div>
    )
}

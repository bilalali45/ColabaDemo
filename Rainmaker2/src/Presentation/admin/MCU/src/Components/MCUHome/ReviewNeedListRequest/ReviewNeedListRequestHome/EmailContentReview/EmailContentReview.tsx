import React from 'react'

export const EmailContentReview = () => {
    return (
        <div className="mcu-panel-body--content">
            <div className="mcu-panel-body padding">
                <h2 className="h2">Review email to Richard Glenn Randall</h2>
                <p>If you'd like, you can customize this email.</p>

                <textarea name="" id="" className="form-control" rows={10}></textarea>

            </div>

            <footer className="mcu-panel-footer text-right">
                <button className="btn btn-primary">Send Request</button>
            </footer>
        </div>
    )
}

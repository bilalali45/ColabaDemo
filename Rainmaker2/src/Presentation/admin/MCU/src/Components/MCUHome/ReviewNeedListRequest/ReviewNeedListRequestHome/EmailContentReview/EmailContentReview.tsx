import React from 'react'

export const EmailContentReview = () => {

    const text = () => {
        return (`Hi Richard Glenn Randall,
As we discussed, I’m adding addition |
To continue your application, we need some more information.

Page 5 0f 5 Case checking account
Financial statements

Complete these items as soon as possible so we can continue reviewing your application.

Thanks & Regards,
Jonny Leo`)
    }

    return (
        <div className="mcu-panel-body--content">
            <div className="mcu-panel-body padding">
                <h2 className="h2">Review email to Richard Glenn Randall</h2>
                <p>If you'd like, you can customize this email.</p>

                <textarea name="" id="" value={text()} className="form-control" rows={17}></textarea>

            </div>

            <footer className="mcu-panel-footer text-right">
                <button className="btn btn-primary">Send Request</button>
            </footer>
        </div>
    )
}

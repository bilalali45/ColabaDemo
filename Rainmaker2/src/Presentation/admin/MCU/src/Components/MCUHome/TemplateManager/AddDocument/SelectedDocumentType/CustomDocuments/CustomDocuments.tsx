import React from 'react'

export const CustomDocuments = () => {
    return (
        <div className="add-custom-doc">
            <div className="title-wrap"><h3>Add Custom Document</h3></div>
            <div className="input-wrap">
                <input type="name" placeholder="Type document name" />
                <button className="btn btn-primary btn-sm">Add</button>
            </div>
        </div>
    )
}

import React, { useState, ChangeEvent } from 'react'

type CustomDocumentsType = {
    addDocToTemplate: Function
}

export const CustomDocuments = ({ addDocToTemplate }: CustomDocumentsType) => {

    const [docName, setDocName] = useState<String>();

    const hanldeChange = ({ target: { value } }: ChangeEvent<HTMLInputElement>) => {
        setDocName(value);
    }

    const addDoc = () => {
        addDocToTemplate(docName, 'docName')
    }

    return (
        <div className="add-custom-doc">
            <div className="title-wrap"><h3>Add Custom Document</h3></div>
            <div className="input-wrap">
                <input
                    onChange={hanldeChange}
                    type="name" placeholder="Type document name" />
                <button onClick={addDoc} className="btn btn-primary btn-sm">Add</button>
            </div>
        </div>
    )
}

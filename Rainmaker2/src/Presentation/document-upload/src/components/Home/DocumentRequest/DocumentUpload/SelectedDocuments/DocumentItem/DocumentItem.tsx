import React, { ChangeEvent } from 'react'

type DocumentItemType = {
    file: File,
    viewDocument: Function,
    changeName: Function,
}

export const DocumentItem = ({file, viewDocument, changeName }: DocumentItemType) => {

    return (
        <div>
            <p>{file.name}</p>
            <button onClick={() => viewDocument(file)}>View</button>
            <input type="text" onChange={(e) => changeName(e.target.value)}/>
        </div>
    )
}

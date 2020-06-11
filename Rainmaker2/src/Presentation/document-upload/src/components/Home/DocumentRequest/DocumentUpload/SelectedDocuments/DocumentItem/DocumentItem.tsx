import React, { ChangeEvent } from 'react'
import { FileSelected } from '../../DocumentUpload'

type DocumentItemType = {
    file: FileSelected,
    viewDocument: Function,
    changeName: Function,
}

export const DocumentItem = ({file, viewDocument, changeName }: DocumentItemType) => {

    return (
        <div>
            <p>{file.name}</p>
            <button onClick={() => viewDocument(file)}>View</button>
            <input type="text" onChange={(e) => changeName(file, e.target.value)}/>
        </div>
    )
}

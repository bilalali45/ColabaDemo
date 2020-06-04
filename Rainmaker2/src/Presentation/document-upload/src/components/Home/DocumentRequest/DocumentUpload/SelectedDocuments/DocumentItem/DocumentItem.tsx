import React, { ChangeEvent } from 'react'

type DocumentItemType = {
    file: File,
    viewDocument: Function
}

export const DocumentItem = ({file, viewDocument }: DocumentItemType) => {

    return (
        <div>
            <p>{file.name}</p>
            <button onClick={() => viewDocument(file)}>View</button>
        </div>
    )
}

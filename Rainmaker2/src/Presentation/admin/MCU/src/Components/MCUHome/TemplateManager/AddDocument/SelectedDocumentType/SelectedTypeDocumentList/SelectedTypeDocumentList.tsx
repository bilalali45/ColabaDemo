import React, { ChangeEvent } from 'react'
import { SelectedDocumentTypeList } from '../SelectedDocumentTypeList/SelectedDocumentTypeList'
import { Document } from '../../../../../../Entities/Models/Document'
import { Template } from '../../../../../../Entities/Models/Template'
import { CategoryDocument } from '../../../../../../Entities/Models/CategoryDocument'
import SearchIcon from '../../../../../../Assets/images/search-icon.svg'

type SelectedTypeType = {
    documentList: Document[],
    selectedCatDocs: CategoryDocument,
    addNewDoc: Function
}

export const SelectedTypeDocumentList = ({ documentList,selectedCatDocs, addNewDoc}: SelectedTypeType) => {
    
    const handleSearch = (e: ChangeEvent<HTMLInputElement>) => {
        
    }
    
    return (
        <div>
            <div className="s-wrap">
            <h4>{selectedCatDocs?.catName}</h4>
            </div>
            <SelectedDocumentTypeList
                documentList={documentList}
                addNewDoc={addNewDoc}
            />
        </div>
    )
}

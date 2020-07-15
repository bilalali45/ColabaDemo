import React, { ChangeEvent } from 'react'
import { CommonDocumentSearch } from './CommonDocumentSearch/CommonDocumentSearch'
import { SelectedDocumentTypeList } from '../SelectedDocumentTypeList/SelectedDocumentTypeList'
import { Document } from '../../../../../../Entities/Models/Document'
import { Template } from '../../../../../../Entities/Models/Template'
import { CategoryDocument } from '../../../../../../Entities/Models/CategoryDocument'
import SearchIcon from '../../../../../../Assets/images/search-icon.svg'

type SelectedTypeType = {
    selectedCatDocs: CategoryDocument,
    addNewDoc: Function,
}

export const CommonDocuments = ({ selectedCatDocs, addNewDoc }: SelectedTypeType) => {
    
    const handleSearch = (e: ChangeEvent<HTMLInputElement>) => {
        
    }
    
    return (
        <div>
            <div className="s-wrap">
                <input onChange={handleSearch} type="name" placeholder="Enter follow up name..." />
                <div className="s-icon"><img src={SearchIcon} alt="" /></div>
            </div>

            <div className="b-title"><h4>{selectedCatDocs.catName}</h4></div>
            <SelectedDocumentTypeList
                documentList={selectedCatDocs?.documents}
                addNewDoc={addNewDoc}
            />
        </div>
    )
}

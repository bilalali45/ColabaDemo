import React, { ChangeEvent, useState } from 'react'
import { CommonDocumentSearch } from './CommonDocumentSearch/CommonDocumentSearch'
import { SelectedDocumentTypeList } from '../SelectedDocumentTypeList/SelectedDocumentTypeList'
import { Document } from '../../../../../../Entities/Models/Document'
import { Template } from '../../../../../../Entities/Models/Template'
import { CategoryDocument } from '../../../../../../Entities/Models/CategoryDocument'

type SelectedTypeType = {
    selectedCatDocs: CategoryDocument,
    addNewDoc: Function
}

export const CommonDocuments = ({ selectedCatDocs, addNewDoc }: SelectedTypeType) => {
    const [selectedCachedDoc, setSelectedCachedDoc] = useState<CategoryDocument>(selectedCatDocs);
    const handleSearch = ({ target: { value } }: ChangeEvent<HTMLInputElement>) => {

        setSelectedCachedDoc((pre: CategoryDocument) => {
            let results = selectedCatDocs?.documents?.filter((cd: Document) => {
                if (cd.docType.toLowerCase().includes(value.toLowerCase())) {
                    return cd;
                }
            })
            return {
                
                    ...selectedCachedDoc,
                    documents: results
            }
        })
    }

    return (
        <div>
            <div className="s-wrap">
                <input onChange={handleSearch} type="name" placeholder="Enter follow up name..." />
            </div>
            <div className="b-title"><h4>{selectedCachedDoc.catName}</h4></div>
            <SelectedDocumentTypeList
                documentList={selectedCachedDoc?.documents}
                addNewDoc={addNewDoc}
            />
        </div>
    )
}

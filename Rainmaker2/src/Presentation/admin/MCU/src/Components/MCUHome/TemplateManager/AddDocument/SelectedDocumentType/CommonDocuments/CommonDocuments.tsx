import React, { ChangeEvent, useState } from 'react'
import { CommonDocumentSearch } from './CommonDocumentSearch/CommonDocumentSearch'
import { SelectedDocumentTypeList } from '../SelectedDocumentTypeList/SelectedDocumentTypeList'
import { Document } from '../../../../../../Entities/Models/Document'
import { Template } from '../../../../../../Entities/Models/Template'
import { CategoryDocument } from '../../../../../../Entities/Models/CategoryDocument'
import SearchIcon from '../../../../../../Assets/images/search-icon.svg'

type SelectedTypeType = {
    setVisible: Function,
    selectedCatDocs: CategoryDocument,
    addNewDoc: Function
}

export const CommonDocuments = ({ selectedCatDocs, addNewDoc, setVisible }: SelectedTypeType) => {

    const [selectedCachedDoc, setSelectedCachedDoc] = useState<CategoryDocument>(selectedCatDocs);
    const [isSearched, setSearched] = useState<boolean>(false);
    const [term, setTerm] = useState<string>();
    
    const handleSearch = ({ target: { value } }: ChangeEvent<HTMLInputElement>) => {
        setTerm(value);
        setSelectedCachedDoc((pre: CategoryDocument) => {
            let results = selectedCatDocs?.documents?.filter((cd: Document) => {
                if (cd.docType.toLowerCase().includes(value.toLowerCase())) {
                    setSearched(false)
                    return cd;
                }
                else{
                    setSearched(true)
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
                <div className="s-icon"><img src={SearchIcon} alt="" /></div>
            </div>

            <div className="b-title"><h4>

                {isSearched?"Search Result":selectedCatDocs.catName}

            </h4></div>
            
            <SelectedDocumentTypeList
                term={term}
                setVisible={setVisible}
                documentList={selectedCachedDoc?.documents}
                addNewDoc={addNewDoc}
            />
        </div>
    )
}

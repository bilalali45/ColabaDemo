import React, { ChangeEvent } from 'react'
import { SelectedDocumentTypeList } from '../SelectedDocumentTypeList/SelectedDocumentTypeList'
import { Document } from '../../../../../../Entities/Models/Document'
import { Template } from '../../../../../../Entities/Models/Template'
import { CategoryDocument } from '../../../../../../Entities/Models/CategoryDocument'
import SearchIcon from '../../../../../../Assets/images/search-icon.svg'
import { CustomDocuments } from '../CustomDocuments/CustomDocuments'

type SelectedTypeType = {
    setVisible: Function,
    documentList: Document[],
    selectedCatDocs: CategoryDocument,
    addNewDoc: Function
}

export const SelectedTypeDocumentList = ({ documentList, selectedCatDocs, addNewDoc, setVisible }: SelectedTypeType) => {

    const handleSearch = (e: ChangeEvent<HTMLInputElement>) => {

    }

    return (
        <div>
            <div className="s-wrap">
                <h4>{selectedCatDocs?.catName}</h4>
            </div>
            <SelectedDocumentTypeList
                setVisible={setVisible}
                documentList={documentList}
                addNewDoc={addNewDoc}
            />
            {selectedCatDocs?.catName === 'Other' && <CustomDocuments
                setVisible={setVisible}
                addDocToTemplate={addNewDoc}
            />}
        </div>
    )
}

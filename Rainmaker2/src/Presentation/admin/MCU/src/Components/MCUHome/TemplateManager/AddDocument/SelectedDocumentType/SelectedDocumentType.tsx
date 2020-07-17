import React from 'react'
import { CommonDocuments } from './CommonDocuments/CommonDocuments'
import { SelectedTypeDocumentList } from './SelectedTypeDocumentList/SelectedTypeDocumentList'
import { SelectedDocumentTypeList } from './SelectedDocumentTypeList/SelectedDocumentTypeList'
import { CustomDocuments } from './CustomDocuments/CustomDocuments'
import { Document } from '../../../../../Entities/Models/Document'
import { Template } from '../../../../../Entities/Models/Template'
import { CategoryDocument } from '../../../../../Entities/Models/CategoryDocument'

type SelectedTypeType = {
    selectedCatDocs: CategoryDocument,
    addNewDoc: Function,
    setVisible: Function
}

export const SelectedType = ({ selectedCatDocs, addNewDoc, setVisible }: SelectedTypeType) => {
    const renderCurrentlySelected = () => {

        if (selectedCatDocs?.catId === 'all') {
            return <CommonDocuments
                setVisible={setVisible}
                selectedCatDocs={selectedCatDocs}
                addNewDoc={addNewDoc} />

        } else if (selectedCatDocs?.catId === 'other') {
            return <CustomDocuments
                setVisible={setVisible}
                addDocToTemplate={addNewDoc} />

        } else {

            return <SelectedTypeDocumentList
                setVisible={setVisible}
                documentList={selectedCatDocs?.documents}
                selectedCatDocs={selectedCatDocs}
                addNewDoc={addNewDoc}
            />
            // return <SelectedDocumentTypeList
            //     documentList={selectedCatDocs?.documents}
            //     addNewDoc={addNewDoc}
            // />
        }
    }

    return (
        <div className="pop-detail-doc">
            <div className="pop-detail-doc--body">
                {
                    renderCurrentlySelected()
                }
            </div>
        </div>
    )



    // return (
    //     <div>
    //         {/* <CommonDocuments/>
    //         <CustomDocuments/> */}
    //         <SelectedDocumentTypeList
    //            documentList={documentList}
    //            addNewDoc={addNewDoc} />
    //     </div>
    // )
}

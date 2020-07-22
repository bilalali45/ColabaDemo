import React, { useState } from 'react'
import { CategoryDocument } from '../../../../../Entities/Models/CategoryDocument'

type DocumentTypesType = {
    currentCategoryDocuments: CategoryDocument
    changeCurrentDocType: Function
    documentTypeList: CategoryDocument[]
}

export const DocumentTypes = ({ documentTypeList, changeCurrentDocType, currentCategoryDocuments }: DocumentTypesType) => {

    // const [showingAll, setshowingAll] = useState<boolean>(false);


    return (
        <div className="list-doc-cat">
            <div key={'all'} className={`listAll ${currentCategoryDocuments?.catId === 'all' ? 'active' : ''} `} onClick={() => changeCurrentDocType('all')}>
                All
            </div>
            <ul className="ul-list-doc-cat">
                {
                    documentTypeList?.map((p: CategoryDocument) => {
                        return (
                            <li key={p.catId} className={currentCategoryDocuments?.catId === p?.catId ? 'active' : ''} onClick={() => changeCurrentDocType(p?.catId)}>{p?.catName}</li>
                        )
                    })
                }
            </ul>

        </div>
    )
}

import React, { useState } from 'react'
import { CategoryDocument } from '../../../../../Entities/Models/CategoryDocument'

type DocumentTypesType = {
    changeCurrentDocType: Function
    documentTypeList: CategoryDocument[]
}

export const DocumentTypes = ({ documentTypeList, changeCurrentDocType }: DocumentTypesType) => {

    // const [showingAll, setshowingAll] = useState<boolean>(false);


    return (
        <div className="list-doc-cat">
            <div className="listAll" onClick={() => changeCurrentDocType('all')}>
                All
            </div>
            <ul>
                {
                    documentTypeList?.map((p: CategoryDocument) => {
                        return (
                            <li onClick={() => changeCurrentDocType(p?.catId)}>{p?.catName}</li>
                        )
                    })
                }
            </ul>
            <div className="listOther" onClick={() => changeCurrentDocType('other')}>
                Other
            </div>
        </div>
    )
}

import React from 'react'
import { CategoryDocument } from '../../../../../Entities/Models/CategoryDocument'

type DocumentTypesType = {
    documentTypeList: CategoryDocument[]
}

export const DocumentTypes = ({documentTypeList} : DocumentTypesType) => {
    return (
        <div>
            <ul>
                {
                    documentTypeList?.map(dt => {
                        return (
                        <li>{dt.catName}</li>
                        )
                    })
                }
            </ul>
        </div>
    )
}

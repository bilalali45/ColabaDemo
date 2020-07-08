import React from 'react'
import { CommonDocumentSearch } from './CommonDocumentSearch/CommonDocumentSearch'
import { CommonDocumentSearchResults } from './CommonDocumentSearchResults/CommonDocumentSearchResults'
import { SelectedTypeDocumentList } from './SelectedTypeDocumentList/SelectedTypeDocumentList'

export const CommonDocuments = () => {
    return (
        <div>
            <h1>CommonDocuments</h1>
            <CommonDocumentSearch/>
            <CommonDocumentSearchResults/>
            <SelectedTypeDocumentList/>
        </div>
    )
}

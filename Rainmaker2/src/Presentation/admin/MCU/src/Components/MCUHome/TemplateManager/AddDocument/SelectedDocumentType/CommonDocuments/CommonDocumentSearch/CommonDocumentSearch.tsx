import React from 'react'
import { CommonDocumentSearchResults } from '../CommonDocumentSearchResults/CommonDocumentSearchResults'

export const CommonDocumentSearch = () => {
    return (
        <div>
            <div className="s-wrap">
                <input type="name" placeholder="Enter follow up name..." />
            </div>
            <CommonDocumentSearchResults />
        </div>
    )
}

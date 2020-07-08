import React from 'react'
import { NeedListViewHeader } from './NeedListViewHeader/NeedListViewHeader'
import { NeedListTable } from './NeedListTable/NeedListTable'

export const NeedListView = () => {
    return (
        <div className="need-list-view">
            <NeedListViewHeader/>
            <NeedListTable/>
        </div>
    )
}

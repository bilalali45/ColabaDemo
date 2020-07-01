import React from 'react'
import { NeedListViewHeader } from './NeedListViewHeader/NeedListViewHeader'
import { NeedListTable } from './NeedListTable/NeedListTable'

export const NeedListView = () => {
    return (
        <div>
            <h1>NeedListView</h1>
            <NeedListViewHeader/>
            <NeedListTable/>
        </div>
    )
}

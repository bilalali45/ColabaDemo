import React from 'react'
import { LoanSnapshot } from './LoanSnapshot/LoanSnapshot'
import { Navigation } from './Navigation/Navigation'
import { NeedListHeader } from './NeedListHeader/NeedListHeader'
import { NeedListView } from './NeedListView/NeedListView'

export const NeedList = () => {
    return (
        <div>
            <h1>NeedList</h1>
            <LoanSnapshot/>
            <Navigation/>
            <NeedListHeader/>
            <NeedListView/>
        </div>
    )
}

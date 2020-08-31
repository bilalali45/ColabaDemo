import React from 'react'
import { LoanSnapshot } from './LoanSnapshot/LoanSnapshot'
import { Navigation } from './Navigation/Navigation'
import { NeedListHeader } from './NeedListHeader/NeedListHeader'
import { NeedListView } from './NeedListView/NeedListView'

export const NeedList = () => {
    return (
        <div>
            <LoanSnapshot/>
            {/* <Navigation/> */}
            <NeedListHeader/>

            <div className="container-mcu">
                <div className="block padding">                    
                    <NeedListView/>
                </div>                
            </div>            
        </div>
    )
}

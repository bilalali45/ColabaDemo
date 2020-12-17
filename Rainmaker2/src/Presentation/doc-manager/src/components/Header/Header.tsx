import React from 'react'
import { LocalDB } from '../../Utilities/LocalDB'

export const Header = () => {
    let loanApplicationId = LocalDB.getLoanAppliationId()
    return (
        <header>
         <div className="dm-h-wrap">
             <a className="dm-h-backBtn" href={`/documentmanagement/${loanApplicationId}`}><i className="zmdi zmdi-arrow-left"></i> <span>Back</span></a>
         </div>
        </header>
    )
}

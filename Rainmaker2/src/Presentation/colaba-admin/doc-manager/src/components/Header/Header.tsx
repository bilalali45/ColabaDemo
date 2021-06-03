import React from 'react'
import { LocalDB } from '../../Utilities/LocalDB'
import { useCookies } from 'react-cookie';

export const Header = () => {
    let loanApplicationId = LocalDB.getLoanAppliationId()
    const [rainmakerRefreshCookie] = useCookies(['Rainmaker2RefreshToken']);
    return (
        <header>
         <div className="dm-h-wrap">
             <a data-testid="back-btn" className="dm-h-backBtn" href={`/documentmanagement/${loanApplicationId}?key=${encodeURIComponent(rainmakerRefreshCookie["Rainmaker2RefreshToken"])}`}><i className="zmdi zmdi-arrow-left"></i> <span >Back</span></a>
         </div>
        </header>
    )
}

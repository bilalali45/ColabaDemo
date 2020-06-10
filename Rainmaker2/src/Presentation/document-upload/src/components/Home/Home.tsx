import React, { useEffect, useState } from 'react'
import { Switch, Route, useLocation, useHistory } from 'react-router-dom'
import { Activity } from './Activity/Activity'
import { DocumentRequest } from './DocumentRequest/DocumentRequest'
import { UploadedDocuments } from './UploadedDocuments/UploadedDocuments'

import { Http } from '../../services/http/Http'
import { Auth } from '../../services/auth/Auth'
import { Store } from '../../store/store'

import ActivityHeader from './AcitivityHeader/ActivityHeader'
import { LoanApplication } from '../../entities/Models/LoanApplication'
import { UserActions } from '../../store/actions/UserActions'
import ImageAssets from '../../utils/image_assets/ImageAssets';
//import Footer from '../../shared/Components/Footer/Footer'
import { RainsoftRcHeader, RainsoftRcFooter } from 'rainsoft-rc'
const httpClient = new Http();

export const Home = () => {
    const location = useLocation();
    const history = useHistory();

    const [authenticated, setAuthenticated] = useState<boolean>(false);
    const gotoDashboardHandler = () => {
        console.log('gotoDashboardHandler')
    }
    const changePasswordHandler = () => {
        console.log('changePasswordHandler')
    }
    const signOutHandler = () => {
        console.log('gotoDashboardHandler')
    }
    const headerDropdowmMenu = [
        { name: 'Dashboard', callback: gotoDashboardHandler },
        { name: 'Change Password', callback: changePasswordHandler },
        { name: 'Sign Out', callback: signOutHandler }
    ]

    useEffect(() => {
        console.log(LoanApplication.formatAmountByCountry(40008094000809)?.BRL());
        console.log('in here!!!', location);
        if (!Auth.checkAuth()) {
            history.push('/loading');
        }
        if (location.pathname === '/') {
            history.push('/home/activity');
        }
    }, [])

    httpClient.get('/home');

    return (
        <div>

            <RainsoftRcHeader
                logoSrc={ImageAssets.header.logoheader}
                displayName={'Jehangir Babul'}
                displayNameOnClick={gotoDashboardHandler}
                options={headerDropdowmMenu}
            />
            <ActivityHeader />
            <Switch>
                <Route path="/home/activity" component={Activity} />
                <Route path="/home/documentsRequest" component={DocumentRequest} />
                <Route path="/home/uploadedDocuments" component={UploadedDocuments} />
            </Switch>
            <RainsoftRcFooter
                title={'Texas Trust Home Loans'}
                streetName={'4100 Spring Valley Rd. Suite 770'}
                address={'Dallas, Texas 75244'}
                phoneOne={'(888) 971-1425'}
                phoneTwo={'(214) 245-3929'}
                contentOne={'Copyright 2002 – 2019. All rights reserved. American Heritage Capital, LP. NMLS 277676.  NMLS Consumer Access Site.  Equal Housing Lender. Portions licensed under U.S. Patent Numbers 7,366,694 and 7,680,728.'}
                contentTwo={'Texas Trust Home Loans is a direct, up-front mortgage lender offering up-to-date mortgage rates. Our loan programs include: Conventional, FHA, Fixed Rate and Adjustable Rate Loans. We are committed to delivering the most accurate and competitive mortgage interest rate and closing costs.'}
                nmlUrl ={'http://www.nmlsconsumeraccess.org/Home.aspx/SubSearch?searchText=277676'}
                nmlLogoSrc={ImageAssets.footer.nmlsLogo}
            />
        </div>
    )
}

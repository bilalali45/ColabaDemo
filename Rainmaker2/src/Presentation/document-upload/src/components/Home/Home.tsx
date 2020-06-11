import React, { useEffect, useState } from 'react'
import { Switch, Route, useLocation, useHistory } from 'react-router-dom'
import { Activity } from './Activity/Activity'
import { UploadedDocuments } from './UploadedDocuments/UploadedDocuments'

import { Http } from '../../services/http/Http'
import { Auth } from '../../services/auth/Auth'
import { Store } from '../../store/store'

import ActivityHeader from './AcitivityHeader/ActivityHeader'
import { LoanApplication } from '../../entities/Models/LoanApplication'
import { UserActions } from '../../store/actions/UserActions'
import {RainsoftRcHeader, RainsoftRcFooter} from 'rainsoft-rc';
import { DocumentStatus } from './Activity/DocumentStatus/DocumentStatus'
import ImageAssets from '../../utils/image_assets/ImageAssets';
const httpClient = new Http();
const currentyear = new Date().getFullYear();
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
    const footerContent = "Copyright 2002 – "+currentyear+". All rights reserved. American Heritage Capital, LP. NMLS 277676";

    useEffect(() => {
        console.log(LoanApplication.formatAmountByCountry(40008094000809)?.BRL());
        console.log('in here!!!', location);
        // if(!Auth.checkAuth()) {
        //     history.push('/loading');
        // }
        // if (location.pathname === '/') {
        //     history.push('/home/activity');
        // }
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
                <Route path="/home/DocumentStatus" component={DocumentStatus} />
                <Route path="/home/uploadedDocuments" component={UploadedDocuments} />
            </Switch>
            <RainsoftRcFooter
             content = {footerContent}
            />
        </div>
    )
}

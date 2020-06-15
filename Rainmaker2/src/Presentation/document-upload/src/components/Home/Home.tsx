import React, { useEffect, useState } from 'react'
import { Switch, Route, useLocation, useHistory, Redirect } from 'react-router-dom'
import { Activity } from './Activity/Activity'
import { UploadedDocuments } from './UploadedDocuments/UploadedDocuments'

import { Http } from '../../services/http/Http'
import { Auth } from '../../services/auth/Auth'
import { Store } from '../../store/store'

import ActivityHeader from './AcitivityHeader/ActivityHeader'
import { LoanApplication } from '../../entities/Models/LoanApplication'
import { UserActions } from '../../store/actions/UserActions'
import { RainsoftRcHeader, RainsoftRcFooter } from 'rainsoft-rc';
import { DocumentStatus } from './Activity/DocumentStatus/DocumentStatus'
import { DocumentRequest } from './DocumentRequest/DocumentRequest'

import ImageAssets from '../../utils/image_assets/ImageAssets';
const httpClient = new Http();
const currentyear = new Date().getFullYear();
export const Home = () => {
    const location = useLocation();
    const history = useHistory();
    const [authenticated, setAuthenticated] = useState<boolean>(false);

    const authenticate = async () => {
        console.log('in authenticate');
        try {
            let res = UserActions.authenticate();

        } catch (error) {
            if (!Auth.checkAuth()) {
                history.push('/error');
            }
        }
    }

    if (!Auth.checkAuth()) {
        authenticate();
    }

    const gotoDashboardHandler = () => {
        console.log('gotoDashboardHandler')
        window.open('https://alphatx.rainsoftfn.com/', '_self');
    }
    const changePasswordHandler = () => {
        console.log('changePasswordHandler')
        window.open('https://alphatx.rainsoftfn.com/', '_self');
    }
    const signOutHandler = () => {
        UserActions.logout();
        window.open('https://alphatx.rainsoftfn.com/', '_self');
    }
    const headerDropdowmMenu = [
        { name: 'Dashboard', callback: gotoDashboardHandler },
        { name: 'Change Password', callback: changePasswordHandler },
        { name: 'Sign Out', callback: signOutHandler }
    ]
    const footerContent = "Copyright 2002 – " + currentyear + ". All rights reserved. American Heritage Capital, LP. NMLS 277676";

    useEffect(() => {
        // if (Auth.checkAuth()) {
        //     if (location.pathname === '/') {
        //         history.push('/activity');
        //     }
        // } else {
        //     history.push('/error');
        // }
    }, []);

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
                <Redirect exact from={ "/" } to={"/activity"} />
                <Route path="/activity" component={Activity} />
                <Route path="/documentsRequest" component={DocumentRequest} />
                <Route path="/uploadedDocuments" component={UploadedDocuments} />
            </Switch>
            <RainsoftRcFooter
                content={footerContent}
            />
        </div>
    )
}

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
            <ActivityHeader />
            <Switch>
                <Redirect exact from={ "/" } to={"/activity"} />
                <Route  path="/activity" component={Activity} />
                <Route  path="/documentsRequest" component={DocumentRequest} />
                <Route  path="/uploadedDocuments" component={UploadedDocuments} />
               
            </Switch>
        </div>
    )
}

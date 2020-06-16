import React, { useEffect, useState } from 'react'
import { useCookies } from 'react-cookie';
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
import { PageNotFound } from '../../shared/Errors/PageNotFound';

const httpClient = new Http();

export const Home = () => {
    const location = useLocation();
    const history = useHistory();
    const [authenticated, setAuthenticated] = useState<boolean>(false);
    const [cookies, setCookie] = useCookies(['Rainmaker2Token']);
    const authenticate = async () => {
        console.log('in authenticate');
        try {
            console.log("cookies", cookies)
            if (cookies != undefined && cookies.Rainmaker2Token != undefined) {
                localStorage.setItem('auth', cookies.Rainmaker2Token);
            }
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
            {!location.pathname.includes('404') && <ActivityHeader />}
            <Switch>
                <Redirect exact from={"/"} to={"/activity"} />
                <Route path="/activity" component={Activity} />
                <Route path="/documentsRequest" component={DocumentRequest} />
                <Route path="/uploadedDocuments" component={UploadedDocuments} />
                <Route path="/404" component={PageNotFound} />
                <Redirect exact from={"*"} to={"/404"} />
            </Switch>
        </div>
    )
}

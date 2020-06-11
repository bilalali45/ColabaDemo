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
import Header from '../../shared/Components/Header/Header'
import Footer from '../../shared/Components/Footer/Footer'
import { DocumentStatus } from './Activity/DocumentStatus/DocumentStatus'
import { DocumentRequest } from './DocumentRequest/DocumentRequest'
const httpClient = new Http();

export const Home = () => {
    const location = useLocation();
    const history = useHistory();

    const [authenticated, setAuthenticated] = useState<boolean>(false);

    const authenticate = async () => {
        let res = UserActions.authenticate();
    }

    if (!Auth.checkAuth()) {
        authenticate();
    }


    useEffect(() => {
        // if(!Auth.checkAuth()) {
        //     history.push('/loading');
        // }
        // if (location.pathname === '/') {
        //     history.push('/activity');
        // }
    }, []);

    return (
        <div>
            <Header></Header>
            <ActivityHeader />
            <Switch>
                <Route path="/activity" component={Activity} />
                <Route path="/documentsRequest" component={DocumentRequest} />
                <Route path="/uploadedDocuments" component={UploadedDocuments} />
            </Switch>
            <Footer></Footer>
        </div>
    )
}

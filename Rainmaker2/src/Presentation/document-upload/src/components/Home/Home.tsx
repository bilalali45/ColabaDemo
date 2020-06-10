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
import Header from '../../shared/Components/Header/Header'
import Footer from '../../shared/Components/Footer/Footer'
const httpClient = new Http();

export const Home = () => {
    const location = useLocation();
    const history = useHistory();

    const [authenticated, setAuthenticated] = useState<boolean>(false);


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
            <Header></Header>
            <ActivityHeader />
            <Switch>
                <Route path="/home/activity" component={Activity} />
                <Route path="/home/documentsRequest" component={DocumentRequest} />
                <Route path="/home/uploadedDocuments" component={UploadedDocuments} />
            </Switch>
            <Footer></Footer>
        </div>
    )
}

<<<<<<< HEAD
import React from 'react'
import { Switch, Route } from 'react-router-dom'
// import { ActivityHeader } from './AcitivityHeader/ActivityHeader'
import { Activity } from './Activity/Activity'
import { DocumentRequest } from './DocumentRequest/DocumentRequest'
import { UploadedDocuments } from './UploadedDocuments/UploadedDocuments'
// import ImageAssets from '../../utils/image_assets/ImageAssets'

import Header from '../../shared/Components/Header/Header';
import Footer from '../../shared/Components/Footer/Footer';
import {LoanStatus} from './Activity/LoanStatus/LoanStatus';
import {DocumentStatus} from './Activity/DocumentsStatus/DocumentStatus';
=======
import React, { useEffect, useContext } from 'react'
import { Switch, Route, useLocation, useHistory } from 'react-router-dom'
import { ActivityHeader } from './AcitivityHeader/ActivityHeader'
import { Activity } from './Activity/Activity'
import { DocumentRequest } from './DocumentRequest/DocumentRequest'
import { UploadedDocuments } from './UploadedDocuments/UploadedDocuments'
import ImageAssets from '../../utils/image_assets/ImageAssets'
import { Http } from '../../services/http/Http'
import { Auth } from '../../services/auth/Auth'
import { Store } from '../../store/store'
import { DocumentView } from '../../shared/Components/DocumentView/DocumentView'
const httpClient = new Http();
>>>>>>> master

export const Home = () => {
    const location = useLocation();
    const history = useHistory();

    const {state, dispatch} = useContext(Store)

    httpClient.get('/home');

    const fetchExample = async () => {
      let res = await  httpClient.fetch({
            method: 'get',
            url: 'https://jsonplaceholder.typicode.com/todos/1'
        });
        console.log(res);
    }

    fetchExample();


    const isAuthenticated = () => {
        let auth = Auth.checkAuth();
        if (!auth) {
            history.push('/login');
        }

    }

    isAuthenticated();


    useEffect(() => {
        if (location.pathname === '/') {
            history.push('/home/activity');
        }
    }, [])

    return (
        <div>
<<<<<<< HEAD
            <Header />

<section className="page-content">
            <div className="container">
                <div className="row gutter15">
                    <div className="col-md-5">
                        <LoanStatus/>
                    </div>
                    <div className="col-md-7">
                        <DocumentStatus/>
                    </div>
                </div>
            </div>
            </section>

            <Footer />

=======
            {/* <DocumentView url={`http://localhost:5000/pdf/file-example_PDF_1MB.pdf`} hide={(e: any) => {}} /> */}
            <h1>Home Rendering</h1>
            {/* <p>{state.auth}</p> */}
            <img src={ImageAssets.footer.logo512} alt="" />

            <ActivityHeader />
>>>>>>> master
            <Switch>
                <Route path="/home/activity" component={Activity} />
                <Route path="/home/documentsRequest" component={DocumentRequest} />
                <Route path="/home/uploadedDocuments" component={UploadedDocuments} />
            </Switch>
        </div>
    )
}

import React, { useEffect } from 'react'
import { Switch, Route, useLocation, useHistory } from 'react-router-dom'
import { ActivityHeader } from './AcitivityHeader/ActivityHeader'
import { Activity } from './Activity/Activity'
import { DocumentRequest } from './DocumentRequest/DocumentRequest'
import { UploadedDocuments } from './UploadedDocuments/UploadedDocuments'
import ImageAssets from '../../utils/image_assets/ImageAssets'
import { Http } from '../../services/http/Http'
import { Auth } from '../../services/auth/Auth'
const httpClient = new Http();

export const Home = () => {
    const location = useLocation();
    const history = useHistory();

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
        if (!location.pathname.includes('activty')) {
            history.push('/home/activity');
        }
    }, [])

    return (
        <div>
            <h1>Home Rendering</h1>
            <img src={ImageAssets.footer.logo512} alt="" />
            <ActivityHeader />
            <Switch>
                <Route path="/home/activity" component={Activity} />
                <Route path="/home/documentsRequest" component={DocumentRequest} />
                <Route path="/home/uploadedDocuments" component={UploadedDocuments} />
            </Switch>
        </div>
    )
}

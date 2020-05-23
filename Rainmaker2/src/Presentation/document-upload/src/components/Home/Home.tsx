import React from 'react'
import { Switch, Route } from 'react-router-dom'
import { ActivityHeader } from './AcitivityHeader/ActivityHeader'
import { Activity } from './Activity/Activity'
import { DocumentRequest } from './DocumentRequest/DocumentRequest'
import { UploadedDocuments } from './UploadedDocuments/UploadedDocuments'
import ImageAssets from '../../utils/image_assets/ImageAssets'

export const Home = () => {
    return (
        <div>
            <h1>Home Rendering</h1>
            <img src={ImageAssets.footer.logo512} alt=""/>
            <ActivityHeader/>
            <Switch>
                <Route path="/home/activity" component={Activity}/>
                <Route path="/home/documentsRequest" component={DocumentRequest}/>
                <Route path="/home/uploadedDocuments" component={UploadedDocuments}/>
            </Switch>
        </div>
    )
}

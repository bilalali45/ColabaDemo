import React from 'react'
import { Switch, Route } from 'react-router-dom'
// import { ActivityHeader } from './AcitivityHeader/ActivityHeader'
import { Activity } from './Activity/Activity'
import { DocumentRequest } from './DocumentRequest/DocumentRequest'
import { UploadedDocuments } from './UploadedDocuments/UploadedDocuments'
// import ImageAssets from '../../utils/image_assets/ImageAssets'

import Header from '../../shared/Components/Header/Header'
import Footer from '../../shared/Components/Footer/Footer'

export const Home = () => {
    return (
        <div>
            <Header />

            <div className="content">
                Hello World
            </div>
            
            <Footer />

            

            <Switch>
                <Route path="/home/activity" component={Activity}/>
                <Route path="/home/documentsRequest" component={DocumentRequest}/>
                <Route path="/home/uploadedDocuments" component={UploadedDocuments}/>
            </Switch>
        </div>
    )
}

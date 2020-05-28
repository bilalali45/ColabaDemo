import React from 'react'
import { Switch, Route } from 'react-router-dom'
import { ActivityHeader } from './AcitivityHeader/ActivityHeader'
import { Activity } from './Activity/Activity'
import { DocumentRequest } from './DocumentRequest/DocumentRequest'
import { UploadedDocuments } from './UploadedDocuments/UploadedDocuments'
import ImageAssets from '../../utils/image_assets/ImageAssets'

import Header from '../../shared/Components/Header/Header'
import Footer from '../../shared/Components/Footer/Footer'

export const Home = () => {
    return (
        <div>
            <Header />
            
            <Footer />

            {/* <div className="alert alert-primary" role="alert">
            A simple primary alert—check it out!
            </div>
            <div className="alert alert-secondary" role="alert">
            A simple secondary alert—check it out!
            </div>
            <div className="alert alert-success" role="alert">
            A simple success alert—check it out!
            </div>
            <div className="alert alert-danger" role="alert">
            A simple danger alert—check it out!
            </div>
            <div className="alert alert-warning" role="alert">
            A simple warning alert—check it out!
            </div>
            <div className="alert alert-info" role="alert">
            A simple info alert—check it out!
            </div>
            <div className="alert alert-light" role="alert">
            A simple light alert—check it out!
            </div>
            <div className="alert alert-dark" role="alert">
            A simple dark alert—check it out!
            </div> */}

            <Switch>
                <Route path="/home/activity" component={Activity}/>
                <Route path="/home/documentsRequest" component={DocumentRequest}/>
                <Route path="/home/uploadedDocuments" component={UploadedDocuments}/>
            </Switch>
        </div>
    )
}

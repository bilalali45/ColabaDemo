import React, { useEffect, useState, Component } from 'react'
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
import { DocumentsStatus } from './Activity/DocumentsStatus/DocumentsStatus'
import { DocumentRequest } from './DocumentRequest/DocumentRequest'
import ImageAssets from '../../utils/image_assets/ImageAssets';
import { PageNotFound } from '../../shared/Errors/PageNotFound';
import { debug } from 'console';
import { Authorized } from '../../shared/Components/Authorized/Authorized';

const httpClient = new Http();


export class Home extends Component {
    render() {
        return (
            <div>
                {!window.location.pathname.includes('404') && <ActivityHeader {...this.props}/>}
                <main className="page-content">
                    <div className="container">
                        <Switch>
                            <Redirect exact from={"/"} to={"/activity"} />
                            <Authorized path="/activity" component={Activity} />
                            <Authorized path="/documentsRequest" component={DocumentRequest} />
                            <Authorized path="/uploadedDocuments" component={UploadedDocuments} />
                            <Route path="/404" component={PageNotFound} />
                            <Redirect exact from={"*"} to={"/404"} />
                        </Switch>
                    </div>
                </main>
            </div>
        )
    }
}

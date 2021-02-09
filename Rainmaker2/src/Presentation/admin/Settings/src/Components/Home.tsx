import React, { Component } from 'react'
import { SideBar } from './Shared/SideBar';
import { Header } from './Shared/Header';
import { Route, BrowserRouter as Router, Switch, Redirect } from 'react-router-dom';
import { Profile } from './Profile/Profile';
import { Notification } from './Notification/Notification';
import {PrivateRoute} from './Shared/PrivateRoute'
import { PageNotFound} from './Shared/PageNotFound'
import ManageDocumentTemplate from './ManageDocumentTemplate/ManageDocumentTemplate';
import ManageUsers from './ManageUsers/ManageUsers';
import RequestEmailTemplates from './RequestEmailTemplates/RequestEmailTemplates';
import LoanOriginationSystem from './LoanOriginationSystem/LoanOriginationSystem';
import NeedsListReminder from './NeedListReminder/NeedListReminder';
import LoanStatusUpdate from './LoanStatusUpdate/LoanStatusUpdate';
import { LocalDB } from '../Utils/LocalDB';



export class Home extends Component<any> {
    lastNavigation = LocalDB.getCurrentUrl();

    render() {
        return (
            <section className="settings__content-area">
                <Switch>
                    <Redirect exact from="/" to={this.lastNavigation} />
                    <PrivateRoute path="/Profile" component={Profile} />
                    <PrivateRoute path="/Notification" component={Notification} />
                    <PrivateRoute path="/ManageDocumentTemplate" component={ManageDocumentTemplate} />
                    <PrivateRoute path="/ManageUsers" component={ManageUsers} />
                    <PrivateRoute path="/RequestEmailTemplates" component={RequestEmailTemplates} />
                    <PrivateRoute path="/NeedsListReminder" component={NeedsListReminder} />
                    <PrivateRoute path="/LoanOriginationSystem" component={LoanOriginationSystem} />
                    <PrivateRoute path="/LoanStatusUpdate" component={LoanStatusUpdate} />


                    <Route path="/PageNotFound" component={PageNotFound}/>
                </Switch>
            </section>
        )
    }
}

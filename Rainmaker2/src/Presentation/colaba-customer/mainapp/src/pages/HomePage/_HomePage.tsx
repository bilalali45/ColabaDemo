import React, { useEffect, useState } from 'react'
import { Switch, useHistory, useLocation } from 'react-router-dom';
import { HeaderPortal } from '../../components/HeaderPortal';
import { Dashboard } from '../Dashboard/_Dashboard';
import '../../lib/rs-authorization.js'
import { PrivateRoute } from '../../components/PrivateRoute';
import { ApplicationEnv } from '../../lib/appEnv';
import { LoanApplication } from '../LoanApplicationPortal';
import { LoanApplicationType } from '../../Entities/Models/LoanApplicationType';
import DashboardActions from '../../Store/actions/DashboardActions';

import Loader from '../../Shared/Components/Loader';


export const Homepage = () => {
    const [isValidated, setisValidated] = useState(false)
    const history = useHistory();
    const location = useLocation();
    useEffect(() => {
        authenticate();
    }, []);

    const authenticate = async () => {
        await window.Authorization.authorize();
        if (window.Authorization.checkAuth()) {
            fetchLoanAppsandRedirection();
        }
    };
    
    const fetchLoanAppsandRedirection = async ()  => {
        //if (!loanApplications || !loanApplications.length) {

        let loanApplications: LoanApplicationType[] = await DashboardActions.fetchLoggedInUserCurrentLoanApplications();
        if (location.pathname.includes("/homepage")) {
            if (loanApplications == null)
                return <Loader type="widget"/>;
            else if (loanApplications?.length === 0) {
                history.push(`${ApplicationEnv.LoanApplicationBasePath}?loanapplicationid=new`)
                return null;
            }
            else if (loanApplications?.length === 1) {
                history.push(`${ApplicationEnv.LoanApplicationBasePath}?loanapplicationid=${loanApplications[0]?.id}`)
                return null;
            }
        }
        setisValidated(true);
        return null;
    }


    console.log("********************************", isValidated)
    return (
        <main>
            <HeaderPortal loggedInUserName={"Test"} />
            <section className="page-main-sect">
                <div className="container">
                    <div className="loan-boxes-row row">
                        <Switch>
                            <PrivateRoute path={`${ApplicationEnv.LoanApplicationBasePath}/:navaigation?/:step?/:loanid?`} component={LoanApplication} />
                            <PrivateRoute path="/dashboard" component={Dashboard} />
                            <PrivateRoute path="/" component={Dashboard} />
                        </Switch>
                    </div>
                </div>
            </section>
        </main>
    )
}

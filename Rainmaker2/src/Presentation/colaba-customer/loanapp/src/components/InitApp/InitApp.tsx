import React, { Fragment, useContext, useEffect, useState } from 'react'
import { useHistory } from 'react-router'
import { LocalDB } from '../../lib/LocalDB'
import Loader from '../../Shared/Components/Loader'
import { CommonActions } from '../../store/actions/CommonActions'
import GettingStartedActions from '../../store/actions/GettingStartedActions'
import { Store } from '../../store/store'
import { ErrorHandler } from '../../Utilities/helpers/ErrorHandler'
import { NavigationHandler } from '../../Utilities/Navigation/NavigationHandler'
import { UrlQueryManager } from '../../Utilities/Navigation/UrlQueryManager'
import { Authorize } from '../Authorize/Authorize'
import { LoanHome } from '../Home/LoanHome'
import { NavigationManager } from '../NavigationManager/NavigationManager'
import { SetupLoan } from '../SetupLoan/SetupLoan'

export const InitApp = () => {

    const [navState, setNavState] = useState({ lastPath: '', history: [], disabledSteps: [] });
    const [pendingLoanInfo, setPendingLoanInfo] = useState<any>();
    const [fetchedPreviousNav, setFechedPreviousNav] = useState(false);
    const [navigationConfigured, setNavigationConfigured] = useState(false);
    const [foundId, setFoundId] = useState<string | undefined>('');

    const { state, dispatch } = useContext(Store);

    const history = useHistory();

    useEffect(() => {
        // if (process.env.NODE_ENV === 'development' && location.port === '3003') {
        //     history.push(`${ApplicationEnv.ApplicationBasePath}?loanapplicationid=new`)
        //     return;
        // }
    }, [])

    useEffect(() => {
        if (foundId) {
            return;
        }
        if (location.search) {
            let id = location.search.split('=')[1];
            if (id === 'new' || !isNaN(Number(id))) {
                setFoundId(id);
            } else {
                UrlQueryManager.extractQueryFromUrl(location.search);
                console.log('Query Params 1', UrlQueryManager.getQueryData());
                id = LocalDB.getLoanAppliationId() || 'new';
                setFoundId(id);
            }
            if (id != 'new')
                LocalDB.setLoanAppliationId(id);
        }
    }, [location.search]);

    useEffect(() => {
        if (foundId && !pendingLoanInfo) {
            getPendingLoanApplication(foundId)
        }
    }, [foundId]);

    useEffect(() => {

        storeLoInfo();
        initNav();

    }, [fetchedPreviousNav]);

    useEffect(() => {

        if (fetchedPreviousNav && pendingLoanInfo) {
            SetupLoan.initLoanApplication(foundId, {
                state,
                dispatch,
                pendingLoanInfo,

            })
        }

    }, [fetchedPreviousNav, pendingLoanInfo]);

    useEffect(() => {
        console.log('pendingLoanInfo', pendingLoanInfo)

        UrlQueryManager.extractQueryFromUrl(location.search);
        console.log('Query Params 2', UrlQueryManager.getQueryData());
        window.scrollTo(0, 0);
    }, [location.pathname, location.search])

    const initNav = async () => {
        if (fetchedPreviousNav) {
            await NavigationHandler.initNavigation({
                location,
                history,
                navState,
                state,
                dispatch
            });
            if (NavigationHandler.navigation) {
                setNavigationConfigured(true);
                NavigationHandler.updateFeatureSettting(pendingLoanInfo?.setting);

            }
        }
    }


    const storeLoInfo = async () => {
        let response = await GettingStartedActions.getLoInfo();
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                LocalDB.setLOImageUrl(response.data.image);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    };

    const getPendingLoanApplication = async (loanapplicationId?: string) => {
        let loanInfoResponse;
        try {
            loanInfoResponse = await CommonActions.getPendingLoanApplication(
                loanapplicationId
            );
            setPendingLoanInfo(loanInfoResponse);

        } catch (error) {

        }
        try {


            if (loanInfoResponse?.restartLoanApplication) {
                setNavState(null);
                setFechedPreviousNav(true);
            } else {
                if (loanInfoResponse?.state) {
                    let parsedSate = JSON.parse(loanInfoResponse?.state);
                    setNavState(parsedSate.navState);

                    if (parsedSate?.navState?.query) {
                        UrlQueryManager.exractQueryFromSavedState(parsedSate?.navState?.query);
                    }

                    setFechedPreviousNav(true);
                }
            }

        } catch (error) {
            setFechedPreviousNav(true);
            setNavState({ lastPath: '', history: [], disabledSteps: [] });
        }
    };

    // const updateIds = (loanInfoResponse) => {
    //     if (foundId === 'new') {
    //         return;
    //     }
    //     let pendingLoanApplication: any = loanInfoResponse;
    //     LocalDB.setLoanAppliationId(foundId);

    //     if(pendingLoanApplication?.loangoalid) {
    //         LocalDB.setLoanGoalId(pendingLoanApplication?.loangoalid);
    //     }

    //     if(pendingLoanApplication?.loanpurposeid) {
    //         LocalDB.setLoanPurposeId(pendingLoanApplication?.loanpurposeid);
    //     }
    //     // LocalDB.setBorrowerId(pendingLoanApplication?.borrowerid);
    //     // LocalDB.setIncomeId(pendingLoanApplication?.incomeid);
    // }

    return (
        <Authorize>
            <Fragment>
                {navigationConfigured ? <NavigationManager navState={navState}>
                    <div className="container">
                        <LoanHome />
                    </div>
                </NavigationManager> : <div><Loader type="page"/></div>}
            </Fragment>
        </Authorize>
    )
}

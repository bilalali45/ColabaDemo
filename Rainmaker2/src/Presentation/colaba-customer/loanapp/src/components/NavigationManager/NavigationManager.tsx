import React, { useContext, useEffect } from 'react'
import { NavigationHandler } from '../../Utilities/Navigation/NavigationHandler';
import { useLocation } from 'react-router-dom';
import { ApplicationEnv } from '../../lib/appEnv';
import { CommonActions } from '../../store/actions/CommonActions';
import { LocalDB } from '../../lib/LocalDB';
import { UrlQueryManager } from '../../Utilities/Navigation/UrlQueryManager';
import { Store } from '../../store/store';
import { LoanInfoType } from '../../Entities/Models/types';

export const NavigationManager = ({ children, navState }) => {

    const { state } = useContext(Store);
    const loanManager: any = state.loanManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;

    const location = useLocation();


    useEffect(() => {
        window.onpopstate = (event) => {
            if (NavigationHandler.navigation?.history?.length) {
                event.preventDefault();
                NavigationHandler.moveBack();
            }
        }
    }, []);

    useEffect(() => {
        console.log('navState', navState);
        computeInitialStep();

    }, [navState?.lastPath])

    useEffect(() => {
        NavigationHandler.changeCurrentStep(location.pathname);
        NavigationHandler.storeNavigationState();
        saveNavigationState()
    }, [location.pathname]);

    useEffect(() => {
        if(!isNaN(LocalDB.getBorrowerId()) && loanInfo?.borrowerId === null) {
            // CommonActions.resettoPrimaryBorrower(loanInfo, dispatch);
        }
    }, [location.pathname, LocalDB.getBorrowerId()])

    const saveNavigationState = () => {

        if (!NavigationHandler.navigation.currentStep) {
            return;
        }

        try {
            let navData = {
                LoanApplicationId: LocalDB.getLoanAppliationId(),
                State: NavigationHandler.getNavigationStateAsString(),
            };
            console.log(UrlQueryManager.getQueryData());
            if (navData) {
                CommonActions.saveNavigation(navData);
            }

        } catch (error) {
            console.log('error', error);
        }
    }

    const computeInitialStep = () => {
        console.log('last path found', navState);
        if (navState?.lastPath) {
            mapDisabledFeatures();
            mapHistory();

            let url = `${navState?.lastPath}`;

            if (navState?.query) {
                url = `${navState?.lastPath}?q=${navState?.query}`;
            }

            NavigationHandler.navigateToPath(url);

        } else {
            let lastPath = location.pathname.split('/')[location.pathname.split('/').length - 1];

            console.log(lastPath, ApplicationEnv.ApplicationBasePath)
            if (`/${lastPath}`.toLowerCase() === ApplicationEnv.ApplicationBasePath.toLowerCase()) {
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingStarted/LoanOfficer`);

            }
            // else {
            //     NavigationHandler.navigateToPath(location.pathname);
            // }
        }
    }

    const mapHistory = () => {

        if (navState?.history) {
            NavigationHandler.mapHistory(navState?.history);
        }
    }
    const mapDisabledFeatures = () => {
        if (navState?.disabledSteps) {
            NavigationHandler.mapDisabledFeatures(navState?.disabledSteps);
        }
    }
    return (
        <React.Fragment>
            {children}
        </React.Fragment>
    )
}

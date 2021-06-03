import React, { useState, useEffect, useContext } from 'react'
import { Store } from '../../../../../store/store'
import { Switch } from 'react-router-dom'
import { ApplicationEnv } from '../../../../../lib/appEnv'

import IncomeModal from '../IncomeModal/IncomeModal'
import { Employment } from '../IncomeSources/Employment/Employment'
import { SelfEmployment } from '../IncomeSources/SelfEmployment/SelfEmployment'
import { Business } from '../IncomeSources/Business/Business'
import { Military } from '../IncomeSources/Military/Military'
import { Retirement } from '../IncomeSources/Retirement/Retirement'
import { Rental } from '../IncomeSources/Rental/Rental'
import { Other } from '../IncomeSources/Other/Other'
import { IncomeSources } from '../IncomeSources/IncomeSources'
import { StringServices } from '../../../../../Utilities/helpers/StringServices'
import { IsRouteAllowed } from '../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed'
import { NavigationHandler } from '../../../../../Utilities/Navigation/NavigationHandler'


const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome`;
let modalClosePath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome`;

export const IncomeSourcesHome = () => {

    const { state } = useContext(Store);
    const commonManager: any = state.commonManager;
    const loanManager: any = state.loanManager;

    const [wizardTitle, setWizardTitle] = useState<string>('');

    useEffect(() => {
        let modalFromPath = NavigationHandler.getPreviousStepPath();
        if(modalFromPath && modalFromPath.includes('/EmploymentHistory')) {
            modalClosePath = '/loanApplication/MyMoney/Income/EmploymentHistory';
        }
    }, [])

    useEffect(() => {
        console.log('========> updated Borrower name', loanManager?.loanInfo?.borrowerName)
        if (commonManager?.incomePopupTitle) {
            let title = `${StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)}'s ${StringServices.capitalizeFirstLetter(commonManager?.incomePopupTitle)} Income`;
            setWizardTitle(title);
        } else {
            setWizardTitle('Select your source of income');
        }
    }, [loanManager?.loanInfo?.borrowerName, commonManager?.incomePopupTitle])

    return (
        <IncomeModal
            title={wizardTitle}
            closePath={modalClosePath}
            className="nothaveFooter"
            handlerCancel={() => { }} >

            <Switch>
                <IsRouteAllowed path={`${containerPath}/IncomeSources`} component={IncomeSources} />
                <IsRouteAllowed path={`${containerPath}/Employment`} component={Employment} />
                <IsRouteAllowed path={`${containerPath}/SelfIncome`} component={SelfEmployment} />
                <IsRouteAllowed path={`${containerPath}/Business`} component={Business} />
                <IsRouteAllowed path={`${containerPath}/Military`} component={Military} />
                <IsRouteAllowed path={`${containerPath}/Retirement`} component={Retirement} />
                <IsRouteAllowed path={`${containerPath}/Rental`} component={Rental} />
                <IsRouteAllowed path={`${containerPath}/Other`} component={Other} />
            </Switch>
        </IncomeModal>
    )
} 
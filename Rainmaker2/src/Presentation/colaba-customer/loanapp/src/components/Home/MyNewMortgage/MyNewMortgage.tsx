import React from 'react'
import { SubjectPropertyUse } from './SubjectPropertyUse/SubjectPropertyUse';
import { SubjectPropertyNewHome } from './SubjectPropertyType/SubjectPropertyType';
import { SubjectPropertyIntend } from './SubjectPropertyIntend/SubjectPropertyIntend';
import { SubjectPropertyAddress } from './SubjectPropertyAddress/SubjectPropertyAddress';
import { LoanAmountDetail } from './LoanAmountDetail/LoanAmountDetail';
import { Income } from './Income/Income';
import { NewMortgageReview } from './NewMortgageReview/NewMortgageReview';

import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../lib/appEnv';
import { IsRouteAllowed } from '../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyNewMortgage`;

export const MyNewMortgage = () => {

    return (
        <div className="loanapp-p-MyNewMortgage fadein">
            <Switch>
                <IsRouteAllowed path={`${containerPath}/SubjectPropertyNewHome`} component={SubjectPropertyNewHome} />
                <IsRouteAllowed path={`${containerPath}/SubjectPropertyUse`} component={SubjectPropertyUse} />
                <IsRouteAllowed path={`${containerPath}/SubjectPropertyIntend`} component={SubjectPropertyIntend} />
                <IsRouteAllowed path={`${containerPath}/SubjectPropertyAddress`} component={SubjectPropertyAddress} />
                <IsRouteAllowed path={`${containerPath}/LoanAmountDetail`} component={LoanAmountDetail} />
                <IsRouteAllowed path={`${containerPath}/Income`} component={Income} />
                <IsRouteAllowed path={`${containerPath}/NewMortgageReview`} component={NewMortgageReview} />
            </Switch>
        </div>
    )
}

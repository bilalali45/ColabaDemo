import React from 'react';
import { Switch } from 'react-router-dom';

import { IncomeHome } from './IncomeHome/IncomeHome';
import { IncomeReview } from './IncomeReview/IncomeReview';
import { EmploymentHistory } from './EmploymentHistory/EmploymentHistory';
import { ApplicationEnv } from '../../../../lib/appEnv';
import { IsRouteAllowed } from '../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';


const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income`;


export const Income = () => {

    return (
        <div>
            <Switch>
                <IsRouteAllowed path={`${containerPath}/IncomeHome`} component={IncomeHome} />
                <IsRouteAllowed path={`${containerPath}/EmploymentHistory`} component={EmploymentHistory} />
                <IsRouteAllowed path={`${containerPath}/IncomeReview`} component={IncomeReview} />
            </Switch>
        </div>
    )
}

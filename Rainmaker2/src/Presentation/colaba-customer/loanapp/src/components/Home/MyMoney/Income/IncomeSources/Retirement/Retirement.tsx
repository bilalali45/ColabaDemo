import React from 'react'
import { Switch } from 'react-router-dom';
import { ApplicationEnv } from '../../../../../../lib/appEnv';
import { IsRouteAllowed } from '../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { RetirementIncomeSource } from './RetirementIncomeSource/RetirementIncomeSource';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Retirement`;

export const Retirement = () => {

    return (

        <Switch>
            <IsRouteAllowed path={`${containerPath}/RetirementIncomeSource`} component={RetirementIncomeSource} />
        </Switch>
    )
}

import React from 'react'
import { Switch } from 'react-router-dom';
import { ApplicationEnv } from '../../../../../../lib/appEnv';
import { IsRouteAllowed } from '../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { BusinessAddress } from './BusinessAddress/BusinessAddress';
import { BusinessIncomeType } from './BusinessIncomeType/BusinessIncomeType';
import { BusinessRevenue } from './BusinessRevenue/BusinessRevenue';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Business`;

export const Business = () => {

    return (
        <Switch>
            <IsRouteAllowed path={`${containerPath}/BusinessIncomeType`} component={BusinessIncomeType} />
            <IsRouteAllowed path={`${containerPath}/BusinessAddress`} component={BusinessAddress} />
            <IsRouteAllowed path={`${containerPath}/BusinessRevenue`} component={BusinessRevenue} />
        </Switch>
    )
}

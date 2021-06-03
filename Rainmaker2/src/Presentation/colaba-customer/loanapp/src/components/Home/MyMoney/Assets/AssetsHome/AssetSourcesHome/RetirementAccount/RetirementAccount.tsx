import React from 'react'
import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../../../../../lib/appEnv';
import { IsRouteAllowed } from '../../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { RetirementAccountDetails } from './RetirementAccountDetails/RetirementAccountDetails';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/RetirementAccount`;

export const RetirementAccount = () => {

    return (
        <div className="RetirementAccount">
            <Switch>
                <IsRouteAllowed path={`${containerPath}/RetirementAccountDetails`} component={RetirementAccountDetails} />
            </Switch>
        </div>
    )
}

import React from 'react'
import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../../../../../lib/appEnv';
import { IsRouteAllowed } from '../../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { GiftFundsDetails } from './GiftFundsDetails/GiftFundsDetails';
import { GiftFundSource } from './GiftFundsSource/GiftFundSource';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/GiftFunds`;

export const GiftFunds = () => {

    return (
        <div className="GiftFunds">
            <Switch>
                <IsRouteAllowed path={`${containerPath}/GiftFundsSource`} component={GiftFundSource}/>
                <IsRouteAllowed path={`${containerPath}/GiftFundsDetails`} component={GiftFundsDetails}/>
            </Switch>
        </div>
    )
}

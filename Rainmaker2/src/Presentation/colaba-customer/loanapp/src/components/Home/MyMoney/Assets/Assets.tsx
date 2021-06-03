import React from 'react'
import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../../lib/appEnv';
import { IsRouteAllowed } from '../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { AssetsHome } from './AssetsHome/AssetsHome';
import { EarnestMoneyDeposit } from './EarnestMoneyDeposit/EarnestMoneyDeposit';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets`;

export const Assets = () => {

    return (
        <div>
            <Switch>
                <IsRouteAllowed
                    path={`${containerPath}/EarnestMoneyDeposit`}
                    component={EarnestMoneyDeposit}
                />

                <IsRouteAllowed
                    path={`${containerPath}/AssetsHome`}
                    component={AssetsHome}
                />
            </Switch>
        </div>
    )
}

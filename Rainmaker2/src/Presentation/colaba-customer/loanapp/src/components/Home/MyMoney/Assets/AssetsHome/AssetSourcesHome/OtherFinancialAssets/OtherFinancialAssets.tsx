import React from 'react'
import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../../../../../lib/appEnv';
import { IsRouteAllowed } from '../../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { FinancialAssetsDetails } from './FinancialAssetsDetails/FinancialAssetsDetails';
import { TypeOfFinancialAssets } from './TypeOfFinancialAssets/TypeOfFinancialAssets';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/OtherFinancialAssets`;

export const OtherFinancialAssets = () => {

    return (
        <div className="OtherFinancialAssets">
            <Switch>
                <IsRouteAllowed path={`${containerPath}/TypeOfFinancialAssets`} component={TypeOfFinancialAssets} />
                <IsRouteAllowed path={`${containerPath}/FinancialAssetsDetail`} component={FinancialAssetsDetails} />
            </Switch>
        </div>
    )
}

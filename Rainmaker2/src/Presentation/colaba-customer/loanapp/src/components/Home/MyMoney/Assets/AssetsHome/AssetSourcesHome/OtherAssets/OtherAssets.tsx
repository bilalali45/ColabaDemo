import React from 'react'
import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../../../../../lib/appEnv';
import { IsRouteAllowed } from '../../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { OtherAssetsDetails } from './OtherAssetsDetails/OtherAssetsDetails';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/OtherAssets`;

export const OtherAssets = () => {

    return (
        <div className="OtherAssets">
            <Switch>
                <IsRouteAllowed path={`${containerPath}/OtherAssetsDetails`} component={OtherAssetsDetails} />
            </Switch>
        </div>
    )
}

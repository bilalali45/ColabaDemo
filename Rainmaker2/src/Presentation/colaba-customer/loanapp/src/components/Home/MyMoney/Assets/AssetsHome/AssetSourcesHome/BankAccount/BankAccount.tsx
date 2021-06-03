import React from 'react'
import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../../../../../lib/appEnv';
import { IsRouteAllowed } from '../../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { DetailsOfBankAccount } from './DetailsOfBankAccount/DetailsOfBankAccount';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/BankAccount`;


export const BankAccount = () => {

    return (
        <div className="BankAccount" data-testid="BankAccount">
            <Switch>
                <IsRouteAllowed path={`${containerPath}/DetailsOfBankAccount`} component={DetailsOfBankAccount} />
            </Switch>
        </div>
    )
    
}



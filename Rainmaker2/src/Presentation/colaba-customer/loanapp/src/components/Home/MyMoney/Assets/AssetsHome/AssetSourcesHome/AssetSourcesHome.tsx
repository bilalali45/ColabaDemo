import React, { useContext, useState } from 'react'
import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../../../../lib/appEnv';
import { Store } from '../../../../../../store/store';
import { IsRouteAllowed } from '../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import IncomeModal from '../../../Income/IncomeModal/IncomeModal';
import { AssetSource } from './AssetSources/AssetSource';
import { BankAccount } from './BankAccount/BankAccount';
import { GiftFunds } from './GiftFunds/GiftFunds';
import { OtherAssets } from './OtherAssets/OtherAssets';
import { OtherFinancialAssets } from './OtherFinancialAssets/OtherFinancialAssets';
import { ProceedsFromTransaction } from './ProceedsFromTransaction/ProceedsFromTransaction';
import { RetirementAccount } from './RetirementAccount/RetirementAccount';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome`;
const modalClosePath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome`;


export const AssetSourcesHome = () => {

    const { state } = useContext(Store);

    const { assetPopupTitle } = state.commonManager;

    const [setShowPopup] = useState(false);

    return (
        <IncomeModal
            title={assetPopupTitle ? assetPopupTitle : "What type of asset is this?"}
            className="nothaveFooter"
            setShowPopup={setShowPopup}
            closePath={modalClosePath}
            //handlerCancel={() => { NavigationHandler.closeWizard('/loanApplication/MyMoney/Assets/AssetsHome') }}
        >

            <Switch>
                <IsRouteAllowed path={`${containerPath}/AssetSources`} component={AssetSource} />
                <IsRouteAllowed path={`${containerPath}/BankAccount`} component={BankAccount} />
                <IsRouteAllowed path={`${containerPath}/RetirementAccount`} component={RetirementAccount} />
                <IsRouteAllowed path={`${containerPath}/GiftFunds`} component={GiftFunds} />
                <IsRouteAllowed path={`${containerPath}/OtherFinancialAssets`} component={OtherFinancialAssets} />
                <IsRouteAllowed path={`${containerPath}/ProceedsFromTransaction`} component={ProceedsFromTransaction} />
                <IsRouteAllowed path={`${containerPath}/OtherAssets`} component={OtherAssets} />
            </Switch>
        </IncomeModal>
    )
}
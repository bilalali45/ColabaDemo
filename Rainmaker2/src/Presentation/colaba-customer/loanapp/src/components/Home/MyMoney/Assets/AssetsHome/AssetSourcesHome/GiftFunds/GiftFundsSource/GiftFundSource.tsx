import React, { useContext, useEffect } from 'react'
import IconRadioSnipit from '../../../../../../../../Shared/Components/IconRadioSnipit'
import {
    IconRelative,
    IconUnmarriedPartner,
    IconFederalAgency,
    IconStateAgency,
    IconLocalAgency,
    IconCommunityNonProfit,
    IconEmployer,
    IconReligiousNonProfit,
    IconLender
} from '../../../../../../../../Shared/Components/SVGs'
import { Store } from '../../../../../../../../store/store'
import { NavigationHandler } from '../../../../../../../../Utilities/Navigation/NavigationHandler'
import { AssetsActionTypes } from '../../../../../../../../store/reducers/AssetsActionReducer';
import { StringServices } from '../../../../../../../../Utilities/helpers/StringServices'
import { CommonType } from '../../../../../../../../store/reducers/CommonReducer'
import { ApplicationEnv } from '../../../../../../../../lib/appEnv'

export const GiftFundSource = () => {
    const { state, dispatch } = useContext(Store);

    const { assetInfo }: any = state.loanManager;

    const giftFundSources = [
        { id: 1, name: 'Federal Agency', icon: <IconFederalAgency /> },
        { id: 2, name: 'Local Agency', icon: <IconLocalAgency /> },
        { id: 3, name: 'State Agency', icon: <IconStateAgency /> },
        { id: 4, name: 'Employer', icon: <IconEmployer /> },
        { id: 5, name: 'Lender', icon: <IconLender /> },
        { id: 6, name: 'Community Non Profit', icon: <IconCommunityNonProfit /> },
        { id: 7, name: 'Religious Non Profit', icon: <IconReligiousNonProfit /> },
        { id: 8, name: 'Relative', icon: <IconRelative /> },
        { id: 9, name: 'Unmarried Partner', icon: <IconUnmarriedPartner /> }
    ]

    useEffect(() => {
        if (!assetInfo) {
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources`);
        }
    }, [])

    const giftFundSourceSelectHandler = (giftSourceId, name) => {
        let title = `<h4>${StringServices.capitalizeFirstLetter(assetInfo?.displayName)} / <span class="highlight-anchor" >${StringServices.capitalizeFirstLetter(name)}</span></h4>`;
        dispatch({ type: CommonType.SetAssetPopupTitle, payload: title });
        dispatch({ type: AssetsActionTypes.setGiftFundSourceId, payload: giftSourceId });
        NavigationHandler.navigateToPath('/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/GiftFunds/GiftFundsDetails');
    }

    return (
        <div>
            <div className="form-group">
                <h3 data-testid='title' className="h3">Where Is This Gift From?</h3>
            </div>

            <div className="row">
                {giftFundSources.map(x => (<div className="col-md-6">
                    <IconRadioSnipit
                        title={x.name}
                        id={x.id}
                        icon={x.icon}
                        handlerClick={() => giftFundSourceSelectHandler(x.id, x.name)}
                        className={""}
                    />
                </div>))}
            </div>

        </div>
    )
}

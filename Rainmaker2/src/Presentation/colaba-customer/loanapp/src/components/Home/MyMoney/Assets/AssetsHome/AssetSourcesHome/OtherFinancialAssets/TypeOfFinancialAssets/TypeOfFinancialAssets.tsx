import React, { useContext, useEffect, useState } from 'react'
import { FinancialAssets } from '../../../../../../../../Entities/Models/types'
import { ApplicationEnv } from '../../../../../../../../lib/appEnv'
import AssetsActions from '../../../../../../../../store/actions/AssetsActions'
import { AssetsActionTypes } from '../../../../../../../../store/reducers/AssetsActionReducer'
import { CommonType } from '../../../../../../../../store/reducers/CommonReducer'
import { Store } from '../../../../../../../../store/store'
import { ErrorHandler } from '../../../../../../../../Utilities/helpers/ErrorHandler'
import { StringServices } from '../../../../../../../../Utilities/helpers/StringServices'
import { NavigationHandler } from '../../../../../../../../Utilities/Navigation/NavigationHandler'
import { TypeOfFinancialAssetsWeb } from './TypeOfFinancialAssets_Web'

export const TypeOfFinancialAssets = () => {

    const { state, dispatch } = useContext(Store);
    const { financialAssetItem }: any = state.assetsManager;
    const { assetInfo }: any = state.loanManager;
    const [financialAssetList, setFinancialAssetList] = useState<FinancialAssets[]>();
    const [selectedItem, setSelectedItem] = useState<FinancialAssets>();

    useEffect(() => {
        if (assetInfo) {
            let title = `<h4>${StringServices.capitalizeFirstLetter(assetInfo?.displayName)}</h4>`;
            dispatch({ type: CommonType.SetAssetPopupTitle, payload: title });
            GetAllFinancialAsset();
        }
        else {
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources`);
        }
    }, [])

    useEffect(() => {
        setSelectedItem(financialAssetItem);
    }, [financialAssetItem])

    const GetAllFinancialAsset = async () => {
        let response = await AssetsActions.GetAllFinancialAsset();
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                setFinancialAssetList(response.data);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    const onSelectHandler = async (item: FinancialAssets) => {
        let title = `<h4>${StringServices.capitalizeFirstLetter(assetInfo?.displayName)} / <span class="highlight-anchor" >${StringServices.capitalizeFirstLetter(item.name)}</span></h4>`;
        dispatch({ type: CommonType.SetAssetPopupTitle, payload: title });
        setSelectedItem(item);
        await dispatch({ type: AssetsActionTypes.setFinancialAssetItem, payload: item })
        NavigationHandler.moveNext();
    }


    return (
        <TypeOfFinancialAssetsWeb
            financialAssetList={financialAssetList}
            onSelectHandler={onSelectHandler}
            selectedItem={selectedItem}
        />
    )
}

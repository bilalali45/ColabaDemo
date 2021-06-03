import React, { useContext, useEffect, useState } from 'react'
import { ApplicationEnv } from '../../../../../../../lib/appEnv'
import { Store } from '../../../../../../../store/store'
import { NavigationHandler } from '../../../../../../../Utilities/Navigation/NavigationHandler'
import { AssetSourceWeb } from './AssetSource_Web'
import { AssetInfo, AssetSourceType } from '../../../../../../../Entities/Models/types';
import AssetsActions from '../../../../../../../store/actions/AssetsActions'
import { ErrorHandler } from '../../../../../../../Utilities/helpers/ErrorHandler'
import { AssetsActionTypes } from '../../../../../../../store/reducers/AssetsActionReducer'
import { LoanApplicationActionsType } from '../../../../../../../store/reducers/LoanApplicationReducer'
import { CommonType } from '../../../../../../../store/reducers/CommonReducer'
import { StringServices } from '../../../../../../../Utilities/helpers/StringServices'
import Loader from '../../../../../../../Shared/Components/Loader'
const assetSourcesPath = {
    'Bank Account': '/BankAccount/DetailsOfBankAccount',
    'Stocks, Bonds, Or Other Financial Assets': '/OtherFinancialAssets/TypeOfFinancialAssets',
    'Retirement Account': '/RetirementAccount/RetirementAccountDetails',
    'Gift Funds': '/GiftFunds/GiftFundsSource',
    'Credits': '/OtherFinancialAssets',
    'Proceeds from Transactions': '/ProceedsFromTransaction/TypeOfProceedsFromTransaction',
    'Other': '/OtherAssets/OtherAssetsDetails',

}

const createAssetSourcePath = (title: string) => {
    return `${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome${assetSourcesPath[title]}`;
}

export const AssetSource = () => {
    const { state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const assetManager: any = state.assetsManager;
    const [assetSourceList, setAsserSourceList] = useState<AssetSourceType[]>();

    useEffect(() => {
        if (assetManager.assetSourceList) {
            setAsserSourceList(assetManager.assetSourceList);
        } else {
            GetSourceOfAssetList();
        }
        dispatch({type: AssetsActionTypes.setFinancialAssetItem, payload: null});
        dispatch({ type: CommonType.SetAssetPopupTitle, payload: null });
    }, [])


    useEffect(() => {
        if (assetSourceList) {
            dispatch({ type: AssetsActionTypes.setAssetSourceList, payload: assetSourceList });
        }
    }, [assetSourceList])


    const GetSourceOfAssetList = async () => {
        let response = await AssetsActions.GetAllAssetCategories();
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                await setAsserSourceList(response.data?.map((source: AssetSourceType) => {
                    return {
                        ...source,
                        path: createAssetSourcePath(source?.displayName)
                    }
                }));

            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    const selectTypeHandler = async (item: AssetSourceType) => {
        let title = '';
        let assetInfo: AssetInfo = {
            borrowerName: loanManager?.loanInfo?.borrowerName,
            borrowerAssetId: null,
            assetCategoryId: item.id,
            displayName: item.displayName

        }
        if(item.id === 2){
            title = `<h4>${StringServices.capitalizeFirstLetter(item.displayName)}</h4>`;
        }else if(item.id === 3){
            title = `<h4>${StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)}'s ${StringServices.capitalizeFirstLetter(item.displayName)} (e,g. 401k, IRA)</h4>`;
        }else if(item.id === 7){
            title = `<h4>${StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)}'s ${StringServices.capitalizeFirstLetter(item.displayName)} Assets</h4>`;
        }else{
            title = `<h4>${StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)}'s ${StringServices.capitalizeFirstLetter(item.displayName)}</h4>`;
        }
        dispatch({ type: CommonType.SetAssetPopupTitle, payload: title });
        await dispatch({ type: LoanApplicationActionsType.SetAssetInfo, payload: assetInfo });
        NavigationHandler.navigateToPath(item.path);
    }


    return (
        <div data-testid="incomesources-screen">

            {assetSourceList && assetSourceList?.length > 0 &&
                <AssetSourceWeb
                    AssetSourceList={assetSourceList}
                    selectType={selectTypeHandler}
                />
            }

            {assetSourceList?.length === 0 &&
                <div><Loader type="widget"/></div>
            }

        </div>
    )
}

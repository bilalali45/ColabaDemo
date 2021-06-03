import React, { useContext, useEffect, useState } from 'react'
import { FinancialAssets, FinancialSatementPayloadType } from '../../../../../../../../Entities/Models/types';
import { ApplicationEnv } from '../../../../../../../../lib/appEnv';
import { LocalDB } from '../../../../../../../../lib/LocalDB';
import AssetsActions from '../../../../../../../../store/actions/AssetsActions';
import { AssetsActionTypes } from '../../../../../../../../store/reducers/AssetsActionReducer';
import { Store } from '../../../../../../../../store/store';
import { ErrorHandler } from '../../../../../../../../Utilities/helpers/ErrorHandler';
import { NavigationHandler } from '../../../../../../../../Utilities/Navigation/NavigationHandler';
import { FinancialAssetsDetailsWeb } from './FinancialAssetsDetails_Web';

export const FinancialAssetsDetails = () => {
    const { state, dispatch } = useContext(Store);
    const { assetInfo, loanInfo }: any = state.loanManager;
    const { financialAssetItem }: any = state.assetsManager;
    const [hasAccountSave, setAccountSave] = useState<boolean>(false);
    const [institutionName, setInstitutionName] = useState<string>('');
    const [acountNumber, setacountNumber] = useState<string>('');
    const [balance, setBalance] = useState<string>('');
    const [btnClick, setBtnClick] = useState<boolean>(false);

    useEffect(() => {
        if (assetInfo) {
            if (assetInfo?.borrowerAssetId) {
                GetFinancialAsset(+LocalDB.getLoanAppliationId(), +loanInfo.borrowerId, assetInfo?.borrowerAssetId);
            }
        }
        else {
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources`);
        }
    }, [assetInfo?.borrowerAssetId])

    const GetFinancialAsset = async (loanApplicationId: number, borrowerId: number, borrowerAssetId: number) => {
        let response = await AssetsActions.GetFinancialAsset(loanApplicationId, borrowerId, borrowerAssetId);
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                populatePrefilledData(response.data);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    const UpdateRetirementAccount = async (assetInfo: FinancialSatementPayloadType) => {
        console.log('=========> UpdateRetirementAccount', assetInfo)
        let response = await AssetsActions.AddOrUpdateFinancialAsset(assetInfo);
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                setAccountSave(true);
                clearFields();
            } else {
                ErrorHandler.setError(dispatch, response);
                setBtnClick(false);
            }
        }
    }

    const onSubmit = async (data) => {
        if (!btnClick) {
            setBtnClick(true);
            console.log('=========> onSubmit', data)
            let { financialInstitution, accountNumber, currentBalance } = data;
            let assetInfoObj: FinancialSatementPayloadType = {
                LoanApplicationId: +LocalDB.getLoanAppliationId(),
                BorrowerId: +loanInfo?.borrowerId,
                Balance: +(currentBalance.replace(/\,/g, '')),
                InstitutionName: financialInstitution,
                AccountNumber: accountNumber,
                Id: assetInfo?.borrowerAssetId,
                AssetTypeId: financialAssetItem?.id,
                State: NavigationHandler.getNavigationStateAsString()
            }
            UpdateRetirementAccount(assetInfoObj);
        }
    }
    const populatePrefilledData = (assetInfo) => {
        setInstitutionName(assetInfo.institutionName);
        setacountNumber(assetInfo.accountNumber);
        setBalance(assetInfo.balance);

        let item: FinancialAssets = {
            id: assetInfo.assetTypeId,
            name: null
        }

        dispatch({ type: AssetsActionTypes.setFinancialAssetItem, payload: item })
    }

    const clearFields = () => {
        setInstitutionName('');
        setBalance('');
        setacountNumber('');
    }
    const onAnotherAssetClick = async () => {
        await dispatch({ type: AssetsActionTypes.setFinancialAssetItem, payload: null })
        NavigationHandler.moveBack();
    }

    return (
        <FinancialAssetsDetailsWeb
            hasAccountSave={hasAccountSave}
            setAccountSave={setAccountSave}
            institutionName={institutionName}
            acountNumber={acountNumber}
            balance={balance}
            setInstitutionName={setInstitutionName}
            setacountNumber={setacountNumber}
            setBalance={setBalance}
            onSubmit={onSubmit}
            onAnotherAssetClick={onAnotherAssetClick}
        />
    )
}

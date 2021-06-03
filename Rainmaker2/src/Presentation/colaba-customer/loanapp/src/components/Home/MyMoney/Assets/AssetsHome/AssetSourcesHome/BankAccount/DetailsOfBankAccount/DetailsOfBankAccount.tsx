import React, { useContext, useEffect, useState } from 'react'
import { AssetPayloadType, AssetTypesByCategory } from '../../../../../../../../Entities/Models/types';
import { ApplicationEnv } from '../../../../../../../../lib/appEnv';
import { LocalDB } from '../../../../../../../../lib/LocalDB';
import AssetsActions from '../../../../../../../../store/actions/AssetsActions';
import { Store } from '../../../../../../../../store/store';
import { ErrorHandler } from '../../../../../../../../Utilities/helpers/ErrorHandler';
import { NavigationHandler } from '../../../../../../../../Utilities/Navigation/NavigationHandler';
import { DetailsOfBankAccountWeb } from './DetailsOfBankAccount_Web'

export const DetailsOfBankAccount = () => {
    const { state, dispatch } = useContext(Store);
    const {assetInfo, loanInfo }: any = state.loanManager;
    const [assetTypesByCategory, setAssetTypesByCategory] = useState<AssetTypesByCategory[] >([]);
    const [accountSeleced, setAccountSelected] = useState<AssetTypesByCategory>();
    const [hasAccountSave, setAccountSave] = useState<boolean>(false);
    const [institutionName, setInstitutionName] = useState<string>('');
    const [acountNumber, setacountNumber] = useState<string>('');
    const [balance, setBalance] = useState<string>('');
    const [btnClick, setBtnClick] = useState<boolean>(false);

    useEffect(() => {
        GetAssetTypesByCategory();
    }, [])

    useEffect(() => {
        if (assetInfo) {
            if (assetInfo?.borrowerAssetId) {
                GetBorrowerAssetDetail(+LocalDB.getLoanAppliationId(), +loanInfo.borrowerId, assetInfo?.borrowerAssetId);
            }
        }
        else {
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources`);
        }
    }, [assetInfo?.borrowerAssetId])

    const GetAssetTypesByCategory = async () => {
        let response = await AssetsActions.GetAssetTypesByCategory(assetInfo?.assetCategoryId);
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                setAssetTypesByCategory(response.data);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    const GetBorrowerAssetDetail = async (loanApplicationId: number, borrowerId: number, borrowerAssetId: number) => {
        let response = await AssetsActions.GetBorrowerAssetDetail(loanApplicationId, borrowerId, borrowerAssetId);
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                populatePrefilledData(response.data);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    const AddOrUpdateBorrowerAsset = async (assetInfo: AssetPayloadType) => {
        let response = await AssetsActions.AddOrUpdateBorrowerAsset(assetInfo);
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

            let { assetCategoryId, id } = accountSeleced;
            let { financialInstitution, accountNumber, currentBalance } = data;

            let assetObj: AssetPayloadType = {
                LoanApplicationId: +LocalDB.getLoanAppliationId(),
                BorrowerId: +loanInfo.borrowerId,
                BorrowerAssetId: assetInfo.borrowerAssetId,
                AssetCategoryId: assetCategoryId,
                AssetTypeId: id,
                InstitutionName: financialInstitution,
                AccountNumber: accountNumber,
                AssetValue: +(currentBalance.replace(/\,/g, '')),
                State: NavigationHandler.getNavigationStateAsString(),
            }

            AddOrUpdateBorrowerAsset(assetObj);

        }
    }

    const populatePrefilledData = (data) => {
        let assetInfo = data.borrowerAssets[0];
        let salectedObje: AssetTypesByCategory = {
            id: assetInfo.assetTypeId,
            assetCategoryId: assetInfo.assetCategoryId,
            name: assetInfo.assetType,
            displayName: assetInfo.assetType,
            tenantAlternateName: null,
            imageUrl: null,
            fieldsInfo: null
        }
        setAccountSelected(salectedObje);
        setInstitutionName(assetInfo.institutionName);
        setacountNumber(assetInfo.accountNumber);
        setBalance(assetInfo.assetValue);
    }

    const clearFields = () => {
        setInstitutionName('');
        setBalance('');
        setacountNumber('');
    }

    return (
        <DetailsOfBankAccountWeb
            accountSeleced={accountSeleced}
            setAccountSelected={setAccountSelected}
            hasAccountSave={hasAccountSave}
            setAccountSave={setAccountSave}
            institutionName={institutionName}
            acountNumber={acountNumber}
            balance={balance}
            setInstitutionName={setInstitutionName}
            setacountNumber={setacountNumber}
            setBalance={setBalance}
            onSubmit={onSubmit}
            assetTypesByCategory={assetTypesByCategory}

        />
    )
}

import React, { useContext, useEffect, useState } from 'react';
import { RetirementPayloadType } from '../../../../../../../../Entities/Models/types';
import { ApplicationEnv } from '../../../../../../../../lib/appEnv';
import { LocalDB } from '../../../../../../../../lib/LocalDB';
import AssetsActions from '../../../../../../../../store/actions/AssetsActions';
import { Store } from '../../../../../../../../store/store';
import { ErrorHandler } from '../../../../../../../../Utilities/helpers/ErrorHandler';
import { NavigationHandler } from '../../../../../../../../Utilities/Navigation/NavigationHandler';
import { RetirementAccountDetailsWeb } from './RetirementAccountDetails_Web'

export const RetirementAccountDetails = () => {

    const { state, dispatch } = useContext(Store);
    const { assetInfo, loanInfo }: any = state.loanManager;
    const [hasAccountSave, setAccountSave] = useState<boolean>(false);
    const [institutionName, setInstitutionName] = useState<string>('');
    const [acountNumber, setacountNumber] = useState<string>('');
    const [balance, setBalance] = useState<string>('');
    const [btnClick, setBtnClick] = useState<boolean>(false);

    useEffect(() => {
        if (assetInfo) {
            if (assetInfo?.borrowerAssetId) {
                GetRetirementAccount(+LocalDB.getLoanAppliationId(), +loanInfo.borrowerId, assetInfo?.borrowerAssetId);
            }
        }
        else {
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources`);
        }
    }, [assetInfo?.borrowerAssetId])


    const GetRetirementAccount = async (loanApplicationId: number, borrowerId: number, borrowerAssetId: number) => {
        console.log('=======> GetRetirementAccount',)
        let response = await AssetsActions.GetRetirementAccount(loanApplicationId, borrowerId, borrowerAssetId);
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                populatePrefilledData(response.data);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    const UpdateRetirementAccount = async (assetInfo: RetirementPayloadType) => {
        let response = await AssetsActions.UpdateRetirementAccount(assetInfo);
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

    const populatePrefilledData = (assetInfo) => {
        console.log('=======> populatePrefilledData', assetInfo)
        setInstitutionName(assetInfo.institutionName);
        setacountNumber(assetInfo.accountNumber);
        setBalance(assetInfo.value);
    }

    const onSubmit = async (data) => {
        if (!btnClick) {
            setBtnClick(true);
            let { financialInstitution, accountNumber, currentBalance } = data;
            let assetInfoObj: RetirementPayloadType = {
                LoanApplicationId: +LocalDB.getLoanAppliationId(),
                BorrowerId: +loanInfo.borrowerId,
                Value: +(currentBalance.replace(/\,/g, '')),
                InstitutionName: financialInstitution,
                AccountNumber: accountNumber,
                Id: assetInfo?.borrowerAssetId
            }
            UpdateRetirementAccount(assetInfoObj);
        }
    }

    const clearFields = () => {
        setInstitutionName('');
        setBalance('');
        setacountNumber('');
    }

    return (
        <RetirementAccountDetailsWeb
            hasAccountSave={hasAccountSave}
            setAccountSave={setAccountSave}
            institutionName={institutionName}
            acountNumber={acountNumber}
            balance={balance}
            setInstitutionName={setInstitutionName}
            setacountNumber={setacountNumber}
            setBalance={setBalance}
            onSubmit={onSubmit}
        />
    )
}

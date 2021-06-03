import React, { useState, useContext, useEffect } from 'react'
import { Store } from '../../../../../store/store';
import {
    AssetBorrowerProto,
    AssetsHomeBorrowerProto,
    AssetProto,
    LoanInfoType
} from '../../../../../Entities/Models/types';
import { ErrorHandler } from '../../../../../Utilities/helpers/ErrorHandler';
import IncomeActions from '../../../../../store/actions/IncomeActions';
import { AssetsHomeList } from './AssetsHomeList';
import Loader from '../../../../../Shared/Components/Loader';
import { LoanApplicationActionsType } from "../../../../../store/reducers/LoanApplicationReducer";
import { LocalDB } from "../../../../../lib/LocalDB";
import { ApplicationEnv } from "../../../../../lib/appEnv";
import { PopupModal } from "../../../../../Shared/Components/Modal";
import BusinessActions from "../../../../../store/actions/BusinessActions";
import { NavigationHandler } from '../../../../../Utilities/Navigation/NavigationHandler';
import { StringServices } from '../../../../../Utilities/helpers/StringServices';
import { CommonType } from '../../../../../store/reducers/CommonReducer';
import AssetsActions from '../../../../../store/actions/AssetsActions';
import { AssetInfo } from '../../../../../Entities/Models/types';

export const AssetsHome = () => {

    const { state, dispatch } = useContext(Store);

    const loanManager: any = state.loanManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;
    const [assetsHomeBorrowerData, setAssetsHomeBorrowerData] = useState<AssetsHomeBorrowerProto>();

    const [deletePopup, setDeletePopup] = useState<boolean>(false);

    useEffect(() => {
        getAssetCategories()
        fetchData()

    }, []);

    useEffect(() => {
        fetchData()
    }, [location.pathname]);

    const getAssetCategories = async () => {
        let response = await IncomeActions.GetSourceOfIncomeList();
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {

            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    const fetchData = async () => {
        if (LocalDB.getLoanAppliationId() != undefined) {
            let response = await AssetsActions.GetMyMoneyHomeScreen(Number(LocalDB.getLoanAppliationId()));
            if (response) {
                if (ErrorHandler.successStatus.includes(response.statusCode)) {
                    setAssetsHomeBorrowerData(response.data)
                }
                else {
                    ErrorHandler.setError(dispatch, response);
                }
            }
        }
    }

    const editAsset = async (assetBorrowerProto: AssetBorrowerProto, assetProto: AssetProto) => {
        await clearDataFromStore(assetBorrowerProto);

        let title = `${StringServices.capitalizeFirstLetter(assetBorrowerProto.borrowerName)}'s ${StringServices.capitalizeFirstLetter(assetProto.assetCategoryName)}`;
        dispatch({ type: CommonType.SetAssetPopupTitle, payload: title });

        const assetInfo: AssetInfo = {
            borrowerName: assetBorrowerProto.borrowerName,
            borrowerAssetId: assetProto.assetId,
            assetCategoryId: assetProto.assetCategoryId,
            assetTypeId: assetProto.assetTypeID,
            displayName: assetProto.assetCategoryName
        };

        await dispatch({
            type: LoanApplicationActionsType.SetAssetInfo,
            payload: assetInfo
        });

        LocalDB.setAssetId(String(assetProto.assetId));
        LocalDB.setBorrowerId(String(assetBorrowerProto.borrowerId))

        switch (assetProto.assetCategoryId) {
            case 1:
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/BankAccount/DetailsOfBankAccount`)
                break

            case 2:
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/OtherFinancialAssets/FinancialAssetsDetail`)
                break

            case 3:
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/RetirementAccount/RetirementAccountDetails`)
                break

            case 4:
            case 5:
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/GiftFunds/GiftFundsDetails`)
                break            

            case 6:
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/ProceedsFromTransaction/DetailsOfProceedsFromTransaction`)
                break;

            case 7:
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/OtherAssets/OtherAssetsDetails`)
                break;

            default:
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome`)
        }
    }

    const deleteAsset = async (assetBorrowerProto: AssetBorrowerProto, assetProto: AssetProto) => {
        await BusinessActions.deleteAsset(Number(LocalDB.getLoanAppliationId()), assetBorrowerProto.borrowerId, assetProto.assetId);
        fetchData();
        //setDeletePopup(false);
    }

    const addAsset = async (assetBorrowerProto: AssetBorrowerProto) => {

        await clearDataFromStore(assetBorrowerProto);

        LocalDB.setBorrowerId(String(assetBorrowerProto.borrowerId))
        LocalDB.clearIncomeFromStorage();
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources`);
    }

    const clearDataFromStore = async (assetBorrowerProto: AssetBorrowerProto) => {
        let loanInfoObj: LoanInfoType = {
            ...loanInfo,
            borrowerId: assetBorrowerProto.borrowerId,
            borrowerName: assetBorrowerProto.borrowerName,
            ownTypeId: assetBorrowerProto.ownTypeId,
        }
        console.log('======> loanInfoObj', loanInfoObj)
        await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
        await dispatch({ type: LoanApplicationActionsType.SetAssetInfo, payload: {} });
    }

    const shouldDisableButton = () => {
        let toggleButton = assetsHomeBorrowerData?.borrowers.some(e => e.ownTypeId === 1 && e.assetsValue === 0)
        console.log("toggleButton  ", toggleButton);
        return toggleButton;
    }

    return (
        <React.Fragment>
            {
                assetsHomeBorrowerData ? <>
                    <AssetsHomeList assetHomeBorrowerData={assetsHomeBorrowerData} editAsset={editAsset}
                        deleteAsset={deleteAsset} addAsset={addAsset}
                        shouldDisableButton={shouldDisableButton}
                        data-testid="asset-box" />
                    <PopupModal
                        modalHeading={`Remove  ?`}
                        modalBodyData={<p>Are you sure you want to delete from this
                            loan application? The data you've entered will not be recoverable.</p>}
                        modalFooterData={<button className="btn btn-min btn-primary" >Yes</button>}
                        show={deletePopup}
                        handlerShow={() => setDeletePopup(!deletePopup)}
                        dialogClassName={`delete-borrower-popup`}
                    ></PopupModal>
                </> : <Loader type="widget" data-testid="asset-home" />
            }
        </React.Fragment>
    )

}
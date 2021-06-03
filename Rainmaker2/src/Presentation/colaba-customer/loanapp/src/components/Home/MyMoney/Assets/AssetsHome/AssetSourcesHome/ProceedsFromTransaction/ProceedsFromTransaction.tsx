import React, { useContext, useEffect, useState } from 'react'
import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../../../../../lib/appEnv';
import { Store } from '../../../../../../../store/store';
import { IsRouteAllowed } from '../../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { DetailsOfProceedsFromTransaction } from './DetailsOfProceedsFromTransaction/DetailsOfProceedsFromTransaction';
import { TypeOfProceedsFromTransaction } from './TypeOfProceedsFromTransaction/TypeOfProceedsFromTransaction';
import { AssetTypesByCategory, CollateralAssetTypesProto, AssetInfo } from "../../../../../../../Entities/Models/types";
import AssetsActions from "../../../../../../../store/actions/AssetsActions";
import { ErrorHandler } from "../../../../../../../Utilities/helpers/ErrorHandler";
import { TransactionProceedsFromLoanDTO, TransactionProceedsFromRealAndNonRealEstateDTO } from "../../../../../../../Entities/Models/TransactionProceeds";
import TransactionProceedsActions from '../../../../../../../store/actions/TransactionProceedsActions';
import { LocalDB } from '../../../../../../../lib/LocalDB';
import { NavigationHandler } from '../../../../../../../Utilities/Navigation/NavigationHandler';


const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/ProceedsFromTransaction`;

export const ProceedsFromTransaction = () => {

    const { state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const assetInfo: AssetInfo = loanManager.assetInfo;

    const [selectedPropertyType, setSelectedPropertyType] = useState<number>();
    const [collateralAssetTypes, setCollateralAssetTypes] = useState<CollateralAssetTypesProto[]>();
    const [assetTypesByCategoryList, setAssetTypesByCategoryList] = useState<AssetTypesByCategory[]>();

    const [transactionProceeds, setTransactionProceeds] = useState<any>();
    let borrowerId = Number(LocalDB.getBorrowerId());
    let borrowerAssetId = assetInfo?.borrowerAssetId;
    let assetTypeId = assetInfo?.assetTypeId;
    let assetCategoryId = assetInfo?.assetCategoryId;
    let loanApplicationId = LocalDB.getLoanAppliationId();
    
    useEffect(() => {
        if (!collateralAssetTypes) {
            GetCollateralAssetTypes();
        }
        if (!assetTypesByCategoryList) {
            if(assetInfo?.assetCategoryId)
                GetSourceOfIncomeList();
            else {
                 NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome`);
            }
        }
        
    }, [])

    /*     useEffect(() => {
            //setTransactionProceeds({});
            return ()=>{
                
            }
        }, [selectedPropertyType])
     */

    useEffect(() => {
        if (borrowerAssetId) {
            gettransactionProceedsData(borrowerAssetId, assetTypeId, borrowerId, loanApplicationId);
        }
    }, [assetInfo?.borrowerAssetId]);

    /* useEffect(() => {
        console.log("transactionProceeds ================ ",transactionProceeds);
        debugger
    }, [transactionProceeds?.borrowerAssetId]);  */

    const gettransactionProceedsData = async (borrowerAssetId, assetTypeId, borrowerId, loanApplicationId) => {
            let transactionProceedsLoan: TransactionProceedsFromLoanDTO;
            let transactionProceedsRealNonReal: TransactionProceedsFromRealAndNonRealEstateDTO;
            let response;

            if (assetTypeId == 12) { /*For proceed form loan  */
                response = await TransactionProceedsActions.GetProceedsfromloan(borrowerAssetId, assetTypeId, borrowerId, loanApplicationId);
                if (response && response.data) {
                    if (ErrorHandler.successStatus.includes(response.statusCode)) {
                        const { result } = response.data;
                        transactionProceedsLoan = new TransactionProceedsFromLoanDTO(assetTypeId, loanApplicationId, borrowerId, assetCategoryId, result.value, result.collateralAssetTypeId, result.securedByCollateral, result.collateralAssetOtherDescription, borrowerAssetId, result.collateralAssetName);
                        setTransactionProceeds(transactionProceedsLoan);
                    }
                    else {
                        ErrorHandler.setError(dispatch, response);
                    }
                }
            } else if (assetTypeId == 13) {
                response = await TransactionProceedsActions.GetFromLoanRealState(borrowerAssetId, assetTypeId, borrowerId, loanApplicationId);

                if (response && response.data) {
                    if (ErrorHandler.successStatus.includes(response.statusCode)) {
                        const { result } = response.data;
                        transactionProceedsRealNonReal = new TransactionProceedsFromRealAndNonRealEstateDTO(assetTypeId, loanApplicationId, borrowerId, assetCategoryId, result.value, result.description, borrowerAssetId);

                        setTransactionProceeds(transactionProceedsRealNonReal);
                    }
                    else {
                        ErrorHandler.setError(dispatch, response);
                    }
                }

            } else if (assetTypeId == 14) {
                response = await TransactionProceedsActions.GetFromLoanRealState(borrowerAssetId, assetTypeId, borrowerId, loanApplicationId);

                if (response && response.data) {
                    if (ErrorHandler.successStatus.includes(response.statusCode)) {
                        const { result } = response.data;
                        transactionProceedsRealNonReal = new TransactionProceedsFromRealAndNonRealEstateDTO(assetTypeId, loanApplicationId, borrowerId, assetCategoryId, result.value, result.description, borrowerAssetId);

                        setTransactionProceeds(transactionProceedsRealNonReal);
                    }
                    else {
                        ErrorHandler.setError(dispatch, response);
                    }
                }

            }

            setSelectedPropertyType(assetTypeId);
            /* debugger */
            //let res = await SelfEmploymentActions.getSelfBusinessIncome(borrowerId, incomeInfoId);
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/ProceedsFromTransaction/DetailsOfProceedsFromTransaction`);
    }

    const GetCollateralAssetTypes = async () => { /* House,  Automobile, Financial, Account Other */
        let response = await AssetsActions.GetCollateralAssetTypes();
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                setCollateralAssetTypes(response.data);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }
    const GetSourceOfIncomeList = async () => {
        let response = await AssetsActions.GetAssetTypesByCategory(assetInfo?.assetCategoryId);
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                setAssetTypesByCategoryList(response.data);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    const updateFormValuesOnChange = async (data) => {
        let values: any = {};

        setTransactionProceeds(pre => {
            values = { ...pre, ...data }
            return values;
        });
        console.log(values);
        let response;
        let transactionProceedsLoan: TransactionProceedsFromLoanDTO;
        let transactionProceedsRealNonReal: TransactionProceedsFromRealAndNonRealEstateDTO;
        switch (selectedPropertyType) {
            case 12: /*Proceeds from a Loan */
                transactionProceedsLoan = new TransactionProceedsFromLoanDTO(selectedPropertyType, LocalDB.getLoanAppliationId(), borrowerId, assetCategoryId, values.expectedProceeds, values.selectedAssetTypeId, values.securedByAnAsset == 'Yes' ? true : false, values.assetDescription);

                if (values.borrowerAssetId)
                    transactionProceedsLoan.borrowerAssetId = values.borrowerAssetId;
                if (values.borrowerAssetId)

                    console.log(transactionProceedsLoan);

                if (values.assetType && values.assetType === 'Other') {
                    response = await TransactionProceedsActions.AddOrUpdateProceedsfromloanOther(transactionProceedsLoan)
                }
                else {

                    if (values.securedByAnAsset === 'No') {
                        transactionProceedsLoan.ColletralAssetTypeId = null;
                        transactionProceedsLoan.CollateralAssetDescription = null;
                    }
                    response = await TransactionProceedsActions.AddOrUpdateProceedsfromloan(transactionProceedsLoan)
                }

                break;
            case 13: /*Proceeds from selling non-real estate assets*/
                transactionProceedsRealNonReal = new TransactionProceedsFromRealAndNonRealEstateDTO(selectedPropertyType, LocalDB.getLoanAppliationId(), borrowerId, assetCategoryId, values.expectedProceeds, values.assetDescription);

                if (values.borrowerAssetId)
                    transactionProceedsRealNonReal.borrowerAssetId = values.borrowerAssetId;

                console.log(transactionProceedsRealNonReal);
                response = await TransactionProceedsActions.AddOrUpdateAssestsNonRealState(transactionProceedsRealNonReal)
                break;
            case 14: /*Proceeds from selling real estate*/
                transactionProceedsRealNonReal = new TransactionProceedsFromRealAndNonRealEstateDTO(selectedPropertyType, LocalDB.getLoanAppliationId(), borrowerId, assetCategoryId, values.expectedProceeds, values.assetDescription);

                if (values.borrowerAssetId)
                    transactionProceedsRealNonReal.borrowerAssetId = values.borrowerAssetId;

                console.log(transactionProceedsRealNonReal);
                response = await TransactionProceedsActions.AddOrUpdateAssestsRealState(transactionProceedsRealNonReal)
                break;
        }
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                //setAssetTypesByCategory(response.data);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
        console.log(values);
        setTransactionProceeds({});
        //NavigationHandler.moveNext()
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources`);
    }

    // return <Loader type="widget" data-testid="proceed-from-transictions-home" />

    return (
        <div>
            <Switch>
                <IsRouteAllowed
                    path={`${containerPath}/TypeOfProceedsFromTransaction`}
                    component={TypeOfProceedsFromTransaction}
                    assetTypesByCategoryList={assetTypesByCategoryList}
                    setSelectedPropertyType={setSelectedPropertyType}
                    selectedPropertyType={selectedPropertyType}
                    
                />

                <IsRouteAllowed
                    path={`${containerPath}/DetailsOfProceedsFromTransaction`}
                    component={DetailsOfProceedsFromTransaction}
                    setSelectedPropertyType={setSelectedPropertyType}
                    assetTypesByCategoryList={assetTypesByCategoryList}
                    selectedPropertyType={selectedPropertyType}
                    collateralAssetTypes={collateralAssetTypes}
                    updateFormValuesOnChange={updateFormValuesOnChange}
                    transactionProceedsDTO={transactionProceeds}
                    
                />

            </Switch>
        </div>
    )
}

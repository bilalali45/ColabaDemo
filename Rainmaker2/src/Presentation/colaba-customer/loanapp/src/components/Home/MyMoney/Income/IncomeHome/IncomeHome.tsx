import React, {useState, useContext, useEffect} from 'react'
import {Store} from '../../../../../store/store';
import {
    IncomeBorrowerProto,
    IncomeHomeBorrowerProto,
    IncomeProto,
    LoanInfoType
} from '../../../../../Entities/Models/types';
import {ErrorHandler} from '../../../../../Utilities/helpers/ErrorHandler';
import IncomeActions from '../../../../../store/actions/IncomeActions';
import {IncomeHomeList} from './IncomeHomeList';
import Loader from '../../../../../Shared/Components/Loader';
import {LoanApplicationActionsType} from "../../../../../store/reducers/LoanApplicationReducer";
import {LocalDB} from "../../../../../lib/LocalDB";
import {ApplicationEnv} from "../../../../../lib/appEnv";
import {PopupModal} from "../../../../../Shared/Components/Modal";
import BusinessActions from "../../../../../store/actions/BusinessActions";
import {NavigationHandler} from '../../../../../Utilities/Navigation/NavigationHandler';
import {BusinessActionTypes} from "../../../../../store/reducers/BusinessIncomeReducer";

import { MyMoneyIncomeSteps} from "../../../../../store/actions/LeftMenuHandler";
import {TenantConfigFieldNameEnum} from '../../../../../Utilities/Enumerations/TenantConfigEnums';
import {CommonType} from '../../../../../store/reducers/CommonReducer';
import { EmploymentIncomeActionTypes } from '../../../../../store/reducers/EmploymentIncomeReducer';
import { MilitaryIncomeActionTypes } from '../../../../../store/reducers/MilitaryIncomeReducer';

export const IncomeHome = () => {

    const {state, dispatch} = useContext(Store);
    const loanManager: any = state.loanManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;
    const [incomeHomeBorowerData, setIncomeHomeBorowerData] = useState<IncomeHomeBorrowerProto>();
    const [allIncomeCategories, setAllIncomeCategories] = useState([]);
    const [deletePopup, setDeletePopup] = useState<boolean>(false);
    
    useEffect(() => {
        getSourceOfIncomeList()
        fetchData()
    }, []);

    useEffect(() => {
        fetchData()
    }, [location.pathname]);


    const getSourceOfIncomeList = async () => {
        let response = await IncomeActions.GetSourceOfIncomeList();
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                setAllIncomeCategories(response.data);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    const fetchData = async () => {
        if (LocalDB.getLoanAppliationId() != undefined) {
            let response = await IncomeActions.GetMyMoneyHomeScreen(Number(LocalDB.getLoanAppliationId()));
            if (response) {
                if (ErrorHandler.successStatus.includes(response.statusCode)) {
                    setIncomeHomeBorowerData(response.data)
                }
                else {
                    ErrorHandler.setError(dispatch, response);
                }
            }
        }

        /* setIncomeHomeBorowerData({
             "totalMonthlyQualifyingIncome": 271.41,
             "borrowers": [
                 {
                     "borrowerId": 8002,
                     "borrowerName": "Syed Kazmi",
                     "ownTypeId": 1,
                     "ownTypeName": "Primary Contact",
                     "ownTypeDisplayName": "Primary Contact",
                     "incomes": [
                         {
                             "incomeName": "Test Business Name",
                             "incomeValue": 167,
                             "incomeId": 52,
                             "incomeTypeId": 3
                         },
                         {
                             "incomeName": "Test Business Name",
                             "incomeValue": 83,
                             "incomeId": 60,
                             "incomeTypeId": 3
                         },
                         {
                             "incomeName": "asdasd",
                             "incomeValue": 0.08,
                             "incomeId": 73,
                             "incomeTypeId": 3
                         },
                         {
                             "incomeName": "asdasd",
                             "incomeValue": 0.08,
                             "incomeId": 74,
                             "incomeTypeId": 3
                         },
                         {
                             "incomeName": "asd",
                             "incomeValue": 9.25,
                             "incomeId": 75,
                             "incomeTypeId": 3
                         },
                         {
                             "incomeName": "asd",
                             "incomeValue": 9.25,
                             "incomeId": 76,
                             "incomeTypeId": 3
                         },
                         {
                             "incomeName": "asdasd",
                             "incomeValue": 0.92,
                             "incomeId": 77,
                             "incomeTypeId": 3
                         },
                         {
                             "incomeName": "asd",
                             "incomeValue": 1.83,
                             "incomeId": 78,
                             "incomeTypeId": 3
                         }
                     ],
                     "monthlyIncome": 271.41
                 },
                 {
                     "borrowerId": 31050,
                     "borrowerName": "BOne BOne",
                     "ownTypeId": 2,
                     "ownTypeName": "Secondary Contact",
                     "ownTypeDisplayName": "Secondary Contact",
                     "incomes": [],
                     "monthlyIncome": 0
                 },
                 {
                     "borrowerId": 31051,
                     "borrowerName": "BTwo BTwo",
                     "ownTypeId": 2,
                     "ownTypeName": "Secondary Contact",
                     "ownTypeDisplayName": "Secondary Contact",
                     "incomes": [],
                     "monthlyIncome": 0
                 }
             ]
         }*/
    }


    const flushIncomeData = async () => {
        await dispatch({type: BusinessActionTypes.SetCurrentBusinessDetails, payload: {}});
        await dispatch({type: LoanApplicationActionsType.SetIncomeInfo, payload: {}});
        LocalDB.clearIncomeFromStorage();
    }

    const editIncome = async (incomeBorrowerProto: IncomeBorrowerProto, incomeProto: IncomeProto) => {
        console.log(incomeBorrowerProto)
        console.log(incomeProto)
        clearDataFromStore();
       // let title = `${StringServices.capitalizeFirstLetter(incomeBorrowerProto.borrowerName)}'s ${StringServices.capitalizeFirstLetter(incomeProto.incomeTypeDisplayName)} Income`;
        dispatch({type: CommonType.SetIncomePopupTitle, payload: incomeProto.incomeTypeDisplayName});

        let loanInfoObj: LoanInfoType = {
            ...loanInfo,
            borrowerId: incomeBorrowerProto.borrowerId,
            borrowerName: incomeBorrowerProto.borrowerName,
            ownTypeId: incomeBorrowerProto.ownTypeId,
        }

        flushIncomeData();
        await allIncomeCategories && allIncomeCategories?.filter((incomeType) => incomeType?.id === +incomeProto?.incomeTypeId)[0]

        await dispatch({type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj});
        await dispatch({
            type: LoanApplicationActionsType.SetIncomeInfo,
            payload: {incomeId: incomeProto.incomeId, incomeTypeId: incomeProto.employmentCategory.categoryId}
        });


        LocalDB.setIncomeId(String(incomeProto.incomeId));
        LocalDB.setBorrowerId(String(incomeBorrowerProto.borrowerId))


        if (incomeProto.employmentCategory.categoryId === 1 && !incomeProto.isCurrentIncome) {
            
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/EmploymentHistory/EmploymentHistoryDetails/PreviousEmployment`);
        } else {
            switch (incomeProto.employmentCategory.categoryId) {
                case 1:
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Employment/EmploymentIncome`)
                    break

                case 2:
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/SelfIncome/SelfEmploymentIncome`)
                    break

                case 3:
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Business/BusinessIncomeType`)
                    break

                case 4:
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Military/MilitaryIncome`)
                    break

                case 5:
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Retirement/RetirementIncomeSource`)
                    break

                case 6:
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Rental/RentalIncome`)
                    break;

                case 7:
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Other/OtherIncomeDetails`)
                    break;

                case 11:
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Other/OtherIncomeDetails`)
                    break
                default:
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/${incomeProto.employmentCategory?.categoryName}/${incomeProto.employmentCategory?.categoryName}IncomeType`)
            }

        }
    }

    const deleteIncome = async (incomeBorrowerProto: IncomeBorrowerProto, incomeProto: IncomeProto) => {
        console.log(incomeBorrowerProto);
        console.log(incomeProto);
        await BusinessActions.deleteEmploymentIncome(Number(LocalDB.getLoanAppliationId()), incomeBorrowerProto.borrowerId, incomeProto.incomeId);
        fetchData();
        //setDeletePopup(false);
    }
    const moveNext = async () => {
            if (NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PreviosEmployment)) {
                NavigationHandler.enableFeature(MyMoneyIncomeSteps.EmploymentAlert)
                NavigationHandler.enableFeature(MyMoneyIncomeSteps.EmploymentHistory)
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/EmploymentAlert`); // Removed because of Bug 3602: Tenenat Configuration - Employment History Section
            }
            else {
                NavigationHandler.disableFeature(MyMoneyIncomeSteps.EmploymentAlert)
                NavigationHandler.disableFeature(MyMoneyIncomeSteps.EmploymentHistory)
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeReview`); // Removed because of Bug 3602: Tenenat Configuration - Employment History Section
            }
            // NavigationHandler.moveNext(); // Bug 3602: Tenenat Configuration - Employment History Section
        
    }

    const addIncome = async (incomeBorrowerProto: IncomeBorrowerProto) => {
        let loanInfoObj: LoanInfoType = {
            ...loanInfo,
            borrowerId: incomeBorrowerProto.borrowerId,
            borrowerName: incomeBorrowerProto.borrowerName,
            ownTypeId: incomeBorrowerProto.ownTypeId,
        }
        await dispatch({type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj});
        await dispatch({type: LoanApplicationActionsType.SetIncomeInfo, payload: {}});
        await dispatch({type: BusinessActionTypes.SetCurrentBusinessDetails, payload: {}});
        clearDataFromStore();
        LocalDB.setBorrowerId(String(incomeBorrowerProto.borrowerId))
        LocalDB.clearIncomeFromStorage();
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/IncomeSources`);
    }

    const clearDataFromStore = () =>{
        dispatch({type: EmploymentIncomeActionTypes.SetEmployerInfo,payload: undefined});
        dispatch({type: EmploymentIncomeActionTypes.SetEmployerAddress,payload: undefined});
        dispatch({type: EmploymentIncomeActionTypes.SetWayOfIncome,payload: undefined});
        dispatch({type: EmploymentIncomeActionTypes.SetAdditionIncome,payload: undefined});
        dispatch({type: MilitaryIncomeActionTypes.SetMilitaryServiceAddress, payload: undefined});
        dispatch({type: MilitaryIncomeActionTypes.SetMilitaryEmployer, payload: undefined});
        dispatch({ type: MilitaryIncomeActionTypes.SetMilitaryPaymentMode, payload: undefined });
    }
  
    const shouldDisableButton = () => {
        let toggleButton = incomeHomeBorowerData?.borrowers.some(e => e.ownTypeId === 1 && e.monthlyIncome === 0)
        console.log("toggleButton  ", toggleButton);
        return toggleButton;
    }
    return (
        <React.Fragment>
            {
                incomeHomeBorowerData ? <>
                    <IncomeHomeList incomeHomeBorowerData={incomeHomeBorowerData} editIncome={editIncome}
                                    deleteIncome={deleteIncome} addIncome={addIncome}
                                    shouldDisableButton={shouldDisableButton} moveNext={moveNext}
                                    data-testid="income-box"/>
                    <PopupModal
                        modalHeading={`Remove  ?`}
                        modalBodyData={<p>Are you sure you want to delete from this
                            loan application? The data you've entered will not be recoverable.</p>}
                        modalFooterData={<button className="btn btn-min btn-primary" onClick={() => {
                            console.log("delete")
                        }}>Yes</button>}
                        show={deletePopup}
                        handlerShow={() => setDeletePopup(!deletePopup)}
                        dialogClassName={`delete-borrower-popup`}
                    ></PopupModal>
                </> : <Loader type="widget" data-testid="income-home"/>
            }
        </React.Fragment>
    )

}
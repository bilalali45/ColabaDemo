import React, { useState, useContext } from "react";
import Dropdown from "react-bootstrap/Dropdown";
import {
  IncomeAddIcon,
  IconEmployment,
  DropdownEdit,
  DropdownDelete,
  MoreIcon,
  IconBusiness,
  IconMilitaryPay,
  IconRetirement,
  IconRental,
  IconOther,
  IconSelfEmployment
} from "../../../../../../Shared/Components/SVGs";
import {
  BorrowerEmploymentHistory,
  BorrowerIncome,
  BorrowersEmploymentHistoryList,
  EmploymentHistory,
  Income,
} from "../../../../../../Entities/Models/EmploymentHistory";
import { NavigationHandler } from "../../../../../../Utilities/Navigation/NavigationHandler";
import { ApplicationEnv } from "../../../../../../lib/appEnv";
import { LoanInfoType } from "../../../../../../Entities/Models/types";
import { Store } from "../../../../../../store/store";
import { LoanApplicationActionsType } from "../../../../../../store/reducers/LoanApplicationReducer";
import EmploymentHistoryActions from "../../../../../../store/actions/EmploymentHistoryActions";
import { LocalDB } from "../../../../../../lib/LocalDB";
import { ErrorHandler } from "../../../../../../Utilities/helpers/ErrorHandler";
import { MyMoneyIncomeSteps } from "../../../../../../store/actions/LeftMenuHandler";
import { TenantConfigFieldNameEnum } from "../../../../../../Utilities/Enumerations/TenantConfigEnums";
import { EmploymentHistoryActionTypes } from "../../../../../../store/reducers/EmploymentHistoryReducer";

type PreviousEmploymentListProps = {
  borrowerEmploymentHistory: BorrowerIncome;
  setTabKey: Function;
  deleteIncome: Function;
  editIncome: Function;
};

export const PreviousEmploymentList = ({
  borrowerEmploymentHistory,
  setTabKey,
  deleteIncome,
  editIncome,
}: PreviousEmploymentListProps) => {
  const { state, dispatch } = useContext(Store);
  const { loanInfo }: any = state.loanManager;
  const { borrowersIncome }: any = state.employmentHistory;
  const [btnClick, setBtnClick] = useState<boolean>(false);
  const [showError, setShowError] = useState<boolean>(false);
  
  const getTenor = (employmentHistory: EmploymentHistory) => {
    const { startDate, endDate }: EmploymentHistory = employmentHistory;

    if(startDate){
      const startDateMonth =
      startDate &&
      new Date(startDate).toLocaleString("default", { month: "long" });
    if (endDate) {
      const endDateMonth =
        endDate &&
        new Date(endDate).toLocaleString("default", { month: "long" });
      return `From ${startDateMonth} ${
        startDate && new Date(startDate).getFullYear()
      } to ${endDateMonth} ${endDate && new Date(endDate).getFullYear()}`;
    } else
      return `From ${startDateMonth} ${
        startDate && new Date(startDate).getFullYear()
      } `;
    }
    return null;
  };

  const addIncome = async () => {
    setShowError(false);
    let loanInfoObj: LoanInfoType = {
      ...loanInfo,
      borrowerId: borrowerEmploymentHistory.borrowerId,
      borrowerName: borrowerEmploymentHistory.borrowerName,
      ownTypeId: null,
    };
    await dispatch({
      type: LoanApplicationActionsType.SetLoanInfo,
      payload: loanInfoObj,
    });
    await dispatch({
      type: LoanApplicationActionsType.SetIncomeInfo,
      payload: {},
    });
    await dispatch({
      type: EmploymentHistoryActionTypes.SetIncomeInformation,
      payload: {},
    });
    dispatch({
      type: EmploymentHistoryActionTypes.SetPreviousEmployerInfo,
      payload: undefined,
    });
    dispatch({
      type: EmploymentHistoryActionTypes.SetPreviousEmployerAddress,
      payload: undefined,
    });
    dispatch({
      type: EmploymentHistoryActionTypes.SetPreviousEmploymentIncome,
      payload: undefined,
    });
    // await dispatch({type: BusinessActionTypes.SetCurrentBusinessDetails, payload: {}});
    LocalDB.setBorrowerId(String(borrowerEmploymentHistory.borrowerId));
    NavigationHandler.navigateToPath(
      `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/EmploymentHistory/EmploymentHistoryDetails/PreviousEmployment`
    );
  };
  const getBorrowersEmploymentHistory = async () => {
    let res: any = await EmploymentHistoryActions.getBorrowersEmploymentHistory(
      +LocalDB.getLoanAppliationId()
    );

    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        const { requiresHistory }: BorrowersEmploymentHistoryList = res.data;

        const employmentHistory = res.data.borrowerEmploymentHistory;
        if (requiresHistory) {
          NavigationHandler.enableFeature(MyMoneyIncomeSteps.EmploymentAlert);
          NavigationHandler.enableFeature(MyMoneyIncomeSteps.EmploymentHistory);
          let borrowersId: number[] = [];
          employmentHistory &&
            employmentHistory?.length &&
            employmentHistory.map((borrower: BorrowerEmploymentHistory) => {
              borrowersId.push(borrower.borrowerId);
            });
          return borrowersId;
        }
      } else {
        ErrorHandler.setError(dispatch, res);
      }
    }
    return null;
  };

  const checkCurrentTabValidity = async (borrowersId: number[]) => {
    if (
      borrowersId &&
      borrowersId.includes(borrowerEmploymentHistory?.borrowerId)
    ) {
      setTabKey(borrowerEmploymentHistory?.borrowerId?.toString());
      setShowError(true);
      return false;
    }
    return true;
  };
  const onSubmit = async () => {
    let currentBorrowerTabIdx = borrowersIncome
      .map(function (b) {
        return b.borrowerId;
      })
      .indexOf(borrowerEmploymentHistory?.borrowerId);
    let invalidTabs: number[] | null = await getBorrowersEmploymentHistory();
    if (invalidTabs) {
      let currentTabValidity: boolean = await checkCurrentTabValidity(
        invalidTabs
      );
      if (currentTabValidity) {
        if (checkIfLastTab(currentBorrowerTabIdx)) {
          const tabValidity = await checkAllTabValidations(invalidTabs);
          if (tabValidity) {
            if (!btnClick) {
              setBtnClick(true);
              setShowError(false);
              NavigationHandler.disableFeature(
                MyMoneyIncomeSteps.EmploymentAlert
              );
              NavigationHandler.disableFeature(
                MyMoneyIncomeSteps.EmploymentHistory
              );
              NavigationHandler.moveNext();
            }
          } else setTabKey(invalidTabs[0]);
        } else
          setTabKey(
            borrowersIncome[currentBorrowerTabIdx + 1]?.borrowerId.toString()
          );
      }
    } else {
      NavigationHandler.moveNext();
     }
    
  };

  const checkIfLastTab = (currentTab: number) => {
    if (borrowersIncome.length - 1 === currentTab) return true;
    return false;
  };

  const checkAllTabValidations = async (invalidTabIds: number[]) => {
    if (invalidTabIds && invalidTabIds.length) {
      for (let id = invalidTabIds.length - 1; id >= 0; id--) {
        let field = document.querySelector(
          "#pre-emp-tab-next-" + invalidTabIds[id]
        ) as HTMLButtonElement;
        field.click();
      }
      return false;
    }
    return true;
  };

  const getIcon = (displayName: string) => {
    let icon: JSX.Element = <></>;
    switch (displayName) {
      case 'Employment':
        icon = <IconEmployment />;
        break;

      case 'Self Employment / Independent Contractor':
        icon = <IconSelfEmployment />;
        break;

      case 'Business':
        icon = <IconBusiness />;
        break;

      case 'Military Pay':
        icon = <IconMilitaryPay />;
        break;

      case 'Retirement':
        icon = <IconRetirement />;
        break;

      case 'Rental':
        icon = <IconRental/>;
        break;

      case 'Other':
        icon = <IconOther />;
        break;

      // case 7:
      //   icon = <IconOther />;
      //   break;
      // case 8:
      //   icon = <IconRetirement />;
      // break;

      // case 11:
      //   icon = <IconOther />;
      //   break;
      default:
        icon = <IconOther />;
    }
    return icon;
  };


    const skipContinueHandler = () =>{
      let currentBorrowerTabIdx = borrowersIncome
      .map(function (b) {
        return b.borrowerId;
      })
      .indexOf(borrowerEmploymentHistory?.borrowerId);

        if (checkIfLastTab(currentBorrowerTabIdx)) {
          NavigationHandler.navigateToPath(
            `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeReview`
          );
        }
          else
          setTabKey(
            borrowersIncome[currentBorrowerTabIdx + 1]?.borrowerId.toString()
          );
     
    }


    console.log('borrowerEmploymentHistory ', borrowerEmploymentHistory);
  
  return (
    <div className="f-cWrap">
      <div className="form-group">
      <h4>{`Where else has ${borrowerEmploymentHistory?.borrowerName} worked in the last two years?`}</h4>
      </div>
      <div className="p-body-tabs">
          {borrowerEmploymentHistory?.borrowerIncomes &&
            borrowerEmploymentHistory?.borrowerIncomes?.length === 0 && (
              <div>
                <p>No Data to Display</p>
              </div>
            )}
          {borrowerEmploymentHistory?.borrowerIncomes &&
            borrowerEmploymentHistory?.borrowerIncomes?.length > 0 &&
            borrowerEmploymentHistory?.borrowerIncomes?.map(
              (employmentHist: Income) => (
                <div className="e-history-list-wrap">
                  <div className="e-h-l">
                    <div className="ehl-icon">
                      {getIcon(employmentHist?.employmentCategory?.categoryDisplayName)}
                    </div>
                    <div className="ehl-content">
                      <div className="ehl-title">
                        <h5>{employmentHist.employerName}</h5>
                      </div>
                      <div className="ehl-date">{getTenor(employmentHist)}</div>
                    </div>

                    <div className="ehl-actions">
                      <Dropdown drop="left" className="ehl-actions-dropdown">
                        <Dropdown.Toggle as="div" className="Vmore-icon">
                          <span className="m-icon">
                            <MoreIcon />
                          </span>
                        </Dropdown.Toggle>

                        <Dropdown.Menu>
                          <Dropdown.Item
                            onClick={() => {
                              setShowError(false);
                              editIncome(
                                borrowerEmploymentHistory?.borrowerId,
                                borrowerEmploymentHistory?.borrowerName,
                                borrowerEmploymentHistory?.ownType?.ownTypeId,
                                employmentHist
                              );
                            }}>
                            <DropdownEdit /> Edit
                          </Dropdown.Item>
                          <Dropdown.Item
                            onClick={() => {
                              setShowError(false);
                              deleteIncome(
                                borrowerEmploymentHistory.borrowerId,
                                employmentHist
                              );
                            }}>
                            <DropdownDelete /> Delete
                          </Dropdown.Item>
                        </Dropdown.Menu>
                      </Dropdown>
                    </div>
                  </div>
                </div>
              )
            )}
          {showError && (
            <span
              className="form-error"
              role="alert"
              id={"prev-emp-error"}
              data-testid={"prev-emp-error"}>
              {"Please provide at least two years of employment history"}
            </span>
          )}
      </div>
      <div className="p-footer-tabs">
        <div className="left-col">
          <div
            className="add-i-source-link"
            onClick={() => {
              addIncome();
            }}>
            <span className="icon">
              <IncomeAddIcon />
            </span>
            Add Previous Employment
          </div>


          {!NavigationHandler.isFieldRequired(
            TenantConfigFieldNameEnum.PreviosEmployment,
            false
          ) && (
            <button
              className="btn btn-inline m-left"
              onClick={skipContinueHandler}>
              Skip and Continue
            </button>
           )}
        </div>
        <div className="right-col">
          <button
            className="btn btn-primary"
            type="button"
            id={`pre-emp-tab-next-${borrowerEmploymentHistory?.borrowerId}`}
            data-testid={`pre-emp-tab-${borrowerEmploymentHistory?.borrowerId}`}
            onClick={onSubmit}>
            Next
          </button>
        </div>
      </div>
    </div>
  );
};

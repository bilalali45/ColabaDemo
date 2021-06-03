import React, { Fragment, useContext, useEffect, useState } from "react";
import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";
import IconRadioSnipit from "../../../../Shared/Components/IconRadioSnipit";
import { ArtWorkPurchaseProcess } from "../../../../Shared/Components/ArtWork";
import { LoanGoalType, LoanInfoType } from "../../../../Entities/Models/types";
import GettingStartedActions from "../../../../store/actions/GettingStartedActions";
import { MyNewMortgageSteps } from "../../../../store/actions/LeftMenuHandler";
import { LocalDB } from "../../../../lib/LocalDB";
import { LoanPurposeEnum } from "../../../../Utilities/Enum";
import { Store } from "../../../../store/store";
import { LoanApplicationActionsType } from "../../../../store/reducers/LoanApplicationReducer";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import {
  IconPurchaseProperty, IconRefinanceMortgage, IconPreApproval,
  IconPUC, IconResearchingLoanOptions, IconGoingToOpenHouse
} from "../../../../Shared/Components/SVGs";
import Loader from "../../../../Shared/Components/Loader";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { TenantConfigFieldNameEnum } from "../../../../Utilities/Enumerations/TenantConfigEnums";
import { LoanGoalNameEnum } from "../../../../Utilities/Enumerations/LoanGoalEnum";
import {MyAssetsSteps} from '../../../../Utilities/Navigation/navigation_config/MyMoney';

export const PurchaseProcessState = () => {
  if (NavigationHandler.isNavigatedBack() == true) {
    NavigationHandler.moveBack();
    NavigationHandler.resetNavigationBack();
    return null;
  }

  const { state, dispatch } = useContext(Store);
  const loanManager: any = state.loanManager;
  const loanInfo: LoanInfoType = loanManager.loanInfo;
  const [loanGoals, setloanGoals] = useState<LoanGoalType[]>();
  const [titleString, setTitlestring] = useState<string>("");
  const [btnClick, setBtnClick] = useState<boolean>(false);
  const [proceedToNext, setProceedToNext] = useState<boolean>(false);
  const [isPurchaseProcessOff, setIsPurchaseProcessOff] = useState<boolean>(false);
  
  useEffect(() => {
    let loanApplicationId = LocalDB.getLoanAppliationId();
    if (loanApplicationId) {
      getLoanGoal(+loanApplicationId);
    } else {
      getAllLoanGoal(loanInfo ? loanInfo?.loanPurposeId : +(LocalDB.getLoanPurposeId()));
    }
  }, []);

  useEffect(() => {
    let purposeId = loanInfo ? loanInfo?.loanPurposeId : +(LocalDB.getLoanPurposeId());
    switch (purposeId) {
      case LoanPurposeEnum.Purchase:
        if (NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.WhereAreYouInPurchaseProcess)) {
          setTitlestring(
            "That's awesome. Where are you in the purchase process?"
          );
        }
        else {
          setTitlestring(
            "Please wait..."
          );
          // NavigationHandler.disableFeature(GettingStartedSteps.PurchaseProcessState);
          setIsPurchaseProcessOff(true);
        }
        break;
      case LoanPurposeEnum.Refinance:
        setTitlestring("Great. Please let us know why you want to refinance.");
        break;
      case LoanPurposeEnum.CashOut:
        setTitlestring("What are your goals?");
        break;
      default:
        setTitlestring("");
        break;
    }
  }, []);
  

  useEffect(() => {
    if (isPurchaseProcessOff == true && loanGoals) {
      var lg = loanGoals?.filter(x => x.name == LoanGoalNameEnum.PreApproval)[0];
      if (lg) {
        createLoanGoal(lg.id, lg.name);
      }
    }
  }, [proceedToNext, loanGoals])

  const getAllLoanGoal = async (purposeId: number) => {
    let response = await GettingStartedActions.getAllLoanGoal(
      purposeId
    );
    if (response) {
      if (ErrorHandler.successStatus.includes(response.statusCode)) {
        setloanGoals(response.data);
      } else {
        ErrorHandler.setError(dispatch, response);
      }
    }
  };

  const getLoanGoal = async (loanApplicationId: number) => {
    let response = await GettingStartedActions.getLoanGoal(loanApplicationId);
    if (response) {
      if (ErrorHandler.successStatus.includes(response.statusCode)) {
        const { loanPurpose, loanGoal } = response.data;
       
        getAllLoanGoal(loanPurpose);
        let loanInfoObj: LoanInfoType = {
          loanApplicationId: loanApplicationId,
          loanPurposeId: loanPurpose,
          loanGoalId: loanGoal,
          borrowerId: loanInfo?.borrowerId,
          ownTypeId: loanInfo?.ownTypeId,
          borrowerName: loanInfo?.borrowerName
        };
        dispatch({
          type: LoanApplicationActionsType.SetLoanInfo,
          payload: loanInfoObj,
        });
        LocalDB.setLoanGoalId(loanGoal);
        LocalDB.setLoanPurposeId(loanPurpose);
        setProceedToNext(true);
      } else {
        ErrorHandler.setError(dispatch, response);
      }
    }
  };

  const createLoanGoal = async (loanGoalId: number, title?: string) => {
    if (!btnClick) {
      setBtnClick(true);

      if (title !== 'Pre-Approval') {
        NavigationHandler.disableFeature(MyNewMortgageSteps.SubjectPropertyIntend);
      } else {
        NavigationHandler.enableFeature(MyNewMortgageSteps.SubjectPropertyIntend);
      }

      if (title == 'Property Under Contract') {
        NavigationHandler.enableFeature(MyAssetsSteps.EarnestMoneyDeposit);
      } else {
        NavigationHandler.disableFeature(MyAssetsSteps.EarnestMoneyDeposit);
      }

      let loanApplicationId = LocalDB.getLoanAppliationId();
      let response = await GettingStartedActions.createLoanGoal(
        loanApplicationId ? +loanApplicationId : null,
        loanGoalId,
        loanInfo?.loanPurposeId,
        NavigationHandler.getNavigationStateAsString()
      );
      if (response) {
        if (ErrorHandler.successStatus.includes(response.statusCode)) {
          if (response.data) {
            LocalDB.setLoanAppliationId(String(response.data));
            LocalDB.setLoanGoalId(String(loanGoalId));
            dispatch({
              type: LoanApplicationActionsType.SetLoanInfo,
              payload: {
                ...loanInfo,
                loanApplicationId: response.data,
                loanGoalId: loanGoalId,
              },
            });
          }
        } else {
          ErrorHandler.setError(dispatch, response);
        }
      }

      NavigationHandler.moveNext();
    }
  };

  const renderAllLoanGoal = () => {

    const icons = {
      'I Want To Purchase A New Property': <IconPurchaseProperty />,
      'I Want To Refinance A Mortgage': <IconRefinanceMortgage />,
      'Property Under Contract': <IconPUC />,
      'Pre-Approval': <IconPreApproval />,
      'Researching Loan Options': <IconResearchingLoanOptions />,
      'Going to Open Houses': <IconGoingToOpenHouse />,
      'Going to Open House': <IconGoingToOpenHouse />
    }

    return (
      <>
        {isPurchaseProcessOff == false && loanGoals?.map((item: LoanGoalType) => {
          return (
            <IconRadioSnipit
              title={item.name}
              id={item.id}
              icon={icons[item.name]}
              handlerClick={createLoanGoal}
              className={loanInfo?.loanGoalId === item.id ? "active" : ""}
            />
          );
        })}
      </>
    );
  };

  return (
    <div data-testid="purchase-process-state">
      {
        isPurchaseProcessOff == false &&
        <div className="compo-purchaseP-state fadein">
          <Fragment>
            <PageHead
              title="Getting Started"
              showBackBtn={LocalDB.getLoanAppliationId() ? false : true}
            />
          </Fragment>

          <Fragment>
            <TooltipTitle title={titleString} />
          </Fragment>
          {!loanGoals &&
            <div><Loader type="widget" /></div>
          }
          {loanGoals &&
            <div data-testid="goal-div" className="wrap50">{renderAllLoanGoal()}</div>
          }


          <div data-testid="art-work" className="bot-artwork-wrap">
            <ArtWorkPurchaseProcess />
          </div>

        </div>
      }

    </div>

  );
};

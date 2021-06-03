import React, { Fragment, useContext, useEffect, useState } from "react";
import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";
import IconRadioSnipit from "../../../../Shared/Components/IconRadioSnipit";
import { ArtWorkHowCanWeHelp } from "../../../../Shared/Components/ArtWork";
import GettingStartedActions from "../../../../store/actions/GettingStartedActions";
import { LoanInfoType, LoanPurposeType } from "../../../../Entities/Models/types";
import { Store } from "../../../../store/store";
import {LoanApplicationActionsType} from '../../../../store/reducers/LoanApplicationReducer';

import { LocalDB } from "../../../../lib/LocalDB";

import { LoanPurposeEnum } from "../../../../Utilities/Enum";
import { IconPurchaseProperty, IconRefinanceMortgage,SinglePropertyIcon,IconPreApproval } from "../../../../Shared/Components/SVGs";
import Loader from "../../../../Shared/Components/Loader";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const HowCanWeHelp = () => {
  const { state, dispatch } = useContext(Store);
  const loanManager: any = state.loanManager;
  const loanInfo: LoanInfoType = loanManager.loanInfo;
  const [btnClick, setBtnClick] = useState<boolean>(false);
  const [loanPurpose, setloanPurpose] = useState<LoanPurposeType[]>();
  
  useEffect(() => {   
      getAllLoanpurpose(); 
  }, []);

  const getAllLoanpurpose = async () => {
    let response = await GettingStartedActions.getAllLoanPurpose();
    if(response){
      if (ErrorHandler.successStatus.includes(response.statusCode)) {
        setloanPurpose(response.data); 
      }else{
        ErrorHandler.setError(dispatch, response);
      }
    } 
  };

  const loanPurposeHandler = async (purposeId: number) => {  
    if(!btnClick){
      setBtnClick(true);
      dispatch({type: LoanApplicationActionsType.SetLoanInfo, payload: {...loanInfo, loanPurposeId: purposeId}});
      LocalDB.setLoanPurposeId(String(purposeId));
      // if(purposeId == LoanPurposeEnum.Purchase && !NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.WhereAreYouInPurchaseProcess)){
      //   NavigationHandler.disableFeature(GettingStartedSteps.PurchaseProcessState);
      // }
      // else{
        // NavigationHandler.enableFeature(GettingStartedSteps.PurchaseProcessState);
      // }
      // LeftMenuHandler.makeDecision(Decisions.PurchaseProcessState);
      NavigationHandler.moveNext();
    }     
  };

  const renderAllLoanPurpose = () => {

    const icons = {
      'I Want To Purchase A New Property': (<IconPurchaseProperty/>),
      'I Want To Refinance A Mortgage': (<IconRefinanceMortgage/>),
      'Property Under Contract': (<SinglePropertyIcon/>),
      'Pre-Approval': (<IconPreApproval/>)
    }

    return (
      <>
        {loanPurpose?.map((item: LoanPurposeType) => {
          return (
            <div key={item.id}>
              <IconRadioSnipit
                id={item.id}
                title={item.name}
                icon={icons[item.name]}
                handlerClick={loanPurposeHandler}
                className = {LoanPurposeEnum.Refinance === item.id ? "disabled" : loanInfo?.loanPurposeId === item.id ? "active": ""}
              />
            </div>
          );
        })}
      </>
    );
  };

  return (
    <div className="compo-hcwh fadein">
      <Fragment>
        <PageHead
          title="Getting Started"
        />
      </Fragment>

      <Fragment>
        <TooltipTitle title="How can we help you?" />
      </Fragment>
            {!loanPurpose &&
              <div><Loader type="widget"/></div>
            }
            {loanPurpose &&
              <div data-testid="purpose-list" className="wrap50">{renderAllLoanPurpose()}</div>
            }
      

      <div data-testid="art-work" className="bot-artwork-wrap">
        <ArtWorkHowCanWeHelp />
      </div>

      

    </div>
  );
};

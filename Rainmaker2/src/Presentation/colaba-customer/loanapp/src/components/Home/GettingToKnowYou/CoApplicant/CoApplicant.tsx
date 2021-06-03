import React, { useContext, useState } from 'react';

import PageHead from '../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../Shared/Components/TooltipTitle';

import InputRadioBox from '../../../../Shared/Components/InputRadioBox';
import LeftMenuHandler, { Decisions } from '../../../../store/actions/LeftMenuHandler';

import { ApplicationEnv } from '../../../../lib/appEnv';
import { LoanInfoType, PrimaryBorrowerInfo } from '../../../../Entities/Models/types';
import { LocalDB } from '../../../../lib/LocalDB';
import { LoanApplicationActionsType } from '../../../../store/reducers/LoanApplicationReducer';
import { Store } from '../../../../store/store';
import {StringServices} from "../../../../Utilities/helpers/StringServices";
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';

export const CoApplicant = () => {
    
    const {state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;
    const primaryBorrowerInfo: PrimaryBorrowerInfo = loanManager.primaryBorrowerInfo
    
    const [selectedOption, setSelectedOption] = useState<boolean| null>(null);

    return (
        <div className="compo-abt-yourSelf fadein">
            <PageHead title="Personal Information" />
            <TooltipTitle title={`Thanks ${ primaryBorrowerInfo?.name && StringServices.capitalizeFirstLetter(primaryBorrowerInfo && primaryBorrowerInfo.name)}. Let us know if you have a co-applicant on the loan.`} />

            <div className="comp-form-panel colaba-form">
                <div className="row">
                    <div className="col-md-12">
                        <div className="form-group">
                            <h4>Are you applying for this loan with another applicant?</h4>
                            <div className="clearfix">
                            <InputRadioBox
                            data-testid="coapplicant_yes"
                                id="coapplicant_yes"
                                className=""
                                name="CoApplicant"                              
                                value={"Yes"}
                                onChange={()=> {setSelectedOption(true)}}
                            >Yes</InputRadioBox>
                            </div>

                            <div className="clearfix">
                            <InputRadioBox
                            data-testid="coapplicant_no"
                                id="coapplicant_no"
                                className=""
                                name="CoApplicant"
                                onChange={()=> {setSelectedOption(false)}}
                                value={"No"}
                            >No</InputRadioBox>
                            </div>
                            
 
                        </div>

                    </div>
                </div>

                 <div className="form-footer">

                 <button 
                     disabled = {selectedOption === undefined ? true : false}
                     className="btn btn-primary" 
                     data-testid="coapplicant-submit" id="coapplicant-submit" onClick={async () => { 
                         
                     if(selectedOption){
                        let loanInfoObj: LoanInfoType = {
                            ...loanInfo,
                            borrowerId: null,
                            ownTypeId: 2
                        }
                        await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
                        LeftMenuHandler.addDecision(Decisions.AddBrowerFromCoApplicant);  
                        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingToKnowYou/AboutYourself`);
                        LocalDB.clearBorrowerFromStorage();           
                       
                     }else{
                       NavigationHandler.moveNext();
                     }
                 }} > 
                 {"Save & Continue"} 
                 </button>
                </div>

            </div>

        </div>
    )
}

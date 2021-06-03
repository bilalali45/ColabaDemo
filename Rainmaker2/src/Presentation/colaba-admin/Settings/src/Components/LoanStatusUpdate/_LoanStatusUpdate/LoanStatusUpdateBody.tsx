import React,{useContext, useState} from 'react'
import EmailNotFound from '../../Shared/EmailNotFound';
import { LoanStatus } from '../../../Entities/Models/LoanStatusUpdate';
import { Store } from '../../../Store/Store';
import { WidgetLoader } from '../../Shared/Loader';
import { ColabaStatus } from './ColabaStatus'
import { ColabaStatusEmailTemplate } from './ColabaStatusEmailTemplate'
type props = {
    
    
  };
export const LoanStatusUpdateBody = ({}: props) => {
    
    const { state, dispatch } = useContext(Store);
    const loanStatusManager: any = state.loanStatusManager;
    const selectedLoanStatus: LoanStatus = loanStatusManager.selectedLoanStatus;
  
    const [disableSaveBtn, setDisableSaveBtn] = useState<boolean>(false);
    const [showEmailContentScreen, setShowEmailContentScreen] = useState<boolean>(false);
    const [error, setError] = useState<string>('');

    return (
        <>
        <div className="col-md-5 no-padding loan-status-update-body--left" data-testid="LoanStatusUpdateBodyLeft">
                <ColabaStatus
                 setDisableSaveBtn= {setDisableSaveBtn}
                 setShowEmailContentScreen = {setShowEmailContentScreen}
                 error = {error}
                 setError = {setError}
                 />
                
        </div>
            <div className={`col-md-7 no-padding loan-status-update-body--right ${(!showEmailContentScreen || selectedLoanStatus?.toStatusId === 0) ? 'haveNothing':''}`} data-testid="LoanStatusUpdateBodyRight">
                { showEmailContentScreen && selectedLoanStatus?.toStatusId != 0 &&
                    <ColabaStatusEmailTemplate 
                    disableSaveBtn = {disableSaveBtn}
                    setDisableSaveBtn= {setDisableSaveBtn}
                    setError = {setError}
                />}
                { (!showEmailContentScreen || selectedLoanStatus?.toStatusId === 0) &&
                <EmailNotFound heading="Loan status Email not Found"></EmailNotFound>
                }
                
            </div>
        </>
    )
}

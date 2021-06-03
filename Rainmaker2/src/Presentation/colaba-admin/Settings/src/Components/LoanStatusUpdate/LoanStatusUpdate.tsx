import React, {useContext, useEffect, useRef, useState} from 'react';
import LoanStatusUpdateModel from '../../Entities/Models/LoanStatusUpdate';
import { LoanStatusUpdateActions } from '../../Store/actions/LoanStatusUpdateActions';
import { Store } from '../../Store/Store';
import ContentBody from '../Shared/ContentBody';
import DisabledWidget from '../Shared/DisabledWidget';
import { LoanStatusUpdateBody } from './_LoanStatusUpdate/LoanStatusUpdateBody';
import { LoanStatusUpdateHeader } from './_LoanStatusUpdate/LoanStatusUpdateHeader';
import {LoanStatusUpdateActionsType} from '../../Store/reducers/LoanStatusUpdateReducer';
import { WidgetLoader } from '../Shared/Loader';

type Props = {
    backHandler?: Function
}

 const LoanStatusUpdate = ({backHandler}: Props) => {
    const {state, dispatch} = useContext(Store);
    const loanStatusManager: any = state.loanStatusManager;
    const loanStatus: LoanStatusUpdateModel  = loanStatusManager.loanStatusData;
    const [enabled, setEnabled] = useState(false);

    useEffect(() => {
        fetchLoanStatusUpdate();
        return ()=>{
            console.log('Loan update status Unmounting...')
            dispatch({type: LoanStatusUpdateActionsType.SetLoanStatusData, payload: undefined});
            dispatch({type: LoanStatusUpdateActionsType.SetSelectedLoanStatus, payload: undefined});
        }
    },[])

    const fetchLoanStatusUpdate = async () => {
      let loanStatusData: LoanStatusUpdateModel | undefined = await LoanStatusUpdateActions.fetchLoanStatusUpdate();
      dispatch({type: LoanStatusUpdateActionsType.SetLoanStatusData, payload: loanStatusData});
      if(loanStatusData?.isActive != undefined){
        setEnabled(loanStatusData?.isActive);
      }   
    }

    const setAllEnableDisableLoanStatus = async (isActive: boolean) => {
       let result = await LoanStatusUpdateActions.updateAllEnableDisableLoanStatusEmail(isActive);
    }
    
    const toggleEnabled = async ()=>{
        setEnabled(!enabled);
        setAllEnableDisableLoanStatus(!enabled);
        let loanStatusData: LoanStatusUpdateModel | undefined = await LoanStatusUpdateActions.fetchLoanStatusUpdate();
        dispatch({type: LoanStatusUpdateActionsType.SetLoanStatusData, payload: loanStatusData});
        dispatch({type: LoanStatusUpdateActionsType.SetSelectedLoanStatus, payload: {}});
    }

  if(!loanStatus){
    return <WidgetLoader reduceHeight="52px"/>
  } 

    return (
        <div className="settings__loan-status-update" data-testid="loanStatusUpdate"> 
            <LoanStatusUpdateHeader enable={enabled} handlerEnabled={()=>toggleEnabled()}/>
            
            <ContentBody className={`loan-status-update-body ${enabled ? '':'disabled flex flex-center'}`}>
                {enabled &&
                    <LoanStatusUpdateBody/>
                }
                {!enabled &&
                    <DisabledWidget text="Loan Status Update emails are disabled"/>
                }
            </ContentBody>
        </div>
    )
}

export default LoanStatusUpdate;

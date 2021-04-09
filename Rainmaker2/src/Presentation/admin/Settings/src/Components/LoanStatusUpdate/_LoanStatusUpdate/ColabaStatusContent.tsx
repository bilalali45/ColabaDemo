import React, { useContext, useEffect, useState } from 'react';
import LoanStatusUpdateModel, { LoanStatus } from '../../../Entities/Models/LoanStatusUpdate';
import { Store } from '../../../Store/Store';
import ContentBody from '../../Shared/ContentBody';
import {LoanStatusUpdateActionsType} from '../../../Store/reducers/LoanStatusUpdateReducer';
import { AlertBox } from '../../Shared/AlertBox';
import { enableBrowserPrompt, disableBrowserPrompt } from '../../../Utils/helpers/Common';

type ColabaStatusContentProps = {
    activeColabaStatus: number;
    setActiveColabaStatus: Function;
    setDisableSaveBtn: Function;
    setShowEmailContentScreen: Function;
    setError: Function;
}

export const ColabaStatusContent = ({activeColabaStatus, setActiveColabaStatus, setDisableSaveBtn, setShowEmailContentScreen, setError}: ColabaStatusContentProps) => {
   const {state, dispatch} = useContext(Store);
   const loanStatusManager: any = state.loanStatusManager;
   const loanStatus: LoanStatusUpdateModel  = loanStatusManager.loanStatusData;
   const selectedLoanStatus: LoanStatus = loanStatusManager.selectedLoanStatus;
   const [showAlert, setshowAlert] = useState<boolean>(false);
   const [toBeActiveItem, setToBeActiveItem] = useState<LoanStatus>();

    const onClickFromStatusHandler = (SelectedItem: LoanStatus) => { 
        
        if(selectedLoanStatus?.EditMode === true){
            setshowAlert(true);
            setToBeActiveItem(SelectedItem);
            return;
        } 
        console.log('-------------------> onClickFromStatusHandler',SelectedItem)
           SelectedItem.fromStatusId = SelectedItem.id;
           SelectedItem.fromStatus = SelectedItem.mcuName;
           SelectedItem.EditMode = false;
           disableBrowserPrompt();
           dispatch({type: LoanStatusUpdateActionsType.SetSelectedLoanStatus, payload: SelectedItem});
           setShowEmailContentScreen(true);
           setDisableSaveBtn(true);
           if(SelectedItem.toStatusId != 0){
            setDisableSaveBtn(true);
            setError('');
           }
       }
   
    const onClickToStatusHandler = (SelectedItem: LoanStatus) => {
        console.log('-------------------> onClickToStatusHandler',SelectedItem)
       let cloneObj = {...selectedLoanStatus};
       cloneObj.toStatusId = SelectedItem.id;
       cloneObj.toStatus = SelectedItem.mcuName;
       cloneObj.EditMode = true;
       dispatch({type: LoanStatusUpdateActionsType.SetSelectedLoanStatus, payload: cloneObj});
       setDisableSaveBtn(false);
       setShowEmailContentScreen(true);
       setError('');
       enableBrowserPrompt();
   }

    const renderFromStatusList = () => {
        let statuses: LoanStatus[] | undefined = loanStatus.loanStatus;
        return statuses?.map((statusObj: LoanStatus) => {
           return <li data-testid="loan-statuses-from" className= {`${selectedLoanStatus?.id === statusObj?.id ? 'active': ''} ${statusObj.isActive ? '' : 'disabled'} `} onClick = {() => onClickFromStatusHandler(statusObj)}><a>{statusObj.mcuName}</a></li> 
        });
    }

    const renderToStatusList = () => {
        let statuses: LoanStatus[] | undefined = loanStatus.loanStatus?.filter((item:LoanStatus) => item.id != selectedLoanStatus?.id);
        return statuses?.map((statusObj: LoanStatus) => {
          return <li data-testid="loan-statuses-to" className= {
              selectedLoanStatus?.toStatusId === 0 
            ? selectedLoanStatus?.id === statusObj?.id ? 'active': '' 
            : selectedLoanStatus?.toStatusId === statusObj?.id ? 'active' : ''
                               }                        
             onClick = {() => onClickToStatusHandler(statusObj)} ><a >{statusObj.mcuName}</a></li> 
        });
    }

    const alertYesHandler = async (value: boolean) => {  
        console.log('-------------------> alertYesHandler',toBeActiveItem)
        if(toBeActiveItem){
            setshowAlert(value);
            toBeActiveItem.fromStatusId = toBeActiveItem.id;
            toBeActiveItem.fromStatus = toBeActiveItem.mcuName;
            dispatch({type: LoanStatusUpdateActionsType.SetSelectedLoanStatus, payload: toBeActiveItem});
            setShowEmailContentScreen(true);
            setDisableSaveBtn(true);
            if(toBeActiveItem.toStatusId != 0){
             setDisableSaveBtn(true);
             setError('');
            }
        }     
    }

    return (
        <>
        <ContentBody className="colaba-status-body">
            <h5 className="h5" data-testid="ColabaStatusContent">Select status from list</h5>
            <nav data-testid="loan-statuses" className="nav-list">              
                <ul data-testid="colabaStatusList">
                    { activeColabaStatus === 0 ? 
                      renderFromStatusList()  
                      :
                      renderToStatusList()
                    }
                </ul>
            </nav>
        </ContentBody>
        {showAlert && (
                    <AlertBox 
                       navigateUrl = {''}
                       hideAlert={() => {
                        setshowAlert(false);
                        setToBeActiveItem(undefined);                      
                       }}
                       setshowAlert = {alertYesHandler}
                    />
                   )}
        </>
    )
}

import React, { useState, useContext, useRef, useEffect } from 'react'
import ContentBody from '../../Shared/ContentBody'
import ContentHeader, { ContentSubHeader } from '../../Shared/ContentHeader'
import DisabledWidget from '../../Shared/DisabledWidget'
import { DropDown } from '../../Shared/DropDown'
import { Toggler } from '../../Shared/Toggler'
import { TokenPopup } from '../../Shared/TokenPopup'
import { ColabaStatusEmailTemplateContent } from './ColabaStatusEmailTemplateContent'
import { Store } from '../../../Store/Store';
import { Tokens } from '../../../Entities/Models/Token'
import { RequestEmailTemplateActionsType } from '../../../Store/reducers/RequestEmailTemplateReducer'
import { disableBrowserPrompt } from '../../../Utils/helpers/Common'
import { LoanStatusUpdateActions } from '../../../Store/actions/LoanStatusUpdateActions'
import LoanStatusUpdateModel, { LoanStatus } from '../../../Entities/Models/LoanStatusUpdate'
import {LoanStatusUpdateActionsType} from '../../../Store/reducers/LoanStatusUpdateReducer';

type props = {
    disableSaveBtn: boolean;
    setDisableSaveBtn: Function;
    setError: Function;
};
export const ColabaStatusEmailTemplate = ({ disableSaveBtn, setDisableSaveBtn, setError}: props) => {
    
    const { state, dispatch } = useContext(Store);
    const emailTemplateManger: any = state.requestEmailTemplateManager;
    const tokens: Tokens[] = emailTemplateManger.tokens;
    const loanStatusManager: any = state.loanStatusManager;
    const selectedLoanStatus: LoanStatus = loanStatusManager.selectedLoanStatus;
    const loanStatus: LoanStatusUpdateModel  = loanStatusManager.loanStatusData;
    
    const [insertTokenPopup, setInsertTokenPopup] = useState<boolean>(false);
    const insertTokenDropDown = useRef<HTMLDivElement>(null);
    const [selectedField, setSelectedField] = useState<string>('');
    const [tokenList, setTokenList] = useState<Tokens[]>();
    // State For Days
    const [stateDays, setstateDays] = useState([
        { text: '1 Days', value: '01' },
        { text: '2 Days', value: '02' },
        { text: '3 Days', value: '03' },
        { text: '4 Days', value: '04' },
        { text: '5 Days', value: '05' }
    ]);
    const [showinsertToken, setshowInsertToken] = useState<boolean>(false);
    const [selectedDay, setSelectedDay] = useState([{ text: '1 Days', value: '01' }]);
    const [enabled, setEnabled] = useState<boolean>(true);

    useEffect(()=> {
        document.addEventListener('click',(e:any)=>{
          if(!insertTokenDropDown.current?.contains(e.target)){
            setInsertTokenPopup(false);
            dispatch({type: RequestEmailTemplateActionsType.SetSelectedToken, payload: null});
          }
        });
        return ()=>{
         disableBrowserPrompt();
        }
    },[]);

    useEffect(() => {
        if(tokens && selectedField){
          let list: any = setSelectedTokenList(selectedField, tokens);
          setTokenList(list)
        }
    
    }, [selectedField, tokens]);

    useEffect(() => {
        if(selectedLoanStatus && selectedLoanStatus?.isActive != undefined){
            setEnabled(selectedLoanStatus?.isActive)
        }    
    }, [selectedLoanStatus])

    const insertTokenClickHandler = (value: boolean) => {
        setshowInsertToken(value);
    }
    const activeTokenPopup = () => {
        setInsertTokenPopup(!insertTokenPopup)
        dispatch({type: RequestEmailTemplateActionsType.SetSelectedToken, payload: null});
    }
    const setSelectedTokenList = (selectedField: string, tokens: Tokens[]) => {
        switch(selectedField){
          case "fromAddress":
            return tokens.filter(item => item.fromAddess === true);    
          case "ccAddress":
            return tokens.filter(item => item.ccAddess === true); 
          case "subjectLine":
            return tokens.filter(item => item.emailSubject === true); 
          default:
            return tokens.filter(item => item.emailBody === true); 
        }
    }

    const setEnableDisableLoanStatus = async (isActive: boolean) => {
        let result = await LoanStatusUpdateActions.updateEnableDisableLoanStatusEmail(selectedLoanStatus.statusId, isActive);
        if(result === 200){
         let cloneObj: LoanStatusUpdateModel = {...loanStatus};
         let updatedData = cloneObj.loanStatus?.map((item: LoanStatus) => {
                if(selectedLoanStatus.id === item.id){
                    item.isActive = isActive;
                }
                return item;
         });
         dispatch({type: LoanStatusUpdateActionsType.SetLoanStatusData, payload: {"isActive": true, "loanStatus": updatedData}});
        }
     }

    const toggleEnabled = ()=>{
        setEnabled(!enabled);
        setEnableDisableLoanStatus(!enabled);
    }
    
    return (
        <div data-testid="ColabaStatusEmailTemplate">
            <ContentHeader title={`From ${selectedLoanStatus?.fromStatus ? selectedLoanStatus?.fromStatus: ''} to ${selectedLoanStatus?.toStatus ? selectedLoanStatus?.toStatus : ''}`} className={`colaba-status-header ${enabled ? '' : 'disabled'}`}>
            <div data-testid="add-emailTemplate" className="arrow-right float-right">
                    {showinsertToken && 
                    <div className="dropdown" ref={insertTokenDropDown}>
                        <button  className="settings-btn dropdown-toggle" data-testid="insertToken-btn" onClick={activeTokenPopup}>Insert Tokens <i className="zmdi zmdi-plus"></i></button>
                        {insertTokenPopup &&
                        <TokenPopup tokens = {tokenList} applyClass="arrow-right"/>
                        }            
                    </div>
                    }

            </div>
            </ContentHeader>
           
            <ContentSubHeader tooltipType={11} className={`colaba-status-subheader ${enabled ? '' : 'disabled'}`}>
                {/* <DropDown listData={stateDays} disabled={!enabled} selectedValue={selectedDay} handlerSelect={(ele: any) => { setSelectedDay(ele) }} /> */}
                <span className="disable-enabled">Disable/Enable <Toggler isDisable={selectedLoanStatus?.statusId != 0 ? false : true} checked={enabled} handlerClick={() =>{
                   toggleEnabled(); 
                    setshowInsertToken(false);
                    }} /></span>

            </ContentSubHeader>

            {enabled &&
                <ColabaStatusEmailTemplateContent 
                    insertTokenClick={insertTokenClickHandler}
                    showinsertToken={showinsertToken}
                    setSelectedField={setSelectedField} 
                    disableSaveBtn = {disableSaveBtn}
                    setDisableSaveBtn= {setDisableSaveBtn}
                    setToStatusError = {setError}
                    />
            }

            {!enabled &&
                <ContentBody className={`flex flex-center ${enabled ? '' : 'disabled'}`}>
                    <DisabledWidget text="Status Email is disabled" />
                </ContentBody>
            }

           
        </div>
    )
}

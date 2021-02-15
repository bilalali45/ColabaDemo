import React, { useContext,useState,useEffect} from 'react';
import { ReminderEmailListActions } from '../../../Store/actions/ReminderEmailsActions';
import ContentBody from '../../Shared/ContentBody';
import ReminderControl from '../../Shared/ReminderControl';
import { cloneDeep } from "lodash";
import { ReminderEmailActionsType } from '../../../Store/reducers/ReminderEmailReducer';
import { Store } from '../../../Store/Store';
import ContentFooter from '../../Shared/ContentFooter';
import ReminderSettingTemplate, {ReminderEmailTemplate} from '../../../Entities/Models/ReminderEmailListTemplate';
import moment from "moment";
import _ from "lodash";
import { AlertBox } from '../../Shared/AlertBox';
import { RequestEmailTemplateActionsType } from '../../../Store/reducers/RequestEmailTemplateReducer';
import HaveNoDataReminder from '../../Shared/HaveNoDataReminder';



type ReminderSettingContent = {
    setShowFooter: Function;
    cancelClick : boolean;
    setCancelClick: Function;
    enableDisableClick: Function;
}

export const ReminderSettingContent = ({setShowFooter, cancelClick, setCancelClick, enableDisableClick}: ReminderSettingContent) => {
    
    const { state, dispatch } = useContext(Store);
    const emailReminderManager: any = state.emailReminderManager;
    const reminderEmailData = emailReminderManager.reminderEmailData;
    const selectedreminderEmail = emailReminderManager.selectedReminderEmail;
    const [currentActive, setCurrentActive] = useState<number>(0);
    const [showAlert, setshowAlert] = useState<boolean>(false);
    const [toBeActive, setToBeActive] = useState<number>(0);
    const [toBeActiveItem, setToBeActiveItem] = useState<ReminderSettingTemplate>();
    const [lastClick, setLastClick] = useState<string>();
    const [yesClick, setYesClick] = useState<boolean>(false);
    const [addNewControl, setAddNewControl] = useState(false);
    
    
    useEffect(() => {
        fetchReminderEmailSettings(0);
    },[])

    useEffect(() => {
       if(cancelClick){         
          fetchReminderEmailSettings(currentActive);
          setCancelClick(false);        
          selectedreminderEmail.isEditMode = false;
       }
       if(yesClick){
        setYesClick(false)
        fetchReminderEmailSettings(toBeActive);
       }
    }, [cancelClick, yesClick])

    const  fetchReminderEmailSettings = async (selectItem: number) => {
           let data: any  = await ReminderEmailListActions.fetchReminderEmails();
           dispatch({type:ReminderEmailActionsType.SetAllReminderEmailEnable, payload: data?.isActive});
            let sortedFiles = _.orderBy(data?.emailReminders, (item: ReminderSettingTemplate) => item.noOfDays, ["asc"]); 
            let reminderEmailsettings: ReminderSettingTemplate[] = cloneDeep(sortedFiles);
            dispatch({ type: ReminderEmailActionsType.SetReminderEmailData, payload: sortedFiles });
            if(reminderEmailsettings && reminderEmailsettings.length > 0){
                CurrentActive(selectItem,reminderEmailsettings[selectItem]);
            }else{
                dispatch({ type: ReminderEmailActionsType.SetSelectedReminderEmail, payload: []})  
            }         
    }

    const setDay = (day:any, getNumber:string) => {  
        const updatedReminderEmail = reminderEmailData.map((item:ReminderSettingTemplate, index:number) => {
            let itemIndex = (index < 9 ? "0" : "") + (index + 1);
            if(itemIndex == getNumber)
            {
               item.noOfDays = day[0].value;                     
            }
            return item;
        });
        selectedreminderEmail.isEditMode = true;
        dispatch({ type: ReminderEmailActionsType.SetReminderEmailData, payload: updatedReminderEmail });
        setShowFooter(true);
    }

    const newDay = (ele:string[]) => { 
        const updatedReminderEmail = reminderEmailData.map((item:ReminderSettingTemplate, index:number) => {           
            return {
                ...reminderEmailData,
                ele
            };
        });
        selectedreminderEmail.isEditMode = true;
        dispatch({ type: ReminderEmailActionsType.SetReminderEmailData, payload: updatedReminderEmail });
       setShowFooter(true);      
    }

    const setTime = (ele:any, getNumber:string, meridiam:any) => {
        let time: Date  = new Date(moment(ele[0].value + ' ' + meridiam,'hh:mm A').format());
        const updatedReminderEmail = reminderEmailData.map((item:ReminderSettingTemplate, index:number) => {
            let itemIndex = (index < 9 ? "0" : "") + (index + 1);
            if(itemIndex == getNumber)
            {
               item.recurringTime = time;        
            }
            return item;
        });
        selectedreminderEmail.isEditMode = true;      
        dispatch({ type: ReminderEmailActionsType.SetReminderEmailData, payload: updatedReminderEmail });
        selectedreminderEmail.recurringTime = time;
        setShowFooter(true); 
    }

    const setMeridiem = (getNumber:string) =>{
        let updatedReminderEmail = reminderEmailData.map((item: ReminderSettingTemplate, index: number) => {
            let itemIndex = (index < 9 ? "0" : "") + (index + 1);
            if (itemIndex == getNumber) {
                var meridiem  = moment(item.recurringTime).format("A");
                if(meridiem == 'AM'){
                    let today = moment().format("L");
                    let time = moment(item.recurringTime).local().format("hh:mm");
                    item.recurringTime   = new Date (moment(today +' ' + time + ' ' + 'PM').format());
                }else{
                    let today = moment().format("L");
                    let time = moment(item.recurringTime).local().format("hh:mm");
                    item.recurringTime   = new Date(moment(today +' ' + time + ' ' + 'AM').format());
                }                
            } 
            return item;
        });
        selectedreminderEmail.isEditMode = true;
        dispatch({ type: ReminderEmailActionsType.SetReminderEmailData, payload: updatedReminderEmail }) 
        setShowFooter(true);
    }

    const makeEnabled = (getNumber:string) =>{
        var updateItem:ReminderSettingTemplate={};
        const me = reminderEmailData.map((item:ReminderSettingTemplate,index:number) => {
            let itemIndex = (index < 9 ? "0" : "") + (index + 1);
            if(itemIndex == getNumber)
            {
                item.isActive = !item.isActive;
                updateItem = item;
            }
            return item;
        });
        
        ReminderEmailListActions.updateEnableDisableEmails(updateItem);
        dispatch({ type: ReminderEmailActionsType.SetSelectedReminderEmail, payload: updateItem });       
    }

    const makeDelete = async (deletedItem:ReminderSettingTemplate, getNumber:string)=>{
        if(deletedItem.id){
          let result = await  ReminderEmailListActions.deleteEmailReminder(deletedItem);        
        }
        fetchReminderEmailSettings(0);
    }

    const addEmailReminderClick = (data?: ReminderSettingTemplate[]) => {  
        let lastElem;
        let days;
        let newTime:Date;
        let reminderEmailsettings = cloneDeep( data ?  data : reminderEmailData);
        let arrayLength = reminderEmailsettings?.length;
        if(arrayLength > 0){
            lastElem = reminderEmailsettings[arrayLength-1];           
            let noofDays = parseInt(lastElem?.noOfDays) + 2; 
            days = noofDays <= 9 ? `0${noofDays}` : noofDays.toString()          
            newTime = new Date(moment().utc().set({h: new Date().getUTCHours(), m: 0}).format());  
            //alert('Yes')       
            setAddNewControl(true);
        }
        else{
            days = "02";
            newTime = new Date(moment().utc().set({h: new Date().getUTCHours(), m: 0}).format());  
                
        }
    
        let newItem : ReminderSettingTemplate =  new ReminderSettingTemplate('',days,newTime, true);  
        newItem.isEditMode = true;
        let item = [...reminderEmailsettings, newItem]    
        dispatch({ type: ReminderEmailActionsType.SetReminderEmailData, payload: item }) ; 
        CurrentActive(item.length - 1 ,newItem); 
        setShowFooter(false);
        
    }
    
    const CurrentActive =(index:number,item:any) =>{
        
        if(!item){
            item = reminderEmailData?.[0];
            index = 0;
        } 
        let indexValue = index+1;
        item.index = '0'+indexValue;
        setCurrentActive(index);
        dispatch({ type: ReminderEmailActionsType.SetSelectedReminderEmail, payload: {...item}})   
    }
    const alertYesHandler = async (value: boolean) => {
        setshowAlert(value);
        if(lastClick === 'item'){
            setYesClick(true);
            setShowFooter(false);          
        }       
       if(lastClick === "addBtn"){
        let result : any   = await ReminderEmailListActions.fetchReminderEmails();
        dispatch({type:ReminderEmailActionsType.SetAllReminderEmailEnable, payload: result.isActive});
        let sortedFiles = _.orderBy(result?.emailReminders, (item: ReminderSettingTemplate) => item.noOfDays, ["asc"]);
        dispatch({ type: ReminderEmailActionsType.SetReminderEmailData, payload: sortedFiles });                  
            addEmailReminderClick(sortedFiles);     
       }
      }
   
    return (
        <>
        <ContentBody className="nlre-settings-body footerEnabled">
            <h5 data-testid="reminder-list-text" className="h5">Reminder Emails</h5>
            <div className={`set-reminder-emails ${(!reminderEmailData || reminderEmailData.length<1) ? 'no-reminder-found': ''}`}>

                {(!reminderEmailData || reminderEmailData.length<1) &&
                    <>
                        <HaveNoDataReminder text={`You have no reminders added yet`}/>
                    </>
                }
                
                {                   
                    reminderEmailData &&
                    <ul>
                    {reminderEmailData && 
                        reminderEmailData?.map((item: ReminderSettingTemplate, index:number) => {
                            let time = moment(item.recurringTime).local().format("hh:mm");
                            let meridiem  = moment(item.recurringTime).format("A");
                            let itemIndex = (index < 9 ? "0" : "") + (index + 1);
                            return (
                                <li data-testid="emailreminder-list" className={ currentActive == index ? 'active' : ''} onClick={()=>{
                                    if(selectedreminderEmail?.isActive){
                                        if(selectedreminderEmail.isEditMode === true && currentActive != index){
                                            setshowAlert(true);
                                            setToBeActive(index);
                                            setToBeActiveItem(item);
                                            setLastClick('item');
                                            return;
                                            }
                                        }
                                    
                                    if(currentActive != index){
                                        CurrentActive(index,item)
                                    }                                 
                                }}>

                                    
                                    <ReminderControl 
                                        number={itemIndex}                                    
                                        days={item?.noOfDays?.toString().length  === 1 ? ""+item.noOfDays : item.noOfDays} 
                                        time={time} 
                                        timeType={meridiem} 
                                        makeEnabled={item.isActive} 
                                        addNewReminder={addNewControl}
                                        handlerAddNewReminder={(val:boolean)=>{setAddNewControl(val)}}
                                        handlerDays={(ele:any)=>setDay(ele,itemIndex)}
                                        handlerTime={(ele:any)=>setTime(ele,itemIndex,meridiem)}                                       
                                        handlerTimeType={()=>{setMeridiem(itemIndex)}}
                                        handlerEnabled={()=>makeEnabled(itemIndex)}
                                        handlerDelete={()=>{makeDelete(item,itemIndex)}}
                                        handlerInput={(ele:any)=>{ newDay(ele)} }
                                        />
                                </li>
                            )
                        })                            
                    }
                    </ul>
                }
                
            </div>
        </ContentBody>
        <ContentFooter className={`nlre-settings-footer nlre-settings-footer-btn`}>
            <button             
                onClick={() => {
                    if(selectedreminderEmail?.isActive){
                        if(selectedreminderEmail.isEditMode === true){
                            setshowAlert(true);
                            setLastClick('addBtn');
                            return;
                        }
                    }
                    
                addEmailReminderClick()
                }}
                className={`settings-btn settings-btn-add`}
                data-testid="add-reminder-btn">
                <span className="nlre-settings-footer-btn-text">Add Reminder Email <em className="zmdi zmdi-plus"></em></span>
            </button>
        </ContentFooter>
           {showAlert && (
                    <AlertBox 
                       navigateUrl = {''}
                       hideAlert={() => {
                        setshowAlert(false);                      
                       }}
                       setshowAlert = {alertYesHandler}
                    />
                   )}
       
        </>
    )
}
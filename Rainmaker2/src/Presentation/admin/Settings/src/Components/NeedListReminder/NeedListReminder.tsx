import React, {useContext, useEffect, useRef, useState} from 'react';
import { ReminderEmailActionsType } from '../../Store/reducers/ReminderEmailReducer';
import { RequestEmailTemplateActionsType } from '../../Store/reducers/RequestEmailTemplateReducer';
import { Store } from '../../Store/Store';
import { AlertBox } from '../Shared/AlertBox';
import ContentBody from '../Shared/ContentBody';
import DisabledWidget from '../Shared/DisabledWidget';
import { SVGReminderEmailsDisabled } from '../Shared/SVG';
import { NeedListReminderBody } from './_NeedListReminder/NeedListReminderBody';
import { NeedListReminderHeader } from './_NeedListReminder/NeedListReminderHeader';
import { ReminderEmailListActions } from '../../Store/actions/ReminderEmailsActions';
import { WidgetLoader } from '../Shared/Loader';
type Props = {
    backHandler?: Function
}

 const NeedListReminder = ({backHandler}: Props) => {
    const { state, dispatch } = useContext(Store);
    const emailReminderManager: any = state.emailReminderManager;
    const reminderEmailData = emailReminderManager.reminderEmailData;
    const allReminderEnable = emailReminderManager.allReminderEnable;
    const [isEnable, showReminderEmailScreen] = useState<boolean>(true);
    
     useEffect(() => {
      fetchReminderEmailSettings();
      },[])

    const  fetchReminderEmailSettings = async () => {
      let data: any  = await ReminderEmailListActions.fetchReminderEmails();
      console.log('----------------->',data)
      dispatch({type:ReminderEmailActionsType.SetAllReminderEmailEnable, payload: data?.isActive}); 
      dispatch({type:ReminderEmailActionsType.SetReminderEmailData, payload: data?.emailReminders });
      showReminderEmailScreenHandler(data?.isActive);
    }

    const showReminderEmailScreenHandler = (value: boolean) => {
        showReminderEmailScreen(value);
       
    }
    if(!reminderEmailData){
      return <WidgetLoader reduceHeight="52px"/>
    }

   

    return (
        <div className="settings__need-list-reminder"> 
                  <NeedListReminderHeader/>

                  {allReminderEnable &&
                    <NeedListReminderBody 
                    enableDisableClick ={showReminderEmailScreenHandler} 
                    />
                  }
                  {!allReminderEnable &&
                    <ContentBody className="need-list-reminder-body flex flex-center">
                        <DisabledWidget text="Reminder Emails are disabled"/>                   
                    </ContentBody>
                   }                                
        </div>
    )
}

export default NeedListReminder;

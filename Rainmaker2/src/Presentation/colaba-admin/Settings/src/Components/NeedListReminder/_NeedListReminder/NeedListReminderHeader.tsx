import React, {useContext, useEffect, useState} from 'react';
import { Store } from '../../../Store/Store';
import ContentHeader from '../../Shared/ContentHeader';
import { Toggler } from '../../Shared/Toggler';
import { ReminderEmailListActions } from '../../../Store/actions/ReminderEmailsActions';
import { ReminderEmailActionsType } from '../../../Store/reducers/ReminderEmailReducer';


export const NeedListReminderHeader = () => {
  const {state, dispatch} = useContext(Store);
  const emailReminderManager: any = state.emailReminderManager;
  const allReminderEnable = emailReminderManager.allReminderEnable;

  const [enabled, setEnabled] = useState(true);

  useEffect(() => {
    setEnabled(allReminderEnable);
  },[allReminderEnable])

  useEffect(() => {
    return ()=>{
      console.log('Reminder Email Unmounting...')
      dispatch({ type: ReminderEmailActionsType.SetReminderEmailData, payload: undefined });
      dispatch({ type: ReminderEmailActionsType.SetSelectedReminderEmail, payload: []}) 
     }
  },[])

  const toggleEnabled = () => {
   
    updateAllEmailListToInActive(!enabled); 
  }

  const updateAllEmailListToInActive = async (isEnable: boolean) => {
    let result = await ReminderEmailListActions.updateEnableDisableAllEmails(isEnable);
    if (result?.status === 200) {    
      setEnabled(!enabled);  
      dispatch({type:ReminderEmailActionsType.SetAllReminderEmailEnable, payload: isEnable});
    }
  }
   
  return (
    <>
      <ContentHeader title="NEEDS LIST REMINDER EMAILS" className="need-list-reminder-header">
        <span data-testid="toggler-label" className="disable-enabled">Disable/Enable <Toggler checked={enabled} handlerClick={toggleEnabled} /></span>
      </ContentHeader>
    </>
  )
}

import React, { useContext, useState } from 'react';
import { Store } from '../../../Store/Store';
import ContentBody from '../../Shared/ContentBody';
import { ReminderEmails } from './ReminderEmails';
import { ReminderSettings } from './ReminderSettings';

type NeedListReminderBodyProps = {
  enableDisableClick: Function;
};

export const NeedListReminderBody = ({
  enableDisableClick
}: NeedListReminderBodyProps) => {
  const { state, dispatch } = useContext(Store);
  const emailReminderManager: any = state.emailReminderManager;
  const reminderEmailData = emailReminderManager.reminderEmailData;

  const [showFooter, setShowFooter] = useState(false);
  const [cancelClick, setCancelClick] = useState(false);

  return (
    <ContentBody className="settings__need-list-reminder-body">
      <div className="col-md-4 no-padding settings__need-list-reminder-body--left">
        <ReminderSettings
          setShowFooter={()=>setShowFooter(!showFooter)}
          cancelClick={cancelClick}
          setCancelClick={setCancelClick}
          enableDisableClick={enableDisableClick}
        />
      </div>

      <div className={`col-md-8 no-padding settings__need-list-reminder-body--right ${reminderEmailData?.length === 0?'data-not-found':''}`}>
        {reminderEmailData?.length > 0 && (
          <ReminderEmails
            showFooter={showFooter}
            setShowFooter={setShowFooter}
            setCancelClick={setCancelClick}
          />
        )}
      </div>


    </ContentBody>
  );
};

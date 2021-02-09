import React, {useEffect, useState, useRef, useContext} from 'react'
import { ReminderEmailsContent } from './ReminderEmailsContent';
import { Store } from '../../../Store/Store';
import { ReminderEmailSubHeader } from './ReminderEmailSubHeader';

type ReminderEmailsProps = {
  showFooter : boolean;
  setShowFooter: Function;
  setCancelClick: Function;
}

export const ReminderEmails = ({showFooter, setShowFooter, setCancelClick}:ReminderEmailsProps) => {

  const [showinsertToken, setshowInsertToken] = useState<boolean>(false);
  const [selectedField, setSelectedField] = useState<string>('');

    const insertTokenClickHandler = (value: boolean) => {
      console.log('----------------------> insertTokenClickHandler', value)
        setshowInsertToken(value);
    }

    const selectFieldHandler = (selectedField: string) => {
      console.log('----------------------> selectFieldHandler', selectedField)
      setSelectedField(selectedField);
    }
    
    return (
        <div className="need-list-reminder-email-body">
            <ReminderEmailSubHeader                          
              showinsertToken = {showinsertToken}
              selectedField = {selectedField} 
            />
          
            <ReminderEmailsContent   
              showinsertToken={showinsertToken}
              insertTokenClick={(data:any)=> insertTokenClickHandler(data)}
              setSelectedField = {selectFieldHandler}
              showFooter = {showFooter}
              setShowFooter = {setShowFooter}
              setCancelClick = {setCancelClick}
            />
        </div>
    )
}

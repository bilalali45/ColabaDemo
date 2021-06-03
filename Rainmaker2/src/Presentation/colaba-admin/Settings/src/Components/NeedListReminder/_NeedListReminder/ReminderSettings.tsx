import React, {useState} from 'react';
import { ContentSubHeader } from '../../Shared/ContentHeader';
import { ReminderSettingContent } from './ReminderSettingContent';

type ReminderSettingsProps = {
    setShowFooter: Function;
    cancelClick: boolean;
    setCancelClick: Function;
    enableDisableClick: Function;
}

export const ReminderSettings = ({setShowFooter, cancelClick, setCancelClick, enableDisableClick}: ReminderSettingsProps) => {
      
    return (
        <div>
            <ContentSubHeader title="Settings" className="nlre-settings-header"></ContentSubHeader>
            <ReminderSettingContent 
             setShowFooter = {setShowFooter} 
             cancelClick = {cancelClick}
             setCancelClick = {setCancelClick}
             enableDisableClick = {enableDisableClick}
            
            />
        </div>
    )
}

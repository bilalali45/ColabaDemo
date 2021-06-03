import React, {useState, useEffect} from 'react';
import ContentHeader from '../../Shared/ContentHeader';

interface Props{

}

export const NotificationHeader:React.FC<Props> = ({}) => {
    return (
        <>
        <ContentHeader title="Notification Settings" tooltipType={4} className="notification-header"/>
        </>
    )
}

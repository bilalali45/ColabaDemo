import React from 'react';
import { Store } from '../../Store/Store';
import { NotificationBody } from './_Notification/NotificationBody';
import { NotificationHeader } from './_Notification/NotificationHeader';

interface NotificationProps {

}


export const  Notification:React.FC<NotificationProps> = ({children}) =>{
    return (
        <div data-testid="Notification">
            <NotificationHeader/>
            <NotificationBody />
        </div>
    )
}

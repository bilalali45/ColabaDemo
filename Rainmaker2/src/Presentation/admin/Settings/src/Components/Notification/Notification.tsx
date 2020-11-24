import React from 'react';
import { Store } from '../../Store/Store';
import { NotificationBody } from './_Notification/NotificationBody';
import { NotificationHeader } from './_Notification/NotificationHeader';


export const  Notification = () =>{
    return (
        <>
            <NotificationHeader/>
            <NotificationBody />
        </>
    )
}

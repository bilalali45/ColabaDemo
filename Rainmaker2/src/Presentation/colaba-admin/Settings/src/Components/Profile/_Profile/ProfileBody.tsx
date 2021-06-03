import React from 'react'
import  AssignRole  from '../../AssignRole/AssignRole';
import {Notification} from '../../Notification/Notification';
import ContentBody from '../../Shared/ContentBody';
import {AccountDetail} from '../../AccountDetail/AccountDetail';

type BodyProps = {
    navigation : number;
}

export const  ProfileBody = ({navigation}: BodyProps) => {
   
      
    return (
        <ContentBody>
              {navigation === 1 && <AccountDetail/>}
              {navigation === 2 && <AssignRole/>}    
              {navigation === 3 && <Notification/>} 
        </ContentBody>
    )
}

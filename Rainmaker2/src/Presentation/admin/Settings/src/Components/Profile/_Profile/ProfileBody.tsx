import React from 'react'
import AccountDetail from '../../AccountDetail/AccountDetail';
import  AssignRole  from '../../AssignRole/AssignRole';
import {Notification} from '../../Notification/Notification';
import ContentBody from '../../Shared/ContentBody';


type BodyProps = {
    navigation : number;
}

export const  ProfileBody = ({navigation}: BodyProps) => {
   
      
    return (
        <div data-testid="ProfileBody" className="settings__profile-body">
        <ContentBody>
              {navigation === 1 && <AccountDetail/>}
              {navigation === 2 && <AssignRole/>}    
              {navigation === 3 && <Notification/>} 
        </ContentBody>
        </div>
    )
}

import React, { useState } from 'react'
import { Profile } from '../../Profile/Profile'
import ContentBody from '../../Shared/ContentBody'
import { UserProfileList } from '../../UserProfileList/UserProfileList';

export const ManageUsersBody = () => {

    const [showProfileList, setShowProfileList] = useState(true);

    const backHandler = () => {
        setShowProfileList(!showProfileList);        
    }

    return (
        <div className={`settings__manage-users--body`}>
          { showProfileList === true 
          ?
            <UserProfileList
            backHandler = {backHandler}
            />
          :
          <Profile
            backHandler = {backHandler} 
            />
          }
            
        </div>
    )
}

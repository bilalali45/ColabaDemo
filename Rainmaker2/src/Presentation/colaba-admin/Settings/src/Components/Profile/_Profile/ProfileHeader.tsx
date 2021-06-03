import React, {useEffect, useState} from 'react';
import { Role } from '../../../Store/Navigation';
import { ProfileMenu } from '../../../Utils/helpers/Enums';
import { LocalDB } from '../../../Utils/LocalDB';
import ContentHeader, { ContentSubHeader } from '../../Shared/ContentHeader';

type HeaderProps = {
    navigation : number;
    changeNav : Function;
    backHandler? : Function;
}


const ProfileHeader = ({navigation, changeNav, backHandler}: HeaderProps) => {
  
    const [title, setTitle] = useState('');
    const [linkText, setLinkText] = useState('');

    useEffect(() => {
       let role = LocalDB.getUserRole();
       if(role === Role.MCU_ROLE){
        setTitle("Your Profile");
        setLinkText("");
       }else if(role === Role.ADMIN_ROLE){
        setTitle("");
        setLinkText("Back");
       }
    },[LocalDB.getUserRole()])

    
    return (
        <>
        <ContentHeader title={'Manage Users'} className="profile-header"></ContentHeader>
        <ContentSubHeader 
            //title={'Manage Users'} 
            //backLinkText={linkText} 
            //backLink={backHandler} 
            className="profile-subheader">

            <ul className="settings__nav-pills">
                {/* <li data-testid="profile-menu" className={`${navigation == ProfileMenu.AccountDetail ? 'active' : ''}`}>
    <a className={`settings-btn`} href="javascript:;" onClick={e=>changeNav(ProfileMenu.AccountDetail)}> {LocalDB.getUserRole()=== Role.MCU_ROLE ? 'Account Detail' : 'Profile'}</a>
                </li> */}
                <li data-testid="profile-menu" className={`${navigation == ProfileMenu.AssignedRole ? 'active' : ''}`}>
                    <a className={`settings-btn`} href="javascript:;" onClick={e=>changeNav(ProfileMenu.AssignedRole)}>Assigned Role</a>
                </li>
                <li data-testid="profile-menu" className={`${navigation == ProfileMenu.Notification ? 'active' : ''}`}>
                    <a className={`settings-btn`} href="javascript:;" onClick={e=>changeNav(ProfileMenu.Notification)}>Notification</a>
                </li>
            </ul>

        </ContentSubHeader>
        </>
    )
}

export default ProfileHeader

import React, { useState } from 'react'
import ContentHeader from '../Shared/ContentHeader';
import Tab from 'react-bootstrap/Tab';
import Nav from 'react-bootstrap/Nav';
import ContentBody from '../Shared/ContentBody';
import AssignRole from '../AssignRole/AssignRole';
import { NotificationBody } from '../Notification/_Notification/NotificationBody';
import  ProfileHeader  from './_Profile/ProfileHeader';
import { ProfileBody } from './_Profile/ProfileBody';
import { ProfileMenu } from '../../Utils/helpers/Enums';


type Props = {
    backHandler?: Function
}

export const Profile = ({backHandler}: Props) => {

    const [Nav, setNav] = useState(ProfileMenu.AssignedRole);
    
    const changeNavHandler = (ele:number) => {
        setNav(ele);
    }

    return (
        <div data-testid="settings__profile" className="settings__profile">
            <ProfileHeader
             navigation = {Nav}
             changeNav = {changeNavHandler}
             backHandler = {backHandler}
             />
            <ProfileBody
             navigation = {Nav}
            />
        </div>
    )
}

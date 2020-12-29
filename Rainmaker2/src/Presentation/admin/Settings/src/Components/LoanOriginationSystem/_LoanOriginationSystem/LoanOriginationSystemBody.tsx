import React from 'react'
import ContentBody from "../../Shared/ContentBody";
import { LOSOrganization } from './Organization'
import { LOSUsers } from './Users'
import {AccountDetail} from "../../AccountDetail/AccountDetail";
import {LoadOriginationMenu} from "../../../Utils/helpers/Enums";
import { ContentSubHeader } from '../../Shared/ContentHeader';
// import { CreateEmailTemplates } from './CreateEmailTemplates'
// import { EmailTemplatesList } from './EmailTemplatesList'

type BodyProps = {
    navigation : number;
    changeNav : Function;
}

export const LoanOriginationSystemBody = ({navigation, changeNav}: BodyProps) => {
    return (
        <>
            <ContentSubHeader className="settings-loadOrigination--tabs">
            <ul className="settings__nav-pills">
                {/* <li data-testid="profile-menu" className={`${navigation == ProfileMenu.AccountDetail ? 'active' : ''}`}>
    <a className={`settings-btn`} href="javascript:;" onClick={e=>changeNav(ProfileMenu.AccountDetail)}> {LocalDB.getUserRole()=== Role.MCU_ROLE ? 'Account Detail' : 'Profile'}</a>
                </li> */}
                <li data-testid="los-menu-user" className={`${navigation == LoadOriginationMenu.Users ? 'active' : ''}`}>
                    <a href="javascript:;" onClick={e=>changeNav(LoadOriginationMenu.Users)}>Users</a>
                </li>
                <li data-testid="los-menu-org" className={`${navigation == LoadOriginationMenu.Organization ? 'active' : ''}`}>
                    <a href="javascript:;" onClick={e=>changeNav(LoadOriginationMenu.Organization)}>Organization</a>
                </li>
            </ul>
            </ContentSubHeader>
            {navigation === 1 && <LOSUsers/>}
            {navigation === 2 && <LOSOrganization/>}
        </>
    )
}

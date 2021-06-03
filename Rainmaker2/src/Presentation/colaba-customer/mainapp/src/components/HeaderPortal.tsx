import React, { useContext, useState } from 'react';
import { LOGOTexasTrust } from '../Shared/Components/SVGs';
import Dropdown from 'react-bootstrap/Dropdown'
import { Store } from "../Store/Store";
import { LocalDB } from '../lib/localStorage';
import { UserInfoFromLocalDB } from '../Entities/Models/UserInfoFromLocalDB';
import { applyTheme } from '../../Utilities/helpers/CommonFunc';
import { Link, useHistory } from 'react-router-dom'
import DashboardActions from '../Store/actions/DashboardActions';
import { ErrorHandler } from '../../Utilities/helpers/ErrorHandler';
import { UserActionsType } from "../Store/reducers/UserReducer";

interface HeaderPortal {
    loggedInUserName: string;
}

export const HeaderPortal: React.FC<HeaderPortal> = () => {
    const { state, dispatch } = useContext(Store);
    let history = useHistory();
    const userInfo: UserInfoFromLocalDB | null = LocalDB.getLoggedInUserDetails();
    const tenantInfo = state.user.tenantInfo;
    const selectTheme = async (color: string) => {
        let res = await LocalDB.getcaptchaCode(DashboardActions.setSettings, color);
        if (res) {
            if (ErrorHandler.successStatus.includes(res.statusCode)) {
                applyTheme(color)
            }
            else {
                ErrorHandler.setError(dispatch, res);
            }
        }
    }

    const clearStore = async () => {
        dispatch({ type: UserActionsType.SetUserInfo, payload: {} });
    }

    const [responsiveHeader, setResponsiveHeader] = useState(true);


    return (
        <header className="main-header">
            <div className="container-fluid">
                <div className="row align-items-center justify-content-between h-inner-wrap">
                    <div className="col-6">
                        <Link to={(tenantInfo) ? '/homepage' : '#'} className="logo-wrap">
                            {(tenantInfo ? <img src={`${tenantInfo.logo}`} /> : <LOGOTexasTrust />)}
                        </Link>
                    </div>
                    <div className="col-6 text-right">

                        <div className="header-options">
                            <button onClick={() => setResponsiveHeader(!responsiveHeader)} className="btn-toggler-menu"><em className="zmdi zmdi-view-headline"></em></button>
                            {responsiveHeader &&
                                <div className="header-options-wrap">
                                    <ul>
                                        <li className="theme-dropdown-wrap">
                                            <Dropdown>
                                                <Dropdown.Toggle as="div">
                                                    Select Theme <i className="zmdi zmdi-caret-down-circle"></i>
                                                </Dropdown.Toggle>
                                                <Dropdown.Menu>
                                                    <Dropdown.Item href="#/action-1" onClick={() => selectTheme('#4384f4')}><span className="theme-color" style={{ backgroundColor: '#4384f4' }}></span> Theme Default</Dropdown.Item>
                                                    <Dropdown.Item href="#/action-2" onClick={() => selectTheme('#f00000')}><span className="theme-color" style={{ backgroundColor: '#f00000' }}></span> Theme Red</Dropdown.Item>
                                                    <Dropdown.Item href="#/action-3" onClick={() => selectTheme('#ff9800')}><span className="theme-color" style={{ backgroundColor: '#ff9800' }}></span> Theme Amber</Dropdown.Item>
                                                </Dropdown.Menu>
                                            </Dropdown>
                                        </li>
                                        <li className="user-dropdown-wrap">
                                            <Dropdown>
                                                <Dropdown.Toggle as="div">
                                                    Hello, {userInfo ? userInfo.firstName : ''} <i className="zmdi zmdi-caret-down-circle"></i>
                                                </Dropdown.Toggle>
                                                <Dropdown.Menu>
                                                <Dropdown.Item onClick={() => { history.push("/homepage"); } } >Dashboard</Dropdown.Item>
                                                    {/* <Dropdown.Item href="#/action-2">Change Password</Dropdown.Item> */}
                                                    <Dropdown.Item onClick={async () => {
                                                        await LocalDB.logOffCurrentUser();
                                                        await clearStore();
                                                        
                                                        LocalDB.clearSessionStorage();
                                                    }}>Sign Out</Dropdown.Item>
                                                </Dropdown.Menu>
                                            </Dropdown>
                                        </li>
                                    </ul>
                                </div>
                            }
                        </div>

                    </div>
                </div>
            </div>
        </header>
    )
}



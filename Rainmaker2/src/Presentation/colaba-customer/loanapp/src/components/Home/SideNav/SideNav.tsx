import React, { useContext } from 'react'
import { useHistory } from 'react-router';

import LeftMenuHandler from '../../../store/actions/LeftMenuHandler';
import { Store } from '../../../store/store'

export const SideNav = ({  }: any) => {

    const { state } = useContext(Store);

    const history = useHistory();

    const changeNav = (nav, index) => {
        let currentIndex: number | null = null;
        // history?.push(nav.steps[0].path)
        // return;
       
            state?.leftMenu?.leftMenuItems.forEach((n, i) => {
                if (n["name"] === LeftMenuHandler.currentNavItem?.name) {
                    currentIndex = i;
                }
            });
            if (LeftMenuHandler.currentNavItem.isDone || nav.name === LeftMenuHandler.currentNavItem.name) {
                history?.push(nav.steps[0].path)
            } else if (currentIndex && index < currentIndex) {
                history?.push(nav.steps[0].path)
            }
        
    }
    
    return (
        <nav className="loanapp-c-sidenav">
            <ul>
                {
                    state?.leftMenu?.leftMenuItems?.map((n: any, i: any) => {
                        return (
                            <li className={`${!(i <= LeftMenuHandler.currentNavItemIndex) ? 'disabled' : ''} ${(LeftMenuHandler.currentNavItemIndex > i) ? 'passed' : ''} ${LeftMenuHandler.currentNavItemIndex === i ? 'active' : ''}`}>
                                <a href="javascript:;" onClick={() =>{ changeNav(n, i)}}>
                                    <span className="nav-icon"> {n.icon}</span>
                                    {n.name}
                                </a>
                                {/* <ul>
                                    <li>
                                        <a href="" className="active">
                                                Submenu
                                        </a>
                                    </li>
                                    <li>
                                        <a href="">
                                        Submenu
                                        </a>
                                    </li>
                                </ul> */}
                            </li>
                        )
                    })
                }
            </ul>
        </nav>
    )
}

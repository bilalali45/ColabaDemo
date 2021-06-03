import React, { useContext } from 'react'
import { LoanInfoType } from '../../Entities/Models/types';
import { IconFinishingUp, IconGetStarted, IconGetToKnow, IconGovtqts, IconMyMoney, IconNewMortgage, IconProperties, IconReview } from '../../Shared/Components/SVGs';
import { CommonActions } from '../../store/actions/CommonActions';
import { Store } from '../../store/store';
import { NavigationHandler } from '../../Utilities/Navigation/NavigationHandler';
import { LeftMenuItems } from '../../Utilities/Navigation/navigation_config/NavigationItems';

const sideNavIcons = {
    [LeftMenuItems.GettingStarted]: IconGetStarted(),
    [LeftMenuItems.GettingToKnowYou]: IconGetToKnow(),
    [LeftMenuItems.MyNewMortgage]: IconNewMortgage(),
    [LeftMenuItems.MyMoney]: IconMyMoney(),
    [LeftMenuItems.MyProperties]: IconProperties(),
    [LeftMenuItems.FinishingUp]: IconFinishingUp(),
    [LeftMenuItems.GovernmentQuestions]: IconGovtqts(),
    [LeftMenuItems.Review]: IconReview(),
}


export const LeftMenu = () => {

    const { state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;

    const { navigation } = NavigationHandler;

    const changeNav = async (nav, index) => {

        if(nav.name === LeftMenuItems.GettingStarted) {
            await CommonActions.resettoPrimaryBorrower(loanInfo, dispatch);
        }
        // let currentNav = state?.leftMenu?.navigation?.currentNav;
        // let navItems = state?.leftMenu?.navigation?.navItems;

        // if (process.env.NODE_ENV === 'development') {
        //     NavigationHandler.changeNav(nav);
        //     return;
        // }

        // let currentIndex = null;
        // navItems?.forEach((n, i) => {
        //     if (n.name === currentNav.name) {
        //         currentIndex = i;
        //     }
        // });
        // if (index < currentIndex || nav.name === currentNav?.name) {
        //     NavigationHandler.changeNav(nav);
        // }
        changeNavIfAllowed(nav, index, false);
    }

    const changeSubNav = (nav, index) => {
        // let currentNav = state?.leftMenu?.navigation?.currentSubNav;
        // let navItems = currentNav?.navItems;


        // let currentIndex = null;
        // navItems?.forEach((n, i) => {
        //     if (n.name === currentNav.name) {
        //         currentIndex = i;
        //     }
        // });

        // if (process.env.NODE_ENV === 'development') {
        //     NavigationHandler.changeNav(nav);
        //     return;
        // }

        // if (index < currentIndex || nav.name === currentNav?.name) {
        //     NavigationHandler.changeSubNav(nav)
        // }
        changeNavIfAllowed(nav, index, true);

    }

    const changeNavIfAllowed = (nav, index, isSub) => {

        let currentNav = state?.leftMenu?.navigation?.currentNav;
        let navItems = state?.leftMenu?.navigation?.navItems;

        if (isSub) {
            navItems = currentNav?.navItems;
            currentNav = state?.leftMenu?.navigation?.currentSubNav;

        }
        if (process.env.NODE_ENV === 'development') {
            NavigationHandler.changeNav(nav);
            return;
        }

        let currentIndex = null;
        navItems?.forEach((n, i) => {
            if (n.name === currentNav?.name) {
                currentIndex = i;
            }
        });
        if (index < currentIndex || nav.name === currentNav?.name) {
            if (isSub) {
                NavigationHandler.changeSubNav(nav)
            }
            NavigationHandler.changeNav(nav);
        }
    }

    const applyClasses = (nav, ind) => {
        let classes = `
        ${!(ind <= navigation?.currentNav?.index) ? 'disabled' : ''} 
        ${(navigation?.currentNav?.index > ind) ? 'passed' : ''} 
        ${navigation?.currentNav?.name === nav?.name ? 'active' : ''}
         `
        console.log(classes);
        return classes;
    }

    return (
        <nav className="loanapp-c-sidenav">
            <ul>
                {
                    navigation?.navItems?.filter(n => !n.isDisabled)?.map((n: any, i: any) => {
                        return (
                            <li className={applyClasses(n, i)}>
                                <a href="javascript:;" onClick={() => { changeNav(n, i) }}>
                                    <span className="nav-icon"> {sideNavIcons[n.name]}</span>
                                    {n.name}
                                </a>
                                {(navigation?.currentNav?.name === n.name && n?.navItems?.length) ? <ul>
                                    {
                                        n?.navItems?.filter(sn => !sn.isDisabled)?.map((sn, si) => {
                                            return (
                                                <li>
                                                    <a href="javascript:;" onClick={() => { changeSubNav(sn, si) }} className={`${location.pathname.includes(sn?.path) ? 'active' : ''}`}>{sn?.name}</a>
                                                </li>
                                            )
                                        })
                                    }
                                </ul> : ''}
                            </li>
                        )
                    })
                }
            </ul>
        </nav>
    )
}

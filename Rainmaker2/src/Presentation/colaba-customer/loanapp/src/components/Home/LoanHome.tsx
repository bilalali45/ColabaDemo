import React, { useContext } from 'react'
import { LeftMenu } from '../NavigationManager/LeftMenu';
import { SelectedNav } from './SelectedNav/SelectedNav';
import Assessment from '../../Shared/Components/Assessment';
import { Store } from '../../store/store';
import { ErrorView } from '../../Shared/Components/ErrorView';
import { UrlQueryManager } from '../../Utilities/Navigation/UrlQueryManager';

export const LoanHome = () => {
    const { state } = useContext(Store);
    const error: any = state.error;
    const errorObj = error.error;

    console.log('===========================================')
    console.log(UrlQueryManager.getQueryData());
    console.log(UrlQueryManager.getQuery('loanApplicationId'));
    console.log('===========================================')
    
    return (
        <div className="loanapp-p-home">
            {errorObj && errorObj.message && <ErrorView />}
            <div className="row">
                <div className="col-md-3 ">
                    {/* <SideNav navItems={leftMenuItems} /> */}
                    <LeftMenu/>
                </div>
                <div className="col-md-9">
                    <div data-testid="selected_nav" id="selected_nav" className="right-panel-warp">
                        {/* // loan app id is to be passed to selectedNav // */}
                        <SelectedNav />
                    </div>
                </div>
            </div>
            <div> <Assessment /> </div>
        </div>
    );
}

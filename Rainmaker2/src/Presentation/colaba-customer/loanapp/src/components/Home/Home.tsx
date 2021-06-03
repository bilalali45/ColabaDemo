import React, { useContext, useEffect, useState } from "react";
import { LeftMenuActionType } from "../../store/reducers/leftMenuReducer";
import {
    useHistory,
    useLocation,
} from "react-router-dom";
import { Store } from "../../store/store";
import { SelectedNav } from "./SelectedNav/SelectedNav";
import { SideNav } from "./SideNav/SideNav";
import "../../lib/rs-authorization.js";
import { ParamsService } from "../../Utilities/helpers/ParamService";
import LeftMenuHandler from "../../store/actions/LeftMenuHandler";
import {
    LoanInfoType,
    PrimaryBorrowerInfo,
    GetReviewBorrowerInfoSectionProto,
} from "../../Entities/Models/types";
import { LoanApplicationActionsType } from "../../store/reducers/LoanApplicationReducer";
import { LocalDB } from "../../lib/LocalDB";
import { OwnTypeEnum } from "../../Utilities/Enum";
import BorrowerActions from "../../store/actions/BorrowerActions";
import { CommonActions } from "../../store/actions/CommonActions";
import { ApplicationEnv } from "../../lib/appEnv";
import Assessment from "../../Shared/Components/Assessment";
import GettingStartedActions from "../../store/actions/GettingStartedActions";
import { ErrorHandler } from "../../Utilities/helpers/ErrorHandler";
import { NavigationHandler } from "../../Utilities/Navigation/NavigationHandler";
import { ErrorView } from "../../Shared/Components/ErrorView";

export const Home = () => {
    const [setLoanAppId] = useState(null);
    const { state, dispatch } = useContext(Store);
    const leftMenu = state?.leftMenu;
    const leftMenuItems = leftMenu?.leftMenuItems;
    const error: any = state.error;
    const errorObj = error.error;

    useEffect(() => {
        authenticate();
    }, []);

    const authenticate = async () => {
        await window.Authorization.authorize();
        console.log('in here ==================== home---------------------------', location.pathname)
        
        if (window.Authorization.checkAuth()) {
            storeLoanInfoData();
            storeLoInfo();    
        }
    };
    
    const location = useLocation();
    const history = useHistory();

    useEffect(() => {

        NavigationHandler.initNavigation({
            location,
            history,
            state,
            dispatch
        });
        setTimeout(() => {
            // NavigationSettings.applySettings(NavigationHandler.navigation);
            setTimeout(() => {
                // NavigationSettings.disableFeature(IncomeSelfEmploymentSteps.SelfEmploymentAddress);
                setTimeout(() => {
                    // NavigationSettings.enableFeature(IncomeSelfEmploymentSteps.SelfEmploymentAddress);
                }, 3000);
            }, 3000);
        }, 3000);

        LeftMenuHandler.initMenu({
            history,
            location,
            dispatch
        });
        LeftMenuHandler.applyNavigationSettings();
        dispatch({
            type: LeftMenuActionType.SetNotAllowedItems,
            payload: LeftMenuHandler.notAllowedSteps,
        });
    }, []);

    // useEffect(() => {
    //     let firstNavPath = LeftMenuHandler.menu.leftMenuItems[0].path;
    //     let firstNavStepPath = LeftMenuHandler.menu.leftMenuItems[0].steps[0].path;
    //     updateMenuState(firstNavPath, firstNavStepPath);
    //     dispatch({
    //         type: LeftMenuActionType.SetNotAllowedItems,
    //         payload: LeftMenuHandler.notAllowedSteps,
    //     });
    // }, []);

    useEffect(() => {
        console.log('---------> 1');
        let currentNav = location.pathname.split("/")[2];
        let currentStep = location.pathname.split("/")[3];
        // if (leftMenuItems.length) {
        updateMenuState(currentNav, currentStep);
        // NavigationHandler.navigation.setCurrentStep(location.pathname);
        // }
        // if (leftMenu && location.pathname.includes('/loanApplication')) {
        // }

    }, [location.pathname]);

    console.log(LeftMenuHandler.paths);
    const updateMenuState = (nav, step, menu?: any) => {

        let {
            items,
            currentNavItem,
            currentNavItemStep,
            currentNavItemIndex,
            currentNavItemStepIndex
        } = LeftMenuHandler.updateCurrentStep(nav, step);
        LeftMenuHandler.setCurrentNavItem(currentNavItem);
        LeftMenuHandler.setCurrentNavItemStep(currentNavItemStep);
        LeftMenuHandler.setCurrentNavItemIndex(currentNavItemIndex);
        LeftMenuHandler.setCurrentNavItemStepIndex(currentNavItemStepIndex);
        if (menu) {
            items = items?.map((lmi, i) => {
                lmi.isDone = menu?.leftMenuItems[i].isDone;
                return lmi;
            })
        }
        dispatch({ type: LeftMenuActionType.SetLeftMenuItems, payload: items });
    };

    const navigateToLastStep = (menu: any) => {
        let savedNav = menu?.leftMenuItems.find((lmi) => lmi.isSelected);
        let savedStep = null;
        let navigateToNextNavItem = null;
        savedNav?.steps.forEach((cs, i) => {
            if (cs.isSelected) {
                savedStep = savedNav.steps[i + 1];
                // savedStep = cs;
                return;
            }
        });
        if (!savedStep) {

            menu.leftMenuItems.forEach((d, i) => {
                if (d.name === savedNav.name) {
                    navigateToNextNavItem = menu.leftMenuItems[i + 1];
                    return;
                }
            });
        }
        if (navigateToNextNavItem) {
            updateMenuState(navigateToNextNavItem.path, navigateToNextNavItem.steps[0].path, menu);
            NavigationHandler.navigateToPath(navigateToNextNavItem.steps[0].path);
        } else {
            updateMenuState(savedNav.path, savedStep?.path, menu);
            NavigationHandler.navigateToPath(savedStep?.path);
        }
    };



    const redirectWork = async (loanapplicationId?: string | null) => {
        if (!loanapplicationId) {
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingStarted/LoanOfficer`);
            return;
        }
        console.log('---------> 2');
        let lastPath = location.pathname.split('/')[location.pathname.split('/').length - 1];
        let loanInfoResponse = await CommonActions.getPendingLoanApplication(
            loanapplicationId
        );
        try {
            loanInfoResponse = JSON.parse(loanInfoResponse?.state);
            // Tenant Setting Update
            LeftMenuHandler.updateTenantSetting(loanInfoResponse?.setting);
            if (loanInfoResponse?.menu) {
                console.log(" ===================== state", loanInfoResponse?.menu);
                LeftMenuHandler.setNotAllowedSteps(loanInfoResponse?.menu?.notAllowedSteps)
                LeftMenuHandler.setDecisions(loanInfoResponse?.menu?.decisionsMade);
                navigateToLastStep(loanInfoResponse?.menu);
            } else if (`/${lastPath}` === ApplicationEnv.ApplicationBasePath) {
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingStarted/LoanOfficer`);
            } else {
                console.log('---------> 3');
                let currentNav = location.pathname.split("/")[2];
                let currentStep = location.pathname.split("/")[3];
                updateMenuState(currentNav, currentStep);
            }
            return loanInfoResponse;
        } catch (error) {
            if (`/${lastPath}` === ApplicationEnv.ApplicationBasePath) {
                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingStarted/LoanOfficer`);
                return null;
            } else {
                console.log('state parse error ===================', error);
                loanInfoResponse = null;
                console.log('---------> 4');
                let currentNav = location.pathname.split("/")[2];
                let currentStep = location.pathname.split("/")[3];
                updateMenuState(currentNav, currentStep);
                return null;
            }
        }
    }

    const storeLoInfo = async () => {
        let response = await GettingStartedActions.getLoInfo();
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                LocalDB.setLOImageUrl(response.data.image);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    };

    const storeLoanInfoData = async () => {
        let loanapplicationId = ParamsService.getParam("loanapplicationid");
        let isEdit = false;

        if (loanapplicationId && loanapplicationId != "new") { isEdit = true }
        loanapplicationId = loanapplicationId || LocalDB.getLoanAppliationId();
        let borrowerId = LocalDB.getBorrowerId();
        console.log("--------------Home Method-->", { loanapplicationId });
        let loanInfoObj: LoanInfoType = {
            loanApplicationId: null,
            loanPurposeId: null,
            loanGoalId: null,
            borrowerId: null,
            ownTypeId: null,
            borrowerName: null,
        };
        let borrowerInfo: PrimaryBorrowerInfo = {
            id: null,
            name: null,
        };
        if (loanapplicationId) {
            if (loanapplicationId === "new") {
                LocalDB.clearSessionStorage();
                redirectWork(null)
            } else {
                await ParamsService.storeParams(loanapplicationId);
                setLoanAppId && setLoanAppId(loanapplicationId);
                const loanInfoResponse = await redirectWork(loanapplicationId);

                if (loanInfoResponse?.borrowerid) {
                    borrowerId = borrowerId || loanInfoResponse?.borrowerid;
                    LocalDB.setBorrowerId(borrowerId);
                    LocalDB.setLoanGoalId(String(loanInfoResponse?.loangoalid));
                    LocalDB.setLoanPurposeId(String(loanInfoResponse?.loanpurposeid));
                }

                if (borrowerId) {
                    let borrowerResponse = await BorrowerActions.getBorrowerInfo(
                        +loanapplicationId,
                        +borrowerId
                    );
                    loanInfoObj.borrowerId = +borrowerId;
                    if (borrowerResponse) {
                        loanInfoObj.ownTypeId = borrowerResponse?.ownTypeId;
                        loanInfoObj.borrowerName = borrowerResponse.firstName+' '+borrowerResponse.lastName;
                    }
                }

                let response = await BorrowerActions.getBorrowersForFirstReview(
                    +loanapplicationId
                );
                if (response.data) {
                    //loanInfoObj.loanPurposeId = response.loanPurpose;
                    //loanInfoObj.loanGoalId = response.loanGoal;
                    //LocalDB.setLoanGoalId(String(response.loanGoal));
                    //LocalDB.setLoanPurposeId(String(response.loanPurpose));
                    let primaryBorrower: GetReviewBorrowerInfoSectionProto = response.data.borrowerReviews?.find(
                        (item) => item.ownTypeId === OwnTypeEnum.PrimaryBorrower
                    );
                    if (primaryBorrower) {
                        if (!borrowerId && isEdit) {

                            loanInfoObj.borrowerId = primaryBorrower.borrowerId;
                            loanInfoObj.ownTypeId = primaryBorrower?.ownTypeId;
                            loanInfoObj.borrowerName = primaryBorrower.firstName+' '+primaryBorrower.lastName;
                            LocalDB.setBorrowerId(String(borrowerId));
                        }
                        borrowerInfo.id = primaryBorrower.borrowerId;
                        borrowerInfo.name = primaryBorrower.firstName+' '+primaryBorrower.lastName;
                    }
                }
                loanInfoObj.loanApplicationId = +loanapplicationId;

            }
        }
        dispatch({
            type: LoanApplicationActionsType.SetLoanInfo,
            payload: loanInfoObj,
        });
        dispatch({
            type: LoanApplicationActionsType.SetPrimaryBorrowerInfo,
            payload: borrowerInfo,
        });
    };

    return (
        <div className="loanapp-p-home">
            {errorObj && errorObj.message && <ErrorView />}
            <div className="row">
                <div className="col-md-3 ">
                    <SideNav navItems={leftMenuItems} />
                </div>
                <div className="col-md-9">
                    <div className="right-panel-warp">
                        <SelectedNav />
                    </div>
                </div>
            </div>
            <div> <Assessment /> </div>
        </div>
    );
};

import { LoanNagivator } from "./LoanNavigator";
import { LeftMenuItems, NavigationItems } from "./navigation_config/NavigationItems";
import { MyMoneySteps } from "../../store/actions/LeftMenuHandler";
import { MyMoney } from "./navigation_config/MyMoney";
import { ApplicationEnv } from "../../lib/appEnv";
import { GettingStarted } from "./navigation_config/GettingStarted";
import { GettingToKnowYou } from "./navigation_config/GettingToKnowYou";
import { MyNewMortgage } from "./navigation_config/MyNewMortgage";
import { LeftMenuActionType } from "../../store/reducers/leftMenuReducer";
import { NavItem } from "./NavItem";
import { NavigationSettings } from "./navigation_settings/NavigationSettings";
import { LocalDB } from "../../lib/LocalDB";
import { Borrower } from "../../Entities/Models/types";
import { TenantConfigFieldNameEnum } from "../Enumerations/TenantConfigEnums";
import { UrlQueryManager } from "./UrlQueryManager";
import { MyProperties, MyPropertiesSteps } from "./navigation_config/MyProperties";
import { FinishingUp } from "./navigation_config/FinishingUp";
import { Declaration } from "./navigation_config/Declaration";
import { FinalReview } from "./navigation_config/FinalReview";

export class NavigationHandler {

    static navigation: LoanNagivator = null;

    static async initNavigation(config) {
        let navigation = new LoanNagivator(
            'Loan App Navigation',
            ApplicationEnv.ApplicationBasePath,
            NavigationItems.navigationItems,
            config);

        navigation.addNavSteps(LeftMenuItems.GettingStarted, GettingStarted.gettingStartedSteps);
        navigation.addNavSteps(LeftMenuItems.GettingToKnowYou, GettingToKnowYou.gettingToKnowYouSteps);
        navigation.addNavSteps(LeftMenuItems.MyNewMortgage, MyNewMortgage.myNewMortgageSteps);
        navigation.addSubNavSteps(LeftMenuItems.MyMoney, MyMoneySteps.Income, MyMoney.myIncomeSteps);
        navigation.addSubNavSteps(LeftMenuItems.MyMoney, MyMoneySteps.Assets, MyMoney.myAssetsSteps);
        navigation.addNavSteps(LeftMenuItems.MyProperties, MyProperties.myPropertiesSteps);

        navigation.addNavSteps(LeftMenuItems.FinishingUp, FinishingUp.finishingUpSteps);
        navigation.addNavSteps(LeftMenuItems.GovernmentQuestions, Declaration.declarationSteps);
        navigation.addNavSteps(LeftMenuItems.Review, FinalReview.finalReviewSteps);

        navigation.updateAllStepsNextPath();
        this.navigation = navigation;
        UrlQueryManager.navigation = navigation;
        NavigationSettings.navigation = navigation;
        await this.updateNavigationFromSavedState(config?.navState);
        await NavigationSettings.applySettings();
        await this.storeNavigationState();
    }

    static updateNavigationFromSavedState(navState) {
        if (navState?.history?.length) {
            this.mapHistory(navState?.history);
        }

        if (navState?.disabledSteps?.length) {
            this.mapDisabledFeatures(navState?.disabledSteps);
        }
    }

    static storeNavigationState() {
        this.navigation.updateAllStepsNextPath();
        this.navigation.config.dispatch({ type: LeftMenuActionType.SetNavigation, payload: this.navigation });
    }


    static changeNav(nav: NavItem) {
        this.navigation.changeCurrentNav(nav);
        this.storeNavigationState();
    }

    static changeSubNav(nav: NavItem) {
        this.navigation.changeCurrentSubNav(nav);
    }

    static getNavigationStateAsString() {
        let disabledSteps = [];
        let stepsDisabled = NavigationSettings.settings?.map(feature => feature?.name);
        stepsDisabled.forEach(s => {
            if (!disabledSteps.includes(s)) {
                disabledSteps.push(s);
            }
        });
        let navState = {
            query: this.navigation.queryString || this.navigation?.config?.location?.search?.split('=')[1],
            lastPath: this.navigation.currentStep.path,
            disabledSteps,
            history: this.navigation.history.map(ds => {
                return {
                    name: ds?.name,
                    query: ds?.query
                }
            }),
        }
        let loanAppData = {
            navState,
            loanapplicationid: LocalDB.getLoanAppliationId() || null,
            borrowerid: LocalDB.getBorrowerId() || null,
            loanpurposeid: LocalDB.getLoanPurposeId() || null,
            loangoalid: LocalDB.getLoanGoalId() || null,
            incomeid: LocalDB.getIncomeId() || null,
            myPropertyTypeId: LocalDB.getMyPropertyTypeId() || null
        }
        return JSON.stringify(loanAppData);
    }

    static closeWizard(path) {
        this.navigation.closeWizard(path);
    }

    static mapHistory(historyState: { name: string, query: string }[]) {
        this.navigation.mapHistory(historyState);
        this.storeNavigationState();
    }


    static mapDisabledFeatures(features: string[]) {
        NavigationSettings.mapDisabledFeatures(features);
        this.storeNavigationState();
    }

    static getDisabledPaths() {
        return NavigationSettings.getDisabledPaths();
    }
    static isStepDisabled(path) {
        return NavigationSettings.isStepDisabled(path);
    }

    static getStepByPath(path) {
        return this.navigation.findStepByPath(path);
    }
    static getNextEnabledStep(path) {
        return this.navigation.getNextEnabledStep(path);
    }

    static navigateToPath(path) {
        this.navigation?.navigateToPath(path);
    }

    static changeCurrentStep(path) {
        this.navigation.changeCurrentStep(path);
    }

    static moveNext() {
        this.navigation?.moveNext();
    }

    static moveBack() {
        this.navigation.moveBack();
    }

    static getPreviousStepPath() {
        let history = this?.navigation?.history;
        if (!history || history.length <= 0) {
            return '';
        }
        return history[history.length - 1]?.path;
    };

    static disableFeature(name) {
        NavigationSettings.disableFeature(name);
        this.storeNavigationState();
    }

    static disableFeatures(features) {
        NavigationSettings.disableFeatures(features);
        this.storeNavigationState();
    }

    static enableFeature(name) {
        NavigationSettings.enableFeature(name);
        this.storeNavigationState();
    }

    static enableFeatures(features) {
        NavigationSettings.enableFeatures(features);
        this.storeNavigationState();
    }

    static getFieldConfig(nameofField: TenantConfigFieldNameEnum) {
        return NavigationSettings.getFieldConfig(nameofField);
    }

    static isFieldVisible(tenantConfigFieldName: TenantConfigFieldNameEnum) {
        return NavigationSettings.isFieldVisible(tenantConfigFieldName);
    }

    static isFieldRequired(tenantConfigFieldName: TenantConfigFieldNameEnum, defaultValue: boolean) {
        return NavigationSettings.isFieldRequired(tenantConfigFieldName, defaultValue);
    }

    static filterBorrowerByFieldConfiguration(borrower: Borrower) {
        return NavigationSettings.filterBorrowerByFieldConfiguration(borrower);
    }

    static updateFeatureSettting(setting: []) {
        var b = NavigationSettings.updateFeatureSettting(setting);
        this.renderDefaultSetting();
        return b;
    }

    static isNavigatedBack() {
        return NavigationSettings?.navigation?.isNavigatedBack;
    }

    static resetNavigationBack() {
        NavigationSettings.navigation.isNavigatedBack = false;
    }

    /**
   * Returns void
   *
   * @remarks
   * This method is used to toggle navigation features according to settings   
   */
    static renderDefaultSetting() {
        if (!this.isFieldVisible(TenantConfigFieldNameEnum.CurrentResidenceMortgage)) {
            NavigationSettings.disableFeatures([MyPropertiesSteps.FirstCurrentResidenceMortgage, MyPropertiesSteps.SecondCurrentResidenceMortgage])
            //NavigationSettings.disableFeatures([MyPropertiesSteps.FirstCurrentResidenceMortgage, MyPropertiesSteps.FirstCurrentResidenceMortgageDetails, MyPropertiesSteps.SecondCurrentResidenceMortgage, MyPropertiesSteps.SecondCurrentResidenceMortgageDetails]);
        }
        else {
            NavigationSettings.enableFeatures([MyPropertiesSteps.FirstCurrentResidenceMortgage, MyPropertiesSteps.SecondCurrentResidenceMortgage])
            //NavigationSettings.enableFeatures([MyPropertiesSteps.FirstCurrentResidenceMortgage, MyPropertiesSteps.FirstCurrentResidenceMortgageDetails, MyPropertiesSteps.SecondCurrentResidenceMortgage, MyPropertiesSteps.SecondCurrentResidenceMortgageDetails]);
        }
        if (!this.isFieldVisible(TenantConfigFieldNameEnum.AdditionalPropertyMortgage)) {
            NavigationSettings.disableFeatures([MyPropertiesSteps.AdditionalPropertyFirstMortgage, MyPropertiesSteps.AdditionalPropertySecondMortgage])
            //NavigationSettings.disableFeatures([MyPropertiesSteps.AdditionalPropertyFirstMortgage, MyPropertiesSteps.AdditionalPropertyFirstMortgageDetails, MyPropertiesSteps.AdditionalPropertySecondMortgage, MyPropertiesSteps.AdditionalPropertySecondMortgageDetails]);
        }
        else {
            NavigationSettings.enableFeatures([MyPropertiesSteps.AdditionalPropertyFirstMortgage, MyPropertiesSteps.AdditionalPropertySecondMortgage])
            //NavigationSettings.enableFeatures([MyPropertiesSteps.AdditionalPropertyFirstMortgage, MyPropertiesSteps.AdditionalPropertyFirstMortgageDetails, MyPropertiesSteps.AdditionalPropertySecondMortgage, MyPropertiesSteps.AdditionalPropertySecondMortgageDetails]);
        }

        if (!this.isFieldVisible(TenantConfigFieldNameEnum.MyProperties)) {
            NavigationSettings.disableFeature(LeftMenuItems.MyProperties);
        }
        else {
            NavigationSettings.enableFeature(LeftMenuItems.MyProperties);
        }

        if (!this.isFieldVisible(TenantConfigFieldNameEnum.PropertyTypeMyProperties)) {
            NavigationSettings.disableFeatures([MyPropertiesSteps.CurrentResidence, MyPropertiesSteps.AdditionalPropertyType]);
        }
        else {
            NavigationSettings.enableFeature([MyPropertiesSteps.CurrentResidence, MyPropertiesSteps.AdditionalPropertyType]);
        }
        this.storeNavigationState();
    }
}
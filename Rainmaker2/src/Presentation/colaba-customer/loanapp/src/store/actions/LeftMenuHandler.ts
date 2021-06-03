import { LeftMenu } from "../../Entities/Models/LeftMenu";
import { MenuItem } from "../../Entities/Models/MenuItem";
import { MenuItemStep } from "../../Entities/Models/MenuItemStep";

import { TenantConfigConditionEnum, TenantConfigFieldNameEnum, TenantConfigType } from "../../Utilities/Enumerations/TenantConfigEnums";

import { LocalDB } from "../../lib/LocalDB";
import { IconFinishingUp, IconGetStarted, IconGetToKnow, IconGovtqts, IconMyMoney, IconNewMortgage, IconProperties, IconReview } from "../../Shared/Components/SVGs";

import { LeftMenuActionType } from "../reducers/leftMenuReducer";


export enum LeftMenuItems {
    GettingStarted = 'Getting Started',
    GettingToKnowYou = 'Getting To Know You',
    MyNewMortgage = 'My New Mortgage',
    MyMoney = 'My Money',
    MyProperties = 'My Properties',
    FinishingUp = 'Finishing Up',
    GovernmentQuestions = 'Government Questions',
    Review = 'Review'
}

export enum GettingStartedSteps {
    LoanOfficer = 'Loan Officer',
    HowCanWeHelp = 'How Can We Help',
    PurchaseProcessState = 'Purchase Process State',
    ReasonForRefinance = 'Reason For Refinance',
    CashOutProcessState = 'Cash Out Process State'
}

export enum GettingStartedPaths {
    LoanOfficer = '/GettingStarted/LoanOfficer',
    HowCanWeHelp = '/GettingStarted/HowCanWeHelp',
    PurchaseProcessState = '/GettingStarted/PurchaseProcessState',
    ReasonForRefinance = '/GettingStarted/ReasonForRefinance',
    CashOutProcessState = '/GettingStarted/CashOutProcessState'
}

export enum GettingToKnowYouSteps {
    AboutYourself = 'About Yourself',
    AboutCurrentHome = 'About Current Home',
    Consent = 'Consent',
    ApplicationReview = 'Application Review',
    CoApplicant = 'Co Applicant',
    MaritalStatus = 'Marital Status',
    MilitaryService = 'Military Service',
    SSN = 'SSN',
    ApplicaitonReviewAfterConsent = 'Applicaiton Review After Agreement'
}

export enum GettingToKnowYouPaths {
    AboutYourself = '/GettingToKnowYou/AboutYourself',
    AboutCurrentHome = '/GettingToKnowYou/AboutCurrentHome',
    Consent = '/GettingToKnowYou/Consent',
    ApplicationReview = '/GettingToKnowYou/ApplicationReview',
    CoApplicant = '/GettingToKnowYou/CoApplicant',
    MaritalStatus = '/GettingToKnowYou/MaritalStatus',
    MilitaryService = '/GettingToKnowYou/MilitaryService',
    SSN = '/GettingToKnowYou/SSN',
    ApplicaitonReviewAfterConsent = '/GettingToKnowYou/ApplicaitonReviewAfterAgreement'
}

export enum MyNewMortgageSteps {
    SubjectPropertyNewHome = 'Subject Property New Home',
    SubjectPropertyUse = 'Subject Property Use',
    SubjectPropertyIntend = 'Subject Property Intend',
    SubjectPropertyAddress = 'Subject Property Address',
    LoanAmountDetail = 'Loan Amount Detail',
    NewMortgageReview = ' New Mortgage Review'
}

export enum MyMoneySteps {
    Income = "Income",
    Assets = "Assets"
}

export enum MyMoneyIncomeSteps {
    AdditionalIncome = "Additional Income",
    BusinessAddress = "Business Address",
    BusinessIncomeType = "Business Income Type",
    BusinessRevenue = "Business Revenue",
    EmployerAddress = "Employer Address",
    EmploymentAlert = "Employment Alert",
    EmploymentHistory = "Employment History",
    EmploymentIncome = "Employment Income",
    IncomeHome = "Income Home",
    IncomeReview = "Income Review",
    MilitaryIncome = "Military Income",
    MilitaryServiceLocation = "Military Service Location",
    ModeOfMilitaryServicePayment = "Mode Of Military Service Payment",
    ModeOfEmploymentIncomePayment = "Mode Of Employment Income Payment",
    NetSelfEmploymentIncome = "Net Self Employment Income",
    OtherInccome = "Other Inccome",
    OtherIncomeDetails = "Other Income Details",
    PreviousEmployment = "Previous Employment",
    PreviousEmploymentAddress = "Previous Employment Address",
    PreviousEmploymentAmount = "Previous Employment Amount",
    RetirementIncomeSource = "Retirement Income Source",
    SelfEmploymentAddress = "Self Employment Address",
    SelfEmploymentIncome = "Self Employment Income",
    SourceOfIncomeSelect = "Sourc eOf Income Select",

}

export enum MyNewMortgagePaths {
    SubjectPropertyNewHome = '/MyNewMortgage/SubjectPropertyNewHome',
    SubjectPropertyUse = '/MyNewMortgage/SubjectPropertyUse',
    SubjectPropertyIntend = '/MyNewMortgage/SubjectPropertyIntend',
    SubjectPropertyAddress = '/MyNewMortgage/SubjectPropertyAddress',
    LoanAmountDetail = '/MyNewMortgage/LoanAmountDetail'
}

export enum Decisions {
    PurchaseProcessState = 'Purchase Process State',
    PreApproval = 'Pre Approval',
    ReasonForRefinance = 'Reason For Refinance',
    CashOutProcessState = 'Cash Out Process State',
    PropertyIdentified = 'Property Identified',
    PropertyNotIdentified = 'Property Not Identified',
    AddBrowerFromApplicationReview = 'Add Brower From Application Review',
    AddBrowerFromApplicationReviewAfterConsent = 'Add Brower From Application Review After Consent',
    AddBrowerFromCoApplicant = 'AddBrower From Co Applicant',
    AddBrowerFromMyNewMortgageReview = 'Add Brower From My New Mortgage Review',
}

type Config = {
    history: any,
    location: any,
    dispatch: Function
}

type TenantConfig = {
    name: string,
    value: number,
}

export default class LeftMenuHandler {

    static config: Config;
    static menu = null;
    static paths = {};
    static selectedNavPath = LeftMenuItems.GettingStarted;
    static selectedStepPath = GettingStartedSteps.LoanOfficer;
    static currentNavItem: MenuItem;
    static currentNavItemStep: MenuItemStep;

    static currentNavItemIndex: number;
    static currentNavItemStepIndex: number;

    static notAllowedSteps = [];

    static decisionsMade = [];

    static nextSteps = [];


    static featureSettings = [
        // type 1 is for Navigation and 2 is for field
        {
            name: TenantConfigFieldNameEnum.AnyPartOfDownPaymentGift,
            dbname: "AnyPartOfDownPaymentGift",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.CoBorowerVeteranStatus, // Conditional Handle on martial post click 
            dbname: "CoBorower_VeteranStatus",
            type: TenantConfigType.Navigation,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.CoBorrowerCellNumber,
            dbname: "CoBorrower_CellNumber",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.CoBorrowerEmailAddress,
            dbname: "CoBorrower_EmailAddress",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.CoBorrowerHomeNumber,
            dbname: "CoBorrower_HomeNumber",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.CoBorrowerWorkNumber,
            dbname: "CoBorrower_WorkNumber",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.CoBorrowerDOB,
            dbname: "CoBorrower_DOB",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.CoBorrowerSSN,
            dbname: "CoBorrower_SSN",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },

        {
            name: TenantConfigFieldNameEnum.PrimaryBorrowerCellNumber,
            dbname: "PrimaryBorrower_CellNumber",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.PrimaryBorrowerHomeNumber,
            dbname: "PrimaryBorrower_HomeNumber",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.PrimaryBorrowerWorkNumber,
            dbname: "PrimaryBorrower_WorkNumber",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.PrimaryBorrowerDOB,
            dbname: "PrimaryBorrower_DOB",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.PrimaryBorrowerSSN,
            dbname: "PrimaryBorrower_SSN",
            type: TenantConfigType.Field,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.PreviosEmployment,
            dbname: "EmploymentHistorySection",
            type: TenantConfigType.Navigation,
            condition: 1
        },
        {
            name: TenantConfigFieldNameEnum.IncomeSection,
            dbname: "IncomeSection",
            type: TenantConfigType.Navigation,
            condition: 1
        },
        {
            name: TenantConfigFieldNameEnum.PrimaryBorowerVeteranStatus,
            dbname: "PrimaryBorower_VeteranStatus",
            type: TenantConfigType.Navigation,
            condition: TenantConfigConditionEnum.Default
        },
        {
            name: TenantConfigFieldNameEnum.WhereAreYouInPurchaseProcess,
            dbname: "WhereAreYouInPurchaseProcess",
            type: TenantConfigType.Navigation,
            condition: TenantConfigConditionEnum.Default
        }
    ]

    static items = [
        {
            id: '1',
            nav: LeftMenuItems.GettingStarted,
            isFirst: true,
            isDone: false,
        },
        {
            id: '2',
            nav: LeftMenuItems.GettingToKnowYou,
            isFirst: false,
            isDone: false,
        },
        {
            id: '3',
            nav: LeftMenuItems.MyNewMortgage,
            isFirst: false,
            isDone: false,
        },
        {
            id: '4',
            nav: LeftMenuItems.MyMoney,
            isFirst: false,
            isDone: false,
        },
        {
            id: '5',
            nav: LeftMenuItems.MyProperties,
            isFirst: false,
            isDone: false,
        },

        {
            id: '6',
            nav: LeftMenuItems.FinishingUp,
            isFirst: false,
            isDone: false,
        },
        {
            id: '7',
            nav: LeftMenuItems.GovernmentQuestions,
            isFirst: false,
            isDone: false,
        },
        {
            id: '8',
            nav: LeftMenuItems.Review,
            isFirst: false,
            isDone: false,
        },

        // LeftMenuItems.GettingStarted,
        // LeftMenuItems.GettingToKnowYou,
        // LeftMenuItems.MyNewMortgage,
        // LeftMenuItems.MyMoney,
        // LeftMenuItems.MyProperties,
        // LeftMenuItems.FinishingUp,
        // LeftMenuItems.GovernmentQuestions,
        // LeftMenuItems.Review
    ];

    static gettingStartedSteps = [
        {
            id: '1.1',
            step: GettingStartedSteps.LoanOfficer,
            firstStep: true,
        },
        {
            id: '1.2',
            step: GettingStartedSteps.HowCanWeHelp,
        },
        {
            id: '1.3',
            lastStep: true,
            step: GettingStartedSteps.PurchaseProcessState,
        },
        // {
        //     id: '1.3',
        //     lastStep: true,
        //     step: GettingStartedSteps.ReasonForRefinance,
        // },
        // {
        //     id: '1.3',
        //     lastStep: true,
        //     step: GettingStartedSteps.CashOutProcessState,
        // }

    ];

    static gettingToKnowYouSteps = [
        {
            id: '2.1',
            step: GettingToKnowYouSteps.AboutYourself,
            firstStep: true,
        },
        {
            id: '2.2',
            step: GettingToKnowYouSteps.AboutCurrentHome
        },
        {
            id: '2.3',
            step: GettingToKnowYouSteps.MaritalStatus
        },
        {
            id: '2.4',
            step: GettingToKnowYouSteps.MilitaryService
        },
        {
            id: '2.5',
            step: GettingToKnowYouSteps.CoApplicant
        },
        {
            id: '2.6',
            step: GettingToKnowYouSteps.ApplicationReview
        },
        {
            id: '2.7',
            step: GettingToKnowYouSteps.SSN
        },
        {
            id: '2.8',
            step: GettingToKnowYouSteps.Consent
        },
        {
            id: '2.9',
            step: GettingToKnowYouSteps.ApplicaitonReviewAfterConsent,
            lastStep: true
        }
        // GettingToKnowYouSteps.AboutYourself,
        // GettingToKnowYouSteps.Consent,
        // GettingToKnowYouSteps.ApplicationReview,
        // GettingToKnowYouSteps.CoApplicant,
        // GettingToKnowYouSteps.MaritalStatus,
        // GettingToKnowYouSteps.MilitaryService,
        // GettingToKnowYouSteps.SSN,

    ];


    static myNewMortgageSteps = [
        {
            id: '3.1',
            step: MyNewMortgageSteps.SubjectPropertyNewHome,
            firstStep: true,
        },
        {
            id: '3.2',
            step: MyNewMortgageSteps.SubjectPropertyUse
        },
        {
            id: '3.3',
            step: MyNewMortgageSteps.SubjectPropertyIntend
        },
        {
            id: '3.4',
            step: MyNewMortgageSteps.SubjectPropertyAddress
        },
        {
            id: '3.5',
            step: MyNewMortgageSteps.LoanAmountDetail,
        },
        {
            id: '3.6',
            step: MyNewMortgageSteps.NewMortgageReview,
            lastStep: true,
        },
    ];

    static myMoneyIncomeSteps = [
        {
            id: '4.1.1',
            step: MyMoneyIncomeSteps.AdditionalIncome,
        },
        {
            id: '4.1.2',
            step: MyMoneyIncomeSteps.BusinessAddress,
        },
        {
            id: '4.1.3',
            step: MyMoneyIncomeSteps.BusinessIncomeType,
        },
        {
            id: '4.1.4',
            step: MyMoneyIncomeSteps.BusinessRevenue,
        },
        {
            id: '4.1.5',
            step: MyMoneyIncomeSteps.EmployerAddress,
        },
        {
            id: '4.1.6',
            step: MyMoneyIncomeSteps.EmploymentAlert,
        },
        {
            id: '4.1.7',
            step: MyMoneyIncomeSteps.EmploymentHistory,
        },
        {
            id: '4.1.8',
            step: MyMoneyIncomeSteps.EmploymentIncome,
        },
        {
            id: '4.1.9',
            step: MyMoneyIncomeSteps.IncomeHome,
        },
        {
            id: '4.1.10',
            step: MyMoneyIncomeSteps.IncomeReview,
        },
        {
            id: '4.1.11',
            step: MyMoneyIncomeSteps.MilitaryIncome,
        },
        {
            id: '4.1.12',
            step: MyMoneyIncomeSteps.MilitaryServiceLocation,
        },
        {
            id: '4.1.13',
            step: MyMoneyIncomeSteps.ModeOfMilitaryServicePayment,
        },
        {
            id: '4.1.14',
            step: MyMoneyIncomeSteps.ModeOfEmploymentIncomePayment,
        },
        {
            id: '4.1.15',
            step: MyMoneyIncomeSteps.NetSelfEmploymentIncome,
        },
        {
            id: '4.1.16',
            step: MyMoneyIncomeSteps.OtherInccome,
        },
        {
            id: '4.1.17',
            step: MyMoneyIncomeSteps.OtherIncomeDetails,
        },
        {
            id: '4.1.18',
            step: MyMoneyIncomeSteps.PreviousEmployment,
        },
        {
            id: '4.1.19',
            step: MyMoneyIncomeSteps.PreviousEmploymentAddress,
        },
        {
            id: '4.1.20',
            step: MyMoneyIncomeSteps.PreviousEmploymentAmount,
        },
        {
            id: '4.1.21',
            step: MyMoneyIncomeSteps.RetirementIncomeSource,
        },
        {
            id: '4.1.22',
            step: MyMoneyIncomeSteps.SelfEmploymentAddress,
        },
        {
            id: '4.1.23',
            step: MyMoneyIncomeSteps.SelfEmploymentIncome,
        },
        {
            id: '4.1.24',
            step: MyMoneyIncomeSteps.SourceOfIncomeSelect,
        }
    ]

    static sideNavIcons = [
        IconGetStarted(),
        IconGetToKnow(),
        IconNewMortgage(),
        IconMyMoney(),
        IconProperties(),
        IconFinishingUp(),
        IconGovtqts(),
        IconReview()
    ]


    static setPaths(paths) {
        this.paths = paths;
        console.log(paths);
    }

    static updateCurrentStep(currentNav, currentStep) {

        console.log('in her -----------------', location.pathname);

        let currentNavItem = null;
        let currentNavItemStep = null;
        let currentNavItemIndex = null;
        let currentNavItemStepIndex = null;

        this.menu.leftMenuItems = this.menu?.leftMenuItems?.map((lmi, i) => {
            let currentIndex = null;
            if (currentNav) {
                if (lmi?.path?.includes(currentNav)) {
                    lmi.isSelected = true;
                    currentIndex = i;
                    currentNavItem = lmi;
                    currentNavItemIndex = i;
                    lmi.steps = lmi.steps.map((lmis, i) => {
                        if (lmis?.path.includes(`${currentStep}`)) {
                            lmis.isSelected = true;
                            currentNavItemStep = lmis;
                            currentNavItemStepIndex = i;
                        } else {
                            lmis.isSelected = false;
                        }
                        return lmis;
                    })
                } else {
                    lmi.isSelected = false;
                }
            }
            if (currentIndex >= i) {
                lmi.isDone = false;
            }
            return lmi;
        });

        // currentNavItemStep.browserPreviousPath = this?.currentNavItemStep?.path;

        return {
            items: this.menu.leftMenuItems,
            currentNavItem,
            currentNavItemStep,
            currentNavItemIndex,
            currentNavItemStepIndex,
        };
    }

    static updateMenuItemIfDone(items, currentNavItemIndex) {
        return items.map((item, i) => {
            console.log('currentNavItemIndex >= i', currentNavItemIndex >= i);
            if (currentNavItemIndex >= i) {
                item.isDone = true;
                return item;
            };
            item.isDone = false;
            return item;
        })
    }

    static applyMenuSettings() {
        let foundNotAllowedItems = [];
        let notAllowedFets = this.featureSettings?.filter((fsi) => {
            if (fsi.condition == TenantConfigConditionEnum.Hidden && fsi.type == TenantConfigType.Navigation) {
                return fsi;
            } else {
                return null;
             }
        }).map((fsif) => fsif.name);


        this.menu.leftMenuItems = this.menu?.leftMenuItems?.filter((lmi) => {
            lmi.steps = lmi.steps?.filter(lmis => {
                if (!notAllowedFets.includes(lmis.name)) {
                    return true;
                } else {
                    foundNotAllowedItems.push(lmis);
                    return false;
                }
            });
            if (!notAllowedFets.includes(lmi.name)) {
                return true;
            } else {
                foundNotAllowedItems.push(lmi);
                return false;
            }
        });

        this.notAllowedSteps = foundNotAllowedItems;
    }

    static applyNavigationSettings() {
        // let foundNotAllowedItems = [];

        for (const item of this.menu.leftMenuItems) {
            this.featureSettings.forEach(fsi => {
                if (fsi.condition == TenantConfigConditionEnum.Hidden && fsi.type == TenantConfigType.Navigation) {
                    if (fsi.name === item.name) {
                        this.notAllowedSteps.push(item);
                    } else {
                        let navItemStep = this.findItem(item.steps, fsi.name);
                        if (navItemStep) {
                            this.notAllowedSteps.push(navItemStep);
                        }
                    }
                }
            })
        }
        console.log('notAllowedFets', this.notAllowedSteps, '...');
        this.handleDisabledFeatures();
    }

    static handleDisabledFeatures() {

        this.menu.leftMenuItems = this?.menu?.leftMenuItems?.map(lmi => {
            let item = this.notAllowedSteps.find(ns => ns?.name === lmi?.name);
            if (item) {
                lmi.isDisabled = true;
                return lmi;
            }
            lmi.isDisabled = false;
            lmi.steps = lmi.steps?.map(lmis => {
                let stepItem = this.notAllowedSteps?.find(s => s?.name === lmis?.name);
                if (!stepItem) {
                    lmis.isDisabled = false;
                    return lmis;
                }
                lmis.isDisabled = true;
                return lmis;
            })
            return lmi;
        });
    }

    static setCurrentNavItem(currentNavItem) {
        this.currentNavItem = currentNavItem;
    }

    static setCurrentNavItemIndex(currentNavItemIndex) {
        this.currentNavItemIndex = currentNavItemIndex;
    }

    static setCurrentNavItemStepIndex(currentNavItemStepIndex) {
        this.currentNavItemStepIndex = currentNavItemStepIndex;
    }

    static setCurrentNavItemStep(currentNavItemStep) {
        this.currentNavItemStep = currentNavItemStep;
    }

    static addNotAllowedStep(navName, stepName) {
        try {


            let navItem = this.findItem(this.menu.leftMenuItems, navName);
            let navItemStep = this.findItem(navItem.steps, stepName);
            this.notAllowedSteps = [...this.notAllowedSteps, navItemStep];
            if (!this.notAllowedSteps.length) {
                return;
            }
            console.log(this.menu);
            this.menu.leftMenuItems = this.menu.leftMenuItems.filter(lmi => {

                let item = this.notAllowedSteps.find(ns => ns?.name === lmi?.name);
                if (!item) {

                    lmi.steps = lmi.steps.filter(lmis => {
                        let stepItem = this.notAllowedSteps.find(s => s?.name === lmis?.name);
                        if (!stepItem) {
                            return lmis;
                        }
                    })

                    return lmi;
                }
            });
            this.config.dispatch({ type: LeftMenuActionType.SetLeftMenuItems, payload: this.menu.leftMenuItems });
            this.config.dispatch({ type: LeftMenuActionType.SetNotAllowedItems, payload: this.notAllowedSteps });
        } catch (error) {
            console.log(error)
        }
    }

    static enableStep(navName, stepName) {
        this._toggleStep(navName, stepName, true);
    }

    static disableStep(navName, stepName) {
        this._toggleStep(navName, stepName, false);
    }

    static _toggleStep(navName, stepName, isAllowedStep) {

        if (!isAllowedStep) {
            let navItem = this.findItem(this.menu.leftMenuItems, navName);
            let navItemStep = this.findItem(navItem.steps, stepName);
            this.notAllowedSteps = [...this.notAllowedSteps, navItemStep];
        } else {
            this.notAllowedSteps = this.notAllowedSteps.filter(nas => nas.name !== stepName);
        }
        this.handleDisabledFeatures();
        this.menu.updateAllStepsNextPaths([
            GettingStartedSteps.HowCanWeHelp,
            // MyNewMortgageSteps.SubjectPropertyIntend
        ]);
        this.menu.updateAllStepsPreviousPaths([
            GettingStartedSteps.PurchaseProcessState,
            GettingStartedSteps.ReasonForRefinance,
            GettingStartedSteps.CashOutProcessState,
            // GettingToKnowYouSteps.AboutYourself,
            // MyNewMortgageSteps.LoanAmountDetail,
            // MyNewMortgageSteps.SubjectPropertyAddress,
        ]);
        this.config.dispatch({ type: LeftMenuActionType.SetLeftMenuItems, payload: this.menu.leftMenuItems });
        this.config.dispatch({ type: LeftMenuActionType.SetNotAllowedItems, payload: this.notAllowedSteps });
    }

    static findItem(list, name) {
        return list.find(ni => ni.name === name);
    }

    static setNotAllowedSteps(steps) {
        if (!steps) return;
        this.notAllowedSteps = [...this.notAllowedSteps, ...steps]
        this.config.dispatch({ type: LeftMenuActionType.SetNotAllowedItems, payload: this.notAllowedSteps });
        this.applyNavigationSettings();
        this.handleDisabledFeatures();
        this.menu.updateAllStepsNextPaths([
            GettingStartedSteps.HowCanWeHelp,
            // MyNewMortgageSteps.SubjectPropertyIntend
        ]);
        this.menu.updateAllStepsPreviousPaths([
            GettingStartedSteps.PurchaseProcessState,
            GettingStartedSteps.ReasonForRefinance,
            GettingStartedSteps.CashOutProcessState,
            // GettingToKnowYouSteps.AboutYourself,
            // MyNewMortgageSteps.LoanAmountDetail,
            // MyNewMortgageSteps.SubjectPropertyAddress,
        ]);
        this.config.dispatch({ type: LeftMenuActionType.SetLeftMenuItems, payload: this.menu.leftMenuItems });
    }

    static setDecisions(decisions) {
        if (!decisions) return;
        this.decisionsMade = [...this.decisionsMade, ...decisions];
    }

    static addDecision(decision) {
        this.decisionsMade.push(Decisions.AddBrowerFromApplicationReview, Decisions.AddBrowerFromCoApplicant);
        this.decisionsMade.push(decision);
        this.clearAddBorrowerDecision(decision);
    }

    static clearAddBorrowerDecision(decision) {
        let decisions = [
            Decisions.AddBrowerFromApplicationReview,
            Decisions.AddBrowerFromCoApplicant,
            Decisions.AddBrowerFromMyNewMortgageReview,
        ];
        decisions = decisions.filter(d => d !== decision);
        let newDecisions = null;
        for (const dec of this.decisionsMade) {
            newDecisions = this.decisionsMade.filter(d => d === dec);
        }
    
        this.decisionsMade = newDecisions;
    }

    static makeDecision(selected) {

        let nav = this.currentNavItem;
        let step = this.currentNavItemStep;
        let next = null;

        switch (selected) {
            case Decisions.PurchaseProcessState:
            case Decisions.ReasonForRefinance:
            case Decisions.CashOutProcessState:
                this.menu.leftMenuItems.forEach(lmi => {
                    let d = lmi.steps.find(lmis => lmis.name === selected);
                    if (d) {
                        next = d;
                        return;
                    }
                });

                this.decisionsMade.push(selected);
                this.menu.updateNextStep(nav, step, next);
                // console.log('this.menu', this.menu);
                break;

            // case Decisions.PreApproval:
            //     nav = this.menu.leftMenuItems[2];
            //     step = this.findItem(nav.steps, MyNewMortgageSteps.SubjectPropertyUse);
            //     next = this.findItem(nav.steps, MyNewMortgageSteps.SubjectPropertyAddress);

            //     this.decisionsMade.push(selected);

            //     break;

            // case Decisions.PropertyIdentified:
            //     nav = this.menu.leftMenuItems[2];
            //     step = this.findItem(nav.steps, MyNewMortgageSteps.SubjectPropertyIntend);
            //     next = this.findItem(nav.steps, MyNewMortgageSteps.SubjectPropertyAddress);

            //     if (this.decisionsMade.includes(Decisions.PropertyNotIdentified)) {
            //         this.decisionsMade = this.decisionsMade.filter(d => d !== Decisions.PropertyNotIdentified);
            //     }
            //     this.decisionsMade.push(selected);

            //     break;

            // case Decisions.PropertyNotIdentified:
            //     nav = this.menu.leftMenuItems[2];
            //     step = this.findItem(nav.steps, MyNewMortgageSteps.SubjectPropertyIntend);
            //     next = this.findItem(nav.steps, MyNewMortgageSteps.LoanAmountDetail);

            //     if (this.decisionsMade.includes(Decisions.PropertyIdentified)) {
            //         this.decisionsMade = this.decisionsMade.filter(d => d !== Decisions.PropertyIdentified);
            //     }
            //     this.decisionsMade.push(selected);
            //     break;

            // case Decisions.CoApplicant:

            //     this.decisionsMade.push(selected);


            //     break;

            default:
                break;
        }

    }

    static navigateBack() {

        let navItem = this.currentNavItem;
        let step = this.currentNavItemStep;
        let previousStep = null;

        switch (this.currentNavItemStep.name) {
            case GettingStartedSteps.PurchaseProcessState:
            case GettingStartedSteps.ReasonForRefinance:
            case GettingStartedSteps.CashOutProcessState:
                previousStep = this.findItem(this.menu.leftMenuItems[0].steps, GettingStartedSteps.HowCanWeHelp);
                this.menu.updatePreviousStep(navItem, step, previousStep);
                break;

            case GettingToKnowYouSteps.AboutYourself:
                if (this.decisionsMade.includes(Decisions.AddBrowerFromApplicationReview)) {
                    previousStep = this.findItem(this.menu.leftMenuItems[1].steps, GettingToKnowYouSteps.ApplicationReview);
                    this.decisionsMade = this.decisionsMade.filter(d => d !== Decisions.AddBrowerFromApplicationReview);
                    this.menu.updatePreviousStep(navItem, step, previousStep);
                    break;
                }
                if (this.decisionsMade.includes(Decisions.AddBrowerFromCoApplicant)) {
                    previousStep = this.findItem(this.menu.leftMenuItems[1].steps, GettingToKnowYouSteps.CoApplicant);
                    this.decisionsMade = this.decisionsMade.filter(d => d !== Decisions.AddBrowerFromCoApplicant);
                    this.menu.updatePreviousStep(navItem, step, previousStep);
                    break;
                }
                if (this.decisionsMade.includes(Decisions.AddBrowerFromApplicationReviewAfterConsent)) {
                    previousStep = this.findItem(this.menu.leftMenuItems[1].steps, GettingToKnowYouSteps.ApplicaitonReviewAfterConsent);
                    this.decisionsMade = this.decisionsMade.filter(d => d !== Decisions.AddBrowerFromApplicationReviewAfterConsent);
                    this.menu.updatePreviousStep(navItem, step, previousStep);
                    break;
                }
                if (this.decisionsMade.includes(Decisions.AddBrowerFromMyNewMortgageReview)) {
                    previousStep = this.findItem(this.menu.leftMenuItems[2].steps, MyNewMortgageSteps.NewMortgageReview);
                    this.decisionsMade = this.decisionsMade.filter(d => d !== Decisions.AddBrowerFromMyNewMortgageReview);
                    this.menu.updatePreviousStep(navItem, step, previousStep);
                    break;
                }
                break;
            // if(!step.previousPath) {
            //     step.previousPath = step.cachedPreviousPath;
            // }


            // case MyNewMortgageSteps.LoanAmountDetail:
            //     if (this.decisionsMade.includes(Decisions.PropertyIdentified)) {
            //         previousStep = this.findItem(this.menu.leftMenuItems[2].steps, MyNewMortgageSteps.SubjectPropertyAddress);
            //     } else {
            //         previousStep = this.findItem(this.menu.leftMenuItems[2].steps, MyNewMortgageSteps.SubjectPropertyIntend);
            //     }
            //     this.menu.updatePreviousStep(navItem, step, previousStep);
            //     break;

            // case MyNewMortgageSteps.SubjectPropertyAddress:
            //     if (this.decisionsMade.includes(Decisions.PreApproval)) {
            //         previousStep = this.findItem(this.menu.leftMenuItems[2].steps, MyNewMortgageSteps.SubjectPropertyUse);
            //     } else {
            //         previousStep = this.findItem(this.menu.leftMenuItems[2].steps, MyNewMortgageSteps.SubjectPropertyIntend);
            //     }
            //     this.menu.updatePreviousStep(navItem, step, previousStep);
            //     break;

            default:
                break;
        }

    }

    static moveNext() {
        this.config.history.push(this.currentNavItemStep?.nextPath);
        // this._handleNav('next');
    }

    static moveBack() {
        console.log('decisions', this.decisionsMade);
        this.navigateBack();
  
        this.config.history.push(this.currentNavItemStep.previousPath);
  
    }

    static _handleNav(nextOrBack) {
        let curItem = this.currentNavItemStep;

        let nextStepItem = null;
        if (nextOrBack === 'next') {

            if (curItem.lastStep) {
                nextStepItem = this.menu.leftMenuItems[this.currentNavItemIndex + 1].steps[0];
            } else {
                nextStepItem = this.currentNavItem.steps[this.currentNavItemStepIndex + 1];
                if (nextStepItem?.isDisabled) {
                    nextStepItem = this.currentNavItem.steps[this.currentNavItemStepIndex + 2]
                }
            }

        } else {

            if (curItem?.firstStep && this.currentNavItemIndex > 0) {
                nextStepItem = this.menu.leftMenuItems[this.currentNavItemIndex - 1].steps[0];
            } else {
                nextStepItem = this.currentNavItem.steps[this.currentNavItemStepIndex - 1];
                if (nextStepItem?.isDisabled) {
                    nextStepItem = this.currentNavItem.steps[this.currentNavItemStepIndex - 2]
                }
            }
        }

        // nextStepItem.referrer = this.currentNavItemStep;
        nextStepItem.previousPath = this.currentNavItemStep.path;
        this.config.history.push(nextStepItem?.path);
    }

    static getMenuStateAsString() {
        this.menu.updateNotAllowedSteps(this.notAllowedSteps);
        this.menu.updateDecisons(this.decisionsMade);
        const currentState = {
            menu: this.menu,
            loanapplicationid: LocalDB.getLoanAppliationId() || null,
            borrowerid: LocalDB.getBorrowerId() || null,
            loanpurposeid: LocalDB.getLoanPurposeId() || null,
            loangoalid: LocalDB.getLoanGoalId() || null
        }
        return JSON.stringify(currentState);
    }


    // static getFieldConfig(nameofField: TenantConfigFieldNameEnum): TenantConfigConditionEnum {
    //     var res = this.featureSettings.find((rec) => rec.name == nameofField)?.condition;
    //     console.log(nameofField + ": " + res)
    //     return res;
    // }

    // static isFieldVisible(tenantConfigFieldName: TenantConfigFieldNameEnum) {
    //     return LeftMenuHandler.getFieldConfig(tenantConfigFieldName) != TenantConfigConditionEnum.Hidden;
    // }

    // static isFieldRequired(tenantConfigFieldName: TenantConfigFieldNameEnum, defaultValue: boolean) {
    //     if (LeftMenuHandler.getFieldConfig(tenantConfigFieldName) == TenantConfigConditionEnum.Required) {
    //         return true;
    //     }
    //     else if (LeftMenuHandler.getFieldConfig(tenantConfigFieldName) == TenantConfigConditionEnum.Optional) {
    //         return false;
    //     }
    //     return defaultValue;
    // }

    // static filterBorrowerByFieldConfiguration(borrower: Borrower) {
    //     let res = true;
    //     if (borrower.ownTypeId == OwnTypeEnum.PrimaryBorrower) {
    //         res = LeftMenuHandler.getFieldConfig(TenantConfigFieldNameEnum.PrimaryBorrowerSSN) != TenantConfigConditionEnum.Hidden || LeftMenuHandler.getFieldConfig(TenantConfigFieldNameEnum.PrimaryBorrowerDOB) != TenantConfigConditionEnum.Hidden;
    //     }
    //     else if (borrower.ownTypeId == OwnTypeEnum.SecondaryBorrower) {
    //         res = LeftMenuHandler.getFieldConfig(TenantConfigFieldNameEnum.CoBorrowerSSN) != TenantConfigConditionEnum.Hidden || LeftMenuHandler.getFieldConfig(TenantConfigFieldNameEnum.CoBorrowerDOB) != TenantConfigConditionEnum.Hidden;
    //     }
    //     return res;
    // }

    static updateTenantSetting(setting: [TenantConfig]) {
        if (setting && setting.length > 0) {
            setting.forEach(element => {
                const index = this.featureSettings.findIndex(rec => rec.dbname == element.name)
                if (index != -1) {
                    this.featureSettings[index].condition = element.value
                }
            });
        }
    }

    static initMenu(config) {
        // let navigation = new LoanNagivator(
        //     'Loan App Navigation',
        //     ApplicationEnv.ApplicationBasePath,
        //     NavigationItems.navigationItems);

        // // navigation.addNavSteps(LeftMenuItems.GettingStarted, GettingStarted.gettingStartedSteps);
        // // navigation.addNavSteps(LeftMenuItems.GettingToKnowYou, GettingToKnowYou.gettingToKnowYouSteps);
        // // navigation.addNavSteps(LeftMenuItems.MyNewMortgage, MyNewMortgage.myNewMortgageSteps);
        // navigation.addSubNavSteps(LeftMenuItems.MyMoney, MyMoneySteps.Income, MyMoney.myIncomeSteps);

        // navigation.updateAllStepsNextPath();
        // console.log('navigation', navigation);

        // setTimeout(() => {
        //     navigation.setCurrentStep('');
        // }, 3000);

        this.menu = new LeftMenu(
            'Left Menu',
            this.items,
            this.selectedNavPath,
            this.selectedStepPath,
            this.sideNavIcons
        );
        this.config = config;
        console.log(this.menu.paths);
        this.setPaths(this.menu.paths)
        this.menu.addStepstoMenuItem(LeftMenuItems.GettingStarted, this.gettingStartedSteps)
        this.menu.addStepstoMenuItem(LeftMenuItems.GettingToKnowYou, this.gettingToKnowYouSteps);
        this.menu.addStepstoMenuItem(LeftMenuItems.MyNewMortgage, this.myNewMortgageSteps);
        this.menu.addStepstoMenuItem(LeftMenuItems.MyMoney, this.myMoneyIncomeSteps);
        this.menu.updateAllStepsNextPaths([
            GettingStartedSteps.HowCanWeHelp,
            // MyNewMortgageSteps.SubjectPropertyIntend
        ]);
        this.menu.updateAllStepsPreviousPaths([
            GettingStartedSteps.PurchaseProcessState,
            GettingStartedSteps.ReasonForRefinance,
            GettingStartedSteps.CashOutProcessState,
            // GettingToKnowYouSteps.AboutYourself,
            // MyNewMortgageSteps.LoanAmountDetail,
            // MyNewMortgageSteps.SubjectPropertyAddress,
        ])
    }



}
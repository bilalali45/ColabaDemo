import { NavStepConfigType } from "../LoanNavigator";

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

export class GettingStarted {
    static gettingStartedSteps : NavStepConfigType[] = [
        {
            name: GettingStartedSteps.LoanOfficer,
            subSteps: []
        },
        {
            name: GettingStartedSteps.HowCanWeHelp,
            subSteps: []
        },
        {
            name: GettingStartedSteps.PurchaseProcessState,
            subSteps: []
        },

    ];

}
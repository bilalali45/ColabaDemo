import { NavStepConfigType } from "../LoanNavigator";

export enum MyNewMortgageSteps {
    SubjectPropertyNewHome = 'Subject Property New Home',
    SubjectPropertyUse = 'Subject Property Use',
    SubjectPropertyIntend = 'Subject Property Intend',
    SubjectPropertyAddress = 'Subject Property Address',
    LoanAmountDetail = 'Loan Amount Detail',
    NewMortgageReview = ' New Mortgage Review'
}

export enum MyNewMortgagePaths {
    SubjectPropertyNewHome = '/MyNewMortgage/SubjectPropertyNewHome',
    SubjectPropertyUse = '/MyNewMortgage/SubjectPropertyUse',
    SubjectPropertyIntend = '/MyNewMortgage/SubjectPropertyIntend',
    SubjectPropertyAddress = '/MyNewMortgage/SubjectPropertyAddress',
    LoanAmountDetail = '/MyNewMortgage/LoanAmountDetail'
}

export class MyNewMortgage {
    static myNewMortgageSteps : NavStepConfigType[] = [
        {
            name: MyNewMortgageSteps.SubjectPropertyNewHome,
            subSteps: [],
        },
        {
            name: MyNewMortgageSteps.SubjectPropertyUse,
            subSteps: [],
        },
        {
            name: MyNewMortgageSteps.SubjectPropertyIntend,
            subSteps: [],
        },
        {
            name: MyNewMortgageSteps.SubjectPropertyAddress,
            subSteps: [],
        },
        {
            name: MyNewMortgageSteps.LoanAmountDetail,
            subSteps: [],
        },
        {
            name: MyNewMortgageSteps.NewMortgageReview,
            subSteps: [],
        },
    ];
}
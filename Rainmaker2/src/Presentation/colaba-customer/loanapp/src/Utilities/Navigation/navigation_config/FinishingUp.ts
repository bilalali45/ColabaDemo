import { NavStepConfigType } from "../LoanNavigator";

export enum FinishingUpSteps {
    BorrowerDependents = 'Borrower Dependents',
    BorrowerMarriedTo = 'Borrower Married To',
    CitizenshipStatus = 'Citizenship Status',
    CoBorrowerDependents = 'Co Borrower Dependents',
    CoResidenceHistory = 'Co Residence History',
    ResidenceAddress = 'Residence Address',
    ResidenceDetail = 'Residence Detail',
    ResidenceMove = 'Residence Move',
    ResidenceAlert = 'Residence Alert',
    ResidenceHistoryList = 'Residence History List',
    Review = 'Review'

}

export class FinishingUp {
    static finishingUpSteps : NavStepConfigType[] = [
        {
            name: FinishingUpSteps.BorrowerDependents,
            subSteps: []
        },
        {
            name: FinishingUpSteps.BorrowerMarriedTo,
            subSteps: []
        },
        {
            name: FinishingUpSteps.CitizenshipStatus,
            subSteps: []
        },
        {
            name: FinishingUpSteps.CoResidenceHistory,
            subSteps: []
        },
        {
            name: FinishingUpSteps.ResidenceAddress,
            subSteps: []
        },
        {
            name: FinishingUpSteps.ResidenceDetail,
            subSteps: []
        },
        {
            name: FinishingUpSteps.ResidenceMove,
            subSteps: []
        },
        {
            name: FinishingUpSteps.ResidenceAlert,
            subSteps: []
        },
        {
            name: FinishingUpSteps.ResidenceHistoryList,
            subSteps: []
        },
        {
            name: FinishingUpSteps.Review,
            subSteps: []
        }
    ];

}
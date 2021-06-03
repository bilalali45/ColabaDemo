import { NavStepConfigType } from "../LoanNavigator";

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


export class GettingToKnowYou {
    static gettingToKnowYouSteps : NavStepConfigType[] = [
        {
            name: GettingToKnowYouSteps.AboutYourself,
            subSteps: [],
        },
        {
            name: GettingToKnowYouSteps.AboutCurrentHome,
            subSteps: [],
        },
        {
            name: GettingToKnowYouSteps.MaritalStatus,
            subSteps: [],
        },
        {
            name: GettingToKnowYouSteps.MilitaryService,
            subSteps: [],
        },
        {
            name: GettingToKnowYouSteps.CoApplicant,
            subSteps: [],
        },
        {
            name: GettingToKnowYouSteps.ApplicationReview,
            subSteps: [],
        },
        {
            name: GettingToKnowYouSteps.SSN,
            subSteps: [],
        },
        {
            name: GettingToKnowYouSteps.Consent,
            subSteps: [],
        },
        {
            name: GettingToKnowYouSteps.ApplicaitonReviewAfterConsent,
            subSteps: [],
        }
    
    ];
}
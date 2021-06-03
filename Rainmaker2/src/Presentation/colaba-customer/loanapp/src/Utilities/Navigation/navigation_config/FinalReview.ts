import { NavStepConfigType } from "../LoanNavigator";

export enum FinalReviewSteps {
    ReviewDetail = 'ReviewDetail',
    
}

export class FinalReview {
    static finalReviewSteps : NavStepConfigType[] = [
        {
            name: FinalReviewSteps.ReviewDetail,
            subSteps: []
        }
    ];

}
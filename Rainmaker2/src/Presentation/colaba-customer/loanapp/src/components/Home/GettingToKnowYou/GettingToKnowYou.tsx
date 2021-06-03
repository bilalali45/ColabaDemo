import React from 'react'
import { AboutYourSelf } from './AboutYourSelf/AboutYourSelf';
import { Consent } from './Consent/Consent';
import { ApplicaitonReview } from './ApplicaitonReview/ApplicaitonReview';
import { CoApplicant } from './CoApplicant/CoApplicant';

import { MaritalStatus } from './MaritalStatus/MaritalStatus';
import MilitaryService from './MilitaryService/MilitaryService';
import { SSN } from './SSN/SSN';
import AboutCurrentHome from './AboutCurrentHome/AboutCurrentHome';

import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../lib/appEnv';
import { ApplicaitonReviewAfterConsent } from './ApplicaitonReviewAfterConsent/ApplicaitonReviewAfterConsent';
import { IsRouteAllowed } from '../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/GettingToKnowYou`;

export const GettingToKnowYou = () => {

    return (
        <div data-testid="gtky-container"className="loanapp-p-getting-to-know fadein">
            <Switch>
                <IsRouteAllowed path={`${containerPath}/AboutYourself`} component={AboutYourSelf} />
                <IsRouteAllowed path={`${containerPath}/AboutCurrentHome`} component={AboutCurrentHome} />
                <IsRouteAllowed path={`${containerPath}/MaritalStatus`} component={MaritalStatus} />
                <IsRouteAllowed path={`${containerPath}/MilitaryService`} component={MilitaryService} />
                <IsRouteAllowed path={`${containerPath}/CoApplicant`} component={CoApplicant} />
                <IsRouteAllowed path={`${containerPath}/ApplicationReview`} component={ApplicaitonReview} />
                <IsRouteAllowed path={`${containerPath}/SSN`} component={SSN} />
                <IsRouteAllowed path={`${containerPath}/Consent`} component={Consent} />
                <IsRouteAllowed path={`${containerPath}/ApplicaitonReviewAfterAgreement`} component={ApplicaitonReviewAfterConsent} />
            </Switch>
        </div>
    )
}

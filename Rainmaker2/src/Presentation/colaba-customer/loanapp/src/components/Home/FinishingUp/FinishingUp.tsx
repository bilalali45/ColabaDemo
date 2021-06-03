import React from 'react'

import { Switch } from 'react-router';

import { ApplicationEnv } from '../../../lib/appEnv';

import { IsRouteAllowed } from '../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { BorrowerDependents } from './BorrowerDependents/BorrowerDependents';
import { BorrowerMarriedTo } from './BorrowerMarriedTo/BorrowerMarriedTo';
import { CitizenshipStatus } from './CitizenshipStatus/CitizenshipStatus';
import { CoBorrowerDependents } from './CoBorrowerDependents/CoBorrowerDependents';
import { CoResidenceHistory } from './CoResidenceHistory/CoResidenceHistory';
import { ResidenceAddress } from './ResidenceAddress/ResidenceAddress';
import { ResidenceDetail } from './ResidenceDetail/ResidenceDetail';
import { ResidenceMove } from './ResidenceMove/ResidenceMove';
import { ResidenceAlert } from './ResidencyAlert/ResidencyAlert';
import { ResidenceHistoryList } from './ResidencyHistoryList/ResidenceHistoryList';
import { Review } from './Review/Review';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/FinishingUp`;

export const FinishingUp = () => {

    return (
        <div data-testid="gtky-container"className="loanapp-p-getting-to-know fadein">
            <Switch>
                <IsRouteAllowed path={`${containerPath}/BorrowerDependents`} component={BorrowerDependents} />
                <IsRouteAllowed path={`${containerPath}/BorrowerMarriedTo`} component={BorrowerMarriedTo} />
                <IsRouteAllowed path={`${containerPath}/CitizenshipStatus`} component={CitizenshipStatus} />
                <IsRouteAllowed path={`${containerPath}/CoBorrowerDependents`} component={CoBorrowerDependents} />
                <IsRouteAllowed path={`${containerPath}/CoResidenceHistory`} component={CoResidenceHistory} />
                <IsRouteAllowed path={`${containerPath}/ResidenceAddress`} component={ResidenceAddress} />
                <IsRouteAllowed path={`${containerPath}/ResidenceDetail`} component={ResidenceDetail} />
                <IsRouteAllowed path={`${containerPath}/ResidenceMove`} component={ResidenceMove} />
                <IsRouteAllowed path={`${containerPath}/ResidenceAlert`} component={ResidenceAlert} />
                <IsRouteAllowed path={`${containerPath}/ResidenceHistoryList`} component={ResidenceHistoryList} />
                <IsRouteAllowed path={`${containerPath}/Review`} component={Review} />
            </Switch>
        </div>
    )
}

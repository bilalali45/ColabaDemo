import React from 'react'

import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../lib/appEnv';

import { IsRouteAllowed } from '../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { ReviewDetail } from './ReviewDetail/ReviewDetail';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/Review`;

export const Review = () => {

    return (
        <div data-testid="gtky-container"className="loanapp-p-getting-to-know fadein">
            <Switch>
                <IsRouteAllowed path={`${containerPath}/ReviewDetail`} component={ReviewDetail} />
            </Switch>
        </div>
    )
}

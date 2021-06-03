import React from 'react'

import { Switch } from 'react-router';
import { ApplicationEnv } from '../../../lib/appEnv';

import { IsRouteAllowed } from '../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { BorrowerDeclarations } from './BorrowerDeclarations/BorrowerDeclarations';


const containerPath = `${ApplicationEnv.ApplicationBasePath}/GovernmentQuestions`;

export const GovernmentQuestions = () => {

    return (
        <div data-testid="gtky-container"className="loanapp-p-getting-to-know fadein">
            <Switch>
                <IsRouteAllowed path={`${containerPath}/BorrowerDeclarations`} component={BorrowerDeclarations} />
            </Switch>
            {/* <button onClick={ () => { NavigationHandler.moveNext(); } } >Move Next</button> */}
        </div>
    )
}

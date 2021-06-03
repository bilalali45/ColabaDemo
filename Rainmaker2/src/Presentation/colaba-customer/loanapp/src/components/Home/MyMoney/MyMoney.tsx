import React from 'react'
import {  Switch } from 'react-router-dom';
import { ApplicationEnv } from '../../../lib/appEnv';
import { IsRouteAllowed } from '../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';

import { Assets } from './Assets/Assets';
import { Income } from './Income/Income';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney`;

export const MyMoney = () => {

    return (
        <div>
            <Switch>
                <IsRouteAllowed path={`${containerPath}/Income`} component={Income} />
                <IsRouteAllowed path={`${containerPath}/Assets`} component={Assets} />
            </Switch>
        </div>
    )
}

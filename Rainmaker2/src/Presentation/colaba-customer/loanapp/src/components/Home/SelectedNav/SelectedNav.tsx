import React from 'react'
import { Switch } from 'react-router'
import { ApplicationEnv } from '../../../lib/appEnv'
import { GettingStarted } from '../GettingStarted/GettingStarted'
import { GettingToKnowYou } from '../GettingToKnowYou/GettingToKnowYou'
import { MyNewMortgage } from '../MyNewMortgage/MyNewMortgage'
import { MyMoney } from '../MyMoney/MyMoney'
import { MyProperties } from '../MyProperties/MyProperties'
import { IsRouteAllowed } from '../../../Utilities/Navigation/navigation_settings/IsRouteAllowed'
import { FinishingUp } from '../FinishingUp/FinishingUp'
import { GovernmentQuestions } from '../GovernmentQuestions/GovernmentQuestions'
import { Review } from '../Review/Review'


export const SelectedNav = () => {

    return (
        <Switch>
            <IsRouteAllowed path={`${ApplicationEnv.ApplicationBasePath}/GettingStarted`} component={GettingStarted} />
            <IsRouteAllowed path={`${ApplicationEnv.ApplicationBasePath}/GettingToKnowYou`} component={GettingToKnowYou} />
            <IsRouteAllowed path={`${ApplicationEnv.ApplicationBasePath}/MyNewMortgage`} component={MyNewMortgage} />
            <IsRouteAllowed path={`${ApplicationEnv.ApplicationBasePath}/MyMoney`} component={MyMoney} />
            <IsRouteAllowed path={`${ApplicationEnv.ApplicationBasePath}/MyProperties`} component={MyProperties} />

            <IsRouteAllowed path={`${ApplicationEnv.ApplicationBasePath}/FinishingUp`} component={FinishingUp} />
            <IsRouteAllowed path={`${ApplicationEnv.ApplicationBasePath}/GovernmentQuestions`} component={GovernmentQuestions} />
            <IsRouteAllowed path={`${ApplicationEnv.ApplicationBasePath}/Review`} component={Review} />
        </Switch>
    )
}

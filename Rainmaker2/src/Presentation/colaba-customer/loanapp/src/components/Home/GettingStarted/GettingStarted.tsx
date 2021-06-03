import React from "react";
import { Switch } from "react-router";
import { ApplicationEnv } from "../../../lib/appEnv";
import { IsRouteAllowed } from "../../../Utilities/Navigation/navigation_settings/IsRouteAllowed";
import { HowCanWeHelp } from "./HowCanWeHelp/HowCanWeHelp";
import { LoanOfficer } from "./LoanOfficer/LoanOfficer";
import { PurchaseProcessState } from "./PurchaseProcessState/PurchaseProcessState";

const containerPath = `${ApplicationEnv.ApplicationBasePath}/GettingStarted`;

export const GettingStarted = () => {

  return <div data-testid="gs-container" id="gs-container" className="getstart-wrap fadein">

    <Switch>
      <IsRouteAllowed path={`${containerPath}/LoanOfficer`} component={LoanOfficer}/>
      <IsRouteAllowed path={`${containerPath}/HowCanWeHelp`} component={HowCanWeHelp}/>
      <IsRouteAllowed path={`${containerPath}/PurchaseProcessState`} component={PurchaseProcessState}/>
      <IsRouteAllowed path={`${containerPath}/ReasonForRefinance`} component={PurchaseProcessState}/>
      <IsRouteAllowed path={`${containerPath}/CashOutProcessState`} component={PurchaseProcessState}/>
    </Switch>

  </div>;

};

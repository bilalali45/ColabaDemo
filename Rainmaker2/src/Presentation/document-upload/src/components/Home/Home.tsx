import React, { useEffect, useState, Component } from "react";
import {
  Switch,
  Route,
  useLocation,
  useHistory,
  Redirect,
  useParams,
} from "react-router-dom";
import { Activity } from "./Activity/Activity";
import { UploadedDocuments } from "./UploadedDocuments/UploadedDocuments";
import { Http } from "../../services/http/Http";
import ActivityHeader from "./AcitivityHeader/ActivityHeader";
import { LoanApplication } from "../../entities/Models/LoanApplication";
import { UserActions } from "../../store/actions/UserActions";
import { RainsoftRcHeader, RainsoftRcFooter } from "rainsoft-rc";
import { DocumentsStatus } from "./Activity/DocumentsStatus/DocumentsStatus";
import { DocumentRequest } from "./DocumentRequest/DocumentRequest";
import ImageAssets from "../../utils/image_assets/ImageAssets";
import { PageNotFound } from "../../shared/Errors/PageNotFound";
import { Authorized } from "../../shared/Components/Authorized/Authorized";
import { ParamsService } from "../../utils/ParamsService";

export class Home extends Component {
  componentDidMount() {
    this.setParams(this.props);
  }

  setParams = (props: any) => {
    console.log("Props", props);
    const { loanApplicationId } = props.match.params;
    debugger;
    if (!isNaN(loanApplicationId)) {
      ParamsService.storeParams(loanApplicationId);
    } else {
      window.open("/404", "_self");
    }
  };

  render() {
    return (
      <div>
        {/* {!window.location.pathname.includes("404") && (
          <ActivityHeader {...this.props} />
        )} */}
        <main className="page-content">
          <div className="container">
            <Switch>
              <Redirect
                exact
                from={"/:loanApplicationId"}
                to={"/activity/:loanApplicationId"}
              />
              <Authorized
                path="/activity/:loanApplicationId"
                component={Activity}
              />
              <Authorized
                path="/documentsRequest/:loanApplicationId"
                component={DocumentRequest}
              />
              <Authorized
                path="/uploadedDocuments/:loanApplicationId"
                component={UploadedDocuments}
              />
              <Route path="/404" component={PageNotFound} />
              <Redirect exact from={"*"} to={"/404"} />
            </Switch>
          </div>
        </main>
      </div>
    );
  }
}

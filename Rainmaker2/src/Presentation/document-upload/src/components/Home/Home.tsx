import React, { Component } from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import { Activity } from "./Activity/Activity";
import { UploadedDocuments } from "./UploadedDocuments/UploadedDocuments";
import ActivityHeader from "./AcitivityHeader/ActivityHeader";
import { DocumentRequest } from "./DocumentRequest/DocumentRequest";
import { PageNotFound } from "../../shared/Errors/PageNotFound";
import { Authorized } from "../../shared/Components/Authorized/Authorized";
import { ParamsService } from "../../utils/ParamsService";

export class Home extends Component<any> {
  
  componentDidMount() {

    this.setParams(this.props);
  }

  setParams = (props: any) => {
    const { loanApplicationId } = props?.match?.params;
    if (!isNaN(loanApplicationId)) {
      ParamsService.storeParams(loanApplicationId);
    } else {
      if (window?.open) {
        window?.open("/404", "_self");
      }
    }
  };

  render() {
    window.isMobile = sessionStorage.getItem("isMobile");
    window.width = sessionStorage.getItem("width");
    return (
      <div data-testid="activity">
        <ActivityHeader {...this.props} />
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

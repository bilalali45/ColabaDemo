import React, { useContext, useEffect, useLayoutEffect, useState } from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import Activity from "./Activity/Activity";
import { UploadedDocuments } from "./UploadedDocuments/UploadedDocuments";
import ActivityHeader from "./AcitivityHeader/ActivityHeader";
import { DocumentRequest } from "./DocumentRequest/DocumentRequest";
import { PageNotFound } from "../../shared/Errors/PageNotFound";
import { Authorized } from "../../shared/Components/Authorized/Authorized";
import { ParamsService } from "../../utils/ParamsService";
import { Store } from "../../store/store";
import { LoanActionsType } from "../../store/reducers/loanReducer";

const Home = (props: any) => {
  const {state, dispatch} = useContext(Store);
  
  const getWindowSize = () => {
    const [size, setSize] = useState([0, 0]);
    useLayoutEffect(() => {
      function updateSize() {
        setSize([window.innerWidth, window.innerHeight]);
      }
      window.addEventListener('resize', updateSize);
      updateSize();
      return () => window.removeEventListener('resize', updateSize);
    }, []);
    return size;
  }

  const [width, height] = getWindowSize();

  useEffect(() => {
    let isMobile = sessionStorage.getItem('isMobile');
    dispatch({type: LoanActionsType.SetIsMobile, payload: {value: isMobile === "false" ? false : true}});
  }, [width, height]);

  useEffect(() => {
    setParams(props);
  }, [])



 const setParams = async(props: any) => {
    const { loanApplicationId } = props?.match?.params;
    if (!isNaN(loanApplicationId)) {
     await ParamsService.storeParams(loanApplicationId);
    } else {
      if (window?.open) {
        window?.open("/404", "_self");
      }
    }
  };

    return (
      <div data-testid="activity">
        <ActivityHeader {...props} />
        <div className={`page-content ${"mainPage-"+window.location?.pathname.split("/")[2]}`}>
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
        </div>
      </div>
    );
  }


export default Home;
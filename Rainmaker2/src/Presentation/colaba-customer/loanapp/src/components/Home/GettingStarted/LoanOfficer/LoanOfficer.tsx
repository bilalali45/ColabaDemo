import React, { useContext, useEffect, useState } from "react";
import { LoanOfficerType } from "../../../../Entities/Models/types";
import { LocalDB } from "../../../../lib/LocalDB";
import {
  IconPhone,
  IconEmail,
  IconWeb,
} from "../../../../Shared/Components/SVGs";
import GettingStartedActions from "../../../../store/actions/GettingStartedActions";
import { Store } from "../../../../store/store";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { ApplicationEnv } from "../../../../lib/appEnv";

export const LoanOfficer = () => {
  const { dispatch } = useContext(Store);
  const [loDetail, setLoDetail] = useState<LoanOfficerType>();
  const [btnClick, setBtnClick] = useState<boolean>(false);

  useEffect(() => {
    getLoInfo();
  }, []);

  const getLoInfo = async () => {
    let response = await GettingStartedActions.getLoInfo();
    if(response){
      
      if (ErrorHandler.successStatus.includes(response.statusCode)) {
        LocalDB.setLOImageUrl(response.data.image);
        setLoDetail(response.data);
      }else{
        ErrorHandler.setError(dispatch, response);
      }
    }
  };
 
  if (LocalDB.getLoanGoalId()) {
    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingStarted/PurchaseProcessState`);
    return <React.Fragment/>;
  }
 
  return (
    <div data-testid="lo-header" className="lo-wrap fadein">

      { loDetail  != undefined ? (
        loDetail?.isLoanOfficer == false ? (
          <div className="lo-h-wrap">
            <h1>If You Have Any Questions, We Are Here To Help</h1>
          </div>
        ) : (
          <div className="lo-h-wrap">
            <h2>If You Have Any Questions, We Are Here To Help</h2>
            <h1>I'll Be Your Loan Officer Throughout The Application Process</h1>
          </div>
        )
       )
         :
         ""
      }

      <div data-testid="lo-box" className="lo-wrap-card">
        <div className="lo-wrap-card-box">
          <div className="lo-wrap-card-box--logo">
            <div className="img-wrap">
              <img src={loDetail?.image} alt="" />
            </div>
            {loDetail?.isLoanOfficer && (
              <div data-testid="lo-name" className="lo-bio">
                <h4 title="Kevin">{loDetail?.name}</h4>
                <div className="lo-bio-text">Your Loan Officer</div>
              </div>
            )}
          </div>
          <div className="lo-wrap-card-box--info">
            <div className="c-wrap">
              <ul>
                <li>
                  <a
                    href={`tel:${loDetail?.phone}`}
                    title={`${loDetail?.phone}`}
                  >
                    <span className="c-icon">
                      <IconPhone />
                    </span>
                    <span className="c-text">{loDetail?.phone}</span>
                  </a>
                </li>
                <li>
                  <a
                    href={`mailto:${loDetail?.email}`}
                    title={`${loDetail?.email}`}
                  >
                    <span className="c-icon">
                      <IconEmail />
                    </span>
                    <span className="c-text"> {loDetail?.email}</span>
                  </a>
                </li>
                <li>
                  <a href={loDetail?.url} title={loDetail?.url?.split("/")[2]}>
                    <span className="c-icon">
                      <IconWeb />
                    </span>
                    <span className="c-text">
                      {" "}
                      {loDetail?.url?.split("/")[2]}
                    </span>
                  </a>
                </li>
              </ul>
              <div className="btn-wrap">
                <button data-testid="lo-continue-btn"
                id="lo-continue-btn"
                  className="btn btn-primary"
                  onClick={() => {
                    if(!btnClick){
                      setBtnClick(true);
                      NavigationHandler.moveNext();
                    }               
                  }}
                >
                  Continue
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
      
    </div>
  );
};

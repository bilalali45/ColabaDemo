import React, { useState, useEffect, useContext, useRef } from "react";
import { useLocation, Link, useHistory } from "react-router-dom";
import { LoanStatus } from "../Activity/LoanStatus/LoanStatus";
import { Store } from "../../../store/store";
import { AlertBox } from "../../../shared/Components/AlertBox/AlertBox";
import { Auth } from "../../../services/auth/Auth";
import { debug } from "console";
import { DocumentsActionType } from "../../../store/reducers/documentReducer";
const ActivityHeader = (props) => {
  const [leftNav, setLeftNav] = useState("");
  const [showAlert, setshowAlert] = useState(false);
  const [rightNav, setRightNav] = useState("");
  const [leftNavUrl, setLeftNavUrl] = useState("");
  const [rightNavUrl, setRightNavUrl] = useState("");
  const [currentUrl, setCurrentUrl] = useState("");
  const [browserBack, setbrowserBack] = useState(false);


  const location = useLocation();
  const history = useHistory();
  const { state, dispatch } = useContext(Store);
  const { pendingDocs, currentDoc }: any = state.documents;

  const selectedFiles = currentDoc?.files || [];

  const activityHeadeRef = useRef<HTMLDivElement>(null);

  const setNavigations = (pathname) => {
    if (pathname.includes("activity")) {
      setLeftNav("Dashboard");
      setRightNav("Documents");
      setLeftNavUrl("/DashBoard");
      setRightNavUrl("/uploadedDocuments/" + Auth.getLoanAppliationId());
    }

    if (pathname.includes("documentsRequest")) {
      setLeftNav("Loan Center");
      setRightNav("Documents");
      setLeftNavUrl("/activity/" + Auth.getLoanAppliationId());
      setRightNavUrl("/uploadedDocuments/" + Auth.getLoanAppliationId());
    }

    if (pathname.includes("uploadedDocuments")) {
      if (
        props.location.state == undefined ||
        props.location.state.from.includes("/activity")
      ) {
        setLeftNav("Loan Center");
        setLeftNavUrl("/activity/" + Auth.getLoanAppliationId());
        if (pendingDocs?.length > 0) {
          setRightNav("Document Request");
          setRightNavUrl("/documentsRequest/" + Auth.getLoanAppliationId());
        } else {
          setRightNav("");
          setRightNavUrl("");
        }
      } else {
        setLeftNav("Document Request");
        setLeftNavUrl("/documentsRequest/" + Auth.getLoanAppliationId());
        setRightNav("");
        setRightNavUrl("");
      }
    }
  };

  useEffect(() => {
    setNavigations(location.pathname);


  }, [location.pathname]);

  useEffect(() => {
    let files =
      selectedFiles.filter((f) => f.uploadStatus === "pending").length > 0;
    if (activityHeadeRef.current) {
      if (files) {
        activityHeadeRef.current.onclick = showAlertPopup;
      } else {
        activityHeadeRef.current.onclick = null;
      }
    }

  }, [selectedFiles]);

  useEffect(() => {
    window.onpopstate = backHandler;

    if (location.pathname.includes('view')) {
      window.onpopstate = () => { };
    }
  }, [location?.pathname, selectedFiles])

  const showAlertPopup = (e) => {

    if (e.target.tagName === "A") {
      setshowAlert(true);
    }
  };

  let backHandler = async (event) => {
    event.preventDefault();
    let files = selectedFiles.filter((f) => f.uploadStatus === "pending").length > 0;
    if (files) {
      let cur = currentDoc;
      history.push(location.pathname);
      setTimeout(() => {
        dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: cur });
        setbrowserBack(() => {
          setshowAlert(true);
          return true
        });
      }, 0);
      return;
    }
    // setbrowserBack(false);
  };

  const renderLeftNav = () => {
    if (leftNav === "Dashboard") {
      return (
        <a
          data-testid='left-nav'

          tabIndex={-1}
          onClick={() => {
            if (showAlert) {
              return;
            }
            window.open("/Dashboard", "_self");
          }}
        >
          <i className="zmdi zmdi-arrow-left"></i>
          {leftNav}
        </a>
      );
    }
    return (
      <Link
        data-testid='left-nav'
        onClick={() => setCurrentUrl(leftNavUrl)}
        to={{
          pathname: showAlert ? location.pathname : leftNavUrl,
          state: { from: location.pathname },
        }}
      >
        <i className="zmdi zmdi-arrow-left"></i>
        {leftNav}
      </Link>
    );
  };

  return (
    <div data-testid="activity-header" className="activityHeader">
      <section className="compo-loan-status">
        <LoanStatus />
      </section>
      <section ref={activityHeadeRef} className="row-subheader">
        <div className="row">
          <div className="container">
            <div className="sub-header-wrap">
              <div className="row">
                <div className="col-6">
                  <ul className="breadcrmubs">
                    <li>{renderLeftNav()}</li>
                  </ul>
                </div>
                <div className="col-6 text-right">
                  <div className="action-doc-upload">
                    <Link
                      data-testid='right-nav'
                      onClick={() => setCurrentUrl(rightNavUrl)}
                      to={{
                        pathname: showAlert ? location.pathname : rightNavUrl,
                        state: { from: location.pathname },
                      }}
                    >
                      {rightNav}
                    </Link>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
      {showAlert && (
        <AlertBox
          navigateUrl={currentUrl}
          isBrowserBack={browserBack}
          hideAlert={() => setshowAlert(false)}
        />
      )}
    </div>
  );
};

export default ActivityHeader;

import React, { useState, useEffect, useContext, useRef } from "react";
import { useLocation, Link } from "react-router-dom";
import { LoanStatus } from "../Activity/LoanStatus/LoanStatus";
import { Store } from "../../../store/store";
import { AlertBox } from "../../../shared/Components/AlertBox/AlertBox";
import { Auth } from "../../../services/auth/Auth";
const ActivityHeader = (props) => {
  const [leftNav, setLeftNav] = useState("");
  const [showAlert, setshowAlert] = useState<boolean>(false);
  const [rightNav, setRightNav] = useState("");
  const [leftNavUrl, setLeftNavUrl] = useState("");
  const [rightNavUrl, setRightNavUrl] = useState("");
  const [currentUrl, setCurrentUrl] = useState("");

  const location = useLocation();
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
      setLeftNav("Home");
      setRightNav("Documents");
      setLeftNavUrl("/activity/" + Auth.getLoanAppliationId());
      setRightNavUrl("/uploadedDocuments/" + Auth.getLoanAppliationId());
    }

    if (pathname.includes("uploadedDocuments")) {
      if (
        props.location.state == undefined ||
        props.location.state.from.includes("/activity")
      ) {
        setLeftNav("Home");
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

  const showAlertPopup = (e) => {
    if (e.target.tagName === "A") {
      setshowAlert(true);
    }
  };

  const renderLeftNav = () => {
    if (leftNav === "Dashboard") {
      return (
        <a
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
    <div className="activityHeader">
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
          hideAlert={() => setshowAlert(false)}
        />
      )}
    </div>
  );
};

export default ActivityHeader;

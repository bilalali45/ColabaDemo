import React from "react";
import Dropdown from 'react-bootstrap/Dropdown';
import Tabs from 'react-bootstrap/Tabs';
import Tab from 'react-bootstrap/Tab';
import TabContainer from 'react-bootstrap/TabContainer';
import TabContent from 'react-bootstrap/TabContent';
import TabPane from 'react-bootstrap/TabPane';

export const ReviewDocumentHeader = ({
  documentDetail,
  buttonsEnabled,
  onClose,
  nextDocument,
  previousDocument,
}: {
  documentDetail: boolean
  buttonsEnabled: boolean
  onClose: () => void;
  nextDocument: () => void;
  previousDocument: () => void;
}) => {
  return (
    <div
      id="ReviewDocumentHeader"
      data-component="ReviewDocumentHeader"
      className="review-document-header"
    >
      <div className="row">
        {!documentDetail && (
          <React.Fragment>
            <div className="review-document-header--left col-md-4">
              <h2>Review Document</h2>
            </div>
            <div className="review-document-header--center col-md-4">
              <div className="btn-group">
                <button className="btn btn-default" onClick={buttonsEnabled ? previousDocument : () => { }}>
                  <em className="zmdi zmdi-arrow-left"></em> Review Previous
              Document
            </button>
                <button className="btn btn-default" onClick={buttonsEnabled ? nextDocument : () => { }}>
                  Review Next Document <em className="zmdi zmdi-arrow-right"></em>
                </button>
              </div>
            </div>
          </React.Fragment>
        )}
        <div className={`review-document-header--right col-md-${!documentDetail ? 4 : 12}`}>

          {/* <button className="btn btn-primary">Activity Log</button> */}
          <Dropdown>
                    <Dropdown.Toggle size="lg" variant="primary" className="mcu-dropdown-toggle no-caret" id="dropdown-basic">
                    Activity Log
                    </Dropdown.Toggle>
                    <Dropdown.Menu show>
                        <section className="vertical-tabs">
                          <div className="vertical-tabs--aside">
                            <header className="vertical-tabs--header">
                              <h2 className="vertical-tabs--header-title">Activity Log</h2>
                            </header>
                            <section className="vertical-tabs--body">
                              <ul className="vertical-tabs--list">
                                <li>
                                  <a href="">
                                    <h6>Requested By</h6>
                                    <h2>Zohaib Siddiqui</h2>
                                    <time className="vertical-tabs--list-time">Jan, 19 at 11:00 PM</time>
                                  </a>
                                </li>
                                <li>
                                  <a href="">
                                    <h6>Requested By</h6>
                                    <h2>Zohaib Siddiqui</h2>
                                    <time className="vertical-tabs--list-time">Jan, 19 at 11:00 PM</time>
                                  </a>
                                </li>
                              </ul>
                            </section>
                          </div>

                          <div className="vertical-tabs--content">
                            <header className="vertical-tabs--header flex">
                              <div className="vertical-tabs--header-left">
                                <h2 className="vertical-tabs--header-title">Log Details</h2>
                              </div>
                              <div className="vertical-tabs--header-right">
                                <button className="btn-go">View Email Log <em className="zmdi zmdi-arrow-right"></em></button>
                              </div>
                            </header>
                            <section className="vertical-tabs--body">
                              <div className="table-responsive">
                               <table className="table table-bordered">
                                <thead>
                                  <tr>
                                    <th>Events</th>
                                    <th>Date & Time</th>
                                  </tr>
                                </thead>
                                <tbody>
                                  <tr>
                                    <td>Status changed : Borrower to do</td>
                                    <td>Jan, 19 at 11:00 PM</td>
                                  </tr>
                                  <tr>
                                    <td>Status changed : Started</td>
                                    <td>Jan, 19 at 11:00 PM</td>
                                  </tr>
                                  <tr>
                                    <td>File submitted : Bank-statement-Jan-to-Mar-2020-1.jpg</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                  <tr>
                                    <td>File submitted : Bank-statement-Jan-to-Mar-2020-2.jpg</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                  <tr>
                                    <td>File submitted : Bank-statement-Jan-to-Mar-2020-3.jpg</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                  <tr>
                                    <td>File submitted : Bank-statement-Jan-to-Mar-2020-4.jpg</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                  <tr>
                                    <td>File submitted : Bank-statement-Jan-to-Mar-2020-5.jpg</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                  <tr>
                                    <td>File submitted : Bank-statement-Jan-to-Mar-2020-6.jpg</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                  <tr>
                                    <td>File submitted : Bank-statement-Jan-to-Mar-2020-7.jpg</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                  <tr>
                                    <td>File submitted : Bank-statement-Jan-to-Mar-2020-8.jpg</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                  <tr>
                                    <td>File submitted : Bank-statement-Jan-to-Mar-2020-9.jpg</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                  <tr>
                                    <td>File submitted : Bank-statement-Jan-to-Mar-2020-10.jpg</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                  <tr>
                                    <td>File changed : Pending View</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                  <tr>
                                    <td>Renamed by : Sheikh Al-Zuhaib Siddiqui</td>
                                    <td>Jan, 20 at 03:15 PM</td>
                                  </tr>
                                </tbody>
                              </table>
                              </div>
                              
                            </section>
                            <footer className="vertical-tabs--footer">
                              <h2><span>Message</span></h2>
                              <h2>Hi Richard Glenn Randall,</h2>
                              <h2>Please Submit 2 months of the most recent Bank Statements</h2>
                            </footer>
                          </div>
                        </section>
                    </Dropdown.Menu>
                </Dropdown>

          <button className="btn btn-close" onClick={onClose}>
            <em className="zmdi zmdi-close"></em>
          </button>
        </div>
      </div>
    </div>
  );
};

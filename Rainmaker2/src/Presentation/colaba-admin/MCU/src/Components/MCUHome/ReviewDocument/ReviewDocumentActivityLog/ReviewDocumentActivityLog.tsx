import React, { useState, useEffect, useRef, useCallback } from 'react';
import { Http } from 'rainsoft-js';
import _ from 'lodash';
import ReactHtmlParser from 'react-html-parser';

import {
  ActivityLogType,
  LogType,
  EmailLogsType
} from '../../../../Entities/Types/Types';
import { ActivityLogFormat } from '../../../../Utils/helpers/DateFormat';
import { NeedListEndpoints } from '../../../../Store/endpoints/NeedListEndpoints';
import { ReviewDocumentActions } from '../../../../Store/actions/ReviewDocumentActions';
import { Tab, Tabs } from 'react-bootstrap';



export const ReviewDocumentActivityLog = ({
  id,
  requestId,
  docId
}: {
  doc?: boolean;
  id?: string | null;
  requestId?: string | null;
  docId?: string | null;
}) => {
  const [tab, setTab] = useState(1);
  const sectionRef = useRef<HTMLElement>(null);
  const allSections: any = sectionRef?.current?.children[0]?.children.length;
  const getWidthSection: any = sectionRef?.current?.offsetWidth;
  const totalWidth: any = allSections * getWidthSection;

  const [activityLogs, setActivityLogs] = useState<ActivityLogType[]>([]);
  const [emailLogs, setEmailLogs] = useState<EmailLogsType[]>([]);
  const [logIndex, setLogIndex] = useState(0);
  const [emailLogIndex, setEmailLogIndex] = useState(0);
  const [sortByEventName, setSortByEventName] = useState<boolean | null>(null);
  const [sortByEventDateTime, setSortByEventDateTime] = useState<
    boolean | null
  >(true);



  const checkActiveTab = (step: any) => {
    if (step == tab) {
      return 'active';
    }
  };

  const getTab = (step: any) => {
    setTab(step);
  };

  const switchTab = () => {
    return (tab - 1) * -(getWidthSection + 2);
  };

  const tabDataStyle: any = {
    transform: 'translateX(' + switchTab() + 'px)',
    width: totalWidth
  };

  const getActivityLogs = useCallback(async (id, docId, requestId) => {
    try {
      ///const {data} = await Http.get<ActivityLogType[]>(NeedListEndpoints.GET.documents.activityLogs(id, docId, requestId));
      const data = await ReviewDocumentActions.getActivityLogs(id, docId, requestId);
      setActivityLogs(data);
    } catch (error) {
      console.log(error);

      alert('Something went wrong while fetching logs. Please try again.');
    }
  }, []);

  const renderActivity = useCallback((logs: LogType[]) => {
    return logs.map((log, index) => {
      // Why split? because from BE we are getting \n for new line
      // For rename file activity we need to display filename at next line with <Br />
      const splitLogs = log.activity.split('\n');

      const isFileSubmittedLog = splitLogs[0].search('File submitted');

      let trimmedLog: string;

      if (isFileSubmittedLog !== -1) {
        trimmedLog =
          splitLogs[0].length > 38
            ? `${splitLogs[0].substring(0, 38)}...`
            : splitLogs[0];
      } else {
        trimmedLog = splitLogs[0];
      }

      return (
        <tr key={index}>
          <td title={isFileSubmittedLog !== -1 ? splitLogs[0] : splitLogs[1]}>
            {trimmedLog}
            {!!splitLogs[1] && <br />}
            {!!splitLogs && !!splitLogs[1] && splitLogs[1].length > 38
              ? `${splitLogs[1].substring(0, 38)}...`
              : splitLogs[1]}
          </td>
          <td>{ActivityLogFormat(log.dateTime)}</td>
        </tr>
      );
    });
  }, []);

  const renderActivityLog = useCallback(
    (activityLogs: ActivityLogType[]) => {
      return activityLogs.map((activityLog: ActivityLogType, index: number) => {
        return (
          <li data-testid="activity-log-list"
            className={`${index === logIndex && 'active'}`}
            key={activityLog.dateTime}
          >
            <a
              href="#"
              onClick={() => {
                setSortByEventDateTime(null);
                setSortByEventName(null);
                setLogIndex(index);
              }}
            >
              <div> {/* className="d-flex justify-content-between"*/}
                <h6>{activityLog.activity}</h6>
                <h2>{activityLog.userName}</h2>
                <time className="vertical-tabs--list-time">
                  {ActivityLogFormat(activityLog.dateTime)}
                </time>
              </div>

            </a>
          </li>
        );
      });
    },
    [logIndex]
  );

  const sortEventNames = () => {
    sortByEventDateTime !== null && setSortByEventDateTime(null);

    const clonedActivityLogs = _.cloneDeep(activityLogs);

    const currentLog = clonedActivityLogs[logIndex];

    const sortedActivityLogs = _.orderBy(
      currentLog.log,
      ['activity'],
      [sortByEventName === false || sortByEventName === null ? 'asc' : 'desc']
    );

    clonedActivityLogs[logIndex].log = sortedActivityLogs;

    setActivityLogs(() => clonedActivityLogs);
    setSortByEventName(() =>
      sortByEventName === false || sortByEventName === null ? true : false
    );
  };

  const sortEventDates = () => {
    sortByEventName !== null && setSortByEventName(() => null);

    const clonedActivityLogs = _.cloneDeep(activityLogs);

    const currentLog = clonedActivityLogs[logIndex];

    const sortedActivityLogs = _.orderBy(
      currentLog.log,
      ['dateTime'],
      [
        sortByEventDateTime === false || sortByEventDateTime === null
          ? 'asc'
          : 'desc'
      ]
    );

    clonedActivityLogs[logIndex].log = sortedActivityLogs;

    setActivityLogs(() => clonedActivityLogs);
    setSortByEventDateTime(() =>
      sortByEventDateTime === false || sortByEventDateTime === null
        ? true
        : false
    );
  };

  const getSortIconClassName = (sortState: boolean | null) => {
    if (sortState === null) {
      return '';
    } else if (sortState === false) {
      return 'zmdi zmdi-long-arrow-down table-th-arrow';
    } else if (sortState === true) {
      return 'zmdi zmdi-long-arrow-up table-th-arrow';
    }
  };

  const getEmailLogs = useCallback(async (id, docId, requestId) => {
    try {
      const { data } = await Http.get<EmailLogsType[]>(
        NeedListEndpoints.GET.documents.emailLogs(id, docId, requestId)
      );

      setEmailLogs(data);
    } catch (error) {
      console.log(error);

      alert('Something went wrong while fetching logs. Please try again.');
    }
  }, []);

  const renderEmailLogs = (emailLogs: EmailLogsType[]) => {
    const style = {
      marginTop: 0
    };
    return emailLogs.map((emailLog, index) => (
      <li className={index === emailLogIndex ? 'active' : ''} key={index}>
        <a href="javascript:void" onClick={() => setEmailLogIndex(index)}>
          <div className="d-flex justify-content-between">
            {/* <h6>
              {emailLog.message ? `${emailLog.message}:` : 'Requested by:'}
            </h6> */}
            <h2 style={style}>{emailLog.userName}</h2>
            <time className="vertical-tabs--list-time">
              {ActivityLogFormat(emailLog.dateTime)}
            </time>
          </div>
        </a>
      </li>
    ));
  };

  const renderEmailLogDetails = (emailLogIndex: number) => {
    
    //setFooter(false);
    const emailLog = emailLogs[emailLogIndex];

    return <div id="emailContent" >{ReactHtmlParser(emailLog?.emailText)}</div>;
  };

  useEffect(() => {
    if (id === null || docId === null || requestId === null) return;

    getActivityLogs(id, requestId, docId);
  }, [getActivityLogs, id, docId, requestId]);

  useEffect(() => {
    if (id === null || docId === null || requestId === null) return;
    getEmailLogs(id, requestId, docId);
  }, [getEmailLogs, id, docId, requestId]);

  const [tabKey , setTabKey] = useState<string>('ActivityLog');
  const checkTab = (eventKey:any) => {
    setTabKey(eventKey);    
  }
  return (
    <section data-testid="logDetail-section" ref={sectionRef} className="vertical-tabs" id="verticalTab">
      <div className="vertical-tabs--data" style={tabDataStyle}>
        {/* Activity Log */}
        <div
          className={'vertical-tabs--wrap activity-log ' + checkActiveTab(1)}
          data-step="1"
          style={{ width: `${getWidthSection}px` }}
        >
          <div className="vertical-tabs--aside">
            <header className="vertical-tabs--header">
              <h2 className="vertical-tabs--header-title">Requests</h2>
            </header>
            <section className="vertical-tabs--body">
              <ul className="vertical-tabs--list">
                {activityLogs.length > 0 && renderActivityLog(activityLogs)}
              </ul>
            </section>
          </div>

          <div className="vertical-tabs--content">
            <header className="vertical-tabs--header flex">
              <div className="vertical-tabs--header-left">
                <h2 className="vertical-tabs--header-title">Log Details</h2>
              </div>
              <div className="vertical-tabs--header-right" style={{ display: 'none' }}>
                <button
                  className="btn-go"
                  onClick={() => {
                    getTab(2);
                  }}
                >
                  View Email Log
                  {/* <em className="zmdi zmdi-arrow-right"></em> */}
                </button>
              </div>
            </header>
            <section className="vertical-tabs--body padding">
              <div className={`horizontal-tabs ` + tabKey}>
                <Tabs defaultActiveKey={tabKey} id="activityLogsTabs" onSelect={(eventKey:any)=>checkTab(eventKey)}>
                  <Tab eventKey="ActivityLog" title="Activity Log" >
                    <table className="table table-noborder">
                      <colgroup>
                        <col style={{ width: '75%' }} />
                        <col style={{ width: '25%' }} />
                      </colgroup>
                      <thead>
                        <tr>
                          <th>
                            <button onClick={sortEventNames}>
                              Events
                          <em
                                className={getSortIconClassName(sortByEventName)}
                              ></em>
                            </button>
                          </th>
                          <th>
                            <button onClick={sortEventDates}>
                              Date & Time
                          <em
                                className={getSortIconClassName(sortByEventDateTime)}
                              ></em>
                            </button>
                          </th>
                        </tr>
                      </thead>
                    </table>
                    <div className="vertical-tabs--scrollable">
                      <table className="table">
                        <colgroup>
                          <col style={{ width: '75%' }} />
                          <col style={{ width: '25%' }} />
                        </colgroup>
                        <tbody>
                          {activityLogs.length > 0 &&
                            renderActivity(activityLogs[logIndex].log)}
                        </tbody>
                      </table>
                    </div>
                  </Tab>
                  <Tab eventKey="EmailSent" title="Email Sent">
                    {!!emailLogs.length && renderEmailLogDetails(emailLogIndex)}
                  </Tab>
                </Tabs>
              </div>
            </section>
            {tabKey == 'ActivityLog' &&
              //footer &&
              <footer className="vertical-tabs--footer">
              <h2>
                <span>Message to borrower</span>
              </h2>
              {activityLogs.length > 0 &&
                <div>{ReactHtmlParser(activityLogs[logIndex].message)}</div>
              }
            </footer>
            }
     
          </div>
        </div>

        {/* Email Log */}
        {/* <div
          className={'vertical-tabs--wrap email-log ' + checkActiveTab(2)}
          data-step="2"
          style={{ width: `${sectionRef?.current?.offsetWidth}px` }}
        >
          <div className="vertical-tabs--aside">
            <header className="vertical-tabs--header">
              <h2 className="vertical-tabs--header-title">
                <button
                  className="btn-go"
                  onClick={() => {
                    getTab(1);
                  }}
                >
                  <em className="zmdi zmdi-arrow-left"></em> Back
                </button>
              </h2>
            </header>
            <section className="vertical-tabs--body">
              <ul className="vertical-tabs--list">
                {!!emailLogs.length && renderEmailLogs(emailLogs)}
              </ul>
            </section>
          </div>

          <div className="vertical-tabs--content">
            <header className="vertical-tabs--header flex">
              <div className="vertical-tabs--header-left">
                <h2 className="vertical-tabs--header-title">Email Log</h2>
              </div>
              <div className="vertical-tabs--header-right">&nbsp;</div>
            </header>
            <section className="vertical-tabs--body padding">
              {!!emailLogs.length && renderEmailLogDetails(emailLogIndex)}
            </section>
          </div>
        </div> */}
      </div>
    </section>
  );
};

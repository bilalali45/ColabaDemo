import React, {useState, useEffect, useContext} from 'react';
import {SVGopenLock} from '../../../../Shared/SVG';
import {LoanApplication} from '../../../../Entities/Models/LoanApplication';
import {NeedListActions} from '../../../../Store/actions/NeedListActions';
import Spinner from 'react-bootstrap/Spinner';
import {LocalDB} from '../../../../Utils/LocalDB';
import {Store} from '../../../../Store/Store';
import {stat} from 'fs';
import {NeedListActionsType} from '../../../../Store/reducers/NeedListReducer';

export const LoanSnapshot = () => {
  const [loanInfo, setLoanInfo] = useState<LoanApplication | null>();
  const {state, dispatch} = useContext(Store);

  const needListManager: any = state?.needListManager;
  const loanData = needListManager?.loanInfo;

  useEffect(() => {
    window.addEventListener('TestSignalR', (data: any) => {
      alert(data.detail.name);
    });
    if (!loanData) {
      fetchLoanApplicationDetail();
    }

    if (loanData) {
      setLoanInfo(new LoanApplication().fromJson(loanData));
    }
  }, [loanData]);

  const fetchLoanApplicationDetail = async () => {
    let applicationId = LocalDB.getLoanAppliationId();
    if (applicationId) {
      let res:
        | LoanApplication
        | undefined = await NeedListActions.getLoanApplicationDetail(
        applicationId
      );
      if (res) {
        dispatch({type: NeedListActionsType.SetLoanInfo, payload: res});
        // setLoanInfo(res)
      }
    }
  };

  const renderLoanProgram = (data: string | undefined) => {
    if (data) {
      let splitData = data.split(' ');
      return (
        <span className="mcu-label-value plus">
          {splitData[0]}
          <span className="text-wrap top inline-block-element">
            <small className="block-element">{splitData[1]}</small>
            <small className="text-primary block-element">{splitData[2]}</small>
          </span>
        </span>
      );
    }
  };

  if (!loanInfo) {
    return (
      <div className="loader-widget loansnapshot">
        <Spinner animation="border" role="status">
          <span className="sr-only">Loading...</span>
        </Spinner>
      </div>
    );
  }
  const formattedAddress = () => {
    return `${loanInfo.streetAddress || ''}   ${
      loanInfo.unitNumber ? ' # ' + loanInfo.unitNumber : ''
    }`;
  };

  return (
    <div className="loansnapshot">
      <div className="loansnapshot--left-side">
        <div className="loansnapshot--wrap">
          <ul>
            <li>
              <label className="mcu-label">Byte Loan No</label>
              <span className="mcu-label-value">{loanInfo.loanNumber}</span>
            </li>
            <li>
              <label className="mcu-label">Primary & co-Borrower</label>
              <span className="mcu-label-value">{loanInfo.borrowersName}</span>
            </li>
            <li>
              <label className="mcu-label">Est. Closing Date</label>
              <span className="mcu-label-value">
                {loanInfo.expectedClosingDate}
              </span>
            </li>
            <li>
              <label className="mcu-label">Loan Purpose</label>
              <span className="mcu-label-value">{loanInfo.loanPurpose}</span>
            </li>
            <li>
              <label className="mcu-label">Property Value</label>
              {loanInfo.getPropertyValue && (
                <span
                  className="mcu-label-value plus"
                  data-testid="propertyVal"
                >
                  <sup className="text-primary">$</sup>
                  {loanInfo.getPropertyValue}
                </span>
              )}
            </li>
            <li>
              <label className="mcu-label">Loan Amount</label>
              {loanInfo.getLoanAmount && (
                <span className="mcu-label-value plus" data-testId="loanAmt">
                  <sup className="text-primary">$</sup>
                  {loanInfo.getLoanAmount}
                </span>
              )}
            </li>
            <li>
              <label className="mcu-label">Milestone/Status</label>
              <span className="mcu-label-value">{loanInfo.status}</span>
            </li>
            <li>
              <label className="mcu-label">Property type</label>
              <span className="mcu-label-value">{loanInfo.propertyType}</span>
            </li>
            <li>
              <label className="mcu-label">Rate</label>
              {loanInfo.rate && (
                <span className="mcu-label-value plus">
                  {loanInfo.rate}
                  <sup className="text-primary">%</sup>
                </span>
              )}
            </li>
            <li>
              <label className="mcu-label">Loan Program</label>
              {renderLoanProgram(loanInfo.loanProgram)}
            </li>
            <li>
              <label className="mcu-label">Property Address</label>
              <span className="mcu-label-value">{}</span>
              <p className="LoanStatus--text ">
                <span className="mcu-label-value" title={formattedAddress()}>
                  {' '}
                  {formattedAddress()}{' '}
                </span>
                <span className="mcu-label-value">
                  {' '}
                  {loanInfo.cityName},{' '}
                  {loanInfo.stateName + ' ' + loanInfo.zipCode}{' '}
                </span>{' '}
              </p>
            </li>
          </ul>
        </div>
      </div>

      <div className="loansnapshot--right-side">
        <div className="loansnapshot--wrap">
          <div className="form-group lock-status">
            <label className="mcu-label">Lock status</label>
            <SVGopenLock />
          </div>

          <div className="form-group row">
            <div className="col-md-6">
              <label className="mcu-label">Lock Date</label>
              <span className="mcu-label-value">{loanInfo.lockDate}</span>
            </div>
            <div className="col-md-6">
              <label className="mcu-label">Expiration date</label>
              <span className="mcu-label-value">{loanInfo.expirationDate}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

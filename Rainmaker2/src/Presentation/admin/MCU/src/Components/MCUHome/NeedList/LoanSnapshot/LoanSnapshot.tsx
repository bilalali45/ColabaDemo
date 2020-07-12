import React, { useState, useEffect } from 'react';
import {SVGopenLock} from '../../../../Shared/SVG';
import { LoanApplication } from '../../../../Entities/Models/LoanApplication';
import { LoanAction } from '../../../../Store/actions/LoanAction';



export const LoanSnapshot = () => {
  
  const [loanInfo, setLoanInfo] = useState<LoanApplication | null>();

  useEffect(()=>{
      if(!loanInfo){
        fetchLoanApplicationDetail()
      }
  },[loanInfo])

  const fetchLoanApplicationDetail = async ()=> {
   let res: LoanApplication | undefined = await LoanAction.getLoanApplicationDetail('5976')
   if(res){
    setLoanInfo(res)
   }
   console.log('res',res)
  }
  console.log('loaninfo',loanInfo)
  if (!loanInfo) {
    return <></>;
}
  const formattedAddress = () => {
    return `${loanInfo.streetAddress || ''}   ${ loanInfo.unitNumber ? ' # ' + loanInfo.unitNumber : '' }`
}
  
    return (
        <div className="loansnapshot">
            <div className="loansnapshot--left-side">
                <div className="loansnapshot--wrap">
                    <ul>
                        <li>
                            <label className="mcu-label">Loan No.</label>
                            <span className="mcu-label-value">{loanInfo.loanNumber}</span>
                        </li>
                        <li>
                            <label className="mcu-label">Property Address</label>
                            <span className="mcu-label-value">{}</span>
                            <p className="LoanStatus--text ">
                            <span className="mcu-label-value" title={formattedAddress()}> {formattedAddress()} </span>
                            <span className="mcu-label-value"> {loanInfo.cityName}, {loanInfo.stateName + ' ' + loanInfo.zipCode} </span> </p>
                        </li>
                        <li>
                            <label className="mcu-label">Est. Closing Date</label>
                          <span className="mcu-label-value">{loanInfo.expectedClosingDate}</span>
                        </li>
                        <li>
                            <label className="mcu-label">Loan Purpose</label>
    <span className="mcu-label-value">{loanInfo.loanPurpose}</span>
                        </li>
                        <li>
                            <label className="mcu-label">Property Value</label>
    <span className="mcu-label-value plus"><sup className="text-primary">$</sup>{loanInfo.getPropertyValue}</span>
                        </li>
                        <li>
                            <label className="mcu-label">Loan Amount</label>
    <span className="mcu-label-value plus"><sup className="text-primary">$</sup>{loanInfo.getLoanAmount}</span>
                        </li>
                        <li>
                            <label className="mcu-label">Primary & co-Borrower</label>
    <span className="mcu-label-value">{loanInfo.borrowersName}</span>
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
                            <span className="mcu-label-value plus">{loanInfo.rate}<sup className="text-primary">%</sup></span>
                        </li>
                        <li>
                            <label className="mcu-label">Loan Program</label>
                            <span className="mcu-label-value plus">{loanInfo.loanProgram}
                                {/* <span className="text-wrap top inline-block-element">
                                    <small className="block-element">Year</small>
                                    <small className="text-primary block-element">ARM</small>
                                </span>  */}
                            </span>
                        </li>
                    </ul>
                </div>                    
            </div>
            <div className="loansnapshot--right-side">
                <div className="loansnapshot--wrap">
                    <div className="loansnapshot--right-side---data">
                        <div className="form-group">
                            <label className="mcu-label">Lock status</label>
                        </div>
                        <div className="form-group">
                            <label className="mcu-label">Lock Date</label>
                            <span className="mcu-label-value">{loanInfo.lockDate}</span>
                        </div>
                        <div className="form-group">
                            <label className="mcu-label">Expiration date</label>
                            <span className="mcu-label-value">{loanInfo.expirationDate}</span>
                        </div>
                    </div>
                    <div className="loansnapshot--right-side---icon">
                        <SVGopenLock />
                    </div>                
                </div>
            </div>
        </div>
    )
}

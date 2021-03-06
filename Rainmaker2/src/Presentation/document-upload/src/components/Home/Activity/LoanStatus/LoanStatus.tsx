import React, { useState, useEffect, useContext } from 'react'
import { LaonActions } from '../../../../store/actions/LoanActions';
import { Store } from '../../../../store/store';
import { LoanActionsType } from '../../../../store/reducers/loanReducer';
import { LoanApplication } from '../../../../entities/Models/LoanApplication';
//import loader from '../../../../assets/images/loader.svg';
import icon1 from '../../../../assets/images/property-address-icon.svg';
import icon2 from '../../../../assets/images/property-type-icon.svg';
import icon3 from '../../../../assets/images/loan-purpose-icon.svg';
import icon4 from '../../../../assets/images/loan-amount-icon.svg';
import { Auth } from '../../../../services/auth/Auth';
import { Loader } from '../../../../shared/Components/Assets/loader';


export const LoanStatus = () => {

    const [loanInfo, setLoanInfo] = useState<LoanApplication>()
    const { state, dispatch } = useContext(Store);

    const loan: any = state.loan;
    const {isMobile} = loan;
    let info = loan.loanInfo;
    useEffect(() => {
        if (!info) {
            fetchLoanStatus();
        }

        if (info) {
            setLoanInfo(new LoanApplication().fromJson(info));
        }
    }, [info])

    const fetchLoanStatus = async () => {
        let loanInfoRes: LoanApplication | undefined = await LaonActions.getLoanApplication(Auth.getLoanAppliationId());
        if (loanInfoRes) {
            dispatch({ type: LoanActionsType.FetchLoanInfo, payload: loanInfoRes });
            // setLoanInfo(loanInfoRes);
        }
    }

    if (!loanInfo) {
        return <Loader containerHeight={"80px"} />
    }

    const formattedAddress = () => {
        return `${loanInfo.streetAddress || ''}   ${ loanInfo.unitNumber ? ' # ' + loanInfo.unitNumber : '' }`
    }

    const LoanStatusDesktop = () => {
        return (
            <section className="row" data-testid="loanStatus">
            <div className="container">
                <div className="LoanStatus nbox-wrap">
                    <div className="nbox-wrap--body">
                        <ul className="row ls-wrap" >
                            <li className="col-sm-3- ls-box ls-box-add" data-testid="prop-address">
                                <div className="i-wrap">
                                    <div className="icon-wrap">
                                        <img src={icon1} alt="" />
                                    </div>
                                    <div className="c-wrap">
                                        <h4 className="LoanStatus--heading">Property Address</h4>
    
                                        <p className="LoanStatus--text " data-private>
                                            <span className="add-txt" title={formattedAddress()}> {formattedAddress()} </span>
                                            {loanInfo.cityName}, {loanInfo.stateName + ' ' + loanInfo.zipCode} </p>
    
                                    </div>
                                </div>
                            </li>
                            <li className="col-sm-3- ls-box ls-box-p-type" data-testid="prop-type">
                                <div className="i-wrap">
                                    <div className="icon-wrap">
                                        <img src={icon2} alt="" />
                                    </div>
                                    <div className="c-wrap">
                                        <h4 className="LoanStatus--heading">Property Type</h4>
                                        <p className="LoanStatus--text" data-private>
                                            {loanInfo.propertyType}
                                        </p>
                                    </div>
    
    
                                </div>
                            </li>
                            <li className="col-sm-3- ls-box ls-box-l-p" data-testid="loan-purpose">
                                <div className="i-wrap">
                                    <div className="icon-wrap">
                                        <img src={icon3} alt="" />
                                    </div>
                                    <div className="c-wrap">
                                        <h4 className="LoanStatus--heading">Loan Purpose</h4>
                                        <p className="LoanStatus--text">
                                            {loanInfo.loanPurpose}
                                        </p>
                                    </div>
    
    
                                </div>
                            </li>
                            <li className="col-sm-3- ls-box ls-box-l-a" data-testid="loan-amt">
                                <div className="i-wrap">
                                    <div className="icon-wrap">
                                        <img src={icon4} alt="" />
                                    </div>
                                    <div className="c-wrap">
                                        <h4 className="LoanStatus--heading">Loan Amount</h4>
                                        {loanInfo.amount &&
                                            <p className="LoanStatus--text">
                                                <span className="number-loanAmount">
                                                    <sup>$</sup>
                                                    <span>{loanInfo.amount}</span>
                                                </span>
                                            </p>
                                        }
                                    </div>
    
    
                                </div>
                            </li>
    
                        </ul>
                    </div>
                </div>
            </div>
        </section>
        )
    }

    const LoanStatusMobile = () => {
        return (
            <section className="row-loanStatus" data-testid="loan-status-mobile">
                <div className="container">
                <div className="LoanStatus LoanStatus-mobile nbox-wrap">
                    <div className="nbox-wrap--body">
                        <div className="row LoanStatus-mobile-row">
                            <div className="col-9 LoanStatus-mobile-left">
                            <ul className="row ls-wrap">
                            <li className="col-12 ls-mobile-box">
                                <div className="i-wrap">
                                    <div className="icon-wrap">
                                        <img src={icon1} alt="" />
                                    </div>
                                    <div className="c-wrap">
                                        <h4 className="LoanStatus--heading">Property Address</h4>
    
                                        <p className="LoanStatus--text " data-private>
                                            <span className="add-txt" title={formattedAddress()}> {formattedAddress()} </span>
                                            {loanInfo.cityName}, {loanInfo.stateName + ' ' + loanInfo.zipCode} </p>
    
                                    </div>
                                </div>
                            </li>

                            <li className="col-12 ls-mobile-box ">
                                <div className="i-wrap">
                                    <div className="icon-wrap lp-icon-wrap">
                                        <img src={icon3} alt="" />
                                    </div>
                                    <div className="c-wrap">
                                        <h4 className="LoanStatus--heading">Loan Purpose</h4>
                                        <p className="LoanStatus--text">
                                            {loanInfo.loanPurpose}
                                        </p>
                                    </div>
    
    
                                </div>
                            </li>
                        </ul>
                            </div>
                            <div className="col-3 LoanStatus-mobile-right">
                            <div className="ls-wrap lp-mobile-sect">
                            <div className="ls-mobile-box">
                                <div className="i-wrap lp-i-wrap">
                                    <div className="icon-wrap">
                                        <img src={icon4} alt="" />
                                    </div>
                                    <div className="c-wrap">
                                        <h4 className="LoanStatus--heading">Loan Amount</h4>
                                        {loanInfo.amount &&
                                            <p className="LoanStatus--text">
                                                <span className="number-loanAmount">
                                                    <sup>$</sup>
                                                    <span>{loanInfo.amount}</span>
                                                </span>
                                            </p>
                                        }
                                    </div>
    
    
                                </div>
                            </div>
    
                        </div>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
        )
    }


    if(isMobile?.value) {
        return (
            LoanStatusMobile()
        )
    }

    return (
        LoanStatusDesktop()
    )

    
}









import React, {useState} from 'react'
import icon1 from '../../../../assets/images/property-address-icon.svg';
import icon2 from '../../../../assets/images/property-type-icon.svg';
import icon3 from '../../../../assets/images/loan-purpose-icon.svg';
import icon4 from '../../../../assets/images/loan-amount-icon.svg';



 export const LoanStatus = () => {
    return (
        <section className="row">
    <div className="container">
        <div className="LoanStatus nbox-wrap">
            <div className="nbox-wrap--body">
                <ul className="row ls-wrap">
                    <li className="col-sm-3 ls-box">
                        <div className="i-wrap">
                            <div className="icon-wrap">
                            <img src={icon1} alt="" />
                            </div>
                                <div className="c-wrap">
                                <h4 className="LoanStatus--heading">Property Address</h4>
                                <p className="LoanStatus--text">
                                    727 Ashleigh LN # 222
                                    Dallas, TX 76099</p>
                                </div>
                            
                            
                        </div>
                    </li>
                    <li className="col-sm-3 ls-box">
                        <div className="i-wrap">
                            <div className="icon-wrap">
                            <img src={icon2} alt="" />
                            </div>
                                <div className="c-wrap">
                                <h4 className="LoanStatus--heading">Property Type</h4>
                                <p className="LoanStatus--text">
                                Single Family 
                                Detached
                                </p>
                                </div>
                            
                            
                        </div>
                    </li>
                    <li className="col-sm-3 ls-box">
                        <div className="i-wrap">
                            <div className="icon-wrap">
                            <img src={icon3} alt="" />
                            </div>
                                <div className="c-wrap">
                                <h4 className="LoanStatus--heading">Loan Purpose</h4>
                                <p className="LoanStatus--text">
                                Purchase a <br />
                                home   
                                </p>
                                </div>
                            
                            
                        </div>
                    </li>
                    <li className="col-sm-3 ls-box">
                        <div className="i-wrap">
                            <div className="icon-wrap">
                            <img src={icon4} alt="" />
                            </div>
                                <div className="c-wrap">
                                <h4 className="LoanStatus--heading">Loan Amount</h4>
                                <p className="LoanStatus--text">
                                <div className="number-loanAmount">
                                   <sup>$</sup>
                                   <span>40,000</span> 
                               </div>
                                    </p>
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

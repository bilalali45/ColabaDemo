import React, {useState} from 'react'
import icon1 from '../../../../assets/images/property-address-icon.svg';

 export const LoanStatus = () => {
    return (
        <section className="row">
    <div className="container">
        <div className="LoanStatus box-wrap">
            <div className="box-wrap--body">
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
                    <li className="col-sm-3">
                        <div className="flex">
                            <div className="flex--side">
                                <h4 className="LoanStatus--heading">Property Type</h4>
                                <p className="LoanStatus--text">Single Family Detached</p>
                            </div>
                        </div>
                    </li>
                    <li className="col-sm-3">
                        <h5 className="LoanStatus--heading">Property Address</h5>
                        <p className="LoanStatus--text">7951 Northeast Bayshore Court, Miami, FL, USA</p>
                    </li>
                    <li className="col-sm-3">
                        <h5 className="LoanStatus--heading">Loan Amount</h5>
                        <p className="LoanStatus--text">$40,000</p>
                    </li>
                </ul>
            </div>
        </div>
        </div>
        </section>
    )
}

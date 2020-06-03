import React from 'react'
//import arrowForward from './../../../../assets/images/arrow-forward.svg';
import {SVG} from './../../../../shared/Components/Assets/SVG';

export const DocumentStatus = () => {
    return (
        <div className="DocumentStatus box-wrap">
            <div className="row">
                <div className="col-md-7 DocumentStatus--left-side">
                    <div className="DocumentStatus--header">
                        <h2 className="heading-h2">Document Request</h2>
                        <p>You have <span className="DocumentStatus--count">8</span> items to complete</p>
                    </div>
                    <div className="DocumentStatus--body">
                        <ul className="list">
                            <li>Bank Statement</li>
                            <li>W-2s 2017</li>
                            <li>W-2s 2018</li>
                            <li>Personal Tax Returns</li>
                        </ul>
                        <div>
                            <a href="" className="DocumentStatus--get-link">Show 4 more Tasks <SVG  shape="arrowFarword"/></a>
                        </div>
                    </div>
                </div>
                <div className="col-md-5 DocumentStatus--right-side">
                    <SVG shape="storage"/>
                    <button className="btn btn-primary float-right">Get Started</button>
                </div>
            </div>
        </div>
    )
}

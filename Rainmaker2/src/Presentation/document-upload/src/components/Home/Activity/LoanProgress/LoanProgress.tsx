import React from 'react';
import {SVGchecked} from './../../../../shared/Components/Assets/SVG';

type Props = {
    //userName: string,
}

export const LoanProgress: React.SFC<Props> = (props) => {
    return (
        <div className="LoanProgress box-wrap">
            <div className="box-wrap--header">
                <h2 className="heading-h2"> Your Loan Progress </h2>
            </div>
            <div className="box-wrap--body">

                <div className="progress-list">
                    <ol>
                        <li data-status="completed">
                            <span className="progress-list--icon"><SVGchecked /></span>
                            <span className="progress-list--status">Completed</span>
                            <h4 className="heading-h4">Fill out loan application</h4>  
                            <p>tell us about yourself and you financial situation so we can find loan options for you</p>  
                        </li>
                        <li data-status="completed">
                            <span className="progress-list--icon"><SVGchecked /></span>
                            <span className="progress-list--status">Completed</span>
                            <h4 className="heading-h4">Review and submit application</h4>  
                            <p>Double-check the information you have entered and
make any edits before you submit your documents </p>  
                        </li>
                        <li data-status="completed">
                            <span className="progress-list--icon"><SVGchecked /></span>
                            <span className="progress-list--status">Completed</span>
                            <h4 className="heading-h4">Loan team review</h4>  
                            <p>Our loan team reviewing your application and will contact you soon</p>  
                        </li>
                        <li data-status="current">
                            <span className="progress-list--icon">4</span>
                            <span className="progress-list--status">Current Step</span>
                            <h4 className="heading-h4">Document upload and loan team review</h4>  
                            <p>Submit document to help us verify the information you provided. We may request follow-up items and we review your application</p>  
                        </li>
                        <li data-status="upcomming">
                            <span className="progress-list--icon"><SVGchecked /></span>
                            <span className="progress-list--status">Upcomming</span>
                            <h4 className="heading-h4">Submit and underwriting</h4>  
                            <p>tell us about yourself and you financial situation so we can find loan options for you</p>  
                        </li>
                    </ol>
                </div>

            </div>
        </div>
    )
}

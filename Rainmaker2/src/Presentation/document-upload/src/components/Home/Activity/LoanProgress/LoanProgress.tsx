import React, { useEffect, useState } from 'react';
import { SVGchecked } from './../../../../shared/Components/Assets/SVG';
import Carousel from 'react-bootstrap/Carousel'
type Props = {
    //userName: string,
}

export const LoanProgress: React.SFC<Props> = (props) => {
    const [index, setIndex] = useState(2);

    function handleSelect(selectedIndex: any, e: any) {
        setIndex(selectedIndex);
    };
    return (
        <div className="LoanProgress box-wrap">
            <div className="box-wrap--header">
                <h2 className="heading-h2"> Your Loan Progress </h2>
            </div>
            <div className="box-wrap--body">
                <div className={index == 4 ||index == 3  ? "lp-wrap upcoming-step" : index == 2 ? "lp-wrap current-step" : "lp-wrap"}>
                    <div className="list-wrap">
                        <Carousel as="div"
                            activeIndex={index}
                            onSelect={handleSelect}
                            touch={true}
                            controls={false}
                            indicators={false}
                            wrap={false}
                            interval={null}
                            nextIcon={<i aria-hidden="true" className="zmdi zmdi-chevron-right"></i>}
                            nextLabel=""
                            prevIcon={<i aria-hidden="true" className="zmdi zmdi-chevron-left"></i>}
                            prevLabel=""
                        >
                            <Carousel.Item>
                                <div className="lp-list">
                                    <div className="step-count">1</div>
                                    <div className="lp-content">
                                        <div className="step-label">Completed</div>
                                        <h6>Fill out application</h6>
                                        <p>Tell us about yourself and your financial situation so we can find loan options for you.</p>
                                    </div>
                                </div>

                            </Carousel.Item>
                            <Carousel.Item>
                                <div className="lp-list">
                                    <div className="step-count">2</div>
                                    <div className="lp-content">
                                        <div className="step-label">Completed</div>
                                        <h6>Review and submit application</h6>
                                        <p>Double-check the information you’ve entered and make any edits before you submit your applications.</p>
                                    </div>
                                </div>

                            </Carousel.Item>
                            <Carousel.Item>
                                <div className="lp-list">
                                    <div className="step-count">3</div>
                                    <div className="lp-content">
                                        <div className="step-label">Completed</div>
                                        <h6>Loan Team Review</h6>
                                        <p>Our Loan team reviewing your application and will contact you soon.</p>
                                    </div>
                                </div>
                            </Carousel.Item>
                            <Carousel.Item>
                                <div className="lp-list ">
                                    <div className="step-count">4</div>
                                    <div className="lp-content">
                                        <div className="step-label">Upcoming</div>
                                        <h6>Document upload and loan team review</h6>
                                        <p>Submit document to help us verify the information you provided. We many request follow-up items add we review your application.</p>
                                    </div>
                                </div>
                            </Carousel.Item>
                            <Carousel.Item >
                                <div className="lp-list">
                                    <div className="step-count">5</div>
                                    <div className="lp-content">
                                        <div className="step-label">Upcoming</div>
                                        <h6>Submitted to Underwriting</h6>
                                        <p>Our underwriter may as additional information as they review your application. To keep your loan.</p>
                                    </div>
                                </div>
                            </Carousel.Item>
                        </Carousel>



                    </div>
                    <div className="lp-footer">
                        <ul>
                            <li className={index==0?"completed-icon active":"completed-icon"}>
                                <a href="javascrit:" onClick={(e) => handleSelect(0, e)}>
                                    <i className="zmdi zmdi-check"></i>
                                </a>
                            </li>

                            <li className={index==1?"completed-icon active":"completed-icon"}>
                                <a href="javascrit:" onClick={(e) => handleSelect(1, e)}>
                                    <i className="zmdi zmdi-check"></i>
                                </a>
                            </li>

                         
                            <li className={index==2?"current-icon active":"current-icon"}>
                                <a href="javascrit:" onClick={(e) => handleSelect(2, e)}>
                                    <i className="zmdi zmdi-male-alt"></i>
                                </a>
                            </li>
                            <li className={index==3?"upcoming-icon active":"upcoming-icon"}>
                                <a href="javascrit:" onClick={(e) => handleSelect(3, e)}>
                                    <span>4</span>
                                </a>
                            </li>
                            <li className={index==4?"upcoming-icon active":"upcoming-icon"}>
                                <a href="javascrit:" onClick={(e) => handleSelect(4, e)}>
                                    <i className="zmdi zmdi-flag"></i>
                                </a>
                            </li>


                        </ul>
                    </div>
                </div>
            </div>
        </div>
    )
}

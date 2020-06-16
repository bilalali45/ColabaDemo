import React, { useEffect, useState } from 'react';
import { SVGchecked } from './../../../../shared/Components/Assets/SVG';
import Carousel from 'react-bootstrap/Carousel'
import Lpstep1 from '../../../../assets/images/lp-step1.svg';
import Lpstep2 from '../../../../assets/images/lp-step2.svg';
import Lpstep3 from '../../../../assets/images/lp-step3.svg';
import Lpstep4 from '../../../../assets/images/lp-step4.svg';
import Lpstep5 from '../../../../assets/images/lp-step5.svg';
import { DocumentActions } from '../../../../store/actions/DocumentActions';
type Props = {
    //userName: string,
}

export const LoanProgress: React.SFC<Props> = (props) => {
    const [index, setIndex] = useState(2);
    const [loanProgress, setLoanProgress] = useState([]);

    useEffect(() => {
        if (!loanProgress.length) {
            fetchLoanProgress();
        }
    });

    const fetchLoanProgress = async () => {
        let applicationId = localStorage.getItem('loanApplicationId');
        let tenantId = localStorage.getItem('tenantId');
        let loanProgress = await DocumentActions.getDocumentsStatus(applicationId ? applicationId : '1', tenantId ? tenantId : '1')
        if (loanProgress) {
            console.log('loanProgress', loanProgress)
            setLoanProgress(loanProgress);
        }
    }

    function handleSelect(selectedIndex: any, e: any) {
        setIndex(selectedIndex);
    };

    const renderCarousel = () => {
        return (
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
                {
                    renderCarouselItems()
                }
            </Carousel>
        )
    }

    const renderCarouselItems = () => {

        return loanProgress.map((l: any) => {

            return (
                <Carousel.Item>
                    <div className="lp-list">
                        <div className="step-count">{l.order}</div>
                        <div className="lp-content">
                            <div className="step-label">{'Completed'}</div>
                            <h6>{l.name}</h6>
                            <p>{l.description}</p>
                        </div>
                    </div>

                </Carousel.Item>
            )
        })
    }

    return (
        <div className="LoanProgress box-wrap">
            <div className="box-wrap--header">
                <h2 className="heading-h2"> Your Loan Progress </h2>
            </div>
            <div className="box-wrap--body">
                <div className={index == 4 || index == 3 ? "lp-wrap upcoming-step" : index == 2 ? "lp-wrap current-step" : "lp-wrap"}>
                    <div className="list-wrap">
                        {
                            renderCarousel()
                        }
                    </div>
                    <div className="lp-footer">
                        <ul>
                            <li className={index == 0 ? "completed-icon active" : "completed-icon"}>
                                <a href="javascrit:" onClick={(e) => handleSelect(0, e)}>
                                    <i className="zmdi zmdi-check"></i>
                                </a>
                            </li>

                            <li className={index == 1 ? "completed-icon active" : "completed-icon"}>
                                <a href="javascrit:" onClick={(e) => handleSelect(1, e)}>
                                    <i className="zmdi zmdi-check"></i>
                                </a>
                            </li>


                            <li className={index == 2 ? "current-icon active" : "current-icon"}>
                                <a href="javascrit:" onClick={(e) => handleSelect(2, e)}>
                                    <i className="zmdi zmdi-male-alt"></i>
                                </a>
                            </li>
                            <li className={index == 3 ? "upcoming-icon active" : "upcoming-icon"}>
                                <a href="javascrit:" onClick={(e) => handleSelect(3, e)}>
                                    <span>4</span>
                                </a>
                            </li>
                            <li className={index == 4 ? "upcoming-icon active" : "upcoming-icon"}>
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

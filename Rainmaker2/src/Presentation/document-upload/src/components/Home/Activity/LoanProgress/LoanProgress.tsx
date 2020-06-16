import React, { useEffect, useState } from 'react';
import { SVGchecked } from './../../../../shared/Components/Assets/SVG';
import Carousel from 'react-bootstrap/Carousel'
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
        return loanProgress.map((l: any, i: number) => {
            console.log(l)
            return (
                <Carousel.Item>
                    <div className="lp-list">
                        <div className="step-count">{l.order}</div>
                        <div className="lp-content">
                            <div className="step-label">{l.status}</div>
                            <h6>{l.name}</h6>
                            <p>{l.description}</p>
                        </div>
                    </div>

                </Carousel.Item>
            )
        })
    }

    const renderCarouselList = () => {
        var totallist = loanProgress.length;
        console.log(loanProgress)
        return loanProgress.map((l: any, i: number) => {
            let liclass = "completed-icon";
            liclass = l.status == 'In progress' ? 'current-icon' : l.status == 'To be done' ? 'upcoming-icon' : 'completed-icon';
            let step = i + 1;
            return (
                <li data-index={i} className={liclass}>
                    <a href="javascrit:" onClick={(e) => handleSelect(i, e)}>
                        {i == totallist - 1 && l.status == 'To be done' ? <i className="zmdi zmdi-flag"></i> : l.status == 'Completed' ? <i className="zmdi zmdi-check"></i> : l.status == 'In progress' ? <i className="zmdi zmdi-male-alt"></i> : <span>{step}</span>}
                    </a>
                </li>

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
<<<<<<< HEAD
                            {renderCarouselList()}
                            {/* <li className={index == 0 ? "completed-icon active" : "completed-icon"}>
=======

                            
                            <li className={index == 0 ? "completed-icon active" : "completed-icon"}>
>>>>>>> c495e9c8db1280b685a8e748a390e1ee951ea326
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
                            </li> */}


                        </ul>
                    </div>
                </div>
            </div>
        </div>
    )
}

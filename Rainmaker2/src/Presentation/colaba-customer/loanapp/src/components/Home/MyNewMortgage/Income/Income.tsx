import React from 'react';
import PageHead from '../../../../Shared/Components/PageHead';

import {
    ReviewSingleApplicantIcon, EditIcon
} from '../../../../Shared/Components/SVGs';

export const Income = () => {
    
    const IncomeReview = () => {
        return (
            <div className="compo-abt-yourSelf fadein">
                <PageHead title="Review And Continue" />

                <div className="comp-form-panel review-panel review-panel-e-history  colaba-form">
                    <div className="row form-group">
                        <div className="col-md-12">
                            <section className="r-e-history-list">
                                <div className="review-item">
                                    <div className="r-icon">
                                        <ReviewSingleApplicantIcon />
                                    </div>
                                    <div className="r-content">
                                        <div className="title">
                                            <h4>Adeel Shafique</h4>
                                        </div>
                                        <div className="c-type">
                                            Primary-Applicant
                                </div>

                                    </div>
                                </div>
                                <div className="review-item-e-history">
                                    <div className="r-content">
                                        <div className="eh-title">
                                            <h4>Employment:</h4>
                                        </div>
                                        <div className="otherinfo">
                                            <ul>
                                                <li>
                                                    <span>Job Title: </span>UX Designer
                                </li>
                                                <li>
                                                    <span>Start Date: </span>December, 01 2020
                                </li>
                                                <li>
                                                    <span>Years in Profession: </span>1 Year
                                </li>
                                                <li>
                                                    <span>Pay Type: </span>Salaried
                                </li>
                                                <li>
                                                    <span>Address: </span>2690 6th Street
                                </li>
                                            </ul>
                                        </div>



                                    </div>
                                    <div className="r-actions">
                                        <button className="btn btn-edit">
                                            <span className="icon">
                                                <EditIcon />
                                            </span>
                                            <span className="lbl">Edit</span>
                                        </button>


                                    </div>
                                </div>
                                <div className="review-item-e-history">
                                    <div className="r-content">
                                        <div className="eh-title">
                                            <h4>Employment:</h4>
                                        </div>
                                        <div className="otherinfo">
                                            <ul>
                                                <li>
                                                    <span>Job Title: </span>UX Designer
                                </li>
                                                <li>
                                                    <span>Start Date: </span>December, 01 2020
                                </li>
                                                <li>
                                                    <span>Years in Profession: </span>1 Year
                                </li>
                                                <li>
                                                    <span>Pay Type: </span>Salaried
                                </li>
                                                <li>
                                                    <span>Address: </span>2690 6th Street
                                </li>
                                            </ul>
                                        </div>



                                    </div>
                                    <div className="r-actions">
                                        <button className="btn btn-edit">
                                            <span className="icon">
                                                <EditIcon />
                                            </span>
                                            <span className="lbl">Edit</span>
                                        </button>


                                    </div>
                                </div>

                            </section>

                            <section className="r-e-history-list">
                                <div className="review-item">
                                    <div className="r-icon">
                                        <ReviewSingleApplicantIcon />
                                    </div>
                                    <div className="r-content">
                                        <div className="title">
                                            <h4>Adeel Shafique</h4>
                                        </div>
                                        <div className="c-type">
                                            Primary-Applicant
                                </div>

                                    </div>
                                </div>
                                <div className="review-item-e-history">
                                    <div className="r-content">
                                        <div className="eh-title">
                                            <h4>Employment:</h4>
                                        </div>
                                        <div className="otherinfo">
                                            <ul>
                                                <li>
                                                    <span>Job Title: </span>UX Designer
                                </li>
                                                <li>
                                                    <span>Start Date: </span>December, 01 2020
                                </li>
                                                <li>
                                                    <span>Years in Profession: </span>1 Year
                                </li>
                                                <li>
                                                    <span>Pay Type: </span>Salaried
                                </li>
                                                <li>
                                                    <span>Address: </span>2690 6th Street
                                </li>
                                            </ul>
                                        </div>



                                    </div>
                                    <div className="r-actions">
                                        <button className="btn btn-edit">
                                            <span className="icon">
                                                <EditIcon />
                                            </span>
                                            <span className="lbl">Edit</span>
                                        </button>


                                    </div>
                                </div>
                                <div className="review-item-e-history">
                                    <div className="r-content">
                                        <div className="eh-title">
                                            <h4>Employment:</h4>
                                        </div>
                                        <div className="otherinfo">
                                            <ul>
                                                <li>
                                                    <span>Job Title: </span>UX Designer
                                </li>
                                                <li>
                                                    <span>Start Date: </span>December, 01 2020
                                </li>
                                                <li>
                                                    <span>Years in Profession: </span>1 Year
                                </li>
                                                <li>
                                                    <span>Pay Type: </span>Salaried
                                </li>
                                                <li>
                                                    <span>Address: </span>2690 6th Street
                                </li>
                                            </ul>
                                        </div>



                                    </div>
                                    <div className="r-actions">
                                        <button className="btn btn-edit">
                                            <span className="icon">
                                                <EditIcon />
                                            </span>
                                            <span className="lbl">Edit</span>
                                        </button>


                                    </div>
                                </div>

                            </section>
                        </div>
                    </div>

                    <div className="form-footer">
                        <button className="btn btn-primary" >{'Save & Continue'}</button>

                    </div>

                </div>

            </div>
        )

    }

    return (

        //  IncomeCardComp()
        IncomeReview()

    )
}

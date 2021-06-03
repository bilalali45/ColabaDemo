import React, { useEffect } from "react";
import PageHead from "../../../../Shared/Components/PageHead";
import {
  EditIcon,
  ReviewSingleApplicantIcon,
  ReviewHomeIcon,
  LockIcon,
  IconSend,
} from "../../../../Shared/Components/SVGs";
import TextareaField from "../../../../Shared/Components/TextareaField";

export const ReviewDetail = () => {
  useEffect(() => {}, []);

  const FinalReview = () => {
    return (
      <section className="compo-abt-yourSelf fadein">
        <PageHead title="Please Review The Details And Continue." />

        <div className="comp-form-panel review-panel review-panel-e-history  colaba-form">
          {/* getting to know you heading */}
          <div className="row form-group">
            <div className="col-md-12">
              <h4 className="primary-title m-0">
                <strong>Getting To Know You</strong>
              </h4>
            </div>
          </div>
          {/* getting to know you heading */}

          {/* type of transaction */}
          <div className="row form-group">
            <div className="col-md-12">
              <div className="review-item">
                <div className="r-icon">
                  <ReviewHomeIcon />
                </div>
                <div className="r-content">
                  <div className="title">
                    <h4>Type of transaction</h4>
                  </div>
                  <div className="otherinfo mt-2">Purchase New Home</div>
                </div>
                <div className="r-actions">
                  <button className="btn btn-lock">
                    <span className="icon">
                      <LockIcon />
                    </span>
                  </button>
                </div>
              </div>

              {/* it will generate bottom border */}
              <div className="review-item-e-history"></div>
              {/* it will generate bottom border */}
            </div>
          </div>
          {/* type of transaction */}

          {/* applicants */}
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
                    <div className="c-type">Primary-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    {/* <div className="eh-title">
                        <h4>Residence History:</h4>
                      </div> */}

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span> adeels76@gmail.com </span>
                        </li>
                        <li>
                          <span> (789) 564-8465</span>
                        </li>
                        <li>
                          <span>Married</span>
                        </li>
                        <li>
                          <span>
                            Not currently serving, veteran, or surviving spouse
                          </span>
                        </li>
                        <li>
                          <span>2690 6th Street</span>
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

                <div className="review-item">
                  <div className="r-icon">
                    <ReviewSingleApplicantIcon />
                  </div>
                  <div className="r-content">
                    <div className="title">
                      <h4>Raza Hasan</h4>
                    </div>
                    <div className="c-type">Co-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="otherinfo">
                      <ul className="mb-0">
                        <li>
                          <span>razahasan@gamil.com</span>
                        </li>
                        <li>
                          <span>(789)-564-8465</span>
                        </li>
                        <li>
                          <span>Separated</span>
                        </li>
                        <li>
                          <span>
                            Not currently serving, veteran, or surviving spouse
                          </span>
                        </li>
                        <li>
                          <span>2690 6th Street</span>
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

              {/* it will generate bottom border */}
              <div className="review-item-e-history"></div>
              {/* it will generate bottom border */}
            </div>
          </div>
          {/* applicants */}

          {/* My new mortgage heading */}
          <div className="row form-group">
            <div className="col-md-12">
              <h4 className="primary-title m-0">
                <strong>My New Mortgage</strong>
              </h4>
            </div>
          </div>
          {/*  My new mortgage heading */}

          {/* purchased property */}
          <div className="row form-group">
            <div className="col-md-12">
              <section className="r-e-history-list">
                <div className="review-item">
                  <div className="r-icon">
                    <ReviewHomeIcon />
                  </div>
                  <div className="r-content">
                    <div className="title">
                      <h4>Purchase Property</h4>
                    </div>
                  </div>
                </div>

                <div className="review-item-e-history pt-0">
                  <div className="r-content">
                    <div className="otherinfo">
                      <ul className="mb-0">
                        <li>
                          <span> Single Family Property</span>
                        </li>
                        <li>
                          <span>Primary Residence</span>
                        </li>
                        <li>
                          <span>
                            5919 Trussville Crossings Pkwy, Birmingham AL 35235,
                          </span>
                        </li>
                        <li>
                          <span>United State </span>
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

              {/* it will generate bottom border */}
              <div className="review-item-e-history"></div>
              {/* it will generate bottom border */}
            </div>
          </div>
          {/* purchased property */}

          {/* My money and income heading */}
          <div className="row form-group">
            <div className="col-md-12">
              <h4 className="primary-title m-0">
                <strong>My Money / Income</strong>
              </h4>
            </div>
          </div>
          {/*  My money and income heading */}

          {/* applicants money and income history */}
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
                    <div className="c-type">Primary-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Google LLC</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span> Chief Executive Officer</span>
                        </li>
                        <li>
                          <span>
                            Current Employment Monthly Income: $9,000.00
                          </span>
                        </li>
                        <li>
                          <span>From June 2019 to Current</span>
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
                      <h4>US Navy</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span> Midshipman </span>
                        </li>
                        <li>
                          <span>Military Pay</span>
                        </li>
                        <li>
                          <span>Monthly Income: $3,000.00</span>
                        </li>
                        <li>
                          <span>From June 2019 to Current</span>
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

                <div className="review-item">
                  <div className="r-icon">
                    <ReviewSingleApplicantIcon />
                  </div>
                  <div className="r-content">
                    <div className="title">
                      <h4>Raza Hasan</h4>
                    </div>
                    <div className="c-type">Co-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Four Seasons</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span>Barista</span>
                        </li>
                        <li>
                          <span>Self Employment or Independent Contractor</span>
                        </li>
                        <li>
                          <span>Monthly Income: $3,000.00</span>
                        </li>
                        <li>
                          <span>From June 2019 to Current</span>
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
                      <h4>IRA / 401K</h4>
                    </div>

                    <div className="otherinfo">
                      <ul className="mb-0">
                        <li>
                          <span>Retirement</span>
                        </li>
                        <li>
                          <span>Monthly Withdrawals: $3,000.00</span>
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
          {/* applicants money and income history */}

          {/* My money and assets heading */}
          <div className="row form-group">
            <div className="col-md-12">
              <h4 className="primary-title m-0">
                <strong>My Money / Assests</strong>
              </h4>
            </div>
          </div>
          {/*  My money and assets heading */}

          {/* applicants money and assets history */}
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
                    <div className="c-type">Primary-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Checking Account</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span>$30,000.00</span>
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
                      <h4>Retirement Account</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span> $30,200.90 </span>
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

                <div className="review-item">
                  <div className="r-icon">
                    <ReviewSingleApplicantIcon />
                  </div>
                  <div className="r-content">
                    <div className="title">
                      <h4>Raza Hasan</h4>
                    </div>
                    <div className="c-type">Co-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Checking Account</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span>$30,000.00</span>
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
                      <h4>Proceeds From Loan</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span>$20,000.90</span>
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

              {/* it will generate bottom border */}
              <div className="review-item-e-history"></div>
              {/* it will generate bottom border */}
            </div>
          </div>
          {/* applicants money and assets history */}

          {/* My property heading */}
          <div className="row form-group">
            <div className="col-md-12">
              <h4 className="primary-title m-0">
                <strong>My Properties</strong>
              </h4>
            </div>
          </div>
          {/*  My property heading */}

          {/* my properties history */}
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
                    <div className="c-type">Primary-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Current Residence</h4>
                    </div>

                    <div className="otherinfo">
                      <ul className="mb-2">
                        <li>
                          <span>727 Ashleigh Lane, South Lake TX, 76092</span>
                        </li>
                      </ul>
                    </div>

                    <div className="otherinfo text-dark">
                      <ul className="">
                        <li>
                          <span>Single Family Residence</span>
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
                      <h4>Additional Property</h4>
                    </div>

                    <div className="otherinfo">
                      <ul className="mb-2">
                        <li>
                          <span>
                            5919 Trussville Crossings Pkwy, Birmingham AL 35235,
                            USA
                          </span>
                        </li>
                      </ul>
                    </div>

                    <div className="otherinfo text-dark">
                      <ul className="">
                        <li>
                          <span>Manufactured Home</span>
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
                    <div className="otherinfo">
                      <ul className="mb-2">
                        <li>
                          <span>727 Ashleigh Lane, South Lake TX, 76092</span>
                        </li>
                      </ul>
                    </div>

                    <div className="otherinfo text-dark">
                      <ul className="mb-2">
                        <li>
                          <span>Duplex</span>
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

              {/* it will generate bottom border */}
              <div className="review-item-e-history"></div>
              {/* it will generate bottom border */}
            </div>
          </div>
          {/* my properties history */}

          {/* Finishing up heading */}
          <div className="row form-group">
            <div className="col-md-12">
              <h4 className="primary-title m-0">
                <strong>Finishing Up</strong>
              </h4>
            </div>
          </div>
          {/*  Finishing up heading */}

          {/* finishing up history */}
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
                    <div className="c-type">Primary-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Residence History</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span>
                            727 Ashleigh Lane, Southlake TX, 76092 (From Dec.
                            2019)
                          </span>
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
                      <h4>Citizenship status</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span> U.S. Citizen </span>
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

                <div className="review-item">
                  <div className="r-icon">
                    <ReviewSingleApplicantIcon />
                  </div>
                  <div className="r-content">
                    <div className="title">
                      <h4>Raza Hasan</h4>
                    </div>
                    <div className="c-type">Co-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Residence History</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span>
                            725 Ashleigh Lane, Southlake TX, 76092 (From Dec.
                            2018 to Dec. 2019)
                          </span>
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
                      <h4>Citizenship status</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span>Non Permenant Resident - Work Visa Holder</span>
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

              {/* it will generate bottom border */}
              <div className="review-item-e-history"></div>
              {/* it will generate bottom border */}
            </div>
          </div>
          {/* finishing up history */}

          {/* Government questions heading */}
          <div className="row form-group">
            <div className="col-md-12">
              <h4 className="primary-title m-0">
                <strong>Government Question</strong>
              </h4>
            </div>
          </div>
          {/*  Government questions heading */}

          {/* applicant info for government question */}
          <div className="row">
            <div className="col-md-12">
              <div className="review-item">
                <div className="r-icon">
                  <ReviewSingleApplicantIcon />
                </div>
                <div className="r-content">
                  <div className="title">
                    <h4>Adeel Shafique</h4>
                  </div>
                  <div className="c-type">Primary-Applicant</div>
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
            </div>
          </div>
          {/* applicant info for government question */}

          {/* applicants government question history */}
          <div className="row form-group">
            <div className="col-md-12">
              <section className="r-e-history-list pt-0">
                <div className="review-item-e-history pt-0">
                  <div className="r-content">
                    <div className="otherinfo text-dark">
                      <ul>
                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are you borrowing any money for this real estate
                                transaction (e.g., money for your closing costs
                                or down payment) obtaining any money from
                                another party, such as the seller or realtor,
                                that you have not disclosed on this loan
                                application?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>Yes</span>
                            </div>
                          </div>

                          <div className="row mt-4">
                            <div className="col-12">
                              <TextareaField
                                name={"explanation"}
                                placeholder={`Lorem Epsom is simply dummy text Lorem Epsom is simply dummy text Lorem Epsom is simply dummy text.`}
                                className={`textarea-with-bg`}
                              />
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you had an ownership interest in another
                                property in the last three years?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you had an ownership interest in another
                                property in the last three years?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Will this property be subject to a lien that
                                could take priority over the first mortgage
                                lien, such as a clean energy lien paid through
                                your property taxes (e.g., the Property Assessed
                                Clean Energy Program)?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you or will you be applying for a mortgage
                                loan on another property (not the property
                                securing this loan) on or before closing this
                                transaction that is not disclosed on this loan
                                application?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you or will you be applying for any new
                                credit (e.g., instalment loan, credit card,
                                etc.) on or before closing this loan that is not
                                disclosed on this application?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are you a co-signer or guarantor on any debt or
                                loan that is not disclosed on this application?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are there any outstanding judgments against you?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are you currently delinquent or in default on a
                                Federal debt?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are you currently delinquent or in default on a
                                Federal debt?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you conveyed title to any property in lieu
                                of foreclosure in the past 7 years?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>
                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Within the past 7 years, have you completed a
                                pre-foreclosure sale or short sale, whereby the
                                property was sold to a third party and the
                                Lender agreed to accept less than the
                                outstanding mortgage balance due?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>
                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you had property foreclosed upon in the
                                last 7 years?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>
                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you declared bankruptcy within the past 7
                                years?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>
                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are you currently obligated to pay child
                                support, alimony or separate maintenance?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </section>
            </div>
          </div>
          {/* applicants government question history */}

          {/* applicant info for government question */}
          <div className="row">
            <div className="col-md-12">
              <div className="review-item">
                <div className="r-icon">
                  <ReviewSingleApplicantIcon />
                </div>
                <div className="r-content">
                  <div className="title">
                    <h4>Raza Hasan</h4>
                  </div>
                  <div className="c-type">Co-Applicant</div>
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
            </div>
          </div>
          {/* applicant info for government question */}

          {/* applicants government question history */}
          <div className="row form-group">
            <div className="col-md-12">
              <section className="r-e-history-list pt-0">
                <div className="review-item-e-history pt-0">
                  <div className="r-content">
                    <div className="otherinfo text-dark">
                      <ul>
                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are you borrowing any money for this real estate
                                transaction (e.g., money for your closing costs
                                or down payment) obtaining any money from
                                another party, such as the seller or realtor,
                                that you have not disclosed on this loan
                                application?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you had an ownership interest in another
                                property in the last three years?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you had an ownership interest in another
                                property in the last three years?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Will this property be subject to a lien that
                                could take priority over the first mortgage
                                lien, such as a clean energy lien paid through
                                your property taxes (e.g., the Property Assessed
                                Clean Energy Program)?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you or will you be applying for a mortgage
                                loan on another property (not the property
                                securing this loan) on or before closing this
                                transaction that is not disclosed on this loan
                                application?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you or will you be applying for any new
                                credit (e.g., instalment loan, credit card,
                                etc.) on or before closing this loan that is not
                                disclosed on this application?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are you a co-signer or guarantor on any debt or
                                loan that is not disclosed on this application?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are there any outstanding judgments against you?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are you currently delinquent or in default on a
                                Federal debt?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are you currently delinquent or in default on a
                                Federal debt?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>

                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you conveyed title to any property in lieu
                                of foreclosure in the past 7 years?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>
                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Within the past 7 years, have you completed a
                                pre-foreclosure sale or short sale, whereby the
                                property was sold to a third party and the
                                Lender agreed to accept less than the
                                outstanding mortgage balance due?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>
                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you had property foreclosed upon in the
                                last 7 years?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>
                        <li className="mb-4">
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Have you declared bankruptcy within the past 7
                                years?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>Yes</span>
                            </div>
                          </div>

                          <div className="row mt-4">
                            <div className="col-12">
                              <TextareaField
                                name={"explanation"}
                                placeholder={`Lorem Epsom is simply dummy text Lorem Epsom is simply dummy text Lorem Epsom is simply dummy text.`}
                                className={`textarea-with-bg`}
                              />
                            </div>
                          </div>
                        </li>
                        <li className="mb-4">
                          <div className="row">
                            <div className="col-12">
                              <h4>
                                Which Type? <strong> - Chapter 7</strong>
                              </h4>
                            </div>
                          </div>
                          <div className="row">
                            <div className="col-10">
                              <span>
                                Are you currently obligated to pay child
                                support, alimony or separate maintenance?
                              </span>
                            </div>
                            <div className="col-2 text-right">
                              <span>No</span>
                            </div>
                          </div>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </section>

              {/* it will generate bottom border */}
              <div className="review-item-e-history"></div>
              {/* it will generate bottom border */}
            </div>
          </div>
          {/* applicants government question history */}

          {/* Demographic information heading */}
          <div className="row form-group">
            <div className="col-md-12">
              <h4 className="primary-title m-0">
                <strong>Demographic Information</strong>
              </h4>
            </div>
          </div>
          {/* Demographic information heading */}

          {/* Demographic applicant info */}
          <div className="row">
            <div className="col-md-12">
              <div className="review-item">
                <div className="r-icon">
                  <ReviewSingleApplicantIcon />
                </div>
                <div className="r-content">
                  <div className="title">
                    <h4>Adeel Shafique</h4>
                  </div>
                  <div className="c-type">Primary-Applicant</div>
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
            </div>
          </div>
          {/* Demographic applicant info */}

          {/* Demographic applicant info in detail */}
          <div className="row form-group">
            <div className="col-md-12">
              <section className="r-e-history-list">
                <div className="review-item-e-history pt-0">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Adeel’s Race</h4>
                    </div>

                    <div className="otherinfo text-dark">
                      <ul className="mb-2">
                        <li>
                          <span>Asian / Indian Asian</span>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Adeel’s Ethnicity?</h4>
                    </div>

                    <div className="otherinfo text-dark">
                      <ul className="mb-2">
                        <li>
                          <span>
                            Hispanic or Latino / Other Hispanic or Latino
                          </span>
                        </li>
                      </ul>
                    </div>

                    <div className="otherinfo text-dark">
                      <TextareaField
                        className={`primary-textarea`}
                        name={"explanation"}
                        placeholder={`Lorem Epsom is simply dummy text Lorem Epsom is simply dummy text Lorem Epsom is simply dummy text Lorem Epsom.`}
                      />
                    </div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Adeel’s Sex?</h4>
                    </div>
                    <div className="otherinfo text-dark">
                      <ul className="mb-2">
                        <li>
                          <span>Male</span>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </section>

              {/* it will generate bottom border */}
              <div className="review-item-e-history"></div>
              {/* it will generate bottom border */}
            </div>
          </div>
          {/* Demographic applicant info in detail */}

          {/* Demographic applicant info */}
          <div className="row">
            <div className="col-md-12">
              <div className="review-item">
                <div className="r-icon">
                  <ReviewSingleApplicantIcon />
                </div>
                <div className="r-content">
                  <div className="title">
                    <h4>Raza Hasan</h4>
                  </div>
                  <div className="c-type">Co-Applicant</div>
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
            </div>
          </div>
          {/* Demographic applicant info */}

          {/* Demographic applicant info in detail */}
          <div className="row form-group">
            <div className="col-md-12">
              <section className="r-e-history-list">
                <div className="review-item-e-history pt-0">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Adeel’s Race</h4>
                    </div>

                    <div className="otherinfo text-dark">
                      <ul className="mb-2">
                        <li>
                          <span>Asian / Indian Asian</span>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Adeel’s Ethnicity?</h4>
                    </div>

                    <div className="otherinfo text-dark">
                      <ul className="mb-2">
                        <li>
                          <span>
                            Hispanic or Latino / Other Hispanic or Latino
                          </span>
                        </li>
                      </ul>
                    </div>

                    <div className="otherinfo text-dark">
                      <TextareaField
                        className={`primary-textarea`}
                        name={"explanation"}
                        placeholder={`Lorem Epsom is simply dummy text Lorem Epsom is simply dummy text Lorem Epsom is simply dummy text Lorem Epsom.`}
                      />
                    </div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Adeel’s Sex?</h4>
                    </div>
                    <div className="otherinfo text-dark">
                      <ul className="mb-2">
                        <li>
                          <span>Male</span>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </section>
            </div>
          </div>
          {/* Demographic applicant info in detail */}

          {/* additional comment heading */}
          <div className="row form-group">
            <div className="col-md-12">
              <h4 className="m-0">
                <strong>Any additional comments you’d like to add?</strong>
              </h4>
            </div>
          </div>
          {/* additional comment heading */}

          {/* additional comment textarea */}
          <div className="row">
            <div className="col-12">
              <TextareaField
                className={`primary-textarea`}
                name={"explanation"}
                placeholder={`Right any additional comments here.`}
              />
            </div>
          </div>
          {/* additional comment textarea */}

          {/* footer button */}
          <div className="form-footer">
            <button className="btn btn-primary btn-block text-center">
              <IconSend /> {"Submit loan application"}
            </button>
          </div>
          {/* footer button */}
        </div>
      </section>
    );
  };

  return <React.Fragment>{FinalReview()}</React.Fragment>;
};

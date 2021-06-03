import React, { useEffect, useState } from "react";
import Tabs from "react-bootstrap/Tabs";
import Tab from "react-bootstrap/Tab";

import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";
import InputRadioBox from "../../../../Shared/Components/InputRadioBox";
import { PopupModal } from "../../../../Shared/Components/Modal";
import InputDatepicker from "../../../../Shared/Components/InputDatepicker";
import {
  AddressHomeIcon,
  IncomeAddIcon,
  ReviewSingleApplicantIcon,
  EditIcon,
  IconPurchaseProperty,
  DeleteIcon,
} from "../../../../Shared/Components/SVGs";
import { SearchLocationInput } from "../../../../Shared/Components/SearchLocationInput";
import InputField from "../../../../Shared/Components/InputField";
import DropdownList from "../../../../Shared/Components/DropdownList";
import Dropdown from "react-bootstrap/Dropdown";
import TextareaField from "../../../../Shared/Components/TextareaField";
import Assessment from "../../../../Shared/Components/Assessment";
// import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const BorrowerDependents = () => {
  const [key, setKey] = useState<string | null>("one");
  const [firstTabRadioBtn, setFirstTabRadioBtn] = useState<boolean | null>(null);
  const [secondTabRadioBtn, setSecondTabRadioBtn] = useState<boolean | null>(null);

  useEffect(() => {}, []);

  // first screens
  const FinishingUpCurrentResidence = () => {
    return (
      <section>
        <div className="compo-finishing-current-residence fadein">
          <PageHead title="Finishing Up" handlerBack={() => {}} />
          <TooltipTitle
            title={`Adeel, please provide some additional details about your current residence.`}
          />

          <div className="comp-form-panel colaba-form">
            <form
              id="finishing-up-step-one-form"
              data-testid="finishing-up-step-one-form"
              className="colaba-form"
              onSubmit={(e) => {
                e.preventDefault();
              }}
              autoComplete="off"
            >
              <div className="row form-group">
                <div className="col-12">
                  <div className="listaddress-warp">
                    <div className="list-add">
                      <span className="icon-add">
                        <AddressHomeIcon />
                      </span>
                      <span
                        data-testid="subtitle"
                        className="cont-add"
                        dangerouslySetInnerHTML={{
                          __html: `727 Ashleigh Lane #377, Dallas, Texas 75602`,
                        }}
                      ></span>
                    </div>
                  </div>
                </div>
              </div>

              <div className="row form-group">
                <div className="col-12">
                  <h4>When did you move in to this property?</h4>
                </div>
                <div className="col-md-6">
                  <InputDatepicker
                    name={"date"}
                    id="datepicker"
                    label={`Move in Date`}
                    autoComplete="off"
                    autoFocus={false}
                  />
                </div>
              </div>

              <div className="form-footer">
                <button
                  data-testid={"fininshing-up-btn"}
                  id={"fininshing-up-btn"}
                  className="btn btn-primary"
                  onClick={() => {
                    // NavigationHandler.moveNext();
                  }}
                >
                  {"Save & Continue"}
                </button>
              </div>
            </form>
          </div>
        </div>
      </section>
    );
  };
  // first screens

  //co applicants screens
  const FinishingUpCoApplicants = () => {
    return (
      <section>
        <div className="compo-finishing-up-co-applicants fadein">
          <PageHead title="Finishing Up" handlerBack={() => {}} />
          <TooltipTitle
            title={`Now we need to learn more about your co-applicants.`}
          />

          <div className="comp-form-panel colaba-form">
            <div className="ssn-panel finishing-up-tabs e-history">
              <Tabs
                data-testid={`finishing-up-tab`}
                id="finishing-up-tab"
                activeKey={key}
                onSelect={(k) => setKey(k)}
              >
                <Tab
                  data-testid={`tab${1}`}
                  key={1}
                  eventKey={"one"}
                  title={"Jessica"}
                >
                  <div className="f-cWrap">
                    {/* radio button with datetime */}
                    {/* remove 'd-none' class for display in screen */}
                    <form
                      id="finishing-up-step-one-form"
                      data-testid="finishing-up-step-one-form"
                      className="colaba-form d-none"
                      onSubmit={(e) => {
                        e.preventDefault();
                      }}
                      autoComplete="off"
                    >
                      <div className="row form-group">
                        <div className="col-12">
                          <div className="listaddress-warp">
                            <div className="list-add">
                              <span className="icon-add">
                                <AddressHomeIcon />
                              </span>
                              <span
                                data-testid="subtitle"
                                className="cont-add"
                                dangerouslySetInnerHTML={{
                                  __html: `727 Ashleigh Lane #377, Dallas, Texas 75602`,
                                }}
                              ></span>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div className="row form-group inline-lblCheckes">
                        <div className="col-9">
                          <div className="lbl-radio">
                            Has Jessica lived here with you since{" "}
                            <strong>Dec. 2019</strong>?
                          </div>
                        </div>
                        <div className="col-3">
                          <div className="inline-radio">
                            <InputRadioBox
                              id={`lived-with-you-yes`}
                              data-testid={`lived-with-you-yes`}
                              value={"false"}
                              name={`livedWithYou`}
                              onChange={() => {
                                setFirstTabRadioBtn(true);
                              }}
                            >
                              Yes
                            </InputRadioBox>

                            <InputRadioBox
                              id={`lived-with-you-no`}
                              data-testid={`lived-with-you-no`}
                              value={"false"}
                              name={`livedWithYou`}
                              onChange={() => {
                                setFirstTabRadioBtn(false);
                              }}
                            >
                              No
                            </InputRadioBox>
                          </div>
                        </div>
                      </div>

                      {!firstTabRadioBtn && firstTabRadioBtn != null ? (
                        <div className="row form-group">
                          <div className="col-12">
                            <h4>When did you move in to this property?</h4>
                          </div>
                          <div className="col-md-6">
                            <InputDatepicker
                              name={"date"}
                              id="datepicker"
                              label={`Move in Date`}
                              autoComplete="off"
                              autoFocus={false}
                            />
                          </div>
                        </div>
                      ) : (
                        ""
                      )}
                    </form>
                    {/* radio button with datetime */}

                    {/* dateTime */}
                    {/* remove 'd-none' class for display in screen */}
                    <form
                      id="finishing-up-step-one-form"
                      data-testid="finishing-up-step-one-form"
                      className="colaba-form d-none"
                      onSubmit={(e) => {
                        e.preventDefault();
                      }}
                      autoComplete="off"
                    >
                      <div className="row form-group">
                        <div className="col-12">
                          <div className="listaddress-warp">
                            <div className="list-add">
                              <span className="icon-add">
                                <AddressHomeIcon />
                              </span>
                              <span
                                data-testid="subtitle"
                                className="cont-add"
                                dangerouslySetInnerHTML={{
                                  __html: `5919 Trussville Crossings Pkwy, Birmingham AL 35235, USA`,
                                }}
                              ></span>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-12">
                          <h4>When did Jessica move in here?</h4>
                        </div>
                        <div className="col-md-6">
                          <InputDatepicker
                            name={"date"}
                            id="datepicker"
                            label={`Move in Date`}
                            autoComplete="off"
                            autoFocus={false}
                          />
                        </div>
                      </div>
                    </form>
                    {/* dateTime */}

                    {/* dateTime with bold label */}
                    {/* remove 'd-none' class for display in screen */}
                    <form
                      id="finishing-up-step-one-form"
                      data-testid="finishing-up-step-one-form"
                      className="colaba-form "
                      onSubmit={(e) => {
                        e.preventDefault();
                      }}
                      autoComplete="off"
                    >
                      <div className="row form-group">
                        <div className="col-12">
                          <div className="listaddress-warp">
                            <div className="list-add">
                              <span className="icon-add">
                                <AddressHomeIcon />
                              </span>
                              <span
                                data-testid="subtitle"
                                className="cont-add"
                                dangerouslySetInnerHTML={{
                                  __html: `727 Ashleigh Lane #377, Dallas, Texas 75602`,
                                }}
                              ></span>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-12">
                          <h4>
                            When did Jessica start living at:{" "}
                            <strong>
                              5919 Trussville Crossings Pkwy, Birmingham AL
                              35235, USA
                            </strong>
                          </h4>
                        </div>
                        <div className="col-md-6">
                          <InputDatepicker
                            name={"date"}
                            id="datepicker"
                            label={`Move in Date`}
                            autoComplete="off"
                            autoFocus={false}
                          />
                        </div>
                      </div>
                    </form>
                    {/* dateTime */}
                  </div>
                </Tab>

                <Tab
                  data-testid={`tab${2}`}
                  key={2}
                  eventKey={"two"}
                  title={"Tommy"}
                >
                  <div className="f-cWrap">
                    {/* radio button with datetime */}
                    {/* remove 'd-none' class for display in screen */}
                    <form
                      id="finishing-up-step-one-form"
                      data-testid="finishing-up-step-one-form"
                      className="colaba-form d-none"
                      onSubmit={(e) => {
                        e.preventDefault();
                      }}
                      autoComplete="off"
                    >
                      <div className="row form-group">
                        <div className="col-12">
                          <div className="listaddress-warp">
                            <div className="list-add">
                              <span className="icon-add">
                                <AddressHomeIcon />
                              </span>
                              <span
                                data-testid="subtitle"
                                className="cont-add"
                                dangerouslySetInnerHTML={{
                                  __html: `727 Ashleigh Lane #377, Dallas, Texas 75602`,
                                }}
                              ></span>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div className="row form-group inline-lblCheckes">
                        <div className="col-9">
                          <div className="lbl-radio">
                            Has Tommy lived here with you since{" "}
                            <strong>Dec. 2019</strong>?
                          </div>
                        </div>
                        <div className="col-3">
                          <div className="inline-radio">
                            <InputRadioBox
                              id={`lived-with-you-yes`}
                              data-testid={`lived-with-you-yes`}
                              value={"false"}
                              name={`livedWithYou`}
                              onChange={() => {
                                setSecondTabRadioBtn(true);
                              }}
                            >
                              Yes
                            </InputRadioBox>

                            <InputRadioBox
                              id={`lived-with-you-no`}
                              data-testid={`lived-with-you-no`}
                              value={"false"}
                              name={`livedWithYou`}
                              onChange={() => {
                                setSecondTabRadioBtn(false);
                              }}
                            >
                              No
                            </InputRadioBox>
                          </div>
                        </div>
                      </div>

                      {!secondTabRadioBtn && secondTabRadioBtn != null ? (
                        <div className="row form-group">
                          <div className="col-12">
                            <h4>When did you move in to this property?</h4>
                          </div>
                          <div className="col-md-6">
                            <InputDatepicker
                              name={"date"}
                              id="datepicker"
                              label={`Move in Date`}
                              autoComplete="off"
                              autoFocus={false}
                            />
                          </div>
                        </div>
                      ) : (
                        ""
                      )}
                    </form>
                    {/* radio button with datetime */}

                    {/* dateTime */}
                    {/* remove 'd-none' class for display in screen */}
                    <form
                      id="finishing-up-step-one-form"
                      data-testid="finishing-up-step-one-form"
                      className="colaba-form d-none"
                      onSubmit={(e) => {
                        e.preventDefault();
                      }}
                      autoComplete="off"
                    >
                      <div className="row form-group">
                        <div className="col-12">
                          <div className="listaddress-warp">
                            <div className="list-add">
                              <span className="icon-add">
                                <AddressHomeIcon />
                              </span>
                              <span
                                data-testid="subtitle"
                                className="cont-add"
                                dangerouslySetInnerHTML={{
                                  __html: `5919 Trussville Crossings Pkwy, Birmingham AL 35235, USA`,
                                }}
                              ></span>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-12">
                          <h4>When did Tommy move in here?</h4>
                        </div>
                        <div className="col-md-6">
                          <InputDatepicker
                            name={"date"}
                            id="datepicker"
                            label={`Move in Date`}
                            autoComplete="off"
                            autoFocus={false}
                          />
                        </div>
                      </div>
                    </form>
                    {/* dateTime */}

                    {/* dateTime with bold label */}
                    {/* remove 'd-none' class for display in screen */}
                    <form
                      id="finishing-up-step-one-form"
                      data-testid="finishing-up-step-one-form"
                      className="colaba-form "
                      onSubmit={(e) => {
                        e.preventDefault();
                      }}
                      autoComplete="off"
                    >
                      <div className="row form-group">
                        <div className="col-12">
                          <div className="listaddress-warp">
                            <div className="list-add">
                              <span className="icon-add">
                                <AddressHomeIcon />
                              </span>
                              <span
                                data-testid="subtitle"
                                className="cont-add"
                                dangerouslySetInnerHTML={{
                                  __html: `727 Ashleigh Lane #377, Dallas, Texas 75602`,
                                }}
                              ></span>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-12">
                          <h4>
                            When did Tommy start living at:{" "}
                            <strong>
                              5919 Trussville Crossings Pkwy, Birmingham AL
                              35235, USA
                            </strong>
                          </h4>
                        </div>
                        <div className="col-md-6">
                          <InputDatepicker
                            name={"date"}
                            id="datepicker"
                            label={`Move in Date`}
                            autoComplete="off"
                            autoFocus={false}
                          />
                        </div>
                      </div>
                    </form>
                    {/* dateTime */}
                  </div>
                </Tab>
              </Tabs>
              <div className="form-footer">
                <button
                  data-testid={"fininshing-up-btn"}
                  id={"fininshing-up-btn"}
                  className="btn btn-primary"
                  onClick={() => {
                    // NavigationHandler.moveNext();
                  }}
                >
                  {"Save & Continue"}
                </button>
              </div>
            </div>
          </div>
        </div>
      </section>
    );
  };
  //co applicants screens

  //residence alert modal screen
  const FinishingUpAlertModalData = () => {
    const alertBody = () => {
      return (
        <div className="emp-alert-data">
          <p>
            Looks like the following applicants don’t have two years of
            Residency history yet
          </p>
          <ul>
            <li>Jessica</li>
            <li>Tommy</li>
          </ul>
          <p>No problem! We’ll collect that on the next page</p>
        </div>
      );
    };

    return (
      <PopupModal
        modalHeading={`Residency Alert`}
        modalBodyData={alertBody()}
        modalFooterData={
          <button className="btn btn-min btn-primary">Continue</button>
        }
        show={true}
        handlerShow={() => {}}
        dialogClassName={`residency-alert-popup`}
      ></PopupModal>
    );
  };
  //residence alert modal screen

  //residence history screen
  const FinishingUpResidenceHistory = () => {
    return (
      <section>
        <div className="compo-finishing-up-co-applicants fadein">
          <PageHead title="Finishing Up" handlerBack={() => {}} />
          <TooltipTitle
            title={`Sorry, Adeel, but we’re going to need some additional residence history.`}
          />

          <div className="comp-form-panel colaba-form">
            <div className="ssn-panel finishing-up-tabs e-history">
              <Tabs
                data-testid={`finishing-up-tab`}
                id="finishing-up-tab"
                activeKey={key}
                onSelect={(k) => setKey(k)}
              >
                <Tab
                  data-testid={`tab${1}`}
                  key={1}
                  eventKey={"one"}
                  title={"Jessica"}
                >
                  <div className="f-cWrap">
                    {/* residence history */}
                    <div className="row form-group">
                      <div className="col-12">
                        <h4>
                          Where else has Jessica lived over the last two years?
                        </h4>
                      </div>
                      <div className="col-12">
                        <div className="e-history-list-wrap">
                          <div className="e-h-l">
                            <div className="ehl-icon">
                              <IconPurchaseProperty />
                            </div>
                            <div className="ehl-content">
                              <div className="ehl-title">
                                <h5>727 Ashleigh Lane, South Lake TX, 76092</h5>
                              </div>
                              <div className="ehl-date">From Dec. 2019</div>
                            </div>

                            <div className="finishing-up-actions ehl-actions">
                              <div className="edit-icon action-icons">
                                <EditIcon />
                              </div>

                              <div className="delete-icon action-icons">
                                <DeleteIcon />
                              </div>
                            </div>
                          </div>

                          <div className="e-h-l">
                            <div className="ehl-icon">
                              <IconPurchaseProperty />
                            </div>
                            <div className="ehl-content">
                              <div className="ehl-title">
                                <h5>727 Ashleigh Lane, South Lake TX, 76092</h5>
                              </div>
                              <div className="ehl-date">
                                From Oct. 2018 to Dec. 2019
                              </div>
                            </div>
                            <div className="finishing-up-actions ehl-actions">
                              <div className="edit-icon action-icons">
                                <EditIcon />
                              </div>

                              <div className="delete-icon action-icons">
                                <DeleteIcon />
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    {/*  residence history  */}
                  </div>
                </Tab>

                <Tab
                  data-testid={`tab${2}`}
                  key={2}
                  eventKey={"two"}
                  title={"Tommy"}
                >
                  <div className="f-cWrap">
                    {/* residence history */}
                    <div className="row form-group">
                      <div className="col-12">
                        <h4>
                          Where else has Tommy lived over the last two years?
                        </h4>
                      </div>
                      <div className="col-12">
                        <div className="e-history-list-wrap">
                          <div className="e-h-l">
                            <div className="ehl-icon">
                              <IconPurchaseProperty />
                            </div>
                            <div className="ehl-content">
                              <div className="ehl-title">
                                <h5>727 Ashleigh Lane, South Lake TX, 76092</h5>
                              </div>
                              <div className="ehl-date">From Dec. 2019</div>
                            </div>

                            <div className="finishing-up-actions ehl-actions">
                              <div className="edit-icon action-icons">
                                <EditIcon />
                              </div>

                              <div className="delete-icon action-icons">
                                <DeleteIcon />
                              </div>
                            </div>
                          </div>

                          <div className="e-h-l">
                            <div className="ehl-icon">
                              <IconPurchaseProperty />
                            </div>
                            <div className="ehl-content">
                              <div className="ehl-title">
                                <h5>727 Ashleigh Lane, South Lake TX, 76092</h5>
                              </div>
                              <div className="ehl-date">
                                From Oct. 2018 to Dec. 2019
                              </div>
                            </div>
                            <div className="finishing-up-actions ehl-actions">
                              <div className="edit-icon action-icons">
                                <EditIcon />
                              </div>

                              <div className="delete-icon action-icons">
                                <DeleteIcon />
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    {/*  residence history  */}
                  </div>
                </Tab>
              </Tabs>

              <div className="p-footer-tabs">
                <div className="left-col">
                  <div className="add-i-source-link">
                    <span className="icon">
                      <IncomeAddIcon />{" "}
                    </span>
                    Add Previous Residence
                  </div>
                  <button className="btn btn-inline m-left">
                    Skip and Continue
                  </button>
                </div>
                <div className="right-col">
                  <button
                    data-testid={"fininshing-up-btn"}
                    id={"fininshing-up-btn"}
                    className="btn btn-primary"
                    onClick={() => {
                      // NavigationHandler.moveNext();
                    }}
                    disabled={true}
                  >
                    {"Save & Continue"}
                  </button>
                </div>
              </div>

              {FinishingUpResidenceModal()}
            </div>
          </div>
        </div>
      </section>
    );
  };
  //residence history screen

  //residence address modal screen
  const FinishingUpResidenceAddress = () => {
    return (
      <>
        <form
          id="finishing-up-residence-detail"
          data-testid="finishing-up-residence-detail-form"
          className="colaba-form"
          onSubmit={(e) => {
            e.preventDefault();
          }}
          autoComplete="off"
        >
          <div className="row">
            <div className="col-sm-12">
              <SearchLocationInput
                caller={2}
                labelText={`Previous Residence Property Address`}
                status={false}
                handlerToggle={() => {}}
                setFieldValues={() => {}}
                refreshFields={() => {}}
                initialAddress={""}
                restrictedCountries={[]}
                homeAddressPlaceholder={`Search Home Address`}
                restrictCountries={true}
              />
            </div>
          </div>
          <div className="row">
            <div className={`col-md-6 fadein`}>
              <InputField
                label={"Street Address"}
                data-testid="street_address"
                id="street_address"
                name="street_address"
                type={"text"}
                placeholder={"Full Street Address"}
              />
            </div>
            <div className={`col-md-6 fadein`}>
              <InputField
                label={"Unit or Apt. number"}
                data-testid="Unit"
                id="unit"
                name="unit"
                type="text"
              />
            </div>
            <div className={`col-md-6 fadein`}>
              <InputField
                label={"City"}
                data-testid="City"
                id="city"
                name="city"
                type={"text"}
                placeholder={"Enter City"}
              />
            </div>

            <div className={`col-md-6 fadein`}>
              <InputField
                label={`State`}
                data-testid="state"
                id="state"
                name="state"
                type={"text"}
                placeholder={"State"}
              />
            </div>

            <div className={`col-md-6 fadein`}>
              <InputField
                label={`Zip Code`}
                data-testid="Zip-Code"
                id="zip_code"
                name="zip_code"
                type={"text"}
                maxLength={5}
                placeholder={"XXXXX"}
              />
            </div>
            <div className={`col-md-6 fadein`}>
              <InputField
                label={`Country`}
                data-testid="country"
                id="country"
                name="country"
                type={"text"}
                placeholder={"Country"}
              />
            </div>
          </div>
          <div className="form-footer text-right">
            <button
              data-testid={"fininshing-up-btn"}
              id={"fininshing-up-btn"}
              className="btn btn-primary"
              onClick={() => {
                // NavigationHandler.moveNext();
              }}
            >
              {"Next"}
            </button>
          </div>
        </form>
      </>
    );
  };
  //residence address modal screen

  //residence moving in modal screen
  const FinishingUpMovingDetail = () => {
    return (
      <>
        <form
          id="finishing-up-moving-detail"
          data-testid="finishing-up-moving-detail-form"
          className="colaba-form"
          onSubmit={(e) => {
            e.preventDefault();
          }}
          autoComplete="off"
        >
          <div className="row form-group">
            <div className="col-12">
              <div className="listaddress-warp">
                <div className="list-add">
                  <span className="icon-add">
                    <AddressHomeIcon />
                  </span>
                  <span
                    data-testid="subtitle"
                    className="cont-add"
                    dangerouslySetInnerHTML={{
                      __html: `727 Ashleigh Lane #377, Dallas, Texas 75602`,
                    }}
                  ></span>
                </div>
              </div>
            </div>
          </div>

          <div className="row form-group">
            <div className="col-md-6">
              <InputDatepicker
                name={"date"}
                id="datepicker"
                label={`Move in Date`}
                autoComplete="off"
                autoFocus={false}
              />
            </div>

            <div className="col-md-6">
              <InputDatepicker
                name={"date"}
                id="datepicker"
                label={`Move out Date`}
                autoComplete="off"
                autoFocus={false}
              />
            </div>

            <div className={`col-md-6`}>
              <DropdownList
                label={"Housing Status"}
                data-testid="Housing-Status"
                id="housingStatus"
                placeholder="Select Housing Status"
                name="housingStatus"
              >
                <Dropdown.Item
                  data-testid={"housingStatus-option"}
                  key={"1"}
                  eventKey={"1"}
                >
                  Housing status 1
                </Dropdown.Item>
                <Dropdown.Item
                  data-testid={"housingStatus-option"}
                  key={"2"}
                  eventKey={"2"}
                >
                  Housing status 2
                </Dropdown.Item>
                <Dropdown.Item
                  data-testid={"housingStatus-option"}
                  key={"3"}
                  eventKey={"3"}
                >
                  Housing status 3
                </Dropdown.Item>
              </DropdownList>
            </div>

            <div className="col-md-6">
              <InputField
                label={"Monthly Rent"}
                id={"monthly-rent"}
                name="monthlRent"
                type={"text"}
                icon={<i className="zmdi zmdi-money"></i>}
                placeholder={"Monthly Rent"}
              />
            </div>
          </div>

          <div className="form-footer text-right">
            <button
              data-testid={"fininshing-up-btn"}
              id={"fininshing-up-btn"}
              className="btn btn-primary"
              onClick={() => {
                // NavigationHandler.moveNext();
              }}
            >
              {"Next"}
            </button>
          </div>
        </form>
      </>
    );
  };
  //residence moving in modal screen

  // residence modal screen
  const FinishingUpResidenceModal = () => {
    return (
      <>
        <div data-testid="pop-modal" className={`income-popup`}>
          <div className="income-popup-head">
            <span className="icon-back" onClick={() => {}}>
              <i className="zmdi zmdi-arrow-left"></i>
            </span>
            <h4
              data-testid="popup-title"
              dangerouslySetInnerHTML={{
                __html: `Where else has Jessica lived in the last two years?`,
              }}
            ></h4>

            <div
              data-testid="popup-close"
              className="icon-close"
              onClick={() => {}}
            >
              <i className="zmdi zmdi-close"></i>
            </div>
          </div>
          <div className="income-popup-body">
            {/* {FinishingUpResidenceAddress()} */}
            {FinishingUpMovingDetail()}
          </div>
        </div>
        <div className="income-popup-wrap"></div>
      </>
    );
  };
  // residence modal screen

  // citizenship status screens
  const FinishingUpCitizenShip = () => {
    return (
      <section>
        <div className="compo-finishing-up-co-applicants fadein">
          <PageHead title="Finishing Up" handlerBack={() => {}} />
          <TooltipTitle
            title={`Now we’ll ask for everybody’s citizenship status.`}
          />

          <div className="comp-form-panel colaba-form">
            <div className="ssn-panel finishing-up-tabs e-history">
              <Tabs
                data-testid={`finishing-up-tab`}
                id="finishing-up-tab"
                activeKey={key}
                onSelect={(k) => setKey(k)}
              >
                <Tab
                  data-testid={`tab${1}`}
                  key={1}
                  eventKey={"one"}
                  title={"You"}
                >
                  <div className="f-cWrap">
                    <form
                      id="finishing-up-step-one-form"
                      data-testid="finishing-up-step-one-form"
                      className="colaba-form"
                      onSubmit={(e) => {
                        e.preventDefault();
                      }}
                      autoComplete="off"
                    >
                      <div className="row form-group">
                        <div className="col-12">
                          <h4>Select Your Citizenship Status.</h4>
                        </div>
                        <div className="col-md-6">
                          <InputRadioBox
                            id="us-citizen"
                            data-testid="us_citizen"
                            className=""
                            name="citizenship_status"
                            value={"us_citizen"}
                          >
                            US Citizen
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="green-card"
                            data-testid="green_card"
                            className=""
                            name="citizenship_status"
                            value={"green_card"}
                          >
                            Permenant Resident Alien (Green Card)
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="resident-alien"
                            data-testid="resident_alien"
                            className=""
                            name="citizenship_status"
                            value={"resident_alien"}
                          >
                            Non Permanent Resident Alien
                          </InputRadioBox>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-12">
                          <h4>Please select your visa status</h4>
                        </div>
                        <div className="col-md-6">
                          <InputRadioBox
                            id="temporary-visa"
                            data-testid="temporary_visa"
                            className=""
                            name="visa_status"
                            value={"temporary_visa"}
                          >
                            I am a temporary worker (H-2A, etc.){" "}
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="work-visa"
                            data-testid="work_visa"
                            className=""
                            name="visa_status"
                            value={"work_visa"}
                          >
                            I hold a valid work visa (H1, L1, etc.){" "}
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="other-visa"
                            data-testid="other_visa"
                            className=""
                            name="visa_status"
                            value={"other_visa"}
                          >
                            Other
                          </InputRadioBox>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-12 col-md-8">
                          <TextareaField
                            name={"other-visa-explanation"}
                            label={`Please provide an explanation of your status`}
                            placeholder={`Status explanation`}
                          />
                        </div>
                      </div>
                    </form>
                  </div>
                </Tab>

                <Tab
                  data-testid={`tab${2}`}
                  key={1}
                  eventKey={"two"}
                  title={"Jessica"}
                >
                  <div className="f-cWrap">
                    <form
                      id="finishing-up-step-one-form"
                      data-testid="finishing-up-step-one-form"
                      className="colaba-form"
                      onSubmit={(e) => {
                        e.preventDefault();
                      }}
                      autoComplete="off"
                    >
                      <div className="row form-group">
                        <div className="col-12">
                          <h4>Select Your Citizenship Status.</h4>
                        </div>
                        <div className="col-md-6">
                          <InputRadioBox
                            id="us-citizen"
                            data-testid="us_citizen"
                            className=""
                            name="citizenship_status"
                            value={"us_citizen"}
                          >
                            US Citizen
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="green-card"
                            data-testid="green_card"
                            className=""
                            name="citizenship_status"
                            value={"green_card"}
                          >
                            Permenant Resident Alien (Green Card)
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="resident-alien"
                            data-testid="resident_alien"
                            className=""
                            name="citizenship_status"
                            value={"resident_alien"}
                          >
                            Non Permanent Resident Alien
                          </InputRadioBox>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-12">
                          <h4>Please select your visa status</h4>
                        </div>
                        <div className="col-md-6">
                          <InputRadioBox
                            id="temporary-visa"
                            data-testid="temporary_visa"
                            className=""
                            name="visa_status"
                            value={"temporary_visa"}
                          >
                            I am a temporary worker (H-2A, etc.){" "}
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="work-visa"
                            data-testid="work_visa"
                            className=""
                            name="visa_status"
                            value={"work_visa"}
                          >
                            I hold a valid work visa (H1, L1, etc.){" "}
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="other-visa"
                            data-testid="other_visa"
                            className=""
                            name="visa_status"
                            value={"other_visa"}
                          >
                            Other
                          </InputRadioBox>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-12 col-md-8">
                          <TextareaField
                            name={"other-visa-explanation"}
                            label={`Please provide an explanation of your status`}
                            placeholder={`Status explanation`}
                          />
                        </div>
                      </div>
                    </form>
                  </div>
                </Tab>

                <Tab
                  data-testid={`tab${3}`}
                  key={1}
                  eventKey={"three"}
                  title={"Tommy"}
                >
                  <div className="f-cWrap">
                    <form
                      id="finishing-up-step-one-form"
                      data-testid="finishing-up-step-one-form"
                      className="colaba-form"
                      onSubmit={(e) => {
                        e.preventDefault();
                      }}
                      autoComplete="off"
                    >
                      <div className="row form-group">
                        <div className="col-12">
                          <h4>Select Your Citizenship Status.</h4>
                        </div>
                        <div className="col-md-6">
                          <InputRadioBox
                            id="us-citizen"
                            data-testid="us_citizen"
                            className=""
                            name="citizenship_status"
                            value={"us_citizen"}
                          >
                            US Citizen
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="green-card"
                            data-testid="green_card"
                            className=""
                            name="citizenship_status"
                            value={"green_card"}
                          >
                            Permenant Resident Alien (Green Card)
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="resident-alien"
                            data-testid="resident_alien"
                            className=""
                            name="citizenship_status"
                            value={"resident_alien"}
                          >
                            Non Permanent Resident Alien
                          </InputRadioBox>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-12">
                          <h4>Please select your visa status</h4>
                        </div>
                        <div className="col-md-6">
                          <InputRadioBox
                            id="temporary-visa"
                            data-testid="temporary_visa"
                            className=""
                            name="visa_status"
                            value={"temporary_visa"}
                          >
                            I am a temporary worker (H-2A, etc.){" "}
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="work-visa"
                            data-testid="work_visa"
                            className=""
                            name="visa_status"
                            value={"work_visa"}
                          >
                            I hold a valid work visa (H1, L1, etc.){" "}
                          </InputRadioBox>
                        </div>

                        <div className="col-md-12">
                          <InputRadioBox
                            id="other-visa"
                            data-testid="other_visa"
                            className=""
                            name="visa_status"
                            value={"other_visa"}
                          >
                            Other
                          </InputRadioBox>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-12 col-md-8">
                          <TextareaField
                            name={"other-visa-explanation"}
                            label={`Please provide an explanation of your status`}
                            placeholder={`Status explanation`}
                          />
                        </div>
                      </div>
                    </form>
                  </div>
                </Tab>
              </Tabs>
              <div className="form-footer">
                <button
                  data-testid={"fininshing-up-btn"}
                  id={"fininshing-up-btn"}
                  className="btn btn-primary"
                  onClick={() => {
                    // NavigationHandler.moveNext();
                  }}
                >
                  {"Save & Continue"}
                </button>
              </div>
            </div>
          </div>
        </div>
      </section>
    );
  };
  // citizenship status screens

  // dependent information screens
  const FinishingUpDependentInfo = () => {
    return (
      <section>
        <div className="compo-finishing-dependent-info fadein">
          <PageHead title="Finishing Up" handlerBack={() => {}} />
          <TooltipTitle title={`Onwards to dependent information!`} />

          <div className="comp-form-panel colaba-form">
            <form
              id="finishing-up-dependent-info-form"
              data-testid="finishing-up-dependent-info-form"
              className="colaba-form"
              onSubmit={(e) => {
                e.preventDefault();
              }}
              autoComplete="off"
            >
              <div className="row form-group">
                <div className="col-12">
                  <h4>How many dependents do you have?</h4>
                </div>
              </div>

              <div className="row">
                <div className="col-md-6">
                  <InputField
                    label={"Number of Dependent"}
                    data-testid="independent"
                    id="independent"
                    name="independent"
                    type={"number"}
                    placeholder={"Add Dependent Number"}
                  />
                </div>
              </div>

              <div className="row form-group">
                <div className="col-md-6">
                  <InputField
                    label={"Age of Dependent Number 1"}
                    data-testid="dependent-age-1"
                    id="dependent-age-1"
                    name="dependent-age-1"
                    type={"text"}
                    placeholder={"Dependent Age"}
                  />
                </div>
                <div className="col-md-6">
                  <InputField
                    label={"Age of Dependent Number 2"}
                    data-testid="dependent-age-2"
                    id="dependent-age-2"
                    name="dependent-age-2"
                    type={"text"}
                    placeholder={"Dependent Age"}
                  />
                </div>
              </div>

              <div className="form-footer">
                <button
                  data-testid={"fininshing-up-btn"}
                  id={"fininshing-up-btn"}
                  className="btn btn-primary"
                  onClick={() => {
                    // NavigationHandler.moveNext();
                  }}
                >
                  {"Save & Continue"}
                </button>
                <button className="btn btn-inline m-left" onClick={() => {}}>
                  Skip and Continue
                </button>
              </div>
            </form>
          </div>
        </div>
      </section>
    );
  };
  // dependent information screens

  // marital status screen
  const FinishingUpMaritalStatus = () => {
    return (
      <section>
        <div className="compo-finishing-up-marital-status fadein">
          <PageHead title="Finishing Up" handlerBack={() => {}} />
          <TooltipTitle
            title={`Adeel, looks we need some more info about you and your co-applicants’ marital status`}
          />

          <div className="comp-form-panel colaba-form">
            <div className="ssn-panel finishing-up-tabs e-history">
              <Tabs
                data-testid={`finishing-up-tab`}
                id="finishing-up-tab"
                activeKey={key}
                onSelect={(k) => setKey(k)}
              >
                <Tab
                  data-testid={`tab${1}`}
                  key={1}
                  eventKey={"one"}
                  title={"Jessica"}
                >
                  <div className="f-cWrap">
                    <form
                      id="finishing-up-step-one-form"
                      data-testid="finishing-up-step-one-form"
                      className="colaba-form"
                      onSubmit={(e) => {
                        e.preventDefault();
                      }}
                      autoComplete="off"
                    >
                      <div className="row form-group">
                        <div className="col-12">
                          <div className="listaddress-warp">
                            <div className="list-add">
                              <span
                                data-testid="subtitle"
                                className="cont-add"
                                dangerouslySetInnerHTML={{
                                  __html: `Who is Jessica married to?`,
                                }}
                              ></span>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-md-6">
                          <InputField
                            label={"Legal First Name"}
                            data-testid="legalFirstName"
                            id="legalFirstName"
                            name="legalFirstName"
                            type={"text"}
                            placeholder={"Legal First Name"}
                            autoFocus={true}
                            maxLength={100}
                          />
                        </div>
                        <div className="col-md-6">
                          <InputField
                            label={"Middle Name"}
                            data-testid="MiddleName"
                            id="MiddleName"
                            name="MiddleName"
                            type={"text"}
                            placeholder={"Middle Name"}
                          />
                        </div>
                        <div className="col-md-6">
                          <InputField
                            label={"Legal Last Name"}
                            data-testid="LegalLastName"
                            id="LegalLastName"
                            name="LegalLastName"
                            type={"text"}
                            placeholder={"Legal Last Name"}
                          />
                        </div>
                        <div className="col-md-6">
                          <InputField
                            label={"Suffix"}
                            data-testid="Suffix"
                            id="Suffix"
                            name="Suffix"
                            type={"text"}
                            placeholder={"Jr., Sr., III, IV, etc."}
                          />
                        </div>
                      </div>
                    </form>
                  </div>
                </Tab>

                <Tab
                  data-testid={`tab${2}`}
                  key={2}
                  eventKey={"two"}
                  title={"Tommy"}
                >
                  <div className="f-cWrap">
                    <form
                      id="finishing-up-step-one-form"
                      data-testid="finishing-up-step-one-form"
                      className="colaba-form"
                      onSubmit={(e) => {
                        e.preventDefault();
                      }}
                      autoComplete="off"
                    >
                      <div className="row form-group">
                        <div className="col-12">
                          <div className="listaddress-warp">
                            <div className="list-add">
                              <span
                                data-testid="subtitle"
                                className="cont-add"
                                dangerouslySetInnerHTML={{
                                  __html: `Who is Tommy married to?`,
                                }}
                              ></span>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div className="row form-group">
                        <div className="col-md-6">
                          <InputField
                            label={"Legal First Name"}
                            data-testid="legalFirstName"
                            id="legalFirstName"
                            name="legalFirstName"
                            type={"text"}
                            placeholder={"Legal First Name"}
                            autoFocus={true}
                            maxLength={100}
                          />
                        </div>
                        <div className="col-md-6">
                          <InputField
                            label={"Middle Name"}
                            data-testid="MiddleName"
                            id="MiddleName"
                            name="MiddleName"
                            type={"text"}
                            placeholder={"Middle Name"}
                          />
                        </div>
                        <div className="col-md-6">
                          <InputField
                            label={"Legal Last Name"}
                            data-testid="LegalLastName"
                            id="LegalLastName"
                            name="LegalLastName"
                            type={"text"}
                            placeholder={"Legal Last Name"}
                          />
                        </div>
                        <div className="col-md-6">
                          <InputField
                            label={"Suffix"}
                            data-testid="Suffix"
                            id="Suffix"
                            name="Suffix"
                            type={"text"}
                            placeholder={"Jr., Sr., III, IV, etc."}
                          />
                        </div>
                      </div>
                    </form>
                  </div>
                </Tab>
              </Tabs>
              <div className="form-footer">
                <button
                  data-testid={"fininshing-up-btn"}
                  id={"fininshing-up-btn"}
                  className="btn btn-primary"
                  onClick={() => {
                    // NavigationHandler.moveNext();
                  }}
                >
                  {"Save & Continue"}
                </button>
              </div>
            </div>
          </div>
        </div>
      </section>
    );
  };
  // marital status screen

  // review and continue screen
  const FinishingUpReviewAndContinue = () => {
    return (
      <section className="compo-abt-yourSelf fadein">
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
                    <div className="c-type">Primary-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Residence History:</h4>
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
                          <span>U.S. Citizen</span>
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
                      <h4>Jessica</h4>
                    </div>
                    <div className="c-type">Co-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Residence History:</h4>
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
                          <span>Permanent Resident Alien (Green Card)</span>
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
                      <h4>Tommy</h4>
                    </div>
                    <div className="c-type">Co-Applicant</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Residence History:</h4>
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

              <section className="r-e-history-list">
                <div className="review-item">
                  <div className="r-icon">
                    <ReviewSingleApplicantIcon />
                  </div>
                  <div className="r-content">
                    <div className="title">
                      <h4>Adeel, Jessica, and Tommy</h4>
                    </div>
                    <div className="c-type">Applicants</div>
                  </div>
                </div>

                <div className="review-item-e-history">
                  <div className="r-content">
                    <div className="eh-title">
                      <h4>Dependents (ages)</h4>
                    </div>

                    <div className="otherinfo">
                      <ul>
                        <li>
                          <span>2</span>
                        </li>
                        <li>
                          <span>12</span>
                        </li>
                        <li>
                          <span>20</span>
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
            <button className="btn btn-primary">{"Save & Continue"}</button>
          </div>
        </div>
      </section>
    );
  };
  // review and continue sreen

  return (
    <React.Fragment>
      {
        // FinishingUpCurrentResidence()
        // FinishingUpCoApplicants()
        // FinishingUpAlertModalData()
        // FinishingUpResidenceHistory()
        // FinishingUpCitizenShip()
        // FinishingUpDependentInfo()
        // FinishingUpMaritalStatus()
        FinishingUpReviewAndContinue()
      }

      <div>
        <Assessment />
      </div>
    </React.Fragment>
  );
};


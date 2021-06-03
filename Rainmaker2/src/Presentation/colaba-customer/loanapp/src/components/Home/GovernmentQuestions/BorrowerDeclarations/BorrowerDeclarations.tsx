import React, { useEffect, useState } from "react";
import Tabs from "react-bootstrap/Tabs";
import Tab from "react-bootstrap/Tab";
import Carousel from 'react-bootstrap/Carousel'
import Dropdown from "react-bootstrap/Dropdown";

import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";
import InputRadioBox from "../../../../Shared/Components/InputRadioBox";
import InputField from "../../../../Shared/Components/InputField";
import TextareaField from "../../../../Shared/Components/TextareaField";
import DropdownList from "../../../../Shared/Components/DropdownList";
import InputCheckedBox from "../../../../Shared/Components/InputCheckedBox";
// import { Section1 } from "../Section1/Section1";
// import { Section2 } from "../Section2/Section2";
// import { Section3 } from "../Section3/Section3";
// import { Section4 } from "../Section4/Section4";

export const BorrowerDeclarations = () => {
  const [key, setKey] = useState<string | null>("one");
  const [index, setIndex] = useState(0);

  const handleSelect = (selectedIndex) => {
    console.log("slide index :: ", index);
    setIndex(selectedIndex);
  };

  useEffect(() => {}, []);


  const FirstSlide = () => {
    return (
      <form
        id={`government-question-slide-one-form-${index}`}
        data-testid={`government-question-slide-one-form-${index}`}
        className="colaba-form"
        onSubmit={(e) => {
          e.preventDefault();
        }}
        autoComplete="off"
      >
        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Are you borrowing any money for this real estate transaction
              (e.g., money for your closing costs or down payment) obtaining any
              money from another party, such as the seller or realtor, that you
              have not disclosed on this loan application?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-one-q1-yes-${index}`}
                data-testid={`slide-one-q1-yes-${index}`}
                value={"true"}
                name={`slide-one-q1`}
                checked
                onChange={() => {}}
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-one-q1-no-${index}`}
                data-testid={`slide-one-q1-no-${index}`}
                value={"false"}
                name={`slide-one-q1`}
                onChange={() => {}}
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group">
          <div className="col-6">
            <InputField
              label={"What is the amount of this money?"}
              id={`estimate-money-${index}`}
              name="estimate_money"
              type={"text"}
              icon={<i className="zmdi zmdi-money"></i>}
              placeholder={"Estimated Money"}
            />
          </div>

          <div className="col-12">
            <TextareaField
              className={`textarea-with-bg`}
              name={"explanation"}
              placeholder={`Please explain`}
            />
          </div>
        </div>

        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Have you had an ownership interest in another property in the last
              three years?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-one-q2-yes-${index}`}
                data-testid={`slide-one-q2-yes-${index}`}
                value={"true"}
                name={`slide-one-q2`}
                checked
                onChange={() => {}}
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-one-q2-no-${index}`}
                data-testid={`slide-one-q2-no-${index}`}
                value={"false"}
                name={`slide-one-q2`}
                onChange={() => {}}
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group">
          <div className="col-6">
            <DropdownList
              id={`slide-one-q2-dropdown-1-${index}`}
              title="Dropdown button"
              label={"What type of property did you own?"}
              placeholder={`Select One`}
              onDropdownSelect={() => {}}
            >
              <Dropdown.Item as="button">option 1</Dropdown.Item>
              <Dropdown.Item as="button">option 2</Dropdown.Item>
              <Dropdown.Item as="button">option 3</Dropdown.Item>
            </DropdownList>
          </div>

          <div className="col-6">
            <DropdownList
              id={`slide-one-q2-dropdown-2-${index}`}
              title="Dropdown button"
              label={"How did you hold title to the property?"}
              placeholder={`Select One`}
              onDropdownSelect={() => {}}
            >
              <Dropdown.Item as="button">option 1</Dropdown.Item>
              <Dropdown.Item as="button">option 2</Dropdown.Item>
              <Dropdown.Item as="button">option 3</Dropdown.Item>
            </DropdownList>
          </div>
        </div>

        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Will this property be subject to a lien that could take priority
              over the first mortgage lien, such as a clean energy lien paid
              through your property taxes (e.g., the Property Assessed Clean
              Energy Program)?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-one-q3-yes-${index}`}
                data-testid={`slide-one-q3-yes-${index}`}
                value={"true"}
                name={`slide-one-q3`}
                onChange={() => {}}
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-one-q3-no-${index}`}
                data-testid={`slide-one-q3-no-${index}`}
                value={"false"}
                name={`slide-one-q3`}
                onChange={() => {}}
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Have you or will you be applying for a mortgage loan on another
              property (not the property securing this loan) on or before
              closing this transaction that is not disclosed on this loan
              application?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-one-q4-yes-${index}`}
                data-testid={`slide-one-q4-yes-${index}`}
                value={"true"}
                name={`slide-one-q4`}
                onChange={() => {}}
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-one-q4-no-${index}`}
                data-testid={`slide-one-q4-no-${index}`}
                value={"false"}
                name={`slide-one-q4`}
                onChange={() => {}}
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Have you or will you be applying for any new credit (e.g.,
              installment loan, credit card, etc.) on or before closing this
              loan that is not disclosed on this application?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-one-q5-yes-${index}`}
                data-testid={`slide-one-q5-yes-${index}`}
                value={"true"}
                name={`slide-one-q5`}
                onChange={() => {}}
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-one-q5-no-${index}`}
                data-testid={`slide-one-q5-no-${index}`}
                value={"false"}
                name={`slide-one-q5`}
                onChange={() => {}}
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>
      </form>
    );
  }
  
  const SecondSlide = () => {
    return (
      <form
        id={`government-question-slide-two-form-${index}`}
        data-testid={`government-question-slide-two-form-${index}`}
        className="colaba-form"
        onSubmit={(e) => {
          e.preventDefault();
        }}
        autoComplete="off"
      >
        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Are you a co-signer or guarantor on any debt or loan that is not
              disclosed on this application?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-two-q1-yes-${index}`}
                data-testid={`slide-two-q1-yes-${index}`}
                value={"true"}
                name={`slide-two-q1`}
                onChange={() => {}}
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-two-q1-no-${index}`}
                data-testid={`slide-two-q1-no-${index}`}
                value={"false"}
                name={`slide-two-q1`}
                onChange={() => {}}
                checked
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Are there any outstanding judgments against you?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-two-q2-yes-${index}`}
                data-testid={`slide-two-q2-yes-${index}`}
                value={"true"}
                name={`slide-two-q2`}
                onChange={() => {}}
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-two-q2-no-${index}`}
                data-testid={`slide-two-q2-no-${index}`}
                value={"false"}
                name={`slide-two-q2`}
                onChange={() => {}}
                checked
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Are you currently delinquent or in default on a Federal debt?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-two-q3-yes-${index}`}
                data-testid={`slide-two-q3-yes-${index}`}
                value={"true"}
                name={`slide-two-q3`}
                onChange={() => {}}
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-two-q3-no-${index}`}
                data-testid={`slide-two-q3-no-${index}`}
                value={"false"}
                name={`slide-two-q3`}
                onChange={() => {}}
                checked
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Are you a party to a lawsuit in which you potentially have any
              personal financial liability?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-two-q4-yes-${index}`}
                data-testid={`slide-two-q4-yes-${index}`}
                value={"true"}
                name={`slide-two-q4`}
                onChange={() => {}}
                checked
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-two-q4-no-${index}`}
                data-testid={`slide-two-q4-no-${index}`}
                value={"false"}
                name={`slide-two-q4`}
                onChange={() => {}}
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group">
          <div className="col-12">
            <TextareaField
              name={"explanation"}
              placeholder={`Please explain`}
              className={`textarea-with-bg`}
            />
          </div>
        </div>

        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Have you conveyed title to any property in lieu of foreclosure in
              the past 7 years?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-two-q5-yes-${index}`}
                data-testid={`slide-two-q5-yes-${index}`}
                value={"true"}
                name={`slide-two-q5`}
                onChange={() => {}}
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-two-q5-no-${index}`}
                data-testid={`slide-two-q5-no-${index}`}
                value={"false"}
                name={`slide-two-q5`}
                onChange={() => {}}
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>
      </form>
    );
  }

  const ThirdSlide = () => {
    return (
      <form
        id={`government-question-slide-three-form-${index}`}
        data-testid={`government-question-slide-three-form-${index}`}
        className="colaba-form"
        onSubmit={(e) => {
          e.preventDefault();
        }}
        autoComplete="off"
      >
        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Within the past 7 years, have you completed a pre-foreclosure sale
              or short sale, whereby the property was sold to a third party and
              the Lender agreed to accept less than the outstanding mortgage
              balance due?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-three-q1-yes-${index}`}
                data-testid={`slide-three-q1-yes-${index}`}
                value={"true"}
                name={`slide-three-q1`}
                onChange={() => {}}
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-three-q1-no-${index}`}
                data-testid={`slide-three-q1-no-${index}`}
                value={"false"}
                name={`slide-three-q1`}
                onChange={() => {}}
                checked
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Have you had property foreclosed upon in the last 7 years?{" "}
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-three-q2-yes-${index}`}
                data-testid={`slide-three-q2-yes-${index}`}
                value={"true"}
                name={`slide-three-q2`}
                onChange={() => {}}
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-three-q2-no-${index}`}
                data-testid={`slide-three-q2-no-${index}`}
                value={"false"}
                name={`slide-three-q2`}
                onChange={() => {}}
                checked
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Have you declared bankruptcy within the past 7 years?{" "}
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-three-q3-yes-${index}`}
                data-testid={`slide-three-q3-yes-${index}`}
                value={"true"}
                name={`slide-three-q3`}
                onChange={() => {}}
                checked
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-three-q3-no-${index}`}
                data-testid={`slide-three-q3-no-${index}`}
                value={"false"}
                name={`slide-three-q3`}
                onChange={() => {}}
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group">
          <div className="col-12">
            <h4>Which type?</h4>
          </div>

          <div className="col-3">
            <div className="form-group chk-agree">
              <InputCheckedBox
                name="chapter-7"
                label={`Chapter 7`}
                checked={true}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-3">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name="chapter-11"
                label={`Chapter 11`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-3">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name="chapter-12"
                label={`Chapter 12`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-3">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name="chapter-13"
                label={`Chapter 13`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-12">
            <TextareaField
              name={"explanation"}
              placeholder={`Please explain`}
              className={`textarea-with-bg`}
            />
          </div>
        </div>

        <div className="row form-group inline-lblCheckes">
          <div className="col-9">
            <div className="lbl-radio">
              Are you currently obligated to pay child support, alimony o r
              separate maintenance?
            </div>
          </div>
          <div className="col-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`slide-three-q4-yes`}
                data-testid={`slide-three-q4-yes`}
                value={"true"}
                name={`slide-three-q4`}
                onChange={() => {}}
                checked
              >
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`slide-three-q4-no`}
                data-testid={`slide-three-q4-no`}
                value={"false"}
                name={`slide-three-q4`}
                onChange={() => {}}
              >
                No
              </InputRadioBox>
            </div>
          </div>
        </div>

        <div className="row form-group">
          <div className="col-12">
            <h4>Select all that apply</h4>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name="child-support"
                label={`Child Support`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name="alimony"
                label={`Alimony`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name="separate-maintenance"
                label={`Separate Maintenance`}
                value={""}
                checked={true}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-12 align-with-checkbox-label">
            <div className="row">
              <div className="col-4">
                <InputField
                  label={"Payments Remaining"}
                  id={"payment-status"}
                  name="payment_status"
                  type={"text"}
                  placeholder={"Payment Status"}
                />
              </div>

              <div className="col-4">
                <InputField
                  label={"Monthly Payment"}
                  id={"monthly-payment"}
                  name="monthly_payment"
                  type={"text"}
                  icon={<i className="zmdi zmdi-money"></i>}
                  placeholder={"Monthly Amount"}
                />
              </div>

              <div className="col-4">
                <InputField
                  label={"Payment Recipient"}
                  id={"name-of-individual"}
                  name="name_of_individual"
                  type={"text"}
                  placeholder={"Name of Individual"}
                />
              </div>
            </div>
          </div>
        </div>
      </form>
    );
  }

  const FourthSlide = () => {
    return (
      <form
        id={`government-question-slide-three-form-${index}`}
        data-testid={`government-question-slide-three-form-${index}`}
        className="colaba-form"
        onSubmit={(e) => {
          e.preventDefault();
        }}
        autoComplete="off"
      >
        <div className="row form-group">
          <div className="col-12">
            <h5>
              <strong>Adeel’s Race</strong>
            </h5>
          </div>
        </div>

        <div className="row form-group">
          <div className="col-12">
            <h4>Select all that apply.</h4>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`American Indian or Alaska Native`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`Asian`}
                value={""}
                checked={true}
              ></InputCheckedBox>
            </div>

            <div className="col-12 px-0 align-with-checkbox-label">
              <div className="row">
                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Asian Indian`}
                      value={""}
                      checked={true}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Chinese`}
                      value={""}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Filipino`}
                      value={""}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Japanese`}
                      value={""}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Korean`}
                      value={""}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Vietnamese`}
                      value={""}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Other Asian`}
                      value={""}
                      checked={true}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-8">
                  <TextareaField
                    name={"explanation"}
                    placeholder={`For example, Pakistani, Laotian, Thai, etc.`}
                    className={`textarea-with-bg`}
                  />
                </div>
              </div>
            </div>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`Black or African American`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`Native Hawaiian or Other Pacific Islander`}
                value={""}
                checked={true}
              ></InputCheckedBox>
            </div>

            <div className="col-12 px-0 align-with-checkbox-label">
              <div className="row">
                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Native Hawaiian`}
                      value={""}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Guamanian or Chamorro`}
                      value={""}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Samoan`}
                      value={""}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Other Pacific Islander`}
                      value={""}
                      checked={true}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-8">
                  <TextareaField
                    name={"explanation"}
                    placeholder={`For example, Fijian, Tongan, etc.`}
                    className={`textarea-with-bg`}
                  />
                </div>
              </div>
            </div>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`White`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`I do not wish to provide this information`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>
        </div>

        <div className="row form-group">
          <div className="col-12">
            <h5>
              <strong>Adeel’s Ethnicity?</strong>
            </h5>
          </div>
        </div>

        <div className="row form-group">
          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`Hispanic or Latino`}
                value={""}
                checked
              ></InputCheckedBox>
            </div>

            <h4> Select all that apply.</h4>

            <div className="col-12 px-0 align-with-checkbox-label">
              <div className="row">
                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Mexican`}
                      value={""}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Puerto Rican`}
                      value={""}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Cuban`}
                      value={""}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-12">
                  <div className="form-group chk-agree">
                    <InputCheckedBox
                      
                      name=""
                      label={`Other Hispanic or Latnio`}
                      value={""}
                      checked={true}
                    ></InputCheckedBox>
                  </div>
                </div>

                <div className="col-8">
                  <TextareaField
                    name={"explanation"}
                    placeholder={`For example, Argentinean, Colombian, Dominican, etc.`}
                    className={`textarea-with-bg`}
                  />
                </div>
              </div>
            </div>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`Not Hispanic or Latino`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`I do not wish to provide this information`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>
        </div>

        <div className="row form-group">
          <div className="col-12">
            <h5>
              <strong>Adeel’s Sex?</strong>
            </h5>
          </div>
        </div>

        <div className="row form-group">
          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`Male`}
                value={""}
                checked={true}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`Female`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>

          <div className="col-12">
            <div className="form-group chk-agree">
              <InputCheckedBox
                
                name=""
                label={`I do not wish to provide this information`}
                value={""}
              ></InputCheckedBox>
            </div>
          </div>
        </div>
      </form>
    );
  }

  const CarouselSlides = () => {
    return (
      <div className="carousel-wrapper">
        <Carousel fade controls={false} interval={null} onSelect={handleSelect}>
          {/* slide one */}
          <Carousel.Item>
            <Carousel.Caption>{FirstSlide()}</Carousel.Caption>
          </Carousel.Item>
          {/* slide one */}

          {/* slide two */}
          <Carousel.Item>
            <Carousel.Caption>{SecondSlide()}</Carousel.Caption>
          </Carousel.Item>
          {/* slide two */}

          {/* slide three */}
          <Carousel.Item>
            <Carousel.Caption>{ThirdSlide()}</Carousel.Caption>
          </Carousel.Item>
          {/* slide three */}

          {/* slide four */}
          <Carousel.Item>
            <Carousel.Caption>{FourthSlide()}</Carousel.Caption>
          </Carousel.Item>
          {/* slide four */}
        </Carousel>
      </div>
    );
  }

  const GovernmentQuestions = () => {
    return (
      <section>
        <div className="compo-government-question fadein">
          <PageHead title="Government Questions" handlerBack={() => {}} />

          {index === 3 ? (
            <TooltipTitle
              title={`Welcome to the final step of your application Adeel! The home stretch is here`}
            />
          ) : (
            <TooltipTitle
              title={`Phew! The final section of the application. You’re in the home stretch!`}
            />
          )}

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
                  title={"Adeel"}
                >
                  <div className="f-cWrap">{CarouselSlides()}</div>
                </Tab>

                <Tab
                  data-testid={`tab${2}`}
                  key={1}
                  eventKey={"two"}
                  title={"Jessica"}
                >
                  <div className="f-cWrap">{CarouselSlides()}</div>
                </Tab>

                <Tab
                  data-testid={`tab${3}`}
                  key={2}
                  eventKey={"three"}
                  title={"Tommy"}
                >
                  <div className="f-cWrap">{CarouselSlides()}</div>
                </Tab>
              </Tabs>

              <div className="form-footer">
                <button
                  data-testid={"fininshing-up-btn"}
                  id={"fininshing-up-btn"}
                  className="btn btn-primary"
                  onClick={() => {}}
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

  return <React.Fragment>{GovernmentQuestions()}</React.Fragment>;

  // return (
  //   <div>
  //     <h2>Borrower Declaration Tabss</h2>
  //     <Section1></Section1>
  //     <Section2></Section2>
  //     <Section3></Section3>
  //     <Section4></Section4>
  //   </div>
  // );
};

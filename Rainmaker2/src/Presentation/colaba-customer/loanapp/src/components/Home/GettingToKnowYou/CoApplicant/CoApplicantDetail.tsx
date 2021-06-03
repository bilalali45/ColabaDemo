import React, { ChangeEvent, useState } from "react";
import { useForm } from "react-hook-form";

import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";
import InputField from "../../../../Shared/Components/InputField";

export const CoApplicantDetail = ({setcurrentStep }: any) => {
  const { register, errors, handleSubmit } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "firstError",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  const [legalFirstName, setLegalFirstName] = useState<string>("");
  
  const personalInfoSaveBtnHandler = (data: any) => {
    console.log('---------------------->', data)
    setcurrentStep("about_current_home");
  }

  const onChangeHandler = (event: React.FormEvent<HTMLInputElement>) => {
    const value = event.currentTarget.value;
    if (value.length > 0 && !/^[A-Za-z\s&(%'.-]+$/.test(value)) return;
    setLegalFirstName(value);
  }

  return (
    <div className="compo-abt-yourSelf fadein">
      <PageHead
        title="Personal Information"
        handlerBack={() => {
          setcurrentStep("about_your_self");
        }}
      />
      <TooltipTitle title="Please provide a few details about your co-applicant." />
      <form data-testid="personal-info-form">
        <div className="comp-form-panel colaba-form">
          <div className="row form-group">
            <div className="col-md-6">
              <InputField
                label={"Co-applicant Legal First Name"}
                data-testid="legalFirstName"
                id="legalFirstName"
                name="legalFirstName"
                type={"text"}
                placeholder={"Legal First Name"}
                autoFocus={true}
                maxLength={100}
                register={register}
                value={legalFirstName}
                onChange={(event:ChangeEvent<HTMLInputElement>) => onChangeHandler(event)}
                rules={{
                  required: "This field is required."
                }}
                errors={errors}
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
                label={"Co-applicant Legal Last Name"}
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

            <div className="col-md-6">
              <InputField
                label={"Email Address"}
                data-testid="EmailAddress"
                id="EmailAddress"
                name="EmailAddress"
                type={"email"}
                placeholder={"Email Address"}
              />
            </div>
            <div className="col-md-6">
              <InputField
                label={"Home Phone Number"}
                data-testid="HomePhoneNumber"
                id="HomePhoneNumber"
                name="HomePhoneNumber"
                type={"text"}
                placeholder={"XXX-XXX-XXXX"}
              />
            </div>

            <div className="col-md-6">
              <InputField
                label={"Work Phone Number "}
                data-testid="WorkPhoneNumber "
                id="WorkPhoneNumber"
                name="WorkPhoneNumber"
                type={"tel"}
                extention={true}
                extentionValue={`555`}
                placeholder={"XXX-XXX-XXXX"}
              />
            </div>
            <div className="col-md-6">
              <InputField
                label={"Cell Phone Number"}
                data-testid="CellPhoneNumber"
                id="CellPhoneNumber"
                name="CellPhoneNumber"
                type={"tel"}
                placeholder={"XXX-XXX-XXXX"}
              />
            </div>
          </div>
          <div className="form-footer">
            <button
              className="btn btn-primary"
              type="submit"
              onClick={handleSubmit(personalInfoSaveBtnHandler)}
            >
              {"Save & Continue"}
            </button>
          </div>
        </div>
      </form>
    </div>
  )
}

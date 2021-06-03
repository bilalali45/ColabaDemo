import React, { useEffect } from "react";
import { Controller, useForm } from "react-hook-form";
import InputField from "../../../../../../../Shared/Components/InputField";
import InputDatepicker from "../../../../../../../Shared/Components/InputDatepicker";


type props = {
    startDate? : any;
    setStartDate?: Function;
    employerName?: string;
    setEmployerName?: Function;
    jobTitle?: string;
    setJobTitle?: Function;
    yearsInProfession?: string;
    setYearsInProfession?: Function;
    onNextClick?: Function;
}

export const MilitaryIncomeWeb = ({startDate, setStartDate, employerName, setEmployerName, jobTitle, setJobTitle, yearsInProfession, setYearsInProfession,onNextClick}: props) => {
  
  const {
    register,
    errors,
    handleSubmit,
    setValue,
    clearErrors,
    control,
  } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
  });
  
  useEffect(() => {
    setValue("startDate", startDate)
  },[startDate])

  const onSubmit = (data) => {
    onNextClick && onNextClick(data)
  };

  const jobTitleOnChangeHandler = (event) => {
    const value = event.currentTarget.value;
    if (value.length > 0 && !/^[a-zA-Z0-9%&(.'\-\s]{1,150}$/g.test(value)) {
      return false
    }
    setJobTitle && setJobTitle(value)
    clearErrors("JobTitle");
    return true;
  };

  return (
    <div>
      <form
        id="employer-info-form"
        data-testid="employer-info-form"
        autoComplete="off">
        <div className="p-body">
        <div className="row">
          <div className="col-sm-6">
            <InputField
              label={"Employer Name"}
              data-testid={"employer-name"}
              id={"employer-name"}
              name="EmployerName"
              type={"text"}
              placeholder={"Employer Name Here"}
              onChange={(e) => {
                setEmployerName && setEmployerName(e.target.value);
                clearErrors("EmployerName");
              }}
              value={employerName}
              register={register}
              rules={{
                required: "This field is required.",
              }}
              autoFocus={true}
              maxLength={100}
              errors={errors}
            />
          </div>
          <div className="col-sm-6">
            <InputField
              label={"Job Title"}
              data-testid={"job-title"}
              id={"job-title"}
              name="JobTitle"
              type={"text"}
              placeholder={"Job Title"}
              onChange={(e) => {
                  jobTitleOnChangeHandler(e)
              }}
              value={jobTitle}
              register={register}
              rules={{
                required: "This field is required.",
              }}
              maxLength={150}
              errors={errors}
            />
          </div>
          <div className="col-sm-6">
            {
              <Controller
                control={control}
                name={"startDate"}
                render={() => (
                  <InputDatepicker
                    isPreviousDateAllowed={true}
                    isFutureDateAllowed = {false}
                    // className={`${errors && 'error'}`}
                    id={"startDate"}
                    data-testid={"startDate"}
                    label={`Start Date`}
                    placeholder={"MM/DD/YYYY"}
                    dateFormat="MM/dd/yyyy"
                    name={"startDate"}
                    handleOnChange={(date: any) => {setStartDate && setStartDate(date); setValue("startDate", date); clearErrors(startDate)}}
                    autoComplete={"off"}
                    selected={startDate}
                    handleOnChangeRaw={(e) => {
                      e.preventDefault();
                    }}
                    errors={errors}
                    ref={register}
                  />
                )}
                errors={errors}
                ref={register}
                rules={{
                  required: "This field is required.",
                  validate: {
                    // validity: value => !(value > new Date())  || "DOB cannot be future date."
                  },
                }}
                defaultValue={""}
              />
            }
          </div>
          <div className="col-sm-6">
            <InputField
              label={"Years in Profession"}
              data-testid={"years-in-profession"}
              id={"years-in-profession"}
              name="YearsInProfession"
              type={"text"}
              maxLength={2}
              placeholder={"Enter Years of Profession"}
              onChange={(e) => {
                let value = e.target.value;
                if(isNaN(Number(value))) return;
                setYearsInProfession && setYearsInProfession(value);
                  clearErrors("YearsInProfession");
                }}
              value={yearsInProfession}
              register={register}
              rules={{
                required: "This field is required.",
              }}
              errors={errors}
            />
          </div>
         
        </div>
        </div>
        <div className="p-footer">
          <button
            id="employer-info-next"
            data-testid="employer-info-next"
            className="btn btn-primary"
            onClick={handleSubmit(onSubmit)}>
            Next
          </button>
        </div>
      </form>
    </div>
  );
};



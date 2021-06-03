import React, { ChangeEvent, useContext, useEffect, useState } from "react";
import InputField from "../../../../../../../Shared/Components/InputField";
import InputDatepicker from "../../../../../../../Shared/Components/InputDatepicker";
import InputRadioBox from "../../../../../../../Shared/Components/InputRadioBox";
import { Controller, useForm } from "react-hook-form";
import { NavigationHandler } from '../../../../../../../Utilities/Navigation/NavigationHandler';
import {
  checkValidUSNumber,
  maskPhoneNumber,
  unMaskPhoneNumber,
} from "../../../../../../../Utilities/helpers/PhoneMasking";
import { EmployerInfo } from "../../../../../../../Entities/Models/Employment";
import _ from "lodash";
import { Store } from "../../../../../../../store/store";
import { EmploymentIncomeActionTypes } from "../../../../../../../store/reducers/EmploymentIncomeReducer";
import moment from "moment";

type initialDataObj = {
  employerName: string, 
  jobTitle: string, 
  startDate: string, 
  yearsInProfession:number,
  employerPhoneNumber: string, 
  employedByFamilyOrParty:boolean, 
  ownershipInterest:string,
  incomeInfoId:number,
}
export const EmploymentIncome = () => {
  
  const { state, dispatch } = useContext(Store);
  const {incomeInfo} :any = state.loanManager;
  const { employerInfo }: any = state.employment;
  const [startDate, setStartDate] = useState<any>(null);

  const [employerName, setEmployerName] = useState<string>("");
  const [jobTitle, setJobTitle] = useState<string>("");
  const [phoneNumber, setPhoneNumber] = useState<string>("");
  const [yearsInProfession, setYearsInProfession] = useState<string>("");
  const [employed, setEmployed] = useState<boolean>(false);
  const [hasOwnership, setHasOwnership] = useState<boolean>(false);
  const [ownershipPercentage, setOwnershipPercentage] = useState<string>("");

  const {
    register,
    errors,
    handleSubmit,
    setValue,
    clearErrors,
    setError,
    control,
  } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
    defaultValues: { EmployerName: employerName, JobTitle:jobTitle, startDate: startDate, YearsInProfession:yearsInProfession, EmployerPhoneNumber:phoneNumber,employed: employed, ownershipInterest: hasOwnership}
  });

  useEffect(()=>{
    if(employerInfo){
      const{EmployerName, JobTitle, StartDate, YearsInProfession, EmployerPhoneNumber, EmployedByFamilyOrParty, IncomeInfoId, OwnershipInterest} = employerInfo;
      let currnetEmployerInfo = {
        employerName:EmployerName,
        jobTitle:JobTitle,
        startDate: StartDate,
        yearsInProfession:+YearsInProfession,
        employerPhoneNumber: EmployerPhoneNumber, 
        employedByFamilyOrParty:EmployedByFamilyOrParty, 
        ownershipInterest:OwnershipInterest,
        incomeInfoId:IncomeInfoId
      }
      setInitialData(currnetEmployerInfo)
    }
  },[employerInfo])

  

  const setInitialData = (data: initialDataObj) =>{
    if(!_.isEmpty(data)){
      const{employerName, jobTitle, startDate, yearsInProfession, employerPhoneNumber, employedByFamilyOrParty, ownershipInterest} = data;
      
      let employed:any = employedByFamilyOrParty ? document.querySelector("#employed-yes"): document.querySelector("#employed-no")
      if(employed) employed.click();
      let hasOwnership:any = ownershipInterest && +ownershipInterest > 0 ? document.querySelector("#ownership-interest-yes"): document.querySelector("#ownership-interest-no")
      if(hasOwnership) hasOwnership.click();setEmployerName(employerName);
      setJobTitle(jobTitle);
      setStartDate(new Date(startDate));
      setValue("startDate",new Date(startDate))
      setYearsInProfession(yearsInProfession?.toString())
      setPhoneNumber(employerPhoneNumber);
      setEmployed(employedByFamilyOrParty);
      setHasOwnership(ownershipInterest && +ownershipInterest > 0 ? true :false);
      setOwnershipPercentage(ownershipInterest && +ownershipInterest > 0 ? ownershipInterest :"");
      
    }
  }


  const onSubmit = () => {
    let currnetEmployerInfo: EmployerInfo = {
      EmployerName:employerName,
      JobTitle:jobTitle,
      StartDate: moment(startDate).format('YYYY-MM-DD'),
      YearsInProfession:+yearsInProfession,
      EmployerPhoneNumber: phoneNumber, 
      EmployedByFamilyOrParty:employed, 
      OwnershipInterest:+ownershipPercentage,
      IncomeInfoId:incomeInfo && incomeInfo.incomeId ? incomeInfo.incomeId : null
    }

      dispatch({type: EmploymentIncomeActionTypes.SetEmployerInfo, payload: currnetEmployerInfo});
      NavigationHandler.navigation?.moveNext();
  };

  const onPhoneNumberChangeHandler = (event: ChangeEvent<HTMLInputElement>) => {
    clearErrors("EmployerPhoneNumber");
    const mobileNumber = unMaskPhoneNumber(event.target.value);
    if (mobileNumber.length > 0 && !/^[0-9]{1,10}$/g.test(mobileNumber)) {
      return false;
    }
    if (
      mobileNumber.length === 10 &&
      !checkValidUSNumber(mobileNumber)
    ) {
      setError("EmployerPhoneNumber", {
        type: "server",
        message: "Please enter US Phone Number only.",
      });
      return false;
    }

    setPhoneNumber(mobileNumber);
    return true;
  };

  const jobTitleOnChangeHandler = (event: ChangeEvent<HTMLInputElement>) => {

    
    const value = event.currentTarget.value;
    if (value.length > 0 && !/^[a-zA-Z0-9%&(.'\-\s]{1,150}$/g.test(value)) {
      return false
    }
    setJobTitle(value)
    clearErrors("JobTitle");
    return true;
  };

  return (
    <div>
      <form
        id="employer-info-form"
        data-testid="employer-info-form"
        onSubmit={handleSubmit(onSubmit)}
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
              onChange={(e: ChangeEvent<HTMLInputElement>) => {
                setEmployerName(e.target.value);
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
              onChange={(e: ChangeEvent<HTMLInputElement>) => {
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
                    // className={`${errors && 'error'}`}
                    isFutureDateAllowed={false}
                    id={"startDate"}
                    data-testid={"startDate"}
                    label={`Start Date`}
                    placeholder={"MM/DD/YYYY"}
                    dateFormat="MM/dd/yyyy"
                    name={"startDate"}
                    handleOnChange={(date: any) => {
                      clearErrors("startDate")
                      setStartDate(date); 
                      setValue("startDate", date); 
                      
                    }}
                    autoComplete={"off"}
                    selected={startDate}
                    handleOnChangeRaw={(e : ChangeEvent<HTMLInputElement>) => {
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
                    validity: value => !(value > new Date())  || "Start Date cannot be future date."
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
              onChange={(e: ChangeEvent<HTMLInputElement>) => {
                let value = e.target.value;
                if(isNaN(Number(value))) return;
                  setYearsInProfession(value);
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
          <div className="col-sm-6">
            <InputField
              label={"Employer Phone Number"}
              data-testid={"employer-phone-number"}
              id={"employer-phone-number"}
              name="EmployerPhoneNumber"
              type={"text"}
              placeholder={"XXX-XXX-XXXX"}
              onChange={onPhoneNumberChangeHandler}
              value={!!phoneNumber ? maskPhoneNumber(phoneNumber) : ""}
              register={register}
              maxLength={14}
              rules={{
                minLength: {
                  value: 14,
                  message: "Please enter US Phone Number only",
                },
                required: "This field is required.",
              }}
              errors={errors}
            />
          </div>
        </div>

        <div className="row inline-lblCheckes">
          <div className="col-sm-9">
            <div className="lbl-radio">
              Are you employed by a family member, property seller, real estate
              agent, or other party to the transaction?
            </div>
          </div>
          <div className="col-sm-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`employed-yes`}
                data-testid={`employed-yes`}
                className=""
                value={"true"}
                name={`employed`}
                onChange={() => {
                  setEmployed(true);
                }}
                register={register}
                rules={{
                  required: "Please select one",
                }}
                errors={errors}>
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`employed-no`}
                data-testid={`employed-no`}
                className=""
                value={"false"}
                name={`employed`}
                onChange={() => {
                  setEmployed(false);
                }}
                register={register}
                rules={{
                  required: "Please select one",
                }}
                errors={errors}>
                No
              </InputRadioBox>
            </div>
            {errors?.employed && <span className="form-error no-padding" role="alert" data-testid="employed-error">{errors?.employed?.message}</span>}
          </div>
        </div>
        <div className="row inline-lblCheckes">
          <div className="col-sm-9">
            <div className="lbl-radio">
              Do you have an ownership interest in this business?
            </div>
          </div>
          <div className="col-sm-3">
            <div className="inline-radio">
              <InputRadioBox
                id={`ownership-interest-yes`}
                data-testid={`ownership-interest-yes`}
                className=""
                value={"true"}
                name={`ownershipInterest`}
                onChange={() => {
                  setHasOwnership(true);
                }}
                register={register}
                rules={{
                  required: "Please select one",
                }}
                errors={errors}>
                Yes
              </InputRadioBox>

              <InputRadioBox
                id={`ownership-interest-no`}
                data-testid={`ownership-interest-no`}
                className=""
                value={"false"}
                name={`ownershipInterest`}
                onChange={() => {
                  setHasOwnership(false);
                  setOwnershipPercentage("");
                }}
                register={register}
                rules={{
                  required: "Please select one",
                }}
                errors={errors}>
                No
              </InputRadioBox>
            </div>
            {errors?.ownershipInterest && <span className="form-error no-padding" role="alert" data-testid="ownershipInterest-error">{errors?.ownershipInterest?.message}</span>}
          </div>
        </div>
        {hasOwnership && (
          <div className="row">
            <div className="col-sm-6">
              <InputField
                label={"Ownership Percentage"}
                data-testid={"ownership-percentage"}
                id={"ownership-percentage"}
                name="OwnershipPercentage"
                icon={<i>%</i>}
                type={"text"}
                placeholder={"Percentage"}
                onChange={(e: ChangeEvent<HTMLInputElement>) => {
                  setOwnershipPercentage(e.target.value);
                  clearErrors("OwnershipPercentage");
                }}
                value={ownershipPercentage}
                register={register}
                rules={{
                  required: "This field is required.",
                  validate: {
                    validity: value => (+value > 0 && +value <=100)  || "Percentage should be between 1 and 100"
                  },
                }}
                maxLength={3}
                errors={errors}
              />
            </div>
          </div>
        )}
</div>
      <div className="p-footer">
        <button
          id="employer-info-next"
          data-testid="employer-info-next"
          className="btn btn-primary"
          type="submit"
          // disabled={true}
          onClick={() => {
            handleSubmit(onSubmit)
            
          }}>
          Next
        </button>
      </div>
      </form>
    </div>
  );
};



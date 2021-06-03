import _ from 'lodash';
import moment from 'moment';
import React, { ChangeEvent, useContext, useEffect, useState } from 'react'
import { Controller, useForm } from 'react-hook-form';
import { EmployerHistoryInfo } from '../../../../../../Entities/Models/EmploymentHistory';
import InputDatepicker from '../../../../../../Shared/Components/InputDatepicker';
import InputField from '../../../../../../Shared/Components/InputField';
import InputRadioBox from '../../../../../../Shared/Components/InputRadioBox';
import { EmploymentHistoryActionTypes } from '../../../../../../store/reducers/EmploymentHistoryReducer';
import { Store } from '../../../../../../store/store';
import { DateDiffInYears } from '../../../../../../Utilities/helpers/DateHelper';
import { NavigationHandler } from '../../../../../../Utilities/Navigation/NavigationHandler';

type initialDataObj = {
  employerName: string, 
  jobTitle: string, 
  startDate: string, 
  endDate: string, 
  yearsInProfession:number,
  employerPhoneNumber: string, 
  employedByFamilyOrParty:boolean, 
  ownershipInterest:string,
  incomeInfoId:number,
  hasOwnershipInterest: boolean
}
export const PreviousEmployment = () => {
    const { state, dispatch } = useContext(Store);
  const [startDate, setStartDate] = useState<any>(null);
  const [endDate, setEndDate] = useState<any>(null);

  const [employerName, setEmployerName] = useState<string>("");
  const [jobTitle, setJobTitle] = useState<string>("");
  const [hasOwnership, setHasOwnership] = useState<boolean>(false);
  const [ownershipPercentage, setOwnershipPercentage] = useState<string>("");
  const { previousEmployerInfo, incomeInformation }: any = state.employmentHistory;
  const {incomeInfo} :any = state.loanManager;


  const {
    register,
    errors,
    handleSubmit,
    getValues,
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

  useEffect(()=>{
    if(previousEmployerInfo){
      const{EmployerName, JobTitle, StartDate, EndDate, YearsInProfession, EmployerPhoneNumber, EmployedByFamilyOrParty, IncomeInfoId, OwnershipInterest, HasOwnershipInterest} = previousEmployerInfo;
      let currnetEmployerInfo = {
        employerName:EmployerName,
        jobTitle:JobTitle,
        startDate: StartDate,
        endDate: EndDate,
        yearsInProfession:+YearsInProfession,
        employerPhoneNumber: EmployerPhoneNumber, 
        employedByFamilyOrParty:EmployedByFamilyOrParty, 
        ownershipInterest:OwnershipInterest,
        incomeInfoId:IncomeInfoId,
        hasOwnershipInterest: HasOwnershipInterest
      }
      setInitialData(currnetEmployerInfo)
    }
    
  },[previousEmployerInfo])

  const setInitialData = (data: initialDataObj) =>{
    if(!_.isEmpty(data)){
      const{employerName, jobTitle, startDate, endDate, hasOwnershipInterest, ownershipInterest} = data;
      let hasOwnership:any = hasOwnershipInterest ? document.querySelector("#ownership-interest-yes"): document.querySelector("#ownership-interest-no")
      if(hasOwnership) hasOwnership.click();
      setEmployerName(employerName);
      setJobTitle(jobTitle);
      setStartDate(new Date(startDate));
      setEndDate(new Date(endDate))
      setValue("startDate",new Date(startDate))
      setValue("endDate",new Date(endDate))
      setHasOwnership(hasOwnershipInterest);
      setOwnershipPercentage(hasOwnershipInterest ? ownershipInterest:"");
      
    }
  }
  
  const onSubmit = () => {
    let employerHistoryInfo: EmployerHistoryInfo = {
      EmployerName:employerName,
      JobTitle:jobTitle,
      StartDate: moment(startDate).format('YYYY-MM-DD'),
      EndDate:moment(endDate).format('YYYY-MM-DD'),
      YearsInProfession: +DateDiffInYears(startDate, endDate),
      EmployerPhoneNumber: "", 
      EmployedByFamilyOrParty:false, 
      OwnershipInterest:+ownershipPercentage === 0 ? null :+ownershipPercentage,
      HasOwnershipInterest: hasOwnership,
      IncomeInfoId:incomeInformation && incomeInformation.incomeId ? incomeInfo.incomeId : null
    }

    // if(!_.isEmpty(employerHistoryInfo))
      dispatch({type: EmploymentHistoryActionTypes.SetPreviousEmployerInfo, payload: employerHistoryInfo});
      NavigationHandler.navigation?.moveNext();
  };

  
  const endDateValidation = (value:Date | null)=>{
    if(value){
      if(value > new Date())
        return "End Date cannot be future date."
      else{
        let {startDate} =getValues();
        if(endDate < startDate )
        return "End Date cannot be less than start date"
      }
    }
    return true;
  }

  const jobTitleOnChangeHandler = (event: ChangeEvent<HTMLInputElement>) => {

    
    const value = event.currentTarget.value;
    if (value.length > 0 && !/^[a-zA-Z0-9%&(.'\-\s]{1,150}$/g.test(value)) {
      return false
    }
    setJobTitle(value)
    clearErrors("JobTitle");
    return null
  };
  return (
    <div>
      <form
        id="previous-employer-info-form"
        data-testid="previous-employer-info-form"
        onSubmit={handleSubmit(onSubmit)}
        autoComplete="off">
          <div className="p-body">         
        <div className="row">
          <div className="col-sm-6">
            <InputField
              label={"Employer or Business Name"}
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
                    handleOnChange={(date: any) => {setStartDate(date); setValue("startDate", date); clearErrors("startDate")}}
                    autoComplete={"off"}
                    selected={startDate}
                    handleOnChangeRaw={(e: ChangeEvent<HTMLInputElement>) => {
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
            {
              <Controller
                control={control}
                name={"endDate"}
                render={() => (
                  <InputDatepicker
                    isPreviousDateAllowed={true}
                    // className={`${errors && 'error'}`}
                    isFutureDateAllowed={false}
                    id={"endDate"}
                    data-testid={"endDate"}
                    label={`End Date`}
                    placeholder={"MM/DD/YYYY"}
                    dateFormat="MM/dd/yyyy"
                    name={"endDate"}
                    handleOnChange={(date: any) => {setEndDate(date); setValue("endDate", date); clearErrors("endDate")}}
                    autoComplete={"off"}
                    selected={endDate}
                    handleOnChangeRaw={(e: ChangeEvent<HTMLInputElement>) => {
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
                    validity: (value) => endDateValidation(value),
                }}}
                defaultValue={""}
              />
            }
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
                errors={errors}
              />
            </div>
          </div>
        )}
        </div>
        <div className="p-footer">
          <button
            id="previous-employer-info-next"
            data-testid="previous-employer-info-next"
            className="btn btn-primary"
            type="submit"
            onChange={handleSubmit(onSubmit)}>
            NEXT
          </button>
        </div>
      </form>
    </div>
  );
}

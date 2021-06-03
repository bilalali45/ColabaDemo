import React, { useState } from 'react';
import PageHead from '../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../Shared/Components/TooltipTitle';

import InputCheckedBox from '../../../../Shared/Components/InputCheckedBox';
import InputRadioBox from '../../../../Shared/Components/InputRadioBox';
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";

import * as yup from "yup";
import InputDatepicker from '../../../../Shared/Components/InputDatepicker';
import Errors from '../../../../Utilities/ErrorMessages';
import { StringServices } from '../../../../Utilities/helpers/StringServices';
import LeftMenuHandler from '../../../../store/actions/LeftMenuHandler';

export function MilitaryServiceForm({ preloadedValues, formOnSubmit, loanManager, isRequired }) {

  let validationSchema = yup.object().shape({
    performedMilitaryService: isRequired ? yup.boolean().required(Errors.MILITARY_SCREEN_ERRORS.SELECT_YES_NO).nullable() : yup.boolean().nullable(),
    performedMilitaryServiceOptional: yup.boolean()
      .nullable(),
    activeDutyPersonnel: yup.boolean().nullable(),
    lastDateOfTourOrService: yup.string(),
    reserveOrNationalGuard: yup.boolean(),
    everActivatedDuringTour: yup.boolean().nullable(),
    veteran: yup.boolean(),
    survivingSpouse: yup.boolean(),
  }).test(
    "MilitaryServicetest",
    null,
    (militaryServiceSchemaTest) => {
      console.info(errors)
      const { performedMilitaryService, activeDutyPersonnel, reserveOrNationalGuard, veteran, survivingSpouse, lastDateOfTourOrService, everActivatedDuringTour } = militaryServiceSchemaTest;
      if (!isRequired && performedMilitaryService == null) {
        return true;
      }
      if (performedMilitaryService || performedMilitaryService == null) {
        if (activeDutyPersonnel || reserveOrNationalGuard || veteran || survivingSpouse) {
          if (activeDutyPersonnel && (/* !/^((0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20)?[0-9]{2})*$/.test(lastDateOfTourOrService) ||  */ !lastDateOfTourOrService)) {
            return new yup.ValidationError(Errors.MILITARY_SCREEN_ERRORS.INVALID_DATE_FORMAT, null, "lastDateOfTourOrService");
          }
          if (reserveOrNationalGuard && (everActivatedDuringTour == null)) {
            return new yup.ValidationError(Errors.MILITARY_SCREEN_ERRORS.SELECT_YES_NO, null, "everActivatedDuringTour");
          }
          return true;
        }
        return new yup.ValidationError(Errors.MILITARY_SCREEN_ERRORS.SELECT_ATLEAST_ONE_SERVICE, null, "serviceTypeErrors");
      }
      else { // when no is selected for army service
        return true;
        //setValue('lastDateOfTourOrService', '');
      }
    }
  );

  function setDateToState(date) {
    setStartDate(date); setValue('lastDateOfTourOrService', date);
    clearErrors("lastDateOfTourOrService");
  }

  const { register, handleSubmit, watch, errors, setValue, control, clearErrors } = useForm({
    resolver: yupResolver(validationSchema),
    defaultValues: preloadedValues,
    mode: "onChange",
    reValidateMode: "onChange",
  });
  const [startDate, setStartDate] = useState<any>(preloadedValues.lastDateOfTourOrService ? new Date(preloadedValues.lastDateOfTourOrService) : null);
  const watchPerformedMilitaryService = watch("performedMilitaryService");
  const watchActiveDutyPersonal = watch("activeDutyPersonnel");
  const watchReserveOrNationalGuard = watch("reserveOrNationalGuard");

  const onChangeHandler = (event?: any, field?: string) => {
    let val = event.target.value;
    if (field === "performedMilitaryService" && val == "true")
      clearErrors("serviceTypeErrors");
    else
      clearErrors(field);

  }
  const getTitle = () => {
    if (loanManager) {
      if (loanManager.loanInfo.ownTypeId === 2) {
        return `We appreciate it. Now we need to learn about ${StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)}'s military service.`
      }
      else {
        return `Thanks ${StringServices.capitalizeFirstLetter(loanManager.primaryBorrowerInfo?.name)}. Now we need to learn about your military service.`
      }
    }
    return "";
  }
  const getTitleForActiveDutyPersonnel = () => {
    if (loanManager) {
      if (loanManager.loanInfo.ownTypeId === 2) {
        return `When is the last date of ${StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)}'s service/tour?`
      }
      else {
        return `When is the last date of your service/tour?`
      }
    }
    return "";
  }
  const getTitleForEverActivated = () => {
    if (loanManager) {
      if (loanManager.loanInfo.ownTypeId === 2) {
        return `Was ${StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)} ever activated during their tour of service?`
      }
      else {
        return `Were you ever activated during your tour of service?`
      }
    }
    return `Were you ever activated during your tour of service?`
  }

  const onSkipStep = () => {
    LeftMenuHandler.moveNext();
  }

  return (
    <div className="compo-abt-yourSelf fadein">
      <PageHead title="Personal Information" handlerBack={() => { }} />
      <TooltipTitle title={getTitle()} />
      <div className="comp-form-panel colaba-form">
        <form onSubmit={handleSubmit(formOnSubmit)} >
          <div className={`form-group  ${loanManager?.loanInfo?.ownTypeId == 1 ? '' : 'reduce'}`}>
            <h4>{(loanManager?.loanInfo?.ownTypeId === 1) ? 'Are you' : 'Is ' + StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)} active-duty personnel, a veteran, or a surviving spouse of the US armed forces?</h4>
            <div className={`step-one ${(loanManager?.loanInfo?.ownTypeId === 2) && 'form-group'}`}>
              <div className="clearfix">
                <InputRadioBox
                data-testid="performedMilitaryService_yes"
                  id="performedMilitaryService"
                  className=""
                  name="performedMilitaryService"
                  value="true"
                  register={register}
                  onChange={(e) => onChangeHandler(e, "performedMilitaryService")}
                >Yes</InputRadioBox>
              </div>
              <div className="clearfix">
                <InputRadioBox
                data-testid="performedMilitaryService_no"
                  id="performedMilitaryService"
                  className="no-margin"
                  name="performedMilitaryService"
                  value="false"
                  register={register}
                  onChange={(e) => onChangeHandler(e, "performedMilitaryService")}
                >No</InputRadioBox>
              </div>
              {errors.performedMilitaryService && <span className="form-error" role="alert" data-testid="performedMilitaryService-error">{errors.performedMilitaryService.message}</span>}
            </div>
          </div>

          {watchPerformedMilitaryService == "true" && (
            <>
              <div className="form-group extend">
                <div className="step-two">
                  <hr />
                  <h4>Please select all that apply.</h4>
                  <InputCheckedBox
                  data-testid="activeDutyPersonnel"
                    name="activeDutyPersonnel"
                    register={register}
                    label={"Active Duty Personnel"}
                    type="checkbox"
                    onChange={(e) => onChangeHandler(e, "serviceTypeErrors")}
                  />
                  {watchActiveDutyPersonal &&
                    <div className="form-group no-margin">
                      <div className="row">
                        <div className="step-three col-md-7">
                          <div className={`intended-feild`}>
                            {<Controller
                              control={control}
                              name="lastDateOfTourOrService"
                              render={() => (
                                <InputDatepicker
                                data-testid="lastDateOfTourOrService"
                                  label={getTitleForActiveDutyPersonnel()}
                                  dateFormat="MM/dd/yyyy"
                                  name="lastDateOfTourOrService"
                                  handleOnChange={setDateToState}
                                  autoComplete={'off'}
                                  selected={startDate}
                                  isPreviousDateAllowed={false}
                                  handleOnChangeRaw={(e) => { e.preventDefault(); }}
                                  errors={errors}
                                />
                              )}
                            />}
                          </div>
                        </div>
                      </div>
                    </div>}
                  <InputCheckedBox
                  data-testid="reserveOrNationalGuard"
                    name="reserveOrNationalGuard"
                    register={register}
                    label={"Reserve Or National Guard"}
                    onChange={(e) => onChangeHandler(e, "serviceTypeErrors")}
                  />
                  {watchReserveOrNationalGuard && <div className="form-group reduce">
                    <div className="step-three">
                      <div className={`intended-feild form-group ${loanManager?.loanInfo?.ownTypeId == 1 && 'reduce_'}`}>
                        <label className="form-label mb-20 mt-30">{getTitleForEverActivated()}</label>
                        <div className="clearfix">
                          <InputRadioBox
                          data-testid="reserveOrNationalGuardYes"
                            id="reserveOrNationalGuardYes"
                            className=""
                            name="everActivatedDuringTour"
                            value="true"
                            register={register}
                          >Yes</InputRadioBox>
                        </div>
                        <div className="clearfix">
                          <InputRadioBox
                            id="reserveOrNationalGuardNo"
                            className="no-margin"
                            name="everActivatedDuringTour"
                            value="false"
                            register={register}
                          >No</InputRadioBox>
                          {errors.everActivatedDuringTour && <span className="form-error pt-2 pb-0" role="alert" data-testid="performedMilitaryService-error">{errors.everActivatedDuringTour.message}</span>}
                        </div>
                      </div>
                    </div>
                  </div>}
                  <InputCheckedBox
                  data-testid="veteran"
                    name="veteran"
                    register={register}
                    label={"Veteran"}
                    onChange={(e) => onChangeHandler(e, "serviceTypeErrors")}
                  />
                  <InputCheckedBox
                  data-testid="survivingSpouse"
                    name="survivingSpouse"
                    register={register}
                    label={"Surviving Spouse"}
                    onChange={(e) => onChangeHandler(e, "serviceTypeErrors")}
                  />
                </div>
                {errors.serviceTypeErrors && <span className="form-error no-padding" role="alert" data-testid="performedMilitaryService-error">{errors.serviceTypeErrors.message}</span>}
              </div>
            </>
          )}
          <button className="btn btn-primary" type="submit" data-testid="military-service-submit" id="military-service-submit">Save & Continue</button>
          {
            !isRequired &&
            <button style={{ marginLeft: 10 }} className="btn btn-primary" onClick={onSkipStep}>Skip</button>
          }

        </form>
      </div>
    </div>
  )
}

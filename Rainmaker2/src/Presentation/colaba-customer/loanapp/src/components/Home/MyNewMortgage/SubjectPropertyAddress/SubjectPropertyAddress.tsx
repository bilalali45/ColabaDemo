import React, { useContext, useEffect, useState } from "react";
import { Controller, useForm } from "react-hook-form";
import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";

import { State, SubjectAddressPropertyReqObj } from "../../../../Entities/Models/types";
import { Store } from "../../../../store/store";
import { LocalDB } from "../../../../lib/LocalDB";
import { ErrorHandler } from '../../../../Utilities/helpers/ErrorHandler'

import HomeAddressFields from '../../../../Shared/Components/HomeAddressFields'
import { HomeAddressCaller } from "../../../../Utilities/Enum";
import InputDatepicker from "../../../../Shared/Components/InputDatepicker";
import moment from "moment";

import MyNewPropertyAddressActions from "../../../../store/actions/MyNewPropertyAddressActions";
import {StringServices} from "../../../../Utilities/helpers/StringServices";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const SubjectPropertyAddress = () => {
  const { register, errors, handleSubmit, getValues, setValue, clearErrors, control, unregister } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  const [toggleSection, setToggleSection] = useState<boolean>(false);
  

  const [initialData, setInitialData] = useState<SubjectAddressPropertyReqObj>();
  const { state, dispatch } = useContext(Store);
  const { loanInfo, states, primaryBorrowerInfo }: any = state.loanManager;
  const [btnClick, setBtnClick] = useState<boolean>(false);
  const [showForm] = useState<boolean>(true);

  const [estClosingDate, setEstClosingDate] = useState<any>(null);

  useEffect(() => {
    if (loanInfo) {
      setCurrentHomeAddress()
    }
  }, [loanInfo])

  const setCurrentHomeAddress = async () => {
    let res: any = await MyNewPropertyAddressActions.getPropertyAddress(+LocalDB.getLoanAppliationId());

    if (res) {
      // console.log(res)
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        setInitialData(res.data)
        let { estimatedClosingDate }: SubjectAddressPropertyReqObj = res.data;
        if (estimatedClosingDate) {
          let estDate = new Date(moment(estimatedClosingDate).format())
          setEstClosingDate(estDate);
          setValue(`estClosingDate`, estDate)
        }

      }
      else {
        ErrorHandler.setError(dispatch, res);
      }
    }
  }
  const onSubmit = async () => {

    if (!btnClick) {
      setBtnClick(true);
      let reqData: SubjectAddressPropertyReqObj = prepareAPIData()

      let res = await MyNewPropertyAddressActions.addOrUpdatePropertyAddress(reqData);
      if (res) {
        if (ErrorHandler.successStatus.includes(res.statusCode)) {
          NavigationHandler.moveNext();
        }
        else {
          ErrorHandler.setError(dispatch, res);


        }
      }
    }
    // onSubmitHandler(data);
  };

  const setFieldValues = async () => {
    
  }


  const prepareAPIData = () => {

    const { street_address, unit, city, zip_code, estClosingDate } = getValues();
    let stateEle = document.querySelector("#state") as HTMLSelectElement;

    let stateObj = states && states?.filter((s: State) => s.name === stateEle.value)[0];
    let dataObj: SubjectAddressPropertyReqObj = {
      loanApplicationId: +LocalDB.getLoanAppliationId(),
      street: street_address,
      unit: unit,
      city: city,
      stateId: stateObj ? stateObj.id : null,
      stateName: stateObj ? stateObj.name : "",
      zipCode: zip_code,
      estimatedClosingDate: moment(estClosingDate).format('YYYY-MM-DD'),
      state: NavigationHandler.getNavigationStateAsString()

    }
    return dataObj;



  }

  const onEstClosingDateSelect = (date) => {

    setEstClosingDate(date)
    setValue("estClosingDate", date);
    clearErrors("estClosingDate")
  }

  const checkFormValidity = () => {

  }


  return (
    <div className="compo-abt-yourSelf fadein">
      <PageHead
        title="Subject Property"
        handlerBack={() => {
        }}
      />
      <TooltipTitle title={`Thanks ${primaryBorrowerInfo && StringServices.capitalizeFirstLetter(primaryBorrowerInfo.name)}. Please tell us about your new home.`} />

      <div className="comp-form-panel colaba-form">
        {/* <div className="form-group">
          <h4>Subject Property Address</h4>
        </div> */}
        <form
          id="current-home-form"
          data-testid="current-home-form"
          className="colaba-form"
          onSubmit={handleSubmit(onSubmit)}>
          <div className="row form-group">

            <HomeAddressFields 
              toggleSection={toggleSection} 
              setToggleSection={setToggleSection} 
              register={register} 
              errors={errors} 
              getValues={getValues} 
              setValue={setValue} 
              clearErrors={clearErrors} 
              setFieldValues={setFieldValues} 
              initialData={initialData} 
              control={control} 
              showForm={showForm} 
              caller={HomeAddressCaller.SubjectProperty} 
              unregister={unregister} 
              checkFormValidity={checkFormValidity} 
              homeAddressLabel={"Subject Property Address"} 
              homeAddressPlaceholder={"Search Home Address"} 
              restrictCountries={true}/>

            {loanInfo && loanInfo.loanGoalId === 4 && 
              <div className={`col-md-6 fadein ${!toggleSection && "d-none"}`}>
              {<Controller
                control={control}
                name={"estClosingDate"}
                render={() => (
                  <InputDatepicker
                    className={`${errors && 'error'}`}
                    id={"estClosingDate"}
                    data-testid={"estClosingDate"}
                    label={"Estimated Closing Date"}
                    placeholder={"MM/DD/YYYY"}
                    dateFormat="MM/dd/yyyy"
                    name={"estClosingDate"}
                    handleOnChange={onEstClosingDateSelect}
                    autoComplete={'off'}
                    selected={estClosingDate}
                    handleOnChangeRaw={(e) => { e.preventDefault(); clearErrors("estClosingDate"); }}
                    errors={errors}
                    ref={register}
                  />
                )}
                errors={errors}
                ref={register}
                rules={{
                  required: "This field is required.",
                  validate: {
                    // validity: value => !(value < new Date()) || "Estimated Closing Date cannot be past date."
                  },
                }}
              />}</div>
              }
          </div>

          <div className="form-footer">
            <button
            data-testid={"subject-address-btn"}
              id={"subject-address-btn"}
              className="btn btn-primary"
              onClick={() => {
                if (errors) setToggleSection(true)
                handleSubmit(onSubmit)
                //  setcurrentStep("maritalStatus");
              }}>
              {"Save & Continue"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

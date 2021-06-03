import React, { useContext, useEffect, useState } from "react";

import InputField from '../../../../Shared/Components/InputField';
import InputDatepicker from '../../../../Shared/Components/InputDatepicker';
import { Borrower, DobSSNReqObj, SSNTabValidation } from "../../../../Entities/Models/types";
import { Controller, useForm } from "react-hook-form";
import GettingToKnowYouActions from "../../../../store/actions/GettingToKnowYouActions";
import { LocalDB } from "../../../../lib/LocalDB";

import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import moment from "moment";
import LeftMenuHandler from "../../../../store/actions/LeftMenuHandler";
import { ssnMasking, unMaskSSN } from "../../../../Utilities/helpers/SSNMasking";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { OwnTypeEnum } from "../../../../Utilities/Enum";
import { TenantConfigFieldNameEnum } from "../../../../Utilities/Enumerations/TenantConfigEnums";
import { Store } from "../../../../store/store";
import { StringServices } from "../../../../Utilities/helpers/StringServices";

type SSNDobProps = {
  borrower: Borrower,
  allBorrowers: Borrower[],
  tabKey: string,
  setTabKey: Function,
  setValidationArray: Function
  validationArray: SSNTabValidation[]

}

export const SSNDobForm = ({ borrower, allBorrowers, setTabKey, setValidationArray, validationArray }: SSNDobProps) => {

  const [dob, setDob] = useState<any>(null);
  const [ssn, setSSN] = useState<string | null>("");
  const [previousDob, setPreviousDob] = useState<any>(null);
  const [previousSSN, setPreviousSSN] = useState<string>("");
  const { dispatch } = useContext(Store);
  const [btnClick, setBtnClick] = useState<boolean>(false);

  const [fieldsConfiguration, setFieldsConfiguration] = useState<any>({
    isDOBVisible: true,
    isDOBRequired: true,
    isSNNVisible: true,
    isSSNRequired: true,
  });
  const protectedSnnString = '***-**-****';

  const { handleSubmit,
    register,
    errors,
    clearErrors,
    control,
    setValue
  } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  useEffect(() => {
    getDobSSN()
  }, [])


  const getDobSSN = async () => {
    let res: any = await GettingToKnowYouActions.getDobSSN(+LocalDB.getLoanAppliationId(), +borrower.id);

    if (res) {
      // console.log(res)
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        // && res.data.ssn
        if (res.data.dobUtc) {

          // ssnMasking(protectedSnnString, true)      
          if (res.data.ssn) {
            setSSN(protectedSnnString);
            setValue(`ssn${borrower.id}`, protectedSnnString)
          }
          setPreviousSSN(res.data.ssn);

          let date = new Date(res.data.dobUtc)

          setValue(`dob${borrower.id}`, date)
          setDob(date)
          setPreviousDob(date);
        }

        // res.data.ssn && 
        if (res.data.dobUtc)
          setValidationArray((prevState) => [
            ...prevState.map((item) => {

              if (item.tabId === borrower.id) {
                return { ...item, validated: true }
              }
              return item;
            }),
          ])
      }
      else {
        ErrorHandler.setError(dispatch, res)

      }
      setFieldsConfiguration({
        isDOBVisible: (borrower.ownTypeId == OwnTypeEnum.PrimaryBorrower ? NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PrimaryBorrowerDOB) : NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.CoBorrowerDOB)),
        isDOBRequired: (borrower.ownTypeId == OwnTypeEnum.PrimaryBorrower ? NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PrimaryBorrowerDOB, true) : NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.CoBorrowerDOB, true)),
        isSNNVisible: (borrower.ownTypeId == OwnTypeEnum.PrimaryBorrower ? NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PrimaryBorrowerSSN) : NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.CoBorrowerSSN)),
        isSNNRequired: (borrower.ownTypeId == OwnTypeEnum.PrimaryBorrower ? NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PrimaryBorrowerSSN, true) : NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.CoBorrowerSSN, true)),
      });
    }
  }

  const setTabValidation = (validated: boolean) => {


    setValidationArray((prevState) => [
      ...prevState.map((item) => {

        if (item.tabId === borrower.id) {
          return { ...item, validated: validated }
        }
        return item;
      }),
    ])

  }


  const moveToNextTab = () => {
    // handleSubmit(onSubmit)

    // event.preventDefault();
    // setTabKey(allBorrowers[currentBorrowerIdx+1].id.toString())
  }

  const onSubmit = async (data) => {

    let dobSSN: DobSSNReqObj = {
      LoanApplicationId: +LocalDB.getLoanAppliationId(),
      BorrowerId: borrower.id,
      DobUtc: null,
      Ssn: null
    }

    if (data[`dob${borrower.id}`] != null && data[`dob${borrower.id}`] != '') {
      dobSSN.DobUtc = moment(data[`dob${borrower.id}`]).format('YYYY-MM-DD');
    }
    if (data[`ssn${borrower.id}`] != null && data[`ssn${borrower.id}`] != protectedSnnString) {
      let ssn = ssnMasking(data[`ssn${borrower.id}`])
      if (ssn) dobSSN.Ssn = ssn;
    }
    else if (data[`ssn${borrower.id}`] == protectedSnnString) {
      dobSSN.Ssn = protectedSnnString;
    }
    else if (previousSSN) {
      let ssn = ssnMasking(previousSSN);
      if (ssn) dobSSN.Ssn = ssn;
    }

    if (previousDob != data[`dob${borrower.id}`] || ssn != protectedSnnString) {
      let res = await GettingToKnowYouActions.addOrUpdateDobSSN(dobSSN);
      if (res) {
        if (!ErrorHandler.successStatus.includes(res.statusCode)) {
          //ErrorHandler.setError(dispatch, res);
          return;
        }
      }
      else {
        return;
      }
    }

    let currentBorrowerTabIdx = allBorrowers.map(function (b) { return b.id; }).indexOf(borrower.id);

    setTabValidation(true)
    let lastTab = checkIfLastTab(currentBorrowerTabIdx)
    if (lastTab) {
      let isAllTabsValid = checkAllTabsValidations();
      if (isAllTabsValid) {
        if (!btnClick)
          setBtnClick(true);
        NavigationHandler.moveNext();
      }

    }

    else {
      setTabKey(allBorrowers[currentBorrowerTabIdx + 1]?.id.toString())
    }
  }

  const onSkipStep = () => {
    let currentBorrowerTabIdx = allBorrowers.map(function (b) { return b.id; }).indexOf(borrower.id);

    setTabValidation(true)
    let lastTab = checkIfLastTab(currentBorrowerTabIdx)
    if (lastTab) {
      let isAllTabsValid = checkAllTabsValidations();
      if (isAllTabsValid) LeftMenuHandler.moveNext();
    }
    else setTabKey(allBorrowers[currentBorrowerTabIdx + 1]?.id.toString())
  }

  const checkIfLastTab = (currentTab: number) => {
    if (allBorrowers.length - 1 === currentTab) return true;
    return false;
  }

  const checkAllTabsValidations = () => {
    let invalidTabs: number[] = [];
    for (let index = 0; index < validationArray.length - 1; index++) {

      const element: SSNTabValidation | undefined = validationArray[index];

      if (element) {
        if (!element.validated) {
          if (element.isDobRequired && !element.hasDobValue) {
            (document.querySelector("#submit" + element.tabId) as HTMLButtonElement).click()
          }
          else if (element.isSSNRequired && !element.hasSSNValue) {
            (document.querySelector("#submit" + element.tabId) as HTMLButtonElement).click()

          }

          invalidTabs.push(element?.tabId)
        }
      }
    }
    if (invalidTabs.length) {
      setTabKey(invalidTabs[0])
      return false;
    }
    return true;
  }
  const onDobSelect = (date) => {

    if (date) setDobValidation(true)
    else setDobValidation(false)
    setDob(date)
    setValue(`dob${borrower?.id}`, date);
    clearErrors("dob" + borrower?.id)


  }

  const setDobValidation = (valid: boolean) => {
    setValidationArray((prevState) => [
      ...prevState.map((item) => {

        if (item.tabId === borrower.id) {
          return { ...item, hasDobValue: valid }
        }
        return item;
      }),
    ])
  }
  const setSSNValidation = (valid: boolean) => {
    setValidationArray((prevState) => [
      ...prevState.map((item) => {

        if (item.tabId === borrower.id) {
          return { ...item, hasSSNValue: valid }
        }
        return item;
      }),
    ])
  }

  const ssnOnChangeHandler = (event) => {
    let value = event.currentTarget.value;
    value = unMaskSSN(value);

    if (isNaN(Number(value))) {
      setSSN("");
      setSSNValidation(false)
      return;
    }
    if (value.length) setSSNValidation(true)
    let ssn = ssnMasking(value)
    setSSN(ssn);
    if (value.length === 0) {
      setSSNValidation(false)
      setTabValidation(false)
    }
    clearErrors("ssn" + borrower?.id)
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)} id={`form-${borrower.id}`}>
      <div className="f-cWrap">
        <div className="row">
          {/* <div className="col-sm-12">
            <div className="form-group">
            <h4>{StringServices.capitalizeFirstLetter(borrower.firstName) + "'s Date of Birth & Social Security Number."}</h4>
            </div>
          </div> */}

          {fieldsConfiguration.isDOBVisible &&

            <div className="col-md-6">
              {
                <Controller
                  control={control}
                  name={`dob${borrower.id}`}
                  render={() => (
                    <InputDatepicker
                      isFutureDateAllowed={false}
                      isPreviousDateAllowed={true}
                      className={`${errors && 'error'}`}
                      id={`dob${borrower.id}`}
                      data-testid={`dob${borrower.id}`}
                      label={StringServices.capitalizeFirstLetter(borrower.firstName) + "'s Date of Birth"}
                      placeholder={"MM/DD/YYYY"}
                      dateFormat="MM/dd/yyyy"
                      name={`dob${borrower.id}`}
                      handleOnChange={onDobSelect}
                      autoComplete={'off'}
                      selected={dob}
                      handleOnChangeRaw={(e) => {
                        e.preventDefault();
                        if (e.target.value.length) {
                          setValidationArray((prevState) => [
                            ...prevState.map((item) => {

                              if (item.tabId === borrower.id) {
                                return { ...item, hasDobValue: true }
                              }
                              return item;
                            }),
                          ])
                        }
                        else {
                          setValidationArray((prevState) => [
                            ...prevState.map((item) => {

                              if (item.tabId === borrower.id) {
                                return { ...item, hasDobValue: false }
                              }
                              return item;
                            }),
                          ])
                        }
                        clearErrors(`dob${borrower?.id}​​​​​​​​`);
                      }}
                      errors={errors}
                      ref={register}
                    />
                  )}
                  errors={errors}
                  ref={register}
                  rules={{
                    required: {
                      value: fieldsConfiguration.isDOBRequired,
                      message: "This field is required."
                    },
                    validate: {
                      validity: value => !(value > new Date()) || "DOB cannot be future date."
                    },
                  }}
                  defaultValue={""}
                />}

            </div>
          }

          {fieldsConfiguration.isSNNVisible &&
            <div className="col-md-6">
              <InputField
                label={StringServices.capitalizeFirstLetter(borrower.firstName) + "'s Social Security Number"}
                data-testid={`ssn${borrower.id}`}
                id={`ssn${borrower.id}`}
                name={`ssn${borrower.id}`}
                type={"text"}
                register={register}
                errors={errors}
                maxLength={11}
                placeholder={"XXX-XX-XXXX"}
                rules={{
                  required: {
                    value: fieldsConfiguration.isSNNRequired,
                    message: "This field is required."
                  },
                  // pattern: {
                  //   value: /^(?!666|000|9\\d{2})\\d{3}-(?!00)\\d{2}-(?!0{4})\\d{4}$/i,
                  //   message: "Please enter a valid SSN.",
                  // },
                  minLength: {
                    value: 11,
                    message: "Invalid SSN.",
                  },
                  maxLength: {
                    value: 11,
                    message: "Max length limit reached.",
                  },
                }}
                onChange={ssnOnChangeHandler}
                value={ssn!}
              />

            </div>
          }
        </div>

        <div className="form-footer">

          <button type="submit" id={"submit" + borrower.id} data-testid={"submit" + borrower.id} className="btn btn-primary" onClick={moveToNextTab}>{'Save & Continue'}</button>

          {

            ((!fieldsConfiguration.isDOBVisible || !fieldsConfiguration.isDOBRequired) &&
              (!fieldsConfiguration.isSNNVisible || !fieldsConfiguration.isSNNRequired)) &&

            <button style={{ marginLeft: 10 }} className="btn btn-primary" onClick={onSkipStep}>Skip</button>
          }

        </div>
      </div>
    </form >
  );
}

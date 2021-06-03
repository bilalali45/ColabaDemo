import { NavigationHandler } from '../../../../../../../Utilities/Navigation/NavigationHandler'
import _ from "lodash";
import React, { ChangeEvent, useContext, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { WayOfIncome } from "../../../../../../../Entities/Models/Employment";
import IconRadioBox from "../../../../../../../Shared/Components/IconRadioBox";
import InputField from "../../../../../../../Shared/Components/InputField";
import { PaidSalaryIcon,PaidHourlyIcon } from "../../../../../../../Shared/Components/SVGs";
import { EmploymentIncomeActionTypes } from "../../../../../../../store/reducers/EmploymentIncomeReducer";
import { Store } from "../../../../../../../store/store";
import { CommaFormatted, removeCommaFormatting } from "../../../../../../../Utilities/helpers/CommaSeparteMasking";
import { ApplicationEnv } from '../../../../../../../lib/appEnv';


export const ModeOfEmploymentIncomePayment = () => {
  const [modeOfPayment, setModeOfPayment] = useState<string>("");
  const { state, dispatch } = useContext(Store);
  const { employerInfo, wayOfIncome }: any = state.employment;
  const [annualBaseSalary, setAnnualBaseSalary] = useState<string>("");
  const [hourlyRate, setHourlyRate] = useState<string>("");
  const [hoursPerWeek, setHoursPerWeek] = useState<string>("");

  const {
    register,
    errors,
    handleSubmit,
    clearErrors,
  } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  useEffect(() => {
    if (employerInfo) {
      if (wayOfIncome) {
        const { EmployerAnnualSalary, HoursPerWeek, HourlyRate, IsPaidByMonthlySalary }:WayOfIncome = wayOfIncome
        let paymentMode: HTMLInputElement  | null= IsPaidByMonthlySalary ? document.querySelector("#inputradio-1") : document.querySelector("#inputradio-2")
        if (paymentMode) paymentMode.click()
        if (EmployerAnnualSalary) setAnnualBaseSalary(EmployerAnnualSalary?.toString());
        if(HoursPerWeek) setHoursPerWeek(HoursPerWeek?.toString());
        if(HourlyRate) setHourlyRate(HourlyRate.toString())
        setModeOfPayment(IsPaidByMonthlySalary ? "salary" : "hourly")

      }
    }
    else {
      NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/IncomeSources`);
    }
  }, [])
  const annualBaseSalaryOnChangeHandler = (event: ChangeEvent<HTMLInputElement>) => {
    clearErrors("annualBaseSalary");
    const value = event.currentTarget.value;
    if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
        return false;
    }
    setAnnualBaseSalary(value.replace(/\,/g, ''))
    return true;
  };

  const hourlyRateOnChangeHandler = (event: ChangeEvent<HTMLInputElement>) => {
    clearErrors("hourlyRate");
    const value = event.currentTarget.value;
    if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
        return false;
    }
    setHourlyRate(value.replace(/\,/g, ''))
    return true;
  };

  const hoursPerWeekOnChangeHandler = (event: ChangeEvent<HTMLInputElement>) => {
    const value = event.target.value;

    if (value.length > 15) return;
    let netAnlIncome = Number(value);
    if (isNaN(netAnlIncome)) return;
    if (isNaN(Number(value.replace(/\,/g, "")))) { setHoursPerWeek(hoursPerWeek); return };
    setHoursPerWeek(value.replace(/\,/g, ""));
    clearErrors("hoursPerWeek");
  };

  const setSubmitData = () => {
    let getPaidBySalary = modeOfPayment === "salary" ? true : false;
    let wayOfIncome: WayOfIncome = {
      IsPaidByMonthlySalary: getPaidBySalary,
      EmployerAnnualSalary: annualBaseSalary ? +removeCommaFormatting(annualBaseSalary) : 0,
      HourlyRate: hourlyRate ? +removeCommaFormatting(hourlyRate) : 0,
      HoursPerWeek: hoursPerWeek ? +hoursPerWeek : 0
    }
    // if (!_.isEmpty(wayOfIncome))
      dispatch({ type: EmploymentIncomeActionTypes.SetWayOfIncome, payload: wayOfIncome });
  };

  const onSubmit = async () => {
    setSubmitData();
    NavigationHandler.navigation?.moveNext();
  }

  const onModeOfPaymentChange = (modeOfPayment: string) => {

    setModeOfPayment(modeOfPayment)
    setAnnualBaseSalary("");
    setHourlyRate("");
    setHoursPerWeek("");
  }

  // const preparePostAPIData = () => {
  //   let currentEmploymentDetails: CurrentEmploymentDetails = {
  //     BorrowerId: +loanInfo.borrowerId,
  //     LoanApplicationId: +LocalDB.getLoanAppliationId(),
  //     EmploymentInfo: employerInfo,
  //     EmployerAddress: employerAddress,
  //     WayOfIncome: wayOfIncome,
  //     State: NavigationHandler.getNavigationStateAsString(),
  //     EmploymentOtherIncomes: null,
  //   };
  //   return currentEmploymentDetails;
  // };

  const Icon = {
    "I Get Paid A Salary" : <PaidSalaryIcon/>,
    "I Get Paid Hourly" : <PaidHourlyIcon/>
  }

  return (
    <React.Fragment>

      <form
        id="net-annual-income-form"
        data-testid="net-annual-income-form"
        onSubmit={handleSubmit(onSubmit)}
        autoComplete="off">
          <div className="p-body">

        <div className="form-group">
        <h4>{`How do you get paid at ${employerInfo && employerInfo.EmployerName
            }?`}</h4>
        </div>

        <div className="row">
          <div className="col-sm-6">
            <IconRadioBox
              id={1}
              data-testid={"mode-of-payment-salary"}
              className=""
              name={`modeOfPayment`}
              title={"I Get Paid A Salary"}
              Icon={Icon["I Get Paid A Salary"]}
              //Icon={IconEmployment}
              handlerClick={() => {
                onModeOfPaymentChange("salary");
              }}
              value={`1`}
            
            />
            {modeOfPayment === "salary" &&
              <div className="row">
                <div className="col-sm-12">
                  <InputField
                    label={"Annual Base Salary"}
                    data-testid={"annual-base-salary"}
                    id={"annual-base-salary"}
                    name="annualBaseSalary"
                    icon={<i className="zmdi zmdi-money"></i>}
                    type={"text"}
                    placeholder={"Amount"}
                    onChange={annualBaseSalaryOnChangeHandler}
                    value={annualBaseSalary? CommaFormatted(annualBaseSalary):""}
                    register={register}
                    rules={{
                      required: "This field is required.",
                      validate: {
                        validity: value => !(value === "0.00")  || "Amount must be greater than 0"
                      },
                    }}
                    errors={errors}
                  />
                </div>
              </div>
            }
          </div>
        </div>
        <div className="row">
          <div className="col-sm-6">
            <IconRadioBox
              id={2}
              data-testid={"mode-of-payment-hourly"}
              name={`modeOfPayment`}
              title={"I Get Paid Hourly"}
              Icon={Icon["I Get Paid Hourly"]}
              handlerClick={() => {
                onModeOfPaymentChange("hourly");
              }}
              value={`2`}
            //   register={register}
            // rules={{
            //   required: "This field is required.",
            // }}
            // errors={errors}
            />

            
          </div>

    
        </div>

        {modeOfPayment === "hourly" &&
              <div className="row">
                <div className="col-sm-6">
                  <InputField
                    label={"Hourly Rate"}
                    data-testid={"hourly-rate"}
                    id={"hourly-rate"}
                    name="hourlyRate"
                    icon={<i className="zmdi zmdi-money"></i>}
                    type={"text"}
                    placeholder={"Amount"}
                    onChange={hourlyRateOnChangeHandler}                    
                    value={hourlyRate? CommaFormatted(hourlyRate):""}
                    register={register}
                    rules={{
                      required: "This field is required.",
                      validate: {
                        validity: value => !(value === "0.00")  || "Amount must be greater than 0"
                      },
                    }}
                    errors={errors}
                  />
                </div>
                <div className="col-sm-6">
                  <InputField
                    label={"Average Hours / Week"}
                    data-testid={"hours-per-week"}
                    id={"hours-per-week"}
                    name="hoursPerWeek"
                    type={"text"}
                    placeholder={"Between 1 and 40 hours"}
                    onChange={hoursPerWeekOnChangeHandler}
                    value={hoursPerWeek}
                    register={register}
                    rules={{
                      required: "This field is required.",
                      validate: {
                        validity: (value) =>
                          (Number(value) > 0 && Number(value) <= 40) ||
                          "Hours must be between 1 and 40",
                      },
                    }}
                    maxLength={2}
                    errors={errors}
                  />
                </div>
              </div>
            }
            {errors.modeOfPayment && <span className="form-error pt-2 pb-0" role="alert" data-testid="modeOfPayment-error">{errors.modeOfPayment.message}</span>}


        </div>
        <div className="p-footer">
          <button className="btn btn-primary" type="submit"
            id="mode-of-payment-nxt"
            data-testid="mode-of-payment-nxt"
            disabled={modeOfPayment ? false : true}
            onClick={handleSubmit(onSubmit)}>
            NEXT
          </button>
        </div>

      </form>
    </React.Fragment>
  );
};

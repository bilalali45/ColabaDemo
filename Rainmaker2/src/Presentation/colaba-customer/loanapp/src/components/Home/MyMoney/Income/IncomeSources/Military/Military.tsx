import React, { useContext, useEffect } from 'react'
import { ApplicationEnv } from '../../../../../../lib/appEnv';
import { Switch } from 'react-router-dom';
import { Store } from '../../../../../../store/store';
import { MilitaryIncome } from './MilitaryIncome/MilitaryIncome';
import { MilitaryServiceLocation } from './MilitaryServiceLocation/MilitaryServiceLocation';
import { ModeOfMilitaryServicePayment } from './ModeOfMilitaryServicePayment/ModeOfMilitaryServicePayment';
import MilitaryIncomeActions from '../../../../../../store/actions/MilitaryIncomeActions';
import { ErrorHandler } from '../../../../../../Utilities/helpers/ErrorHandler';
import { MilitaryIncomeEmployer, MilitaryPaymentMode, ServiceLocationAddressObject } from '../../../../../../Entities/Models/types';
import { MilitaryIncomeActionTypes } from '../../../../../../store/reducers/MilitaryIncomeReducer';
import { IsRouteAllowed } from '../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Military`;

export const Military = () => {

  const { state, dispatch } = useContext(Store);
  const { incomeInfo, loanInfo }: any = state.loanManager;


  useEffect(() => {
    if (incomeInfo && incomeInfo?.incomeId) {
      getMilitaryIncome();
    }
  }, [])

  const getMilitaryIncome = async () => {
    console.log("========> getMilitaryIncome Component")
    let response = await MilitaryIncomeActions.GetMilitaryIncome(loanInfo.borrowerId, incomeInfo?.incomeId);
    if (response) {
      if (ErrorHandler.successStatus.includes(response.statusCode)) {
        dispatchScreensData(response.data);
      } else {
        ErrorHandler.setError(dispatch, response);
      }
    }
  }

  const dispatchScreensData = (data) => {
    console.log("========> dispatchScreensData", data)
    let { employerName, jobTitle, startDate, yearsInProfession, monthlyBaseSalary, militaryEntitlements } = data;
    let { street, unit, city, stateId, zipCode, countryId, countryName, stateName } = data.address;

    let employerInfo: MilitaryIncomeEmployer = {
      EmployerName: employerName,
      JobTitle: jobTitle,
      startDate: startDate,
      YearsInProfession: +yearsInProfession,
    }

    let serviceAddress: ServiceLocationAddressObject = {
      street: street,
      unit,
      city,
      stateId: stateId,
      zipCode: zipCode,
      countryId: countryId,
      countryName: countryName,
      stateName: stateName,

    }
    let modeofPayment: MilitaryPaymentMode = {
      baseSalary: monthlyBaseSalary,
      entitlementL: militaryEntitlements
    }

    dispatch({ type: MilitaryIncomeActionTypes.SetMilitaryEmployer, payload: employerInfo });
    dispatch({ type: MilitaryIncomeActionTypes.SetMilitaryServiceAddress, payload: serviceAddress });
    dispatch({ type: MilitaryIncomeActionTypes.SetMilitaryPaymentMode, payload: modeofPayment });
  }



  return (

    <Switch>
      <IsRouteAllowed path={`${containerPath}/MilitaryIncome`} component={MilitaryIncome} />
      <IsRouteAllowed path={`${containerPath}/MilitaryServiceLocation`} component={MilitaryServiceLocation} />
      <IsRouteAllowed path={`${containerPath}/ModeOfMilitaryServicePayment`} component={ModeOfMilitaryServicePayment} />
    </Switch>

  )
}

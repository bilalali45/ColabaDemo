import React, { useContext, useEffect } from 'react'
import { Switch } from 'react-router-dom'
import { Store } from '../../../../../../store/store'
import { ApplicationEnv } from '../../../../../../lib/appEnv'
import { EmployerAddress } from './EmployerAddress/EmployerAddress'
import { EmploymentIncome } from './EmploymentIncome/EmploymentIncome'
import { ModeOfEmploymentIncomePayment } from './ModeOfEmploymentIncomePayment/ModeOfEmploymentIncomePayment'
import { AdditionalIncome } from './AdditionalIncome/AdditionalIncome'

import EmploymentActions from '../../../../../../store/actions/EmploymentActions'
import { LocalDB } from '../../../../../../lib/LocalDB'
import { ErrorHandler } from '../../../../../../Utilities/helpers/ErrorHandler'
import { EmploymentIncomeActionTypes } from '../../../../../../store/reducers/EmploymentIncomeReducer'
import { EmployerInfo, WayOfIncome } from '../../../../../../Entities/Models/Employment'
import { IsRouteAllowed } from '../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed'


const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Employment`;


export const Employment = () => {

  const { state, dispatch } = useContext(Store);

  const { incomeInfo, loanInfo }: any = state.loanManager;
  const { employerInfo }: any = state.employment;


  useEffect(() => {
    if (!employerInfo) {
      if (incomeInfo && incomeInfo?.incomeId) {
        getEmploymentIncomeDetails()
      }
    }

  }, [])

  const getEmploymentIncomeDetails = async () => {
    let res: any = await EmploymentActions.getEmployerInfo(+LocalDB.getLoanAppliationId(), +loanInfo.borrowerId, +incomeInfo.incomeId);

    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        console.log(res.data)
        const { employerAddress, employmentInfo, employmentOtherIncomes, wayOfIncome } = res.data;
        setEmployerInfo(employmentInfo)
        dispatch({ type: EmploymentIncomeActionTypes.SetEmployerAddress, payload: employerAddress });
        setWayOfPayment(wayOfIncome)
        dispatch({ type: EmploymentIncomeActionTypes.SetAdditionIncome, payload: employmentOtherIncomes });
      }
      else {
        ErrorHandler.setError(dispatch, res);
      }
    }
  }

  const setEmployerInfo = (employmentInfo) => {
    let { employerName, jobTitle, startDate, yearsInProfession, employedByFamilyOrParty, employerPhoneNumber, ownershipInterest, hasOwnershipInterest } = employmentInfo;
    let employmentDetails: EmployerInfo = {
      EmployerName: employerName,
      JobTitle: jobTitle,
      StartDate: startDate,
      EmployedByFamilyOrParty: employedByFamilyOrParty,
      EmployerPhoneNumber: employerPhoneNumber,
      YearsInProfession: yearsInProfession,
      IncomeInfoId: incomeInfo.incomeId,
      HasOwnershipInterest: hasOwnershipInterest,
      OwnershipInterest: ownershipInterest
    }
    dispatch({ type: EmploymentIncomeActionTypes.SetEmployerInfo, payload: employmentDetails });
  }

  const setWayOfPayment = (wayOfIncome) => {
    const { employerAnnualSalary, hoursPerWeek, hourlyRate, isPaidByMonthlySalary } = wayOfIncome;
    let wayOfIncomeDetails: WayOfIncome = {
      IsPaidByMonthlySalary: isPaidByMonthlySalary,
      HourlyRate: hourlyRate,
      HoursPerWeek: hoursPerWeek,
      EmployerAnnualSalary: employerAnnualSalary
    }
    dispatch({ type: EmploymentIncomeActionTypes.SetWayOfIncome, payload: wayOfIncomeDetails });
  }
  return (
    <div data-testid="employment-div">
      <Switch>
        <IsRouteAllowed path={`${containerPath}/EmploymentIncome`} component={EmploymentIncome} />
        <IsRouteAllowed path={`${containerPath}/EmployerAddress`} component={EmployerAddress} />
        <IsRouteAllowed path={`${containerPath}/ModeOfEmploymentIncomePayment`} component={ModeOfEmploymentIncomePayment} />
        <IsRouteAllowed path={`${containerPath}/AdditionalIncome`} component={AdditionalIncome} />
      </Switch>
    </div>
  )
}

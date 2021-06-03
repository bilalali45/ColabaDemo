import React, { useContext, useEffect, useState } from 'react'
import moment from "moment";
import { MilitaryIncomeInfo, ServiceLocationAddressObject } from '../../../../../../../Entities/Models/types'
import { LocalDB } from '../../../../../../../lib/LocalDB'
import { Store } from '../../../../../../../store/store'
import { ModeOfMilitaryServicePaymentWeb } from './ModeOfMilitaryServicePayment_Web'
import MilitaryIncomeActions from '../../../../../../../store/actions/MilitaryIncomeActions';
import { ErrorHandler } from '../../../../../../../Utilities/helpers/ErrorHandler';
import { MilitaryIncomeActionTypes } from '../../../../../../../store/reducers/MilitaryIncomeReducer';
import { LoanApplicationActionsType } from '../../../../../../../store/reducers/LoanApplicationReducer';
import { NavigationHandler } from '../../../../../../../Utilities/Navigation/NavigationHandler';
import { ApplicationEnv } from '../../../../../../../lib/appEnv';


export const ModeOfMilitaryServicePayment = () => {

    const { state, dispatch } = useContext(Store);
    const { militaryEmployer, militaryServiceAddress, militaryPaymentMode }: any = state.militaryIncomeManager;
    const { incomeInfo, loanInfo }: any = state.loanManager;
    const [baseSalary, setBaseSalary] = useState<string>('');
    const [entitlements, setEntitlements] = useState<string>('');
    const [enableBtn, setEnableBtn] = useState<boolean>(false);
    const [btnClick, setBtnClick] = useState<boolean>(false);

    useEffect(() => {
        if (militaryPaymentMode) {
            setBaseSalary(militaryPaymentMode.baseSalary);
            setEntitlements(militaryPaymentMode.entitlementL);
            setEnableBtn(true);
        }
    }, [militaryPaymentMode])

    useEffect(() => {
        if (!militaryServiceAddress) NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/IncomeSources`);
    }, [])



    const saveOrUpdateMilitaryIncome = async (data) => {
        if (!btnClick) {
            setBtnClick(true);

            const { street, unit, city, stateId, zipCode, countryId, countryName, stateName } = militaryServiceAddress;
            const { EmployerName, JobTitle, startDate, YearsInProfession } = militaryEmployer;
            let address: ServiceLocationAddressObject = {
                street,
                unit,
                city,
                stateId,
                zipCode,
                countryId,
                countryName,
                stateName

            }

            let militaryObj: MilitaryIncomeInfo = {
                loanApplicationId: +LocalDB.getLoanAppliationId(),
                id: incomeInfo?.incomeId ? incomeInfo?.incomeId : null,
                borrowerId: loanInfo.borrowerId,
                employerName: EmployerName,
                jobTitle: JobTitle,
                startDate: moment(startDate).format('YYYY-MM-DD'),
                yearsInProfession: YearsInProfession,
                address: address,
                monthlyBaseSalary: Number(data.Salary.replace(/\,/g, '')),
                militaryEntitlements: Number(data.Entitlements.replace(/\,/g, '')),
                state: NavigationHandler.getNavigationStateAsString()
            }
            let response = await MilitaryIncomeActions.AddOrUpdateMilitaryIncome(militaryObj);
            if (response) {
                if (ErrorHandler.successStatus.includes(response.statusCode)) {

                    dispatch({ type: MilitaryIncomeActionTypes.SetMilitaryServiceAddress, payload: undefined });
                    dispatch({ type: MilitaryIncomeActionTypes.SetMilitaryEmployer, payload: undefined });
                    dispatch({ type: MilitaryIncomeActionTypes.SetMilitaryPaymentMode, payload: undefined });
                    dispatch({ type: LoanApplicationActionsType.SetIncomeInfo, payload: undefined });
                    LocalDB.clearIncomeFromStorage();
                    NavigationHandler.moveNext();
                } else {
                    ErrorHandler.setError(dispatch, response);
                    setBtnClick(false);
                }
            }
        }  
}

    return (
        <ModeOfMilitaryServicePaymentWeb
            baseSalary={baseSalary}
            setBaseSalary={setBaseSalary}
            entitlements={entitlements}
            setEntitlements={setEntitlements}
            enableBtn={enableBtn}
            setEnableBtn={setEnableBtn}
            saveOrUpdateMilitaryIncome={saveOrUpdateMilitaryIncome}
            countryName={militaryServiceAddress?.countryName}
            EmployerName={militaryEmployer?.EmployerName}
        />
    )
}

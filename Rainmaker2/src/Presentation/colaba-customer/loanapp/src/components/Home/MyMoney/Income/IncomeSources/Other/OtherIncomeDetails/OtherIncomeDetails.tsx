import React, { useContext, useEffect, useState } from 'react'
import { FieldsType, OtherIncomeSubmitDataType, OtherIncomeType, SelectedOtherIncomeType } from '../../../../../../../Entities/Models/types';
import { LocalDB } from '../../../../../../../lib/LocalDB';
import IncomeActions from '../../../../../../../store/actions/IncomeActions';
import { Store } from '../../../../../../../store/store';
import { ErrorHandler } from '../../../../../../../Utilities/helpers/ErrorHandler';
import { NavigationHandler } from '../../../../../../../Utilities/Navigation/NavigationHandler';
import { OtherIncomeDetailsWeb } from './OtherIncomeDetails_Web'

export const OtherIncomeDetails = () => {

    const { state, dispatch } = useContext(Store);
    const { selectedOtherIncome }: any = state.otherIncomeManager;
    const { incomeInfo, loanInfo }: any = state.loanManager;

    const [fieldNames, setFieldNames] = useState<string[]>([]);
    const [incomeTypeId, setIncomeTypeId] = useState<number | null>(null);
    const [monthlyBaseIncome, setMonthlyBaseIncome] = useState<string | null>("");
    const [annualBaseIncome, setannualBaseIncome] = useState<string | null>('');
    const [description, setDescription] = useState<string | null>(null);
    const [fieldsInfo, setFieldsInfo] = useState<string>();
    const [maxLength, setMaxLength] = useState<number>(0);
    const [enableBtn, setEnableBtn] = useState<boolean>(false);
    const [btnClick, setBtnClick] = useState<boolean>(false);

    useEffect(() => {
        if(selectedOtherIncome){
            prepareDataForPopulate(selectedOtherIncome);
        }      
    }, [selectedOtherIncome])

    const prepareDataForPopulate = (selectedItem: SelectedOtherIncomeType) => {
        console.log("==========> prepareDataForPopulate", selectedItem)
        const fields = JSON.parse(selectedItem.fieldsInfo).fieldsInfo;

        //Preparing Data for Elements
        setFieldNames(fields.filter((x: FieldsType) => x.Enabled).map((x: FieldsType) => x.name));
        setIncomeTypeId(selectedItem.id);
        setFieldsInfo(selectedItem.name);
        setMaxLength(fields.find((x: FieldsType) => x.name === "description").maxLendth);
        
        //Setting default values
        setMonthlyBaseIncome(selectedItem.monthlySalary);
        setannualBaseIncome(selectedItem.annualSalary);
        setDescription(selectedItem.description);
    }

    const submit = async (data: OtherIncomeSubmitDataType) => {
        if (!btnClick) {
            setBtnClick(true);

            let monthlySalary: string | null = null, annualSalary: string | null = null, description: string | null | undefined = null;
            if (data.hasOwnProperty('description')) description = data?.description;
            if (data.hasOwnProperty('monthlyBaseIncome')) monthlySalary = String(data?.monthlyBaseIncome)
            if (data.hasOwnProperty('annualBaseIncome')) annualSalary = String(data?.annualBaseIncome);

            let postData: OtherIncomeType = {
                LoanApplicationId: +(LocalDB.getLoanAppliationId()),
                IncomeInfoId: incomeInfo?.incomeId ? incomeInfo?.incomeId : null,
                BorrowerId: loanInfo.borrowerId,
                MonthlyBaseIncome: monthlySalary ? Number(monthlySalary.replace(/\,/g, '')) : null,
                AnnualBaseIncome: annualSalary ? Number(annualSalary.replace(/\,/g, '')) : null,
                Description: description,
                IncomeTypeId: incomeTypeId,
                State: NavigationHandler.getNavigationStateAsString()
            }

            let response = await IncomeActions.AddOrUpdateOtherIncome(postData);
            if (response) {
                if (ErrorHandler.successStatus.includes(response.statusCode)) {
                    NavigationHandler.navigation?.moveNext();
                }else{
                    ErrorHandler.setError(dispatch, response);
                    setBtnClick(false);
                }
            }          
        }
    }

    return (
        <OtherIncomeDetailsWeb
            fieldNames={fieldNames}
            monthlyBaseIncome={monthlyBaseIncome}
            annualBaseIncome={annualBaseIncome}
            description={description}
            fieldsInfo={fieldsInfo}
            maxLength={maxLength}
            submit={submit}
            enableBtn={enableBtn}
            setEnableBtn={setEnableBtn}
            setMonthlyBaseIncome={setMonthlyBaseIncome}
            setannualBaseIncome={setannualBaseIncome}
            setDescription = {setDescription}
        />
    )
}
                
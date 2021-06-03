import React, { useState, useEffect, useContext } from 'react';
import { useForm } from "react-hook-form";

import { SocialSecurityIcon, PensionIcon, IRAIcon, OtherIncomeIcon } from '../../../../../../../Shared/Components/SVGs';
import IconRadioSnipit2 from '../../../../../../../Shared/Components/IconRadioSnipit2';
import InputField from '../../../../../../../Shared/Components/InputField';
import RetirementIncomeActions from '../../../../../../../store/actions/RetirementIncomeActions';
import { IncomeInfo, LoanInfoType, RetirementIncomeInfo } from '../../../../../../../Entities/Models/types';
import { ErrorHandler } from "../../../../../../../Utilities/helpers/ErrorHandler";
import { Store } from '../../../../../../../store/store';
import { LocalDB } from '../../../../../../../lib/LocalDB';
import { NavigationHandler } from '../../../../../../../Utilities/Navigation/NavigationHandler';
import { CommaFormatted } from '../../../../../../../Utilities/helpers/CommaSeparteMasking';
import { CommonType } from '../../../../../../../store/reducers/CommonReducer';

type salaryObj = {
    id: number,
    salary: string,
    employeeName: string
}
export const RetirementIncomeSource = () => {
    const {
        register,
        errors,
        handleSubmit,
    } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    const { state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;
    const incomeInfo: IncomeInfo = loanManager.incomeInfo;
    const [incomeTypes, setIncomeTypes] = useState<any[]>([]);
    const [incomeTypeId, setIncomeTypeId] = useState<number | null>(null);
    const [monthlyCaption, setMonthlyCaption] = useState<string | null>('Monthly Income');
    const [description, setDescription] = useState<string | null>(null);
    const [fieldNames, setFieldNames] = useState<string[]>([]);
    const [salaryValues, setSalaryValues] = useState<salaryObj[]>([])

    const [btnClick, setBtnClick] = useState<boolean>(false);


    useEffect(() => {
        getAllIncomeTypes();
    }, [])

    useEffect(() => {
        getRetirementIncomeInfo();
    }, [incomeTypes])

    const incomeTypeIcons = {
        6: <SocialSecurityIcon />,
        7: <PensionIcon />,
        8: <IRAIcon />,
        9: <OtherIncomeIcon />
    }

    const getAllIncomeTypes = async () => {
        const response = await RetirementIncomeActions.GetRetirementIncomeTypes();
        if (response) {
            setIncomeTypes(response.data);
            // var sValues = [];
           
            response.data.forEach(element => {
                const sal : salaryObj = {
                    id:element.id,
                    salary: '',
                    employeeName: ''
                }
                salaryValues.push(sal)
            });
            // setSalaryValues(sValues);
        }

    }

    const setSalary = (id: number | null, salary: string, employeeName: string) => {
        if (!id)
            return null;
        var index = salaryValues.findIndex(x => x.id === id);

        let g = salaryValues[index]
        if (g) {
            if (salary)
                g.salary = salary;
            if (employeeName)
                g.employeeName = employeeName;

            let copy = [...salaryValues];
            copy[index] = g;
            setSalaryValues(copy);
        }
    }

    const getSalary = (id: number) => {
        if (!id)
            return null;
        var index = salaryValues.findIndex(x => x.id === id);

        return salaryValues[index]
    }

    const getRetirementIncomeInfo = async () => {
        if (!incomeInfo?.incomeId || !loanInfo.borrowerId || incomeTypes?.length === 0) {
            return;
        }

        const response = await RetirementIncomeActions.GetRetirementIncomeInfo(incomeInfo?.incomeId, loanInfo.borrowerId);

        if (response) {
            handleIncomeTypeChange(response.data.incomeTypeId);
            setSalary(response.data.incomeTypeId, response.data.monthlyBaseIncome, response.data.employerName)
            setDescription(response.data.description);
        }
    }

    const addOrUpdateRetirementIncomeInfo = async (data) => {
        if (!btnClick) {
            setBtnClick(true);
            let obj = getSalary(incomeTypeId);
            let model: RetirementIncomeInfo = {
                incomeInfoId: incomeInfo?.incomeId ?? null,
                loanApplicationId: +LocalDB.getLoanAppliationId(),
                borrowerId: Number(loanInfo?.borrowerId),
                incomeTypeId: incomeTypeId,
                employerName: obj ? obj["employeeName"] : undefined,
                monthlyBaseIncome: Number(obj["salary"].toString().replace(/\,/g, '')),
                description: data.description,
                state: NavigationHandler.getNavigationStateAsString(),
            };

            const res = await RetirementIncomeActions.AddOrUpdateRetirementIncomeInfo(model);

            if (res) {
                if (ErrorHandler.successStatus.includes(res.statusCode)) {
                    NavigationHandler.moveNext();
                }
                else {
                    ErrorHandler.setError(dispatch, res);
                    setBtnClick(false);
                }
            }
        }
    }

    const handleIncomeTypeChange = async (id) => {
        if (!incomeTypes || incomeTypes.length == 0) {
            return;
        }

        const incomeType = incomeTypes.find(x => x.id === id);
        // let title = `${StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)}'s ${StringServices.capitalizeFirstLetter(incomeType?.name)} Income`;
        await dispatch({ type: CommonType.SetIncomePopupTitle, payload: incomeType?.name });

        const fields = JSON.parse(incomeType.fieldsInfo).fieldsInfo;

        setFieldNames(fields.filter(x => x.Enabled).map(x => x.name));
        setMonthlyCaption(fields.filter(x => x.Enabled && x.name == 'monthlyBaseIncome')[0]?.caption);
        setIncomeTypeId(id);
    }

    return (
        <section>
            <div className="row form-group">
                <div className="col-sm-12">
                    <h4>Where is your retirement income coming from?</h4>
                </div>
            </div>
            <form
                id="employer-info-form"
                data-testid="employer-info-form"
                onSubmit={handleSubmit(addOrUpdateRetirementIncomeInfo)}
                autoComplete="off">
                <div className="p-body">
                    <div className="row form-group">
                        {
                            incomeTypes.map(i =>
                                <div className="col-sm-6">
                                    <IconRadioSnipit2
                                        dataTestId={"income-type-" + i.id}
                                        id={i.id}
                                        title={i.name}
                                        icon={incomeTypeIcons[i.id]}
                                        handlerClick={handleIncomeTypeChange}
                                        className={i.id === incomeTypeId ? "active" : ""}
                                    />
                                </div>)
                        }
                    </div>
                    <div className="row form-group">
                        {
                            fieldNames.includes("employerName") &&
                            <div className="col-sm-6">
                                <InputField
                                    label={"Employer Name"}
                                    data-testid="employer-name"
                                    id={"employer-name"}
                                    name={"employerName"}
                                    value={getSalary(incomeTypeId)?.employeeName}
                                    onChange={(e) => setSalary(incomeTypeId, null, e.target.value)}

                                    type={'text'}
                                    placeholder={"Employer Name here"}
                                    register={register}
                                    rules={{
                                        required: "This field is required.",
                                    }}
                                    errors={errors}
                                />
                            </div>
                        }
                        {
                            fieldNames.includes("description") &&
                            <div className="col-sm-6">
                                <InputField
                                    label={"Description"}
                                    data-testid="description-value"
                                    id={"description"}
                                    name={"description"}
                                    value={description}
                                    onChange={(e) => setDescription(e.target.value)}
                                    type={'text'}
                                    placeholder={"Retirement Income Description"}
                                    register={register}
                                    errors={errors}
                                />
                            </div>
                        }
                        {
                            fieldNames.includes("monthlyBaseIncome") &&
                            <div className="col-sm-6">
                                <InputField
                                    label={monthlyCaption}
                                    data-testid="monthly-base-income"
                                    id={"monthly-base-income"}
                                    name={"monthlyBaseIncome"}
                                    value={CommaFormatted(getSalary(incomeTypeId)?.salary)}
                                    onChange={(e) => {
                                        let value = e.target.value;
                                        if (value.length > 0 && !/^[0-9,]{1,11}$/g.test(value)) {
                                            return false;
                                        }
                                        setSalary(incomeTypeId, value.replace(/\,/g, ''), null)
                                        return true;
                                    }}
                                    type={'text'}
                                    icon={<i className="zmdi zmdi-money"></i>}
                                    placeholder={"Amount"}
                                    register={register}
                                    rules={{
                                        required: "This field is required.",
                                    }}
                                    errors={errors}
                                />
                            </div>
                        }
                        
                        

                    </div>
                </div>
            </form>
            <div className="p-footer">
                <button className={`btn btn-lg btn-primary float-right`} onClick={handleSubmit(addOrUpdateRetirementIncomeInfo)} disabled={incomeTypeId ? false : true}>Save Income Source</button>
            </div>
        </section>
    )
}

import React from 'react';
import { useForm } from "react-hook-form";
import InputField from '../../../../../../../Shared/Components/InputField';
import {OtherIncomeSubmitDataType} from '../../../../../../../Entities/Models/types';
import { CommaFormatted } from '../../../../../../../Utilities/helpers/CommaSeparteMasking';

type props = {
    fieldNames: string[];
    monthlyBaseIncome?: string | null;
    annualBaseIncome?: string | null;
    description?: string | null;
    fieldsInfo?: string;
    maxLength: number;
    submit: (data: OtherIncomeSubmitDataType) => void;
    setEnableBtn: Function;
    enableBtn: boolean;
    setannualBaseIncome: Function;
    setMonthlyBaseIncome: Function;
    setDescription: Function;
}

export const OtherIncomeDetailsWeb = ({ setMonthlyBaseIncome, setannualBaseIncome, fieldNames, monthlyBaseIncome, annualBaseIncome, description, fieldsInfo, maxLength, submit, setDescription }: props) => {
    const {
        register,
        errors,
        handleSubmit,
        clearErrors,
    } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "firstError",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    return (
        <section>
            <div className="row form-group">
                <div className="col-sm-12">
                    <span>{fieldsInfo}</span>
                </div>
            </div>
            <form
                id="otherIncome-info-form"
                data-testid="otherIncome-info-form"
            >
                <div className="p-body">

                <div className="row">
                    {
                        fieldNames.includes("description") &&
                        <div className="col-sm-6">
                            <InputField
                                label={"Description"}
                                data-testid="description"
                                id={"description"}
                                name={"description"}
                                value={description ? description : ''}
                                type={'text'}
                                placeholder={"Description info"}
                                register={register}
                                errors={errors}
                                maxLength={maxLength}
                                rules={{
                                        required: "This field is required.",
                                    }}
                                    onChange= {(event:React.FormEvent<HTMLInputElement>) => {
                                        clearErrors("description");
                                        const value = event.currentTarget.value;
                                        setDescription(value);
                                    }}
                            />
                        </div>
                    }

                    {
                        fieldNames.includes("monthlyBaseIncome") &&
                        <div className="col-sm-6">
                            <InputField
                                label={"Monthly Income"}
                                data-testid="monthlyBaseIncome"
                                id={"monthlyBaseIncome"}
                                name={"monthlyBaseIncome"}
                                icon={<i className="zmdi zmdi-money"></i>}
                                value={monthlyBaseIncome ? CommaFormatted(monthlyBaseIncome) : ''}
                                type={'text'}
                                placeholder={"Amount"}
                                register={register}
                                rules={{
                                    required: "This field is required.",
                                }}
                                errors={errors}
                                onChange={(event: React.FormEvent<HTMLInputElement>) => {
                                    clearErrors("monthlyBaseIncome")
                                    const value = event.currentTarget.value;
                                    if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
                                        return false;
                                    }
                                    setMonthlyBaseIncome(value.replace(/\,/g, ''));
                                    return true;
                                }}
                            />
                        </div>
                    }
                    {
                        fieldNames.includes("annualBaseIncome") &&
                        <div className="col-sm-6">
                            <InputField
                                label={"Annual Income"}
                                data-testid="annualBaseIncome"
                                id={"annualBaseIncome"}
                                name={"annualBaseIncome"}
                                icon={<i className="zmdi zmdi-money"></i>}
                                value={annualBaseIncome ? CommaFormatted(annualBaseIncome) : ''}
                                type={'text'}
                                placeholder={"Amount"}
                                register={register}
                                rules={{
                                    required: "This field is required.",
                                }}
                                errors={errors}
                                onChange= {(event: React.FormEvent<HTMLInputElement>) => {
                                    clearErrors("annualBaseIncome")
                                    const value = event.currentTarget.value;
                                    if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
                                        return false;
                                    }
                                    setannualBaseIncome(value.replace(/\,/g, ''))
                                    return true;
                                }}
                            />
                        </div>
                    }

                </div>
            
                </div>
                <div className="p-footer">
                <button
                    className="btn btn-primary"
                    type="button"
                  
                    onClick={handleSubmit(submit)}
                >
                    Save Income Source
                </button>
            </div>
            </form>

           
        </section>

    )
}

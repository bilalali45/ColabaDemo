import React from 'react';
import { useForm } from "react-hook-form";

import InputField from '../../../../../../../Shared/Components/InputField';
import { CommaFormatted } from '../../../../../../../Utilities/helpers/CommaSeparteMasking';

type props = {
    baseSalary?: string;
    setBaseSalary: Function;
    entitlements?: string;
    setEntitlements?: Function;
    enableBtn: boolean;
    setEnableBtn: Function;
    saveOrUpdateMilitaryIncome: (data) => void,
    countryName: string;
    EmployerName?: string;
}


export const ModeOfMilitaryServicePaymentWeb = ({ baseSalary, EmployerName, setBaseSalary, entitlements, setEntitlements, enableBtn, countryName, setEnableBtn, saveOrUpdateMilitaryIncome }: props) => {
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

    // const onSubmit = (data) => {
    //     saveOrUpdateMilitaryIncome(data);
    // }

    return (
        <section>
            <form
                id="military-info-form"
                data-testid="military-info-form"
                autoComplete="off">
                <div className="p-body">
                    <div data-testid="mode-quest" className="form-group">
                        <h4>{`How do you get paid at ${EmployerName} ${countryName}?`}</h4>
                    </div>
                    <div className="row">
                        <div className="col-sm-6">
                            <InputField
                                label={"Monthly Base Salary"}
                                data-testid="monthyBaseSalary"
                                id=""
                                name="Salary"
                                icon={<i className="zmdi zmdi-money"></i>}
                                type={"text"}
                                placeholder={"Amount"}
                                value={CommaFormatted(baseSalary)}
                                register={register}
                                rules={{
                                    required: "This field is required.",
                                }}
                                maxLength={100}
                                errors={errors}
                                onChange={(event) => {
                                    clearErrors("Salary");
                                    let value = event.target.value;

                                    if (value.length > 0 && !/^[0-9,]{1,11}$/g.test(value)) {
                                        return false;
                                    }
                                    setBaseSalary(value.replace(/\,/g, ''));
                                    if (value != "" && entitlements != "") {
                                        setEnableBtn(true);
                                    }
                                    return true;
                                }}
                            />
                        </div>
                        <div className="col-sm-6">
                            <InputField
                                label={"Military Entitlements"}
                                data-testid="entitlement"
                                id=""
                                name="Entitlements"
                                icon={<i className="zmdi zmdi-money"></i>}
                                type={"text"}
                                placeholder={"Monthly Amount"}
                                value={CommaFormatted(entitlements)}
                                register={register}
                                rules={{
                                    required: "This field is required.",
                                }}
                                maxLength={100}
                                errors={errors}
                                onChange={(event) => {
                                    clearErrors("Amount");
                                    let value = event.target.value;
                                    if (value.length > 0 && !/^[0-9,]{1,11}$/g.test(value)) {
                                        return false;
                                    }
                                    setEntitlements && setEntitlements(value.replace(/\,/g, ''));
                                    if (value != "" && baseSalary != "") {
                                        setEnableBtn(true);
                                    }

                                    return true
                                }}


                            />
                        </div>
                    </div>
                </div><div className="p-footer">
                    <button
                        data-testid="modeOfPayement"
                        className="btn btn-primary"
                        type="button"
                        disabled={!enableBtn}
                        onClick={handleSubmit(saveOrUpdateMilitaryIncome)}
                    >
                        Save Income Source
    </button>
                </div>
            </form>
        </section>
    )
}

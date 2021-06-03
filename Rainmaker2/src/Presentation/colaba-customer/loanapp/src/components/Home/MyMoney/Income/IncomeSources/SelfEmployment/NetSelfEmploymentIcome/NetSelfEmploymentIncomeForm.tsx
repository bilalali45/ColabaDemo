import React, { useEffect, useState } from 'react'
import InputField from '../../../../../../../Shared/Components/InputField'
import { useForm } from 'react-hook-form';
import { CommaFormatted, removeCommaFormatting } from '../../../../../../../Utilities/helpers/CommaSeparteMasking';

export const NetSelfEmploymentIncomeForm = ({ onSubmit, selfIncome = { jobTitle: '' } }) => {

    const label = 'annualIncome';
    const placeholder = 'Amount';
    const title = 'Net Annual Income';

    const [annualIncome, setAnnualIncome] = useState('')
    const [btnClick, setBtnClick] = useState<boolean>(false);

    const {
        register,
        handleSubmit,
        watch,
        errors,
        getValues,
        setValue,
        clearErrors,
    } = useForm({
        mode: "onChange",
        reValidateMode: "onSubmit",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    useEffect(() => {
        register(label, {
            validate: (value) => {
                if (!value) {
                    return `${title} is required.`
                }
                return true;
            },
        });

    }, [register]);

    useEffect(() => {
        watch(label)
    }, []);

    useEffect(() => {
        clearErrors();
    }, [getValues(label)]);

    useEffect(() => {
        if (selfIncome) {
            setAnnualIncome(CommaFormatted(selfIncome[label]))
            setValue(label, selfIncome[label]);
        }
    }, [selfIncome['jobTitle']])



    const submit = handleSubmit((data) => {
        if (!btnClick) {
            setBtnClick(true);
            onSubmit(data)
        }
    });

    return (
        <form onSubmit={submit}>
            <div className="p-body">
                <div className="row">
                    <div className="col-sm-6">
                        <InputField
                            ref={register}
                            value={CommaFormatted(annualIncome)}
                            onChange={e => {
                                let val = removeCommaFormatting(e.target.value);
                                if (isNaN(Number(val))) return;
                                setAnnualIncome(val);
                                setValue(label, val);

                            }}
                            label={title}
                            data-testid={label}
                            id={label}
                            name={label}
                            icon={<i className="zmdi zmdi-money"></i>}
                            type={"text"}
                            placeholder={placeholder}
                            errors={errors}
                        />
                    </div>
                </div>
            </div>

            <div className="p-footer">
                <button
                data-testid="self-emp-net-income"
                id="self-emp-net-income"
                    className="btn btn-primary"
                    type="submit"

                >Save Income Source</button>
            </div>
        </form>
    )
}

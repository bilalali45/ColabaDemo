import React, { useState, useEffect } from 'react'
import InputField from '../../../../../../../Shared/Components/InputField'
import InputDatepicker from '../../../../../../../Shared/Components/InputDatepicker'
import { useForm } from 'react-hook-form';
import { useSelfEmploymentIncomeErrors } from './useSelfEmploymentIncomeErrors';
import { maskPhoneNumber, unMaskPhoneNumber } from '../../../../../../../Utilities/helpers/PhoneMasking';

export const SelfEmploymentIncomeFields = {
    Name: { Label: 'Business Name', Key: 'businessName', PlaceHolder: 'Business Name Here' },
    Phone: { Label: 'Business Phone Number', Key: 'businessPhone', PlaceHolder: 'XXX-XXX-XXXX' },
    StartDate: { Label: 'Business Start Date', Key: 'startDate', PlaceHolder: 'MM/DD/YYYY' },
    Title: { Label: 'Job Title', Key: 'jobTitle', PlaceHolder: 'Job Title' },
}

const { Name, Phone, StartDate, Title } = SelfEmploymentIncomeFields;

export const SelfEmploymentIncomeForm = ({ onSubmit, selfIncome = {} }) => {

    const [incomeFormData, setIncomeFormData] = useState<any>({
        [Phone.Key]: '',
    });

    const {
        register,
        handleSubmit,
        errors,
        setValue,
        clearErrors,
    } = useForm({
        mode: "onChange",
        reValidateMode: "onSubmit",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    useSelfEmploymentIncomeErrors(register);

    useEffect(() => {
        if (selfIncome['jobTitle']) {
            setIncomeFormData(setPreviousValues());
        }
    }, [selfIncome['jobTitle']]);

    const setPreviousValues = () => {
        let keys = [Name.Key, Phone.Key, StartDate.Key, Title.Key];
        let updatedIncomeForm = {}
        for (const key of keys) {
            updatedIncomeForm[key] = key === StartDate.Key ? new Date(selfIncome[key]) : selfIncome[key];
            setValue(key, selfIncome[key]);
        }
        return updatedIncomeForm;
    }

    const handleChange = ({ target: { id, value } }) => {

        clearErrors(id);

        let newValue = id === Phone.Key ? unMaskPhoneNumber(value) : value;

        if (id === Phone.Key) {
            if (isNaN(newValue) || newValue.length > 10) {
                return;
            }
        }

        setIncomeFormData(pre => {
            return {
                ...pre,
                [id]: newValue
            }
        });
        setValue(id, newValue);
    }

    const getFieldValue = (key) => {
        if (key === Phone.Key && incomeFormData[key]) {
            return maskPhoneNumber(incomeFormData[key]);
        }
        return incomeFormData[key];

    }

    const getFieldProps = (key, label, placeholder) => {
        let defaultProps = {
            ref: register,
            name: key,
            id: key,
            label: label,
            placeholder: placeholder,
            errors: errors,
            'data-testid': key
        }

        if (key === StartDate.Key) {
            return {
                ...defaultProps,
                autoComplete: "off",
                type: 'date',
                selected: incomeFormData[key],
                handleOnChange: (date) => handleChange({ target: { value: date, id: key } }),
                handleDateSelect: (date) => handleChange({ target: { value: date, id: key } })
            }
        }

        return {
            ...defaultProps,
            onChange: (e) => handleChange(e),
            value: getFieldValue(key),
        };
    }


    const createField = (key, label, placeholder, isDate = false) => {

        let field;;

        if (isDate) {

            field = <InputDatepicker
                {...getFieldProps(key, label, placeholder)}
                type={"date"}
                isPreviousDateAllowed={true}
            />;

        } else {

            field = <InputField
                {...getFieldProps(key, label, placeholder)}
                type={"text"}
            />;
        }

        return (
            {
                field,
                key
            }
        )
    }

    let formFields = [
        createField(Name.Key, Name.Label, Name.PlaceHolder),
        createField(Title.Key, Title.Label, Title.PlaceHolder),
        createField(StartDate.Key, StartDate.Label, StartDate.PlaceHolder, true),
        createField(Phone.Key, Phone.Label, Phone.PlaceHolder),
    ]

    const submit = handleSubmit((data) => onSubmit(data));

    return (
        <section>
            <form onSubmit={submit}>
                <div className="p-body">
                    <div className="row">

                        {
                            formFields?.map(field => {
                                return (
                                    <div className="col-sm-6">
                                        {field.field}
                                    </div>
                                )
                            })
                        }

                    </div>
                </div>

                <div className="p-footer">
                    <button
                    data-testid="self_employment_next"
                    id="self_employment_next"
                        className="btn btn-primary"
                        type="submit"

                    >Next</button>
                </div>
            </form>
        </section>
    )
}

import React, { FunctionComponent } from "react";
import { RegisterOptions, UseFormMethods } from "react-hook-form/dist/types";
import DatePicker from "react-datepicker";
import cx from "classnames";
// import "react-datepicker/dist/react-datepicker.css";
import { DatePickerIcon } from "./SVGs";
import MaskedInput from 'react-maskedinput';

interface InputProps
    extends Partial<Pick<UseFormMethods, "register" | "errors">> {
    id?: any;
    label?: any;
    rules?: RegisterOptions;
    name: string;
    selected?: any;
    minDate?: any;
    maxDate?: any;
    autoComplete?: string;
    placeholder?: any;
    className?: string;
    // Handlers
    handleDateSelect?: any;
    dateFormat?: any;
    handleOnChangeRaw?: Function;
    handleOnChange?: Function;
    handleOnBlur?: Function;
    isPreviousDateAllowed?: boolean;
    isFutureDateAllowed?: boolean;
}

const InputDatepicker: FunctionComponent<InputProps & React.HTMLProps<HTMLInputElement>> = ({
    id,
    label,
    selected,
    minDate,
    maxDate,
    placeholder,
    dateFormat,
    autoComplete,
    className,
    // Handlers
    handleDateSelect,
    handleOnChangeRaw,
    //handleOnBlur,
    handleOnChange,
    name,
    rules = {},
    register,
    errors = {},
    isPreviousDateAllowed = false,
    isFutureDateAllowed = true,
    ...rest
}) => {
    
    

    return (
        <div className="form-group dp-c">
            {label &&
                <label className="form-label" htmlFor={label} data-testid={label}>
                    <div className="form-text">{label}</div>
                </label>
            }
            <div className="d-wrap">
                <DatePicker
                    showMonthDropdown
                    showYearDropdown
                    data-testid={name}
                    className={`form-control ${cx(errors[name] && "error")}`}
                    id={id}
                    placeholderText={placeholder ? placeholder : 'MM/DD/YYYY'}
                    selected={selected}
                    onSelect={handleDateSelect}
                    autoComplete={autoComplete}
                    name={name}
                    ref={register && register(rules)}
                    dateFormat={dateFormat}
                    //onChangeRaw={handleOnChangeRaw}
                    onChange={(date) => { handleOnChange && handleOnChange(date) }}
                    minDate={isPreviousDateAllowed ? new Date(-8640000000000000) : new Date().setDate(new Date().getDate() + 1)}
                    maxDate={isFutureDateAllowed ? new Date(8640000000000000) : new Date().setDate(new Date().getDate() - 1)}
                    strictParsing
                    customInput={
                        <MaskedInput data-testid={name} mask="11/11/1111" placeholder="mm/dd/yyyy" />
                    }
                    {...rest}
                />

                <div className="d-icon">
                    <DatePickerIcon />
                </div>
            </div>

            {errors[name] && (
                <span className="form-error" role="alert" data-testid={name + "-error"}>
                    {errors[name].message}
                </span>
            )}
        </div>
    )
}

export default InputDatepicker

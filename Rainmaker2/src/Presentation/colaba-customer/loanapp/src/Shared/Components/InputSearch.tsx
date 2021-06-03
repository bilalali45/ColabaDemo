import React, { FunctionComponent } from "react";
// import cx from "classnames";
import { RegisterOptions, UseFormMethods } from "react-hook-form/dist/types";
import { IconSearch } from "./SVGs";
// import { Actions } from "../pages/SignUp/reducer";

interface InputProps
    extends Partial<Pick<UseFormMethods, "register" | "errors">> {
    rules?: RegisterOptions;
    name?: string;
    label?: string;
    onChange?: () => void;
    icon?: React.ReactNode;
    extention?: boolean;
}

const InputSearch: FunctionComponent<InputProps & React.HTMLProps<HTMLInputElement>> = ({
    name,
    type,
    label,
    icon,
    extention,
    rules = {},
    register,
    errors = {},
    ...rest
}) => {

    return (
        <div className="form-group input-search">
            <label className="form-label" htmlFor={name} data-testid={name}>
                {icon && <span className="form-icon">{icon}</span>}
                <div className="form-text">{label}</div>
            </label>
            <div className="input-group">
                <div className="input-group-prepend"><IconSearch /></div>
                <input
                    className={`${(type != 'checkbox') ? 'form-control' : ''}`}
                    aria-invalid={name && errors[name] ? "true" : "false"}
                    type={type}
                    name={name}
                    id={name}
                    ref={register && register(rules)}
                    {...rest}
                />
                <div className="input-group-append">
                    <button className="btn">
                        <em className="zmdi zmdi-chevron-down"></em>
                    </button>
                </div>
            </div>
        </div>
    )
}

export default InputSearch;

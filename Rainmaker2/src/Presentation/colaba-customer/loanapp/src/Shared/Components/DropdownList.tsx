import React, { FunctionComponent } from "react";
import cx from "classnames";
import { RegisterOptions, UseFormMethods } from "react-hook-form/dist/types";
// import { Actions } from "../pages/SignUp/reducer";
import Dropdown from 'react-bootstrap/Dropdown'

export const ListItem = (title) => {
    return (
        <span>{title}</span>
    );
}




interface DropdownListProps
    extends Partial<Pick<UseFormMethods, "register" | "errors">> {
    rules?: RegisterOptions;
    name?: string;
    label?: string;
    onChange?: () => void;
    icon?: React.ReactNode;
    extention?: boolean;
    value?: string;
    placeholder?: string;
    //Item?:any
    children?: any,
    onDropdownSelect?: (key: string) => void
}

const DropdownList: FunctionComponent<DropdownListProps & React.HTMLProps<any>> = ({
    name,
    type,
    label,
    icon,
    extention,
    rules = {},
    register,
    errors = {},
    value = "",
    placeholder,
    // Item,
    children,
    onDropdownSelect,
    ...rest
}) => {

    // const [listValue, setValue] = useState(value);


    return (
        <div className="form-group dropdownList">
            <label className="form-label" htmlFor={name} data-testid={name}>
                {icon && <span className="form-icon">{icon}</span>}
                <div className="form-text">{label}</div>
            </label>
            <div className="dropdown-group">
                <Dropdown

                    onSelect={(eventKey: string) => {
                        // setValue(event.currentTarget.innerText)
                        if (onDropdownSelect) onDropdownSelect(eventKey)
                    }}


                >
                    <Dropdown.Toggle as="div" >

                        <input
                            data-testid={name + "-dropdown"}
                            className={`${'form-control'} ${name && cx(errors[name] && "error")}`}
                            aria-invalid={name && errors[name] ? "true" : "false"}
                            type={type}
                            name={name}
                            id={name}
                            value={value}
                            readOnly={true}
                            placeholder={placeholder}
                            ref={register && register(rules)}
                            {...rest}


                        />
                        <div className="input-group-append">
                            <span id="toggle-fields-btn" className="btn "><em className="zmdi zmdi-chevron-down"></em></span>
                        </div>
                    </Dropdown.Toggle>

                    <Dropdown.Menu>
                        <Dropdown.Item eventKey="" disabled   >{placeholder}</Dropdown.Item>
                        {children}


                    </Dropdown.Menu>
                </Dropdown>

            </div>
            {name && errors[name] && (
                <span className="form-error" role="alert" data-testid={name + "-error"}>
                    {name && errors[name].message}
                </span>
            )}
        </div>
    )

}

export default DropdownList;

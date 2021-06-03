import React, { ChangeEvent, FunctionComponent, useState } from "react";
import cx from "classnames";
import { RegisterOptions, UseFormMethods } from "react-hook-form/dist/types";
import { SVGEyeCross, SVGEye } from "../Shared/Components/SVGs";

interface InputProps
  extends Partial<Pick<UseFormMethods, "register" | "errors">> {
  rules?: RegisterOptions;
  name: string;
  label: string;
  onChange: (value: ChangeEvent<HTMLInputElement>) => void;
  icon?: React.ReactNode;
  className?: string;
}

const InputField: FunctionComponent<InputProps & React.HTMLProps<HTMLInputElement>> = ({
  name,
  type,
  label,
  icon,
  className,
  rules = {},
  register,
  errors = {},
  children,
  ...rest
}) => {

  const [typePassword, setTypePassword] = useState('password');
  const showPasswordToggle = (event : React.MouseEvent<HTMLElement>)=> {
    event.preventDefault()
    typePassword == 'password' ? setTypePassword('text') : setTypePassword('password')
  }
  return (
    <div className="form-group">
      <label className="form-label" htmlFor={name} data-testid={name}>
        {icon && <span className="form-icon">{icon}</span>}
        <div className="form-text">{label}</div>
      </label>

      {type == 'password'?

        (<div className={`input-group ${cx(className,errors[name] && "error")}`}>
          <input
            className={`${cx("input form-control")}`}
            aria-invalid={errors[name] ? "true" : "false"}
            type={typePassword}
            name={name}
            id={name}
            ref={register && register(rules)}
            {...rest}
          />
          <div className="input-group-append">
            <a className="btn" onClick={showPasswordToggle}>
              {typePassword == 'text' &&
                <SVGEyeCross/>
              }
              {typePassword == 'password' &&
                <SVGEye />
              }
            </a>
          </div>
        </div>):
      (

        <input
          className={`${(type != 'checkbox') ? 'form-control' : ''} ${cx("input",className, errors[name] && "error")}`}
          aria-invalid={errors[name] ? "true" : "false"}
          type={type}
          name={name}
          id={name}
          ref={register && register(rules)}
          {...rest}
        />
      )     }




      {errors[name] && (
        <span className="form-error" role="alert" data-testid={name+"-error"}>
          {errors[name].message}
        </span>
      )}

      <div>
        {children}
      </div>

    </div>
  );
};

export default InputField;

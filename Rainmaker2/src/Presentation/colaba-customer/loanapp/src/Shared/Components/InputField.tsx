import React, { FunctionComponent, useState } from "react";
import cx from "classnames";
import { RegisterOptions, UseFormMethods } from "react-hook-form/dist/types";
import { IconEyeCross, IconEye } from "../../Shared/Components/SVGs";

interface InputProps
  extends Partial<Pick<UseFormMethods, "register" | "errors">> {
  rules?: RegisterOptions;
  name: string;
  label: string;
  onChange?: (value) => void;
  icon?: React.ReactNode;
  extention?: boolean;
  extentionValue?: string;
  value?: string;
  min?: string;
  max?: string;
}

const InputField: FunctionComponent<InputProps & React.HTMLProps<HTMLInputElement>> = ({
  name,
  type,
  label,
  icon,
  extention,
  extentionValue,
  value,
  min,
  max,
  rules = {},
  register,
  errors = {},
  disabled,
  ...rest
}) => {

  if (disabled == false) {
    return null;
  }

  const [typePassword, setTypePassword] = useState('password');
  const showPasswordToggle = (event) => {
    event.preventDefault()
    typePassword == 'password' ? setTypePassword('text') : setTypePassword('password')
  }

  return (
    <div className="form-group">
      <label className="form-label" htmlFor={name} data-testid={name}>
        {/* {icon && <span className="form-icon">{icon}</span>} */}
        <div className="form-text">{label}</div>
      </label>


      {type == 'password' &&
        <div className={`input-group ${cx(errors[name] && "error")}`}>
          {icon && <span className="form-icon">{icon}</span>}
          <input
            className={`form-control`}
            aria-invalid={errors[name] ? "true" : "false"}
            type={typePassword}
            name={name}
            id={name}
            ref={register && register(rules)}
            data-testid={name + "-input"}
            {...rest}
          />
          <div className="input-group-append">
            <a className="btn" onClick={showPasswordToggle}>
              {typePassword == 'text' &&
                <IconEyeCross />
              }
              {typePassword == 'password' &&
                <IconEye />
              }
            </a>
          </div>
        </div>}

      {(type == 'tel' && extention == true) &&
        <div className="row">
          <div className="col-md-8">
            <div className={`input-group  ${cx(errors[name] && "error")}`}>
              {/* {icon && <span className="form-icon">{icon}</span>} */}
              <input
                className={`form-control`}
                aria-invalid={errors[name] ? "true" : "false"}
                type={type}
                name={name}
                id={name}
                value={value}
                ref={register && register(rules)}
                data-testid={name + "-input"}
                {...rest}
              />
            </div>
          </div>

          <div className="col-md-4">
            <div className={`input-group  ${cx(errors[name] && "error")}`}>
              {/* {icon && <span className="form-icon">{icon}</span>} */}
              <input
                className={`form-control`}
                aria-invalid={errors[name] ? "true" : "false"}
                type={type}
                name={name + "_ext"}
                id={name + "_ext"}
                ref={register && register(rules)}
                placeholder={"EXT. XXXX"}
                value={extentionValue}
                data-testid={name + "-input"}
              />
            </div>
          </div>
        </div>
      }

      {(type == 'tel' && (extention == false || extention == undefined)) &&
        <div className={`input-group  ${cx(errors[name] && "error")}`}>
          {icon && <span className="form-icon">{icon}</span>}
          <input
            className={`form-control`}
            aria-invalid={errors[name] ? "true" : "false"}
            type={type}
            name={name}
            id={name}
            value={value}
            data-testid={name + "-input"}
            ref={register && register(rules)}
            {...rest}
          />
        </div>
      }

      {(type !== 'password' && type !== 'tel') &&
        <div className={`input-group  ${cx(errors[name] && "error")}`}>
          {icon && <span className="form-icon">{icon}</span>}
          <input
            className={`${(type != 'checkbox') ? 'form-control' : ''}`}
            aria-invalid={errors[name] ? "true" : "false"}
            type={type}
            name={name}
            id={name}
            value={value}
            data-testid={name + "-input"}
            ref={register && register(rules)}
            {...rest}
          />
        </div>
      }

      {errors[name] && (
        <span className="form-error" role="alert" data-testid={name + "-error"}>
          {errors[name].message}
        </span>
      )}
    </div>
  );
};

export default InputField;

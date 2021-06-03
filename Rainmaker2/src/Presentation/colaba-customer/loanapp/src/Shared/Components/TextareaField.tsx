import React, { FunctionComponent } from "react";

interface TextareaProps {
  name?: string;
  label?: string;
  placeholder?: string;
  disabled?: boolean;
  className?: string;
}

const TextareaField: FunctionComponent<TextareaProps> = ({
  name,
  label,
  placeholder,
  disabled,
  className,
  ...rest
}) => {
  return (
    <div className="form-group">
      {label ? (
        <label className="form-label" htmlFor={name} data-testid={name}>
          <div className="form-text">{label}</div>
        </label>
      ) : (
        ""
      )}

      <textarea
        className={`form-control textarea-control ${className ? className : ''}`}
        name={name}
        id={name}
        data-testid={name + "-textarea"}
        placeholder={placeholder}
        disabled={disabled}
        {...rest}
      ></textarea>
    </div>
  );
};

export default TextareaField;

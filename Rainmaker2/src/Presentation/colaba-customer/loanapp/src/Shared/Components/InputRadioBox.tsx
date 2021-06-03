import React, { FunctionComponent } from 'react'
import { RegisterOptions, UseFormMethods } from "react-hook-form/dist/types";

interface InputProps
	extends Partial<Pick<UseFormMethods, "register" | "errors">> {
	rules?: RegisterOptions;
	name?: string;
	label?: string;
	onChange?: any;
	icon?: React.ReactNode;
	checked?: boolean | undefined;
	dataTestId?: string;
}

const InputRadioBox: FunctionComponent<InputProps & React.HTMLProps<HTMLInputElement>> = ({
	id,
	className,
	name,
	checked,
	value,
	onChange,
	children,
	rules = {},
	register,
	dataTestId,
	errors = {},
	...rest
}) => {
	return (
		<label
			data-testid="InputRadioBox"
			className={`form-control-Radiobox ${className} ${checked==true && checked != undefined ? 'active' : ''}`}
		>
			<input
				type="radio"
				data-testid={dataTestId}
				onChange={onChange}
				id={id}
				name={name}
				checked={checked}
				value={value}
				ref={register && register(rules)}
				{...rest}
			/>
			<span className={`form-control-Radiobox-type`}></span>
			<span className={`form-control-Radiobox-label`} >{children}</span>
		</label>
	)
}

export default InputRadioBox;

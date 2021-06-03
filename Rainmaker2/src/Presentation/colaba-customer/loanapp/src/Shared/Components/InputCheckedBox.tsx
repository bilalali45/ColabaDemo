import React, { FunctionComponent } from 'react'
import { RegisterOptions, UseFormMethods } from "react-hook-form/dist/types";

interface InputProps
	extends Partial<Pick<UseFormMethods, "register" | "errors">> {
	rules?: RegisterOptions;
	name?: string;
	label?: string;
	onChange?: (e) => void;
	onClick?: () => void;
	icon?: React.ReactNode;
}

const InputCheckedBox: FunctionComponent<InputProps & React.HTMLProps<HTMLInputElement>> = ({
	id,
	className,
	name,
	checked,
	label,
	value,
	onChange,
	onClick,
	children,
	rules = {},
	register,
	errors = {},
	...rest
}) => {
	return (
		<label
			data-testid="InputCheckedBox"
			className={`form-control-checkedbox ${className}`}
			onChange={onClick}
		>
			<input
				type="checkbox"
				onChange={onChange}
				id={id}
				name={name}
				checked={checked}
				value={value}
				ref={register && register(rules)}
				data-testid={name + "-checkbox"}
				{...rest}
			/>
			<span className={`form-control-checkedbox-type`}></span>
			<span className="form-control-checkedbox-label clickable" onClick={onChange}>{label}</span> {children}
		</label>
	)
}

export default InputCheckedBox;

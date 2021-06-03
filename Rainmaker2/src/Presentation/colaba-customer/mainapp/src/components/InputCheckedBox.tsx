import React, { FunctionComponent } from 'react'
import { RegisterOptions, UseFormMethods } from "react-hook-form/dist/types";

interface InputProps
	extends Partial<Pick<UseFormMethods, "register" | "errors">> {
	rules?: RegisterOptions;
	name: string;
	label: string;
	onChange: any;
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
	children,
	rules = {},
	register,
	errors = {},
	...rest
}) => {
	return (
		<label
			data-testid="InputCheckedBox"
			className="form-control-checkedbox"
		>
			<input
				type="checkbox"
				onChange={onChange}
				id={id}
				className={className}
				name={name}
				checked={checked}
				value={value}
				ref={register && register(rules)}
				{...rest}
			/>
			<span className={`form-control-checkedbox-type`}></span>
			<label className={`form-control-checkedbox-label`} >
				<span className="clickable" onClick={onChange}>{label}</span> {children}
			</label>
		</label>
	)
}

export default InputCheckedBox;

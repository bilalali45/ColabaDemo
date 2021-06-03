import React, { FunctionComponent, useState, useRef } from "react";

import { UseFormMethods } from "react-hook-form/dist/types";
import TextField from "@material-ui/core/TextField";
import Autocomplete from "@material-ui/lab/Autocomplete";
// import { Actions } from "../pages/SignUp/reducer";

import { Controller } from "react-hook-form";
import { InputAdornment } from "@material-ui/core";

export const ListItem = (title) => {
  return <span>{title}</span>;
};

interface AutoCompleteDropdown
  extends Partial<Pick<UseFormMethods, "register" | "errors">> {
  value?: string;
  onChange?: any;
  options: any[];
  icon?: string;
  label?: string;
  onInputChange?: any;
  register?: any;
  control: any;
  errors: any;
  name?: string;
  optionLabel?: string;
  placeholder?: string;
  defaultValue?: Object;
  disabled:boolean;
  rules?:any;
}

const AutoCompleteDropdown: FunctionComponent<AutoCompleteDropdown> = ({
  value,
  onChange,
  options,
  icon,
  label,
  onInputChange,
  register,
  control,
  errors,
  name = "",
  optionLabel,
  placeholder,
  defaultValue,
  disabled,
  rules,
  ...rest
}) => {

  const input = useRef<any>();
  const [showPopup, setSetShowPopUp] = useState<boolean>(false);

  return (
    <div className="form-group dropdownList">
      <label className="form-label" htmlFor={name} data-testid={name+"-label"}>
        {icon && <span className="form-icon">{icon}</span>}
        <div className="form-text">{label}</div>
      </label>
      <div className="dropdown-group" ref={input}>
        <Controller
          as={
            <Autocomplete   
              disabled={disabled}
              className={errors[name] && 'error'}
              id={name}
              freeSolo
              disableClearable
              value={value}
              onChange={onChange}
              open={!disabled && showPopup}     
              selectOnFocus = {true}
              openOnFocus={true}
              inputValue={value}
              onInputChange={onInputChange}
              blurOnSelect = {true}
              options={options ? options : []}
              getOptionLabel={(option: any) => option.name}
              renderOption={(option: any) => <span>{option.name}</span>}
              renderInput={(params) => (
                <TextField 
                ref={input}
                {...params} 
                placeholder={placeholder} 
                data-testid={name}
                onBlur={()=>setSetShowPopUp(false)}
                onClick={(e)=>{setSetShowPopUp(!showPopup); console.log(e.target)}}
                onKeyDown={()=>setSetShowPopUp(true)}
                autoComplete="none"onFocus={()=>{
                  var autoCompleteDropDown: HTMLInputElement | null = document.querySelector("#"+name);
                  if (autoCompleteDropDown){
                    autoCompleteDropDown.setAttribute("autocomplete", "search");
                  }
                }}
                InputProps={{
                  ...params.InputProps,
                  endAdornment: (
                    
                    <InputAdornment position="end" >
                      <div className="btn" onClick={()=>{setSetShowPopUp(!showPopup)}}>
                        <em className="zmdi zmdi-chevron-down"></em>
                      </div>                      
                    </InputAdornment>
                  )
                }}
                />
              )}
            />  
          }
          autoComplete={name}
          name={name}
          control={control}
          onChange={onChange}
          rules={rules}
          ref={register}
          errors={errors}
          value={value}
          inputValue={value}
          onInputChange={onInputChange}
          defaultValue={defaultValue}
          {...rest}
        />
      </div>
      {errors[name] && (
        <span className="form-error" role="alert" data-testid={name + "-error"}>
          {errors[name].message}
        </span>
      )}
    </div>
  );
};

export default AutoCompleteDropdown;

import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { Password } from "../../../Utilities/helpers/Password";
import Input from "../../components/InputField";
import { formData } from "../../lib/types";
import { SVGLock } from "../../Shared/Components/SVGs";

interface resetPasswordFields {
  submitBtnText: string;
  onSubmitHandler: Function;
}

const ResetPasswordForm = ({
  submitBtnText,
  onSubmitHandler,
}: resetPasswordFields) => {
  const { register, errors, handleSubmit, getValues, clearErrors } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  const [showPassHelp, setShowPassHelp] = useState<boolean>(false);
  const [isMinimumPassLength, setIsMinimumPassLength] = useState<boolean>(
    false
  );
  const [
    numOrLetterOrSpecialChar,
    setNumOrLetterOrSpecialChar,
  ] = useState<boolean>(false);
  const [
    hasSeqAndIdenticalChars,
    setHasSeqAndIdenticalChars,
  ] = useState<boolean>(false);

  useEffect(()=>{

  },[])


 


  useEffect(() => {
    window.addEventListener("mousedown", handleClickOutside);
    return () => {
      window.removeEventListener("mousedown", handleClickOutside);
    };
  }, [showPassHelp]);

  const handleClickOutside = (event: any) => {
    if (
      (showPassHelp && document) &&
      !document.getElementById("password")?.isEqualNode(event.target)
    ) {
      setShowPassHelp(false);
    }
  };

  const onSubmit = (data: formData) => {
    onSubmitHandler(data);
  };

  const passwordInputHandler = (e: any) => {
    clearErrors("password")
    Password.setPassword(e.target.value);
    let minPassValidity = Password.checkMinimumLengthValidity()
    let numOrLetOrSpeChar = Password.checkNumOrLetterOrSpecialChar()
    let seqOrIdenChars = !Password.checkSeqAndIdenticalChars()
    setIsMinimumPassLength(minPassValidity);
    setNumOrLetterOrSpecialChar(numOrLetOrSpeChar);
    setHasSeqAndIdenticalChars(seqOrIdenChars);
    if(minPassValidity &&
      numOrLetOrSpeChar &&
      seqOrIdenChars)
      
    setShowPassHelp(false);
    else setShowPassHelp(true)
  };

  const showPasswordHelp = () => {
    return (
      <div className="password-help" data-testid="pass-help" id="pass-help">
        <div className="password-help-wrap">
        <h5>Password help:</h5>
        <ul>
          <li className={`${isMinimumPassLength ? "success" : "failed"} `}>
            At Least 8 characters
          </li>
          <li className={`${numOrLetterOrSpecialChar ? "success" : "failed"}`}>
            At least 2 of the following
            <ul>
              <li>1 letter (case sensitive)</li>
              <li>1 number</li>
              <li>1 of these Special characters: @!#$%+?=~</li>
            </ul>
          </li>
          <li className={`${hasSeqAndIdenticalChars ? "success" : "failed"}`}>
            No more than 2 identical or sequential characters (111, aaa,123,
            abc, !!!)
          </li>
        </ul>
      </div>
      </div>
    );
  };
  return (
    <form
      id="reset-pass-form"
      data-testid="reset-pass-form"
      className="colaba-form"
      onSubmit={handleSubmit(onSubmit)}>
      <Input
        
        data-testid="reset-password-input"
        maxLength={100}
        id="password"
        icon={<SVGLock />}
        label={"Password"}
        name="password"
        type={"password"}
        register={register}
        autoFocus
        onChange={passwordInputHandler}
        rules={{
          required: "This field is required.",
          validate: {
            validity: () =>
              (isMinimumPassLength &&
                numOrLetterOrSpecialChar &&
                hasSeqAndIdenticalChars) ||
              "Password must be matched with given instructions.",
          },
        }}
        errors={errors}
        onCopy={(event) => event.preventDefault()}
        onPaste={(event) => event.preventDefault()}
      >
        {showPassHelp && showPasswordHelp()}
      </Input>

      <div className="form-group">
        <Input
          data-testid="confirm-password-input"
          maxLength={100}
          id="confirm-password"
          icon={<SVGLock />}
          label={"Confirm Password"}
          name="confirmPassword"
          type={"password"}
          register={register}
          onChange={() => {clearErrors("confirmPassword")}}
          rules={{
            required: "This field is required.",
            validate: {
              validity: (value) => {
                const { password } = getValues();
                return (
                  password === value || "Please enter the same password again."
                );
              },
            },
          }}
          errors={errors}
          onCopy={(event) => event.preventDefault()}
          onPaste={(event) => event.preventDefault()}
        />
      </div>
      <div className="form-group extend">
        <button
          id="reset-pass-btn"
          className="btn btn-primary btn-lg btn-block"
          type="submit"
          data-testid="reset-pass-btn"
            onClick={handleSubmit(onSubmit)}>
          {submitBtnText}
        </button>
      </div>
    </form>
  );
};

export default ResetPasswordForm;

import React, { ChangeEvent } from "react";
import { useForm } from "react-hook-form";

import InputField from "../../../../../Shared/Components/InputField";
import InputRadioBox from "../../../../../Shared/Components/InputRadioBox";
import { AddressHomeIcon } from "../../../../../Shared/Components/SVGs";

import { CommaFormatted } from "../../../../../Utilities/helpers/CommaSeparteMasking";
import { CurrentResidencePostProps } from "../../CurrentResidenceDetails/CurrentResidenceDetails";

type PropertyValueProps = {
  propVal: string;
  setPropVal: Function;
  propDues: string;
  setPropDues: Function;
  selling: boolean | null;
  setSelling: Function;
  onSave: Function;
  onPropValChangeHandler: Function;
  onPropDuesChangeHandler: Function;
  homeAddress: string;
};
export const PropertyValue = ({
  propVal,
  propDues,
  selling,
  setSelling,
  onSave,
  onPropValChangeHandler,
  onPropDuesChangeHandler,
  homeAddress,
}: PropertyValueProps) => {
  
  const {
    register,
    errors,
    handleSubmit,
    clearErrors,
  } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  const onSubmit = (data: CurrentResidencePostProps) => {
    onSave(data);
  };

  return (
    <div className="comp-form-panel income-panel colaba-form">
      <div className="row form-group">
        <div className="col-md-12">
          <div className="listaddress-warp">
            <div className="list-add">
              <div className="icon-add">
                <AddressHomeIcon />
              </div>
              <div className="cont-add">{`${homeAddress}?`}</div>
            </div>
          </div>
        </div>
      </div>
      <form id="prop-val-form" data-testid="prop-val-form" autoComplete="off">
        <div className="row form-group">
          <div className="col-md-6">
            <InputField
              label={"Property Value"}
              id="prop_val"
              name="prop_val"
              icon={<i className="zmdi zmdi-money"></i>}
              type={"text"}
              placeholder={"Estimated Value"}
              register={register}
              value={propVal ? CommaFormatted(propVal) : ""}
              rules={{
                required: "This field is required.",
                validate: {
                  validity: (value) =>
                    !(value === "0.00") || "Amount must be greater than 0",
                },
              }}
              errors={errors}
              onChange={(e: ChangeEvent<HTMLInputElement>) => {
                clearErrors("prop_val");
                onPropValChangeHandler(e);
              }}
            />
          </div>
          <div className="col-md-6">
            <InputField
              label={"Homeowners Association Dues (if applicable)"}
              id="prop_dues"
              name="prop_dues"
              value={propDues ? CommaFormatted(propDues) : ""}
              icon={<i className="zmdi zmdi-money"></i>}
              type={"text"}
              placeholder={"Estimated Annual Dues"}
              register={register}
              rules={{
                // required: "This field is required.",
                validate: {
                  validity: (value) =>
                    !(value === "0.00") || "Amount must be greater than 0",
                },
              }}
              errors={errors}
              onChange={(e: ChangeEvent<HTMLInputElement>) => {
                clearErrors("prop_dues");
                onPropDuesChangeHandler(e);
              }}
            />
          </div>
        </div>
        <div className="row form-group">
          <div className="col-sm-12">
            <h4>Will you be selling this property?</h4>
          </div>
          <div className="col-md-6">
            <InputRadioBox
              id="selling_yes"
              data-testid="selling_yes"
              className=""
              name="selling"
              register={register}
              rules={{
                required: "Please select one",
              }}
              errors={errors}
              onChange={() => {
                clearErrors("selling");
                setSelling(true);
              }}
              checked={selling && selling ? true : false}
              value={"Yes"}>
              Yes
            </InputRadioBox>
          </div>
          <div className="col-md-12">
            <InputRadioBox
              id="selling_no"
              data-testid="selling_no"
              className=""
              name="selling"
              register={register}
              rules={{
                required: "Please select one",
              }}
              errors={errors}
              onChange={() => {
                clearErrors("selling");
                setSelling(false);
              }}
              checked={selling === null ? false : !selling ? true : false}
              value={"No"}>
              No
            </InputRadioBox>
          </div>
          {errors?.selling && (
            <span
              className="form-error no-padding"
              role="alert"
              data-testid="selling-error">
              {errors?.selling?.message}
            </span>
          )}
        </div>

        <div className="form-footer">
          <button
            className="btn btn-primary"
            id="prop_val_save"
            data-testid="prop_val_save"
            // disabled={true}
            onClick={handleSubmit(onSubmit)}>
            {"Save and continue"}
          </button>
        </div>
      </form>
    </div>
  );
};

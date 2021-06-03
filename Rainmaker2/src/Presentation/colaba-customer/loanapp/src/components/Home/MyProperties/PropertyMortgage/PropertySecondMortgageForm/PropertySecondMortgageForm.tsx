import React from "react";

import { useForm } from "react-hook-form";
import { AddressHomeIcon } from "../../../../../Shared/Components/SVGs";
import InputRadioBox from "../../../../../Shared/Components/InputRadioBox";

type PropertySecondMortgageFormProps = {
  haveMortgage: boolean | null;
  setHaveMortgage: Function;
  onSave: Function;
  homeAddress: string;
};

export const PropertySecondMortgageForm = ({
  haveMortgage,
  setHaveMortgage,
  onSave,
  homeAddress,
}: PropertySecondMortgageFormProps) => {
  const { register, errors, handleSubmit, clearErrors } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  const onSubmit = () => {
    onSave();
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
              <div className="cont-add">{homeAddress}</div>
            </div>
          </div>
        </div>
      </div>

      <form
        id="has-second-mortgage-form"
        data-testid="has-second-mortgage-form"
        autoComplete="off">
        <div className="row form-group">
          <div className="col-sm-12">
            <h4>Do you have a second mortgage on this property?</h4>
          </div>
          <div className="col-md-6">
            <InputRadioBox
              id="mortgage_yes"
              data-testid="mortgage_yes"
              className=""
              name="have_mortgage"
              register={register}
              rules={{
                required: "Please select one",
              }}
              errors={errors}
              onChange={() => {
                clearErrors("have_mortgate");
                setHaveMortgage(true);
              }}
              checked={haveMortgage && haveMortgage ? true : false}
              value={"Yes"}>
              Yes
            </InputRadioBox>
          </div>

          <div className="col-md-12">
            <InputRadioBox
              id="mortgage_no"
              data-testid="mortgage_no"
              className=""
              name="have_mortgage"
              register={register}
              rules={{
                required: "Please select one",
              }}
              errors={errors}
              onChange={() => {
                clearErrors("have_mortgate");
                setHaveMortgage(false);
              }}
              checked={
                haveMortgage === null || haveMortgage === undefined? false : !haveMortgage ? true : false
              }
              value={"No"}>
              No
            </InputRadioBox>
          </div>
          {errors?.have_mortgage && (
            <span
              className="form-error no-padding"
              role="alert"
              data-testid="have_mortgage-error">
              {errors?.have_mortgage?.message}
            </span>
          )}
        </div>

        <div className="form-footer">
          <button
            className="btn btn-primary"
            id="have_mortgage_save"
            data-testid="have_mortgage_save"
            // disabled={true}
            onClick={handleSubmit(onSubmit)}>
            {"Save and continue"}
          </button>
        </div>
      </form>
    </div>
  );
};

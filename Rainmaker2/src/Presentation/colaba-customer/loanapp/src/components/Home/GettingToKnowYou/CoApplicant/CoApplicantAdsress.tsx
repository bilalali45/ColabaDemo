import React from "react";

import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";

import InputRadioBox from "../../../../Shared/Components/InputRadioBox";
import { AddressHomeIcon} from '../../../../Shared/Components/SVGs';
export const CoApplicantAddress = ({ setcurrentStep }: any) => {

  return (
    <div className="compo-abt-yourSelf fadein">
      <PageHead
        title="Personal Information"
        handlerBack={() => {
          setcurrentStep("about_your_self");
        }}
      />
      <TooltipTitle title="Thanks! Please tell us about  Jessica’s current home address." />
      <form data-testid="personal-info-form">
        <div className="comp-form-panel colaba-form">
          <div className="row form-group">
            <div className="col-md-12">
              <h4>Is Jessica’s current home address the same as yours?</h4>
            </div>
            <div className="col-md-12">
              <div className="listaddress-warp">
                <div className="list-add">
<div className="icon-add">
<AddressHomeIcon />
</div>
<div className="cont-add">
5919 Trussville Crossings Pkwy, Birmingham AL 35235, USA
</div>
                </div>
              </div>
            </div>



          </div>

          <div className="form-group">
            <InputRadioBox
              id=""
              className=""
              name="sameAdd"
              // checked={true}
              value={"Yes"}
            >Yes</InputRadioBox>
            <InputRadioBox
              id=""
              className=""
              name="sameAdd"
              // checked={true}
              value={"No"}
            >No</InputRadioBox>
          </div>

          <div className="form-footer">
            <button
              className="btn btn-primary"
              type="submit"
            // onClick={}
            >
              {"Save & Continue"}
            </button>
          </div>
        </div>
      </form>
    </div>
  )
}

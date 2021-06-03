import React from "react";

import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";

import { SinglePropertyIcon } from '../../../../Shared/Components/SVGs';
import IconRadioBox from "../../../../Shared/Components/IconRadioBox";

export const AboutNewHome = ({ setcurrentStep }: any) => {

  return (
    <div className="compo-abt-yourSelf fadein">
      <PageHead
        title="Subject Property"
        handlerBack={() => {
          setcurrentStep("about_your_self");
        }}
      />
      <TooltipTitle title="Please let us know about your new home." />
      <form data-testid="personal-info-form">
        <div className="comp-form-panel colaba-form">
          <div className="row form-group">
            <div className="col-md-6">
              <IconRadioBox
                id={1}
                className=""
                name="radio1"
                //checked={false}
                value={"Single Family Property"}
                title="Single Family Property"
                Icon={<SinglePropertyIcon />}
              />

            </div>
            <div className="col-md-6">
              <IconRadioBox
                id={2}
                className=""
                name="radio1"
                //checked={false}
                value={"Townhouse"}
                title="Townhouse"
                Icon={<SinglePropertyIcon />}
              />

            </div>
            <div className="col-md-6">
              <IconRadioBox
                id={3}
                className="checked"
                name="radio1"
                checked={true}
                value={"Condominium"}
                title="Condominium"
                Icon={<SinglePropertyIcon />}
              />

            </div>



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

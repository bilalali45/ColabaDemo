import React, {useState} from "react";
import PageHead from '../../Shared/Components/PageHead';
import TooltipTitle from '../../Shared/Components/TooltipTitle';
import InputField from '../../Shared/Components/InputField';

import InputSearch from '../../Shared/Components/InputSearch';
import InputCheckedBox from '../../Shared/Components/InputCheckedBox';
import InputRadioBox from '../../Shared/Components/InputRadioBox';
// import DatePicker from 'react-bootstrap-date-picker';
import InputDatepicker from '../../Shared/Components/InputDatepicker';

const DummyFormControls = () => {
    const [startDate, setStartDate] = useState<any>(new Date());
    return (
        <div className="compo-abt-yourSelf fadein">
            <PageHead title="Dummy Form Controls" handlerBack={() => { }} />
            <TooltipTitle title="Hey Zohaib, Please provide few details about yourself." />

            <div className="comp-form-panel colaba-form">
                <div className="row form-group">

                    <div className="col-md-12">

                        <InputSearch label={"Current Home Address"}
                            data-testid="CurrentHomeAddress"
                            id="CurrentHomeAddress"
                            name="CurrentHomeAddress"
                            type={"text"}
                            placeholder={"Search Home Address"}
                        />

                        <div className="form-group">
                        <InputCheckedBox
                            id=""
                            className=""
                            name=""
                            label={`Active Duty Personnel`}
                            // checked={true}
                            value={""}
                        ></InputCheckedBox>
                        </div>
                        <div className="form-group">
                        <InputRadioBox
                            id=""
                            className=""
                            name="radio1"
                            // checked={true}
                            value={""}
                        >Active Duty Personnel</InputRadioBox>
                        <InputRadioBox
                            id=""
                            className=""
                            name="radio1"
                            // checked={true}
                            value={""}
                        >Active Duty Personnel2</InputRadioBox>
                        </div>

                    </div>



                    <div className="col-md-6">

                    <div className="col-md-6">
                    <InputDatepicker 
                    name={"date"}
                        id="datepicker"
                        label={`Date`}
                        autoComplete="off" 
                        autoFocus={true}
                        selected={startDate}
                        handleOnChange={(date:any)=>setStartDate(date)}
                        handleDateSelect={(date:any)=>setStartDate(date)}
                          />
                </div>

                        <InputField
                            label={"Legal First Name"}
                            data-testid="LegalFirstName"
                            id="LegalFirstName"
                            name="LegalFirstName"
                            type={"text"}
                            placeholder={"Legal First Name"}
                        />
                        <InputField
                            label={"Legal Last Name"}
                            data-testid="LegalLastName"
                            id="LegalLastName"
                            name="LegalLastName"
                            type={"text"}
                            placeholder={"Legal Last Name"}
                        />
                        <InputField
                            label={"Email Address"}
                            data-testid="EmailAddress"
                            id="EmailAddress"
                            name="EmailAddress"
                            type={"email"}
                            placeholder={"Email Address"}
                        />

                        <InputField
                            label={"Work Phone Number "}
                            data-testid="WorkPhoneNumber "
                            id="WorkPhoneNumber"
                            name="WorkPhoneNumber"
                            type={"tel"}
                            extention={true}
                            placeholder={"EXT. XXXX"}
                        />


                    </div>

                    <div className="col-md-6">
                        <InputField
                            label={"Middle Name"}
                            data-testid="MiddleName"
                            id="MiddleName"
                            name="MiddleName"
                            type={"text"}
                            placeholder={"Middle Name"}
                        />
                        <InputField
                            label={"Suffix"}
                            data-testid="Suffix"
                            id="Suffix"
                            name="Suffix"
                            type={"text"}
                            placeholder={"Jr., Sr., III, IV, etc."}
                        />
                        <InputField
                            label={"Home Phone Number"}
                            data-testid="HomePhoneNumber"
                            id="HomePhoneNumber"
                            name="HomePhoneNumber"
                            type={"tel"}
                            placeholder={"XXX-XXX-XXXX"}
                        />
                        <InputField
                            label={"Cell Phone Number"}
                            data-testid="CellPhoneNumber"
                            id="CellPhoneNumber"
                            name="CellPhoneNumber"
                            type={"tel"}
                            placeholder={"XXX-XXX-XXXX"}
                        />
                    </div>
                </div>

                <button className="btn btn-primary">Save & Continue</button>

            </div>

        </div>
    )
}

export default DummyFormControls

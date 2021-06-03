import React, { useEffect } from 'react'
import InputRadioBox from '../../../../Shared/Components/InputRadioBox';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { TextField } from '@material-ui/core';
import { useForm, Controller } from 'react-hook-form';
import  {  MyNewMortgageSteps } from '../../../../store/actions/LeftMenuHandler';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';

export const SubjectPropertyIntendForm = ({
    onSubmit,
    isPropertyIdentified,
    states,
}) => {


    const { register, handleSubmit, watch, errors, getValues, setValue, clearErrors, control } = useForm({
        mode: "onChange",
        reValidateMode: "onSubmit",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });


    useEffect(() => {
        register('propertyIdentification', {
            validate: (value) => {
                if (!value) {
                    return 'Please select one.'
                }
                return true;
            },
        });
        register('stateName', {
            validate: (value) => {
                if (getValues('propertyIdentification') === 'No') {
                    if (!value) {
                        return 'Please select state name.'
                    }
                    return true;
                }
                return true;
            },
        });
    }, [register]);

    useEffect(() => {
        watch("propertyIdentification");
        watch("stateName");
    }, []);

    useEffect(() => {
        console.log('isPropertyIdentified', isPropertyIdentified)
        if (!isPropertyIdentified) return;
        setValue('propertyIdentification', (
            isPropertyIdentified.isIdentified === true ? 'Yes' :
                isPropertyIdentified.isIdentified === false ? 'No' : ''
        ));

        let foundStateName = states?.find(s => {
            if (s?.id === isPropertyIdentified.stateId) {
                return s;
            }
        })?.name;
        setValue('stateName', foundStateName);
    }, [isPropertyIdentified?.isIdentified]);


    useEffect(() => {
        console.log(getValues('propertyIdentification'));
        clearErrors();
    }, [getValues('propertyIdentification'), getValues('stateName')]);

    useEffect(() => {
        switch (getValues("propertyIdentification")) {
            case 'No':
                toggleSubjectPropertyAddressForm('disableFeature');
                break;

            case 'Yes':
                toggleSubjectPropertyAddressForm('enableFeature');
                break;

            default:
                break;
        }
    }, [getValues("propertyIdentification")]);

    const submit = handleSubmit((data) => onSubmit(data));


    const toggleSubjectPropertyAddressForm = (enableOrDisable) => {
        NavigationHandler[enableOrDisable](MyNewMortgageSteps.SubjectPropertyAddress);
    }

    return (
        <form data-testid="personal-info-form" onSubmit={submit}>
            <div className="comp-form-panel colaba-form">
                <div className="form-group ">
                    <h4>Have you identified the property you intend to purchase?</h4>
                    {<div className="intend-wrap">
                        {
                            ['Yes', 'No'].map(op => {
                                return (
                                    <div className="clearfix"><InputRadioBox
                                        register={register}
                                        id={op}
                                        className=""
                                        name="propertyIdentification"
                                        value={op}
                                        onChange={(e) => setValue('propertyIdentification', e.target.value)}
                                        defaultChecked={getValues("propertyIdentification") == op}
                                    >{op}</InputRadioBox></div>
                                )
                            })
                        }
                        {errors?.propertyIdentification && <span className="form-error no-padding" role="alert" >{errors?.propertyIdentification?.message}</span>}

                    </div>}



                    <div style={{ display: getValues('propertyIdentification') === 'No' ? 'block' : 'none' }}>
                        <div className="form-group">
                            <h4>Where will the property be located?</h4>
                        </div>
                        <div className="row">
                            <div className="col-md-6">
                                <label className="form-label">State</label>
                                <Controller
                                    name="stateName"
                                    render={({ onChange, value }) => (
                                        <Autocomplete
                                            className={errors?.stateName && 'error'}
                                            options={states.map(s => s.name)}
                                            getOptionLabel={(option: string) => option}
                                            onChange={(e, option) => {
                                                console.log(e)
                                                onChange(option)
                                            }}
                                            value={value}
                                            renderInput={(params) => (
                                                <TextField
                                                    {...params}
                                                />

                                            )}
                                        />

                                    )}
                                    control={control}
                                    defaultValue={[]}
                                />
                            </div>
                        </div>


                    </div>


                    {errors?.stateName && <span className="form-error" role="alert" >{errors?.stateName?.message}</span>}

                </div>


                <div className="form-footer">
                    <button
                        className="btn btn-primary"
                        type="submit"
                    >
                        {"Save & Continue"}
                    </button>


                </div>
            </div>
        </form>
    )
}

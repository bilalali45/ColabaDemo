import React, { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import PageHead from '../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../Shared/Components/TooltipTitle';
import { IconSingleFamilyProperty, IconTownhouse, IconInvestmentProperty } from '../../../../Shared/Components/SVGs';
import InputRadioBox from '../../../../Shared/Components/InputRadioBox';
import { StringServices } from '../../../../Utilities/helpers/StringServices';
import {
    AddOrUpdatePropertyUsagePayload,
    PropertyUsageBorrowerProto
} from "../../../../Entities/Models/PropertSubjectUseEntities";
import IconRadioBox from '../../../../Shared/Components/IconRadioBox';


export const SubjectPropertyUseForm = ({ formSchema, allpropertyusages, formOnSubmit }) => {

    const [isButtonDisabled, setIsButtonDisabled] = useState<boolean>(true);
    const [borrowerState, setBorrowerState] = useState<AddOrUpdatePropertyUsagePayload>(formSchema);
    useEffect(() => {
        onChangeHandler()
    }, []);

    const { register, handleSubmit } = useForm({
        mode: "onChange",
        reValidateMode: "onChange",
    });

    function onSubmit() {
        formOnSubmit(borrowerState)
    }

    const toggleRadio = (index, e) => {
        let temp = {...borrowerState}
        temp.borrowers[index].willLiveIn = e.target.value == "true"?true:false
        setBorrowerState(temp)
        onChangeHandler()
    }


    const onChangeHandler = () => {
        console.log('=========> onChangeHandler')
        let { propertyUsageId, borrowers } = borrowerState;

        let ifNullPreset = true;

        if (borrowers != null) {
            ifNullPreset = borrowers.some(function (e: PropertyUsageBorrowerProto) {
                return e.willLiveIn == null
            });
        }

        if (propertyUsageId && propertyUsageId > 1)
            setIsButtonDisabled(false);

        else if (ifNullPreset || (propertyUsageId == undefined || propertyUsageId == null))
            setIsButtonDisabled(true);
        else
            setIsButtonDisabled(false);
    }

    const icons = {
        'I Will Live Here (Primary Residence)': <IconSingleFamilyProperty />,
        'This Will Be A Second Home': <IconTownhouse />,
        'This Is An Investment Property': <IconInvestmentProperty />
    }


    return (
        <>
            <div data-testid="subjectProperty-usage" className="compo-subject-P compo-subject-P-use fadein">
                <PageHead
                    title="Subject Property"
                />
                <TooltipTitle title="Excellent! How will you use this property?" />
                <form onSubmit={handleSubmit(onSubmit)} data-testid="subjectPropertUsage-form">
                    <div className="comp-form-panel colaba-form">
                        <div className="form-group">
                            <h4>Select one:</h4>
                        </div>
                        <div className="row form-group">
                            <div className="col-md-7">

                                <div className="full-wrap">
                                    {
                                        (allpropertyusages?.map(({id, name}) => {

                                                return (
                                                    <>
                                                        <IconRadioBox //className="checked"
                                                                               name="propertyUsageId" title={name}
                                                                               Icon={icons[name]}
                                                                               handlerClick={(e) =>
                                                                                {
                                                                                    let temp = {...borrowerState}
                                                                                    temp.propertyUsageId = e;
                                                                                    setBorrowerState(temp)
                                                                                    onChangeHandler()
                                                                                }
                                                                               }
                                                                               checked={borrowerState.propertyUsageId == id}
                                                                               id={id}
                                                                               value={`${id}`}/>
                                                    </>
                                                )
                                            })
                                        )}

                                </div>
                            </div>
                        </div>

                        <div className="form-group list-otherOption-wrap">
                            {(borrowerState.propertyUsageId == 1 && borrowerState?.borrowers?.map(({ id, firstName, willLiveIn }, index) => {
                                return (
                                    <>
                                        <div className="list-otherOption-useProperty">
                                            <div className="row">
                                                <div className="col-sm-9">
                                                    <div className="lbl-radio">
                                                        Will {StringServices.capitalizeFirstLetter(firstName)} live here with you?
                                                        </div>

                                                    </div>
                                                    <div className="col-sm-3">
                                                        <div className="inline-radio">
                                                            <InputRadioBox
                                                                id={`borrowers[${id}willLiveInTrue`}
                                                                className=""
                                                                value={'true'}
                                                                register={register}
                                                                checked={willLiveIn == true}
                                                                name={`borrowers[${id}]`}
                                                                onChange={(e) => toggleRadio(index, e)}
                                                            >Yes</InputRadioBox>

                                                            <InputRadioBox
                                                                id={`borrowers[${id}willLiveInFalse`}
                                                                className=""
                                                                value={'false'}
                                                                register={register}
                                                                checked={willLiveIn == false}
                                                                name={`borrowers[${id}]`}
                                                                onChange={(e) => toggleRadio(index, e)}
                                                            >No</InputRadioBox>
                                                        </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </>
                                )
                            })
                            )}
                        </div>


                        <div className="form-footer">
                            <button
                                className="btn btn-primary"
                                type="submit"
                                disabled={isButtonDisabled}

                            >
                                {"Save & Continue"}
                            </button>
                        </div>

                    </div>
                </form>
            </div>
        </>
    )
}

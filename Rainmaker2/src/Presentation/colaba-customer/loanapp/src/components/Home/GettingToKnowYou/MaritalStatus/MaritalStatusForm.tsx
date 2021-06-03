import React, { useEffect, useState } from 'react';
import PageHead from '../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../Shared/Components/TooltipTitle';

import InputRadioBox from '../../../../Shared/Components/InputRadioBox';
import { yupResolver } from '@hookform/resolvers/yup';
import { useForm } from 'react-hook-form';
import * as yup from "yup";

import Errors from '../../../../Utilities/ErrorMessages';
import MaritalStatusBusinessRules from './MaritalStatusBusinessRules';
import { StringServices } from '../../../../Utilities/helpers/StringServices';

export const MaritalStatusForm = ({ allMaritialOptions, setcurrentStep, formOnSubmit, currentMaritalStatus = {}, loanManager }: any) => {
    const { maritalStatus, marriedToPrimary } = currentMaritalStatus || {};
    const [toggleSpouseComp, setToggleSpouseComp] = useState(false);
    const [shouldReRunUseEfect, setShouldReRunUseEfect] = useState('');

    useEffect(() => {
        (async () => {
            setToggleSpouseComp(await MaritalStatusBusinessRules.shouldAskIfCurrentBorrowerIsSpouse(loanManager, Number(watchMaritalStatus)));
        })()
    }, [shouldReRunUseEfect])

    let validationSchema = yup.object().shape({
        maritalStatus: yup.number().required(Errors.MARITAL_STATUS_SCREEN.SELECT_MARITAL_STATUS).nullable(),
        isPrimaryBorrowerSpouse: yup.boolean().nullable(),
    }).test(

        "MaritalStatus",
        null,
        (maritalStatusSchemaTest) => {
            const { isPrimaryBorrowerSpouse, maritalStatus } = maritalStatusSchemaTest;
            
            if (maritalStatus === 1 && (isPrimaryBorrowerSpouse == null || isPrimaryBorrowerSpouse == undefined) && toggleSpouseComp) {
                return new yup.ValidationError(Errors.MARITAL_STATUS_SCREEN.SELECT_YES_NO, null, "isPrimaryBorrowerSpouse");
            }
            else
                return true;
        }
    );

    const { register, handleSubmit, watch, errors, getValues } = useForm({
        resolver: yupResolver(validationSchema),
        defaultValues: { maritalStatus: String(maritalStatus), isPrimaryBorrowerSpouse: String(marriedToPrimary) },
        mode: "onChange",
        reValidateMode: "onChange",
    });
    const watchMaritalStatus = watch("maritalStatus");
    if (shouldReRunUseEfect !== watchMaritalStatus) {
        setShouldReRunUseEfect(watchMaritalStatus);
    }
    const getTitle = () => {
        if(loanManager){
            if(loanManager.loanInfo.ownTypeId === 2){
                return `Great! Please tell us about ${StringServices.capitalizeFirstLetter(loanManager.loanInfo.borrowerName)}'s marital status.`
            }
            else{
                return `Great! Please tell us about your marital status.`
            }
        }
        return "";
    }
    return (
        <div className="compo-abt-yourSelf fadein">
            <PageHead title="Personal Information" handlerBack={() => { setcurrentStep('about_current_home') }} />
            <TooltipTitle title={getTitle()} />

            <div className="comp-form-panel colaba-form">
                <div className="row form-group">
                    <form onSubmit={handleSubmit(formOnSubmit)} className="col-md-12 MaritalStatusForm">
                        <div className="clearfix">
                            <div className="form-group">
                                <h4>Select {(loanManager?.loanInfo?.ownTypeId === 1)? 'your': StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)+"'s" } marital status</h4>
                                <>
                                    {(allMaritialOptions.length > 0
                                        &&
                                        allMaritialOptions.map(({ id, name }) => (
                                            <div className="clearfix">
                                                <InputRadioBox
                                                    key={id}
                                                    value={`${id}`}
                                                    className=""
                                                    name="maritalStatus"
                                                    data-testid={`marital-status-checkbox-${id}`}
                                                    register={register}
                                                    defaultChecked={getValues("maritalStatus") == id}
                                                >{name}
                                                </InputRadioBox>
                                            </div>
                                        ))
                                    )}
                                    {errors?.maritalStatus && <span className="form-error no-padding" role="alert" data-testid="maritalStatus-error">{errors?.maritalStatus?.message}</span>}
                                    
                                </>
                            </div>
                        </div>
                        
                        {toggleSpouseComp &&
                            (<div className="col-md-12" >
                                <div className="form-group">
                                    <hr />
                                    <h4>Is {StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)} your spouse?</h4>
                                    <div className="clearfix">
                                    <InputRadioBox
                                        id="isPrimaryBorrowerSpouse"
                                        className=""
                                        name="isPrimaryBorrowerSpouse"
                                        value="true"
                                        register={register}
                                    >Yes</InputRadioBox>
                                    </div>
                                    <div className="clearfix">
                                    <InputRadioBox
                                        id="isPrimaryBorrowerSpouse"
                                        className=""
                                        name="isPrimaryBorrowerSpouse"
                                        value="false"
                                        register={register}
                                    >No</InputRadioBox>
                                    </div>
                                    {errors?.isPrimaryBorrowerSpouse && <span className="form-error" role="alert" data-testid="maritalStatus-error">{errors?.isPrimaryBorrowerSpouse?.message}</span>}
                                </div></div>)}

                        <button className="btn btn-primary" type="submit" data-testid="marital-stats-submit">Save & Continue</button>
                    </form>
                </div>
            </div>
        </div>
    )
}

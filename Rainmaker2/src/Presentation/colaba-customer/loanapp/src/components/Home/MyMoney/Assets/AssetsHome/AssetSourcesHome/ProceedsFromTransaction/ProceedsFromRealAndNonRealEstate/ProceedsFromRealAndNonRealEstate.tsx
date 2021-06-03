import React, { useState,useEffect} from 'react'

import InputField from "../../../../../../../../Shared/Components/InputField";
import {CommaFormatted} from "../../../../../../../../Utilities/helpers/CommaSeparteMasking";



import {useForm} from "react-hook-form";

export const ProceedsFromRealAndNonRealEstate = ({transactionProceedsDTO,updateFormValuesOnChange}) => {
    
    const [expectedProceeds, setExpectedProceeds] = useState<string>(transactionProceedsDTO?.AssetValue);
    const [assetDescription, setAssetDescription] = useState<string>(transactionProceedsDTO?.Description);
    const [isClicked, setIsClicked] = useState<boolean>(false);
    const {
        register,
        errors,
        handleSubmit,
        clearErrors,
        setError
    } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    useEffect(() => {
        setDefault();
    }, []);

    const setDefault = async () => {
        console.log("transactionProceedsDTO  ==== ",transactionProceedsDTO);
    }
    const onSubmit = async (data) => {
        if(!isClicked){
            setIsClicked(true);
            updateFormValuesOnChange(data)
        }
    };

    return (
        <React.Fragment>
            
            <form
                id="net-annual-income-form"
                data-testid="net-annual-income-form"
                onSubmit={handleSubmit(onSubmit)}
                autoComplete="off">
                <div className="p-body">
                    <div className="row">
                        <div className="col-sm-6">
                            <InputField
                                label={"Expected Proceeds"}
                                data-testid={"expected-proceeds"}
                                id={"expected-proceeds"}
                                name="expectedProceeds"
                                icon={<i className="zmdi zmdi-money"></i>}
                                value={expectedProceeds ? CommaFormatted(expectedProceeds) : ''}
                                type={'text'}
                                placeholder={"Amount"}
                                register={register}
                                rules={{
                                    required: "This field is required.",
                                }}
                                errors={errors}
                                onBlur={() => {
                                    let netAnlIncome = Number(expectedProceeds).toFixed(2);
                                    if (netAnlIncome === "NaN") return;

                                    else if (+expectedProceeds == 0.0 || expectedProceeds == undefined)
                                        setError("expectedProceeds", {
                                            type: "server",
                                            message: "This field is required.",
                                        });

                                    else
                                        setExpectedProceeds(CommaFormatted(String(netAnlIncome)));
                                }}
                                onChange={(event: React.FormEvent<HTMLInputElement>) => {
                                    clearErrors("expectedProceeds")
                                    const value = event.currentTarget.value;
                                    if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
                                        return false;
                                    }
                                    setExpectedProceeds(value.replace(/\,/g, ''))
                                    return true;
                                }}
                            />
                        </div>
                        <div className="col-sm-6">
                            <InputField
                                label={"Asset Description"}
                                data-testid={"asset-description"}
                                id={"asset-description"}
                                name="assetDescription"
                                type={"text"}
                                placeholder={"Description"}
                                onChange={(e) => {
                                    clearErrors("assetDescription");
                                    if (e.target.value.length > 0 && !/^[a-zA-Z0-9%&(.'\-\s]{1,150}$/g.test(e.target.value)) {
                                        return false
                                    }
                                    setAssetDescription(e.target.value);
                                    return true;
                                }}
                                value={assetDescription}
                                register={register}
                                rules={{
                                    required: "This field is required.",
                                }}
                                errors={errors}
                            />
                        </div>
                    </div>
                </div>
                <div className="p-footer">
                    <button className="btn btn-primary" type="submit" onChange={handleSubmit(onSubmit)}
                            disabled={isClicked}
                            data-testid="net-annual-income-submit">
                        SAVE ASSETS
                    </button>
                </div>
            </form>
        </React.Fragment>
    )
}

import React, { useEffect, useState } from 'react'
import InputField from "../../../../../../../../Shared/Components/InputField";
import { CommaFormatted } from "../../../../../../../../Utilities/helpers/CommaSeparteMasking";
import { useForm } from "react-hook-form";
import InputRadioBox from "../../../../../../../../Shared/Components/InputRadioBox";
import DropdownList from "../../../../../../../../Shared/Components/DropdownList";

import Dropdown from "react-bootstrap/Dropdown";
import Errors from '../../../../../../../../Utilities/ErrorMessages';
import Loader from '../../../../../../../../Shared/Components/Loader';

export const ProceedsFromLoan = ({ collateralAssetTypes, transactionProceedsDTO, updateFormValuesOnChange }) => {
    
    const [expectedProceeds, setExpectedProceeds] = useState<string>(transactionProceedsDTO?.AssetValue);
    const [assetDescription, setAssetDescription] = useState<string>(transactionProceedsDTO?.CollateralAssetDescription);

    const [securedByAnAsset, setSecuredByAnAsset] = useState<boolean>(transactionProceedsDTO?.SecuredByColletral);
    
    const [selectedAssetType, setSelectedAssetType] = useState(transactionProceedsDTO?.collateralAssetName);
    const [selectedAssetTypeId, setSelectedAssetTypeId] = useState<number>(transactionProceedsDTO?.ColletralAssetTypeId);
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
        console.log("collateralAssetTypes, transactionProceedsDTO, updateFormValuesOnChange  ==== ",collateralAssetTypes, transactionProceedsDTO, updateFormValuesOnChange)
        if (transactionProceedsDTO?.assetType) {
            let selectedIncomeType = await collateralAssetTypes && collateralAssetTypes?.filter((assetType) => assetType.name === transactionProceedsDTO?.assetType)[0];
            setSelectedAssetTypeId(selectedIncomeType.id);
        }
        
    }

    const onSubmit = async (data) => {
        if (!data.securedByAnAsset) {
            setError("securedByAnAsset", {
                type: "server",
                message: Errors.MILITARY_SCREEN_ERRORS.SELECT_YES_NO,
            });
            return;
        }
        if(!isClicked){
            setIsClicked(true);
            updateFormValuesOnChange({ ...data,"selectedAssetTypeId":selectedAssetTypeId });}
    }
    const onAssetTypeSelection = async (incomeTypeId: string | number) => {
        let selectedIncomeType = await collateralAssetTypes && collateralAssetTypes?.filter((assetType) => assetType.id === +incomeTypeId)[0]
        setSelectedAssetType(selectedIncomeType.name);
        setSelectedAssetTypeId(selectedIncomeType.id);
        clearErrors("assetType")
    }

    if (!collateralAssetTypes)
        return <Loader type="widget"/>

    else
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
                                    label={"Proceeds"}
                                    data-testid={"net-annual-income"}
                                    id={"net-annual-income"}
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
                        </div>


                        <div className="form-group">
                            <h4>Was this loan secured by an asset (e.g. your car or a bank account)?</h4>
                        </div>

                        <div className="row">
                            <div className="col-sm-6">
                                <div className="clearfix">
                                    <InputRadioBox
                                        key='Yes'
                                        value='Yes'
                                        className=""
                                        name="securedByAnAsset"
                                        data-testid={`proceding-checkbox-yes`}
                                        register={register}
                                        defaultChecked={securedByAnAsset == true}
                                        onChange={() => {
                                            setSecuredByAnAsset(true)
                                            clearErrors("securedByAnAsset");
                                        }
                                        }
                                    >Yes
                            </InputRadioBox>
                                </div>


                            </div>
                        </div>


                        {securedByAnAsset &&
                            <div className="intended-feild">
                                <div className="row">
                                    <div className="col-md-6">
                                        <DropdownList
                                            label={"Which asset?"}
                                            data-testid="asset-type"
                                            id="asset-type"
                                            placeholder="Asset Type"
                                            name="assetType"
                                            onDropdownSelect={onAssetTypeSelection}
                                            value={selectedAssetType}
                                            register={register}
                                            rules={{
                                                required: "This field is required.",
                                            }}
                                            errors={errors}>
                                            {collateralAssetTypes && collateralAssetTypes?.map((ca: any) => (
                                                <Dropdown.Item data-testid={"assetType-options"} key={ca.id}
                                                    eventKey={ca.id}>
                                                    {ca.name}
                                                </Dropdown.Item>
                                            ))}
                                        </DropdownList>
                                    </div>
                                    <div className="col-md-6">
                                        {selectedAssetType == 'Other' &&

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
                                                    required: "Please select one.",
                                                }}
                                               
                                                errors={errors}
                                            />
                                        }
                                    </div>
                                </div>


                            </div>

                        }

                        <div className="clearfix">
                            <InputRadioBox
                                key='No'
                                value='No'
                                className=""
                                name="securedByAnAsset"
                                data-testid={`proceding-checkbox-no`}
                                register={register}
                                defaultChecked={securedByAnAsset == false}
                                onChange={() => {
                                    clearErrors("securedByAnAsset");
                                    setSecuredByAnAsset(false)
                                }}
                                rules={{
                                    required: "Please select one.",
                                }}
                            >No
                            </InputRadioBox>
                            {errors.securedByAnAsset && <span className="form-error" role="alert" data-testid="securedByAnAsset-error">{errors.securedByAnAsset.message}</span>}
                        </div>
                    </div>
                    <div className="p-footer">
                        <button className="btn btn-primary" type="submit" onChange={handleSubmit(onSubmit)} disabled={isClicked}
                            data-testid="net-annual-income-submit">
                            SAVE ASSETS
                    </button>
                    </div>
                    
                </form>
            </React.Fragment>
        )
}

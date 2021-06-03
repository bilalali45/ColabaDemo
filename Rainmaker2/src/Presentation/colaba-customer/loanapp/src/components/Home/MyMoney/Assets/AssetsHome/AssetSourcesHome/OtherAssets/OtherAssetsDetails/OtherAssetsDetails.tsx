import React, { useContext, useEffect, useState } from 'react'
import DropdownList from '../../../../../../../../Shared/Components/DropdownList'
import Dropdown from "react-bootstrap/Dropdown";
import InputField from '../../../../../../../../Shared/Components/InputField';
import OtherAssetsActions from '../../../../../../../../store/actions/OtherAssetsActions';
import { ErrorHandler } from '../../../../../../../../Utilities/helpers/ErrorHandler';
import { Store } from '../../../../../../../../store/store';
import { RegisterOptions, useForm } from "react-hook-form";
import { NumberServices } from '../../../../../../../../Utilities/helpers/NumberServices';
import { LocalDB } from '../../../../../../../../lib/LocalDB';
import { NavigationHandler } from '../../../../../../../../Utilities/Navigation/NavigationHandler';
import { ApplicationEnv } from '../../../../../../../../lib/appEnv';
import { AssetInfo } from 'webpack';

export type AssetTypesByCategoryProto = {
    id: number,
    assetCategoryId: number,
    name: string,
    displayName: string,
    tenantAlternateName: string | null,
    fieldsInfo: string
}
export type GetOtherAssetsInfoproto = {
    assetId: number
    assetTypeId: number
    assetTypeName: string
    assetValue: string
    assetDescription: string
    institutionName: string
    accountNumber: string
}

export type FieldsInfoProto = {
    Enabled: boolean
    caption: string
    datatype: string
    displayOrder: number
    maxLength: number
    name: string
    regex: string | null
    icon: string | null
}

export type FieldsInfoPayload = {
    AssetTypeId: number
    Value: number
    InstitutionName: string
    AccountNumber: string
    Description: string
    AssetId: number
    BorrowerId: number
}

export class AssetTypesByCategoryDTO {
    id: number
    assetCategoryId: number
    name: string
    displayName: string
    tenantAlternateName: string | null
    fieldsInfo: Array<FieldsInfoDTO>
    constructor(
        id: number,
        assetCategoryId: number,
        name: string,
        displayName: string,
        tenantAlternateName: string | null,
        fieldsInfo: Array<FieldsInfoDTO>) {
        this.id = id;
        this.assetCategoryId = assetCategoryId;
        this.name = name;
        this.displayName = displayName;
        this.tenantAlternateName = tenantAlternateName;
        this.fieldsInfo = fieldsInfo;
    }
}

export class FieldsInfoDTO {
    Enabled: boolean
    caption: string
    datatype: string
    displayOrder: number
    maxLength: number
    name: string
    regex: string | null
    icon: string | null
    value: string | null
    placeHolder: string | null
    rules: RegisterOptions | null = null
    onBlur: Function | null = () => {}
    onFocus: Function | null= () => {}
    onChange: Function | null = () => {}
    formatter: Function | null = () => {}

    constructor(
        Enabled: boolean,
        caption: string,
        datatype: string,
        displayOrder: number,
        maxLength: number,
        name: string,
        regex: string | null,
        icon: string | null,
        value: string | null,
        placeHolder: string | null) {

        this.Enabled = Enabled;
        this.caption = caption;
        this.datatype = datatype;
        this.displayOrder = displayOrder;
        this.maxLength = maxLength;
        this.name = name;
        this.regex = regex;
        this.icon = icon;
        this.value = value;
        this.placeHolder = placeHolder;
    }
}



export const OtherAssetsDetails = () => {
    const [otherAssetData, setOtherAssetData] = useState<AssetTypesByCategoryDTO[]>([]);
    const [isClicked, setIsClicked] = useState<boolean>(false);
    const [selectedOtherAssetType, setSelectedOtherAssetType] = useState<AssetTypesByCategoryDTO>();
    const [otherAssetsInfo, setOtherAssetsInfo] = useState<GetOtherAssetsInfoproto>();

    const { state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const assetInfo: AssetInfo = loanManager.assetInfo;
    let borrowerId = Number(LocalDB.getBorrowerId());
    let borrowerAssetId = assetInfo?.borrowerAssetId;
    let assetCategoryId = assetInfo?.assetCategoryId;


    const {
        register,
        errors,
        handleSubmit,
        clearErrors,
        watch
    } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    useEffect(() => {
        if (!assetInfo) {
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources`);
        }
    }, []);
    useEffect(() => {
        if ((!otherAssetData || otherAssetData.length == 0) && !borrowerAssetId) {
            populateDataAssetList();
        }
    }, [])
    useEffect(() => {
        if (borrowerAssetId && !otherAssetsInfo) {
            populateDataWhenAvailable();
        }
    }, [borrowerAssetId, otherAssetsInfo])

    useEffect(() => {
        clearErrors();
    }, [selectedOtherAssetType])


    useEffect(() => {
        if (otherAssetsInfo && otherAssetData && !selectedOtherAssetType) {
            onAssetTypeSelection(otherAssetsInfo.assetTypeId)
        }
    }, [otherAssetData, otherAssetsInfo])


    const onAssetTypeSelection = async (incomeTypeId: string | number) => {
        let selectedIncomeType = await otherAssetData && otherAssetData?.filter((businessType) => businessType.id === +incomeTypeId)[0]
        setSelectedOtherAssetType(selectedIncomeType);
        clearErrors("assetType")
    }
    const populateDataWhenAvailable = async () => {
        let otherAssetsData: GetOtherAssetsInfoproto = await poupulateData();
        await populateDataAssetList(otherAssetsData);

        //let response = await Promise.all([poupulateData, populateDataAssetList]);
    }
    const poupulateData = async () => {
        if (borrowerAssetId) {
            let response = await OtherAssetsActions.GetOtherAssetsInfo(borrowerAssetId);
            if (response) {
                if (ErrorHandler.successStatus.includes(response.statusCode)) {
                    setOtherAssetsInfo(response.data);
                    return response.data;
                } else {
                    ErrorHandler.setError(dispatch, response);
                }
            }
        }
    }

    const populateDataAssetList = async (otherAssetsInfo?: GetOtherAssetsInfoproto) => {

        let response = await OtherAssetsActions.GetAssetTypesByCategory(assetCategoryId ? assetCategoryId : 7);
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                const arr: AssetTypesByCategoryDTO[] = [];
                response.data.forEach((r: AssetTypesByCategoryProto) => {
                    let fieldsInfoDTO: FieldsInfoDTO[] = JSON.parse(r?.fieldsInfo)?.fieldsInfo;
                    fieldsInfoDTO.forEach((f: FieldsInfoDTO) => {
                        if (otherAssetsInfo?.assetTypeId === r.id) {

                            if (f.name == "InstitutionName")
                                f.value = otherAssetsInfo?.institutionName;

                            if (f.name == "AccountNumber")
                                f.value = otherAssetsInfo?.accountNumber;

                            if (f.name == "Value")
                                f.value = NumberServices.formateNumber(otherAssetsInfo?.assetValue);

                            if (f.name == "Description")
                                f.value = otherAssetsInfo?.assetDescription;

                        }
                    });
                    let assetTypesByCategoryDTO: AssetTypesByCategoryDTO = new AssetTypesByCategoryDTO(r.id, r.assetCategoryId, r.name, r.displayName, r.tenantAlternateName, fieldsInfoDTO)
                    // let values = [...otherAssetData, assetTypesByCategoryDTO];
                    arr.push(assetTypesByCategoryDTO)
                });
                setOtherAssetData([...arr]);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }
    const onSubmit = async (data) => {
        let formdata = { ...data, "assetTypeId": selectedOtherAssetType?.id, "Value": NumberServices.removeCommas(data.Value), "BorrowerId": borrowerId, "AssetId": borrowerAssetId ? borrowerAssetId : null }

        console.log("data >>>>>", formdata);
        if (!isClicked) {
            setIsClicked(true)
            let response: any = await OtherAssetsActions.AddOrUpdateOtherAssetsInfo(formdata);
            if (response && response.data) {
                if (ErrorHandler.successStatus.includes(response.statusCode)) {
                    console.log("response.data >>>>>>>>>>", response.data);
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources`);
                }
                else {
                    ErrorHandler.setError(dispatch, response);
                }
            }
        }
    };
    const updateFormData = async (id, value, dataObj) => {
        const arr: AssetTypesByCategoryDTO[] = [...dataObj];
        let ids = id.split('-');
        arr.forEach((r: AssetTypesByCategoryDTO) => {
            if (r.id == ids[1]) {
                r.fieldsInfo.forEach((fi: FieldsInfoDTO) => {
                    if (fi.displayOrder == ids[2]) {
                        fi.value = value;
                    }
                })
            }
        });
        setOtherAssetData([...arr]);
    }

    const watchFields = watch(["selectedOtherAssetType"]);
    return (
        <div className="OtherAssetsDetails">
            <form id="employer-info-form" data-testid="employer-info-form" onSubmit={handleSubmit(onSubmit)} autoComplete="off">
                <div className="p-body">
                    <div className="row">
                        <div className="col-md-6">
                            <DropdownList
                                label={"What type of asset is this?"}
                                data-testid="asset-type"
                                id="asset-type"
                                placeholder="Account Type"
                                name="assetType"
                                onDropdownSelect={onAssetTypeSelection}
                                value={selectedOtherAssetType?.name}
                                register={register}
                                rules={{
                                    required: "This field is required.",
                                }}
                                errors={errors}
                            >
                                {otherAssetData && otherAssetData.map((otherAssetsTypes: any) => (
                                    <Dropdown.Item data-testid={"assetType-options"} key={otherAssetsTypes.id}
                                        eventKey={otherAssetsTypes.id}>
                                        {otherAssetsTypes.name}
                                    </Dropdown.Item>
                                ))}
                            </DropdownList>
                        </div>
                    </div>
                    <div className="row">
                        {watchFields && selectedOtherAssetType?.fieldsInfo &&
                            selectedOtherAssetType?.fieldsInfo.filter((f: FieldsInfoDTO) => f.Enabled == true).sort((f1: FieldsInfoDTO, f2: FieldsInfoDTO) => {
                                if (f1.displayOrder < f2.displayOrder) return -1;
                                if (f1.displayOrder > f2.displayOrder) return 1;
                                return 0;
                            }).map(
                                (f: FieldsInfoDTO) => {
                                    let id = `${f.name}-${selectedOtherAssetType?.id}-${f.displayOrder}`;
                                    return <div className="col-md-6">
                                        <InputField

                                            icon={f.icon ? <i className="zmdi zmdi-money"></i> : ''}
                                            name={f.name}
                                            id={id}
                                            label={f.caption}
                                            placeholder={f?.placeHolder}
                                            value={f?.value}
                                            data-testid={id}
                                            onChange={(e) => {

                                                let key = e.nativeEvent.inputType;
                                                let value = (f.formatter) ? NumberServices.formateNumber(e.target.value) : e.target.value;
                                                // Don't validate the input if below  delete and backspace keys were pressed 
                                                console.log(key)
                                                if (!(value == "" || key == "deleteContentForward" || key == "deleteContentBackward") && f.regex && !(new RegExp(f.regex)).test(value))
                                                    return;

                                                else {
                                                    updateFormData(e.target.id, value, otherAssetData);
                                                    clearErrors(f.name)
                                                }

                                                /* f.value = e.target.value; */
                                                /* let tempFieldsData = f;
                                                tempFieldsData.
                                                let temp = [...otherAssetData]; */
                                                //setBusinessName(e.target.value);
                                                //clearErrors("businessName");
                                            }}
                                            register={register}
                                            rules={f.rules}
                                            onBlur={
                                                (e) => {
                                                    if (f.onBlur)
                                                        updateFormData(e.target.id,
                                                            NumberServices.curruncyFormatterIncomeHome(+NumberServices.addProceedingZeros(f.value)), otherAssetData);
                                                }
                                            }
                                            onFocus={
                                                () => {
                                                    /* if (f.onFocus)
                                                    updateFormData(e.target.id,
                                                        removeCommaFormatting(+NumberServices.addProceedingZeros(f.value)), otherAssetData); */
                                                }
                                            }
                                            errors={errors}
                                        /></div>;
                                }
                            )
                        }
                    </div>
                </div>

                <div className="form-footer">
                    <button
                        data-testid="saveBtn"
                        disabled={false}
                        className="btn btn-primary float-right"
                        onClick={() => { }}
                    >
                        {"Save Assets"}
                    </button>
                </div>

            </form>
        </div>
    )
}

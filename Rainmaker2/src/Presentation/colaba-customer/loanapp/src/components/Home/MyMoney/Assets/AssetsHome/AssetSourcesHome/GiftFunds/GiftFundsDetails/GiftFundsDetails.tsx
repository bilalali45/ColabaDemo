import React, { useContext, useState, useEffect } from 'react';
import { useForm, Controller } from 'react-hook-form';
import { LoanInfoType, GiftAsset, AssetInfo } from '../../../../../../../../Entities/Models/types';
import { LocalDB } from '../../../../../../../../lib/LocalDB';
import IconRadioBox from '../../../../../../../../Shared/Components/IconRadioBox';
import InputField from '../../../../../../../../Shared/Components/InputField';
import InputRadioBox from '../../../../../../../../Shared/Components/InputRadioBox';
import { IconCashGift, IconGiftOfEquity } from '../../../../../../../../Shared/Components/SVGs';
import { Store } from '../../../../../../../../store/store';
import { ErrorHandler } from '../../../../../../../../Utilities/helpers/ErrorHandler';
import { NavigationHandler } from '../../../../../../../../Utilities/Navigation/NavigationHandler';
import GiftAssetActions from '../../../../../../../../store/actions/GiftAssetActions';
import InputDatepicker from '../../../../../../../../Shared/Components/InputDatepicker';
import { CommaFormatted } from '../../../../../../../../Utilities/helpers/CommaSeparteMasking';
import moment from 'moment';
import { ApplicationEnv } from '../../../../../../../../lib/appEnv';

export const GiftFundsDetails = () => {
    const {
        register,
        errors,
        handleSubmit,
        control
    } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    const assetTypes = {
        cashGift: 10,
        grant: 11,
        giftOfEquity: 26
    };

    const relativeGiftFundSourceId = 8;

    const { state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const assetsManager: any = state.assetsManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;
    const assetInfo: AssetInfo = loanManager.assetInfo;

    const [giftFundSourceId, setGiftFundSourceId] = useState<number | null>(assetsManager.giftFundSourceId);
    const [assetTypeId, setAssetTypeId] = useState<number | null>(null);
    const [value, setValue] = useState<number | null>(null);
    const [isDeposited, setIsDeposited] = useState<boolean | null>(null);
    const [valueDate, setValueDate] = useState<any>(null);

    const [isFormSubmitted, setIsFormSubmitted] = useState<boolean>(false);

    useEffect(() => {
        if (!assetInfo) {
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources`);
        }
        else {
            loadGiftFundDetails();
        }

    }, [assetInfo])

    const loadGiftFundDetails = async () => {

        if (!assetInfo || !assetInfo.borrowerAssetId || !loanInfo.borrowerId) {
            return;
        }

        const response = await GiftAssetActions.GetGiftAsset(assetInfo.borrowerAssetId, loanInfo.borrowerId, loanInfo?.loanApplicationId);

        if (response) {
            setGiftFundSourceId(response.data.giftSourceId);
            setAssetTypeId(response.data.assetTypeId);
            setValue(response.data.value);
            setValueDate(new Date(response.data.valueDate));
            setIsDeposited(response.data.isDeposited);
        }
    }

    const addOrUpdateGiftFundDetails = async () => {
        if (!isFormSubmitted) {
            setIsFormSubmitted(true);

            let model: GiftAsset = {
                id: assetInfo.borrowerAssetId,
                loanApplicationId: +LocalDB.getLoanAppliationId(),
                borrowerId: Number(loanInfo.borrowerId),
                assetTypeId: assetTypeId ,
                giftSourceId: giftFundSourceId,
                isDeposited: isDeposited,
                value: Number(value?.toString().replace(/\,/g, '')),
                valueDate: valueDate ? moment(valueDate).format('YYYY-MM-DD') : null,
                state: NavigationHandler.getNavigationStateAsString(),
            };

            const res = await GiftAssetActions.AddOrUpdateGiftAsset(model);

            if (res) {
                if (ErrorHandler.successStatus.includes(res.statusCode)) {
                    NavigationHandler.moveNext();
                }
                else {
                    ErrorHandler.setError(dispatch, res);
                    setIsFormSubmitted(false);
                }
            }
        }
    }

    const changeAssetType = (assetTypeId: number) => {
        setAssetTypeId(assetTypeId);
        setIsDeposited(null);
        setValueDate(null);
    }

    const isDisabled = () => {
        if (isFormSubmitted) {
            return true;
        }

        if (!assetTypeId) {
            return true;
        }

        if (!value) {
            return true;
        }

        if (assetTypeId === assetTypes.cashGift && (isDeposited === null || !valueDate)) {
            return true;
        }

        return false;
    }

    return (
        <div>
            <div className="form-group">
                <h3 data-testid='title' className="h3">What type of gift was this?</h3>
            </div>
            <form
                id="employer-info-form"
                data-testid="employer-info-form"
                onSubmit={handleSubmit(addOrUpdateGiftFundDetails)}
                autoComplete="off">
                <div className="p-body">
                    <div className="row">
                        <div className="col-md-6">
                            <IconRadioBox
                                id={assetTypes.cashGift}
                                data-testid="cash-gift"
                                className=""
                                name="radio1"
                                value={"Cash Gift"}
                                title="Cash Gift"
                                Icon={<IconCashGift />}
                                checked={assetTypeId === assetTypes.cashGift}
                                handlerClick={(id) => changeAssetType(id)}
                            />
                        </div>
                        <div className="col-md-6">
                            {
                                giftFundSourceId === relativeGiftFundSourceId ?
                                    <IconRadioBox
                                        id={assetTypes.giftOfEquity}
                                        className=""
                                        name="radio1"
                                        value={"Gift Of Equity"}
                                        title={"Gift Of Equity"}
                                        Icon={<IconGiftOfEquity />}
                                        checked={assetTypeId === assetTypes.giftOfEquity}
                                        handlerClick={(id) => changeAssetType(id)}
                                    />
                                    :
                                    <IconRadioBox
                                        id={assetTypes.grant}
                                        data-testid="grant"
                                        className=""
                                        name="radio1"
                                        value={"Grant"}
                                        title={"Grant"}
                                        Icon={<IconGiftOfEquity />}
                                        checked={assetTypeId === assetTypes.grant}
                                        handlerClick={(id) => changeAssetType(id)}
                                    />
                            }
                        </div>
                    </div>
                    {assetTypeId
                        &&
                        <div>
                            <div className="row">
                                <div className="col-md-6">
                                    <InputField
                                        icon={<i className="zmdi zmdi-money"></i>}
                                        name={`value`}
                                        label={assetTypeId === assetTypes.cashGift ? `Cash Value` : `Market Value`}
                                        placeholder={`Amount`}
                                        value={CommaFormatted(value?.toString())}
                                        onChange={(e) => {
                                            let value = e.target.value;
                                            if (value.length > 0 && !/^[0-9,]{1,11}$/g.test(value)) {
                                                return false;
                                            }

                                            if (Number(value) < 1) {
                                                setValue(null);
                                                return false;
                                            }

                                            setValue(value.replace(/\,/g, ''))
                                            return true;
                                        }}
                                        register={register}
                                    />
                                </div>
                            </div>
                            {assetTypeId === assetTypes.cashGift
                                &&
                                <div>
                                    <div className="form-group">
                                        <h2 data-testid="cash-hasdeposited">Has this gift been deposited already?</h2>
                                    </div>

                                    <div className="clearfix">
                                        <InputRadioBox
                                            id=""
                                            data-testid="isDeposited"
                                            className=""
                                            name="addressCheck"
                                            register={register}
                                            value={"Yes"}
                                            checked={isDeposited}
                                            rules={{
                                                required: "Please select one"
                                            }}
                                            onChange={() => { setIsDeposited(true); setValueDate(null); }}
                                        >Yes</InputRadioBox>
                                    </div>

                                    {isDeposited
                                        && <div className="row">
                                            <div className="step-three col-md-7">
                                                <div className={`intended-feild`}>
                                                    {<Controller
                                                        control={control}
                                                        name="dateOfTransfer"
                                                        render={() => (
                                                            <InputDatepicker
                                                                label={'Date of Transfer'}
                                                                dateFormat="MM/dd/yyyy"
                                                                name="valueDate"
                                                                autoComplete={'off'}
                                                                selected={valueDate}
                                                                isPreviousDateAllowed={true}
                                                                isFutureDateAllowed={false}
                                                                handleOnChange={(date: any) => setValueDate(date)}
                                                                handleDateSelect={(date: any) => setValueDate(date)}
                                                                errors={errors}
                                                            />
                                                        )}
                                                    />}
                                                </div>
                                            </div>
                                        </div>
                                    }

                                    <div className="clearfix">
                                        <InputRadioBox
                                            id=""
                                            className=""
                                            name="addressCheck"
                                            register={register}
                                            onChange={() => { setIsDeposited(false); setValueDate(null); }}
                                            checked={isDeposited === false}
                                            value={"No"}
                                            rules={{
                                                required: "Please select one"
                                            }}
                                        >No</InputRadioBox>
                                    </div>


                                    {(isDeposited === false)
                                        && <div className="row">
                                            <div className="step-three col-md-7">
                                                <div className={`intended-feild`}>
                                                    {<Controller
                                                        control={control}
                                                        name="expectedDateOfTransfer"
                                                        render={() => (
                                                            <InputDatepicker
                                                                label={'Expected Date of Transfer'}
                                                                dateFormat="MM/dd/yyyy"
                                                                name="valueDate"
                                                                autoComplete={'off'}
                                                                selected={valueDate}
                                                                isPreviousDateAllowed={false}
                                                                isFutureDateAllowed={true}
                                                                handleOnChange={(date: any) => setValueDate(date)}
                                                                handleDateSelect={(date: any) => setValueDate(date)}
                                                                errors={errors}
                                                            />
                                                        )}
                                                    />}
                                                </div>
                                            </div>
                                        </div>
                                    }
                                </div>
                            }
                        </div>
                    }
                </div>
            </form>
            <div className="form-footer">
                <button
                    data-testid="saveBtn"
                    disabled={isDisabled()}
                    className="btn btn-primary float-right"
                    onClick={handleSubmit(addOrUpdateGiftFundDetails)}
                >
                    {"Save Assets"}
                </button>
            </div>
        </div>
    )
}

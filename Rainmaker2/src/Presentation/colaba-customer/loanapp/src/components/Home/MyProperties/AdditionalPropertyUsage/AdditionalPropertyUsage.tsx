import React, { ChangeEvent, Fragment, useContext, useEffect, useState } from 'react';
import PageHead from '../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../Shared/Components/TooltipTitle';
import {
    IconPropertyDoc
} from '../../../../Shared/Components/SVGs';
import IconRadioBox from '../../../../Shared/Components/IconRadioBox';
import InputField from '../../../../Shared/Components/InputField';
import { useForm } from 'react-hook-form';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';
import MyPropertyActions from '../../../../store/actions/MyPropertyActions';
import { LocalDB } from '../../../../lib/LocalDB';
import { CommaFormatted, removeCommaFormatting } from '../../../../Utilities/helpers/CommaSeparteMasking';
import { Store } from '../../../../store/store';
import { SectionTypeEnum } from '../../../../Utilities/Enumerations/MyPropertyEnums';

type AdditionalPropertyUsageProps = {
    address: string
}
export const AdditionalPropertyUsage = ({ }: AdditionalPropertyUsageProps) => {

    const [propertyUsages, setPropertyUsages] = useState([]);
    const [propertyUsageForm, setPropertyUsageForm] = useState({});
    const { state } = useContext(Store);
    const { primaryBorrowerInfo }: any = state.loanManager;

    const { register, handleSubmit, watch, errors, getValues, setValue, clearErrors } = useForm({
        mode: "onChange",
        reValidateMode: "onSubmit",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });


    useEffect(() => {
        register('propertyUsage', {
            validate: (value) => {
                if (!value) {
                    return 'Please select one.'
                }
                return true;
            },
        });
        register('income', {
            validate: (value) => {
                if (getValues('propertyUsage') === 4) {
                    if (!value) {
                        return 'This field is required.';
                    }
                }
                return true;
            },
        });
    }, [register]);

    useEffect(() => {


        if (!propertyUsageForm['propertyUsage']) {
            getPropertyInfo();
        }

        if (!propertyUsages.length) {
            getAllPropertyUsages();
        }

        watch("propertyUsage");
        watch("investment");
    }, [])


    console.log('errors', errors);

    const handleChange = ({ target: { value, id } }) => {
        if (id === 'income') {
            value = removeCommaFormatting(value);
            if (isNaN(Number(value))) {
                return;
            }
        }

        setPropertyUsageForm(pre => {
            let values = { ...pre, [id]: value };
            if (value === 'home') {
                delete values['income']
            }
            return values;
        })
        let key = id.split('-')[1] || 'income';
        setValue(key, isNaN(value) ? value : Number(value));
        clearErrors();
    }

    const getAllPropertyUsages = async () => {
        try {
            let res = await MyPropertyActions.getAllPropertyUsages(SectionTypeEnum.AdditionalProperty);
            setPropertyUsages(res.data);
        } catch (error) {

        }

    }

    const getPropertyInfo = async () => {
        let loanApplicationId = LocalDB.getLoanAppliationId();
        let propertyId = LocalDB.getAddtionalPropertyTypeId();
        if (loanApplicationId && propertyId) {
            try {
                let res = await MyPropertyActions.getBorrowerAdditionalPropertyInfo(loanApplicationId, propertyId);
                if (res) {
                    setPropertyUsageForm(() => {
                        return {
                            income: res?.data?.rentalIncome,
                            propertyUsage: res?.data?.propertyUsageId,
                        }
                    })
                    setValue('income', Number(res?.data?.rentalIncome))
                    setValue('propertyUsage', Number(res?.data?.propertyUsageId))
                }
            } catch (error) {

            }
        }

    }

    const updatePropertyInfo = async (data) => {
        try {
            let res = await MyPropertyActions.addOrUpdateAdditionalPropertyInfo(data);
            if (res) {
                LocalDB.setAdditionalPropertyTypeId(res.data as number);
                NavigationHandler.moveNext();
            }
        } catch (error) {

        }
    }

    const onSubmit = (data) => {
        let loanApplicationId = LocalDB.getLoanAppliationId();
        let borrowerId = primaryBorrowerInfo.id; //LocalDB.getBorrowerId();
        let propertyId = LocalDB.getAddtionalPropertyTypeId() && LocalDB.getAddtionalPropertyTypeId() > 0 ? LocalDB.getAddtionalPropertyTypeId() : null;
        let values = {
            loanApplicationId,
            borrowerId,
            id: propertyId,
            propertyUsageId: data.propertyUsage,

        };
        if (data.income) {
            values['rentalIncome'] = data.income
        }
        updatePropertyInfo(values);
    }


    const submit = handleSubmit((data) => onSubmit(data));

    const renderpropertyUsage = () => {

        return (
            <Fragment>
                <div className="row form-group">
                    {
                        propertyUsages?.map(f => {
                            return (
                                <div className="col-md-6">
                                    <IconRadioBox
                                        onChange={handleChange}
                                        ref={register}
                                        className={""}
                                        id={'propertyUsage'}
                                        name="propertyUsage"
                                        checked={getValues('propertyUsage') === f.id}
                                        value={f.id}
                                        title={f.name}
                                        Icon={<IconPropertyDoc />}
                                    // Icon={<img src={f.image} />}

                                    />
                                </div>
                            )
                        })
                    }
                </div>
                {errors?.propertyUsage && <span className="form-error no-padding" role="alert" >{errors?.propertyUsage?.message}</span>}
            </Fragment>
        )

    }

    const renderRentalIncome = () => {
        return (
            <div className="row form-group">
                <div className="col-md-6">
                    <InputField
                        onChange={(e: ChangeEvent<HTMLInputElement>) => handleChange(e)}
                        value={CommaFormatted(propertyUsageForm['income'])}
                        ref={register}
                        label={"Current Rental Income"}
                        id={'income'}
                        name="income"
                        type={"text"}
                        icon={<i className="zmdi zmdi-money"></i>}
                        placeholder={"Monthly Rent"}
                        errors={errors}
                    />
                </div>

            </div>
        )
    }

    const renderFooter = () => {
        return (
            <div className="form-footer">
                <button data-testid="btn-save" className="btn btn-primary" type="submit" disabled={getValues('propertyUsage') ? false : true} >{'Save and continue'} </button>
            </div>
        )
    }

    return (
        <section>
            <div className="compo-myMoney-income fadein">
                <PageHead title="My Properties" handlerBack={() => { }} />
                <TooltipTitle title={`Excellent ${primaryBorrowerInfo?.name}! How do you plan on using this property?`} />
                <form className="comp-form-panel income-panel colaba-form no-minheight" onSubmit={submit}>

                    {
                        // renderHeader()
                    }

                    {
                        renderpropertyUsage()
                    }

                    {
                        getValues('propertyUsage') === 4 && renderRentalIncome()
                    }

                    {
                        renderFooter()
                    }
                </form>

            </div>
        </section>
    )
}

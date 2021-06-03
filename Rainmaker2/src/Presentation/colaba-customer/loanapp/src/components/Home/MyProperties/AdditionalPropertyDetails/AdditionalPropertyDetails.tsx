import React, { ChangeEvent, useEffect, useState } from 'react'
import PageHead from '../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../Shared/Components/TooltipTitle';
import {
    AddressHomeIcon
} from '../../../../Shared/Components/SVGs';
import InputField from '../../../../Shared/Components/InputField';
import InputRadioBox from '../../../../Shared/Components/InputRadioBox';
import { useForm } from 'react-hook-form';
import MyPropertyActions from '../../../../store/actions/MyPropertyActions';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';
import { LocalDB } from '../../../../lib/LocalDB';
import { CommaFormatted, removeCommaFormatting } from '../../../../Utilities/helpers/CommaSeparteMasking';
import { useContext } from 'react';
import { Store } from '../../../../store/store';
type AdditionalPropertyDetailsProps = {
    address: string
}
export const AdditionalPropertyDetails = ({ address }: AdditionalPropertyDetailsProps) => {

    const [propertyDetails, setPropertyDetails] = useState({});
    const { state } = useContext(Store);
    const { primaryBorrowerInfo }: any = state.loanManager;

    const { register, errors, handleSubmit, setValue, clearErrors } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    useEffect(() => {
        if (!Object.keys(propertyDetails).length) {
            getPropertyValue();
        }
    }, [])

    useEffect(() => {
        register('value', {
            validate: (value) => {
                if (!value) {
                    return 'This field is required.'
                }
                return true;
            },
        });

        register('dues');

        register('sale', {
            validate: (value) => {
                if (!value) {
                    return 'Please select one.'
                }
                return true;
            },
        });
    }, [register]);


    const handleChange = ({ target: { id, value } }) => {
        console.log('values details', id, value);

        if (id !== 'sale') {
            value = removeCommaFormatting(value);
            if (isNaN(Number(value))) {
                return;
            }
        }

        setPropertyDetails(pre => {
            let values = {
                ...pre,
                [id]: value,
            };
            console.log('values details', values);
            return values;
        })
        setValue(id, isNaN(value) ? value : Number(value));
        clearErrors(id);
    }

    const getPropertyValue = async () => {
        let loanApplicationId = LocalDB.getLoanAppliationId();
        let propertyId = LocalDB.getAddtionalPropertyTypeId();
        if (loanApplicationId && propertyId) {
            try {
                let res = await MyPropertyActions.getPropertyValue(loanApplicationId, propertyId);

                if (res) {
                    console.log('res', res);
                    let values = {
                        value: res?.data?.propertyValue,
                        dues: res?.data?.ownersDue == 0 ? '' : res?.data?.ownersDue,
                    };
                    if (res?.data?.isSelling !== null) {
                        values['sale'] = res?.data?.isSelling ? 'Yes' : 'No'
                    }
                    setPropertyDetails(values)

                    setValue('value', Number(values.value))

                    setValue('dues', Number(values.dues))

                    if (values['sale']) {
                        setValue('sale', values['sale'])
                    }
                }
            } catch (error) {
                console.log(error);
            }
        }

    }


    const updatePropertyValue = async (data) => {
        try {
            let res = await MyPropertyActions.addOrUpdatePropertyValue(data);
            if (res) {
                NavigationHandler.moveNext();
            }
        } catch (error) {

        }
    }

    const onSubmit = (data) => {
        let loanApplicationId = LocalDB.getLoanAppliationId();
        let propertyId = LocalDB.getAddtionalPropertyTypeId();
        let values = {
            LoanApplicationId: loanApplicationId,
            Id: propertyId,
            PropertyValue: data.value,
            OwnersDue: data.dues,
            IsSelling: data.sale === 'Yes' ? true : false,
            BorrowerId: primaryBorrowerInfo.id,
            State: NavigationHandler.getNavigationStateAsString(),

        };
        updatePropertyValue(values);
    }


    const submit = handleSubmit((data) => onSubmit(data));




    const renderHeader = () => {
        return (
            <div className="row form-group">
                <div className="col-md-12">
                    <div className="listaddress-warp">
                        <div className="list-add">
                            <div className="icon-add">
                                <AddressHomeIcon />
                            </div>
                            <div className="cont-add">
                                {address}
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        )
    }


    const renderInputFiedds = () => {

        let fields = [
            {
                name: 'value',
                title: 'Property Value'
            },
            {
                name: 'dues',
                title: 'Homeowners Association Dues (if applicable)'
            },
        ]

        return (
            <div className="row form-group">
                {
                    fields.map(f => {
                        return (
                            <div className="col-md-6">
                                <InputField
                                    onChange={(e: ChangeEvent<HTMLInputElement>) => handleChange(e)}
                                    value={CommaFormatted(propertyDetails[f.name])}
                                    label={f.title}
                                    id={f.name}
                                    name={f.name}
                                    type={"text"}
                                    icon={<i className="zmdi zmdi-money"></i>}
                                    placeholder={"Estimated Value"}
                                    errors={errors}
                                />
                            </div>
                        )
                    })
                }

            </div>
        )
    }

    const renderRadioButtons = () => {
        return (

            <div className="row form-group">
                <div className="col-sm-12">
                    <h4>Will you be selling this property prior to closing?</h4>
                </div>

                {
                    ['Yes', 'No'].map(radio => {
                        return (
                            <div className="col-md-12">
                                <InputRadioBox
                                    dataTestId={"sale" + radio}
                                    onChange={(e: ChangeEvent<HTMLInputElement>) => handleChange(e)}
                                    ref={register}
                                    id={'sale'}
                                    className=""
                                    name="sale"
                                    checked={propertyDetails['sale'] === radio}
                                    value={radio}>
                                    {radio}
                                </InputRadioBox>
                            </div>
                        )
                    })
                }
                {errors?.sale && <span className="form-error no-padding" role="alert" >{errors?.sale?.message}</span>}
            </div>
        )
    }

    const renderFooter = () => {
        return (
            <div className="form-footer">
                <button data-testid="btn-save" className="btn btn-primary" type="submit"  >{'Save and continue'} </button>
            </div>
        )
    }

    console.log('errors', errors);

    return (
        <section>
            <div className="compo-myMoney-income fadein">
                <PageHead title="My Properties" handlerBack={() => { }} />
                <TooltipTitle title={`Excellent! Please let us know about additional property address.`} />
                <form onSubmit={submit} className="comp-form-panel income-panel colaba-form no-minheight">
                    {
                        renderHeader()
                    }

                    {
                        renderInputFiedds()
                    }

                    {
                        renderRadioButtons()
                    }

                    {
                        renderFooter()
                    }
                </form>

            </div>
        </section>
    )
}

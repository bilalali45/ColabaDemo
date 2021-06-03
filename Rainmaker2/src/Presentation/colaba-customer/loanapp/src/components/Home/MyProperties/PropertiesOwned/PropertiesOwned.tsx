import React, { ChangeEvent, Fragment, useContext, useEffect, useState } from 'react'
import { useForm } from 'react-hook-form';
import { ApplicationEnv } from '../../../../lib/appEnv';
import { LocalDB } from '../../../../lib/LocalDB';
import InputRadioBox from '../../../../Shared/Components/InputRadioBox';
import Loader from '../../../../Shared/Components/Loader';
import PageHead from '../../../../Shared/Components/PageHead';
import { AddressHomeIcon } from '../../../../Shared/Components/SVGs';
import TooltipTitle from '../../../../Shared/Components/TooltipTitle';
import MyPropertyActions from '../../../../store/actions/MyPropertyActions';
import { Store } from '../../../../store/store';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';

export const PropertiesOwned = ({ editting }) => {
    const [propertyDetails, setPropertyDetails] = useState({});
    const { state } = useContext(Store);
    const { primaryBorrowerInfo }: any = state.loanManager;
    const [hasProperties, setHasProperties] = useState<boolean | null>(null);


    const { register, errors, handleSubmit, setValue, clearErrors } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    useEffect(() => {
        doYouOwnAdditionalProperty();
    }, []);

    useEffect(() => {
        register('owned', {
            validate: () => {
                return true;
            },
        });

    }, [register]);

    useEffect(() => {
        if (primaryBorrowerInfo?.id) {
            fetchPropertyList();
        }
    }, [])

    const fetchPropertyList = async () => {
        let loanApplicationId = LocalDB.getLoanAppliationId();
        let borrowerId = +primaryBorrowerInfo?.id;//LocalDB.getBorrowerId();
        let res = await MyPropertyActions.getPropertyList(loanApplicationId, borrowerId);
        if (res && res.data && res.data.length > 0) {
            NavigationHandler.navigateToPath('/loanApplication/MyProperties/AllProperties');
        }
        else {
            setHasProperties(false);
        }
    }


    const handleChange = ({ target: { id, value } }) => {
        console.log('values details', id, value);

        setPropertyDetails(pre => {
            let values = {
                ...pre,
                [id]: value,
            };
            console.log('values details', values);
            return values;
        })
        setValue(id, value);
        clearErrors(id);
    }

    const doYouOwnAdditionalProperty = async () => {
        let loanApplicationId = LocalDB.getLoanAppliationId();
        let borrowerId = primaryBorrowerInfo?.id; //LocalDB.getBorrowerId();
        if (loanApplicationId && borrowerId) {
            try {
                let res = await MyPropertyActions.doYouOwnAdditionalProperty(loanApplicationId, borrowerId);

                if (res) {
                    console.log('res', res);
                }
            } catch (error) {

            }
        }
    }


    const onSubmit = (data) => {
        let url;
        if (data.owned === 'Yes') {
            url = '/loanApplication/MyProperties/AdditionalPropertyType';
        } else {
            url = '/loanApplication/MyProperties/PropertiesReview'
        }
        LocalDB.setAdditionalPropertyTypeId(null);
        NavigationHandler.navigateToPath(url);
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
                                Do you currently own any properties?
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        )
    }

    const renderRadioButtons = () => {
        return (

            <div className="row form-group">
                {
                    ['Yes', 'No'].map(radio => {
                        return (
                            <div className="col-md-12">
                                <InputRadioBox
                                    dataTestId={"owned-" + radio}
                                    onChange={(e: ChangeEvent<HTMLInputElement>) => handleChange(e)}
                                    ref={register}
                                    id={'owned'}
                                    className=""
                                    name="owned"
                                    checked={propertyDetails['owned'] === radio}
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

    if (editting) {
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyProperties/AllProperties`);
        return <Fragment />
    }

    return (
        <section>
            <div className="compo-myMoney-income fadein">
                <PageHead title="My Properties" handlerBack={() => { }} />
                <TooltipTitle title={`Excellent! Please let us about additional property address.`} />
                <form onSubmit={submit} className="comp-form-panel income-panel colaba-form no-minheight">
                    {hasProperties == false &&
                        <div>
                            {
                                renderHeader()
                            }

                            {
                                renderRadioButtons()
                            }

                            {
                                renderFooter()
                            }
                        </div>
                    }
                    {hasProperties != false &&
                        <div>
                            <Loader type="widget" />
                        </div>
                    }

                </form>

            </div>
        </section>
    )
}

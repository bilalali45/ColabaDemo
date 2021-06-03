import React, { useContext, useEffect, useState } from 'react'
import { Store } from '../../../../store/store';
import PageHead from '../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../Shared/Components/TooltipTitle';
import {
    AddressHomeIcon,
    IconCooperative, IconSingleFamilyProperty, IconTownhouse, IconCondominium, IconDuplex2Unit, IconManufacturedHome, IconTriplex3Unit, IconQuadplex4Unit, IconLandProperty
} from '../../../../Shared/Components/SVGs';
import IconRadioBox from '../../../../Shared/Components/IconRadioBox';
import InputField from '../../../../Shared/Components/InputField';
import MyPropertyActions from '../../../../store/actions/MyPropertyActions';
import { LocalDB } from '../../../../lib/LocalDB';
import { ErrorHandler } from '../../../../Utilities/helpers/ErrorHandler';
import { MyPrimaryPropertyType } from '../../../../Entities/Models/types';
import { CommaFormatted } from '../../../../Utilities/helpers/CommaSeparteMasking';
import { useForm } from 'react-hook-form';
import { PrimaryPropertyType } from '../../../../Entities/Models/CurrentResidence';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';
import { PropertyTypeEnum, SectionTypeEnum } from '../../../../Utilities/Enumerations/MyPropertyEnums';
import { TenantConfigFieldNameEnum } from '../../../../Utilities/Enumerations/TenantConfigEnums';
//import MyNewMortgageActions from '../../../../store/actions/MyNewMortgageActions';

const PropertyTypeIcons = {
    'Single Family Property': <IconSingleFamilyProperty />,
    'Townhouse': <IconTownhouse />,
    'Condominium': <IconCondominium />,
    'Cooperative': <IconCooperative />,
    'Duplex (2 Unit)': <IconDuplex2Unit />,
    'Manufactured Home': <IconManufacturedHome />,
    'Triplex (3 Unit)': <IconTriplex3Unit />,
    'Quadplex (4 Unit)': <IconQuadplex4Unit />,
    'Land': <IconLandProperty />,
}
export const getPropertyTypeIcon = propertyType => PropertyTypeIcons[propertyType]

export const PropertyTypes = ({
    pageTitle,
    title,
    selectedTypeAction,
    submitAction,
    runAfterSubmit,
    address,
    propertyTypeId,
    setPropertyTypeId,
    primaryBorrowerId,
    sectionType,
    shouldAskForRentalIncome = false,
    onValueChanges = (e) => { return e }
}) => {
    const { register, errors, handleSubmit, clearErrors } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "firstError",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    const { dispatch } = useContext(Store);

    const [propertyTypes, setPropertyTypes] = useState<{ id: number; name: string, image: string }[]>();
    const [selectedPropertyTypeId, setSelectedPropertyTypeId] = useState<number>();
    const [showRentalIncome, setShowRentalIncome] = useState<boolean>(false);
    const [rentalIncome, setRentalIncome] = useState<number | null>(null);
    const [primaryPropertyType, setPrimaryPropertyType] = useState<MyPrimaryPropertyType | null>(null);
    const [btnClicked, setBtnClicked] = useState<boolean>(false);

    useEffect(() => {
        if (!selectedPropertyTypeId) {
            getandSetAllPropertyTypes();
        }
        if (!primaryPropertyType && propertyTypeId) {
            getandSetPrimaryPropertyType();
        }

    }, [])

    const getandSetAllPropertyTypes = async () => {
        let response = await MyPropertyActions.getAllpropertytypes(sectionType);
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                setPropertyTypes(response.data);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }

    };

    const getandSetPrimaryPropertyType = async () => {
        var response = await MyPropertyActions[selectedTypeAction](+LocalDB.getLoanAppliationId(), +propertyTypeId)
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                if (response.data) {
                    setPrimaryPropertyType(response.data);
                    setRentalIncome(response.data.rentalIncome);
                    onSelectPropertyType(response.data.propertyTypeId)
                }
            } else {
                //ErrorHandler.setError(dispatch, response);
            }
        }
    };

    const onSelectPropertyType = (id) => {
        if (id) {
            onValueChanges(id);

            setSelectedPropertyTypeId(id);
            if ([PropertyTypeEnum.Duplex, PropertyTypeEnum.Triplex, PropertyTypeEnum.Quadplex].includes(id)) {
                setShowRentalIncome(true);
            }
            else {
                setShowRentalIncome(false);
            }
        }
    }

    const prepareApiData = () => {
        let propertyVal: PrimaryPropertyType = {
            LoanApplicationId: Number(LocalDB.getLoanAppliationId()),
            PropertyTypeId: selectedPropertyTypeId,
            BorrowerId: Number(LocalDB.getBorrowerId()),
            State: NavigationHandler.getNavigationStateAsString(), //
        };
        if (primaryBorrowerId && primaryBorrowerId != 0) {
            propertyVal.BorrowerId = +primaryBorrowerId;
        }
        if (showRentalIncome && rentalIncome) {
            propertyVal.RentalIncome = +rentalIncome
        }
        if (primaryPropertyType) {
            propertyVal.id = primaryPropertyType.id;
        }
        return propertyVal;
    }

    const onSaveandContinue = async () => {
        if (!btnClicked) {
            setBtnClicked(true);
            let postData: PrimaryPropertyType = prepareApiData();
            try {
                var response = await MyPropertyActions[submitAction](postData);
                if (response) {
                    await runAfterSubmit(response);
                    let id = response.data as number
                    setPropertyTypeId(id);
                }
                NavigationHandler.moveNext()
            } catch (error) {
                setBtnClicked(false);
            }

        }
    };

    const onSkip = async () => {
        NavigationHandler.moveNext()
    }

    const renderFormHeader = () => {
        return (
            <div className="row form-group">
                <div className="col-md-12">
                    <div className="listaddress-warp">
                        <div className="list-add">
                            <div className="icon-add">
                                <AddressHomeIcon />
                            </div>
                            <div className="cont-add">
                                {title}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }

    const renderRadioButton = (item) => {
        return (
            <div data-testid="property-list" className="col-md-6">
                <IconRadioBox
                    id={item.id}
                    className={selectedPropertyTypeId ? selectedPropertyTypeId === item.id ? "active" : "" : ""}
                    name="radio1"
                    checked={selectedPropertyTypeId === item.id ? true : false}
                    value={item.name}
                    title={item.name}
                    Icon={getPropertyTypeIcon(item.name)}
                    handlerClick={() => {
                        onSelectPropertyType(item.id);
                    }}
                />
            </div>
        )
    }

    const renderInputField = () => {
        return (
            <InputField
                label={"Rental Income (if applicable)"}
                data-testid="rental-income"
                id=""
                name="rentalIncome"
                type={"text"}
                icon={<i className="zmdi zmdi-money"></i>}
                placeholder={"Monthly Rental Income"}
                value={CommaFormatted(rentalIncome)}
                register={register}
                errors={errors}
                onChange={(event) => {
                    clearErrors("rentalIncome");
                    let value = event.target.value;
                    if (value.length > 0 && !/^[0-9,]{1,11}$/g.test(value)) {
                        return false;
                    }

                    if (value.length > 1) {
                        clearErrors();
                    }

                    if (value.length > 9) {
                        return false;
                    }
                    value = value.replace(/\,/g, '');
                    setRentalIncome(value);
                    return true;

                }}
                onBlur={() => {
                }}
            />
        )
    }

    const renderRentalIncomeForm = () => {
        return (
            <div className="row form-group">
                <div className="col-md-6">
                    {renderInputField()}
                </div>
            </div>
        )
    }

    const isFieldRequired = (sectionType: SectionTypeEnum) => {
        switch (sectionType) {
            case SectionTypeEnum.CurrentResidence:
            case SectionTypeEnum.AdditionalProperty:
                return NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PropertyTypeMyProperties, true);
            case SectionTypeEnum.SubjectProperty:
                return NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PropertyTypeSubjectProperty, true);
            default:
                return true;
        }
    }

    const renderFooter = () => {
        return (
            <div className="form-footer">
                <button id="save-btn" data-testid="save-btn" className="btn btn-primary" type="submit" disabled={selectedPropertyTypeId ? false : true} onClick={handleSubmit(onSaveandContinue)} >
                    {'Save and continue'}
                </button>
                {
                    !isFieldRequired(sectionType) &&
                    <button id="skip-btn" style={{ marginLeft: 10 }} data-testid="skip-btn" className="btn btn-primary" type="button" onClick={onSkip} >
                        {'Skip'}
                    </button>
                }
            </div>
        )
    }

    const renderPropertyTypeList = () => {
        return (
            <div className="row form-group">
                {propertyTypes?.map((item) => {
                    return renderRadioButton(item);
                })}
            </div>
        )
    }

    return (
        <form>
            <section>
                <div className="compo-myMoney-income fadein">
                    <PageHead title={pageTitle} handlerBack={() => { }} />
                    {address ? <TooltipTitle title={`Great. What type of property is <strong>${address}?</strong>`} /> : <TooltipTitle title={`Please tell us more about this property`} />}
                    <div className="comp-form-panel income-panel colaba-form">

                        {
                            renderFormHeader()
                        }

                        {
                            renderPropertyTypeList()
                        }

                        {
                            shouldAskForRentalIncome && showRentalIncome && renderRentalIncomeForm()
                        }

                        {
                            renderFooter()
                        }


                    </div>

                </div>
            </section>
        </form>
    )
}

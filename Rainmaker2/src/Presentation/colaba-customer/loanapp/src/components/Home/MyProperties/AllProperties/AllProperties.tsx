import React, { useContext, useEffect, useState } from 'react'
import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";
import { EditIcon, DeleteIcon } from "../../../../Shared/Components/SVGs";
import MyPropertyActions from '../../../../store/actions/MyPropertyActions';
import { LocalDB } from '../../../../lib/LocalDB';
import { getPropertyTypeIcon } from '../PropertyTypes/PropertyTypes';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';
import { Store } from '../../../../store/store';
import { HomeAddress, propertyItemObj } from '../../../../Entities/Models/types';

type AllPropertiesProps = {
    setEditting: Function;
}


export const AllProperties = ({ setEditting }: AllPropertiesProps) => {

    const [proertyList, setPropertyList] = useState([]);
    //const [deleteRequested, setDeleteRequested] = useState(false);
    const { state } = useContext(Store);
    const { primaryBorrowerInfo }: any = state.loanManager;

    useEffect(() => {
        if (!proertyList?.length && primaryBorrowerInfo?.id) {
            fetchPropertyList();
        }
    }, [primaryBorrowerInfo?.id])

    const fetchPropertyList = async () => {
        let loanApplicationId = LocalDB.getLoanAppliationId();
        let borrowerId = +primaryBorrowerInfo?.id;//LocalDB.getBorrowerId();
        let res = await MyPropertyActions.getPropertyList(loanApplicationId, borrowerId);
        if (res?.data?.length) {
            setPropertyList(res.data)
        }
        else {
            NavigationHandler.navigateToPath('/loanApplication/MyProperties/PropertiesOwned');
            setPropertyList([])
        }
    }

    const createAddressString = ({ street, city, stateName, zipCode }: HomeAddress) => {
        if (!street) return 'Address Not Given';
        return `${street}, ${city}, ${stateName}, ${zipCode}`
    }

    const handleEdit = (typeId: number, propertyId: number) => {

        let url = '/loanApplication/MyProperties/AdditionalPropertyType';

        if (typeId == 1) {
            url = '/loanApplication/MyProperties/CurrentResidence'
        } else {
            LocalDB.setAdditionalPropertyTypeId(propertyId);
        }

        setEditting(true);
        NavigationHandler.navigateToPath(url);
    }

    const handleDelete = async (id: number) => {

        //if (deleteRequested) return;

        //setDeleteRequested(true);
        try {
            await MyPropertyActions.deleteProperty(LocalDB.getLoanAppliationId(), id);
            await fetchPropertyList();
        } catch (error) {
            console.log('error', error);
            await fetchPropertyList();
        }

    }

    const propertyItem = ({ typeId, propertyType, address, id }: propertyItemObj) => {
        return (
            <li data-testid="property-item">
                <div className="icon-wrap">
                    {getPropertyTypeIcon(propertyType)}
                </div>
                <div className="c-wrap">
                    <h5>{createAddressString(address)}</h5>
                    <p>{propertyType}</p>
                </div>
                <div className="pl-actions">
                    <a onClick={() => handleEdit(typeId, id)} className="btn btn-edit" data-testid="edit-btn">
                        <EditIcon />
                    </a>
                    {typeId == 2 && <a className="btn btn-delete" onClick={() => handleDelete(id)} data-testid="delete-btn">
                        <DeleteIcon />
                    </a>}
                </div>
            </li>
        )
    }

    const renderPropertyList = () => {
        return (
            <div className="row form-group" data-testid="properties-container">
                <div className="col-md-12">
                    <ul className="properties-listwrap">
                        {
                            proertyList?.map((property) => {
                                return propertyItem(property);
                            })
                        }
                    </ul>
                </div>
            </div>
        )
    }

    const renderPropertyListFooter = () => {
        return (
            <div className="form-footer properties-footer">
                <button data-testid="no-additional-properties" className="btn btn-secondary" onClick={() => {
                    let url = '/loanApplication/MyProperties/PropertiesReview'
                    NavigationHandler.navigateToPath(url)
                }}>
                    {"No Additional Properties"}
                </button>
                <button data-testid="add-additional-properties" className="btn btn-primary" onClick={() => {
                    let url = '/loanApplication/MyProperties/AdditionalPropertyType'
                    LocalDB.setAdditionalPropertyTypeId(null);
                    NavigationHandler.navigateToPath(url)
                }}>
                    {"Add Additional Property"}
                </button>
            </div>
        )
    }


    return (
        <section>
            <div className="compo-myMoney-income fadein">
                <PageHead title="My Properties" handlerBack={() => { }} />
                <TooltipTitle title={`${primaryBorrowerInfo?.name}, please list all properties you own.`} />
                <div className="comp-form-panel income-panel colaba-form properties-form">
                    {
                        (proertyList && proertyList.length > 0) &&
                        renderPropertyList()
                    }

                    {
                        renderPropertyListFooter()
                    }

                </div>
            </div>
        </section>
    )
}

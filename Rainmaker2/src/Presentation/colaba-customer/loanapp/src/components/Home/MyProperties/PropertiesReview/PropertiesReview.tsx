import React, { useContext, useEffect, useState } from 'react'
import { MyPropertyReviewProto } from '../../../../Entities/Models/types';
import { ApplicationEnv } from '../../../../lib/appEnv';
import { LocalDB } from '../../../../lib/LocalDB';
import Loader from '../../../../Shared/Components/Loader';
import MyPropertyActions from '../../../../store/actions/MyPropertyActions';
import { LoanApplicationActionsType } from '../../../../store/reducers/LoanApplicationReducer';
import { Store } from '../../../../store/store';
import { ErrorHandler } from '../../../../Utilities/helpers/ErrorHandler';
import { StringServices } from '../../../../Utilities/helpers/StringServices';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';
import { PropertiesReviewList } from './PropertiesReviewList';

export const PropertiesReview = () => {

    const [myPropertyReviewInfo, setMyPropertyReviewInfo] = useState<MyPropertyReviewProto>();

    const { state, dispatch } = useContext(Store);
    const { primaryBorrowerInfo }: any = state.loanManager;

    useEffect(() => {
        getandSetReviewInformation();
    }, [primaryBorrowerInfo?.id])

    const getandSetReviewInformation = async () => {
        //
        let response = await MyPropertyActions.getFinalScreenReview(+primaryBorrowerInfo?.id, +LocalDB.getLoanAppliationId());

        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                if (response.data && response.data.length > 0) {
                    let primaryApplicant = response.data[0];
                    let data = {
                        primaryApplicantName: StringServices.componseFullName(primaryApplicant.firstName, primaryApplicant.lastName),
                        propertyList: []
                    }

                    response.data.forEach(element => {
                        data.propertyList.push({
                            id: element.id,
                            propertyType: element.propertyType,
                            typeId: element.typeId,
                            firstName: element.firstName,
                            lastName: element.lastName,
                            ownTypeId: element.ownTypeId,
                            street: element.address.street,
                            unit: element.address.unit,
                            city: element.address.city,
                            stateId: element.address.stateId,
                            zipCode: element.address.zipCode,
                            countryId: element.address.countryId,
                            countryName: element.address.countryName,
                            stateName: element.address.stateName
                        })
                    });
                    setMyPropertyReviewInfo(data);
                }
                else {
                    NavigationHandler.moveNext();
                }

            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }


    const saveAndContinue = () => {
        NavigationHandler.moveNext();
    }

    const flushPropertyStateData = async () => {
        await dispatch({ type: LoanApplicationActionsType.SetAdditionalPropertyInfo, payload: {} });
    }

    const editProperty = async (id: number, typeId: number) => {
        if (typeId === 1) {
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyProperties/CurrentResidence`);
        }
        else if (typeId == 2) {
            flushPropertyStateData();
            await dispatch({ type: LoanApplicationActionsType.SetAdditionalPropertyInfo, payload: { id: id } });
            LocalDB.setAdditionalPropertyTypeId(id);
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyProperties/AdditionalPropertyType`);
        }
    }

    return myPropertyReviewInfo ? <><PropertiesReviewList
        myPropertyReviewProto={myPropertyReviewInfo}
        editProperty={editProperty}
        saveAndContinue={saveAndContinue}
    />
    </> : <Loader type="widget" />
}

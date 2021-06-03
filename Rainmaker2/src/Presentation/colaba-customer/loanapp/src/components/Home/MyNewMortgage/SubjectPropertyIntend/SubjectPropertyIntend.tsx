import React, { useState, useContext, useEffect } from "react";
import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";
import { Store } from "../../../../store/store";
import LeftMenuHandler, { Decisions } from "../../../../store/actions/LeftMenuHandler";
import MyNewMortgageActions from "../../../../store/actions/MyNewMortgageActions";
import { LocalDB } from "../../../../lib/LocalDB";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import { LoanApplicationActionsType } from "../../../../store/reducers/LoanApplicationReducer";
import GettingToKnowYouActions from "../../../../store/actions/GettingToKnowYouActions";
import { SubjectPropertyIntendForm } from "./SubjectPropertyIntendForm";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";


export type PropertyIdentification = {
    LoanApplicationId: number,
    IsIdentified: boolean,
    State: string,
    StateId?: number
}

type IsPropertyIdentified = {
    isIdentified: boolean,
    loanApplicationId: number,
    stateId: number,
}

export const SubjectPropertyIntend = () => {


    const [isPropertyIdentified, setIsPropertyIdentified] = useState<IsPropertyIdentified>();

    const { state, dispatch } = useContext(Store);

    const { states }: any = state.loanManager;


    useEffect(() => {
        !isPropertyIdentified && getPropertyIdentifiedFlag();
        !states && getAllStates();
    }, []);


    const getAllStates = async () => {

        let res = await GettingToKnowYouActions.getStates(true);
        if (res) {
            if (ErrorHandler.successStatus.includes(res.statusCode)) {
                dispatch({
                    type: LoanApplicationActionsType.SetStates,
                    payload: res.data,
                });
            }
        }
    };


    const getPropertyIdentifiedFlag = async () => {
        try {
            let res = await MyNewMortgageActions.getPropertyIdentifiedFlag(Number(LocalDB.getLoanAppliationId()));
            setIsPropertyIdentified(res.data);
        } catch (error) {
            console.log(error)
        }

    }

    const updatePropertyIdentified = async ({ propertyIdentification, stateName }) => {
        let body: PropertyIdentification = {
            LoanApplicationId: Number(LocalDB.getLoanAppliationId()),
            IsIdentified: false,
            State: NavigationHandler.getNavigationStateAsString()

        };

        if (propertyIdentification === 'Yes') {
            body.IsIdentified = true;
        } else {
            body.StateId = states?.find(s => s?.name === stateName)?.id;
        }
        await MyNewMortgageActions.updatePropertyIdentified(body);
        LeftMenuHandler.makeDecision(propertyIdentification === 'Yes' ? Decisions.PropertyIdentified : Decisions.PropertyNotIdentified);
        NavigationHandler.moveNext();
    }

    const onSubmit = (data) => updatePropertyIdentified(data)

    return (

        <div className="compo-subject-P compo-subject-P-intend fadein">
            <PageHead
                title="Subject Property"
                handlerBack={() => {

                }}
            />
            <TooltipTitle title="Now, we need to learn about the property you’re purchasing." />
            {states?.length && <SubjectPropertyIntendForm
                onSubmit={onSubmit}
                isPropertyIdentified={isPropertyIdentified}
                states={states}

            />}
        </div>
    )
}

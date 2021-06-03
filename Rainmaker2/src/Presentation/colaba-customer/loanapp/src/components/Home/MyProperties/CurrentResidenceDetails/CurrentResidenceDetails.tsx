import React, { ChangeEvent, useContext, useEffect, useState } from "react";
import { Store } from "../../../../store/store";
import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";
import { CurrentResidenceDetails_Web } from "./CurrentResidenceDetails_Web";
import { CurrentPropertyVal } from "../../../../Entities/Models/CurrentResidence";
import { LocalDB } from "../../../../lib/LocalDB";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import MyPropertyActions from "../../../../store/actions/MyPropertyActions";
import {
    CommaFormatted,
    removeCommaFormatting,
} from "../../../../Utilities/helpers/CommaSeparteMasking";
import { MyPropertyInfo } from "../../../../Entities/Models/types";
import { LoanApplicationActionsType } from "../../../../store/reducers/LoanApplicationReducer";

export type CurrentResidencePostProps = {
    prop_val: string, 
    prop_dues: string
} 
export const CurrentResidenceDetails = () => {


    const { state, dispatch } = useContext(Store);
    const { myPropertyInfo }: any = state.loanManager;
    const { primaryBorrowerInfo }: any = state.loanManager;


    const [propVal, setPropVal] = useState<string>("");
    const [propDues, setPropDues] = useState<string>("");
    const [selling, setSelling] = useState<boolean | null>(null);
    const [btnClick, setBtnClick] = useState<boolean>(false);

    useEffect(() => {
        if (LocalDB.getMyPropertyTypeId())
            getPropertyValue();
    }, []);

    const getPropertyValue = async () => {
        let res: any = await MyPropertyActions.getPropertyValue(
            Number(LocalDB.getLoanAppliationId()),
            +LocalDB.getMyPropertyTypeId()
        );

        if (res) {
            if (ErrorHandler.successStatus.includes(res.statusCode)) {
                const { propertyValue, ownersDue, isSelling } = res.data;
                setPropVal(CommaFormatted(propertyValue));
                setPropDues(CommaFormatted(ownersDue));
                setSelling(isSelling);
            } else {
                ErrorHandler.setError(dispatch, res);
            }
        }
    };

    const onPropValChangeHandler = (event: ChangeEvent<HTMLInputElement>) => {
        const value = event.currentTarget.value;
        if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
            return false;
        }
        setPropVal(value.replace(/\,/g, ""));
        return true;
    };

    const onPropDuesChangeHandler = (event: ChangeEvent<HTMLInputElement>) => {
        const value = event.currentTarget.value;
        if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
            return false;
        }
        setPropDues(value.replace(/\,/g, ""));
        return true;
    };

    const onSave = async (data: CurrentResidencePostProps) => {
        if (!btnClick) {
            setBtnClick(true);
            let reqData: CurrentPropertyVal = preparePostAPIData(data);
            let res = await MyPropertyActions.addOrUpdatePropertyValue(reqData);
            if (res) {
                if (ErrorHandler.successStatus.includes(res.statusCode)) {
                    LocalDB.setMyPropertyTypeId(res.data as number);
                    let data: MyPropertyInfo = {
                        primaryPropertyTypeId: res.data as number
                    };
                    dispatch({
                        type: LoanApplicationActionsType.SetMyPropertyInfo,
                        payload: data,
                    });
                    
                    NavigationHandler.moveNext();
                } else {
                    ErrorHandler.setError(dispatch, res);
                }
            }
        }
    };

    const preparePostAPIData = (data: CurrentResidencePostProps) => {
        const { prop_val, prop_dues } = data;

        let propertyVal: CurrentPropertyVal = {
            LoanApplicationId: Number(LocalDB.getLoanAppliationId()),
            Id: +myPropertyInfo?.primaryPropertyTypeId,
            IsSelling: selling,
            PropertyValue: prop_val ? +removeCommaFormatting(prop_val) : 0,
            OwnersDue: prop_dues ? +removeCommaFormatting(prop_dues) : null,
            State: NavigationHandler.getNavigationStateAsString(), //
            BorrowerId: primaryBorrowerInfo.id
        };

        return propertyVal;
    };

    // if (!LocalDB.getMyPropertyTypeId()) {
    //     NavigationHandler.navigateToPath(
    //         `${ApplicationEnv.ApplicationBasePath}/MyProperties/CurrentResidence`
    //     );
    //     return <React.Fragment />;
    // }

    return (
        <section>
            <div className="compo-myMoney-income">
                <PageHead title="My Current Residence" handlerBack={() => { }} />
                <TooltipTitle title="Now, we’ll need some additional details about your current residence." />
                <CurrentResidenceDetails_Web
                    propVal={propVal}
                    setPropVal={setPropVal}
                    propDues={propDues}
                    setPropDues={setPropDues}
                    selling={selling}
                    setSelling={setSelling}
                    onSave={onSave}
                    onPropValChangeHandler={onPropValChangeHandler}
                    onPropDuesChangeHandler={onPropDuesChangeHandler}
                />
            </div>
        </section>
    );
};

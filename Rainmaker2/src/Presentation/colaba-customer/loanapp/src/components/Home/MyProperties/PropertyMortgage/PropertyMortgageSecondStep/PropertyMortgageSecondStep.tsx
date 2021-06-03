import _ from 'lodash';
import React, { useContext, useEffect, useState } from 'react'
import { HasSecondMortgage } from '../../../../../Entities/Models/CurrentResidence';
import { ApplicationEnv } from '../../../../../lib/appEnv';
import { LocalDB } from '../../../../../lib/LocalDB';
import PageHead from '../../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../../Shared/Components/TooltipTitle';
import MyPropertyActions from '../../../../../store/actions/MyPropertyActions';
import { Store } from '../../../../../store/store';
import { ErrorHandler } from '../../../../../Utilities/helpers/ErrorHandler';
import { NavigationHandler } from '../../../../../Utilities/Navigation/NavigationHandler';
import { SecondCurrentResidenceMortgage_Web } from '../../SecondCurrentResidenceMortgage/SecondCurrentResidenceMortgage_Web';

type PropertyMortgageSecondStepProps = {
  propertyId:number, 
  title:string, 
  address:string,
  animatedText: string 
}

export const PropertyMortgageSecondStep = ({ propertyId, title, address, animatedText }: PropertyMortgageSecondStepProps) => {
    const { state, dispatch } = useContext(Store);
    const { myPropertyInfo }: any = state.loanManager;

    const [haveMortgage, setHaveMortgage] = useState<boolean | null>(null);

    const [btnClick, setBtnClick] = useState<boolean>(false);


    useEffect(() => {
        //   if(myPropertyInfo && myPropertyInfo?.primaryPropertyTypeId)
        if (propertyId)
            doYouHaveSecondMortgage();
    }, [myPropertyInfo]);


    const doYouHaveSecondMortgage = async () => {
        let res: any = await MyPropertyActions.doYouHaveSecondMortgage(
            +LocalDB.getLoanAppliationId(),
            +propertyId
            // +myPropertyInfo?.primaryPropertyTypeId
        );

        if (res) {
            if (ErrorHandler.successStatus.includes(res.statusCode)) {
                if (res.data === "") return;
                setHaveMortgage(res.data);
            } else {
                ErrorHandler.setError(dispatch, res);
            }
        };
    }

    const onSave = async () => {
        if (!btnClick) {
            setBtnClick(true);
            let reqData: HasSecondMortgage = preparePostAPIData();
            let res = await MyPropertyActions.addOrUpdateHasSecondMortgage(reqData);
            if (res) {
                if (ErrorHandler.successStatus.includes(res.statusCode)) {
                    if (haveMortgage) NavigationHandler.moveNext();
                    else NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyProperties/AllProperties`)
                } else {
                    ErrorHandler.setError(dispatch, res);
                }
            }
        }
    };

    const preparePostAPIData = () => {
        let hasFirstMortgage: HasSecondMortgage = {
            LoanApplicationId: Number(LocalDB.getLoanAppliationId()),
            Id: +propertyId,
            // Id: +myPropertyInfo?.primaryPropertyTypeId,
            State: NavigationHandler.getNavigationStateAsString(),
            HasSecondMortgage: haveMortgage ? haveMortgage : false,
        };
        return hasFirstMortgage;
    };

    if (!propertyId) {
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyProperties/CurrentResidence`);
        return <React.Fragment />;
    }

    return (
        <section>
            <div className="compo-myMoney-income fadein">
                <PageHead title={title} handlerBack={() => { }} />
                <TooltipTitle title={animatedText} />
                <SecondCurrentResidenceMortgage_Web
                    haveMortgage={haveMortgage}
                    setHaveMortgage={setHaveMortgage}
                    onSave={onSave}
                    address={address}
                />
            </div>
        </section>
    );
}

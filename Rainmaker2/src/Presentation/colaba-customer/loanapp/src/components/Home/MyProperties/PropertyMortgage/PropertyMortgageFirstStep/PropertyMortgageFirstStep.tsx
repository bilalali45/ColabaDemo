import React, { useContext, useEffect, useState } from 'react'
import { HasFirstMortgage } from '../../../../../Entities/Models/CurrentResidence';
import { ApplicationEnv } from '../../../../../lib/appEnv';
import { LocalDB } from '../../../../../lib/LocalDB';
import PageHead from '../../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../../Shared/Components/TooltipTitle';
import MyPropertyActions from '../../../../../store/actions/MyPropertyActions';
import { Store } from '../../../../../store/store';
import { CommaFormatted, removeCommaFormatting } from '../../../../../Utilities/helpers/CommaSeparteMasking';
import { ErrorHandler } from '../../../../../Utilities/helpers/ErrorHandler';
import { NavigationHandler } from '../../../../../Utilities/Navigation/NavigationHandler';
import { FirstCurrentResidenceMortgage_Web } from '../../FirstCurrentResidenceMortgage/FirstCurrentResidenceMortgage_Web';
export type PropertyMortgageFirstStepAPIProps = {
  prop_tax: string, 
  insurance: string, 
  flood_insurance: string
}

type PropertyMortgageFirstStepProps = {
  propertyId: number, 
    title: string, 
    address: string,
    animatedText: string
}
export const PropertyMortgageFirstStep = ({propertyId, title, address, animatedText}: PropertyMortgageFirstStepProps) => {
    const { state, dispatch } = useContext(Store);

    const { myPropertyInfo }: any = state.loanManager;
  
    const [propTax, setPropTax] = useState<string>("");
    const [insurance, setInsurance] = useState<string>("");
    const [floodIns, setFloodIns] = useState<string>("");
    const [haveMortgage, setHaveMortgage] = useState<boolean | null>(null);
    const [btnClick, setBtnClick] = useState<boolean>(false);
  
    useEffect(() => {
    //   if(myPropertyInfo && myPropertyInfo?.primaryPropertyTypeId)
      if(propertyId)
      doYouHaveFirstMortgage();
    }, [myPropertyInfo]);
  
  
    const doYouHaveFirstMortgage = async () => {
      let res: any = await MyPropertyActions.doYouHaveFirstMortgage(
        +LocalDB.getLoanAppliationId(),
        +propertyId
        // +myPropertyInfo?.primaryPropertyTypeId
      );
  
      if (res) {
        if (ErrorHandler.successStatus.includes(res.statusCode)) {
          const { propertyTax, homeOwnerInsurance, hasFirstMortgage, floodInsurance } = res.data;
          setPropTax(CommaFormatted(propertyTax));
          setInsurance(CommaFormatted(homeOwnerInsurance));
          setHaveMortgage(hasFirstMortgage);
          setFloodIns(CommaFormatted(floodInsurance))
        } else {
          ErrorHandler.setError(dispatch, res);
        }
      }
    };
  
  
    const onSave = async (data: PropertyMortgageFirstStepAPIProps) => {
      if (!btnClick) {
        setBtnClick(true);
        let reqData: HasFirstMortgage = preparePostAPIData(data);
        let res = await MyPropertyActions.addOrUpdateHasFirstMortgage(reqData);
        if (res) {
          if (ErrorHandler.successStatus.includes(res.statusCode)) {
            if(haveMortgage) NavigationHandler.moveNext();
            else NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyProperties/AllProperties`) 
          } else {
            ErrorHandler.setError(dispatch, res);
          }
        }
      }
    };
  
    const preparePostAPIData = (data: PropertyMortgageFirstStepAPIProps) => {
      const { prop_tax, insurance, flood_insurance } = data;
  
      let hasFirstMortgage: HasFirstMortgage = {
        LoanApplicationId: Number(LocalDB.getLoanAppliationId()),
        Id: +propertyId,
        // Id: +myPropertyInfo?.primaryPropertyTypeId,
        State: NavigationHandler.getNavigationStateAsString(),
        HasFirstMortgage: haveMortgage === null ? false : haveMortgage,
        PropertyTax: prop_tax ? +removeCommaFormatting(prop_tax) : 0,
        HomeOwnerInsurance: insurance ? +removeCommaFormatting(insurance) : 0,
        FloodInsurance: flood_insurance ? +removeCommaFormatting(flood_insurance) : 0
      };
      return hasFirstMortgage;
    };
  
  
    // if (!propertyId) {
    //   NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyProperties/CurrentResidence`);
    //   return <React.Fragment/>;
    // }
  
    return (
      // <div>
      //     <h1>FirstCurrentResidenceMortgage</h1>
      //     <PropertyMortgageForm/>
      // </div>
      <section>
        <div className="compo-myMoney-income fadein">
          <PageHead title={title} handlerBack={() => {}} />
          <TooltipTitle title={animatedText} />
          <FirstCurrentResidenceMortgage_Web
            propTax={propTax}
            setPropTax={setPropTax}
            insurance={insurance}
            setInsurance={setInsurance}
            haveMortgage={haveMortgage}
            setHaveMortgage={setHaveMortgage}
            onSave={onSave}
            floodIns = {floodIns}
            setFloodIns = {setFloodIns}
            address={address}
          />
        </div>
      </section>
    );
}

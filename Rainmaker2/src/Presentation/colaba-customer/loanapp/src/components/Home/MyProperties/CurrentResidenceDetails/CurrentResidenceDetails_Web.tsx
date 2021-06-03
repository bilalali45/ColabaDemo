import React, { useContext, useEffect, useState } from "react";

import { Store } from "../../../../store/store";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import { LocalDB } from "../../../../lib/LocalDB";
import MyPropertyActions from "../../../../store/actions/MyPropertyActions";
import { StringServices } from "../../../../Utilities/helpers/StringServices";
import { PropertyValue } from "../PropertyMortgage/PropertyValue/PropertyValue";
import { LoanApplicationActionsType } from "../../../../store/reducers/LoanApplicationReducer";

type CurrentResidenceDetails_WebProps = {
  propVal: string;
  setPropVal: Function;
  propDues: string;
  setPropDues: Function;
  selling: boolean | null;
  setSelling: Function;
  onSave: Function;
  onPropValChangeHandler: Function;
  onPropDuesChangeHandler: Function;
};
export const CurrentResidenceDetails_Web = ({
  propVal,
  setPropVal,
  propDues,
  setPropDues,
  selling,
  setSelling,
  onSave,
  onPropValChangeHandler,
  onPropDuesChangeHandler,
}: CurrentResidenceDetails_WebProps) => {
  const { state, dispatch } = useContext(Store);
  const { myPropertyInfo }: any = state.loanManager;
  const [homeAddress, setHomeAddress] = useState<string>("");

  useEffect(() => {
    if (myPropertyInfo?.address) setHomeAddress(myPropertyInfo?.address);
    else getPrimaryBorrowerAddressDetail();
  }, []);

  const getPrimaryBorrowerAddressDetail = async () => {
    // let loanApplicationId = LocalDB.getLoanAppliationId();
    // if (loanApplicationId) {
    var response = await MyPropertyActions.getPrimaryBorrowerAddressDetail(
      +LocalDB.getLoanAppliationId()
    );
    if (response) {
      if (ErrorHandler.successStatus.includes(response.statusCode)) {
        if (response.data.address) {
          var address = StringServices.addressGenerator({
            countryId: response.data.address?.countryId,
            countryName: response.data.address?.countryName,
            stateId: response.data.address?.stateId,
            stateName: response.data.address?.stateName,
            cityName: response.data.address?.city,
            streetAddress: response.data.address?.street,
            zipCode: response.data.address?.zipCode,
            unitNo: response.data.address?.unit,
          });
          dispatch({
            type: LoanApplicationActionsType.SetMyPropertyInfo,
            payload: { ...myPropertyInfo, address: address },
          });
          setHomeAddress(address);
        }
      } else {
        ErrorHandler.setError(dispatch, response);
      }
      // }
    }
  };

  return (
    <PropertyValue
      propVal={propVal}
      setPropVal={setPropVal}
      propDues={propDues}
      setPropDues={setPropDues}
      selling={selling}
      setSelling={setSelling}
      onSave={onSave}
      onPropValChangeHandler={onPropValChangeHandler}
      onPropDuesChangeHandler={onPropDuesChangeHandler}
      homeAddress={homeAddress}
    />
  );
};

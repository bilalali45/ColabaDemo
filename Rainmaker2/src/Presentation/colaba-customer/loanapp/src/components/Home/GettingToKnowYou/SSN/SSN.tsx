import React, { useContext, useEffect, useState } from "react";
import { Borrower, SSNTabValidation } from "../../../../Entities/Models/types";
import PageHead from '../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../Shared/Components/TooltipTitle';
import BorrowerActions from "../../../../store/actions/BorrowerActions";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import { LocalDB } from "../../../../lib/LocalDB";
import { SSNBorrowerTabs } from "./SSNBorowerTabs";
import { Store } from "../../../../store/store";

import { OwnTypeEnum } from "../../../../Utilities/Enum";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { TenantConfigFieldNameEnum } from "../../../../Utilities/Enumerations/TenantConfigEnums";
export const SSN = () => {

  const [allBorrowers, setAllBorowers] = useState<Borrower[]>([]);
  const { state, dispatch } = useContext(Store);
  const { primaryBorrowerInfo }: any = state.loanManager;
  const [validationArray, setValidationArray] = useState<
    SSNTabValidation[]
  >([]);
  
  useEffect(() => {
    getAllBorrowers();
  }, []);

  /*
    @returns true if borrower's SSN and DOB is enabled by LeftMenuHandler's featureSettings
  */


  const getAllBorrowers = async () => {
    let res: any = await BorrowerActions.getAllBorrower(
      +LocalDB.getLoanAppliationId()
    );

    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        // filering borrowers based on LeftMenyHandler's featureSettings
        let filterdBorrowers = res?.data?.filter(x => NavigationHandler.filterBorrowerByFieldConfiguration(x));
        setAllBorowers(filterdBorrowers);
        updateValidationArray(filterdBorrowers);
      }
      else{
        ErrorHandler.setError(dispatch, res);
        }
    }
  };
  const updateValidationArray = async (borrowers: Borrower[]) => {
    let validationArr: SSNTabValidation[] = []
    borrowers.forEach(element => {
      let item: SSNTabValidation = {
        tabId: element.id,
        validated: false,
      }
      item = getTenantConfigForDobSSN(item, element.ownTypeId)
      validationArr.push(item)
    });


    setValidationArray(validationArr);
  };

  const getTenantConfigForDobSSN = (item : SSNTabValidation, ownTypeId: number) => {
    // const isDOBVisible= (ownTypeId == OwnTypeEnum.PrimaryBorrower ? NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PrimaryBorrowerDOB) : NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.CoBorrowerDOB))
        const isDOBRequired= (ownTypeId == OwnTypeEnum.PrimaryBorrower ? NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PrimaryBorrowerDOB, true) : NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.CoBorrowerDOB, true))
        if (isDOBRequired) {
          item.isDobRequired =true;
          item.hasDobValue = false;
        }
        else {
          item.isDobRequired =false;
          item.hasDobValue = true;
        }
        // const isSNNVisible= (ownTypeId == OwnTypeEnum.PrimaryBorrower ? NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PrimaryBorrowerSSN) : NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.CoBorrowerSSN))
       const  isSNNRequired= (ownTypeId == OwnTypeEnum.PrimaryBorrower ? NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PrimaryBorrowerSSN, true) : NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.CoBorrowerSSN, true))
       if (isSNNRequired) {
        item.isSSNRequired = true;
        item.hasSSNValue = false;
       }
        else {
          item.isSSNRequired = false;
          item.hasSSNValue = true;
         }
         return item
  }
  
  
  return (
    <div className="compo-abt-yourSelf fadein">
      <PageHead title="Personal Information" handlerBack={() => { }} />
      {/* <TooltipTitle title="Now, it's time to know Social Security Number of all borrowers." /> */}
      <TooltipTitle title={`Sorry to pry, ${primaryBorrowerInfo && primaryBorrowerInfo.name}, but we'll need some very sensitive data about you to continue.`} />
      <div className="comp-form-panel ssn-panel colaba-form">
        <div className="row form-group_">
          <div className="col-md-12">
            <SSNBorrowerTabs allBorrowers={allBorrowers}  setValidationArray={setValidationArray} validationArray={validationArray} />

          </div>
        </div>



      </div>

    </div>
  )
}

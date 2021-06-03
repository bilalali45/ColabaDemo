import React, { useState, useEffect, useContext } from 'react';

import { MilitaryServiceForm } from './MilitaryServiceForm';
import moment from "moment";
import { Store } from "../../../../store/store";
import { AddOrUpdateBorrowerVaStatusPayload } from '../../../../Entities/Models/AddOrUpdateBorrowerVaStatusPayload';
import { GetBorrowerVaDetailFormObjectProto, LoanInfoType } from '../../../../Entities/Models/types';
import MilitaryActions from '../../../../store/actions/MilitaryActions';
import Loader from '../../../../Shared/Components/Loader';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';


const MilitaryService = () => {
  const [militaryServiceData, setMilitaryServiceData] = useState<GetBorrowerVaDetailFormObjectProto | null> (null)
  const { state } = useContext(Store);
  const loanManager: any = state.loanManager;
  const loanInfo: LoanInfoType = loanManager.loanInfo;
  const [isClicked, setIsClicked] = useState<boolean>(false);

  useEffect(() => {
    fetchData()
  }, [loanInfo]);
  const fetchData = async () => {
    if (loanInfo && loanInfo.borrowerId != null){
      let militaryAffiliationsList = await MilitaryActions.fetchAllMilitaryAffiliationsList(loanInfo.borrowerId)
      if(militaryAffiliationsList) setMilitaryServiceData(militaryAffiliationsList);
    }
      
  }

  const onSubmit = async (data) => {
    if (!isClicked) {
      setIsClicked(true);

      const { activeDutyPersonnel, everActivatedDuringTour, lastDateOfTourOrService, performedMilitaryService, reserveOrNationalGuard, survivingSpouse, veteran } = data;
      // if (performedMilitaryService == null && !isRequired()) {
      //   LeftMenuHandler.moveNext();
      //   return;
      // }
      const vaServicePayload = new AddOrUpdateBorrowerVaStatusPayload(loanInfo.borrowerId);
      vaServicePayload.IsVaEligible = performedMilitaryService;

      if (activeDutyPersonnel) {
        vaServicePayload.MilitaryAffiliationIds.push(1);
        vaServicePayload.ExpirationDateUtc = moment.utc(lastDateOfTourOrService).format();
      }
      if (reserveOrNationalGuard) {
        vaServicePayload.MilitaryAffiliationIds.push(2); vaServicePayload.ReserveEverActivated = everActivatedDuringTour;
      }
      if (veteran)
        vaServicePayload.MilitaryAffiliationIds.push(3);
      if (survivingSpouse)
        vaServicePayload.MilitaryAffiliationIds.push(4);

      await MilitaryActions.addOrUpdateBorrowerVaStatus(vaServicePayload);

      NavigationHandler.moveNext();
    };
  }

  // const isRequired = () => {
  //   if (loanManager.loanInfo.ownTypeId == OwnTypeEnum.PrimaryBorrower) {
  //     return NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.PrimaryBorowerVeteranStatus, true);
  //   }
  //   else if (loanManager.loanInfo.ownTypeId == OwnTypeEnum.PrimaryBorrower) {
  //     return NavigationHandler.isFieldRequired(TenantConfigFieldNameEnum.CoBorowerVeteranStatus, true);
  //   }
  //   return true;
  // };

  return militaryServiceData ? <MilitaryServiceForm preloadedValues={militaryServiceData} formOnSubmit={onSubmit} loanManager={loanManager} isRequired={true} /> : <Loader type="widget" />
}

export default MilitaryService

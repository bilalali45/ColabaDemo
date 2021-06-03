import React, { useEffect, useState, useContext } from 'react';
import { LocalDB } from '../../../../lib/LocalDB';
import MaritalStatusActions from '../../../../store/actions/MaritalStatusActions';
import { MaritalStatusForm } from './MaritalStatusForm';
import { Store } from '../../../../store/store'
import { MaritalStatusPayload } from '../../../../Entities/Models/MaritalStatusPayload';
import { GettingToKnowYouSteps } from '../../../../store/actions/LeftMenuHandler';
import { LoanInfoType } from '../../../../Entities/Models/types';
import Loader from '../../../../Shared/Components/Loader';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';
import { OwnTypeEnum } from '../../../../Utilities/Enum';
import { TenantConfigFieldNameEnum } from '../../../../Utilities/Enumerations/TenantConfigEnums';


export const MaritalStatus = () => {
  const [maritalStatusData, setMaritalStatusData] = useState(null)
  const [personMaritalStatusData, setPersonMaritalStatusData] = useState(null)
  const { state } = useContext(Store);
  const loanManager: any = state.loanManager;
  const loanInfo: LoanInfoType = loanManager.loanInfo;
  const [isClicked, setIsClicked] = useState<boolean>(false);
  useEffect(() => {
    fetchData()
  }, [loanInfo])
  const fetchData = async () => {
    if (loanInfo.borrowerId) {
      setPersonMaritalStatusData(await MaritalStatusActions.getMaritalStatus(Number(LocalDB.getLoanAppliationId()), loanInfo.borrowerId));
    }
    setMaritalStatusData(await MaritalStatusActions.getAllMaritalStatuses());
  }

  const onSubmit = async (data) => {
    if (!isClicked) {
      setIsClicked(true);
      const primaryAddOrUpdateMaritalStatusPayload = new MaritalStatusPayload(Number(LocalDB.getLoanAppliationId()), NavigationHandler.getNavigationStateAsString(), data.maritalStatus, loanInfo?.borrowerId, data.maritalStatus, data.isPrimaryBorrowerSpouse);
      await MaritalStatusActions.addOrUpdateMaritalStatus(primaryAddOrUpdateMaritalStatusPayload);

      // For Primary Borrower

      if (loanInfo.ownTypeId == OwnTypeEnum.PrimaryBorrower && !NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PrimaryBorowerVeteranStatus)) {
        NavigationHandler.disableFeature(GettingToKnowYouSteps.MilitaryService);
      }
      else if (loanInfo.ownTypeId == OwnTypeEnum.PrimaryBorrower && NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PrimaryBorowerVeteranStatus)) {
        NavigationHandler.enableFeature(GettingToKnowYouSteps.MilitaryService);
      }

      // For Secondary Borrower
      if (loanInfo.ownTypeId == OwnTypeEnum.SecondaryBorrower && !NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.CoBorowerVeteranStatus)) {
        NavigationHandler.disableFeature(GettingToKnowYouSteps.MilitaryService);
      }
      else if (loanInfo.ownTypeId == OwnTypeEnum.SecondaryBorrower && NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.CoBorowerVeteranStatus)) {
        NavigationHandler.enableFeature(GettingToKnowYouSteps.MilitaryService);
      }

      NavigationHandler.moveNext();
    }
  }

  return maritalStatusData ? <MaritalStatusForm allMaritialOptions={maritalStatusData} currentMaritalStatus={personMaritalStatusData} formOnSubmit={onSubmit} loanManager={loanManager} /> : <Loader type="widget" />
}

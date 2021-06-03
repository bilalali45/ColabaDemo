import React, { useEffect, useContext } from 'react'
import { Switch } from 'react-router-dom'
import IncomeModal from '../../IncomeModal/IncomeModal'
import { PreviousEmploymentAmount } from '../PreviousEmploymentAmount/PreviousEmploymentAmount'
import { PreviousEmploymentAddress } from '../PreviousEmploymentAddress/PreviousEmploymentAddress'
import { ApplicationEnv } from '../../../../../../lib/appEnv'
import { Store } from '../../../../../../store/store'
import { PreviousEmployment } from '../PreviousEmployment/PreviousEmployment'
import EmploymentActions from '../../../../../../store/actions/EmploymentActions'
import { LocalDB } from '../../../../../../lib/LocalDB'
import { ErrorHandler } from '../../../../../../Utilities/helpers/ErrorHandler'
import { EmploymentHistoryActionTypes } from '../../../../../../store/reducers/EmploymentHistoryReducer'
import { EmployerHistoryInfo } from '../../../../../../Entities/Models/EmploymentHistory'
import { CommonType } from '../../../../../../store/reducers/CommonReducer'
import { IsRouteAllowed } from '../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed'

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/EmploymentHistory/EmploymentHistoryDetails`;
const modalClosePath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/EmploymentHistory`;

export const EmploymentHistoryDetails = () => {

    const { state, dispatch } = useContext(Store);

    const commonManager: any = state.commonManager;
    const { previousEmployerInfo }: any = state.employmentHistory;
    const {incomeInfo, loanInfo} :any = state.loanManager;
    
    useEffect(()=>{

        if(!previousEmployerInfo){
          if(incomeInfo && incomeInfo?.incomeId){
            getEmploymentIncomeDetails()
          } 
        }
        //let title = `${StringServices.capitalizeFirstLetter(loanInfo?.borrowerName)}'s Previous Employment`;
        dispatch({type: CommonType.SetIncomePopupTitle, payload: 'Previous Employment'});
      },[])



    const getEmploymentIncomeDetails = async() => {
        let res: any = await EmploymentActions.getEmployerInfo(+LocalDB.getLoanAppliationId(), +loanInfo.borrowerId, +incomeInfo.incomeId);
    
        if (res) {
          if (ErrorHandler.successStatus.includes(res.statusCode)) {
           console.log(res.data)
           const {employerAddress, employmentInfo, wayOfIncome} = res.data;

           setEmployerInfo(employmentInfo)
           dispatch({type: EmploymentHistoryActionTypes.SetPreviousEmployerAddress, payload: employerAddress});
           dispatch({type: EmploymentHistoryActionTypes.SetPreviousEmploymentIncome, payload: wayOfIncome});
          }
          else{
            ErrorHandler.setError(dispatch, res);
            }
        }
      }
      

      const setEmployerInfo = (employmentInfo)=>{
          let {employerName, jobTitle, startDate, endDate,  ownershipInterest, hasOwnershipInterest} = employmentInfo;
          let employmentDetails: EmployerHistoryInfo = {
              EmployerName: employerName,
              JobTitle: jobTitle,
              StartDate:startDate,
              EndDate: endDate,
              EmployedByFamilyOrParty: false,
              EmployerPhoneNumber:"",
              YearsInProfession:0,
              IncomeInfoId:incomeInfo.incomeId,
              HasOwnershipInterest: hasOwnershipInterest,
              OwnershipInterest:ownershipInterest
          }
          dispatch({type: EmploymentHistoryActionTypes.SetPreviousEmployerInfo, payload: employmentDetails});
      }
    return (
        <IncomeModal
            title={commonManager?.incomePopupTitle}
            className="nothaveFooter"
            closePath={modalClosePath}
            handlerCancel={() => { }} >

            <Switch>
                <IsRouteAllowed path={`${containerPath}/PreviousEmployment`} component={PreviousEmployment} />
                <IsRouteAllowed path={`${containerPath}/PreviousEmploymentAddress`} component={PreviousEmploymentAddress} />
                <IsRouteAllowed path={`${containerPath}/PreviousEmploymentAmount`} component={PreviousEmploymentAmount} />
            </Switch>
        </IncomeModal>
    )
}
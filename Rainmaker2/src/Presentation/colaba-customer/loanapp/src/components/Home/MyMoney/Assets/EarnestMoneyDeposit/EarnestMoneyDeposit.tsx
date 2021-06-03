import React, { useContext, useEffect, useState } from 'react'
import { LocalDB } from '../../../../../lib/LocalDB';
import AssetsActions from '../../../../../store/actions/AssetsActions';
import { Store } from '../../../../../store/store';
import { ErrorHandler } from '../../../../../Utilities/helpers/ErrorHandler';
import { NavigationHandler } from '../../../../../Utilities/Navigation/NavigationHandler';
import { EarnestMoneyDepositWeb } from './EarnestMoneyDeposit_Web';



export const EarnestMoneyDeposit = () => {

  const [hasEarnestMoneyDeposit, setEarnestMoneyDeposit] = useState<boolean>(false);
  const { dispatch } = useContext(Store);
  const [amount, setAmount] = useState<string | null>(null);
  const [btnClick, setBtnClick] = useState<boolean>(false);

  useEffect(() => {
    getEarnestMoneyDeposit();
  }, [])

  const getEarnestMoneyDeposit = async () => {
    let response = await AssetsActions.GetEarnestMoneyDeposit(+LocalDB.getLoanAppliationId());
    if (response) {
      if (ErrorHandler.successStatus.includes(response.statusCode)) {
        let { deposit, isEarnestMoneyProvided } = response.data;
        if (isEarnestMoneyProvided === true || isEarnestMoneyProvided === false) {
          if(deposit){
            setAmount(deposit);
            setEarnestMoneyDeposit(true);
          }else{
            setEarnestMoneyDeposit(false);
          }        
        } else {
          setEarnestMoneyDeposit(null)
        }

      } else {
        ErrorHandler.setError(dispatch, response);
      }
    }
  }

  const UpdateEarnestMoneyDeposit = async (loanApplicationId: number, deposit: number | null, state: string) => {
    let response = await AssetsActions.UpdateEarnestMoneyDeposit(loanApplicationId, deposit, state, hasEarnestMoneyDeposit);
    if (response) {
      if (ErrorHandler.successStatus.includes(response.statusCode)) {
        NavigationHandler.moveNext();
      } else {
        setBtnClick(false);
        ErrorHandler.setError(dispatch, response);
      }
    }
  }

  const onSubmit = async (data) => {
    if (!btnClick) {
      setBtnClick(true);
      if (hasEarnestMoneyDeposit) {
        let { earnestMoneyDAmount } = data;
        let deposit = +(earnestMoneyDAmount.replace(/\,/g, ''))
        UpdateEarnestMoneyDeposit(+LocalDB.getLoanAppliationId(), deposit, NavigationHandler.getNavigationStateAsString())
      } else {
        UpdateEarnestMoneyDeposit(+LocalDB.getLoanAppliationId(), null, NavigationHandler.getNavigationStateAsString())
      }
    }
  }

  return (
    <EarnestMoneyDepositWeb
      hasEarnestMoneyDeposit={hasEarnestMoneyDeposit}
      setEarnestMoneyDeposit={setEarnestMoneyDeposit}
      setAmount={setAmount}
      amount={amount}
      onSubmit={onSubmit}
    />
  );
}

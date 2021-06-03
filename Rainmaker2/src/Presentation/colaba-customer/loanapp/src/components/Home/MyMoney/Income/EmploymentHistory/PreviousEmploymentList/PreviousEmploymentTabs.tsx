import React, { useState, useEffect, useContext } from "react";
import Tabs from "react-bootstrap/Tabs";
import Tab from "react-bootstrap/Tab";

import { PreviousEmploymentList } from "./PreviousEmploymentList";
import { BorrowerIncome } from "../../../../../../Entities/Models/EmploymentHistory";
import { StringServices } from "../../../../../../Utilities/helpers/StringServices";
import { Store } from "../../../../../../store/store";


type PreviousEmploymentTabsProps = {
  deleteIncome:Function;
  editIncome:Function;
};

export const PreviousEmploymentTabs = ({deleteIncome, editIncome}:PreviousEmploymentTabsProps) => {
  const [key, setKey] = useState<string | null>("");
  const { state } = useContext(Store);
  const {borrowersIncome}:any = state.employmentHistory;
  const {loanInfo}: any =state.loanManager;

  useEffect(() => {
    setFirstTab()
  }, [borrowersIncome])

  const setFirstTab = () => {
    if (loanInfo && loanInfo?.borrowerId) {
      setKey(loanInfo?.borrowerId.toString());
    }
  }

  return (
    <div className="ssn-panel e-history">
      {borrowersIncome && borrowersIncome?.length === 1 && (
        <PreviousEmploymentList borrowerEmploymentHistory = {borrowersIncome[0] }setTabKey={setKey} deleteIncome={deleteIncome} editIncome={editIncome}/>
      )}
      {borrowersIncome && borrowersIncome?.length > 1 && (
        <Tabs
          data-testid={`tab`}
          id="employment-history-tabs"
          activeKey={key}
          onSelect={(k) => {
            setKey(k);
          }}>
             { borrowersIncome.map((borrower:BorrowerIncome)=> 
          <Tab
            data-testid={`tab${borrower?.borrowerId}`}
            key={borrower?.borrowerId}
            eventKey={borrower?.borrowerId?.toString()}
            title={StringServices.capitalizeFirstLetter(borrower?.borrowerName)}>
            <PreviousEmploymentList borrowerEmploymentHistory={borrower} setTabKey={setKey} deleteIncome={deleteIncome} editIncome={editIncome}/>
          </Tab>)}
        </Tabs>
      )}
    </div>
  );
};

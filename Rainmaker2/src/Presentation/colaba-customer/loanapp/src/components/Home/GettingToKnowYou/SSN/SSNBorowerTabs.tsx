import React, {
  useEffect,
  useState,
} from "react";
import Tabs from "react-bootstrap/Tabs";
import Tab from "react-bootstrap/Tab";
import { Borrower } from "../../../../Entities/Models/types";


import { SSNDobForm } from "./_SSN";

import { StringServices } from "../../../../Utilities/helpers/StringServices";

export const SSNBorrowerTabs = ({ allBorrowers, setValidationArray, validationArray }: any) => {



  const [key, setKey] = useState<string | null>("");
  
  // const filteredBorrowers = (): Borrower[] => {
  //   var res = allBorrowers.filter(x => filterBorrowerByFieldConfiguration(x));
  //   console.log("!! Filtered borrowers");
  //   console.log(res);
  //   return res;
  // };

  useEffect(() => {
    setFirstTab()
  }, [allBorrowers])

  const setFirstTab = () => {
    if (allBorrowers && allBorrowers.length) {
      setKey(allBorrowers[0].id);
    }
  }


  return (
    <div>
      { allBorrowers && allBorrowers.length == 1 &&
        <SSNDobForm
          allBorrowers={allBorrowers}
          borrower={allBorrowers[0]}
          tabKey={key!}
          setTabKey={setKey}
          setValidationArray={setValidationArray}
          validationArray={validationArray}
        />
      }
      { allBorrowers && allBorrowers.length > 1 &&
        <Tabs
          data-testid={`tab`}
          id="ssn-borrower-tabs"
          activeKey={key}
          onSelect={(k) => {
            setKey(k);
          }}>
          {
            allBorrowers.map((borrower: Borrower) => (
              <Tab
                data-testid={`tab${borrower.id}`}
                key={borrower.id}
                eventKey={borrower.id.toString()}
                title={StringServices.capitalizeFirstLetter(borrower.firstName)}>
                <SSNDobForm
                  allBorrowers={allBorrowers}
                  borrower={borrower}
                  tabKey={key!}
                  setTabKey={setKey}
                  setValidationArray={setValidationArray}
                  validationArray={validationArray}
                />
              </Tab>
            ))}
        </Tabs>
      }
    </div>


  );
};

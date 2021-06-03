import React, {
  useEffect,
  useState,
} from "react";
import Tabs from "react-bootstrap/Tabs";
import Tab from "react-bootstrap/Tab";
import {
  Borrower,
  tabValidationArrayType,
} from "../../../../Entities/Models/types";
import { Agreement } from "../../../../components/Home/GettingToKnowYou/Consent/Agreement";
import BorrowerActions from "../../../../store/actions/BorrowerActions";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import { LocalDB } from "../../../../lib/LocalDB";
import Loader from "../../../../Shared/Components/Loader";
import { StringServices } from "../../../../Utilities/helpers/StringServices";

export const BorrowerConsentTabs = () => {
  
  const [key, setKey] = useState<string | null>("");
  const [allBorrowers, setAllBorowers] = useState<Borrower[]>([]);
  const [validationArray, setValidationArray] = useState<
    tabValidationArrayType[]
  >([]);

  useEffect(() => {
    getAllBorrowers();
  }, []);

  const getAllBorrowers = async () => {
    let res: any = await BorrowerActions.getAllBorrower(
      +LocalDB.getLoanAppliationId()
    );
    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        setAllBorowers(res.data);
        setKey(res?.data[0]?.id);
        updateValidationArray(res.data);
      }
    }
  };

  const updateValidationArray = async (borrowers: Borrower[]) => {
    let validationArr: tabValidationArrayType[] = []
    borrowers.forEach(element => {
      let item: tabValidationArrayType = {
        tabId: element.id,
        validated:false
      }
      validationArr.push(item)
    });

    setValidationArray(validationArr);
  };
  
  return (
    <form  data-testid="consent-tab-screen" >
      {allBorrowers && allBorrowers.length == 1 &&
          <Agreement 
          borrower = {allBorrowers[0]}
          allBorrowers = {allBorrowers}
          validationArray={validationArray}
          setValidationArray={setValidationArray}
          setTabKey = {setKey}
          key= {key}
          />
        }
      {(allBorrowers && allBorrowers.length > 1) &&
        <Tabs
        id="controlled-tab-example"
        activeKey={key}
        onSelect={(k) => {
          setKey(k);
        }}
      >
        {
          allBorrowers.map((borrower: Borrower) => (
            <Tab
              key={borrower.id}
              eventKey={borrower.id.toString()}
              title={StringServices.capitalizeFirstLetter(borrower.firstName)}
            >
              <Agreement 
                borrower = {borrower}
                allBorrowers = {allBorrowers}
                validationArray={validationArray}
                setValidationArray={setValidationArray}
                setTabKey = {setKey}
              />
            </Tab>
          ))}
      </Tabs>
      }
      {allBorrowers.length === 0 &&
          <div><Loader type="widget"/></div>
      }
      
    </form>
  );
};

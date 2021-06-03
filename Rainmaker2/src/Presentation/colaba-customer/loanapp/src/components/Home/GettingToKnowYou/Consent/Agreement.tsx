import React, { useEffect, useState, useRef, useContext } from "react";
import {
  Borrower,
  consentType,
  tabValidationArrayType,
} from "../../../../Entities/Models/types";
import { useForm } from "react-hook-form";
import InputCheckedBox from "../../../../Shared/Components/InputCheckedBox";
import { AgreeHeadingIcon } from "../../../../Shared/Components/SVGs";
import BorrowerActions from "../../../../store/actions/BorrowerActions";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import { LocalDB } from "../../../../lib/LocalDB";
import { Store } from "../../../../store/store";
import { CommonType } from "../../../../store/reducers/CommonReducer";
import Loader from "../../../../Shared/Components/Loader";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

type props = {
  borrower: Borrower,
  allBorrowers: Borrower[],
  setTabKey: Function,
  setValidationArray: Function
  validationArray: tabValidationArrayType[];
  key?: string;
};

export const Agreement = ({ validationArray, setValidationArray, borrower, allBorrowers, setTabKey, key }: props) => {
  const { dispatch } = useContext(Store);
  
  const [activeCheckBox, setCheckBox] = useState<boolean>(false);
  const [disableSaveBtn, setdisableSaveBtn] = useState<boolean>(true);
  const [check, setCheck] = useState<boolean>(false);
  const [consent, setAllConsent] = useState<consentType[]>([]);
  const [hash, setHash] = useState<string>("");
  const [isAccepted, setIsAccepted] = useState<boolean>(false);
  const inputRef = useRef<any>([]);

  const { handleSubmit } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "firstError",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  useEffect(() => {
    checkIsConsentAccepted();
  }, []);

  useEffect(() => {
    if (consent.length > 0) {
      var hasScrollbar = inputRef.current.scrollHeight > inputRef.current.clientHeight;
      if (!hasScrollbar) {
        setCheckBox(true);
      }  
    }
  }, [key])

  const checkIsConsentAccepted = async () => {
    let response = await GetBorrowerAcceptedConsents(+(LocalDB.getLoanAppliationId()), borrower.id);
    const { isAccepted, acceptedConsentList } = response;
    if (isAccepted) {
      setAllConsent(acceptedConsentList);
      setIsAccepted(isAccepted);
      setCheck(true);
      setTabValidation();
    } else {
      setIsAccepted(isAccepted);
      GetAllConsentTypes();
    }
  }

  const GetAllConsentTypes = async () => {
    let res: any = await BorrowerActions.getAllConsentTypes(borrower.id);
    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        const { consentHash, consentList } = res.data;
        setAllConsent(consentList);
        setHash(consentHash);
        if(consentList.length < 3) setCheckBox(true);
      } else {
        ErrorHandler.setError(dispatch, res);
      }
    }
  };

  const GetBorrowerAcceptedConsents = async (loanApplicationId: number, borrowerId: number) => {
    let res: any = await BorrowerActions.getBorrowerAcceptedConsents(loanApplicationId, borrowerId);
    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        return res.data;
      } else {
        ErrorHandler.setError(dispatch, res);
      }
    }
  };

  const renderAgreementHtml = () => {
    return (
      <>
        {consent?.map((item: any) => {
          return (
            <div className="item">
              <h4>
                <span className="h-icon">
                  <AgreeHeadingIcon />
                </span>
                {item.name}
              </h4>

              <p>{item.description}</p>
            </div>
          );
        })}
      </>
    );
  };

  const setTabValidation = () => {
    setValidationArray((prevState) => [
      ...prevState.map((item) => {
        if (item.tabId === borrower.id) {
          let idx = prevState.map((b) => { return b.tabId; }).indexOf(borrower.id)
          return prevState[idx] = { tabId: borrower.id, validated: true }
        }
        return item;
      }),
    ])
  }

  const checkIfLastTab = (currentTab: number) => {
    if (allBorrowers.length - 1 === currentTab) return true;

    return false;
  }

  const checkAllTabsValidations = () => {
    let isNotAcceptedFound: boolean = false;
    for (let index = 0; index < validationArray.length; index++) {
      const element = validationArray[index];
      if (!element?.validated) {
        setTabKey(element?.tabId);
        dispatch({ type: CommonType.SetBorrowerConsent, payload: true });
        isNotAcceptedFound = true;
        break;
      }
    }
    if (!isNotAcceptedFound) NavigationHandler.moveNext();
  }

  const acceptConsentHandler = async () => {
    if (!isAccepted) {
      let response = await BorrowerActions.addOrUpdateBorrowerConsents(
        +(LocalDB.getLoanAppliationId()),
        borrower.id,
        true,
        NavigationHandler.getNavigationStateAsString(),
        hash
      )
      if (response) {
        if (ErrorHandler.successStatus.includes(response.statusCode)) {
          setIsAccepted(true);
        } else if (response?.response?.status === "400") {
          window.location.reload(false);
        } else {
          ErrorHandler.setError(dispatch, response)
          return;
        }

      }

    }
    let currentBorrowerTabIdx = allBorrowers.map(function (b) { return b.id; }).indexOf(borrower.id);
    setTabValidation();
    let lastTab = checkIfLastTab(currentBorrowerTabIdx);
    if (lastTab) {
      checkAllTabsValidations();
    } else {
      if (validationArray.find(item => item.validated === false)) setTabKey(allBorrowers[currentBorrowerTabIdx + 1]?.id.toString());
      else NavigationHandler.moveNext();
    }

  }

 

  return (
    <>
      {consent.length === 0 &&
        <div><Loader type="widget" /></div>
      }

      {consent.length > 0 &&
        <div className="agree-cWrap">
          <div className="p-body">          
          <div className="row">
            <div className="col-sm-12">
              <div
                data-testid="agreement-screen"
                id={String(borrower.id)}
                className="item-wrap"
                ref={inputRef}
                onScroll={() => {
                  if (inputRef && inputRef != undefined) {
                    let endScroll = inputRef.current?.scrollTop + inputRef.current?.offsetHeight;
                    let currentPositionScroll = inputRef.current?.scrollHeight;
                    if (Math.round(endScroll) === Math.round(currentPositionScroll)) setCheckBox(true);
                  }
                }}
              >

                {renderAgreementHtml()}
              </div>
            </div>

            <div className="col-sm-12">
              <div className="form-group chk-agree">
                <InputCheckedBox
                  id=""
                  className={isAccepted ? "disabled" : activeCheckBox ? "" : "disabled"}
                  name=""
                  label={`I, ${borrower.firstName} Borrower, agree to the above`}
                  checked={check}
                  value={""}
                  onChange={() => {
                    setCheck(!check);
                    setdisableSaveBtn(!disableSaveBtn);
                    dispatch({ type: CommonType.SetBorrowerConsent, payload: false });
                  }}
                  onClick={() => {
                    setCheck(!check);
                    setdisableSaveBtn(!disableSaveBtn);
                    dispatch({ type: CommonType.SetBorrowerConsent, payload: false });
                  }}
                ></InputCheckedBox>
              </div>       
            </div>
          </div>
          </div>
          <div className="form-footer">
            <button
              data-testid= "acceptBtn"
              disabled={isAccepted ? false : disableSaveBtn}
              className="btn btn-primary"
              onClick={handleSubmit(acceptConsentHandler)}
            >
              {"Save & Continue"}
            </button>
          </div>
        </div>
      }
    </>
  );
};

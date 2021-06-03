import React, { Fragment, useContext, useEffect, useState } from "react";
import Dropdown from "react-bootstrap/Dropdown";
import { CurrentHomeAddressReqObj, HomeOwnershipTypes } from "../../Entities/Models/types";
import GettingToKnowYouActions from "../../store/actions/GettingToKnowYouActions";
import { LoanApplicationActionsType } from "../../store/reducers/LoanApplicationReducer";
import { Store } from "../../store/store";
import { ErrorHandler } from "../../Utilities/helpers/ErrorHandler";
import DropdownList from "./DropdownList";
import _ from "lodash";
import { CommaFormatted, removeCommaFormatting } from "../../Utilities/helpers/CommaSeparteMasking";
import InputField from "./InputField";
import { HousingStatusEnum } from "../../Utilities/Enumerations/HomeEnums";

type HomeStatusProps = {
  register: any,
  errors: any,
  getValues: Function,
  setValue: Function,
  selectedHomeOwnershipTypeObj: HomeOwnershipTypes | undefined
  setSelectedHomeOwnershipTypeObj: Function,
  initialData: CurrentHomeAddressReqObj,
  clearErrors: Function,
  toggleSection:boolean,
  setIsButtonActive?:Function, 
  checkFormValidity?:Function
};


const HousingStatus = ({ register, errors, setValue, clearErrors, setSelectedHomeOwnershipTypeObj, toggleSection, initialData,
  checkFormValidity, selectedHomeOwnershipTypeObj }: HomeStatusProps) => {
  const { state, dispatch } = useContext(Store);
  const { loanInfo, homeOwnershipTypes }: any = state.loanManager;
  const [monthRent, setMonthRent] = useState<string>("");
  const [selectedHomeOwnershipType, setSelectedHomeOwnershipType] = useState<string>("");

  useEffect(() => {
    getHomeOwnershipTypes();
  }, []);

  useEffect(() => {
    if (initialData && loanInfo.ownTypeId === 1)
      setInitialFields(initialData)

  }, [initialData])


  useEffect(()=>{
    if(homeOwnershipTypes && homeOwnershipTypes.length && initialData){
      if(loanInfo.ownTypeId === 1)
        setInitialFields(initialData);
    }
  },[homeOwnershipTypes])

  const setInitialFields = async (dataValues: CurrentHomeAddressReqObj) => {
    if (!_.isEmpty(dataValues)) {
    const { housingStatusId, rent } = dataValues;
    if(housingStatusId){
      let housingStatus = await onHomeTypeSelection(housingStatusId);
      if(housingStatus){
        setValue("housingStatus", housingStatus.name);
        setSelectedHomeOwnershipType(housingStatus.name)
      } 
    }
   

    if(rent){
      setValue("monthlyRent", rent);
      setMonthRent(rent?.toString())
    }
   
  }
  else{
    setValue("housingStatus", "");
    // setValue("monthlyRent", "");
  }
  checkFormValidity && checkFormValidity()
  }
  const getHomeOwnershipTypes = async () => {
    let res: any = await GettingToKnowYouActions.getHomeOwnershipTypes();

    if (res) {
      // console.log(res)
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        dispatch({type: LoanApplicationActionsType.SetHomeOwnershiptypes, payload: res.data});
        // setSuccessMessage(true);
      }
      else{
        ErrorHandler.setError(dispatch, res);

        }
    }
  };

  const onHomeTypeSelection = async (homeType: string | number) => {
    let selectedHomeType = homeOwnershipTypes && homeOwnershipTypes?.filter((homeOwnershipType: HomeOwnershipTypes) => homeOwnershipType.id === +homeType)[0]
    if(selectedHomeType){
      await setSelectedHomeOwnershipTypeObj(selectedHomeType)
      await setSelectedHomeOwnershipType(selectedHomeType.name)
      clearErrors("housingStatus")
    }
    checkFormValidity && checkFormValidity()
    return selectedHomeType;
  } 

  const checkRentValidity = () =>{
   
   if(Number(monthRent) > 999999 || Number(monthRent) < 1 ) return true;

    return null;
  }

  const onRentChangeHandler = async (event)=> {
    const value = event.target.value;
    if(value.length > 8) return;
    if (isNaN(Number(value.replace(/\,/g, '')))) return;
    setMonthRent(value.replace(/\,/g, ''));            
    clearErrors("monthlyRent")
    checkFormValidity && checkFormValidity()
  }
  return (
    <Fragment>
      <div className={`col-md-6 fadein ${!toggleSection && "d-none"}`}>
        <DropdownList
          label={"Housing Status"}
          data-testid="Housing-Status"
          id="housingStatus"
          placeholder="Select Housing Status"
          name="housingStatus"
          onDropdownSelect={onHomeTypeSelection}
          value={selectedHomeOwnershipType}
          register={register}
          rules={{
            required: "This field is required.",
          }}
          errors={errors}
        >
          {homeOwnershipTypes && homeOwnershipTypes.map((homeOwnershipType: any) => (
            <Dropdown.Item data-testid={"housingStatus-option" }key={homeOwnershipType.id} eventKey={homeOwnershipType.id}>
              {homeOwnershipType.name}
            </Dropdown.Item>
          ))}
        </DropdownList>
      </div>
      {selectedHomeOwnershipTypeObj && selectedHomeOwnershipTypeObj.id === HousingStatusEnum.Rent &&
        <div className={`col-md-6 fadein ${!toggleSection && "d-none"}`}>
          <InputField
            minLength={10}
            label={"Monthly Rent"}
            data-testid="Monthly-Rent"
            id="Monthly-Rent"
            name="monthlyRent"
            type={"text"}
            placeholder={"Rent amount"}
            icon={<i className="zmdi zmdi-money"></i>}
            register={register}
            value = {monthRent}
            rules={{
              required: "This field is required.",
              validate: {
                validity: () =>
                !checkRentValidity() ||
                  "Amount must be between $1 and $999,999.",
              },
            }}
            onChange = {onRentChangeHandler}                   
            onBlur = {() => {
                let rent  = Number(monthRent).toFixed(2);
                if(rent === "NaN") return;
                setMonthRent(CommaFormatted(String(rent)))
                //setMonthRent(String(rent))
            }}
            onFocus={() =>{
              setMonthRent(removeCommaFormatting(monthRent));
            }}
            errors={errors}
          />
        </div>
      }
    </Fragment>
  );
};

export default HousingStatus;

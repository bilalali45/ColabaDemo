import React, { useContext, useEffect, useState } from "react";
import { Store } from "../../../../../../../store/store";
import { MilitaryIncomeWeb } from "./MilitaryIncome_Web";
import {MilitaryIncomeEmployer} from "../../../../../../../Entities/Models/types";
import { MilitaryIncomeActionTypes } from "../../../../../../../store/reducers/MilitaryIncomeReducer";
import { NavigationHandler } from "../../../../../../../Utilities/Navigation/NavigationHandler";


export const MilitaryIncome = () => {

    const { state, dispatch } = useContext(Store);
    const {militaryEmployer}: any = state.militaryIncomeManager;
    const [startDate, setStartDate] = useState<Date | null>(null); 
    const [employerName, setEmployerName] = useState<string>("");
    const [jobTitle, setJobTitle] = useState<string>("");
    const [yearsInProfession, setYearsInProfession] = useState<string>("");


    useEffect(() => {
     if(militaryEmployer){
         const {EmployerName, JobTitle, startDate, YearsInProfession} = militaryEmployer;
         setEmployerName(EmployerName);
         setJobTitle(JobTitle);
         setYearsInProfession(YearsInProfession);
         setStartDate(new Date(startDate));
     }
    },[militaryEmployer])

  const onNextClickHandler = (employer: MilitaryIncomeEmployer) => {
        let employerInfo: MilitaryIncomeEmployer = {
            EmployerName:employer.EmployerName,
            JobTitle:employer.JobTitle,
            startDate: employer.startDate,
            YearsInProfession: employer.YearsInProfession ? +employer.YearsInProfession : null,
          }
            
          dispatch({type: MilitaryIncomeActionTypes.SetMilitaryEmployer, payload: employerInfo});
          NavigationHandler.navigation?.moveNext();
    }
 

    return (
        <div>
           <MilitaryIncomeWeb
           startDate = {startDate}
           setStartDate = {setStartDate}
           employerName = {employerName}
           setEmployerName ={setEmployerName}
           jobTitle= {jobTitle}
           setJobTitle = {setJobTitle}
           yearsInProfession = {yearsInProfession}
           setYearsInProfession = {setYearsInProfession}
           onNextClick = {onNextClickHandler}
           />
        </div>
    )
}

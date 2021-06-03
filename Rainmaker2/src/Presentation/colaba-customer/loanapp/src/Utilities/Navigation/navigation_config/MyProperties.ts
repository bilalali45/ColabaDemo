import { NavStepConfigType } from "../LoanNavigator";

export enum MyPropertiesSteps {
    CurrentResidence = "Current Residence",
    CurrentResidenceDetails = "Current Residence Details",
    FirstCurrentResidenceMortgage = "First Current Residence Mortgage",
    FirstCurrentResidenceMortgageDetails = "First Current Residence Mortgage Details",
    SecondCurrentResidenceMortgage = "Second Current Residence Mortgage",
    SecondCurrentResidenceMortgageDetails = "Second Current Residence Mortgage Details",
    AllProperties = "All Properties",
    AdditionalPropertyType = "Additional  Property Type",
    AdditionalPropertyAddress = " Additional Property Address",
    AdditionalPropertyUsage = "Additional  Property Usage",
    AdditionalPropertyDetails = " Additional Property Details",
    AdditionalPropertyFirstMortgage = "Additional Property First Mortgage",
    AdditionalPropertyFirstMortgageDetails = "Additional Property First Mortgage Details",
    AdditionalPropertySecondMortgage = "Additional Property Second Mortgage",
    AdditionalPropertySecondMortgageDetails = "Additional Property Second Mortgage Details",
    PropertiesOwned = "Properties Owned",
    PropertiesReview = "Properties Review"
}

export class MyPropertiesStepsConstants {
    static OwnHousingStatusSteps: string[] = [
        MyPropertiesSteps.CurrentResidence,
        MyPropertiesSteps.CurrentResidenceDetails,
        MyPropertiesSteps.FirstCurrentResidenceMortgage,
        MyPropertiesSteps.FirstCurrentResidenceMortgageDetails,
        MyPropertiesSteps.SecondCurrentResidenceMortgage,
        MyPropertiesSteps.SecondCurrentResidenceMortgageDetails,
    ];

    static RentorNoPrimaryHousingStatusSteps: string[] = [
        MyPropertiesSteps.PropertiesOwned
    ]
}

export class MyProperties {
    static myPropertiesSteps: NavStepConfigType[] = [

        {
            name: MyPropertiesSteps.CurrentResidence,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.CurrentResidenceDetails,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.FirstCurrentResidenceMortgage,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.FirstCurrentResidenceMortgageDetails,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.SecondCurrentResidenceMortgage,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.SecondCurrentResidenceMortgageDetails,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.PropertiesOwned,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.AllProperties,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.AdditionalPropertyType,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.AdditionalPropertyUsage,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.AdditionalPropertyAddress,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.AdditionalPropertyDetails,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.AdditionalPropertyFirstMortgage,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.AdditionalPropertyFirstMortgageDetails,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.AdditionalPropertySecondMortgage,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.AdditionalPropertySecondMortgageDetails,
            subSteps: []
        },
        {
            name: MyPropertiesSteps.PropertiesReview,
            subSteps: []
        }
    ]
}
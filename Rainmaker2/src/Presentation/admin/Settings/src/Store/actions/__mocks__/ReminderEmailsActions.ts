import { Token } from "typescript";
import ReminderSettingTemplate, { ReminderEmailTemplate } from "../../../Entities/Models/ReminderEmailListTemplate";
import { Tokens } from "../../../Entities/Models/Token";

let mockData = {
    "isActive": true,
    "emailReminders": [
        {
            "id": "6017dfedb8e4423af06f7816",
            "noOfDays": "2",
            "recurringTime": new Date("2021-02-01T11:00:46Z"),
            "isActive": true,
            "email": {
                "id": undefined,
                "fromAddress": "sdhfsdj@cvxcv.vxcv",
                "ccAddress": "",
                "subject": "You have new tasks to complete for your Texas Trust Home Loans loan application",
                "emailBody": "<p>sdb gashdghasg dhjsagd hgashd ashd hjsagdhj agd adda</p>\n"
            }
        },
        {
            "id": "6017e415b8e4423af06f7823",
            "noOfDays": "6",
            "recurringTime":new Date("2021-02-01T14:00:00Z"),
            "isActive": false,
            "email": {
                "id": "6017e415b8e4423af06f7822",
                "fromAddress": "###RequestorUserEmail###",
                "ccAddress": "###Co-BorrowerEmailAddress###",
                "subject": "###BusinessUnitName###",
                "emailBody": "<p>jjjjj</p>\n"
            }
        },
        {
            "id": "6017e2ccb8e4423af06f7821",
            "noOfDays": "4",
            "recurringTime": new Date("2021-02-01T14:30:00Z"),
            "isActive": true,
            "email": {
                "id": "6017e2ccb8e4423af06f7820",
                "fromAddress": "###RequestorUserEmail###",
                "ccAddress": "###PrimaryBorrowerEmailAddress###",
                "subject": "###BusinessUnitName###",
                "emailBody": "<p>7:30 PM</p>\n"
            }
        }
    ]
}

const tokensMock = [
    {
        "id": "5fc8eb6ae9518e77f013f42c",
        "name": "Branch Nmls No",
        "symbol": "###BranchNmlsNo###",
        "description": "Branch Nmls No",
        "key": "BranchNmlsNo",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f42d",
        "name": "Business Unit Name",
        "symbol": "###BusinessUnitName###",
        "description": "Loan Application Business Unit Name",
        "key": "BusinessUnitName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f42e",
        "name": "Business Unit Phone Number",
        "symbol": "###BusinessUnitPhoneNumber###",
        "description": "Loan Application Business Unit Toll Free Number",
        "key": "BusinessUnitPhoneNumber",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f42f",
        "name": "Business Unit WebSite Url",
        "symbol": "###BusinessUnitWebSiteUrl###",
        "description": "Business Unit Website URL",
        "key": "BusinessUnitWebSiteUrl",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f41c",
        "name": "Co-Borrower Email Address",
        "symbol": "###Co-BorrowerEmailAddress###",
        "description": "Co-Borrower Email Address",
        "key": "Co-BorrowerEmailAddress",
        "fromAddess": false,
        "ccAddess": true,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f41a",
        "name": "Co-Borrower First Name",
        "symbol": "###Co-BorrowerFirstName###",
        "description": "Co-Borrower First Name",
        "key": "Co-BorrowerFirstName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f41b",
        "name": "Co-Borrower Last Name",
        "symbol": "###Co-BorrowerLastName###",
        "description": "Co-Borrower Last Name",
        "key": "Co-BorrowerLastName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f442",
        "name": "Co-Borrower Present City",
        "symbol": "###Co-BorrowerPresentCity###",
        "description": "Co-Borrower Present City",
        "key": "Co-BorrowerPresentCity",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f443",
        "name": "Co-Borrower Present State",
        "symbol": "###Co-BorrowerPresentState###",
        "description": "Co-Borrower Present State",
        "key": "Co-BorrowerPresentState",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f444",
        "name": "Co-Borrower Present State Abbreviation",
        "symbol": "###Co-BorrowerPresentStateAbbreviation###",
        "description": "Co-Borrower Present State Abbreviation",
        "key": "Co-BorrowerPresentStateAbbreviation",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f440",
        "name": "Co-Borrower Present Street Address",
        "symbol": "###Co-BorrowerPresentStreetAddress###",
        "description": "Co-Borrower Present Street Address",
        "key": "Co-BorrowerPresentStreetAddress",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f441",
        "name": "Co-Borrower Present Unit No.",
        "symbol": "###Co-BorrowerPresentUnitNo.###",
        "description": "Co-Borrower Present Unit No.",
        "key": "Co-BorrowerPresentUnitNo.",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f445",
        "name": "Co-Borrower Present Zip Code",
        "symbol": "###Co-BorrowerPresentZipCode###",
        "description": "Co-Borrower Present Zip Code",
        "key": "Co-BorrowerPresentZipCode",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f436",
        "name": "Company NMLS No.",
        "symbol": "###CompanyNMLSNo.###",
        "description": "NMLS number of your company",
        "key": "CompanyNMLSNo.",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f446",
        "name": "Document Upload Button",
        "symbol": "###DocumentUploadButton###",
        "description": "Button directing recepient to document upload module",
        "key": "DocumentUploadButton",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": false,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f448",
        "name": "Documents Page Button",
        "symbol": "###DocumentsPageButton###",
        "description": "Button directing recepient to documents page on loan portal",
        "key": "DocumentsPageButton",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": false,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f41d",
        "name": "EmailTag",
        "symbol": "###EmailTag###",
        "description": "Unique email tag for identifying email sender",
        "key": "EmailTag",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f427",
        "name": "Loan Amount",
        "symbol": "###LoanAmount###",
        "description": "Loan Application Loan Amount",
        "key": "LoanAmount",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f430",
        "name": "Loan Application Login Link",
        "symbol": "###LoanApplicationLoginLink###",
        "description": "Loan Application Login Link",
        "key": "LoanApplicationLoginLink",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f439",
        "name": "Loan Officer Cell Phone Number",
        "symbol": "###LoanOfficerCellPhoneNumber###",
        "description": "Loan Officer Cell Phone Number",
        "key": "LoanOfficerCellPhoneNumber",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f437",
        "name": "Loan Officer Email Address",
        "symbol": "###LoanOfficerEmailAddress###",
        "description": "Loan Officer Email Address",
        "key": "LoanOfficerEmailAddress",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f432",
        "name": "Loan Officer First Name",
        "symbol": "###LoanOfficerFirstName###",
        "description": "Loan Officer First Name",
        "key": "LoanOfficerFirstName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f433",
        "name": "Loan Officer Last Name",
        "symbol": "###LoanOfficerLastName###",
        "description": "Loan Officer Last Name",
        "key": "LoanOfficerLastName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f438",
        "name": "Loan Officer Office Phone Number",
        "symbol": "###LoanOfficerOfficePhoneNumber###",
        "description": "Loan Officer Office Phone Number",
        "key": "LoanOfficerOfficePhoneNumber",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f431",
        "name": "Loan Officer Page Url",
        "symbol": "###LoanOfficerPageUrl###",
        "description": "Web Page of Loan Officer",
        "key": "LoanOfficerPageUrl",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f447",
        "name": "Loan Portal Home Button",
        "symbol": "###LoanPortalHomeButton###",
        "description": "Button directing recepient to loan portal home screen",
        "key": "LoanPortalHomeButton",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": false,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f41e",
        "name": "Loan Portal Url",
        "symbol": "###LoanPortalUrl###",
        "description": "Loan Portal Url",
        "key": "LoanPortalUrl",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f426",
        "name": "Loan Purpose",
        "symbol": "###LoanPurpose###",
        "description": "Loan Purpose",
        "key": "LoanPurpose",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f41f",
        "name": "Loan Status",
        "symbol": "###LoanStatus###",
        "description": "Status of Borrower Loan Application on Colaba",
        "key": "LoanStatus",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f417",
        "name": "Primary Borrower Email Address",
        "symbol": "###PrimaryBorrowerEmailAddress###",
        "description": "Primary Borrower Email Address",
        "key": "PrimaryBorrowerEmailAddress",
        "fromAddess": false,
        "ccAddess": true,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f418",
        "name": "Primary Borrower First Name",
        "symbol": "###PrimaryBorrowerFirstName###",
        "description": "Primary Borrower First Name",
        "key": "PrimaryBorrowerFirstName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f419",
        "name": "Primary Borrower Last Name",
        "symbol": "###PrimaryBorrowerLastName###",
        "description": "Primary Borrower Last Name",
        "key": "PrimaryBorrowerLastName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f43c",
        "name": "Primary Borrower Present City",
        "symbol": "###PrimaryBorrowerPresentCity###",
        "description": "Primary Borrower Present City",
        "key": "PrimaryBorrowerPresentCity",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f43d",
        "name": "Primary Borrower Present State",
        "symbol": "###PrimaryBorrowerPresentState###",
        "description": "Primary Borrower Present State",
        "key": "PrimaryBorrowerPresentState",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f43e",
        "name": "Primary Borrower Present State Abbreviation",
        "symbol": "###PrimaryBorrowerPresentStateAbbreviation###",
        "description": "Primary Borrower Present State Abbreviation",
        "key": "PrimaryBorrowerPresentStateAbbreviation",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f43a",
        "name": "Primary Borrower Present Street Address",
        "symbol": "###PrimaryBorrowerPresentStreetAddress###",
        "description": "Primary Borrower Present Street Address",
        "key": "PrimaryBorrowerPresentStreetAddress",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f43b",
        "name": "Primary Borrower Present Unit No.",
        "symbol": "###PrimaryBorrowerPresentUnitNo.###",
        "description": "Primary Borrower Present Unit No.",
        "key": "PrimaryBorrowerPresentUnitNo.",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fd22040e9518e77f013f43f",
        "name": "Primary Borrower Present Zip Code",
        "symbol": "###PrimaryBorrowerPresentZipCode###",
        "description": "Primary Borrower Present Zip Code",
        "key": "PrimaryBorrowerPresentZipCode",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f429",
        "name": "Property Type",
        "symbol": "###PropertyType###",
        "description": "Property Type",
        "key": "PropertyType",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f42a",
        "name": "Property Usage",
        "symbol": "###PropertyUsage###",
        "description": "Property Usage",
        "key": "PropertyUsage",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f428",
        "name": "Property Value",
        "symbol": "###PropertyValue###",
        "description": "Property Value",
        "key": "PropertyValue",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f434",
        "name": "Request Document List",
        "symbol": "###RequestDocumentList###",
        "description": "Bullet Point list of Documents being requested",
        "key": "RequestDocumentList",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": false,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f435",
        "name": "Requestor User Email",
        "symbol": "###RequestorUserEmail###",
        "description": "Email Address of the Needs List requestor",
        "key": "RequestorUserEmail",
        "fromAddess": true,
        "ccAddess": true,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f420",
        "name": "Subject Property Address",
        "symbol": "###SubjectPropertyAddress###",
        "description": "Subject Property Address",
        "key": "SubjectPropertyAddress",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f424",
        "name": "Subject Property City",
        "symbol": "###SubjectPropertyCity###",
        "description": "Subject Property City",
        "key": "SubjectPropertyCity",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f423",
        "name": "Subject Property County",
        "symbol": "###SubjectPropertyCounty###",
        "description": "Subject Property County",
        "key": "SubjectPropertyCounty",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f421",
        "name": "Subject Property State",
        "symbol": "###SubjectPropertyState###",
        "description": "Subject Property State",
        "key": "SubjectPropertyState",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f422",
        "name": "Subject Property State Abbreviation",
        "symbol": "###SubjectPropertyStateAbbreviation###",
        "description": "Subject Property State Abbreviation",
        "key": "SubjectPropertyStateAbbreviation",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    },
    {
        "id": "5fc8eb6ae9518e77f013f425",
        "name": "Subject Property ZipCode",
        "symbol": "###SubjectPropertyZipCode###",
        "description": "Subject Property Zip Code",
        "key": "SubjectPropertyZipCode",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true,
        "defaultValue": ""
    }
]
export class ReminderEmailListActions {
    
    static async fetchReminderEmails() {
        try {
                              
                let mappedData = mockData.emailReminders.map((ReminderEmail: ReminderSettingTemplate) => {
                    return new ReminderSettingTemplate(ReminderEmail.id,ReminderEmail.noOfDays, ReminderEmail.recurringTime, ReminderEmail.isActive,
                       new ReminderEmailTemplate(ReminderEmail.email?.id, ReminderEmail.email?.fromAddress,ReminderEmail.email?.ccAddress,ReminderEmail.email?.subject, ReminderEmail.email?.emailBody)
                       // ReminderEmail.email
                        );
                });
                let finalResult = {"isActive": mockData.isActive, "emailReminders": mappedData};
              return Promise.resolve(finalResult);

        } catch (error) {
            console.log('error',error)
        }   
   
    }


    static async updateEnableDisableAllEmails(isActive: boolean){
    return Promise.resolve(200);
    }

    static async fetchTokens() {
        let mappedData = tokensMock.map((tokenMock: any) => {
            return new Tokens(tokenMock.id, tokenMock.name, tokenMock.symbol, tokenMock.description, tokenMock.key, tokenMock.fromAddess, tokenMock.ccAddess, tokenMock.emailBody, tokenMock.emailSubject);
        });
        
       return Promise.resolve(mappedData);
    }
}
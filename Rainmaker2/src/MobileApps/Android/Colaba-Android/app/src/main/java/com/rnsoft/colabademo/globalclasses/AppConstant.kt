package com.rnsoft.colabademo

import org.json.JSONObject




object AppConstant {

         var userTwoFaSetting:Boolean?= null

        const val APP_PREFERENCES: String = "COLABA_PREFERENCES"

        //const val BASE_URL = "https://devmobilegateway.rainsoftfn.com:5002/"

        //const val BASE_URL = "https://qamobilegateway.rainsoftfn.com/"

        //const val BASE_URL = "https://devmobilegateway.rainsoftfn.com:5002/api/mobile/identity/"
        //const val BASE_URL = "https://72.255.61.15:5002/api/mobile/identity/";

        //const val  LOGIN_NAME:String = "khatri03.mcu@mailinator.com"
        //const val  PASSWORD:String = "test123"


        const val relaceDocFormat : String = "###RequestDocumentList###"


        const val INVITATION_STATUS_INVITE: String = "Invite Borrower"
        const val INVITATION_STATUS_PENDING: String = "Invitation Pending"
        const val INVITATION_STATUS_RESENT: String = "Invitation Resent"
        const val INVITATION_STATUS_ACCEPTED: String = "Invitation Accepted"
        const val INVITATION_TYPE: String = "type"


        const val MARITAL_STATUS_MARRIED: Int = 1
        const val MARITAL_STATUS_SEPARATED: Int = 2
        const val MARITAL_STATUS_UNMARRIED: Int = 9
        const val PRIMARY_BORROWER_ID: Int = 1
        const val SECONDARY_BORROWER_ID: Int = 2

        const val real_estate_delete_text: String = "Are you sure you want to delete this property?"
        const val income_borrowerId: String = "income-borrower-id"
        const val income_update: String = "income-update"
        const val income_self_employment: String = "Self Employment / Independent Contractor"
        const val income_employment: String = "Employment"
        const val income_business: String = "Business"
        const val income_military: String = "Military Pay"
        const val income_retirement: String = "Retirement"
        const val income_other: String = "Other"

        const val previous_address: String = "prev_address"
        const val add_previous_address: String = "add_prev_address"
        const val delete_previous_address: String = "delete_prev_address"
        const val delete_mailing_address: String = "delete_mailing_address"
        const val owntypeid: String = "owntype_id"
        const val borrowerTypeId: Int = 1 // owntype id

        const val service_date: String = "service_date"
        const val borrower_citizenship: String = "borrower_citizenship"
        const val visa_status_other : String = "Other"
        const val visa_status_temp_worker : String = "I am a temporary worker (H-2A, etc.)"
        const val visa_status_work_visa : String = "I hold a valid work visa (H1, L1, etc.)"

        const val married: String = "Married"
        const val separated: String = "Separated"
        const val marriage_type: String = "Marriage_type"

        const val RESPONSE_CODE_SUCCESS = "200"
        const val IS_LOGGED_IN:String ="IS_LOGGED_IN"
        const val RACE_DETAILS : String = "race_details"
        const val RACE_BASE_LIST : String = "race_base_list"
        const val ETHNICITY_DETAILS : String = "ethnicity_details"
        const val GENDER_DETAILS : String = "gender_details"
        const val token:String = "token"
        const val refreshToken:String = "refreshToken"
        const val userProfileId:String = "userProfileId"
        const val userName:String = "userName"
        const val firstName:String = "firstName"
        const val lastName:String = "lastName"
        const val middleName:String = "middleName"
        const val validFrom:String = "validFrom"
        const val validTo:String = "validTo"
        const val refreshTokenValidTo:String = "refreshTokenValidTo"
        const val tokenType:String = "tokenType"
        const val tokenTypeName:String = "tokenTypeName"

        const val tenantTwoFaSetting:String = "tenantTwoFaSetting"
        const val tabBorrowerId:String = "tabBorrowerId"

        const val assetRealStateId = 14
        const val assetNonRealStateId = 13

        const val otpDataJson:String =  "otpDataJson"
        const val phoneNumber:String = "phoneNumber"

        const val secondsCount:String = "secondsCount"
        const val showData: String = "showData"

        const val borrowerParcelObject:String = "borrowerParcelObject"

        const val loanApplicationId:String = "loanApplicationId"
        const val fullName : String = "fullName"

        const val loanPurpose:String = "loanPurpose"
        const val loanPurposeNumber:String = "loanPurposeNumber"
        const val PURPOSE_ID_PURCHASE:Int = 1
        const val PURPOSE_ID_REFINANCE:Int = 2


        const val borrowerId:String = "borrowerId"
        const val borrowerName:String = "borrowerName"
        const val propertyInfoId:String = "propertyInfoId"
        const val addBorrower:String ="AddBorrower"
        const val coborrowers:String ="CoBorrower"


        const val assetUniqueId:String = "borrowerAssetId"

        const val assetCategoryId:String = "assetCategoryId"

        const val assetBorrowerName:String = "assetBorrowerName"


        const val assetCategoryName:String = "assetCategoryName"

        const val listenerAttached:String = "listenerAttached"

        const val assetTypeID:String = "assetTypeID"

        const val incomeId:String = "incomeAssetId"
        const val incomeCategoryId:String = "incomeCategoryId"
        const val incomeTypeID:String = "incomeTypeID"

        const val borrowerPurpose:String = "purpose"

        const val borrowerPropertyId:String = "property_id"

        const val purchase:String = "Purchase"

        const val refinance:String = "Refinance"

        const val subjectProperty:String = "subjectPropertyPurpose"

        const val maxOtpSendAllowed:String = "maxOtpSendAllowed"

        const val twoFaResendCoolTimeInMinutes:String = "twoFaResendCoolTimeInMinutes"

        const val otp_message:String =   "otp_message"

        const val isbiometricEnabled:String =   "isbiometricEnabled"

        const val AccessToken:String =   "AccessToken"

        const val IntermediateToken:String =   "IntermediateToken"

        const val dontAskTwoFaIdentifier:String = "dontAskTwoFaIdentifier"

        const val  NO_CONVERSATION:String = "NO_CONVERSATION.."
        const val  INTERNET_ERR_MSG:String = "Please check internet connectivity.."
        const val  WEB_SERVICE_ERR_MSG:String = "Webservice not responding..."
        const val  NO_RECORDS_FOUND:String = "No records found..."
        const val  INTERNET_ERR_CODE:String = "6048"
        const val  ASSIGN_TO_ME:String = "ASSIGN_TO_ME"

        const val  innerFilesName:String = "innerFilesName"
        const val  docObject:String = "docObject"
        const val  docName:String = "docName"
        const val  docMessage:String = "docMessage"

        const val oldLoans:String = "oldLoans"
        const val oldActiveLoans:String = "oldActiveLoans"
        const val oldNonActiveLoans:String = "oldNonActiveLoans"

        const val download_id:String = "download_id"
        const val download_requestId:String = "download_requestId"
        const val download_docId:String = "download_docId"

        // doc filters/status
        const val filter_all:String = "All"
        const val filter_inDraft:String = "In Draft"
        const val filter_borrower_todo:String = "Borrower to do"
        const val filter_started:String = "Started"
        const val filter_pending:String = "Pending"
        const val filter_pending_review:String = "Pending review"
        const val filter_completed:String = "Completed"
        const val filter_manuallyAdded:String = "Manually Added"

        // file formats
        const val file_format_pdf:String = "pdf"
        const val file_format_png:String = "png"
        const val file_format_jpg:String = "jpg"
        const val file_format_jpeg:String = "jpeg"

        const val bPhoneNumber:String = "bPhoneNumber"

        const val bEmail:String = "bEmail"
        const val isNewApplicationCreated : String = "true"

        const val innerScreenName:String = "innerScreenName"

        const val borrowerAppScreen:String = "borrowerAppScreen"

        const val borrowerDocScreen:String = "borrowerDocScreen"

        const val addUpdateQuestionsParams : String = "addUpdateQuestionsParams"

        const val govtUserName : String = "govtUserName"
        const val questionno : String = "questionno"


        const val questionId : String = "questionId"
        const val whichBorrowerId : String = "whichBorrowerId"

        const val childGlobalList : String = "childGlobalList"
        const val ownerShipGlobalData : String = "ownerShipGlobalData"
        const val bankruptcyAnswerData : String = "bankruptcyGlobalData"


        const val  downloadedFileName:String = "pdfFileName"
        const val address: String = "address"
        const val current_address: String = "address"
        const val mailing_address: String = "mailing_address"
        const val marital_status: String = "marital_status"

        const val mixedPropertyDetails: String = "mixed_property_details"
        const val application_tab_updated: String = "tab_data_updated"
        const val income_delete_text: String = "Are you sure you want to remove this income source?"
        const val MIXED_USE_PROPERTY_DESC: String = "mixed_property_explaination"
        const val TOOLBAR_TITLE: String = "toolbar_title"
        const val RESERVE_ACTIVATED: String = "reserve_activated"
        const val secMortgage : String = "sec_mortgage"
        const val firstMortgage : String = "first_mortgage"
        const val docTypeObject : String = "docTypeObject"
        const val docTypeName : String = "toolbar_heading"
        const val docTypeDetail : String = "variable_message"

        const val search_word : String = "search_word"

        const val  assetBorrowerList:String = "assetBorrowerList"
        const val  incomeBorrowerList:String = "incomeBorrowerList"
        const val footerAddEmployment = "Add Employment"


        const val  selectedQuestionHeader:String = "questionSelected"
        const val  borrowerList:String = "BorrowerList"
        const val  borrowerOwnTypeList:String = "BorrowerOwnTypeList"

        const val  ownershipConstantValue = "Ownership Interest in Property"
        const val  childConstantValue = "Child Support, Alimony, etc."
        const val  UndisclosedBorrowerFunds = "Undisclosed Borrowered Funds"
        const val  Bankruptcy = "Bankruptcy"
        const val demographicInformation = "Demographic Information"

        const val INCOME_BONUS = "Bonus"
        const val INCOME_OVERTIME ="Overtime"
        const val INCOME_COMMISSION = "Commission"


        const val assetReturnParams = "assetReturnParams"
        const val assetDeleted = "assetDeleted"
        const val assetUpdated = "assetUpdated"
        const val assetAdded = "assetAdded"

        const val selectedAsianChildList = "selectedAsianChildList"
        const val asianChildList = "AsianChildList"

        const val nativeHawaianChildList = "NativeHawaianChildList"
        const val selectedNativeHawaianChildList = "selectedNativeHawaianChildList"

        const val ethnicityChildList = "EthnicityChildList"
        const val selectedEthnicityChildList = "selectedEthnicityChildList"


        var jsonObj = JSONObject("{\n" +
                "  \"LoanApplicationId\": \"5\",\n" +
                "  \"BorrowerId\": 5,\n" +
                "  \"Questions\": [\n" +
                "        {\n" +
                "            \"id\": 10,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Undisclosed Borrowered Funds\",\n" +
                "            \"questionSectionId\": 1,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Are you borrowing any money for this real estate transaction (e.g., money for your closing costs or down payment) obtaining any money from another party, such as the seller or realtor, that you have not disclosed on this loan application?\",\n" +
                "            \"answer\": \"Yes\",\n" +
                "            \"answerDetail\": \"test\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": null\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 11,\n" +
                "            \"parentQuestionId\": 10,\n" +
                "            \"headerText\": \"Amount to Borrow?\",\n" +
                "            \"questionSectionId\": 1,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"What is the amount of money you’ve borrowed or intend to borrow?\",\n" +
                "            \"answer\": \"5435\",\n" +
                "            \"answerDetail\": \"fsdf\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": null\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 20,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Ownership Interest in Property\",\n" +
                "            \"questionSectionId\": 1,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Have you had an ownership interest in another property in the last three years?\",\n" +
                "            \"answer\": \"Yes\",\n" +
                "            \"answerDetail\": null,\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": null\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 21,\n" +
                "            \"parentQuestionId\": 20,\n" +
                "            \"headerText\": \"Own Property Type\",\n" +
                "            \"questionSectionId\": 1,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"What type of property did you own?\",\n" +
                "            \"answer\": \"Yes\",\n" +
                "            \"answerDetail\": null,\n" +
                "            \"selectionOptionId\": 3,\n" +
                "            \"answerData\": {\n" +
                "                \"selectionOptionId\": 1,\n" +
                "                \"selectionOptionText\": \"Second Home\"\n" +
                "            }\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 22,\n" +
                "            \"parentQuestionId\": 20,\n" +
                "            \"headerText\": \"Property Title\",\n" +
                "            \"questionSectionId\": 1,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"How did you hold title to the property?\",\n" +
                "            \"answer\": \"Yes\",\n" +
                "            \"answerDetail\": null,\n" +
                "            \"selectionOptionId\": 2,\n" +
                "            \"answerData\": {\n" +
                "                \"selectionOptionId\": 1,\n" +
                "                \"selectionOptionText\": \"Jointly with your spouse\"\n" +
                "            }\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 45,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Family or Business affiliation\",\n" +
                "            \"questionSectionId\": 1,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Do you have a family or business affliation with the seller of the property?\",\n" +
                "            \"answer\": \"No\",\n" +
                "            \"answerDetail\": \"\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": {\n" +
                "                \"IsAffiliatedWithSeller\": false,\n" +
                "                \"AffiliationDescription\": null\n" +
                "            }\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 60,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Debt Co-Signer or Guarantor\",\n" +
                "            \"questionSectionId\": 2,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Are you a co-signer or guarantor on any debt or loan that is not disclosed on this application?\",\n" +
                "            \"answer\": \"No\",\n" +
                "            \"answerDetail\": \"\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": null\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 70,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Outstanding Judgements\",\n" +
                "            \"questionSectionId\": 2,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Are there any outstanding judgments against you?\",\n" +
                "            \"answer\": \"No\",\n" +
                "            \"answerDetail\": \"\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": null\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 80,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Federal Debt Deliquency\",\n" +
                "            \"questionSectionId\": 2,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Are you currently delinquent or in default on a Federal debt?\",\n" +
                "            \"answer\": \"Yes\",\n" +
                "            \"answerDetail\": \"asd\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": null\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 90,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Party to Lawsuit\",\n" +
                "            \"questionSectionId\": 2,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Are you a party to a lawsuit in which you potentially have any personal financial liability?\",\n" +
                "            \"answer\": \"Yes\",\n" +
                "            \"answerDetail\": \"asd\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": null\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 100,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Title Conveyance\",\n" +
                "            \"questionSectionId\": 2,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Have you conveyed title to any property in lieu of foreclosure in the past 7 years?\",\n" +
                "            \"answer\": \"Yes\",\n" +
                "            \"answerDetail\": \"asd\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": null\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 110,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Pre-Foreclosureor Short Sale\",\n" +
                "            \"questionSectionId\": 3,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Within the past 7 years, have you completed a pre-foreclosure sale or short sale, whereby the property was sold to a third party and the Lender agreed to accept less than the outstanding mortgage balance due?\",\n" +
                "            \"answer\": \"Yes\",\n" +
                "            \"answerDetail\": \"\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": null\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 120,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Foreclosured Property\",\n" +
                "            \"questionSectionId\": 3,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Have you had property foreclosed upon in the last 7 years?\",\n" +
                "            \"answer\": \"Yes\",\n" +
                "            \"answerDetail\": \"asd 1\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": null\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 130,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Bankruptcy \",\n" +
                "            \"questionSectionId\": 3,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Have you declared bankruptcy within the past 7 years?\",\n" +
                "            \"answer\": \"Yes\",\n" +
                "            \"answerDetail\": \"dfg\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": null\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 131,\n" +
                "            \"parentQuestionId\": 130,\n" +
                "            \"headerText\": \"Type\",\n" +
                "            \"questionSectionId\": 3,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Which Type?\",\n" +
                "            \"answer\": null,\n" +
                "            \"answerDetail\": null,\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": [\n" +
                "                {\n" +
                "                    \"2\": \"Chapter 11\"\n" +
                "                }\n" +
                "            ]\n" +
                "        },\n" +
                "        {\n" +
                "            \"id\": 140,\n" +
                "            \"parentQuestionId\": null,\n" +
                "            \"headerText\": \"Child Support, Alimony, etc.\",\n" +
                "            \"questionSectionId\": 3,\n" +
                "            \"ownTypeId\": 1,\n" +
                "            \"firstName\": \"Quentin\",\n" +
                "            \"lastName\": \"Finley\",\n" +
                "            \"question\": \"Are you currently obligated to pay child support, alimony or separate maintenance?\",\n" +
                "            \"answer\": \"Yes\",\n" +
                "            \"answerDetail\": \"\",\n" +
                "            \"selectionOptionId\": null,\n" +
                "            \"answerData\": [\n" +
                "                {\n" +
                "                    \"liabilityTypeId\": 1,\n" +
                "                    \"liabilityName\": \"Child Support\",\n" +
                "                    \"remainingMonth\": 2,\n" +
                "                    \"monthlyPayment\": 3535,\n" +
                "                    \"name\": \"gdfgd\"\n" +
                "                },\n" +
                "                {\n" +
                "                    \"liabilityTypeId\": 2,\n" +
                "                    \"liabilityName\": \"Separate Maintenance\",\n" +
                "                    \"remainingMonth\": 3,\n" +
                "                    \"monthlyPayment\": 1111,\n" +
                "                    \"name\": \"111\"\n" +
                "                }\n" +
                "            ]\n" +
                "        }\n" +
                "    ]\n" +
                "}")


        //const val fakeUserToken:String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIzODA2NCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtb2JpbGV1c2VyMUBtYWlsaW5hdG9yLmNvbSIsIkZpcnN0TmFtZSI6Ik1vYmlsZSIsIkxhc3ROYW1lIjoiVXNlcjEiLCJUZW5hbnRDb2RlIjoibGVuZG92YSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJNQ1UiLCJMb2FuIE9mZmljZXIiXSwiZXhwIjoxNjI1Njg4MjAzLCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.YDTSZPAATl1URzpMugemrvuMH4bXKQDRnJyEhzjRaaY"
        //const val fakeMubashirToken:String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0MTA4NCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtdWJhc2hpci5tY3VAbWFpbGluYXRvci5jb20iLCJGaXJzdE5hbWUiOiJBbGl5YSIsIkxhc3ROYW1lIjoiUHJhc2xhIiwiVGVuYW50Q29kZSI6ImxlbmRvdmEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiTUNVIiwiTG9hbiBPZmZpY2VyIl0sImV4cCI6MTYyNTg5MDkyMCwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.3lthGSEXeLRUiJ_mZt2m1Vv10TNfHcSr6l9RDjOnJsM"
}


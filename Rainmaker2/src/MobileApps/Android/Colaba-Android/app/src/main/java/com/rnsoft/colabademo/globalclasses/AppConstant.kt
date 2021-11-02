package com.rnsoft.colabademo

object AppConstant {

         var userTwoFaSetting:Boolean?= null

        const val APP_PREFERENCES: String = "COLABA_PREFERENCES"

        //const val BASE_URL = "https://devmobilegateway.rainsoftfn.com:5002/"

        //const val BASE_URL = "https://qamobilegateway.rainsoftfn.com/"

        //const val BASE_URL = "https://devmobilegateway.rainsoftfn.com:5002/api/mobile/identity/"
        //const val BASE_URL = "https://72.255.61.15:5002/api/mobile/identity/";

        //const val  LOGIN_NAME:String = "khatri03.mcu@mailinator.com"
        //const val  PASSWORD:String = "test123"
        const val RESPONSE_CODE_SUCCESS = "200"
        const val IS_LOGGED_IN:String ="IS_LOGGED_IN"
        const val RACE_DETAILS : String = "race_details"
        const val RACE_BASE_LIST : String = "race_base_list"
        const val ETHNICITY_DETAILS : String = "ethnicity_details"
        const val GENDER_DETAILS : String = "gender_details"
        const val token:String = "token"
        val authToken : String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNhZGlxQHJhaW5zb2Z0Zm4uY29tIiwiRmlyc3ROYW1lIjoiU2FkaXEiLCJMYXN0TmFtZSI6Ik1hY2tub2ppYSIsIlRlbmFudENvZGUiOiJhaGNsZW5kaW5nIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTUNVIiwiZXhwIjoxNjM1NDUwNjg5LCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.LUDDCZxvAxbfKQIom2RZzIS72bEK22XAp2wtxwZkzx4"
        const val refreshToken:String = "refreshToken"
        const val userProfileId:String = "userProfileId"
        const val userName:String = "userName"
        const val firstName:String = "firstName"
        const val lastName:String = "lastName"
        const val validFrom:String = "validFrom"
        const val validTo:String = "validTo"
        const val refreshTokenValidTo:String = "refreshTokenValidTo"
        const val tokenType:String = "tokenType"
        const val tokenTypeName:String = "tokenTypeName"

        const val tenantTwoFaSetting:String = "tenantTwoFaSetting"
        const val tabBorrowerId:String = "tabBorrowerId"

        const val otpDataJson:String =  "otpDataJson"
        const val phoneNumber:String = "phoneNumber"

        const val secondsCount:String = "secondsCount"

        const val borrowerParcelObject:String = "borrowerParcelObject"

        const val loanApplicationId:String = "loanApplicationId"

        const val loanPurpose:String = "loanPurpose"

        const val borrowerId:String = "borrowerId"

        const val borrowerAssetId:String = "borrowerAssetId"

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

        const val innerScreenName:String = "innerScreenName"

        const val borrowerAppScreen:String = "borrowerAppScreen"

        const val borrowerDocScreen:String = "borrowerDocScreen"


        const val  downloadedFileName:String = "pdfFileName"
        const val address:String = "address"
        const val secMortgage : String = "sec_mortgage"
        const val firstMortgage : String = "first_mortgage"
        const val heading : String = "toolbar_heading"

        const val search_word : String = "search_word"

        const val  assetBorrowerList:String = "assetBorrowerList"
        const val  incomeBorrowerList:String = "incomeBorrowerList"
        const val footerAddEmployment = "Add Employment"

        const val  borrowerList:String = "BorrowerList"
        const val  borrowerOwnTypeList:String = "BorrowerOwnTypeList"

        const val  ownershipConstantValue = "Ownership Interest in Property"
        const val  childConstantValue = "Child Support, Alimony, etc."
        const val  Bankruptcy = "Bankruptcy"
        const val demographicInformation = "Demographic Information"


        const val asianChildList = "AsianChildList"
        const val nativeHawaianChildList = "NativeHawaianChildList"
        const val ethnicityChildList = "EthnicityChildList"

        //const val fakeUserToken:String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIzODA2NCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtb2JpbGV1c2VyMUBtYWlsaW5hdG9yLmNvbSIsIkZpcnN0TmFtZSI6Ik1vYmlsZSIsIkxhc3ROYW1lIjoiVXNlcjEiLCJUZW5hbnRDb2RlIjoibGVuZG92YSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJNQ1UiLCJMb2FuIE9mZmljZXIiXSwiZXhwIjoxNjI1Njg4MjAzLCJpc3MiOiJyYWluc29mdGZuIiwiYXVkIjoicmVhZGVycyJ9.YDTSZPAATl1URzpMugemrvuMH4bXKQDRnJyEhzjRaaY"
        //const val fakeMubashirToken:String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0MTA4NCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJtdWJhc2hpci5tY3VAbWFpbGluYXRvci5jb20iLCJGaXJzdE5hbWUiOiJBbGl5YSIsIkxhc3ROYW1lIjoiUHJhc2xhIiwiVGVuYW50Q29kZSI6ImxlbmRvdmEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiTUNVIiwiTG9hbiBPZmZpY2VyIl0sImV4cCI6MTYyNTg5MDkyMCwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.3lthGSEXeLRUiJ_mZt2m1Vv10TNfHcSr6l9RDjOnJsM"
}


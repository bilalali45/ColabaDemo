import { APIResponse } from "../../../Entities/Models/APIResponse";

const mockDataLogin = {
  "status": "Success",
  "data": {
      "tenant2FaStatus": 3,
      "userPreference": true,
      "isLoggedIn": false,
      "phoneNoMissing": true,
      "requiresTwoFa": true,
      "phoneNo": null,
      "verificationSid": null,
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsIlVzZXJQcm9maWxlSWQiOiI5IiwiQ29udGFjdElkIjoiMTIiLCJFbWFpbCI6ImplaGFuZ2lyQGdtYWlsLmNvbSIsImV4cCI6MTYxNTg5NDE2NiwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.8R4rooCx9FTEvHmev0DywsLh2Mc8JkSJX7Um0rE_cI4",
      "userProfileId": 9,
      "userName": "jehangir@gmail.com",
      "validFrom": "0001-01-01T00:00:00",
      "validTo": "2021-03-16T11:29:26Z",
      "verify_attempts_count": 0,
      "otp_valid_till": "0001-01-01T00:10:00",
      "next2FaInSeconds": 15,
      "twoFaRecycleMinutes": 10,
      "sendAttemptsCount": []
  },
  "message": null,
  "code": "200"
}

const mockDataSend2Fa = {
  "status": "pending",
  "data": {
      "twoFaResponse": {
          "status": "pending",
          "date_updated": "2021-03-16T15:41:35Z",
          "send_code_attempts": [
              {
                  "attempt_sid": "VLf6f2ed57aee1d4de78d2acfaf694fd76",
                  "channel": "sms",
                  "time": "2021-03-16T15:41:35Z"
              }
          ],
          "verify_attempts_count": 0,
          "to": "+12254165226",
          "valid": false,
          "lookup": {
              "carrier": {
                  "mobile_country_code": "311",
                  "type": "voip",
                  "error_code": null,
                  "mobile_network_code": "950",
                  "name": "Twilio - Inteliquent - SMS-Sybase365/MMS-SVR"
              }
          },
          "sid": "VE44dca1d06b194ecc3e8b1fe657170a8c",
          "date_created": "2021-03-16T15:41:35Z",
          "otp_valid_till": "2021-03-16T15:51:35Z",
          "twoFaRecycleSeconds": 598,
          "statusCode": 0,
          "canSend2Fa": true,
          "next2FaInSeconds": 15.0,
          "twoFaRecycleMinutes": 10
      },
      "cookiePath": "/lendova/"
  },
  "message": null,
  "code": null
}

const mockDataVerify2Fa = {
  "status": "BadRequest",
  "data": {
      "status": "max_attempts_reached",
      "date_updated": "2021-03-15T07:29:44Z",
      "send_code_attempts": [
          {
              "attempt_sid": "VL92876fb75acece6346b8ee7a430e63ee",
              "channel": "sms",
              "time": "2021-03-15T07:27:15Z"
          }
      ],
      "verify_attempts_count": 6,
      "to": "+12254165226",
      "valid": false,
      "lookup": {
          "carrier": {
              "mobile_country_code": "311",
              "type": "voip",
              "error_code": null,
              "mobile_network_code": "950",
              "name": "Twilio - Inteliquent - SMS-Sybase365/MMS-SVR"
          }
      },
      "sid": "VE8320cfa3cf44da31e0932fa2c8c510b5",
      "date_created": "2021-03-15T07:27:15Z",
      "otp_valid_till": "2021-03-15T07:37:15Z",
      "twoFaRecycleSeconds": null,
      "statusCode": 429,
      "twoFaSent": false,
      "next2FaInSeconds": 0.0,
      "twoFaRecycleMinutes": 0
  },
  "message": null,
  "code": "400"
}

const mockDataSkip2Fa = {
  "status": "Success",
  "data": {
      "isLoggedIn": true,
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsIlVzZXJJZCI6IjkiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiamVoYW5naXJAZ21haWwuY29tIiwiRmlyc3ROYW1lIjoiSmVoYW5naXIiLCJMYXN0TmFtZSI6IkJhYnVsIiwiVGVuYW50Q29kZSI6ImxlbmRvdmEiLCJCcmFuY2hDb2RlIjoibGVuZG92YSIsImV4cCI6MTYxNjE5MzA1NCwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.V8hr7e9ZIt6x1kcelbS3SXCMNNpOOK6kO4v6DAqpz-E",
      "refreshToken": "hOcnyqhQfUD+emERHDW3Fm8u5NOIeLvIcBlFKAyjeK0=",
      "validFrom": "0001-01-01T00:00:00",
      "validTo": "2021-03-19T22:30:54Z",
      "cookiePath": "/lendova/"
  },
  "message": null,
  "code": "200"
}

const mockDataTerms = "<p>By entering my phone number and clicking the “Agree and Continue” button above, I agree to all the following:</p><br/><p>Receive SMS messages and/or phone calls sent by this website using an automatic telephone dialing system or the use of a prerecorded voice to\
the telephone numbers you designate for the purpose of sending you security codes.</p>";

export class UserActions {
 static async insertContactEmail(){

  }

  static async forgotPassword() {
   return new APIResponse(200, "success");

  }

  static async forgotPasswordResponse() {
    return new APIResponse(200, "success");
  }
  
  static async changePassword() {
    return true
  }

  static async signIn() {
    return Promise.resolve(mockDataLogin);
  }

  static async send2FaRequest() {
    return Promise.resolve(mockDataSend2Fa);
  }

  static async verify2FaSignIn() {
    return Promise.resolve(mockDataVerify2Fa);
  }

  static async skip2FaRequest() {
    return Promise.resolve(mockDataSkip2Fa);
  }

  static async getTermConditionAndAgreement(){
    return Promise.resolve(mockDataTerms);
  }

 
}
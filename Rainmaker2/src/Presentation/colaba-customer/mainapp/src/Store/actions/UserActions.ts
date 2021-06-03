import { Endpoints } from "../endpoints/Endpoints";
import { Http } from "rainsoft-js";
import { AxiosResponse } from 'axios';
import { forgotPasswordResponse, getSessionValidityForResetPassword, insertContactEmailLog, insertContactEmailPhoneLog, LoginResponseType, register, send2FaRequest, signIn, verify2Fa } from "../../lib/types";
import { APIResponse } from "../../Entities/Models/APIResponse";


export class UserActions {

  static async register({ firstName, lastName, email, phone, password, DontAsk2Fa, MapPhoneNumber, RequestSid, Skipped2Fa }: register, captchaCode: string) {
    const url = Endpoints.User.POST.register()

    try {
      const res = await Http.post(url, {
        firstName,
        lastName,
        email,
        phone,
        password,
        DontAsk2Fa,
        MapPhoneNumber,
        RequestSid,
        Skipped2Fa
      }, {
        'RecaptchaCode': captchaCode
      })

      return res.data;
    } catch (error) {
      throw error
    }
  }

  static async insertContactEmailLog({ firstName, lastName, email }:insertContactEmailLog, captchaCode: string) {
    const url = Endpoints.User.POST.insertContactEmailLog();

    try {
      const res = await Http.post(url, {
        firstName,
        lastName,
        email
      }, {
        'RecaptchaCode': captchaCode
      }, true)

      return res.data;
    } catch (error) {
      throw error
    }
  }

  static async insertContactEmailPhoneLog({ firstName, lastName, email, phone }: insertContactEmailPhoneLog, captchaCode: string) {
    const url = Endpoints.User.POST.insertContactEmailPhoneLog();

    try {
      const res = await Http.post(url, {
        firstName,
        lastName,
        email,
        phone
      }, {
        'RecaptchaCode': captchaCode
      },true)

      return res.data;
    } catch (error) {
      throw error
    }
  }

  static async doesCustomerAccountExist(email: string, captchaCode: string) {
    try {
      const res = await Http.get(Endpoints.User.GET.doesCustomerAccountExist(email), {
        'RecaptchaCode': captchaCode
      }, true)

      return res.data;
    } catch (error) {
      throw error
    }
  }

  static async signIn({ email, password }:signIn, captchaCode: string) {
    let url = Endpoints.User.POST.signIn();
    try {
      let response: AxiosResponse<LoginResponseType> = await Http.post(url, {
        email: email,
        password: password
      }, {
        'RecaptchaCode': captchaCode
      }, 
      true,
      true
      );
      return response.data;
    } catch (error) {
      console.log(error);
      return error.response.data;
    }
  }

  static async skip2FaRequest(skip2Fa: boolean, captchaCode: string) {
    let url = Endpoints.User.POST.skip2FaRequest();
    try {
      let response: AxiosResponse = await Http.post(url, {
        skip2Fa: skip2Fa
      },
        {
          'RecaptchaCode': captchaCode
        });

      return response.data;
    } catch (error) {
      console.log(error.response.data);
      return error.response.data;
    }
  }

  static async dontAsk2Fa(dntAsk2fa: boolean , captchaCode: string){
    let url = Endpoints.User.POST.dontAsk2FA();
    try {
      let res: AxiosResponse = await Http.post(url,{
        dntAsk2fa: dntAsk2fa
      },
        {
          'RecaptchaCode': captchaCode
        },
        false,
        true
        );
      return res;
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async verify2FaSignUp({ otpCode, verificationSid, email, phoneNumber, dontAsk2Fa, mapPhoneNumber }:verify2Fa, captchaCode: string) {
    let url = Endpoints.User.POST.verify2FaSignUp();
    try {
      let response: AxiosResponse = await Http.post(url, {
        Code: otpCode,
        RequestSid: verificationSid,
        Email: email,
        PhoneNumber: phoneNumber,
        DontAsk2Fa: dontAsk2Fa,
        MapPhoneNumber: mapPhoneNumber,
      }, {
        'RecaptchaCode': captchaCode,
      }, true);

      return response.data;
    } catch (error) {
      console.log(error.response.data);
      return error.response.data;
    }
  }

  static async verify2FaSignIn({ otpCode, verificationSid, email, phoneNumber, dontAsk2Fa, mapPhoneNumber }:verify2Fa, captchaCode: string) {
    let url = Endpoints.User.POST.verify2FaSignIn();
    try {
      let response: AxiosResponse = await Http.post(url, {
        Code: otpCode,
        RequestSid: verificationSid,
        Email: email,
        PhoneNumber: phoneNumber,
        DontAsk2Fa: dontAsk2Fa,
        MapPhoneNumber: mapPhoneNumber,
      }, {
        'RecaptchaCode': captchaCode
      });

      return response.data;
    } catch (error) {
      console.log(error.response.data);
      return error.response.data;
    }
  }

  static async send2FaRequest({ phoneNumber, email, verificationSid }:send2FaRequest, captchaCode: string) {
    let url = Endpoints.User.POST.send2FaRequest();
    try {
      let response: AxiosResponse = await Http.post(url, {
        PhoneNumber: '+1' + phoneNumber,
        Email: email,
        VerificationSid: verificationSid
      }, {
        'RecaptchaCode': captchaCode
      },
        true
      );
      return response.data;
    } catch (error) {
      console.log(error);
      return error.response.data;
    }
  }

  static async getTermConditionAndAgreement(type: number, captchaCode: string) {
    try {
      let response = await Http.get(Endpoints.User.GET.getTermConditionAndAgreement(type),
        {
          'RecaptchaCode': captchaCode
        },true);
      return response;
    } catch (error) {
      console.log(error);
      return undefined;
    }
  }

  static async getTenant2FaConfig(getCustomerConfig: boolean, captchaCode: string) {
    try {
      let response = await Http.get(Endpoints.User.GET.getTenant2FaConfig(getCustomerConfig),
        {
          'RecaptchaCode': captchaCode
        },true);
      return response;
    } catch (error) {
      console.log(error);
      return undefined;
    }
  }

  static async get2FaIntervalValue({ }, captchaCode: string) {
    try {
      let response = await Http.get(Endpoints.User.GET.get2FaIntervalValue(),
        {
          'RecaptchaCode': captchaCode
        },true);
      return response;
    } catch (error) {
      console.log(error);
      return undefined;
    }
  }

  static async forgotPassword(email: string, captchaCode: string) {
    let url = Endpoints.User.POST.forgotPassword();
    try {
      let res
        : AxiosResponse = await Http.post(
          url,
          {
            email
          }, {
          'RecaptchaCode': captchaCode
        }, true
        );
      return new APIResponse(res.status, "");
    } catch (error) {
      console.log(error);
      return error;
    }

  }

  static async forgotPasswordResponse({ userId, key, newPassword }: forgotPasswordResponse, captchaCode: string) {
    let url = Endpoints.User.POST.forgotPasswordResponse();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          userId: userId,
          key: key,
          password: newPassword
        }, {
        'RecaptchaCode': captchaCode
      }, true
      );
      return new APIResponse(res.status, "");
    } catch (error) {
      console.log(error);
      return error;
    }

  }

  static async changePassword(oldPassword: string, newPassword: string) {
    let url = Endpoints.User.POST.changePassword();
    try {
      await Http.post(
        url,
        {
          oldPassword,
          newPassword
        }
      );
      return true;
    } catch (error) {
      console.log(error);
      return false;
    }

  }

  static async getTenantSettings({ }, captchaCode: string) {
    try {
      let res: AxiosResponse = await Http.get(Endpoints.User.GET.getTenantSettings(),
        {
          'RecaptchaCode': captchaCode
        },true);
        
      return new APIResponse(res.status, res.data);;
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async getSessionValidityForResetPassword({userId, key}: getSessionValidityForResetPassword, captchaCode: string) {
    try {
      const res = await Http.get(Endpoints.User.GET.getSessionValidityForResetPassword(userId, key), {
        'RecaptchaCode': captchaCode
      })

      return  new APIResponse(res.status, res.data);
    } catch (error) {
      throw error
    }
  }


}
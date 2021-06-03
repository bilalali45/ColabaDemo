export interface PasswordValidationType {
  rule: string
  result: boolean
}

export interface Skip2FAType {
  requiredForAll: boolean
  inActiveForAll: boolean
  userPrefrences: boolean
}

export interface InitialState {
  firstName: string
  lastName: string
  email: string
  confirmEmail: string
  termsConditions: boolean
  mobileNumber: string | null
  otp: string
  password: string
  confirmPassword: string,
  mobileNumberSkipped: boolean
  passwordValidation: PasswordValidationType[]
  termsConditionsContent: string | null
  validationPassed: boolean
  DontAsk2Fa: boolean
  RequestSid: string | null
  skip2FA: Skip2FAType
  modalOpened: boolean
}

export type Actions =
  | { type: 'FIRST_NAME_CHANGE', payload:string }
  | { type: 'LAST_NAME_CHANGE', payload:string }
  | { type: 'TERMS_CONDITIONS_CHANGE', payload:boolean }
  | { type: 'EMAIL_CHANGE', payload:string }
  | { type: 'CONFIRM_EMAIL_CHANGE', payload:string }
  | { type: 'MOBILE_NUMBER_CHANGE', payload:string | null }
  | { type: 'OTP_CHANGE', payload:string }
  | { type: 'PASSWORD_CHANGE', payload:string }
  | { type: 'CONFIRM_PASSWORD_CHANGE', payload:string }
  | { type: 'MOBILE_NUMBER_SKIPPED_CHANGE', payload:boolean }
  | { type: 'PASSWORD_VALIDATION_CHANGE', payload:PasswordValidationType[] }
  | { type: 'TERMS_CONDITIONS_CONTENT_CHANGE', payload:string | null }
  | { type: 'RESET_STATE', payload:any }
  | { type: 'VALIDATION_PASSED', payload:boolean }
  | { type: 'DONT_ASK_2FA_CHANGE', payload:boolean }
  | { type: 'REQUEST_SID_CHANGE', payload:string | null }
  | { type: 'SKIP_2FA_CHANGE', payload:any }
  | { type: 'MODAL_OPEN_CHANGE', payload:boolean }

export const initialState: InitialState = {
  firstName: '',
  lastName: '',
  email: '',
  confirmEmail: '',
  termsConditions: false,
  mobileNumber: null,
  otp: '',
  password: '',
  confirmPassword: '',
  mobileNumberSkipped: false,
  passwordValidation: [],
  termsConditionsContent: null,
  validationPassed: false,
  DontAsk2Fa: false,
  RequestSid: null,
  skip2FA: {
    requiredForAll: false,
    inActiveForAll: false,
    userPrefrences: false
  },
  modalOpened: false
}

export const signupReducer = (state: InitialState, action: Actions) => {
  const { type, payload } = action

  switch (type) {
    case 'FIRST_NAME_CHANGE':
      return {
        ...state,
        firstName: payload
      }
    case 'LAST_NAME_CHANGE':
      return {
        ...state,
        lastName: payload
      }
    case 'EMAIL_CHANGE':
      return {
        ...state,
        email: payload
      }
    case 'CONFIRM_EMAIL_CHANGE':
      return {
        ...state,
        confirmEmail: payload
      }
    case 'TERMS_CONDITIONS_CHANGE':
      return {
        ...state,
        termsConditions: payload
      }
    case 'MOBILE_NUMBER_CHANGE':
      return {
        ...state,
        mobileNumber: payload
      }
    case 'OTP_CHANGE':
      return {
        ...state,
        otp: payload
      }
    case 'PASSWORD_CHANGE':
      return {
        ...state,
        password: payload
      }
    case 'CONFIRM_PASSWORD_CHANGE':
      return {
        ...state,
        confirmPassword: payload
      }
    case 'MOBILE_NUMBER_SKIPPED_CHANGE':
      return {
        ...state,
        mobileNumberSkipped: payload
      }
    case 'PASSWORD_VALIDATION_CHANGE':
      return {
        ...state,
        passwordValidation: payload,
        validationPassed: false
      }
    case 'TERMS_CONDITIONS_CONTENT_CHANGE':
      return {
        ...state,
        termsConditionsContent: payload
      }
    case 'VALIDATION_PASSED':
      return {
        ...state,
        validationPassed: true
      }
    case 'DONT_ASK_2FA_CHANGE':
      return {
        ...state,
        DontAsk2Fa: payload
      }
    case 'REQUEST_SID_CHANGE':
      return {
        ...state,
        RequestSid: payload
      }
    case 'SKIP_2FA_CHANGE': {
      return {
        ...state,
        skip2FA: {
          ...state.skip2FA,
          ...payload
        }
      }
    }
    case 'MODAL_OPEN_CHANGE':
      return {
        ...state,
        modalOpened: payload
      }
    case 'RESET_STATE':
      return initialState
    default:
      return state
  }
}

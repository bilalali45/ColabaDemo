export class LoanStatusUpdateEndPoints {
    static GET = {
      getLoanStatusUpdateSettings: () => `/api/Milestone/setting/GetLoanStatuses`
    }
    static POST = {
       addLoanStatusUpdateListSetting: () => `/api/Milestone/Setting/UpdateLoanStatuses`,
       updateLoanStatusListSetting: () => `/api/Milestone/Setting/UpdateLoanStatuses`,
       updateAllEnableDisableLoanStatusEmail: () => `/api/Milestone/Setting/EnableDisableAllEmailReminders`,
       updateEnableDisableLoanStatusEmail: () => `/api/Milestone/Setting/EnableDisableEmailReminder`
      
    }
    static DELETE = {

    }
}
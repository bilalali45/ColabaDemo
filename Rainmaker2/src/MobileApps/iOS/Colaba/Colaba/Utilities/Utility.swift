//
//  Utility.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import Foundation
import UIKit
import NVActivityIndicatorView

struct Utility {
    
    static let authStoryboard = UIStoryboard.init(name: "Authentication", bundle: nil)
    static let mainStoryboard = UIStoryboard.init(name: "Main", bundle: nil)
    static let loanDetailStoryboard = UIStoryboard.init(name: "PipelineDetail", bundle: nil)
    static let testStoryboard = UIStoryboard.init(name: "Test", bundle: nil)
    static let newApplicationStoryboard = UIStoryboard.init(name: "NewApplication", bundle: nil)
    static let requestDocumentStoryboard = UIStoryboard.init(name: "RequestDocument", bundle: nil)
    
    static private var pipelineDateFormatter: DateFormatter?
    static private var loanApplicationDateFormatter: DateFormatter?
    static private var documentDateFormatter: DateFormatter?
    
    static func getLoginNavigationVC() -> LoginNavigationViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "LoginNavigationViewController")  as! LoginNavigationViewController
    }
    
    static func getFaceLockNavigationVC() -> FaceLockNavigationViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "FaceLockNavigationViewController")  as! FaceLockNavigationViewController
    }
    
    static func getFingerPrintNavigationVC() -> FingerPrintNavigationViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "FingerPrintNavigationViewController")  as! FingerPrintNavigationViewController
    }
    
    static func getFaceRecognitionVC() -> FaceRecognitionViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "FaceRecognitionViewController") as! FaceRecognitionViewController
    }
    
    static func getFingerPrintVC() -> FingerPrintViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "FingerPrintViewController") as! FingerPrintViewController
    }
    
    static func getLoginVC() -> LoginViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "LoginViewController") as! LoginViewController
    }
    
    static func getForgotPasswordVC() -> ForgotPasswordViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "ForgotPasswordViewController") as! ForgotPasswordViewController
    }
    
    static func getResetPasswordSuccessfullVC() -> ResetPasswordSuccessfullViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "ResetPasswordSuccessfullViewController") as! ResetPasswordSuccessfullViewController
    }
    
    static func getPhoneNumberVC() -> PhoneNumberViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "PhoneNumberViewController") as! PhoneNumberViewController
    }
    
    static func getCodeVC() -> CodeViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "CodeViewController") as! CodeViewController
    }
    
    static func getMainTabBarVC() -> MainTabBarViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "MainTabBarViewController") as! MainTabBarViewController
    }
    
    static func getDashboardVC() -> DashboardViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "DashboardViewController") as! DashboardViewController
    }
    
    static func getDummyDashboardVC() -> DummyDashboardViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "DummyDashboardViewController") as! DummyDashboardViewController
    }
    
    static func getDashboardNavVC() -> UINavigationController{
        return mainStoryboard.instantiateViewController(withIdentifier: "DashboardNavigation") as! UINavigationController
    }
    
    static func getPipelineVC() -> PipelineViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "PipelineViewController") as! PipelineViewController
    }
    
    static func getActivePipelineVC() -> ActivePipelineViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "ActivePipelineViewController") as! ActivePipelineViewController
    }
    
    static func getInActivePipelineVC() -> InActivePipelineViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "InActivePipelineViewController") as! InActivePipelineViewController
    }
    
    static func getPipelineMoreVC() -> PipelineMoreViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "PipelineMoreViewController") as! PipelineMoreViewController
    }
    
    static func getFiltersVC() -> FiltersViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "FiltersViewController") as! FiltersViewController
    }
    
    static func getSearchVC() -> SearchViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "SearchViewController") as! SearchViewController
    }
    
    static func getCreateNewPopupVC() -> CreateNewPopupViewController{
        return mainStoryboard.instantiateViewController(withIdentifier: "CreateNewPopupViewController") as! CreateNewPopupViewController
    }
    
    static func getLoanDetailVC() -> LoanDetailViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "LoanDetailViewController") as! LoanDetailViewController
    }
    
    static func getOverviewVC() -> OverviewViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "OverviewViewController") as! OverviewViewController
    }
    
    static func getApplicationVC() -> ApplicationViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "ApplicationViewController") as! ApplicationViewController
    }
    
    static func getDocumentsVC() -> DocumentsViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "DocumentsViewController") as! DocumentsViewController
    }
    
    static func getApplicationStatusVC() -> ApplicationStatusViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "ApplicationStatusViewController") as! ApplicationStatusViewController
    }
    
    static func getDocumentsDetailVC() -> DocumentsDetailViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "DocumentsDetailViewController") as! DocumentsDetailViewController
    }
    
    static func getBorrowerInformationVC() -> BorrowerInformationViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "BorrowerInformationViewController") as! BorrowerInformationViewController
    }
    
    static func getAddResidenceVC() -> AddResidenceViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "AddResidenceViewController") as! AddResidenceViewController
    }
    
    static func getAddPreviousResidenceVC() -> AddPreviousResidenceViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "AddPreviousResidenceViewController") as! AddPreviousResidenceViewController
    }
    
    static func getAddMailingAddressVC() -> AddMailingAddressViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "AddMailingAddressViewController") as! AddMailingAddressViewController
    }
    
    static func getUnmarriedFollowUpQuestionsVC() -> UnmarriedFollowUpQuestionsViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "UnmarriedFollowUpQuestionsViewController") as! UnmarriedFollowUpQuestionsViewController
    }
    
    static func getNonPermanentResidenceFollowUpQuestionsVC() -> NonPermanentResidenceFollowUpQuestionsViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "NonPermanentResidenceFollowUpQuestionsViewController") as! NonPermanentResidenceFollowUpQuestionsViewController
    }
    
    static func getActiveDutyPersonnelFollowUpQuestionVC() -> ActiveDutyPersonnelFollowUpQuestionViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "ActiveDutyPersonnelFollowUpQuestionViewController") as! ActiveDutyPersonnelFollowUpQuestionViewController
    }
    
    static func getReserveFollowUpQuestionsVC() -> ReserveFollowUpQuestionsViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "ReserveFollowUpQuestionsViewController") as! ReserveFollowUpQuestionsViewController
    }
    
    static func getSaveAddressPopupVC() -> SaveAddressPopupViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "SaveAddressPopupViewController") as! SaveAddressPopupViewController
    }
    
    static func getDeleteAddressPopupVC() -> DeleteAddressPopupViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: "DeleteAddressPopupViewController") as! DeleteAddressPopupViewController
    }
    
    static func getPurchaseLoanInfoVC() -> PurchaseLoanInfoViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: PurchaseLoanInfoViewController.self)) as! PurchaseLoanInfoViewController
    }
    
    static func getRefinanceLoanInfoVC() -> RefinanceLoanInfoViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: RefinanceLoanInfoViewController.self)) as! RefinanceLoanInfoViewController
    }
    
    static func getPurchaseSubjectPropertyVC() -> PurchaseSubjectPropertyViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: PurchaseSubjectPropertyViewController.self)) as! PurchaseSubjectPropertyViewController
    }
    
    static func getRefinanceSubjectPropertyVC() -> RefinanceSubjectPropertyViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: RefinanceSubjectPropertyViewController.self)) as! RefinanceSubjectPropertyViewController
    }
    
    static func getMixPropertyDetailFollowUpVC() -> MixPropertyDetailFollowUpViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: MixPropertyDetailFollowUpViewController.self)) as! MixPropertyDetailFollowUpViewController
    }
    
    static func getSubjectPropertyAddressVC() -> SubjectPropertyAddressViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: SubjectPropertyAddressViewController.self)) as! SubjectPropertyAddressViewController
    }
    
    static func getFirstMortgageFollowupQuestionsVC() -> FirstMortgageFollowupQuestionsViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: FirstMortgageFollowupQuestionsViewController.self)) as! FirstMortgageFollowupQuestionsViewController
    }
    
    static func getSecondMortgageFollowupQuestionsVC() -> SecondMortgageFollowupQuestionsViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: SecondMortgageFollowupQuestionsViewController.self)) as! SecondMortgageFollowupQuestionsViewController
    }
    
    static func getAssetsVC() -> AssetsViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: AssetsViewController.self)) as! AssetsViewController
    }
    
    static func getAssetsDetailVC() -> AssetsDetailViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: AssetsDetailViewController.self)) as! AssetsDetailViewController
    }
    
    static func getAddBankAccountVC() -> AddBankAccountViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: AddBankAccountViewController.self)) as! AddBankAccountViewController
    }
    
    static func getAddRetirementAccountVC() -> AddRetirementAccountViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: AddRetirementAccountViewController.self)) as! AddRetirementAccountViewController
    }
    
    static func getAddStockBondVC() -> AddStockBondViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: AddStockBondViewController.self)) as! AddStockBondViewController
    }
    
    static func getAddProceedsFromTransactionVC() -> AddProceedsFromTransactionViewController{
        return loanDetailStoryboard.instantiateViewController(withIdentifier: String(describing: AddProceedsFromTransactionViewController.self)) as! AddProceedsFromTransactionViewController
    }
    
    static func getAddGiftFundsVC() -> AddGiftFundsViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: AddGiftFundsViewController.self)) as! AddGiftFundsViewController
    }
    
    static func getAddOtherAssetsVC() -> AddOtherAssetsViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: AddOtherAssetsViewController.self)) as! AddOtherAssetsViewController
    }
    
    static func getIncomeVC() -> IncomeViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: IncomeViewController.self)) as! IncomeViewController
    }
    
    static func getIncomeDetailVC() -> IncomeDetailViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: IncomeDetailViewController.self)) as! IncomeDetailViewController
    }
    
    static func getAddEmployementPopupVC() -> AddEmployementPopupViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: AddEmployementPopupViewController.self)) as! AddEmployementPopupViewController
    }
    
    static func getAddCurrentEmployementVC() -> AddCurrentEmployementViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: AddCurrentEmployementViewController.self)) as! AddCurrentEmployementViewController
    }
    
    static func getCurrentEmployerAddressVC() -> CurrentEmployerAddressViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: CurrentEmployerAddressViewController.self)) as! CurrentEmployerAddressViewController
    }
    
    static func getAddPreviousEmploymentVC() -> AddPreviousEmploymentViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: AddPreviousEmploymentViewController.self)) as! AddPreviousEmploymentViewController
    }
    
    static func getAddSelfEmploymentVC() -> AddSelfEmploymentViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: AddSelfEmploymentViewController.self)) as! AddSelfEmploymentViewController
    }
    
    static func getAddBusinessVC() -> AddBusinessViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: AddBusinessViewController.self)) as! AddBusinessViewController
    }
    
    static func getAddMilitaryPayVC() -> AddMilitaryPayViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: AddMilitaryPayViewController.self)) as! AddMilitaryPayViewController
    }
    
    static func getAddRetirementIncomeVC() -> AddRetirementIncomeViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: AddRetirementIncomeViewController.self)) as! AddRetirementIncomeViewController
    }
    
    static func getAddOtherIncomeVC() -> AddOtherIncomeViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: AddOtherIncomeViewController.self)) as! AddOtherIncomeViewController
    }
    
    static func getRealEstateVC() -> RealEstateViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: RealEstateViewController.self)) as! RealEstateViewController
    }
    
    static func getGovernmentQuestionsVC() -> GovernmentQuestionsViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: GovernmentQuestionsViewController.self)) as! GovernmentQuestionsViewController
    }
    
    static func getGovernmentQuestionDetailVC() -> GovernmentQuestionDetailViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: GovernmentQuestionDetailViewController.self)) as! GovernmentQuestionDetailViewController
    }
    
    static func getUndisclosedBorrowerFundsVC() -> UndisclosedBorrowerFundsViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: UndisclosedBorrowerFundsViewController.self)) as! UndisclosedBorrowerFundsViewController
    }
    
    static func getSendDocumentRequestVC() -> SendDocumentRequestViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: SendDocumentRequestViewController.self)) as! SendDocumentRequestViewController
    }
    
    static func getUndisclosedBorrowerFundsFollowupQuestionsVC() -> UndisclosedBorrowerFundsFollowupQuestionsViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: UndisclosedBorrowerFundsFollowupQuestionsViewController.self)) as! UndisclosedBorrowerFundsFollowupQuestionsViewController
    }
    
    static func getOwnershipInterestInPropertyVC() -> OwnershipInterestInPropertyViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: OwnershipInterestInPropertyViewController.self)) as! OwnershipInterestInPropertyViewController
    }
    
    static func getOwnershipInterestInPropertyFollowupQuestionVC() -> OwnershipInterestInPropertyFollowupQuestionViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: OwnershipInterestInPropertyFollowupQuestionViewController.self)) as! OwnershipInterestInPropertyFollowupQuestionViewController
    }
    
    static func getPriorityLiensViewController() -> PriorityLiensViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: PriorityLiensViewController.self)) as! PriorityLiensViewController
    }
    
    static func getPriorityLiensFollowupQuestionViewController() -> PriorityLiensFollowupQuestionViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: PriorityLiensFollowupQuestionViewController.self)) as! PriorityLiensFollowupQuestionViewController
    }
    
    static func getUndisclosedMortgageApplicationVC() -> UndisclosedMortgageApplicationViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: UndisclosedMortgageApplicationViewController.self)) as! UndisclosedMortgageApplicationViewController
    }
    
    static func getUndisclosedCreditApplicationVC() -> UndisclosedCreditApplicationViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: UndisclosedCreditApplicationViewController.self)) as! UndisclosedCreditApplicationViewController
    }
    
    static func getDebtCoSignerVC() -> DebtCoSignerViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: DebtCoSignerViewController.self)) as! DebtCoSignerViewController
    }
    
    static func getOutstandingJudgementsVC() -> OutstandingJudgementsViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: OutstandingJudgementsViewController.self)) as! OutstandingJudgementsViewController
    }
    
    static func getFedralDebtVC() -> FedralDebtViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: FedralDebtViewController.self)) as! FedralDebtViewController
    }
    
    static func getPartyToLawsuitVC() -> PartyToLawsuitViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: PartyToLawsuitViewController.self)) as! PartyToLawsuitViewController
    }
    
    static func getTitleConveyanceVC() -> TitleConveyanceViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: TitleConveyanceViewController.self)) as! TitleConveyanceViewController
    }
    
    static func getPreForceClosureVC() -> PreForceClosureViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: PreForceClosureViewController.self)) as! PreForceClosureViewController
    }
    
    static func getForceClosedPropertyVC() -> ForceClosedPropertyViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: ForceClosedPropertyViewController.self)) as! ForceClosedPropertyViewController
    }
    
    static func getBankruptcyVC() -> BankruptcyViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: BankruptcyViewController.self)) as! BankruptcyViewController
    }
    
    static func getBankruptcyFollowupVC() -> BankruptcyFollowupViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: BankruptcyFollowupViewController.self)) as! BankruptcyFollowupViewController
    }
    
    static func getChildSupportVC() -> ChildSupportViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: ChildSupportViewController.self)) as! ChildSupportViewController
    }
    
    static func getChildSupportFollowupQuestionsVC() -> ChildSupportFollowupQuestionsViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: ChildSupportFollowupQuestionsViewController.self)) as! ChildSupportFollowupQuestionsViewController
    }
    
    static func getDemographicInformationVC() -> DemographicInformationViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: DemographicInformationViewController.self)) as! DemographicInformationViewController
    }
    
    static func getDemographicDetailVC() -> DemographicDetailViewController{
        return testStoryboard.instantiateViewController(withIdentifier: String(describing: DemographicDetailViewController.self)) as! DemographicDetailViewController
    }
    
    static func getStartNewApplicationVC() -> StartNewApplicationViewController{
        return newApplicationStoryboard.instantiateViewController(withIdentifier: String(describing: StartNewApplicationViewController.self)) as! StartNewApplicationViewController
    }
    
    static func getLoanOfficerMainVC() -> LoanOfficerMainViewController{
        return newApplicationStoryboard.instantiateViewController(withIdentifier: String(describing: LoanOfficerMainViewController.self)) as! LoanOfficerMainViewController
    }
    
    static func getLoanOfficerListVC() -> LoanOfficerListViewController{
        return newApplicationStoryboard.instantiateViewController(withIdentifier: String(describing: LoanOfficerListViewController.self)) as! LoanOfficerListViewController
    }
    
    static func getAssignLoanOfficerPopupVC() -> AssignLoanOfficerPopupViewController{
        return newApplicationStoryboard.instantiateViewController(withIdentifier: String(describing: AssignLoanOfficerPopupViewController.self)) as! AssignLoanOfficerPopupViewController
    }
    
    static func getAssignLoanOfficerVC() -> AssignLoanOfficerViewController{
        return newApplicationStoryboard.instantiateViewController(withIdentifier: String(describing: AssignLoanOfficerViewController.self)) as! AssignLoanOfficerViewController
    }
    
    static func getDuplicateContactPopupVC() -> DuplicateContactPopupViewController{
        return newApplicationStoryboard.instantiateViewController(withIdentifier: String(describing: DuplicateContactPopupViewController.self)) as! DuplicateContactPopupViewController
    }
    
    static func getRequestDocumentVC() -> RequestDocumentViewController{
        return requestDocumentStoryboard.instantiateViewController(withIdentifier: String(describing: RequestDocumentViewController.self)) as! RequestDocumentViewController
    }
    
    static func getDocumentTemplatesVC() -> DocumentTemplatesViewController{
        return requestDocumentStoryboard.instantiateViewController(withIdentifier: String(describing: DocumentTemplatesViewController.self)) as! DocumentTemplatesViewController
    }
    
    static func getDocumentsListVC() -> DocumentsListViewController{
        return requestDocumentStoryboard.instantiateViewController(withIdentifier: String(describing: DocumentsListViewController.self)) as! DocumentsListViewController
    }
    
    static func getCheckListVC() -> CheckListViewController{
        return requestDocumentStoryboard.instantiateViewController(withIdentifier: String(describing: CheckListViewController.self)) as! CheckListViewController
    }
    
    static func getSearchRequestDocumentVC() -> SearchRequestDocumentViewController{
        return requestDocumentStoryboard.instantiateViewController(withIdentifier: String(describing: SearchRequestDocumentViewController.self)) as! SearchRequestDocumentViewController
    }
    
    static func getDocumentsTypeViewController() -> DocumentsTypeViewController{
        return requestDocumentStoryboard.instantiateViewController(withIdentifier: String(describing: DocumentsTypeViewController.self)) as! DocumentsTypeViewController
    }
    
    static func getBankStatementVC() -> BankStatementViewController{
        return requestDocumentStoryboard.instantiateViewController(withIdentifier: String(describing: BankStatementViewController.self)) as! BankStatementViewController
    }
    
    static func getCustomDocumentVC() -> CustomDocumentViewController{
        return requestDocumentStoryboard.instantiateViewController(withIdentifier: String(describing: CustomDocumentViewController.self)) as! CustomDocumentViewController
    }
    
    static func getDeleteDocumentPopupVC() -> DeleteDocumentPopupViewController{
        return requestDocumentStoryboard.instantiateViewController(withIdentifier: String(describing: DeleteDocumentPopupViewController.self)) as! DeleteDocumentPopupViewController
    }
    
    static func getDocumentRequestSentVC() -> DocumentRequestSentViewController{
        return requestDocumentStoryboard.instantiateViewController(withIdentifier: String(describing: DocumentRequestSentViewController.self)) as! DocumentRequestSentViewController
    }
    
    static var localPiplineDateFormatter: DateFormatter{
        get{
            if (pipelineDateFormatter == nil){
                pipelineDateFormatter = DateFormatter()
                //pipelineDateFormatter?.timeZone = TimeZone(abbreviation: "UTC")
                pipelineDateFormatter?.locale = .current
                pipelineDateFormatter?.dateFormat = "yyyy-MM-dd HH:mm:ss"
            }
            return pipelineDateFormatter!
        }
        set{
            
        }
    }
    
    static var localLoanApplicationDateFormatter: DateFormatter{
        get{
            if (loanApplicationDateFormatter == nil){
                loanApplicationDateFormatter = DateFormatter()
                loanApplicationDateFormatter?.timeZone = TimeZone(abbreviation: "UTC")
                loanApplicationDateFormatter?.locale = .current
                loanApplicationDateFormatter?.dateFormat = "yyyy-MM-dd HH:mm:ss"
            }
            return loanApplicationDateFormatter!
        }
        set{
            
        }
    }
    
    static var localDocumentDateFormatter: DateFormatter{
        get{
            if (documentDateFormatter == nil){
                documentDateFormatter = DateFormatter()
                documentDateFormatter?.locale = .current
                documentDateFormatter?.dateFormat = "eee MMM dd, yyyy hh:mm a"
            }
            return documentDateFormatter!
        }
        set{
            
        }
        
    }
    
    static func checkDeviceAuthType() -> String {
         let authType = LocalAuthManager.shared.biometricType
            switch authType {
            case .none:
                return kDeviceNotRegistered
            case .touchID:
                return kTouchID
            case .faceID:
                return kFaceID
            }
     }

    static func showOrHideLoader(shouldShow: Bool){
    
        let activityData = ActivityData(size: CGSize(width: 50, height: 50), message: "", messageFont: nil, messageSpacing: nil, type: .circleStrokeSpin, color: UIColor.init(red: 0.0, green: 0.0, blue: 0.0, alpha: 0.40), padding: nil, displayTimeThreshold: nil, minimumDisplayTime: 2, backgroundColor: .clear, textColor: nil)
        if (shouldShow){
            NVActivityIndicatorPresenter.sharedInstance.startAnimating(activityData)
        }
        else{
            NVActivityIndicatorPresenter.sharedInstance.stopAnimating()
        }
    }
    
    static func getUserAccessToken() -> String{
        if let user = UserModel.getCurrentUser(){
            return user.token
        }
        return ""
    }
    
    static func getUserRefreshToken() -> String{
        if let user = UserModel.getCurrentUser(){
            return user.refreshToken
        }
        return ""
    }
    
    static func getUserFirstName() -> String{
        if let user = UserModel.getCurrentUser(){
            return user.firstName
        }
        return ""
    }
    
    static func getUserLastName() -> String{
        if let user = UserModel.getCurrentUser(){
            return user.lastName
        }
        return ""
    }
    
    static func getUserFullName() -> String{
        if let user = UserModel.getCurrentUser(){
            return "\(user.firstName) \(user.lastName)"
        }
        return ""
    }
    
    static func getUserEmail() -> String{
        if let user = UserModel.getCurrentUser(){
            return user.userName
        }
        return ""
    }
    
    static func getTokenValidityDate() -> String{
        if let user = UserModel.getCurrentUser(){
            return user.validTo
        }
        return ""
    }
    
    static func getDate() -> String{
        var date = localPiplineDateFormatter.string(from: Date())
        date = date.replacingOccurrences(of: " ", with: "T")
        date = "\(date)Z"
        return date
    }
    
    static func getIsTokenExpire(tokenValidityDate: String) -> Bool{
        //2021-07-02T18:54:21Z
        let tokenValidToUTC = tokenValidityDate.replacingOccurrences(of: "T", with: " ").replacingOccurrences(of: "Z", with: "")
        let todayDateStringInUTC = localLoanApplicationDateFormatter.string(from: Date())
        
        if let tokenValidDate = localLoanApplicationDateFormatter.date(from: tokenValidToUTC), let todayDate = localLoanApplicationDateFormatter.date(from: todayDateStringInUTC){
            
            var tokenValidDateInTimeStamp = Int(tokenValidDate.timeIntervalSince1970)
            tokenValidDateInTimeStamp = tokenValidDateInTimeStamp - 43200
            let todayDateInTimeStamp = Int(todayDate.timeIntervalSince1970)
            
            let difference = tokenValidDateInTimeStamp - todayDateInTimeStamp
            if (difference > 0){
                return false
            }
            else{
                return true
            }
        }
                
        return true
    }
    
    static func getGreetingMessage() -> String{
        
        let hour = Calendar.current.component(.hour, from: Date())
        
        if (hour == 0 || hour == 24){
            return "Good Morning"
        }
        else if (hour >= 1 && hour < 12){
            return "Good Morning"
        }
        else if (hour >= 12 && hour < 17){
            return "Good Afternoon"
        }
        else if (hour >= 17 && hour <= 23){
            return "Good Evening"
        }
        else{
            return "Good Morning"
        }
    }
    
    static func getDocumentFilesTimeStamp(_ dateString: String) -> Int{
        var actualDate = ""
        let loanDate = dateString.components(separatedBy: "T")
        if loanDate.count > 1{
            let loanTime = loanDate[1].components(separatedBy: ".")
            if loanTime.first != nil{
                actualDate = "\(loanDate.first!) \(loanTime.first!)"
            }
        }
        
        if let date = localLoanApplicationDateFormatter.date(from: actualDate){
            return Int(date.timeIntervalSince1970)
        }
        else{
            return 0
        }
    }
    
    static public func timeAgoSince(_ dateString: String) -> String {
        
        var actualDate = ""
        let loanDate = dateString.components(separatedBy: "T")
        if loanDate.count > 1{
            let loanTime = loanDate[1].components(separatedBy: ".")
            if loanTime.first != nil{
                actualDate = "\(loanDate.first!) \(loanTime.first!)"
            }
        }
        
        if let date = localLoanApplicationDateFormatter.date(from: actualDate){
            let calendar = Calendar.current
            let now = Date()
            let unitFlags: NSCalendar.Unit = [.second, .minute, .hour, .day, .weekOfYear, .month, .year]
            let components = (calendar as NSCalendar).components(unitFlags, from: date, to: now, options: [])
            
            if let year = components.year, year >= 2 {
                return "\(year) years ago"
            }
            
            if let year = components.year, year >= 1 {
                return "Last year"
            }
            
            if let month = components.month, month >= 2 {
                return "\(month) months ago"
            }
            
            if let month = components.month, month >= 1 {
                return "Last month"
            }
            
            if let week = components.weekOfYear, week >= 2 {
                return "\(week) weeks ago"
            }
            
            if let week = components.weekOfYear, week >= 1 {
                return "Last week"
            }
            
            if let day = components.day, day >= 2 {
                return "\(day) days ago"
            }
            
            if let day = components.day, day >= 1 {
                return "Yesterday"
            }
            
            if let hour = components.hour, hour >= 2 {
                return "\(hour) hours ago"
            }
            
            if let hour = components.hour, hour >= 1 {
                return "1 hour ago"
            }
            
            if let minute = components.minute, minute >= 2 {
                return "\(minute) minutes ago"
            }
            
            if let minute = components.minute, minute >= 1 {
                return "1 minute ago"
            }
            
            if let second = components.second, second >= 3 {
                return "\(second) seconds ago"
            }
            
            return "Just now"
        }
        return "Just now"
    }
    
    static func getDocumentDate(_ dateString: String) -> String{
        
        var actualDate = ""
        let loanDate = dateString.components(separatedBy: "T")
        if loanDate.count > 1{
            let loanTime = loanDate[1].components(separatedBy: ".")
            if loanTime.first != nil{
                actualDate = "\(loanDate.first!) \(loanTime.first!)"
            }
        }
        
        if let date = localLoanApplicationDateFormatter.date(from: actualDate){
            return localDocumentDateFormatter.string(from: date)
        }
        
        return ""
    }
    
    static func getDocumentFileTypeIcon(fileName: String) -> UIImage{
        
        if let fileType = fileName.components(separatedBy: ".").last{
            if (fileType == "pdf"){
                return UIImage(named: "pdf-icon")!
            }
            else if (fileType == "png"){
                return UIImage(named: "png-icon")!
            }
            else if (fileType == "jpg" || fileType == "jpeg"){
                return UIImage(named: "jpg-icon")!
            }
            else{
                return UIImage(named: "png-icon")!
            }
        }
        else{
            return UIImage(named: "png-icon")!
        }
        
    }
    
    static func getDocumentStatusIcon(documentStatus: String) -> UIImage{
        if (documentStatus.contains("Pending")){
            return UIImage(named: "Blue-Dot")!
        }
        else if (documentStatus.contains("Started")){
            return UIImage(named: "Yellow-Dot")!
        }
        else if (documentStatus.contains("Completed")){
            return UIImage(named: "Green-Dot")!
        }
        else if (documentStatus.contains("Borrower")){
            return UIImage(named: "Red-Dot")!
        }
        else if (documentStatus.contains("In draft")){
            return UIImage(named: "LightBlue-Dot")!
        }
        else if (documentStatus.contains("Manually")){
            return UIImage(named: "Grey-Dot")!
        }
        else{
            return UIImage(named: "Red-Dot")!
        }
    }
    
    static func checkIsSmallDevice() -> Bool{
        if (UIDevice.current.screenType == .iPhones_4_4S || UIDevice.current.screenType == .iPhones_5_5s_5c_SE || UIDevice.current.screenType == .iPhones_6_6s_7_8){
            return true
        }
        else{
            return false
        }
    }
    
}

func formatNumber(with mask: String, number: String) -> String {
    let numbers = number.replacingOccurrences(of: "[^0-9]", with: "", options: .regularExpression)
    var result = ""
    var index = numbers.startIndex // numbers iterator

    // iterate over the mask characters until the iterator of numbers ends
    for ch in mask where index < numbers.endIndex {
        if ch == "X" {
            // mask requires a number in this place, so take the next one
            result.append(numbers[index])

            // move numbers iterator to the next index
            index = numbers.index(after: index)

        } else {
            result.append(ch) // just append a mask character
        }
    }
    return result
}

func cleanString(string: String, replaceCharacters: [String], replaceWith: String) -> String {
    var replaceString = string
    for character in replaceCharacters {
        replaceString = replaceString.replacingOccurrences(of: character, with: replaceWith)
    }
    return replaceString
}

func createAttributedString(baseString: String, string: String, fontColor: UIColor, font: UIFont) -> NSMutableAttributedString {
    
    //        Base String would remain same. String attributes would change
    let textAttributes = [
        NSAttributedString.Key.foregroundColor : fontColor,
        NSAttributedString.Key.font : font,
        ] as [NSAttributedString.Key : Any]
    let attributedString = NSMutableAttributedString(string: baseString, attributes: nil)
    let range = (attributedString.string as NSString).range(of: string)
    attributedString.setAttributes(textAttributes, range: range)
    
    return attributedString
}

func updateAttributedString(baseString: NSMutableAttributedString, string: String, fontColor: UIColor, font: UIFont) -> NSMutableAttributedString {
    
    //        Base String would remain same. String attributes would change
    let textAttributes = [
        NSAttributedString.Key.foregroundColor : fontColor,
        NSAttributedString.Key.font : font,
        ] as [NSAttributedString.Key : Any]
    let range = (baseString.string as NSString).range(of: string)
    baseString.setAttributes(textAttributes, range: range)
    
    return baseString
}

func appendAttributedString(baseString: NSMutableAttributedString, string: String, fontColor: UIColor, font: UIFont) -> NSMutableAttributedString {
    
    //        Base String would remain same. String attributes would change
    let textAttributes = [
        NSAttributedString.Key.foregroundColor : fontColor,
        NSAttributedString.Key.font : font,
        ] as [NSAttributedString.Key : Any]
    let attributedString = NSMutableAttributedString(string: string, attributes: textAttributes)
    baseString.append(attributedString)
    return baseString
}

func createAttributedPrefix(prefix : String) -> NSMutableAttributedString {
    //TODO: Create new attributed string function with basic strings attributes and target strings attributes
    var attributedString =  createAttributedString(baseString: prefix, string: prefix, fontColor: Theme.getAppGreyColor(), font: Theme.getRubikMediumFont(size: 15.0))
    attributedString =  updateAttributedString(baseString: attributedString, string: "|", fontColor: Theme.getSeparatorNormalColor(), font: Theme.getRubikRegularFont(size: 14.0))
    return attributedString
}

func createAttributedTextWithPrefix(prefix : String, string: String) -> NSMutableAttributedString {
    return appendAttributedString(baseString: createAttributedPrefix(prefix: prefix), string: string, fontColor: Theme.getAppBlackColor(), font: Theme.getRubikRegularFont(size: 15))
}

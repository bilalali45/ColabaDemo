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
    
    static private var pipelineDateFormatter: DateFormatter?
    static private var loanApplicationDateFormatter: DateFormatter?
    static private var documentDateFormatter: DateFormatter?
    
    static func getLoginNavigationVC() -> LoginNavigationViewController{
        return authStoryboard.instantiateViewController(withIdentifier: "LoginNavigationViewController")  as! LoginNavigationViewController
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
    
    static var localPiplineDateFormatter: DateFormatter{
        get{
            if (pipelineDateFormatter == nil){
                pipelineDateFormatter = DateFormatter()
                pipelineDateFormatter?.timeZone = TimeZone(abbreviation: "UTC")
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

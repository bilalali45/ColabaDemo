//
//  LoanDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/07/2021.
//

import UIKit
import CarbonKit
import MessageUI

class LoanDetailViewController: BaseViewController {

    //MARK:- Outlets and properties
    
    @IBOutlet weak var navigationView: UIView!
    @IBOutlet weak var navigationViewTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var navigationSeperator: UIView!
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var lblLoanPurpose: UILabel!
    @IBOutlet weak var btnOptions: UIButton!
    @IBOutlet weak var btnAddPerson: UIButton!
    @IBOutlet weak var tabView: UIView!
    @IBOutlet weak var btnRequestDocument: UIButton!
    @IBOutlet weak var btnCall: UIButton!
    @IBOutlet weak var btnSms: UIButton!
    @IBOutlet weak var btnEmail: UIButton!
    @IBOutlet weak var walkthroughView: UIView!
    @IBOutlet weak var walkthroughViewTrailingConstraint: NSLayoutConstraint!
    @IBOutlet weak var walkthroughViewBottomConstraint: NSLayoutConstraint!
    
    var loanApplicationId = 0
    var borrowerName = ""
    var loanPurpose = ""
    var phoneNumber = ""
    var email = ""
    var documentCounterView = UIView()
    var selectedTab = 0
    var isAfterCreateNewApplication = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupHeaderAndFooter()
        
        NotificationCenter.default.addObserver(self, selector: #selector(hidesNavigationBar), name: NSNotification.Name(rawValue: kNotificationHidesNavigationBar), object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(showNavigationBar), name: NSNotification.Name(rawValue: kNotificationShowNavigationBar), object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(showRequestDocumentFooterButton), name: NSNotification.Name(rawValue: kNotificationShowRequestDocumentFooterButton), object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(hideRequestDocumentFooterButton), name: NSNotification.Name(rawValue: kNotificationHideRequestDocumentFooterButton), object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(showDocumentTab), name: NSNotification.Name(rawValue: kNotificationShowDocumentsTab), object: nil)
        
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        getUnreadDocuments()
    }
    
    //MARK:- Methods and Actions
    
    func setupHeaderAndFooter(){
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.03) {
            let tabItems = ["Overview", "Application", "Documents"]
            let carbonTabSwipeNavigation = CarbonTabSwipeNavigation(items: tabItems, delegate: self)
            
            carbonTabSwipeNavigation.carbonSegmentedControl?.backgroundColor = Theme.getTopTabsColor()
            carbonTabSwipeNavigation.setTabBarHeight(50)
            carbonTabSwipeNavigation.setIndicatorColor(nil)
            carbonTabSwipeNavigation.setIndicatorHeight(4)
            carbonTabSwipeNavigation.setNormalColor(Theme.getAppGreyColor(), font: Theme.getRubikRegularFont(size: 15))
            carbonTabSwipeNavigation.setSelectedColor(Theme.getButtonBlueColor(), font: Theme.getRubikRegularFont(size: 15))
            carbonTabSwipeNavigation.carbonSegmentedControl?.imageNormalColor = .clear
            carbonTabSwipeNavigation.carbonSegmentedControl?.imageSelectedColor = .clear
            
            self.documentCounterView = UIView(frame: CGRect(x: (self.view.bounds.width) - 23, y: 15, width: 7, height: 7))
            self.documentCounterView.backgroundColor = .red
            self.documentCounterView.layer.cornerRadius = self.documentCounterView.frame.height / 2
            self.documentCounterView.isHidden = true
            carbonTabSwipeNavigation.carbonSegmentedControl?.addSubview(self.documentCounterView)
            
            let segmentWidth = (self.tabView.frame.width / 3)
            
            let indicator = carbonTabSwipeNavigation.carbonSegmentedControl?.indicator
            let subView = UIView()
            subView.backgroundColor = Theme.getButtonBlueColor()
            subView.roundOnlyTopCorners(radius: 4)
            indicator?.addSubview(subView)
            subView.translatesAutoresizingMaskIntoConstraints = false
            subView.widthAnchor.constraint(equalToConstant: segmentWidth * 0.8).isActive = true
            subView.centerXAnchor.constraint(equalTo: indicator!.centerXAnchor, constant: 0).isActive = true
            subView.topAnchor.constraint(equalTo: indicator!.topAnchor, constant: 0).isActive = true
            subView.bottomAnchor.constraint(equalTo: indicator!.bottomAnchor, constant: 0).isActive = true
            carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 0)
            carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 1)
            carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 2)
            carbonTabSwipeNavigation.insert(intoRootViewController: self, andTargetView: self.tabView)
            carbonTabSwipeNavigation.setCurrentTabIndex(UInt(self.selectedTab), withAnimation: true)
            
        }
        
        setupFooterButtons(buttons: [btnRequestDocument, btnCall, btnSms, btnEmail])
        walkthroughView.layer.cornerRadius = walkthroughView.frame.height / 2
        walkthroughView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(walkthroughViewTapped)))
        let swipeDownGesture = UISwipeGestureRecognizer(target: self, action: #selector(walkthroughViewTapped))
        swipeDownGesture.direction = .down
        walkthroughView.addGestureRecognizer(swipeDownGesture)
        
        lblBorrowerName.text = borrowerName
        lblLoanPurpose.text = loanPurpose.uppercased()
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.5) {
            
            if (UserDefaults.standard.bool(forKey: kIsWalkthroughShowed) == false){
                UserDefaults.standard.setValue(true, forKey: kIsWalkthroughShowed)
                self.walkthroughViewTrailingConstraint.constant = -320
                self.walkthroughViewBottomConstraint.constant = 320
                UIView.animate(withDuration: 0.4) {
                    self.view.layoutIfNeeded()
                }
            }
        }
    }
    
    func setupFooterButtons(buttons: [UIButton]){
        for button in buttons{
            button.layer.borderWidth = 1
            button.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            button.roundButtonWithShadow()
        }
    }
    
    @objc func walkthroughViewTapped(){
        self.walkthroughViewTrailingConstraint.constant = -640
        self.walkthroughViewBottomConstraint.constant = 640
        UIView.animate(withDuration: 0.4) {
            self.view.layoutIfNeeded()
        }
        
    }
    
    @objc func hidesNavigationBar(){
        
        DispatchQueue.main.async {
            self.navigationViewTopConstraint.constant = -84
            self.navigationView.isHidden = true
            self.navigationSeperator.isHidden = true
            UIView.animate(withDuration: 0.3) {
                self.view.layoutIfNeeded()
            }
        }
        
    }
    
    @objc func showNavigationBar(){
        
        DispatchQueue.main.async {
            self.navigationViewTopConstraint.constant = 0
            self.navigationView.isHidden = false
            self.navigationSeperator.isHidden = false
            UIView.animate(withDuration: 0.3) {
                self.view.layoutIfNeeded()
            }
        }
        
    }
    
    @objc func showRequestDocumentFooterButton(){
        btnRequestDocument.isHidden = false
    }
    
    @objc func hideRequestDocumentFooterButton(){
        btnRequestDocument.isHidden = true
    }
    
    @objc func showDocumentTab(){
        selectedTab = 2
        setupHeaderAndFooter()
    }
    
    private func createEmailUrl(to: String, subject: String, body: String) -> URL? {
        let subjectEncoded = subject.addingPercentEncoding(withAllowedCharacters: .urlQueryAllowed)!
        let bodyEncoded = body.addingPercentEncoding(withAllowedCharacters: .urlQueryAllowed)!
        
        let gmailUrl = URL(string: "googlegmail://co?to=\(to)&subject=\(subjectEncoded)&body=\(bodyEncoded)")
        let outlookUrl = URL(string: "ms-outlook://compose?to=\(to)&subject=\(subjectEncoded)")
        let yahooMail = URL(string: "ymail://mail/compose?to=\(to)&subject=\(subjectEncoded)&body=\(bodyEncoded)")
        let sparkUrl = URL(string: "readdle-spark://compose?recipient=\(to)&subject=\(subjectEncoded)&body=\(bodyEncoded)")
        let defaultUrl = URL(string: "mailto:\(to)?subject=\(subjectEncoded)&body=\(bodyEncoded)")
        
        if let gmailUrl = gmailUrl, UIApplication.shared.canOpenURL(gmailUrl) {
            return gmailUrl
        } else if let outlookUrl = outlookUrl, UIApplication.shared.canOpenURL(outlookUrl) {
            return outlookUrl
        } else if let yahooMail = yahooMail, UIApplication.shared.canOpenURL(yahooMail) {
            return yahooMail
        } else if let sparkUrl = sparkUrl, UIApplication.shared.canOpenURL(sparkUrl) {
            return sparkUrl
        }
        
        return defaultUrl
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        if (isAfterCreateNewApplication){
            self.goToDashboard()
        }
        else{
            self.dismissVC()
        }
    }
    
    @IBAction func btnOptionsTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnAddPersonTapped(_ sender: UIButton) {
        let vc = Utility.getAssignLoanOfficerPopupVC()
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnNewDocumentTapped(_ sender: UIButton) {
        let vc = Utility.getRequestDocumentVC()
        let navVC = UINavigationController(rootViewController: vc)
        navVC.modalPresentationStyle = .fullScreen
        navVC.navigationBar.isHidden = true
        self.presentVC(vc: navVC)
    }
    
    @IBAction func btnCallTapped(_ sender: UIButton) {
        if let url = URL(string: "tel://\(phoneNumber))") {
            UIApplication.shared.open(url, options: [:], completionHandler: nil)
         }
    }
    
    @IBAction func btnSmsTapped(_ sender: UIButton) {
        let sms: String = "sms:\(phoneNumber)&body=Colaba"
        if let strURL = sms.addingPercentEncoding(withAllowedCharacters: .urlQueryAllowed){
            if let smsURL = URL(string: strURL){
                UIApplication.shared.open(smsURL, options: [:], completionHandler: nil)
            }
        }
    }
    
    @IBAction func btnEmailTapped(_ sender: UIButton) {
        
        let recipientEmail = email
        let subject = "Colaba Email"
        let body = "Testing Email"
                    
        // Show default mail composer
        if MFMailComposeViewController.canSendMail() {
            let mail = MFMailComposeViewController()
            mail.mailComposeDelegate = self
            mail.setToRecipients([recipientEmail])
            mail.setSubject(subject)
            mail.setMessageBody(body, isHTML: false)
            
            present(mail, animated: true)
        
        // Show third party email composer if default Mail app is not present
        } else if let emailUrl = createEmailUrl(to: recipientEmail, subject: subject, body: body) {
            UIApplication.shared.open(emailUrl)
        }
    }
    
    //MARK:- API's
    
    func getUnreadDocuments(){
        
        let extraData = "loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getLoanDocuments, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                if (status == .success){
                    
                    if (result.arrayValue.count == 0){
                        self.documentCounterView.isHidden = true
                        return
                    }
                    var documentFilesArray: [DocumentFileModel] = []
                    let documentArray = result.arrayValue
                    for document in documentArray{
                        let documentModel = LoanDocumentModel()
                        documentModel.updateModelWithJSON(json: document)
                        documentFilesArray = documentFilesArray + documentModel.files
                    }
                    if documentFilesArray.filter({$0.isRead == false}).count > 0{
                        self.documentCounterView.isHidden = false
                    }
                    else{
                        self.documentCounterView.isHidden = true
                    }
                }
                else{
                    self.documentCounterView.isHidden = true
                }
            }
            
        }
    }
                
}

extension LoanDetailViewController: CarbonTabSwipeNavigationDelegate{
    
    func carbonTabSwipeNavigation(_ carbonTabSwipeNavigation: CarbonTabSwipeNavigation, viewControllerAt index: UInt) -> UIViewController {
        
        if (index == 0){
            let vc = Utility.getOverviewVC()
            vc.loanApplicationId = self.loanApplicationId
            vc.delegate = self
            return vc
        }
        else if (index == 1){
            let vc = Utility.getApplicationVC()
            vc.loanApplicationId = self.loanApplicationId
            return vc
        }
        else{
            let vc = Utility.getDocumentsVC()
            vc.loanApplicationId = self.loanApplicationId
            return vc
        }
        
    }
    
    func carbonTabSwipeNavigation(_ carbonTabSwipeNavigation: CarbonTabSwipeNavigation, didMoveAt index: UInt) {
        if (index == 0){
            self.tabView.backgroundColor = .white
        }
        else{
            self.tabView.backgroundColor = Theme.getDashboardBackgroundColor()
        }
    }
    
}

extension LoanDetailViewController: OverviewViewControllerDelegate{
    func getLoanDetailForMainPage(loanPurpose: String, email: String, phoneNumber: String) {
        lblLoanPurpose.text = loanPurpose.uppercased()
        self.email = email
        self.phoneNumber = phoneNumber
    }
}

extension LoanDetailViewController: MFMailComposeViewControllerDelegate{
    
    func mailComposeController(_ controller: MFMailComposeViewController, didFinishWith result: MFMailComposeResult, error: Error?) {
        controller.dismiss(animated: true, completion: nil)
    }
}


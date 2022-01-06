//
//  InvitePrimaryBorrowerViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/12/2021.
//

import UIKit

class InvitePrimaryBorrowerViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var emailBodyContainer: UIView!
    @IBOutlet weak var textviewBody: UITextView!
    @IBOutlet weak var btnSendInvite: UIButton!
    
    var loanApplicationId = 0
    var borrowerId = 0
    var isForResend = false
    
    var emailSubject = ""
    var emailBody = ""
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        getEmailTemplate()
    }
    
    //MARK:- Methods and Actions
    
    func setupViews(){
        emailBodyContainer.layer.cornerRadius = 6
        emailBodyContainer.layer.borderWidth = 1
        emailBodyContainer.layer.borderColor = Theme.getButtonGreyColor().cgColor
        textviewBody.delegate = self
        btnSendInvite.layer.cornerRadius = 5
    }
    
    func setEmailTemplate(){
        
        textviewBody.attributedText = emailBody.htmlToAttributedString
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    func setScreenHeight(){
        let emailBodyHeight = textviewBody.contentSize.height
        self.mainViewHeightConstraint.constant = emailBodyHeight + 100
        self.view.layoutSubviews()
//        UIView.animate(withDuration: 0.0) {
//            self.view.layoutIfNeeded()
//        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.dismissVC()
    }
    
    @IBAction func btnSendInviteTapped(_ sender: UIButton){
        self.dismissVC()
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowInviteSendPopup), object: nil)
    }
    
    //MARK:- API's
    
    func getEmailTemplate(){
        
        let extraData = "borrowerId=\(borrowerId)&loanapplicationid=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getBorrowerInvitationEmailTemplate, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                if (status == .success){
                    self.emailSubject = result["emailSubject"].stringValue
                    self.emailBody = result["emailBody"].stringValue
                    self.setEmailTemplate()
                }
                else{
                    self.showPopup(message: "No details found", popupState: .error, popupDuration: .custom(2)) { reason in
                        self.dismissVC()
                    }
                }
            }
            
        }
        
    }

}

extension InvitePrimaryBorrowerViewController: UITextViewDelegate{
    
    func textViewDidEndEditing(_ textView: UITextView) {
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.3) {
            self.setScreenHeight()
        }
    }
    
}

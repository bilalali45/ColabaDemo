//
//  PiplineMoreViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 15/06/2021.
//

import UIKit
import MessageUI

class PipelineMoreViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var lblMoreUsers: UILabel!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var emailView: UIView!
    @IBOutlet weak var callView: UIView!
    @IBOutlet weak var messageView: UIView!
    @IBOutlet weak var applicationView: UIView!
    @IBOutlet weak var documentsView: UIView!
    @IBOutlet weak var conversationsView: UIView!
    @IBOutlet weak var archiveView: UIView!
    @IBOutlet weak var bottomViewHeightConstraint: NSLayoutConstraint!
    
    var userFullName = ""
    var coBorrowers = 0
    var phoneNumber = ""
    var email = ""
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        mainView.roundOnlyTopCorners(radius: 20)
        emailView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(emailViewTapped)))
        callView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(callViewTapped)))
        messageView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(messageViewTapped)))
        applicationView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(applicationViewTapped)))
        documentsView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(documentsViewTapped)))
        conversationsView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(conversationViewTapped)))
        archiveView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(archiveViewTapped)))
        self.view.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(backgroundTapped)))
        let swipeDownGesture = UISwipeGestureRecognizer(target: self, action: #selector(backgroundTapped))
        swipeDownGesture.direction = .down
        self.view.addGestureRecognizer(swipeDownGesture)
        
        lblUsername.text = userFullName
        lblMoreUsers.text = coBorrowers == 0 ? "" : "+\(coBorrowers)"
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        
        self.bottomViewHeightConstraint.constant = 0
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
        UIView.animate(withDuration: 0.30, animations: {
            self.view.backgroundColor = UIColor(red: 0, green: 0, blue: 0, alpha: 0.08)
            
        }, completion: nil)
        
    }
    
    
    
    //MARK:- Methods and Actions
    
    func dismissPopup(){
        
        self.bottomViewHeightConstraint.constant = -340
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
        UIView.animate(withDuration: 0.30) {
            self.view.backgroundColor = .clear
        } completion: { _ in
            self.dismissVC()
        }
        
    }
    
    @objc func emailViewTapped(){
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
    
    @objc func callViewTapped(){
        if let url = URL(string: "tel://\(phoneNumber))") {
            UIApplication.shared.open(url, options: [:], completionHandler: nil)
         }
    }
    
    @objc func messageViewTapped(){
        let sms: String = "sms:\(phoneNumber)&body=Colaba"
        if let strURL = sms.addingPercentEncoding(withAllowedCharacters: .urlQueryAllowed){
            if let smsURL = URL(string: strURL){
                UIApplication.shared.open(smsURL, options: [:], completionHandler: nil)
            }
        }
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
    
    @objc func applicationViewTapped(){
        dismissPopup()
    }
    
    @objc func documentsViewTapped(){
        dismissPopup()
    }
    
    @objc func conversationViewTapped(){
        dismissPopup()
    }
    
    @objc func archiveViewTapped(){
        dismissPopup()
    }
    
    @objc func backgroundTapped(){
        dismissPopup()
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        dismissPopup()
    }
    
}

extension PipelineMoreViewController: MFMailComposeViewControllerDelegate{
    
    func mailComposeController(_ controller: MFMailComposeViewController, didFinishWith result: MFMailComposeResult, error: Error?) {
        controller.dismiss(animated: true, completion: nil)
    }
}

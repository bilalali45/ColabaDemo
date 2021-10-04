//
//  ForgotPasswordViewController.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit
import Material

class ForgotPasswordViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var emailView: UIView!
    @IBOutlet weak var txtFieldEmail: ColabaTextField!
    @IBOutlet weak var btnReset: UIButton!
    
    //MARK:- View Controller Life Cycle
    override func viewDidLoad() {
        super.viewDidLoad()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.10) {
            self.setupViews()
        }
    }
    
    //MARK:- Actions and Methods
    
    func setupViews(){
        emailView.layer.cornerRadius = 8
        emailView.addShadow()
        btnReset.layer.cornerRadius = 5
        setTextFields()
    }
    
    func setTextFields() {
        ///Email Text Field
        txtFieldEmail.setTextField(placeholder: "Email",controller: self, validationType: .email, keyboardType: .emailAddress)
        txtFieldEmail.setIsValidateOnEndEditing(validate: false)
    }
    
    func forgotPasswordWithRequest(email: String){
        
        let params = ["Email": email]
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeAPI(type: .forgotPassword, method: .post, params: params) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                   // self.showPopup(message: message == "" ? "Email sent successfully" : message, popupState: .success, popupDuration: .custom(3)) { (reason) in
                        let vc = Utility.getResetPasswordSuccessfullVC()
                        self.pushToVC(vc: vc)
                   // }
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(10), completionHandler: nil)
                }
            }
            
        }
    }
    
    @IBAction func btnBackPressed(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnResetPasswordTapped(_ sender: UIButton) {
        if validate() {
            self.forgotPasswordWithRequest(email: txtFieldEmail.text!)
        }
    }
    
    func validate() -> Bool {
        if !txtFieldEmail.validate() {
            return false
        }
        return true
    }
}

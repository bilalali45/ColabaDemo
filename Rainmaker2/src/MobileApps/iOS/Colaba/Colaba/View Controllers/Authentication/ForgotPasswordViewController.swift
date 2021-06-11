//
//  ForgotPasswordViewController.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit

class ForgotPasswordViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var emailView: UIView!
    @IBOutlet weak var txtFieldEmail: UITextField!
    @IBOutlet weak var emailSeparator: UIView!
    @IBOutlet weak var lblEmailError: UILabel!
    @IBOutlet weak var btnReset: UIButton!
    
    var validation: Validation
    
    //MARK:- View Controller Life Cycle
    
    init(validation: Validation) {
        self.validation = validation
        super.init(nibName: nil, bundle: nil)
    }
    
    required init?(coder: NSCoder) {
        self.validation = Validation()
        super.init(coder: coder)
    }
    
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
        txtFieldEmail.delegate = self
    }
    
    func forgotPasswordWithRequest(email: String){
        
        let params = ["Email": email]
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeAPI(type: .forgotPassword, method: .post, params: params) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    self.showPopup(message: message == "" ? "Email sent successfully" : message, popupState: .success, popupDuration: .custom(1.5)) { (reason) in
                        let vc = Utility.getResetPasswordSuccessfullVC()
                        self.pushToVC(vc: vc)
                    }
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
        
        do{
            let email = try validation.validateEmail(txtFieldEmail.text)
            
            DispatchQueue.main.async {
                self.lblEmailError.isHidden = true
                self.emailSeparator.backgroundColor = Theme.getSeparatorNormalColor()
                self.forgotPasswordWithRequest(email: email)
            }
            
        }
        catch{
            self.lblEmailError.text = error.localizedDescription
            self.lblEmailError.isHidden = false
            self.emailSeparator.backgroundColor = Theme.getSeparatorErrorColor()
        }
        
    }
}

extension ForgotPasswordViewController: UITextFieldDelegate{
    
//    func textFieldDidEndEditing(_ textField: UITextField) {
//        do{
//            let email = try validation.validateEmail(txtFieldEmail.text)
//            self.lblEmailError.isHidden = true
//            self.emailSeparator.backgroundColor = Theme.getSeparatorNormalColor()
//        }
//        catch{
//            self.lblEmailError.text = error.localizedDescription
//            self.lblEmailError.isHidden = false
//            self.emailSeparator.backgroundColor = Theme.getSeparatorErrorColor()
//        }
//    }
    
}

//
//  LoginViewController.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit
import RealmSwift
import SwiftyJSON
import Material

class LoginViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var loginView: UIView!
    @IBOutlet weak var txtFieldEmail: TextField!
    @IBOutlet weak var emailSeparator: UIView!
    @IBOutlet weak var lblEmailError: UILabel!
    @IBOutlet weak var txtFieldPassword: TextField!
    @IBOutlet weak var passwordSeparator: UIView!
    @IBOutlet weak var lblPasswordError: UILabel!
    @IBOutlet weak var btnEye: UIButton!
    @IBOutlet weak var faceIdSwitch: UISwitch!
    @IBOutlet weak var btnLogin: UIButton!
    
    var shouldShowPassword = false
    var isAPIInProgress = false
    private let validation: Validation
    
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
        if (isAppOpenFromBackground){
            txtFieldEmail.text = Utility.getUserEmail()
            txtFieldEmail.isUserInteractionEnabled = false
        }
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.10) {
            self.setupViews()
        }
    }
    
    //MARK:- Methods and Actions
    
    func setupViews(){
        loginView.layer.cornerRadius = 8
        loginView.addShadow()
        btnLogin.layer.cornerRadius = 5
        txtFieldEmail.delegate = self
        txtFieldPassword.delegate = self
        
        setMaterialTextFieldsAndViews(textfields: [txtFieldEmail, txtFieldPassword])
    }
    
    func setMaterialTextFieldsAndViews(textfields: [TextField]){
        for textfield in textfields{
            textfield.dividerActiveColor = Theme.getButtonBlueColor()
            textfield.dividerColor = Theme.getSeparatorNormalColor()
            textfield.placeholderActiveColor = Theme.getAppGreyColor()
            textfield.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
            textfield.detailLabel.font = Theme.getRubikRegularFont(size: 12)
            textfield.detailColor = .red
            textfield.detailVerticalOffset = 4
            textfield.placeholderVerticalOffset = 8
            textfield.textColor = Theme.getAppBlackColor()
        }
        if (isAppOpenFromBackground){
            txtFieldEmail.textColor = Theme.getAppGreyColor()
        }
    }
    
    func setPlaceholderLabelColorAfterTextFilled(selectedTextField: UITextField, allTextFields: [TextField]){
        for allTextField in allTextFields{
            if (allTextField == selectedTextField){
                if (allTextField.text == ""){
                    allTextField.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                }
                else{
                    allTextField.placeholderLabel.textColor = Theme.getAppGreyColor()
                }
            }
        }
    }
    
    func completeLoginWithBiometric(){
            
        UserDefaults.standard.set(isBiometricAllow ? kYes : kNo, forKey: kIsUserRegisteredWithBiometric)
        self.goToDashboard()
    }
    
    @IBAction func btnEyeTapped(_ sender: UIButton) {
        shouldShowPassword = !shouldShowPassword
        btnEye.setImage(UIImage(named: shouldShowPassword ? "hide" : "eyeIcon"), for: .normal)
        txtFieldPassword.isSecureTextEntry = !shouldShowPassword
    }
    
    @IBAction func faceIdSwitchChanged(_ sender: UISwitch) {
        
        if (sender.isOn){
            if (Utility.checkDeviceAuthType() == kDeviceNotRegistered){
                self.faceIdSwitch.setOn(false, animated: true)
                isBiometricAllow = false
                self.showPopup(message: kDeviceNotRegistered, popupState: .info, popupDuration: .custom(5), completionHandler: nil)
            }
            else{
                isBiometricAllow = true
            }
        }
        else{
            isBiometricAllow = false
        }
    }
    
    @IBAction func btnForgotPasswordTapped(_ sender: UIButton) {
        let vc = Utility.getForgotPasswordVC()
        self.pushToVC(vc: vc)
    }
    
    @IBAction func btnLoginTapped(_ sender: UIButton) {
        
        do{
            let email = try validation.validateEmail(txtFieldEmail.text)
            let password = try validation.validatePassword(txtFieldPassword.text)
            DispatchQueue.main.async {
//                self.lblEmailError.isHidden = true
//                self.emailSeparator.backgroundColor = Theme.getSeparatorNormalColor()
//                self.lblPasswordError.isHidden = true
//                self.passwordSeparator.backgroundColor = Theme.getSeparatorNormalColor()
                self.txtFieldEmail.dividerColor = Theme.getSeparatorNormalColor()
                self.txtFieldEmail.detail = ""
                self.txtFieldPassword.dividerColor = Theme.getSeparatorNormalColor()
                self.txtFieldPassword.detail = ""
                
                if (!self.isAPIInProgress){
                    self.loginUserWithRequest(email: email, password: password)
                }
                
            }
            
        }
        catch{
            if (error.localizedDescription == ValidationError.invalidEmail.localizedDescription || error.localizedDescription == ValidationError.noEmail.localizedDescription){
                
//                self.lblEmailError.text = error.localizedDescription
//                self.lblEmailError.isHidden = false
//                self.emailSeparator.backgroundColor = Theme.getSeparatorErrorColor()
                self.txtFieldEmail.dividerColor = .red
                self.txtFieldEmail.detail = error.localizedDescription
                self.txtFieldPassword.dividerColor = Theme.getSeparatorNormalColor()
                self.txtFieldPassword.detail = ""
//                self.lblPasswordError.isHidden = true
//                self.passwordSeparator.backgroundColor = Theme.getSeparatorNormalColor()
            }
            else{
//                self.lblPasswordError.text = error.localizedDescription
//                self.lblPasswordError.isHidden = false
//                self.passwordSeparator.backgroundColor = Theme.getSeparatorErrorColor()
                self.txtFieldPassword.dividerColor = .red
                self.txtFieldPassword.detail = error.localizedDescription
                self.txtFieldEmail.dividerColor = Theme.getSeparatorNormalColor()
                self.txtFieldEmail.detail = ""
//                self.lblEmailError.isHidden = true
//                self.emailSeparator.backgroundColor = Theme.getSeparatorNormalColor()
            }
        }
        
    }
    
    //MARK:- API's
    
    func loginUserWithRequest(email: String, password: String){
        
        isAPIInProgress = true
        let params = ["Email": email,
                      "Password": password]
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeAPI(type: .login, method: .post, params: params) { status, result, message in
            
            DispatchQueue.main.async {
                
                if (status == .success){
                    
                    let realm = try! Realm()
                    realm.beginWrite()
                    realm.deleteAll()
                    let model = UserModel()
                    model.updateModelWithJSON(json: result["data"])
                    realm.add(model)
                    try! realm.commitWrite()
                    
                    if let user = UserModel.getCurrentUser(){
                        if user.tokenTypeName == "AccessToken"{
                            
                            self.isAPIInProgress = false
                            Utility.showOrHideLoader(shouldShow: false)
                            self.completeLoginWithBiometric()
                        }
                        else{
                            // MCU Configuration API
                            self.getMCUConfigurationWithRequest()
                        }
                    }

                }
                else{
                    self.isAPIInProgress = false
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5), completionHandler: nil)
                }
            }
            
        }
    }
    
    func getMCUConfigurationWithRequest(){
        
        APIRouter.sharedInstance.executeAPI(type: .getMCUConfiguration, method: .get, params: nil, extraHeaderKey: UserModel.getCurrentUser()!.tokenTypeName, extraHeaderValue: UserModel.getCurrentUser()!.token) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    
                    if (result["data"]["tenantTwoFaSetting"].intValue == 1){ // 2FA is required for all
                        // Create 2FA API. don't show skip button
                        shouldShowSkipButton = false
                        self.send2FAWithRequest()
                    }
                    else if (result["data"]["tenantTwoFaSetting"].intValue == 3){ // 2FA is depend on User Prefrences
                        if (result["data"]["userTwoFaSetting"] == JSON.null){ // User not set 2FA setting yet..
                            //Create 2FA API and show skip button
                            shouldShowSkipButton = true
                            self.send2FAWithRequest()
                        }
                        else if (result["data"]["userTwoFaSetting"].boolValue == true){ // User allow 2FA.
                            //Create 2FA API and don't show skip button
                            shouldShowSkipButton = false
                            self.send2FAWithRequest()
                        }
                        else{ // User don't allow 2FA
                            Utility.showOrHideLoader(shouldShow: false)
                            self.goToDashboard()
                            //We will get access token and then go to dashboard
                        }
                    }
                    
                }
                else{
                    self.isAPIInProgress = false
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5), completionHandler: nil)
                }
            }
            
        }
    }
    
    func get2FASettings(){
        
        APIRouter.sharedInstance.executeAPI(type: .get2FASettings, method: .get, params: nil, extraHeaderKey: UserModel.getCurrentUser()!.tokenTypeName, extraHeaderValue: UserModel.getCurrentUser()!.token) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success && result["code"].stringValue == "200"){
                    let codeLimit = result["data"]["maxTwoFaSendAllowed"].intValue
                    totalCodeLimit = codeLimit
                }
                else{
                    totalCodeLimit = 5
                }
            }
            
        }
        
    }
    
    func send2FAWithRequest(){
        
        APIRouter.sharedInstance.executeAPI(type: .send2FA, method: .post, params: nil, extraHeaderKey: UserModel.getCurrentUser()!.tokenTypeName, extraHeaderValue: UserModel.getCurrentUser()!.token) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                
                self.get2FASettings()
                self.isAPIInProgress = false
                
                if (status == .success){ // Code is send to phone number. show code screen.
                    // Save response and navigate to code screen.
                    
                    let attemptsCount = result["data"]["attemptsCount"].intValue
                    let phoneNumber = result["data"]["phoneNumber"].stringValue
                    let remainingTimeoutSeconds = result["data"]["twoFaMaxAttemptsCoolTimeInSeconds"].intValue
                    
                    let vc = Utility.getCodeVC()
                    vc.totalAttemptsCount = attemptsCount
                    vc.phoneNumber = phoneNumber
                    vc.resendTotalTime = remainingTimeoutSeconds
                    vc.resendTimeMessage = message
                    self.pushToVC(vc: vc)
                }
                else{
                    if (result["code"].stringValue == "404"){ // Phone number not found. Navigate to phone number screen
                        let vc = Utility.getPhoneNumberVC()
                        self.pushToVC(vc: vc)
                    }
                    else{
                        self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                            
                        }
                    }
                }
                
            }
            
        }
        
    }
}

extension LoginViewController: UIGestureRecognizerDelegate{
    
    func gestureRecognizer(_ gestureRecognizer: UIGestureRecognizer, shouldBeRequiredToFailBy otherGestureRecognizer: UIGestureRecognizer) -> Bool {
        return true
    }
}

extension LoginViewController: UITextFieldDelegate{
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtFieldEmail, txtFieldPassword])
    }
    
//    func textFieldDidEndEditing(_ textField: UITextField) {
//        if (textField == txtFieldEmail){
//            do{
//                let email = try validation.validateEmail(txtFieldEmail.text)
//                self.lblEmailError.isHidden = true
//                self.emailSeparator.backgroundColor = Theme.getSeparatorNormalColor()
//            }
//            catch{
//                self.lblEmailError.text = error.localizedDescription
//                self.lblEmailError.isHidden = false
//                self.emailSeparator.backgroundColor = Theme.getSeparatorErrorColor()
//            }
//        }
//        else{
//            do{
//                let password = try validation.validatePassword(txtFieldPassword.text)
//                self.lblPasswordError.isHidden = true
//                self.passwordSeparator.backgroundColor = Theme.getSeparatorNormalColor()
//            }
//            catch{
//                self.lblPasswordError.text = error.localizedDescription
//                self.lblPasswordError.isHidden = false
//                self.passwordSeparator.backgroundColor = Theme.getSeparatorErrorColor()
//                
//            }
//        }
//            
//    }
    
}

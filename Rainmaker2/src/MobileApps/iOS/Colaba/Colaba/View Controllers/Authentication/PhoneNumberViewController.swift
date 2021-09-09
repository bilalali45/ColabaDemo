//
//  PhoneNumberViewController.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit
import RealmSwift
import Material

class PhoneNumberViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var phoneView: UIView!
    @IBOutlet weak var txtFieldPhone: ColabaTextField!
    @IBOutlet weak var btnContinue: UIButton!
    @IBOutlet weak var btnSkipThisStep: UIButton!
    
    //MARK:- View Controller Life Cycle
    override func viewDidLoad() {
        super.viewDidLoad()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.10) {
            self.setupViews()
        }
    }
    
    //MARK:- Methods and Actions
    
    func setupViews(){
        phoneView.layer.cornerRadius = 8
        phoneView.addShadow()
        btnContinue.layer.cornerRadius = 5
        btnSkipThisStep.isHidden = !shouldShowSkipButton
        setTextFields()
    }
    
    func setTextFields() {
        ///Mobile Text Field
        txtFieldPhone.setTextField(placeholder: "Mobile Number")
        txtFieldPhone.setDelegates(controller: self)
        txtFieldPhone.setValidation(validationType: .phoneNumber)
        txtFieldPhone.setTextField(keyboardType: .phonePad)
        txtFieldPhone.setIsValidateOnEndEditing(validate: false)
    }
    
    func completeLoginWithBiometric(){
            
        UserDefaults.standard.set(isBiometricAllow ? kYes : kNo, forKey: kIsUserRegisteredWithBiometric)
        self.goToDashboard()
    }
    
    @IBAction func btnBackPressed(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnContinueTapped(_ sender: UIButton) {
        
        if validate() {
            self.send2FAtoPhoneNumberWithRequest(phoneNumber: cleanString(string: txtFieldPhone.text!, replaceCharacters: ["(",")"," ","-"], replaceWith: ""))
        }
    }

    func validate() -> Bool {
        if !txtFieldPhone.validate() {
            return false
        }
        return true
    }
    
    @IBAction func btnSkipTapped(_ sender: UIButton) {
        skip2FAWithRequest()
    }
    
    //MARK:- API's
    
    func send2FAtoPhoneNumberWithRequest(phoneNumber: String){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeAPI(type: .send2FAToPhoneNo, method: .post, params: nil, extraHeaderKey: UserModel.getCurrentUser()!.tokenTypeName, extraHeaderValue: UserModel.getCurrentUser()!.token, extraData: phoneNumber) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){ // Save response and navigate to code screen
                    
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
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                        
                    }
                }
                
            }
            
        }
        
    }
    
    func skip2FAWithRequest(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeAPI(type: .skip2FA, method: .post, params: nil, extraHeaderKey: UserModel.getCurrentUser()!.tokenTypeName, extraHeaderValue: UserModel.getCurrentUser()!.token) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){ // Will get access token. Navigate to dashboard or biometric authentication
                    
                    let realm = try! Realm()
                    realm.beginWrite()
                    realm.deleteAll()
                    let model = UserModel()
                    model.updateModelWithJSON(json: result["data"])
                    realm.add(model)
                    try! realm.commitWrite()

                    self.completeLoginWithBiometric()
                    
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
}

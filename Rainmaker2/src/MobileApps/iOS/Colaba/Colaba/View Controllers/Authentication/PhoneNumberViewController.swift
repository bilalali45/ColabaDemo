//
//  PhoneNumberViewController.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit
import RealmSwift

class PhoneNumberViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var phoneView: UIView!
    @IBOutlet weak var txtFieldPhone: UITextField!
    @IBOutlet weak var phoneSeparator: UIView!
    @IBOutlet weak var lblPhoneError: UILabel!
    @IBOutlet weak var btnContinue: UIButton!
    @IBOutlet weak var btnSkipThisStep: UIButton!
    
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
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.10) {
            self.setupViews()
        }
    }
    
    //MARK:- Methods and Actions
    
    func setupViews(){
        phoneView.layer.cornerRadius = 8
        phoneView.addShadow()
        btnContinue.layer.cornerRadius = 5
        txtFieldPhone.delegate = self
        btnSkipThisStep.isHidden = !shouldShowSkipButton
    }
    
    func completeLoginWithBiometric(){
            
        UserDefaults.standard.set(isBiometricAllow ? kYes : kNo, forKey: kIsUserRegisteredWithBiometric)
        self.goToDashboard()
    }
    
    @IBAction func btnBackPressed(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnContinueTapped(_ sender: UIButton) {
        
        do{
            let phoneNumber = try validation.validatePhoneNumber(txtFieldPhone.text)
            
            DispatchQueue.main.async {
                self.lblPhoneError.isHidden = true
                self.phoneSeparator.backgroundColor = Theme.getSeparatorNormalColor()
                self.send2FAtoPhoneNumberWithRequest(phoneNumber: phoneNumber.replacingOccurrences(of: "(", with: "").replacingOccurrences(of: ")", with: "").replacingOccurrences(of: " ", with: "").replacingOccurrences(of: "-", with: ""))
            }
            
        }
        catch{
            self.lblPhoneError.text = error.localizedDescription
            self.lblPhoneError.isHidden = false
            self.phoneSeparator.backgroundColor = Theme.getSeparatorErrorColor()
        }
        
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

extension PhoneNumberViewController: UITextFieldDelegate{
    
//    func textFieldDidEndEditing(_ textField: UITextField) {
//        do{
//            let phoneNumber = try validation.validatePhoneNumber(txtFieldPhone.text)
//            self.lblPhoneError.isHidden = true
//            self.phoneSeparator.backgroundColor = Theme.getSeparatorNormalColor()
//        }
//        catch{
//            self.lblPhoneError.text = error.localizedDescription
//            self.lblPhoneError.isHidden = false
//            self.phoneSeparator.backgroundColor = Theme.getSeparatorErrorColor()
//        }
//    }
    
    func textField(_ textField: UITextField, shouldChangeCharactersIn range: NSRange, replacementString string: String) -> Bool {
        
        guard let text = textField.text else { return false }
        let newString = (text as NSString).replacingCharacters(in: range, with: string)
        textField.text = self.formatPhoneNumber(with: "(XXX) XXX-XXXX", phone: newString)
        do{
            let phoneNumber = try validation.validatePhoneNumber(txtFieldPhone.text)
            self.lblPhoneError.isHidden = true
            self.phoneSeparator.backgroundColor = Theme.getSeparatorNormalColor()
        }
        catch{
//            self.lblPhoneError.text = error.localizedDescription
//            self.lblPhoneError.isHidden = false
//            self.phoneSeparator.backgroundColor = Theme.getSeparatorErrorColor()
        }
        return false
    }
    
}

//
//  CodeViewController.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit
import RealmSwift

class CodeViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var codeView: UIView!
    @IBOutlet weak var codeViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblPhoneNumber: UILabel!
    @IBOutlet weak var txtFieldCode: UITextField!
    @IBOutlet weak var checkIcon: UIImageView!
    @IBOutlet weak var btnCheckTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblDescription: UILabel!
    @IBOutlet weak var lblDescriptionTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var btnResendCode: UIButton!
    @IBOutlet weak var btnCheckBox: UIButton!
    @IBOutlet weak var lblDontAsk: UILabel!
    @IBOutlet weak var btnVerify: UIButton!
    @IBOutlet weak var timerView: UIView!
    @IBOutlet weak var lblMins: UILabel!
    @IBOutlet weak var lblSecs: UILabel!
    
    var isCheck = false
    var codeLimit = totalCodeLimit
    var resendTimer: Timer?
    var resendTotalTime = 0 // Timer Second.... Will get from API
    
    var totalAttemptsCount = 0 // Will get from API
    var phoneNumber = ""
    var resendTimeMessage = ""
    
    //MARK:- View Controller Life Cycle
    override func viewDidLoad() {
        super.viewDidLoad()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.10) {
            self.setupViews()
        }
    }
   
    //MARK:- Methods and Actions
    
    func setupViews(){
        codeView.layer.cornerRadius = 8
        codeView.addShadow()
        btnVerify.layer.cornerRadius = 5
        txtFieldCode.addTarget(self, action: #selector(textFieldCodeChanged), for: .editingChanged)
        txtFieldCode.delegate = self
        btnVerify.isEnabled = false
        let last4Digits = String(phoneNumber.suffix(4))
        lblPhoneNumber.text = "Enter the code sent to (***) ***-\(last4Digits)"
        codeLimit = totalCodeLimit - totalAttemptsCount
        self.btnResendCode.setTitle("Resend code (\(self.codeLimit) left)", for: .normal)
        if (self.resendTotalTime > 0){
            self.codeLimit = 0
            self.changeUIAfterResendCode(message: resendTimeMessage)
        }
    }
    
    @objc func textFieldCodeChanged(){
        checkIcon.isHidden = txtFieldCode.text!.count != 6
        btnVerify.backgroundColor = txtFieldCode.text!.count != 6 ? Theme.getButtonGreyColor() : Theme.getButtonBlueColor()
        btnVerify.setTitleColor(txtFieldCode.text!.count != 6 ? Theme.getButtonGreyTextColor() : .white, for: .normal)
        btnVerify.isEnabled = txtFieldCode.text!.count == 6
    }
    
    func changeUIAfterResendCode(message: String){
        codeLimit = codeLimit - totalAttemptsCount
        if (codeLimit < 1){
            
            self.lblDescription.text = message == "" ? "Max resend attempts reached. Please try again after few minutes" : message
            self.btnResendCode.isHidden = true
            self.codeLimit = totalCodeLimit - self.totalAttemptsCount
            self.codeViewHeightConstraint.constant = 356
            self.btnCheckTopConstraint.constant = 50
            self.lblDescriptionTopConstraint.constant = 20
            self.timerView.isHidden = false
            startTimer(resendTime: self.resendTotalTime)
        }
        else{
            
            //self.btnResendCode.isUserInteractionEnabled = false
           // self.btnResendCode.setTitleColor(.lightGray, for: .normal)
            self.btnResendCode.isHidden = false
            self.btnResendCode.setTitle("Resend code (\(self.codeLimit) left)", for: .normal)
            self.lblDescription.text = "Didn't receive the code?"
            self.codeViewHeightConstraint.constant = 343
            self.btnCheckTopConstraint.constant = 30
            self.lblDescriptionTopConstraint.constant = 30
            self.timerView.isHidden = true
//            DispatchQueue.main.asyncAfter(deadline: .now() + 3) {
//                self.btnResendCode.isUserInteractionEnabled = true
//                self.btnResendCode.setTitleColor(Theme.getButtonBlueColor(), for: .normal)
//            }
        }
        UIView.animate(withDuration: 0.5) {
            self.view.layoutIfNeeded()
            self.codeView.layer.cornerRadius = 5
            self.codeView.addShadow()
        }
        
    }
    
    func startTimer(resendTime: Int) {
        self.resendTotalTime = resendTime
        self.resendTimer = Timer.scheduledTimer(timeInterval: 1.0, target: self, selector: #selector(updateTimer), userInfo: nil, repeats: true)
    }
    
    @objc func updateTimer() {
        
        self.lblMins.text = self.timeFormatted(self.resendTotalTime).mins
        self.lblSecs.text = self.timeFormatted(self.resendTotalTime).secs
        
        if resendTotalTime != 0 {
            resendTotalTime -= 1
        }
        else {
            if let timer = self.resendTimer {
                timer.invalidate()
                self.resendTimer = nil
                
                self.codeLimit = totalCodeLimit - totalAttemptsCount
                self.btnResendCode.isHidden = false
                self.btnResendCode.setTitle("Resend code (\(self.codeLimit) left)", for: .normal)
                self.btnResendCode.setTitleColor(Theme.getButtonBlueColor(), for: .normal)
                self.lblDescription.text = "Didn't receive the code?"
                self.codeViewHeightConstraint.constant = 343
                self.btnCheckTopConstraint.constant = 30
                self.lblDescriptionTopConstraint.constant = 30
                self.timerView.isHidden = true
                self.lblMins.text = ""
                self.lblSecs.text = ""
                UserDefaults.standard.setValue(0, forKey: kResendEnableTimeStamp)
                
                UIView.animate(withDuration: 0.5) {
                    self.view.layoutIfNeeded()
                    self.codeView.layer.cornerRadius = 5
                    self.codeView.addShadow()
                }
            }
        }
    }
        
    func timeFormatted(_ totalSeconds: Int) -> (mins: String, secs: String) {
            let seconds: Int = totalSeconds % 60
            let minutes: Int = (totalSeconds / 60) % 60
            return (String(format: "%02d", minutes), String(format: "%02d", seconds))
    }
    
    func completeLoginWithBiometric(){
            
        UserDefaults.standard.set(isBiometricAllow ? kYes : kNo, forKey: kIsUserRegisteredWithBiometric)
        self.goToDashboard()
    }
    
    @IBAction func btnBackPressed(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnResendTapped(_ sender: UIButton) {
        send2FAtoPhoneNumberWithRequest()
    }
    
    @IBAction func btnCheckBoxTapped(_ sender: UIButton) {
        isCheck = !isCheck
        btnCheckBox.setImage(UIImage(named: isCheck ? "checkbox-check" : "checkbox-uncheck"), for: .normal)
    }
    
    @IBAction func btnVerifyTapped(_ sender: UIButton) {
        verify2FAWithRequest()
    }
    
    //MARK:- API's
    
    func send2FAtoPhoneNumberWithRequest(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeAPI(type: .send2FAToPhoneNo, method: .post, params: nil, extraHeaderKey: UserModel.getCurrentUser()!.tokenTypeName, extraHeaderValue: UserModel.getCurrentUser()!.token, extraData: phoneNumber.replacingOccurrences(of: "+1", with: "")) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    print(result)
                    let attemptsCount = result["data"]["attemptsCount"].intValue
                    let remainingTimeoutSeconds = result["data"]["remainingTimeoutInSeconds"].intValue
                    
                    self.totalAttemptsCount = attemptsCount
                    self.resendTotalTime = remainingTimeoutSeconds
                    
                    self.codeLimit = totalCodeLimit - self.totalAttemptsCount
                    self.btnResendCode.setTitle("Resend code (\(self.codeLimit) left)", for: .normal)
                    
                    if (self.resendTotalTime > 0){
                        self.codeLimit = 0
                        self.changeUIAfterResendCode(message: message)
                    }
                    else{
                        self.showPopup(message: message, popupState: result["code"].stringValue == "200" ? .success : .error, popupDuration: result["code"].stringValue == "200" ? .custom(5) : .custom(10)) { reason in
                            
                        }
                    }
                    
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(10)) { reason in
                        
                    }
                }
                
            }
            
        }
        
    }
    
    func verify2FAWithRequest(){
        
        if let otpCode = Int(txtFieldCode.text!){
            
            Utility.showOrHideLoader(shouldShow: true)
            
            let params = ["PhoneNumber": phoneNumber,
                          "Otp": otpCode] as [String:Any]
            
            APIRouter.sharedInstance.executeAPI(type: .verify2FA, method: .post, params: params, extraHeaderKey: UserModel.getCurrentUser()!.tokenTypeName, extraHeaderValue: UserModel.getCurrentUser()!.token) { status, result, message in
                
                DispatchQueue.main.async {
                    Utility.showOrHideLoader(shouldShow: false)
                    
                    if (status == .success){
                        
                        if (result["code"].stringValue == "200"){
                            
                            let realm = try! Realm()
                            realm.beginWrite()
                            realm.deleteAll()
                            let model = UserModel()
                            model.updateModelWithJSON(json: result["data"])
                            realm.add(model)
                            try! realm.commitWrite()
                            
                            if (self.isCheck){
                                self.dontAskFor2FAWithRequest()
                            }
                            self.completeLoginWithBiometric()
                    
                        }
                        else{
                            self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                                
                            }
                        }
                    }
                    else{
                        self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                            
                        }
                    }
                }
                
            }
        }
        
    }
    
    func dontAskFor2FAWithRequest(){
        
        APIRouter.sharedInstance.executeAPI(type: .dontAskFor2FA, method: .post, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    
                }
            }
            
        }
        
    }
    
}

extension CodeViewController: UITextFieldDelegate{
    
    func textField(_ textField: UITextField, shouldChangeCharactersIn range: NSRange, replacementString string: String) -> Bool {
        if (string == "" || textField.text!.count < 6){
            return true
        }
        else{
            return false
        }
    }
    
}

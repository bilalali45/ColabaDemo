//
//  PurchaseLoanInfoViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 30/08/2021.
//

import UIKit

class PurchaseLoanInfoViewController: BaseViewController {
    
    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldLoanStage: ColabaTextField!
    @IBOutlet weak var txtfieldPurchasePrice: ColabaTextField!
    @IBOutlet weak var txtfieldLoanAmount: ColabaTextField!
    @IBOutlet weak var txtfieldDownPayment: ColabaTextField!
    @IBOutlet weak var txtfieldPercentage: ColabaTextField!
    @IBOutlet weak var txtfieldClosingDate: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isDownPaymentPercentageChanged = false
    
    var loanApplicationId = 0
    var loanStageArray = [LoanGoalModel]()
    var loanInfo = LoanInfoDetailModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
        getLoanDetail()
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Loan Stage Text Field
        txtfieldLoanStage.setTextField(placeholder: "Loan Stage", controller: self,validationType: .required)
        txtfieldLoanStage.type = .dropdown
        
        ///Purchase Price Text Field
        txtfieldPurchasePrice.setTextField(placeholder: "Purchase Price", controller: self, validationType: .purchasePrice, keyboardType: .numberPad)
        txtfieldPurchasePrice.type = .amount
        
        ///Loan Amount Text Field
        txtfieldLoanAmount.setTextField(placeholder: "Loan Amount", controller: self, validationType: .required, keyboardType: .numberPad)
        txtfieldLoanAmount.type = .amount
        
        ///Down Payment Salary Text Field
        txtfieldDownPayment.setTextField(placeholder: "Down Payment", controller: self, validationType: .required, keyboardType: .numberPad)
        txtfieldDownPayment.type = .amount
        
        ///Down Payment Salary Text Field
        txtfieldPercentage.setTextField(placeholder: "", controller: self, validationType: .required, keyboardType: .numberPad)
        txtfieldPercentage.type = .percentage
        
        ///Estimated Closing Date Salary Text Field
        txtfieldClosingDate.setTextField(placeholder: "Estimated Closing Date", controller: self, validationType: .required)
        txtfieldClosingDate.type = .monthlyDatePicker
        
    }
    
    func setLoanInfo(){
        if let loanGoalModel = self.loanStageArray.filter({$0.id == self.loanInfo.loanGoalId}).first{
            txtfieldLoanStage.setTextField(text: loanGoalModel.loanGoal)
        }
        txtfieldClosingDate.isHidden = (txtfieldLoanStage.text! == "" || txtfieldLoanStage.text!.localizedCaseInsensitiveContains("Pre-Approval"))
        txtfieldPurchasePrice.setTextField(text: String(format: "%.0f", self.loanInfo.propertyValue.rounded()))
        txtfieldLoanAmount.setTextField(text: String(format: "%.0f", self.loanInfo.loanPayment.rounded()))
        txtfieldDownPayment.setTextField(text: String(format: "%.0f", self.loanInfo.downPayment.rounded()))
        txtfieldClosingDate.setTextField(text: Utility.getMonthYear(self.loanInfo.expectedClosingDate))
        calculatePercentage()
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        
        if validate() {
            updateLoanInfo()
        }
        
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldLoanStage.validate()
        isValidate = txtfieldPurchasePrice.validate() && isValidate
        isValidate = txtfieldLoanAmount.validate() && isValidate
        isValidate = txtfieldDownPayment.validate() && isValidate
        isValidate = txtfieldPercentage.validate() && isValidate
        if !(txtfieldClosingDate.isHidden){
            isValidate = txtfieldClosingDate.validate() && isValidate
        }
        return isValidate
    }
    
    //MARK:- API's
    
    func getLoanDetail(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getLoanInfoDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                if (status == .success){
                    let loanInfoDetailModel = LoanInfoDetailModel()
                    loanInfoDetailModel.updateModelWithJSON(json: result["data"])
                    self.loanInfo = loanInfoDetailModel
                    self.getLoanGoals()
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
    }
    
    func getLoanGoals(){
        
        let extraData = "loanpurposeid=\(self.loanInfo.loanPurposeId)"
        self.loanStageArray.removeAll()
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getLoanGoals, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    let loanGoalsArray = result.arrayValue
                    for loanGoal in loanGoalsArray{
                        let model = LoanGoalModel()
                        model.updateModelWithJSON(json: loanGoal)
                        self.loanStageArray.append(model)
                    }
                    self.txtfieldLoanStage.setDropDownDataSource(self.loanStageArray.map{$0.loanGoal})
                    self.setLoanInfo()
                }
                else{
                    
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
    }
    
    func updateLoanInfo(){
        
        var loanGoalId = 0
        var propertyValue: Any = NSNull()
        var downPayment: Any = NSNull()
        var expectedClosingDate: Any = NSNull()
        
        if let loanGoalModel = self.loanStageArray.filter({$0.loanGoal == txtfieldLoanStage.text!}).first{
            loanGoalId = loanGoalModel.id
        }
        
        if (txtfieldPurchasePrice.text != ""){
            if let value = Double(cleanString(string: txtfieldPurchasePrice.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                propertyValue = value
            }
        }
        
        if (txtfieldDownPayment.text != ""){
            if let value = Double(cleanString(string: txtfieldDownPayment.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                downPayment = value
            }
        }
        
        if (txtfieldLoanStage.text! == "" || txtfieldLoanStage.text!.localizedCaseInsensitiveContains("Pre-Approval")){
            expectedClosingDate = NSNull()
        }
        else{
            let dateComponent = txtfieldClosingDate.text!.components(separatedBy: "/")
            if (dateComponent.count == 2){
                expectedClosingDate = "\(dateComponent[1])-\(dateComponent[0])-01T00:00:00"
            }
        }
        
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let params = ["loanApplicationId": loanApplicationId,
                      "loanPurposeId": loanInfo.loanPurposeId,
                      "loanGoalId": loanGoalId,
                      "propertyValue": propertyValue,
                      "downPayment": downPayment,
                      "expectedClosingDate": expectedClosingDate] as [String: Any]
        
        APIRouter.sharedInstance.executeAPI(type: .updateLoanInformation, method: .post, params: params) { status, result, message in
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
//                    self.showPopup(message: "Loan Information updated sucessfully", popupState: .success, popupDuration: .custom(5)) { dismiss in
//                        self.goBack()
//                    }
                    self.goBack()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
        }
        
    }
}

extension PurchaseLoanInfoViewController: ColabaTextFieldDelegate {
    func textFieldDidChange(_ textField: ColabaTextField) {
         
        if textField == txtfieldPurchasePrice {
            if !isDownPaymentPercentageChanged {
                txtfieldPercentage.attributedText = createAttributedTextWithPrefix(prefix: PrefixType.percentage.rawValue, string: "20")
                isDownPaymentPercentageChanged = true
            }
            calculateDownPayment()
        }
        
        if textField == txtfieldLoanAmount{
            isDownPaymentPercentageChanged = true
            calculateDownPaymentFromLoanAmount()
        }
        
        if textField == txtfieldPercentage {
            isDownPaymentPercentageChanged = true
            calculateDownPayment()
        }
        
        if textField == txtfieldDownPayment {
            isDownPaymentPercentageChanged = true
            calculatePercentage()
        }
    }
    
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if (textField == txtfieldLoanStage){
            txtfieldClosingDate.isHidden = (txtfieldLoanStage.text! == "" || txtfieldLoanStage.text!.localizedCaseInsensitiveContains("Pre-Approval"))
        }
    }
    
    func calculateDownPayment() {
        let percentage = Double(cleanString(string: txtfieldPercentage.text ?? "0.0", replaceCharacters: [PrefixType.percentage.rawValue], replaceWith: "")) ?? 0.0
        
        if let purchaseAmount = Double(cleanString(string: txtfieldPurchasePrice.text!, replaceCharacters: [PrefixType.amount.rawValue, ","], replaceWith: "")) {
            let downPaymentPercentage = percentage / 100
            let downPayment = Int(round(purchaseAmount * downPaymentPercentage))
            let downPaymentString = cleanString(string: downPayment.withCommas(), replaceCharacters: ["$",".00"], replaceWith: "")
            let loanPayment = purchaseAmount - Double(downPayment)
            txtfieldDownPayment.attributedText = createAttributedTextWithPrefix(prefix: PrefixType.amount.rawValue, string: downPaymentString)
            txtfieldLoanAmount.setTextField(text: String(format: "%.0f", loanPayment))
        }
    }
    
    func calculatePercentage() {
        let downPayment = Double(cleanString(string: txtfieldDownPayment.text ?? "0.0", replaceCharacters: [PrefixType.amount.rawValue, ","], replaceWith: "")) ?? 0.0
        
        if let purchaseAmount = Double(cleanString(string: txtfieldPurchasePrice.text!, replaceCharacters: [PrefixType.amount.rawValue, ","], replaceWith: "")) {
            let loanPayment = purchaseAmount - downPayment
            txtfieldLoanAmount.setTextField(text: String(format: "%.0f", loanPayment))
            if purchaseAmount > 0{
                let percentage = Int(round(downPayment / purchaseAmount * 100))
                txtfieldPercentage.attributedText = createAttributedTextWithPrefix(prefix: PrefixType.percentage.rawValue, string: percentage.description)
            }
            
        }
    }
    
    func calculateDownPaymentFromLoanAmount(){
        if let purchaseAmount = Double(cleanString(string: txtfieldPurchasePrice.text!, replaceCharacters: [PrefixType.amount.rawValue, ","], replaceWith: "")) {
            if let loanAmount = Double(cleanString(string: txtfieldLoanAmount.text!, replaceCharacters: [PrefixType.amount.rawValue, ","], replaceWith: "")){
                let downPayment = purchaseAmount - loanAmount
                txtfieldDownPayment.setTextField(text: String(format: "%.0f", downPayment))
                calculatePercentage()
            }
        }
    }
}


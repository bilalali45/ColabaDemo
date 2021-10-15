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
            if (txtfieldLoanStage.text != "" && txtfieldPurchasePrice.text != "" && txtfieldLoanAmount.text != "" && txtfieldDownPayment.text != "" && txtfieldPercentage.text != "" && txtfieldClosingDate.text != ""){
                self.goBack()
            }
        }
        
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldLoanStage.validate()
        isValidate = txtfieldPurchasePrice.validate() && isValidate
        isValidate = txtfieldLoanAmount.validate() && isValidate
        isValidate = txtfieldDownPayment.validate() && isValidate
        isValidate = txtfieldPercentage.validate() && isValidate
        isValidate = txtfieldClosingDate.validate() && isValidate
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
        if textField == txtfieldPercentage {
            isDownPaymentPercentageChanged = true
            calculateDownPayment()
        }
        
        if textField == txtfieldDownPayment {
            isDownPaymentPercentageChanged = true
            calculatePercentage()
        }
    }
    
    func calculateDownPayment() {
        let percentage = Double(cleanString(string: txtfieldPercentage.text ?? "0.0", replaceCharacters: [PrefixType.percentage.rawValue], replaceWith: "")) ?? 0.0
        
        if let purchaseAmount = Double(cleanString(string: txtfieldPurchasePrice.text!, replaceCharacters: [PrefixType.amount.rawValue, ","], replaceWith: "")) {
            let downPaymentPercentage = percentage / 100
            let downPayment = Int(round(purchaseAmount * downPaymentPercentage))
            let downPaymentString = cleanString(string: downPayment.withCommas(), replaceCharacters: ["$",".00"], replaceWith: "")
            txtfieldDownPayment.attributedText = createAttributedTextWithPrefix(prefix: PrefixType.amount.rawValue, string: downPaymentString)
        }
    }
    
    func calculatePercentage() {
        let downPayment = Double(cleanString(string: txtfieldDownPayment.text ?? "0.0", replaceCharacters: [PrefixType.amount.rawValue, ","], replaceWith: "")) ?? 0.0
        
        if let purchaseAmount = Double(cleanString(string: txtfieldPurchasePrice.text!, replaceCharacters: [PrefixType.amount.rawValue, ","], replaceWith: "")) {
            if purchaseAmount > 0{
                let percentage = Int(round(downPayment / purchaseAmount * 100))
                txtfieldPercentage.attributedText = createAttributedTextWithPrefix(prefix: PrefixType.percentage.rawValue, string: percentage.description)
            }
            
        }
    }
}


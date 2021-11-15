//
//  AddBankAccountViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 07/09/2021.
//

import UIKit

class AddBankAccountViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldAccountType: ColabaTextField!
    @IBOutlet weak var txtfieldFinancialInstitution: ColabaTextField!
    @IBOutlet weak var txtfieldAccountNumber: ColabaTextField!
    @IBOutlet weak var txtfieldAnnualBaseSalary: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var borrowerName = ""
    var isShowAccountNumber = false
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var borrowerAssetId = 0
    var accountTypeArray = [DropDownModel]()
    var bankAccountDetail = BankAccountDetailModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        lblBorrowerName.text = borrowerName.uppercased()
        setTextFields()
        getBankAccountTypes()
        btnDelete.isHidden = isForAdd
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Account Type Text Field
        txtfieldAccountType.setTextField(placeholder: "Account Type", controller: self, validationType: .required)
        txtfieldAccountType.type = .dropdown
        
        ///Financial Institution Text Field
        txtfieldFinancialInstitution.setTextField(placeholder: "Financial Institution", controller: self, validationType: .noValidation)
        
        ///Account Number Text Field
        txtfieldAccountNumber.setTextField(placeholder: "Account Number", controller: self, validationType: .noValidation, keyboardType: .numberPad)
        txtfieldAccountNumber.type = .password
        
        ///Annual Base Salary Text Field
        txtfieldAnnualBaseSalary.setTextField(placeholder: "Annual Base Salary", controller: self, validationType: .noValidation)
        txtfieldAnnualBaseSalary.type = .amount
    }
    
    func setBankAccountDetail(){
        if let accountType = self.accountTypeArray.filter({$0.optionId == self.bankAccountDetail.assetTypeId}).first{
            self.txtfieldAccountType.setTextField(text: accountType.optionName)
        }
        txtfieldFinancialInstitution.setTextField(text: self.bankAccountDetail.institutionName)
        txtfieldAccountNumber.setTextField(text: self.bankAccountDetail.accountNumber)
        txtfieldAnnualBaseSalary.setTextField(text: String(format: "%.0f", self.bankAccountDetail.balance.rounded()))
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        let vc = Utility.getDeleteAddressPopupVC()
        vc.popupTitle = "Are you sure you want to remove this asset type?"
        vc.screenType = 4
        vc.delegate = self
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
            addUpdateBankAccount()
        }
    }
    
    func validate() -> Bool {
        let isValidate = txtfieldAccountType.validate()
//        isValidate = txtfieldFinancialInstitution.validate() && isValidate
//        isValidate = txtfieldAccountNumber.validate() && isValidate
//        isValidate = txtfieldAnnualBaseSalary.validate() && isValidate
        return isValidate
    }
    
    //MARK:- API's
    
    func getBankAccountTypes(){
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllBankAccountType, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option)
                        self.accountTypeArray.append(model)
                    }
                    self.txtfieldAccountType.setDropDownDataSource(self.accountTypeArray.map({$0.optionName}))
                    if (self.isForAdd){
                        Utility.showOrHideLoader(shouldShow: false)
                    }
                    else{
                        self.getBankAccountDetail()
                    }
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
            
        }
        
    }
    
    func getBankAccountDetail(){
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerId=\(borrowerId)&borrowerAssetId=\(borrowerAssetId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getBankAccountDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = BankAccountDetailModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.bankAccountDetail = model
                    self.setBankAccountDetail()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
    
    func addUpdateBankAccount(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        var assetTypeId = 0
        var institutionName: Any = NSNull()
        var accountNumber: Any = NSNull()
        var balance: Any = NSNull()
        
        if let type = accountTypeArray.filter({$0.optionName.localizedCaseInsensitiveContains(txtfieldAccountType.text!)}).first{
            assetTypeId = type.optionId
        }
        
        if (txtfieldFinancialInstitution.text != ""){
            institutionName = txtfieldFinancialInstitution.text!
        }
        
        if (txtfieldAccountNumber.text != ""){
            accountNumber = txtfieldAccountNumber.text!
        }
        
        if (txtfieldAnnualBaseSalary.text != ""){
            if let value = Double(cleanString(string: txtfieldAnnualBaseSalary.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                balance = value
            }
        }
        
        let params = ["id": isForAdd ? NSNull() : bankAccountDetail.id,
                      "AssetTypeId": assetTypeId,
                      "InstitutionName": institutionName,
                      "AccountNumber": accountNumber,
                      "Balance": balance,
                      "LoanApplicationId": loanApplicationId,
                      "BorrowerId": borrowerId] as [String: Any]
        
        APIRouter.sharedInstance.executeAPI(type: .addUpdateBankAccount, method: .post, params: params) { status, result, message in
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    self.dismissVC()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
        }
    }
    
    func deleteAsset(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "AssetId=\(borrowerAssetId)&borrowerId=\(borrowerId)&loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeAPI(type: .deleteAsset, method: .delete, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    self.dismissVC()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
}

extension AddBankAccountViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        deleteAsset()
    }
}

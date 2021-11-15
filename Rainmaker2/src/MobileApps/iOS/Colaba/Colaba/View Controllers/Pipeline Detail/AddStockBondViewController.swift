//
//  AddStockBondViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 07/09/2021.
//

import UIKit

class AddStockBondViewController: BaseViewController {

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
    @IBOutlet weak var txtfieldCurrentMarketValue: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var borrowerName = ""
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var borrowerAssetId = 0
    var accountTypeArray = [DropDownModel]()
    var financialAccountsDetail = FinancialAccountDetailModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
        lblBorrowerName.text = borrowerName.uppercased()
        getAccountType()
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
        txtfieldAccountNumber.type = .password
        txtfieldAccountNumber.setTextField(placeholder: "Account Number", controller: self, validationType: .noValidation, keyboardType: .numberPad)
    
        ///Current Market Value Text Field
        txtfieldCurrentMarketValue.setTextField(placeholder: "Current Market Value", controller: self, validationType: .noValidation)
        txtfieldCurrentMarketValue.type = .amount
        
    }
    
    func setFinancialAccountDetail(){
        if let accountType = self.accountTypeArray.filter({$0.optionId == self.financialAccountsDetail.assetTypeId}).first{
            self.txtfieldAccountType.setTextField(text: accountType.optionName)
        }
        txtfieldFinancialInstitution.setTextField(text: self.financialAccountsDetail.institutionName)
        txtfieldAccountNumber.setTextField(text: self.financialAccountsDetail.accountNumber)
        txtfieldCurrentMarketValue.setTextField(text: String(format: "%.0f", self.financialAccountsDetail.balance.rounded()))
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
            addUpdateFinancialAccount()
        }
    }
    
    func validate() -> Bool {
        let isValidate = txtfieldAccountType.validate()
//        isValidate = txtfieldFinancialInstitution.validate() && isValidate
//        isValidate = txtfieldAccountNumber.validate() && isValidate
//        isValidate = txtfieldCurrentMarketValue.validate() && isValidate
        return isValidate
    }
    
    //MARK:- API's
    
    func getAccountType(){
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllFinancialAssets, method: .get, params: nil) { status, result, message in
            
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
                        self.getFinancialAccountDetail()
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
    
    func getFinancialAccountDetail(){
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerId=\(borrowerId)&borrowerAssetId=\(borrowerAssetId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getFinancialAccountDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = FinancialAccountDetailModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.financialAccountsDetail = model
                    self.setFinancialAccountDetail()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
    
    func addUpdateFinancialAccount(){
        
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
        
        if (txtfieldCurrentMarketValue.text != ""){
            if let value = Double(cleanString(string: txtfieldCurrentMarketValue.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                balance = value
            }
        }
        
        let params = ["Id": isForAdd ? NSNull() : financialAccountsDetail.id,
                      "AssetTypeId": assetTypeId,
                      "InstitutionName": institutionName,
                      "AccountNumber": accountNumber,
                      "Balance": balance,
                      "LoanApplicationId": loanApplicationId,
                      "BorrowerId": borrowerId] as [String: Any]
        
        APIRouter.sharedInstance.executeAPI(type: .addUpdateFinancialAccount, method: .post, params: params) { status, result, message in
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

extension AddStockBondViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        deleteAsset()
    }
}

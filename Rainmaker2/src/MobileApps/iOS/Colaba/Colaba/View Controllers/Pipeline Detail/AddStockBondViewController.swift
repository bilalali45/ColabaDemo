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
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Account Type Text Field
        txtfieldAccountType.setTextField(placeholder: "Account Type", controller: self, validationType: .required)
        txtfieldAccountType.type = .dropdown
        
        ///Financial Institution Text Field
        txtfieldFinancialInstitution.setTextField(placeholder: "Financial Institution", controller: self, validationType: .required)

        ///Account Number Text Field
        txtfieldAccountNumber.type = .password
        txtfieldAccountNumber.setTextField(placeholder: "Account Number", controller: self, validationType: .required, keyboardType: .numberPad)
    
        ///Current Market Value Text Field
        txtfieldCurrentMarketValue.setTextField(placeholder: "Current Market Value", controller: self, validationType: .required)
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
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
            if (txtfieldAccountType.text != "" && txtfieldFinancialInstitution.text != "" && txtfieldAccountNumber.text != "" && txtfieldCurrentMarketValue.text != ""){
                self.dismissVC()
            }
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldAccountType.validate()
        isValidate = txtfieldFinancialInstitution.validate() && isValidate
        isValidate = txtfieldAccountNumber.validate() && isValidate
        isValidate = txtfieldCurrentMarketValue.validate() && isValidate
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
}

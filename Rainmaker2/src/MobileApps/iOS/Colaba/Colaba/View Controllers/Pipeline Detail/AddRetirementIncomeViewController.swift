//
//  AddRetirementIncomeViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit

class AddRetirementIncomeViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldRetirementIncomeType: ColabaTextField!
    @IBOutlet weak var txtfieldEmployerName: ColabaTextField!
    @IBOutlet weak var txtfieldEmployerNameHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldEmployerNameTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldMonthlyIncome: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var borrowerName = ""
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var incomeInfoId = 0
    var retirementTypeArray = [DropDownModel]()
    var retirementDetail = RetirementDetailModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
        lblUsername.text = borrowerName.uppercased()
        getRetirementTypes()
        btnDelete.isHidden = isForAdd
    }
        
    //MARK:- Methods and Actions
    func setupTextFields(){
        
        txtfieldRetirementIncomeType.setTextField(placeholder: "Retirement Income Type", controller: self, validationType: .required)
        txtfieldRetirementIncomeType.type = .dropdown
        
        txtfieldEmployerName.setTextField(placeholder: "Employer Name", controller: self, validationType: .required)
        
        txtfieldMonthlyIncome.setTextField(placeholder: "Monthly Income", controller: self, validationType: .required)
        txtfieldMonthlyIncome.type = .amount
    }
    
    func setRetirementIncomeDetail(){
        if let retirementType = retirementTypeArray.filter({$0.optionId == retirementDetail.incomeTypeId}).first{
            txtfieldRetirementIncomeType.setTextField(text: retirementType.optionName)
            setTextFieldAccordingToOptions(option: retirementType.optionName)
        }
        txtfieldEmployerName.setTextField(text: txtfieldRetirementIncomeType.text!.localizedCaseInsensitiveContains("Other Retirement Source") ? retirementDetail.descriptionField : retirementDetail.employerName)
        txtfieldMonthlyIncome.setTextField(text: String(format: "%.0f", retirementDetail.monthlyBaseIncome))
    }
    
    func setTextFieldAccordingToOptions(option: String){
        if (option.localizedCaseInsensitiveContains("Social Security") || option.localizedCaseInsensitiveContains("IRA / 401K")){
            txtfieldEmployerName.isHidden = true
            txtfieldEmployerNameTopConstraint.constant = 0
            txtfieldEmployerNameHeightConstraint.constant = 0
            txtfieldMonthlyIncome.isHidden = false
            txtfieldMonthlyIncome.placeholder = option.localizedCaseInsensitiveContains("Social Security") ? "Monthly Income" : "Monthly Withdrawal"
            self.view.layoutSubviews()
        }
        else {
            txtfieldEmployerName.isHidden = false
            txtfieldEmployerNameTopConstraint.constant = 30
            txtfieldEmployerNameHeightConstraint.constant = 39
            txtfieldEmployerName.placeholder = option.localizedCaseInsensitiveContains("Pension") ? "Employer Name" : "Description"
            txtfieldMonthlyIncome.isHidden = false
            txtfieldMonthlyIncome.placeholder = "Monthly Income"
        }

    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        let vc = Utility.getDeleteAddressPopupVC()
        vc.popupTitle = "Are you sure you want to remove this income source?"
        vc.screenType = 4
        vc.delegate = self
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate(){
            addUpdateRetirementIncome()
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldRetirementIncomeType.validate()
        if (!txtfieldEmployerName.isHidden){
            isValidate = txtfieldEmployerName.validate() && isValidate
        }
        isValidate = txtfieldMonthlyIncome.validate() && isValidate
        return isValidate
    }
    
    //MARK:- API's
    
    func getRetirementTypes(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllRetirementTypes, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option)
                        self.retirementTypeArray.append(model)
                    }
                    self.txtfieldRetirementIncomeType.setDropDownDataSource(self.retirementTypeArray.map({$0.optionName}))
                    if (self.isForAdd){
                        Utility.showOrHideLoader(shouldShow: false)
                    }
                    else{
                        self.getRetirementIncomeDetail()
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
    
    func getRetirementIncomeDetail(){
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerid=\(borrowerId)&incomeInfoId=\(incomeInfoId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getRetirementIncomeDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = RetirementDetailModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.retirementDetail = model
                    self.setRetirementIncomeDetail()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
    
    func addUpdateRetirementIncome(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        var incomeTypeId = 0
        var employerNameOrDescription: Any = NSNull()
        var monthlyBaseIncome: Any = NSNull()
        
        if let incomeType = (retirementTypeArray.filter({$0.optionName.localizedCaseInsensitiveContains(txtfieldRetirementIncomeType.text!)})).first{
            incomeTypeId = incomeType.optionId
        }
        if (txtfieldEmployerName.text! != ""){
            employerNameOrDescription = txtfieldEmployerName.text!
        }
        if (txtfieldMonthlyIncome.text! != ""){
            if let value = Int(cleanString(string: txtfieldMonthlyIncome.text!, replaceCharacters: ["$  |  ",","], replaceWith: "")){
                monthlyBaseIncome = value
            }
        }
        
        let params = ["IncomeInfoId": isForAdd ? NSNull() : retirementDetail.incomeInfoId,
                      "BorrowerId": borrowerId,
                      "employerName": employerNameOrDescription,
                      "description": employerNameOrDescription,
                      "MonthlyBaseIncome": monthlyBaseIncome,
                      "IncomeTypeId": incomeTypeId,
                      "LoanApplicationId": loanApplicationId] as [String: Any]
        
        APIRouter.sharedInstance.executeAPI(type: .addUpdateRetirementIncome, method: .post, params: params) { status, result, message in
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
    
    func deleteIncome(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "IncomeInfoId=\(incomeInfoId)&borrowerId=\(borrowerId)&loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeAPI(type: .deleteIncome, method: .delete, params: nil, extraData: extraData) { status, result, message in
            
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

extension AddRetirementIncomeViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldRetirementIncomeType {
            setTextFieldAccordingToOptions(option: option)
        }
    }
}

extension AddRetirementIncomeViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        deleteIncome()
    }
}

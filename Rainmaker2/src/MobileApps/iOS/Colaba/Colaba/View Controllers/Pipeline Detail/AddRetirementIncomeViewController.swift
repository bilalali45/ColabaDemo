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
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate(){
            self.dismissVC()
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldRetirementIncomeType.validate()
        isValidate = txtfieldEmployerName.validate() && isValidate
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
}

extension AddRetirementIncomeViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldRetirementIncomeType {
            setTextFieldAccordingToOptions(option: option)
        }
    }
}

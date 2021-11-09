//
//  AddOtherIncomeViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit

class AddOtherIncomeViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldIncomeType: ColabaTextField!
    @IBOutlet weak var txtfieldDescription: ColabaTextField!
    @IBOutlet weak var txtfieldDescriptionTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldDescriptionHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldMonthlyIncome: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var borrowerName = ""
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var incomeInfoId = 0
    var otherIncomeTypeArray = [DropDownModel]()
    var otherIncomeDetail = OtherIncomeDetailModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
        lblUsername.text = borrowerName.uppercased()
        getOtherIncomeType()
    }
        
    //MARK:- Methods and Actions
    
    func setupTextFields(){
        ///Income Type Text Field
        txtfieldIncomeType.setTextField(placeholder: "Income Type", controller: self, validationType: .required)
        txtfieldIncomeType.type = .dropdown
        
        ///Description Text Field
        txtfieldDescription.setTextField(placeholder: "Description", controller: self, validationType: .required)
        
        ///Monthly Income Text Field
        txtfieldMonthlyIncome.setTextField(placeholder: "Monthly Income", controller: self, validationType: .required)
        txtfieldMonthlyIncome.type = .amount
    }
    
    func setOtherIncomeDetail(){
        if let otherIncomeType = otherIncomeTypeArray.filter({$0.optionId == otherIncomeDetail.incomeTypeId}).first{
            txtfieldIncomeType.setTextField(text: otherIncomeType.optionName)
            setTextfieldAccordingToOption(option: otherIncomeType.optionName)
        }
        txtfieldDescription.setTextField(text: otherIncomeDetail.descriptionField)
        txtfieldMonthlyIncome.setTextField(text: String(format: "%.0f", (txtfieldIncomeType.text == "Capital Gains" || txtfieldIncomeType.text == "Interest / Dividends" || txtfieldIncomeType.text == "Other Income Source") ? otherIncomeDetail.annualBaseIncome : otherIncomeDetail.monthlyBaseIncome ))
    }
    
    func setTextfieldAccordingToOption(option: String){
        txtfieldMonthlyIncome.isHidden = false
        txtfieldMonthlyIncome.placeholder = (option == "Capital Gains" || option == "Interest / Dividends" || option == "Other Income Source") ? "Annual Income" : "Monthly Income"
        txtfieldDescription.isHidden = !(option == "Annuity" || option == "Other Income Source")
        txtfieldDescriptionTopConstraint.constant = (option == "Annuity" || option == "Other Income Source") ? 30 : 0
        txtfieldDescriptionHeightConstraint.constant = (option == "Annuity" || option == "Other Income Source") ? 39 : 0
        self.view.layoutSubviews()
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
        var isValidate = txtfieldIncomeType.validate()
        if (!txtfieldDescription.isHidden){
            isValidate = txtfieldDescription.validate() && isValidate
        }
        isValidate = txtfieldMonthlyIncome.validate() && isValidate
        return isValidate
    }
    
    //MARK:- API's
    
    func getOtherIncomeType(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getOtherIncomeType, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option)
                        self.otherIncomeTypeArray.append(model)
                    }
                    self.txtfieldIncomeType.setDropDownDataSource(self.otherIncomeTypeArray.map({$0.optionName}))
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
        
        APIRouter.sharedInstance.executeAPI(type: .getOtherIncomeDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = OtherIncomeDetailModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.otherIncomeDetail = model
                    self.setOtherIncomeDetail()
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

extension AddOtherIncomeViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldIncomeType {
            setTextfieldAccordingToOption(option: option)
        }
    }
}

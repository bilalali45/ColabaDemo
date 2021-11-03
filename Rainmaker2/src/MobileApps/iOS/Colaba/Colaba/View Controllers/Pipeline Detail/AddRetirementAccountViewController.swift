//
//  AddRetirementAccountViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 07/09/2021.
//

import UIKit
import Material

class AddRetirementAccountViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldFinancialInstitution: ColabaTextField!
    @IBOutlet weak var txtfieldAccountNumber: ColabaTextField!
    @IBOutlet weak var txtfieldAnnualBaseSalary: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var borrowerName = ""
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var borrowerAssetId = 0
    var retirementAccountDetail = RetirementAccountDetailModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
        lblBorrowerName.text = borrowerName.uppercased()
        if (!isForAdd){
            getRetirementAccountDetail()
        }
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Financial Institution Text Field
        txtfieldFinancialInstitution.setTextField(placeholder: "Financial Institution", controller: self, validationType: .required)
        
        ///Account Number Text Field
        txtfieldAccountNumber.setTextField(placeholder: "Account Number", controller: self, validationType: .required, keyboardType: .numberPad)
        txtfieldAccountNumber.type = .password
        
        ///Annual Base Salary Text Field
        txtfieldAnnualBaseSalary.setTextField(placeholder: "Annual Base Salary", controller: self, validationType: .required)
        txtfieldAnnualBaseSalary.type = .amount
    }
    
    func setRetirementAccountDetail(){
        txtfieldFinancialInstitution.setTextField(text: self.retirementAccountDetail.institutionName)
        txtfieldAccountNumber.setTextField(text: self.retirementAccountDetail.accountNumber)
        txtfieldAnnualBaseSalary.setTextField(text: String(format: "%.0f", self.retirementAccountDetail.value.rounded()))
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
            if (txtfieldFinancialInstitution.text != "" && txtfieldAccountNumber.text != "" && txtfieldAnnualBaseSalary.text != ""){
                self.dismissVC()
            }
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldFinancialInstitution.validate()
        isValidate = txtfieldAccountNumber.validate() && isValidate
        isValidate = txtfieldAnnualBaseSalary.validate() && isValidate
        return isValidate
    }
    
    //MARK:- API's
    
    func getRetirementAccountDetail(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerId=\(borrowerId)&borrowerAssetId=\(borrowerAssetId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getRetirementAccountDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = RetirementAccountDetailModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.retirementAccountDetail = model
                    self.setRetirementAccountDetail()
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

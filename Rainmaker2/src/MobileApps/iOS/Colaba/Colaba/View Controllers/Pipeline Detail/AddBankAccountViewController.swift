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
    
    var isShowAccountNumber = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Account Type Text Field
        txtfieldAccountType.setTextField(placeholder: "Account Type", controller: self, validationType: .required)
        txtfieldAccountType.type = .dropdown
        txtfieldAccountType.setDropDownDataSource(kAccountTypeArray)
        
        ///Financial Institution Text Field
        txtfieldFinancialInstitution.setTextField(placeholder: "Financial Institution", controller: self, validationType: .required)
        
        ///Account Number Text Field
        txtfieldAccountNumber.setTextField(placeholder: "Account Number", controller: self, validationType: .required, keyboardType: .numberPad)
        txtfieldAccountNumber.type = .password
        
        ///Annual Base Salary Text Field
        txtfieldAnnualBaseSalary.setTextField(placeholder: "Annual Base Salary", controller: self, validationType: .required)
        txtfieldAnnualBaseSalary.type = .amount
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
            if (txtfieldAccountType.text != "" && txtfieldFinancialInstitution.text != "" && txtfieldAccountNumber.text != "" && txtfieldAnnualBaseSalary.text != ""){
                self.dismissVC()
            }
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldAccountType.validate()
        isValidate = txtfieldFinancialInstitution.validate() && isValidate
        isValidate = txtfieldAccountNumber.validate() && isValidate
        isValidate = txtfieldAnnualBaseSalary.validate() && isValidate
        return isValidate
    }
}

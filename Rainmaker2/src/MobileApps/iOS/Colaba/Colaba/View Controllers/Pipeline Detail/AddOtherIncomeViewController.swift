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
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
    }
        
    //MARK:- Methods and Actions
    func setupTextFields(){
        
        txtfieldIncomeType.setTextField(placeholder: "Income Type")
        txtfieldIncomeType.setDelegates(controller: self)
        txtfieldIncomeType.setValidation(validationType: .required)
        txtfieldIncomeType.type = .dropdown
        txtfieldIncomeType.setDropDownDataSource(kOtherIncomeTypeArray)
        
        txtfieldDescription.setTextField(placeholder: "Description")
        txtfieldDescription.setDelegates(controller: self)
        txtfieldDescription.setValidation(validationType: .required)
        txtfieldDescription.setTextField(keyboardType: .asciiCapable)
        txtfieldDescription.setIsValidateOnEndEditing(validate: true)
        
        txtfieldMonthlyIncome.setTextField(placeholder: "Monthly Income")
        txtfieldMonthlyIncome.setDelegates(controller: self)
        txtfieldMonthlyIncome.setValidation(validationType: .required)
        txtfieldMonthlyIncome.setTextField(keyboardType: .numberPad)
        txtfieldMonthlyIncome.setIsValidateOnEndEditing(validate: true)
        txtfieldMonthlyIncome.type = .amount
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
        isValidate = txtfieldDescription.validate()
        isValidate = txtfieldMonthlyIncome.validate()
        return isValidate
    }
}

extension AddOtherIncomeViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldIncomeType {
            txtfieldMonthlyIncome.isHidden = false
            txtfieldMonthlyIncome.placeholder = (option == "Capital Gains" || option == "Interest / Dividends" || option == "Other Income Source") ? "Annual Income" : "Monthly Income"
            txtfieldDescription.isHidden = !(option == "Annuity" || option == "Other Income Source")
            txtfieldDescriptionTopConstraint.constant = (option == "Annuity" || option == "Other Income Source") ? 30 : 0
            txtfieldDescriptionHeightConstraint.constant = (option == "Annuity" || option == "Other Income Source") ? 39 : 0
            self.view.layoutSubviews()
        }
    }
}

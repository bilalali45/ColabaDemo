//
//  AddOtherIncomeViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit

class AddOtherIncomeViewController: BaseViewController {

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
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
    }
        
    //MARK:- Methods and Actions
    
    func setupTextFields(){
        
        txtfieldIncomeType.setTextField(placeholder: "Income Type")
        txtfieldIncomeType.setDelegates(controller: self)
        txtfieldIncomeType.setValidation(validationType: .required)
        txtfieldIncomeType.setTextField(keyboardType: .asciiCapable)
        txtfieldIncomeType.setIsValidateOnEndEditing(validate: true)
        txtfieldIncomeType.type = .dropdown
        
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
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
    }
    
    func validate() -> Bool {

//        if (!txtfieldIncomeType.validate()) {
//            return false
//        }
//        if (!txtfieldDescription.validate()){
//            return false
//        }
        if (!txtfieldMonthlyIncome.validate()){
            return false
        }

        return true
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        txtfieldIncomeType.validate()
        txtfieldDescription.validate()
        txtfieldMonthlyIncome.validate()
        
        if validate(){
            self.dismissVC()
        }
    }
}

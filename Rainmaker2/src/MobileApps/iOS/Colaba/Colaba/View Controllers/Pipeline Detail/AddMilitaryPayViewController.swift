//
//  AddMilitaryPayViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit

class AddMilitaryPayViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldEmployerName: ColabaTextField!
    @IBOutlet weak var addressView: UIView!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var txtfieldJobTitle: ColabaTextField!
    @IBOutlet weak var txtfieldProfessionYears: ColabaTextField!
    @IBOutlet weak var txtfieldStartDate: ColabaTextField!
    @IBOutlet weak var txtfieldMonthlyBaseSalary: ColabaTextField!
    @IBOutlet weak var txtfieldMilitaryEntitlements: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
    }
        
    //MARK:- Methods and Actions
    
    func setupTextFields(){
        
        txtfieldEmployerName.setTextField(placeholder: "Employer Name")
        txtfieldEmployerName.setDelegates(controller: self)
        txtfieldEmployerName.setValidation(validationType: .required)
        txtfieldEmployerName.setTextField(keyboardType: .asciiCapable)
        txtfieldEmployerName.setIsValidateOnEndEditing(validate: true)
        
        txtfieldJobTitle.setTextField(placeholder: "Job Title")
        txtfieldJobTitle.setDelegates(controller: self)
        txtfieldJobTitle.setValidation(validationType: .required)
        txtfieldJobTitle.setTextField(keyboardType: .asciiCapable)
        txtfieldJobTitle.setIsValidateOnEndEditing(validate: true)
        
        txtfieldProfessionYears.setTextField(placeholder: "Years in Profession")
        txtfieldProfessionYears.setDelegates(controller: self)
        txtfieldProfessionYears.setValidation(validationType: .required)
        txtfieldProfessionYears.setTextField(keyboardType: .numberPad)
        txtfieldProfessionYears.setIsValidateOnEndEditing(validate: true)
        
        txtfieldStartDate.setTextField(placeholder: "Start Date (MM/DD/YYYY)")
        txtfieldStartDate.setDelegates(controller: self)
        txtfieldStartDate.setValidation(validationType: .required)
        txtfieldStartDate.setTextField(keyboardType: .asciiCapable)
        txtfieldStartDate.setIsValidateOnEndEditing(validate: true)
        txtfieldStartDate.type = .datePicker
        
        txtfieldMonthlyBaseSalary.setTextField(placeholder: "Monthly Base Salary")
        txtfieldMonthlyBaseSalary.setDelegates(controller: self)
        txtfieldMonthlyBaseSalary.setValidation(validationType: .required)
        txtfieldMonthlyBaseSalary.setTextField(keyboardType: .numberPad)
        txtfieldMonthlyBaseSalary.setIsValidateOnEndEditing(validate: true)
        txtfieldMonthlyBaseSalary.setValidation(validationType: .required)
        txtfieldMonthlyBaseSalary.type = .amount
        
        txtfieldMilitaryEntitlements.setTextField(placeholder: "Military Entitlements")
        txtfieldMilitaryEntitlements.setDelegates(controller: self)
        txtfieldMilitaryEntitlements.setValidation(validationType: .required)
        txtfieldMilitaryEntitlements.setTextField(keyboardType: .numberPad)
        txtfieldMilitaryEntitlements.setIsValidateOnEndEditing(validate: true)
        txtfieldMilitaryEntitlements.setValidation(validationType: .required)
        txtfieldMilitaryEntitlements.type = .amount
        
        addressView.layer.cornerRadius = 6
        addressView.layer.borderWidth = 1
        addressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addressView.dropShadowToCollectionViewCell()
        addressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
    }
    
    @objc func addressViewTapped(){
        let vc = Utility.getCurrentEmployerAddressVC()
        vc.topTitle = "Service Location Address"
        vc.searchTextFieldPlaceholder = "Search Service Location Address"
        self.pushToVC(vc: vc)
    }
    
    func validate() -> Bool {

        if (!txtfieldEmployerName.validate()) {
            return false
        }
        else if (!txtfieldJobTitle.validate()){
            return false
        }
        else if (!txtfieldProfessionYears.validate()){
            return false
        }
        else if (!txtfieldStartDate.validate()) {
            return false
        }
        else if (!txtfieldMonthlyBaseSalary.validate()){
            return false
        }
        else if (!txtfieldMilitaryEntitlements.validate()){
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
        txtfieldEmployerName.validate()
        txtfieldJobTitle.validate()
        txtfieldProfessionYears.validate()
        txtfieldStartDate.validate()
        txtfieldMonthlyBaseSalary.validate()
        txtfieldMilitaryEntitlements.validate()
        
        if validate(){
            self.dismissVC()
        }
    }
}

//
//  AddSelfEmploymentViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit

class AddSelfEmploymentViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldBusinessName: ColabaTextField!
    @IBOutlet weak var txtfieldBusinessPhoneNumber: ColabaTextField!
    @IBOutlet weak var addressView: UIView!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var txtfieldJobTitle: ColabaTextField!
    @IBOutlet weak var txtfieldBusinessStartDate: ColabaTextField!
    
    @IBOutlet weak var txtfieldNetAnnualIncome: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
    }
        
    //MARK:- Methods and Actions
    
    func setupTextFields(){
        txtfieldBusinessName.setTextField(placeholder: "Business Name")
        txtfieldBusinessName.setDelegates(controller: self)
        txtfieldBusinessName.setValidation(validationType: .required)
        txtfieldBusinessName.setTextField(keyboardType: .asciiCapable)
        txtfieldBusinessName.setIsValidateOnEndEditing(validate: true)
        
        txtfieldBusinessPhoneNumber.setTextField(placeholder: "Business Phone Number")
        txtfieldBusinessPhoneNumber.setDelegates(controller: self)
        txtfieldBusinessPhoneNumber.setTextField(keyboardType: .numberPad)
        txtfieldBusinessPhoneNumber.setIsValidateOnEndEditing(validate: true)
        txtfieldBusinessPhoneNumber.setValidation(validationType: .phoneNumber)
        
        txtfieldJobTitle.setTextField(placeholder: "Job Title")
        txtfieldJobTitle.setDelegates(controller: self)
        txtfieldJobTitle.setTextField(keyboardType: .asciiCapable)
        txtfieldJobTitle.setIsValidateOnEndEditing(validate: false)
        
        txtfieldBusinessStartDate.setTextField(placeholder: "Business Start Date (MM/DD/YYYY)")
        txtfieldBusinessStartDate.setDelegates(controller: self)
        txtfieldBusinessStartDate.setValidation(validationType: .required)
        txtfieldBusinessStartDate.setTextField(keyboardType: .asciiCapable)
        txtfieldBusinessStartDate.setIsValidateOnEndEditing(validate: true)
        txtfieldBusinessStartDate.type = .datePicker
        
        txtfieldNetAnnualIncome.setTextField(placeholder: "Net Annual Income")
        txtfieldNetAnnualIncome.setDelegates(controller: self)
        txtfieldNetAnnualIncome.setTextField(keyboardType: .numberPad)
        txtfieldNetAnnualIncome.setIsValidateOnEndEditing(validate: true)
        txtfieldNetAnnualIncome.setValidation(validationType: .netAnnualIncome)
        txtfieldNetAnnualIncome.type = .amount
        
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
        vc.topTitle = "Business Main Address"
        vc.searchTextFieldPlaceholder = "Search Business Address"
        self.pushToVC(vc: vc)
    }
    
    func validate() -> Bool {
        if (!txtfieldBusinessName.validate()) {
            return false
        }
        else if (txtfieldBusinessPhoneNumber.text != "" && !txtfieldBusinessPhoneNumber.validate()){
            return false
        }
        else if (!txtfieldBusinessStartDate.validate()) {
            return false
        }
        else if (!txtfieldNetAnnualIncome.validate()){
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
        txtfieldBusinessName.validate()
        txtfieldBusinessStartDate.validate()
        txtfieldNetAnnualIncome.validate()
        if (txtfieldBusinessPhoneNumber.text != ""){
            txtfieldBusinessPhoneNumber.validate()
        }
        if validate(){
            self.dismissVC()
        }
    }
}

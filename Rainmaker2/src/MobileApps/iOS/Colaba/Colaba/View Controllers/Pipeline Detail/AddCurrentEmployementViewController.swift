//
//  AddCurrentEmployementViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/09/2021.
//

import UIKit

class AddCurrentEmployementViewController: BaseViewController {

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldEmployerName: ColabaTextField!
    @IBOutlet weak var txtfieldEmployerPhoneNumber: ColabaTextField!
    @IBOutlet weak var addressView: UIView!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var txtfieldJobTitle: ColabaTextField!
    @IBOutlet weak var txtfieldStartDate: ColabaTextField!
    @IBOutlet weak var txtfieldProfessionYears: ColabaTextField!
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
        
        txtfieldEmployerPhoneNumber.setTextField(placeholder: "Employer Phone Number")
        txtfieldEmployerPhoneNumber.setDelegates(controller: self)
        txtfieldEmployerPhoneNumber.setTextField(keyboardType: .numberPad)
        txtfieldEmployerPhoneNumber.setIsValidateOnEndEditing(validate: false)
        txtfieldEmployerPhoneNumber.setValidation(validationType: .phoneNumber)
        
        txtfieldJobTitle.setTextField(placeholder: "Job Title")
        txtfieldJobTitle.setDelegates(controller: self)
        txtfieldJobTitle.setTextField(keyboardType: .asciiCapable)
        txtfieldJobTitle.setIsValidateOnEndEditing(validate: false)
        
        txtfieldStartDate.setTextField(placeholder: "Start Date (MM/DD/YYYY)")
        txtfieldStartDate.setDelegates(controller: self)
        txtfieldStartDate.setValidation(validationType: .required)
        txtfieldStartDate.setTextField(keyboardType: .asciiCapable)
        txtfieldStartDate.setIsValidateOnEndEditing(validate: true)
        
        txtfieldProfessionYears.setTextField(placeholder: "Years in Profession")
        txtfieldProfessionYears.setDelegates(controller: self)
        txtfieldProfessionYears.setTextField(keyboardType: .numberPad)
        txtfieldProfessionYears.setIsValidateOnEndEditing(validate: false)
        
        addressView.layer.cornerRadius = 6
        addressView.layer.borderWidth = 1
        addressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addressView.dropShadowToCollectionViewCell()
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.dismissVC()
    }
}

//
//  AddPreviousEmploymentViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit

class AddPreviousEmploymentViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldEmployerName: ColabaTextField!
    @IBOutlet weak var txtfieldEmployerPhoneNumber: ColabaTextField!
    @IBOutlet weak var addressView: UIView!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var txtfieldJobTitle: ColabaTextField!
    @IBOutlet weak var txtfieldProfessionYears: ColabaTextField!
    @IBOutlet weak var txtfieldStartDate: ColabaTextField!
    @IBOutlet weak var txtfieldEndDate: ColabaTextField!
    @IBOutlet weak var ownershipView: UIView!
    @IBOutlet weak var lblOwnershipQuestion: UILabel!
    @IBOutlet weak var ownershipYesStackView: UIStackView!
    @IBOutlet weak var btnOwnershipYes: UIButton!
    @IBOutlet weak var lblOwnershipYes: UILabel!
    @IBOutlet weak var ownershipNoStackView: UIStackView!
    @IBOutlet weak var btnOwnershipNo: UIButton!
    @IBOutlet weak var lblOwnershipNo: UILabel!
    @IBOutlet weak var txtfieldOwnershipPercentage: ColabaTextField!
    @IBOutlet weak var ownershipViewHeightConstraint: NSLayoutConstraint! //215 or 126
    @IBOutlet weak var txtfieldNetAnnualIncome: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var hasOwnershipInterest = true
    
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
        txtfieldEmployerPhoneNumber.setIsValidateOnEndEditing(validate: true)
        txtfieldEmployerPhoneNumber.setValidation(validationType: .phoneNumber)
        
        txtfieldJobTitle.setTextField(placeholder: "Job Title")
        txtfieldJobTitle.setDelegates(controller: self)
        txtfieldJobTitle.setTextField(keyboardType: .asciiCapable)
        txtfieldJobTitle.setIsValidateOnEndEditing(validate: false)
        
        txtfieldProfessionYears.setTextField(placeholder: "Years in Profession")
        txtfieldProfessionYears.setDelegates(controller: self)
        txtfieldProfessionYears.setTextField(keyboardType: .numberPad)
        txtfieldProfessionYears.setIsValidateOnEndEditing(validate: false)
        txtfieldProfessionYears.setTextField(maxLength: 2)
        
        txtfieldStartDate.setTextField(placeholder: "Start Date (MM/DD/YYYY)")
        txtfieldStartDate.setDelegates(controller: self)
        txtfieldStartDate.setValidation(validationType: .required)
        txtfieldStartDate.setTextField(keyboardType: .asciiCapable)
        txtfieldStartDate.setIsValidateOnEndEditing(validate: true)
        txtfieldStartDate.type = .datePicker
        
        txtfieldEndDate.setTextField(placeholder: "End Date (MM/DD/YYYY)")
        txtfieldEndDate.setDelegates(controller: self)
        txtfieldEndDate.setValidation(validationType: .required)
        txtfieldEndDate.setTextField(keyboardType: .asciiCapable)
        txtfieldEndDate.setIsValidateOnEndEditing(validate: true)
        txtfieldEndDate.type = .datePicker
        
        txtfieldOwnershipPercentage.setTextField(placeholder: "Ownership Percentage")
        txtfieldOwnershipPercentage.setDelegates(controller: self)
        txtfieldOwnershipPercentage.setTextField(keyboardType: .numberPad)
        txtfieldOwnershipPercentage.setIsValidateOnEndEditing(validate: true)
        txtfieldOwnershipPercentage.setValidation(validationType: .required)
        txtfieldOwnershipPercentage.type = .percentage
        
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
        

        
        ownershipYesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(ownershipYesStackViewTapped)))
        ownershipNoStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(ownershipNoStackViewTapped)))
        
    }
    
    func setScreenHeight(){
        
        let ownershipViewHeight = ownershipView.frame.height
        
        let totalHeight = ownershipViewHeight + 900
        self.mainViewHeightConstraint.constant = totalHeight
        
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func addressViewTapped(){
        let vc = Utility.getCurrentEmployerAddressVC()
        vc.topTitle = "Previous Employer Address"
        vc.searchTextFieldPlaceholder = "Search Main Address"
        self.pushToVC(vc: vc)
    }
    
    @objc func ownershipYesStackViewTapped(){
        hasOwnershipInterest = true
        changeOwnershipInterestStatus()
    }
    
    @objc func ownershipNoStackViewTapped(){
        hasOwnershipInterest = false
        changeOwnershipInterestStatus()
    }
    
    func changeOwnershipInterestStatus(){
        btnOwnershipYes.setImage(UIImage(named: hasOwnershipInterest ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblOwnershipYes.font = hasOwnershipInterest ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnOwnershipNo.setImage(UIImage(named: hasOwnershipInterest ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
        lblOwnershipNo.font = hasOwnershipInterest ? Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
        txtfieldOwnershipPercentage.isHidden = !hasOwnershipInterest
        ownershipViewHeightConstraint.constant = hasOwnershipInterest ? 215 : 126
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    func validate() -> Bool {
        if (!txtfieldEmployerName.validate()) {
            return false
        }
        else if (txtfieldEmployerPhoneNumber.text != "" && !txtfieldEmployerPhoneNumber.validate()){
            return false
        }
        else if (!txtfieldStartDate.validate()) {
            return false
        }
        else if (!txtfieldEndDate.validate()) {
            return false
        }
        else if (hasOwnershipInterest && !txtfieldOwnershipPercentage.validate()){
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
        txtfieldEmployerName.validate()
        txtfieldStartDate.validate()
        txtfieldEndDate.validate()
        if (txtfieldEmployerPhoneNumber.text != ""){
            txtfieldEmployerPhoneNumber.validate()
        }
        txtfieldNetAnnualIncome.validate()
        if (hasOwnershipInterest){
            txtfieldOwnershipPercentage.validate()
        }
        
        if validate(){
            self.dismissVC()
        }
    }
}


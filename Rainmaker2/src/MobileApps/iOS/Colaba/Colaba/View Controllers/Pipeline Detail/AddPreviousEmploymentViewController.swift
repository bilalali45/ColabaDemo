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
    @IBOutlet weak var addAddressView: UIView!
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
    
    var hasOwnershipInterest: Bool?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
        btnOwnershipYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblOwnershipYes.font = Theme.getRubikRegularFont(size: 14)
        txtfieldOwnershipPercentage.isHidden = true
        ownershipViewHeightConstraint.constant = 126
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
        
    //MARK:- Methods and Actions
    
    func setupTextFields(){
        txtfieldEmployerName.setTextField(placeholder: "Employer Name", controller: self, validationType: .required)
    
        txtfieldEmployerPhoneNumber.setTextField(placeholder: "Employer Phone Number", controller: self, validationType: .phoneNumber, keyboardType: .phonePad)
        txtfieldEmployerPhoneNumber.setIsValidateOnEndEditing(validate: false)
        
        txtfieldJobTitle.setTextField(placeholder: "Job Title", controller: self, validationType: .required)
        txtfieldJobTitle.setIsValidateOnEndEditing(validate: false)
        
        txtfieldProfessionYears.setTextField(placeholder: "Years in Profession", controller: self, validationType: .required)
        txtfieldProfessionYears.setIsValidateOnEndEditing(validate: false)
        txtfieldProfessionYears.setTextField(maxLength: 2)
        
        txtfieldStartDate.setTextField(placeholder: "Start Date (MM/DD/YYYY)", controller: self, validationType: .required)
        txtfieldStartDate.type = .datePicker
            
        txtfieldEndDate.setTextField(placeholder: "End Date (MM/DD/YYYY)", controller: self, validationType: .required)
        txtfieldEndDate.type = .datePicker
        
        txtfieldOwnershipPercentage.setTextField(placeholder: "Ownership Percentage", controller: self, validationType: .required)
        txtfieldOwnershipPercentage.type = .percentage
        
        txtfieldNetAnnualIncome.setTextField(placeholder: "Net Annual Income", controller: self, validationType: .netAnnualIncome)
        txtfieldNetAnnualIncome.type = .amount
        
        addressView.layer.cornerRadius = 6
        addressView.layer.borderWidth = 1
        addressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addressView.dropShadowToCollectionViewCell()
        addressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        addAddressView.layer.cornerRadius = 6
        addAddressView.layer.borderWidth = 1
        addAddressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addAddressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        ownershipYesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(ownershipYesStackViewTapped)))
        ownershipNoStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(ownershipNoStackViewTapped)))
        
    }
    
    func setScreenHeight(){
        
        let ownershipViewHeight = ownershipView.frame.height
        
        let totalHeight = ownershipViewHeight + 900
        self.mainViewHeightConstraint.constant = totalHeight
        
        UIView.animate(withDuration: 0.0) {
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
        
        if let ownershipInterest = hasOwnershipInterest{
            btnOwnershipYes.setImage(UIImage(named: ownershipInterest ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblOwnershipYes.font = ownershipInterest ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            btnOwnershipNo.setImage(UIImage(named: ownershipInterest ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
            lblOwnershipNo.font = ownershipInterest ? Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
            txtfieldOwnershipPercentage.isHidden = !ownershipInterest
            ownershipViewHeightConstraint.constant = ownershipInterest ? 215 : 126
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                self.setScreenHeight()
            }
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
        else if (!txtfieldOwnershipPercentage.isHidden && !txtfieldOwnershipPercentage.validate()){
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
        if (!txtfieldOwnershipPercentage.isHidden){
            txtfieldOwnershipPercentage.validate()
        }
        
        if validate(){
            self.dismissVC()
        }
    }
}

extension AddPreviousEmploymentViewController: ColabaTextFieldDelegate{
    
    func textFieldEndEditing(_ textField: ColabaTextField) {
        if (textField == txtfieldStartDate){
            let dateFormater = DateFormatter()
            dateFormater.dateStyle = .medium
            dateFormater.dateFormat = "MM/dd/yyyy"
            txtfieldEndDate.setMinDate(date: dateFormater.date(from: txtfieldStartDate.text!)!)
        }
        else if (textField == txtfieldEndDate){
            let dateFormater = DateFormatter()
            dateFormater.dateStyle = .medium
            dateFormater.dateFormat = "MM/dd/yyyy"
            txtfieldStartDate.setMaxDate(date: dateFormater.date(from: txtfieldEndDate.text!)!)
        }
    }
    
}

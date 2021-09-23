//
//  AddCurrentEmployementViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/09/2021.
//

import UIKit

class AddCurrentEmployementViewController: BaseViewController {

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
    @IBOutlet weak var txtfieldStartDate: ColabaTextField!
    @IBOutlet weak var txtfieldProfessionYears: ColabaTextField!
    @IBOutlet weak var employedView: UIView!
    @IBOutlet weak var lblEmployedViewQuestion: UILabel!
    @IBOutlet weak var employedViewYesStackView: UIStackView!
    @IBOutlet weak var btnEmployedYes: UIButton!
    @IBOutlet weak var lblEmployedYes: UILabel!
    @IBOutlet weak var employedViewNoStackView: UIStackView!
    @IBOutlet weak var btnEmployedNo: UIButton!
    @IBOutlet weak var lblEmployedNo: UILabel!
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
    @IBOutlet weak var payTypeView: UIView!
    @IBOutlet weak var salaryStackView: UIStackView!
    @IBOutlet weak var btnSalary: UIButton!
    @IBOutlet weak var lblSalary: UILabel!
    @IBOutlet weak var hourlyStackView: UIStackView!
    @IBOutlet weak var btnHourly: UIButton!
    @IBOutlet weak var lblHourly: UILabel!
    @IBOutlet weak var txtfieldAnnualBaseSalary: ColabaTextField!
    @IBOutlet weak var txtfieldHoursPerWeek: ColabaTextField!
    @IBOutlet weak var payTypeViewHeightConstraint: NSLayoutConstraint! // 300 or 230
    @IBOutlet weak var additionalIncomeView: UIView!
    @IBOutlet weak var bonusStackView: UIStackView!
    @IBOutlet weak var btnBonus: UIButton!
    @IBOutlet weak var lblBonus: UILabel!
    @IBOutlet weak var txtfieldAnnualBonusIncome: ColabaTextField!
    @IBOutlet weak var overtimeStackView: UIStackView!
    @IBOutlet weak var btnOvertime: UIButton!
    @IBOutlet weak var lblOvertime: UILabel!
    @IBOutlet weak var overtimeStackViewTopConstraint: NSLayoutConstraint! // 100 or 0
    @IBOutlet weak var txtfieldAnnualOvertime: ColabaTextField!
    @IBOutlet weak var commissionStackView: UIStackView!
    @IBOutlet weak var btnCommision: UIButton!
    @IBOutlet weak var lblCommission: UILabel!
    @IBOutlet weak var commissionStackViewTopConstraint: NSLayoutConstraint! //100 or 0
    @IBOutlet weak var txtfieldAnnualCommision: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var hasEmployed: Bool?
    var hasOwnershipInterest: Bool?
    var hasSalaryPayType: Bool?
    var hasBonusIncome = false
    var hasOvertimeIncome = false
    var hasCommissionIncome = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
        btnEmployedYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblEmployedYes.font = Theme.getRubikRegularFont(size: 14)
        btnOwnershipYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblOwnershipYes.font = Theme.getRubikRegularFont(size: 14)
        txtfieldOwnershipPercentage.isHidden = true
        ownershipViewHeightConstraint.constant = 126
        btnSalary.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblSalary.font = Theme.getRubikRegularFont(size: 14)
        txtfieldAnnualBaseSalary.isHidden = true
        payTypeViewHeightConstraint.constant = 160
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
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
        
        txtfieldStartDate.setTextField(placeholder: "Start Date (MM/DD/YYYY)")
        txtfieldStartDate.setDelegates(controller: self)
        txtfieldStartDate.setValidation(validationType: .required)
        txtfieldStartDate.setTextField(keyboardType: .asciiCapable)
        txtfieldStartDate.setIsValidateOnEndEditing(validate: true)
        txtfieldStartDate.type = .datePicker
        
        txtfieldProfessionYears.setTextField(placeholder: "Years in Profession")
        txtfieldProfessionYears.setDelegates(controller: self)
        txtfieldProfessionYears.setTextField(keyboardType: .numberPad)
        txtfieldProfessionYears.setIsValidateOnEndEditing(validate: false)
        txtfieldProfessionYears.setTextField(maxLength: 2)
        
        txtfieldOwnershipPercentage.setTextField(placeholder: "Ownership Percentage")
        txtfieldOwnershipPercentage.setDelegates(controller: self)
        txtfieldOwnershipPercentage.setTextField(keyboardType: .numberPad)
        txtfieldOwnershipPercentage.setIsValidateOnEndEditing(validate: true)
        txtfieldOwnershipPercentage.setValidation(validationType: .required)
        txtfieldOwnershipPercentage.type = .percentage
        
        txtfieldAnnualBaseSalary.setTextField(placeholder: "Annual Base Salary")
        txtfieldAnnualBaseSalary.setDelegates(controller: self)
        txtfieldAnnualBaseSalary.setTextField(keyboardType: .numberPad)
        txtfieldAnnualBaseSalary.setIsValidateOnEndEditing(validate: true)
        txtfieldAnnualBaseSalary.setValidation(validationType: .required)
        txtfieldAnnualBaseSalary.type = .amount
        
        txtfieldHoursPerWeek.setTextField(placeholder: "Average Hours / Week")
        txtfieldHoursPerWeek.setDelegates(controller: self)
        txtfieldHoursPerWeek.setTextField(keyboardType: .numberPad)
        txtfieldHoursPerWeek.setIsValidateOnEndEditing(validate: true)
        txtfieldHoursPerWeek.setValidation(validationType: .required)
        
        txtfieldAnnualBonusIncome.setTextField(placeholder: "Annual Bonus Income")
        txtfieldAnnualBonusIncome.setDelegates(controller: self)
        txtfieldAnnualBonusIncome.setTextField(keyboardType: .numberPad)
        txtfieldAnnualBonusIncome.setIsValidateOnEndEditing(validate: true)
        txtfieldAnnualBonusIncome.setValidation(validationType: .required)
        txtfieldAnnualBonusIncome.type = .amount
        
        txtfieldAnnualOvertime.setTextField(placeholder: "Annual Overtime Income")
        txtfieldAnnualOvertime.setDelegates(controller: self)
        txtfieldAnnualOvertime.setTextField(keyboardType: .numberPad)
        txtfieldAnnualOvertime.setIsValidateOnEndEditing(validate: true)
        txtfieldAnnualOvertime.setValidation(validationType: .required)
        txtfieldAnnualOvertime.type = .amount
        
        txtfieldAnnualCommision.setTextField(placeholder: "Annual Commission Income")
        txtfieldAnnualCommision.setDelegates(controller: self)
        txtfieldAnnualCommision.setTextField(keyboardType: .numberPad)
        txtfieldAnnualCommision.setIsValidateOnEndEditing(validate: true)
        txtfieldAnnualCommision.setValidation(validationType: .required)
        txtfieldAnnualCommision.type = .amount
        
        addressView.layer.cornerRadius = 6
        addressView.layer.borderWidth = 1
        addressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addressView.dropShadowToCollectionViewCell()
        addressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        addAddressView.layer.cornerRadius = 6
        addAddressView.layer.borderWidth = 1
        addAddressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addAddressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        employedViewYesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(employedYesStackViewTapped)))
        employedViewNoStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(employedNoStackViewTapped)))
        ownershipYesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(ownershipYesStackViewTapped)))
        ownershipNoStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(ownershipNoStackViewTapped)))
        salaryStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(salaryStackViewTapped)))
        hourlyStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(hourlyStackViewTapped)))
        bonusStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(bonusStackViewTapped)))
        overtimeStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(overtimeStackViewTapped)))
        commissionStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(commissionStackViewTapped)))
    }
    
    func setScreenHeight(){
        
        let employedViewHeight = employedView.frame.height
        let ownershipViewHeight = ownershipView.frame.height
        let payTypeViewHeight = payTypeView.frame.height
        let additionalIncomeViewHeight = additionalIncomeView.frame.height
        
        let totalHeight = employedViewHeight + ownershipViewHeight + payTypeViewHeight + additionalIncomeViewHeight + 700
        self.mainViewHeightConstraint.constant = totalHeight
        
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func addressViewTapped(){
        let vc = Utility.getCurrentEmployerAddressVC()
        vc.topTitle = "Current Employer Address"
        vc.searchTextFieldPlaceholder = "Search Main Address"
        self.pushToVC(vc: vc)
    }
    
    @objc func employedYesStackViewTapped(){
        hasEmployed = true
        changeEmployedViewStatus()
    }
    
    @objc func employedNoStackViewTapped(){
        hasEmployed = false
        changeEmployedViewStatus()
    }
    
    func changeEmployedViewStatus(){
        
        if let employed = hasEmployed{
            btnEmployedYes.setImage(UIImage(named: employed ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblEmployedYes.font = employed ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            btnEmployedNo.setImage(UIImage(named: employed ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
            lblEmployedNo.font = employed ? Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
        }
        
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
    
    @objc func salaryStackViewTapped(){
        hasSalaryPayType = true
        changePayTypeStatus()
    }
    
    @objc func hourlyStackViewTapped(){
        hasSalaryPayType = false
        changePayTypeStatus()
    }
    
    func changePayTypeStatus(){
        
        if let payTypeSalary = hasSalaryPayType{
            btnSalary.setImage(UIImage(named: payTypeSalary ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblSalary.font = payTypeSalary ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            btnHourly.setImage(UIImage(named: payTypeSalary ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
            lblHourly.font = payTypeSalary ? Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
            txtfieldAnnualBaseSalary.placeholder = payTypeSalary ? "Annual Base Salary" : "Hourly Rate"
            txtfieldAnnualBaseSalary.isHidden = false
            txtfieldHoursPerWeek.isHidden = payTypeSalary
            payTypeViewHeightConstraint.constant = payTypeSalary ? 230 : 300
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                self.setScreenHeight()
            }
        }
        
    }
    
    @objc func bonusStackViewTapped(){
        hasBonusIncome = !hasBonusIncome
        btnBonus.setImage(UIImage(named: hasBonusIncome ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblBonus.font = hasBonusIncome ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        txtfieldAnnualBonusIncome.isHidden = !hasBonusIncome
        overtimeStackViewTopConstraint.constant = hasBonusIncome ? 100 : 0
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @objc func overtimeStackViewTapped(){
        hasOvertimeIncome = !hasOvertimeIncome
        btnOvertime.setImage(UIImage(named: hasOvertimeIncome ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblOvertime.font = hasOvertimeIncome ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        txtfieldAnnualOvertime.isHidden = !hasOvertimeIncome
        commissionStackViewTopConstraint.constant = hasOvertimeIncome ? 100 : 0
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @objc func commissionStackViewTapped(){
        hasCommissionIncome = !hasCommissionIncome
        btnCommision.setImage(UIImage(named: hasCommissionIncome ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblCommission.font = hasCommissionIncome ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        txtfieldAnnualCommision.isHidden = !hasCommissionIncome
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
        else if (!txtfieldOwnershipPercentage.isHidden && !txtfieldOwnershipPercentage.validate()){
            return false
        }
        else if (!txtfieldAnnualBaseSalary.isHidden && !txtfieldAnnualBaseSalary.validate()){
            return false
        }
        else if (!txtfieldHoursPerWeek.isHidden && !txtfieldHoursPerWeek.validate()){
            return false
        }
        else if (hasBonusIncome && !txtfieldAnnualBonusIncome.validate()){
            return false
        }
        else if (hasOvertimeIncome && !txtfieldAnnualOvertime.validate()){
            return false
        }
        else if (hasCommissionIncome && !txtfieldAnnualCommision.validate()){
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
        if (txtfieldEmployerPhoneNumber.text != ""){
            txtfieldEmployerPhoneNumber.validate()
        }
        if (!txtfieldOwnershipPercentage.isHidden){
            txtfieldOwnershipPercentage.validate()
        }
        txtfieldAnnualBaseSalary.validate()
        if (!txtfieldHoursPerWeek.isHidden){
            txtfieldHoursPerWeek.validate()
        }
        if (hasBonusIncome){
            txtfieldAnnualBonusIncome.validate()
        }
        if (hasOvertimeIncome){
            txtfieldAnnualOvertime.validate()
        }
        if (hasCommissionIncome){
            txtfieldAnnualCommision.validate()
        }
        if validate(){
            self.dismissVC()
        }
    }
}

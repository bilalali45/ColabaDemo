//
//  AddCurrentEmployementViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/09/2021.
//

import UIKit
import SwiftyJSON

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
    
    var borrowerName = ""
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var incomeInfoId = 0
    var employmentDetail = EmployementDetailModel()
    var savedAddress: Any = NSNull()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
        lblUsername.text = borrowerName.uppercased()
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
        if (!isForAdd){
            getEmploymentDetail()
        }
        btnDelete.isHidden = isForAdd
        addressView.isHidden = isForAdd
        addAddressView.isHidden = !isForAdd
    }
        
    //MARK:- Methods and Actions
    
    func setupTextFields(){
        txtfieldEmployerName.setTextField(placeholder: "Employer Name", controller: self, validationType: .required)
        
        txtfieldEmployerPhoneNumber.setTextField(placeholder: "Employer Phone Number", controller: self, validationType: .phoneNumber, keyboardType: .phonePad)
        
        txtfieldJobTitle.setTextField(placeholder: "Job Title", controller: self, validationType: .required)
        txtfieldJobTitle.setIsValidateOnEndEditing(validate: false)
        
        txtfieldStartDate.setTextField(placeholder: "Start Date (MM/DD/YYYY)", controller: self, validationType: .required)
        txtfieldStartDate.type = .datePicker
        
        txtfieldProfessionYears.setTextField(placeholder: "Years in Profession", controller: self, validationType: .noValidation, keyboardType: .numberPad)
        txtfieldProfessionYears.setIsValidateOnEndEditing(validate: false)
        txtfieldProfessionYears.setTextField(maxLength: 2)
        
        txtfieldOwnershipPercentage.setTextField(placeholder: "Ownership Percentage", controller: self, validationType: .required)
        txtfieldOwnershipPercentage.type = .percentage
        
        txtfieldAnnualBaseSalary.setTextField(placeholder: "Annual Base Salary", controller: self, validationType: .required)
        txtfieldAnnualBaseSalary.type = .amount
        
        txtfieldHoursPerWeek.setTextField(placeholder: "Average Hours / Week", controller: self, validationType: .required, keyboardType: .numberPad)
        
        txtfieldAnnualBonusIncome.setTextField(placeholder: "Annual Bonus Income", controller: self, validationType: .required)
        txtfieldAnnualBonusIncome.type = .amount
        
        txtfieldAnnualOvertime.setTextField(placeholder: "Annual Overtime Income", controller: self, validationType: .required)
        txtfieldAnnualOvertime.type = .amount
        
        txtfieldAnnualCommision.setTextField(placeholder: "Annual Commission Income", controller: self, validationType: .required)
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
    
    func setEmployementDetail(){
        txtfieldEmployerName.setTextField(text: employmentDetail.employmentInfo.employerName)
        let employerPhoneNumber = formatNumber(with: "(XXX) XXX-XXXX", number: employmentDetail.employmentInfo.employerPhoneNumber)
        txtfieldEmployerPhoneNumber.setTextField(text: employerPhoneNumber)
        let address = employmentDetail.employerAddress
        lblAddress.text = "\(address.street) \(address.unit),\n\(address.city), \(address.stateName) \(address.zipCode)"
        txtfieldJobTitle.setTextField(text: employmentDetail.employmentInfo.jobTitle)
        txtfieldStartDate.setTextField(text: Utility.getDayMonthYear(employmentDetail.employmentInfo.startDate))
        txtfieldProfessionYears.setTextField(text: "\(employmentDetail.employmentInfo.yearsInProfession)")
        hasEmployed = employmentDetail.employmentInfo.employedByFamilyOrParty
        hasOwnershipInterest = employmentDetail.employmentInfo.hasOwnershipInterest
        txtfieldOwnershipPercentage.setTextField(text: "\(employmentDetail.employmentInfo.ownershipInterest)")
        hasSalaryPayType = employmentDetail.wayOfIncome.isPaidByMonthlySalary
        if (hasSalaryPayType == true){
            txtfieldAnnualBaseSalary.setTextField(text: String(format: "%.0f", employmentDetail.wayOfIncome.employerAnnualSalary.rounded()))
        }
        else{
            txtfieldAnnualBaseSalary.setTextField(text: String(format: "%.0f", employmentDetail.wayOfIncome.hourlyRate.rounded()))
            //txtfieldAnnualBaseSalary.setTextField(text: String(format: "%.0f", employmentDetail.wayOfIncome.employerAnnualSalary.rounded()))
            txtfieldHoursPerWeek.setTextField(text: "\(employmentDetail.wayOfIncome.hoursPerWeek)")
        }
        
        if let bonusIncome = employmentDetail.employmentOtherIncome.filter({$0.name.localizedCaseInsensitiveContains("Bonus")}).first{
            hasBonusIncome = true
            txtfieldAnnualBonusIncome.setTextField(text: String(format: "%.0f", bonusIncome.annualIncome.rounded()))
        }
        
        if let overtimeIncome = employmentDetail.employmentOtherIncome.filter({$0.name.localizedCaseInsensitiveContains("Overtime")}).first{
            hasOvertimeIncome = true
            txtfieldAnnualOvertime.setTextField(text: String(format: "%.0f", overtimeIncome.annualIncome.rounded()))
        }
        
        if let commissionIncome = employmentDetail.employmentOtherIncome.filter({$0.name.localizedCaseInsensitiveContains("Commission")}).first{
            hasCommissionIncome = true
            txtfieldAnnualCommision.setTextField(text: String(format: "%.0f", commissionIncome.annualIncome.rounded()))
        }
        
        changeEmployedViewStatus()
        changeOwnershipInterestStatus()
        changePayTypeStatus()
        setBonusIncomeView()
        setOvertimeIncomeView()
        setCommisionIncomeView()
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
        vc.borrowerFullName = self.borrowerName
        vc.delegate = self
        if (!isForAdd){
            vc.selectedAddress = employmentDetail.employerAddress
        }
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
        setBonusIncomeView()
    }
    
    func setBonusIncomeView(){
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
        setOvertimeIncomeView()
    }
    
    func setOvertimeIncomeView(){
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
        setCommisionIncomeView()
    }
    
    func setCommisionIncomeView(){
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
        let vc = Utility.getDeleteAddressPopupVC()
        vc.popupTitle = "Are you sure you want to remove this income source?"
        vc.screenType = 4
        vc.delegate = self
        self.present(vc, animated: false, completion: nil)
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
            addUpdateCurrentEmployment()
        }
    }
    
    //MARK:- API's
    
    func getEmploymentDetail(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerid=\(borrowerId)&incomeInfoId=\(incomeInfoId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getEmploymentDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = EmployementDetailModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.employmentDetail = model
                    if (result["data"]["employerAddress"] == JSON.null){
                        self.savedAddress = NSNull()
                    }
                    else{
                        self.savedAddress = ["street": result["data"]["employerAddress"]["street"] == JSON.null ? NSNull() : result["data"]["employerAddress"]["street"].stringValue,
                                             "unit": result["data"]["employerAddress"]["unit"] == JSON.null ? NSNull() : result["data"]["employerAddress"]["unit"].stringValue,
                                             "cityId": result["data"]["employerAddress"]["cityId"] == JSON.null ? NSNull() : result["data"]["employerAddress"]["cityId"].intValue,
                                             "city": result["data"]["employerAddress"]["city"] == JSON.null ? NSNull() : result["data"]["employerAddress"]["city"].stringValue,
                                             "stateId": result["data"]["employerAddress"]["stateId"] == JSON.null ? NSNull() : result["data"]["employerAddress"]["stateId"].intValue,
                                             "stateName": result["data"]["employerAddress"]["stateName"] == JSON.null ? NSNull() : result["data"]["employerAddress"]["stateName"].stringValue,
                                             "zipCode": result["data"]["employerAddress"]["zipCode"] == JSON.null ? NSNull() : result["data"]["employerAddress"]["zipCode"].stringValue,
                                             "countryId": result["data"]["employerAddress"]["countryId"] == JSON.null ? NSNull() : result["data"]["employerAddress"]["countryId"].intValue]as [String: Any]
                    }
                    self.setEmployementDetail()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
    
    func addUpdateCurrentEmployment(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        var employerName: Any = NSNull()
        var jobTitle: Any = NSNull()
        var startDate: Any = NSNull()
        var yearsInProfession: Any = NSNull()
        var employerPhoneNumber: Any = NSNull()
        var employedByFamily: Any = NSNull()
        var hasInterestInOwnership: Any = NSNull()
        var ownershipInterest: Any = NSNull()
        var isMonthlySalary: Any = NSNull()
        var annualSalary = 0
        var hourlyRate = 0
        var hoursPerWeek = 0
        
        if (txtfieldEmployerName.text! != ""){
            employerName = txtfieldEmployerName.text!
        }
        if (txtfieldJobTitle.text! != ""){
            jobTitle = txtfieldJobTitle.text!
        }
        let startDateComponent = txtfieldStartDate.text!.components(separatedBy: "/")
        if (startDateComponent.count == 3){
            startDate = "\(startDateComponent[2])-\(startDateComponent[0])-\(startDateComponent[1])"
        }
        if (txtfieldProfessionYears.text! != ""){
            if let years = Int(txtfieldProfessionYears.text!){
                yearsInProfession = years
            }
        }
        if (txtfieldEmployerPhoneNumber.text != ""){
            employerPhoneNumber = cleanString(string: txtfieldEmployerPhoneNumber.text!, replaceCharacters: ["(", ")", " ", "-"], replaceWith: "")
        }
        if let employedByFamilyOrParty = hasEmployed{
            employedByFamily = employedByFamilyOrParty
        }
        if let interest = hasOwnershipInterest{
            hasInterestInOwnership = interest
        }
        if (txtfieldOwnershipPercentage.text! != ""){
            if let ownership = Int(cleanString(string: txtfieldOwnershipPercentage.text!, replaceCharacters: ["%  |  ",","], replaceWith: "")){
                ownershipInterest = ownership
            }
        }
        if let hasPayTypeSalary = hasSalaryPayType{
            isMonthlySalary = hasPayTypeSalary
            if (txtfieldAnnualBaseSalary.text! != ""){
                if let value = Int(cleanString(string: txtfieldAnnualBaseSalary.text!, replaceCharacters: ["$  |  ",","], replaceWith: "")){
                    if (hasPayTypeSalary){
                        annualSalary = value
                    }
                    else{
                        hourlyRate = value
                    }
                }
            }
        }
        if (txtfieldHoursPerWeek.text! != ""){
            if let value = Int(txtfieldHoursPerWeek.text!){
                hoursPerWeek = value
            }
        }
        
        let employementInfo = ["EmployerName": employerName,
                               "JobTitle": jobTitle,
                               "StartDate": startDate,
                               "YearsInProfession": yearsInProfession,
                               "EmployerPhoneNumber": employerPhoneNumber,
                               "EmployedByFamilyOrParty": employedByFamily,
                               "HasOwnershipInterest": hasInterestInOwnership,
                               "OwnershipInterest": ownershipInterest,
                               "IncomeInfoId": isForAdd ? NSNull() : employmentDetail.employmentInfo.incomeInfoId] as [String: Any]
        
        let wayOfIncome = ["IsPaidByMonthlySalary": isMonthlySalary,
                           "EmployerAnnualSalary": annualSalary,
                           "HourlyRate": hourlyRate,
                           "HoursPerWeek": hoursPerWeek] as [String: Any]
        
        var otherIncomes = [] as [[String: Any]]
        
        if (hasBonusIncome){
            var annualIncome: Any = NSNull()
            if (txtfieldAnnualBonusIncome.text != ""){
                if let value = Double(cleanString(string: txtfieldAnnualBonusIncome.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                    annualIncome = value
                }
            }
            let otherIncome = ["IncomeTypeId": 2,
                               "AnnualIncome": annualIncome]
            otherIncomes.append(otherIncome)
        }
        
        if (hasOvertimeIncome){
            var annualIncome: Any = NSNull()
            if (txtfieldAnnualOvertime.text != ""){
                if let value = Double(cleanString(string: txtfieldAnnualOvertime.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                    annualIncome = value
                }
            }
            let otherIncome = ["IncomeTypeId": 1,
                               "AnnualIncome": annualIncome]
            otherIncomes.append(otherIncome)
        }
        
        if (hasCommissionIncome){
            var annualIncome: Any = NSNull()
            if (txtfieldAnnualCommision.text != ""){
                if let value = Double(cleanString(string: txtfieldAnnualCommision.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                    annualIncome = value
                }
            }
            let otherIncome = ["IncomeTypeId": 3,
                               "AnnualIncome": annualIncome]
            otherIncomes.append(otherIncome)
        }
        
        let params = ["BorrowerId": borrowerId,
                      "LoanApplicationId": loanApplicationId,
                      "EmploymentInfo": employementInfo,
                      "EmployerAddress": savedAddress,
                      "WayOfIncome": wayOfIncome,
                      "EmploymentOtherIncomes": otherIncomes] as [String:Any]
        
        APIRouter.sharedInstance.executeAPI(type: .addUpdateCurrentEmployment, method: .post, params: params) { status, result, message in
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    self.dismissVC()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
        }
        
    }
    
    func deleteIncome(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "IncomeInfoId=\(incomeInfoId)&borrowerId=\(borrowerId)&loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeAPI(type: .deleteIncome, method: .delete, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    self.dismissVC()
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

extension AddCurrentEmployementViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        deleteIncome()
    }
}

extension AddCurrentEmployementViewController: CurrentEmployerAddressViewControllerDelegate{
    func saveAddressObject(address: [String : Any]) {
        savedAddress = address
        addressView.isHidden = false
        addAddressView.isHidden = true
        
        var street = "", unit = "", city = "", stateName = "", zipCode = ""
        if let addressStreet = address["street"] as? String{
            street = addressStreet
        }
        if let addressUnit = address["unit"] as? String{
            unit = addressUnit
        }
        if let addressCity = address["city"] as? String{
            city = addressCity
        }
        if let addressState = address["stateName"] as? String{
            stateName = addressState
        }
        if let addressZipCode = address["zipCode"] as? String{
            zipCode = addressZipCode
        }
        lblAddress.text = "\(street) \(unit),\n\(city), \(stateName) \(zipCode)"
        
        if let stateId = address["stateId"] as? Int{
            employmentDetail.employerAddress.stateId = stateId
        }
        if let countryId = address["countryId"] as? Int{
            employmentDetail.employerAddress.countryId = countryId
        }
        if let countryName = address["countryName"] as? String{
            employmentDetail.employerAddress.countryName = countryName
        }
        if let countyId = address["countyId"] as? Int{
            employmentDetail.employerAddress.countyId = countyId
        }
        if let countyName = address["countyName"] as? String{
            employmentDetail.employerAddress.countyName = countyName
        }
        employmentDetail.employerAddress.street = street
        employmentDetail.employerAddress.unit = unit
        employmentDetail.employerAddress.city = city
        employmentDetail.employerAddress.stateName = stateName
        employmentDetail.employerAddress.zipCode = zipCode
    }
}

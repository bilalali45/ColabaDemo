//
//  AddPreviousEmploymentViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit
import SwiftyJSON

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
        btnOwnershipYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblOwnershipYes.font = Theme.getRubikRegularFont(size: 14)
        txtfieldOwnershipPercentage.isHidden = true
        ownershipViewHeightConstraint.constant = 126
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
    
    func setEmployementDetail(){
        txtfieldEmployerName.setTextField(text: employmentDetail.employmentInfo.employerName)
        let employerPhoneNumber = formatNumber(with: "(XXX) XXX-XXXX", number: employmentDetail.employmentInfo.employerPhoneNumber)
        txtfieldEmployerPhoneNumber.setTextField(text: employerPhoneNumber)
        let address = employmentDetail.employerAddress
        lblAddress.text = "\(address.street) \(address.unit),\n\(address.city), \(address.stateName) \(address.zipCode)"
        txtfieldJobTitle.setTextField(text: employmentDetail.employmentInfo.jobTitle)
        txtfieldStartDate.setTextField(text: Utility.getDayMonthYear(employmentDetail.employmentInfo.startDate))
        txtfieldEndDate.setTextField(text: Utility.getDayMonthYear(employmentDetail.employmentInfo.endDate))
        txtfieldProfessionYears.setTextField(text: "\(employmentDetail.employmentInfo.yearsInProfession)")
    
        hasOwnershipInterest = employmentDetail.employmentInfo.hasOwnershipInterest
        txtfieldOwnershipPercentage.setTextField(text: "\(employmentDetail.employmentInfo.ownershipInterest)")
        txtfieldNetAnnualIncome.setTextField(text: String(format: "%.0f", employmentDetail.wayOfIncome.employerAnnualSalary))
        changeOwnershipInterestStatus()
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
        vc.borrowerFullName = self.borrowerName
        vc.delegate = self
        if (!isForAdd){
            vc.selectedAddress = employmentDetail.employerAddress
        }
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
        let vc = Utility.getDeleteAddressPopupVC()
        vc.popupTitle = "Are you sure you want to remove this income source?"
        vc.screenType = 4
        vc.delegate = self
        self.present(vc, animated: false, completion: nil)
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
            addUpdatePreviousEmployment()
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
    
    func addUpdatePreviousEmployment(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        var employerName: Any = NSNull()
        var jobTitle: Any = NSNull()
        var startDate: Any = NSNull()
        var endDate: Any = NSNull()
        var yearsInProfession: Any = NSNull()
        var employerPhoneNumber: Any = NSNull()
        var hasInterestInOwnership: Any = NSNull()
        var ownershipInterest: Any = NSNull()
        var annualSalary = 0
        
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
        let endDateComponent = txtfieldEndDate.text!.components(separatedBy: "/")
        if (endDateComponent.count == 3){
            endDate = "\(endDateComponent[2])-\(endDateComponent[0])-\(endDateComponent[1])"
        }
        if (txtfieldProfessionYears.text! != ""){
            if let years = Int(txtfieldProfessionYears.text!){
                yearsInProfession = years
            }
        }
        if (txtfieldEmployerPhoneNumber.text != ""){
            employerPhoneNumber = cleanString(string: txtfieldEmployerPhoneNumber.text!, replaceCharacters: ["(", ")", " ", "-"], replaceWith: "")
        }
        if let interest = hasOwnershipInterest{
            hasInterestInOwnership = interest
        }
        if (txtfieldOwnershipPercentage.text! != ""){
            if let ownership = Int(cleanString(string: txtfieldOwnershipPercentage.text!, replaceCharacters: ["%  |  ",","], replaceWith: "")){
                ownershipInterest = ownership
            }
        }
        if (txtfieldNetAnnualIncome.text! != ""){
            if let value = Int(cleanString(string: txtfieldNetAnnualIncome.text!, replaceCharacters: ["$  |  ",","], replaceWith: "")){
                annualSalary = value
            }
        }
        
        let employementInfo = ["EmployerName": employerName,
                               "JobTitle": jobTitle,
                               "StartDate": startDate,
                               "EndDate": endDate,
                               "YearsInProfession": yearsInProfession,
                               "EmployerPhoneNumber": employerPhoneNumber,
                               "HasOwnershipInterest": hasInterestInOwnership,
                               "OwnershipInterest": ownershipInterest,
                               "IncomeInfoId": isForAdd ? NSNull() : employmentDetail.employmentInfo.incomeInfoId] as [String: Any]
        
        let wayOfIncome = ["EmployerAnnualSalary": annualSalary] as [String: Any]
        
        let params = ["BorrowerId": borrowerId,
                      "LoanApplicationId": loanApplicationId,
                      "EmploymentInfo": employementInfo,
                      "EmployerAddress": savedAddress,
                      "WayOfIncome": wayOfIncome] as [String:Any]
        
        APIRouter.sharedInstance.executeAPI(type: .addUpdatePreviousEmployment, method: .post, params: params) { status, result, message in
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

extension AddPreviousEmploymentViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        deleteIncome()
    }
}

extension AddPreviousEmploymentViewController: CurrentEmployerAddressViewControllerDelegate{
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

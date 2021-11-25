//
//  AddMilitaryPayViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit
import SwiftyJSON

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
    @IBOutlet weak var addAddressView: UIView!
    @IBOutlet weak var txtfieldJobTitle: ColabaTextField!
    @IBOutlet weak var txtfieldProfessionYears: ColabaTextField!
    @IBOutlet weak var txtfieldStartDate: ColabaTextField!
    @IBOutlet weak var txtfieldMonthlyBaseSalary: ColabaTextField!
    @IBOutlet weak var txtfieldMilitaryEntitlements: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var borrowerName = ""
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var incomeInfoId = 0
    var militaryDetail = MilitaryPayDetailModel()
    var savedAddress: Any = NSNull()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
        lblUsername.text = borrowerName.uppercased()
        if (!isForAdd){
            getMilitaryPayDetail()
        }
        btnDelete.isHidden = isForAdd
        addressView.isHidden = isForAdd
        addAddressView.isHidden = !isForAdd
    }
        
    //MARK:- Methods and Actions
    
    func setupTextFields(){
        /// Text Field
        txtfieldEmployerName.setTextField(placeholder: "Employer Name", controller: self, validationType: .required)
        
        /// Text Field
        txtfieldJobTitle.setTextField(placeholder: "Job Title", controller: self, validationType: .required)
        
        /// Text Field
        txtfieldProfessionYears.setTextField(placeholder: "Years in Profession", controller: self, validationType: .required, keyboardType: .numberPad)
        
        /// Text Field
        txtfieldStartDate.setTextField(placeholder: "Start Date (MM/DD/YYYY)", controller: self, validationType: .required)
        txtfieldStartDate.type = .datePicker
        
        /// Text Field
        txtfieldMonthlyBaseSalary.setTextField(placeholder: "Monthly Base Salary", controller: self, validationType: .required)
        txtfieldMonthlyBaseSalary.type = .amount
        
        /// Text Field
        txtfieldMilitaryEntitlements.setTextField(placeholder: "Military Entitlements", controller: self, validationType: .required)
        txtfieldMilitaryEntitlements.type = .amount
        
        addressView.layer.cornerRadius = 6
        addressView.layer.borderWidth = 1
        addressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addressView.dropShadowToCollectionViewCell()
        addressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        addAddressView.layer.cornerRadius = 6
        addAddressView.layer.borderWidth = 1
        addAddressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addAddressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
    }
    
    func setMilitaryPayDetail(){
        txtfieldEmployerName.setTextField(text: militaryDetail.employerName)
        let address = militaryDetail.address
        lblAddress.text = "\(address.street) \(address.unit),\n\(address.city), \(address.stateName) \(address.zipCode)"
        txtfieldJobTitle.setTextField(text: militaryDetail.jobTitle)
        txtfieldProfessionYears.setTextField(text: "\(militaryDetail.yearsInProfession)")
        txtfieldStartDate.setTextField(text: Utility.getDayMonthYear(militaryDetail.startDate))
        txtfieldMonthlyBaseSalary.setTextField(text: String(format: "%.0f", militaryDetail.monthlyBaseSalary))
        txtfieldMilitaryEntitlements.setTextField(text: String(format: "%.0f", militaryDetail.militaryEntitlements))
    }
    
    @objc func addressViewTapped(){
        let vc = Utility.getCurrentEmployerAddressVC()
        vc.topTitle = "Service Location Address"
        vc.searchTextFieldPlaceholder = "Search Service Location Address"
        vc.borrowerFullName = self.borrowerName
        vc.delegate = self
        if (!isForAdd){
            vc.selectedAddress = militaryDetail.address
        }
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
        let vc = Utility.getDeleteAddressPopupVC()
        vc.popupTitle = "Are you sure you want to remove this income source?"
        vc.screenType = 4
        vc.delegate = self
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        txtfieldEmployerName.validate()
        txtfieldJobTitle.validate()
        txtfieldProfessionYears.validate()
        txtfieldStartDate.validate()
        txtfieldMonthlyBaseSalary.validate()
        txtfieldMilitaryEntitlements.validate()
        
        if validate(){
            addUpdateMilitaryPay()
        }
    }
    
    //MARK:- API's
    
    func getMilitaryPayDetail(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerid=\(borrowerId)&incomeInfoId=\(incomeInfoId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getMilitaryPayDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = MilitaryPayDetailModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.militaryDetail = model
                    self.setMilitaryPayDetail()
                    if (result["data"]["address"] == JSON.null){
                        self.savedAddress = NSNull()
                    }
                    else{
                        self.savedAddress = ["street": result["data"]["address"]["street"] == JSON.null ? NSNull() : result["data"]["address"]["street"].stringValue,
                                             "unit": result["data"]["address"]["unit"] == JSON.null ? NSNull() : result["data"]["address"]["unit"].stringValue,
                                             "cityId": result["data"]["address"]["cityId"] == JSON.null ? NSNull() : result["data"]["address"]["cityId"].intValue,
                                             "city": result["data"]["address"]["city"] == JSON.null ? NSNull() : result["data"]["address"]["city"].stringValue,
                                             "stateId": result["data"]["address"]["stateId"] == JSON.null ? NSNull() : result["data"]["address"]["stateId"].intValue,
                                             "stateName": result["data"]["address"]["stateName"] == JSON.null ? NSNull() : result["data"]["address"]["stateName"].stringValue,
                                             "zipCode": result["data"]["address"]["zipCode"] == JSON.null ? NSNull() : result["data"]["address"]["zipCode"].stringValue,
                                             "countryId": result["data"]["address"]["countryId"] == JSON.null ? NSNull() : result["data"]["address"]["countryId"].intValue,
                                             "countryName": result["data"]["address"]["countryName"] == JSON.null ? NSNull() : result["data"]["address"]["countryName"].stringValue]as [String: Any]
                    }
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
    
    func addUpdateMilitaryPay(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        var employerName: Any = NSNull()
        var jobTitle: Any = NSNull()
        var startDate = ""
        var yearsInProfession: Any = NSNull()
        var monthlyBaseSalary: Any = NSNull()
        var militaryEntitlements: Any = NSNull()
        
        if (txtfieldEmployerName.text! != ""){
            employerName = txtfieldEmployerName.text!
        }
        
        let startDateComponent = txtfieldStartDate.text!.components(separatedBy: "/")
        if (startDateComponent.count == 3){
            startDate = "\(startDateComponent[2])-\(startDateComponent[0])-\(startDateComponent[1])"
        }
        if (txtfieldJobTitle.text! != ""){
            jobTitle = txtfieldJobTitle.text!
        }
        if (txtfieldProfessionYears.text! != ""){
            if let years = Int(txtfieldProfessionYears.text!){
                yearsInProfession = years
            }
        }
        if (txtfieldMonthlyBaseSalary.text! != ""){
            if let value = Int(cleanString(string: txtfieldMonthlyBaseSalary.text!, replaceCharacters: ["$  |  ",","], replaceWith: "")){
                monthlyBaseSalary = value
            }
        }
        if (txtfieldMilitaryEntitlements.text! != ""){
            if let value = Int(cleanString(string: txtfieldMilitaryEntitlements.text!, replaceCharacters: ["$  |  ",","], replaceWith: "")){
                militaryEntitlements = value
            }
        }
        
        let params = ["loanApplicationId": loanApplicationId,
                      "id": isForAdd ? NSNull() : militaryDetail.id,
                      "borrowerId": borrowerId,
                      "employerName": employerName,
                      "jobTitle": jobTitle,
                      "startDate": startDate,
                      "yearsInProfession": yearsInProfession,
                      "address": savedAddress,
                      "monthlyBaseSalary": monthlyBaseSalary,
                      "militaryEntitlements": militaryEntitlements] as [String: Any]
        
        APIRouter.sharedInstance.executeAPI(type: .addUpdateMilitaryPay, method: .post, params: params) { status, result, message in
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

extension AddMilitaryPayViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        deleteIncome()
    }
}

extension AddMilitaryPayViewController: CurrentEmployerAddressViewControllerDelegate{
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
            militaryDetail.address.stateId = stateId
        }
        if let countryId = address["countryId"] as? Int{
            militaryDetail.address.countryId = countryId
        }
        if let countryName = address["countryName"] as? String{
            militaryDetail.address.countryName = countryName
        }
        if let countyId = address["countyId"] as? Int{
            militaryDetail.address.countyId = countyId
        }
        if let countyName = address["countyName"] as? String{
            militaryDetail.address.countyName = countyName
        }
        militaryDetail.address.street = street
        militaryDetail.address.unit = unit
        militaryDetail.address.city = city
        militaryDetail.address.stateName = stateName
        militaryDetail.address.zipCode = zipCode
    }
}

//
//  AddBusinessViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit
import SwiftyJSON

class AddBusinessViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldBusinessType: ColabaTextField!
    @IBOutlet weak var txtfieldBusinessName: ColabaTextField!
    @IBOutlet weak var txtfieldBusinessPhoneNumber: ColabaTextField!
    @IBOutlet weak var txtfieldBusinessStartDate: ColabaTextField!
    @IBOutlet weak var addressView: UIView!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var addAddressView: UIView!
    @IBOutlet weak var txtfieldJobTitle: ColabaTextField!
    @IBOutlet weak var txtfieldOwnershipPercentage: ColabaTextField!
    @IBOutlet weak var txtfieldNetAnnualIncome: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var borrowerName = ""
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var incomeInfoId = 0
    var businessTypeArray = [DropDownModel]()
    var businessDetail = BusinessDetailModel()
    var savedAddress: Any = NSNull()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
        lblUsername.text = borrowerName.uppercased()
        getBusinessType()
        btnDelete.isHidden = isForAdd
        addressView.isHidden = isForAdd
        addAddressView.isHidden = !isForAdd
    }
        
    //MARK:- Methods and Actions
    func setupTextFields(){
        
        txtfieldBusinessType.setTextField(placeholder: "Select Your Business Type", controller: self, validationType: .required)
        txtfieldBusinessType.type = .dropdown
        
        txtfieldBusinessName.setTextField(placeholder: "Business Name", controller: self, validationType: .required)
        
        txtfieldBusinessPhoneNumber.setTextField(placeholder: "Business Phone Number", controller: self, validationType: .phoneNumber, keyboardType: .phonePad)
        
        txtfieldBusinessStartDate.setTextField(placeholder: "Business Start Date (MM/DD/YYYY)", controller: self, validationType: .required)
        txtfieldBusinessStartDate.type = .datePicker
        
        txtfieldJobTitle.setTextField(placeholder: "Job Title", controller: self, validationType: .required)
        txtfieldJobTitle.setIsValidateOnEndEditing(validate: false)
        
        txtfieldOwnershipPercentage.setTextField(placeholder: "Ownership Percentage", controller: self, validationType: .required)
        txtfieldOwnershipPercentage.type = .percentage
        
        txtfieldNetAnnualIncome.setTextField(placeholder: "Net Annual Income", controller: self, validationType: .required)
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
    }
    
    func setBusinessDetail(){
        if let businessType = businessTypeArray.filter({$0.optionId == businessDetail.incomeTypeId}).first{
            txtfieldBusinessType.setTextField(text: businessType.optionName)
        }
        txtfieldBusinessName.setTextField(text: businessDetail.businessName)
        let businessPhoneNumber = formatNumber(with: "(XXX) XXX-XXXX", number: businessDetail.businessPhone)
        txtfieldBusinessPhoneNumber.setTextField(text: businessPhoneNumber)
        txtfieldBusinessStartDate.setTextField(text: Utility.getDayMonthYear(businessDetail.startDate))
        let address = businessDetail.address
        lblAddress.text = "\(address.street) \(address.unit),\n\(address.city), \(address.stateName) \(address.zipCode)"
        txtfieldJobTitle.setTextField(text: businessDetail.jobTitle)
        txtfieldOwnershipPercentage.setTextField(text: "\(businessDetail.ownershipPercentage)")
        txtfieldNetAnnualIncome.setTextField(text: String(format: "%.0f", businessDetail.annualIncome))
    }
    
    @objc func addressViewTapped(){
        let vc = Utility.getCurrentEmployerAddressVC()
        vc.topTitle = "Business Main Address"
        vc.searchTextFieldPlaceholder = "Search Business Address"
        vc.borrowerFullName = self.borrowerName
        vc.delegate = self
        if (!isForAdd){
            vc.selectedAddress = businessDetail.address
        }
        self.pushToVC(vc: vc)
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldBusinessType.validate()
        isValidate = txtfieldBusinessName.validate() && isValidate
        isValidate = txtfieldBusinessStartDate.validate() && isValidate
        isValidate = txtfieldNetAnnualIncome.validate() && isValidate
        return isValidate
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
        if validate(){
            addUpdateBusiness()
        }
    }
    
    //MARK:- API's
    
    func getBusinessType(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllBusinessType, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option)
                        self.businessTypeArray.append(model)
                    }
                    self.txtfieldBusinessType.setDropDownDataSource(self.businessTypeArray.map({$0.optionName}))
                    if (self.isForAdd){
                        Utility.showOrHideLoader(shouldShow: false)
                    }
                    else{
                        self.getBusinessDetail()
                    }
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
    
    func getBusinessDetail(){
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerid=\(borrowerId)&incomeInfoId=\(incomeInfoId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getBusinessIncomeDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = BusinessDetailModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.businessDetail = model
                    self.setBusinessDetail()
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
    
    func addUpdateBusiness(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        var businessName: Any = NSNull()
        var businessPhoneNumber: Any = NSNull()
        var startDate = ""
        var jobTitle: Any = NSNull()
        var annualIncome: Any = NSNull()
        var incomeTypeId = 0
        var ownershipInterest = 0
        
        if let businessType = (businessTypeArray.filter({$0.optionName.localizedCaseInsensitiveContains(txtfieldBusinessType.text!)})).first{
            incomeTypeId = businessType.optionId
        }
        
        if (txtfieldBusinessName.text! != ""){
            businessName = txtfieldBusinessName.text!
        }
        if (txtfieldBusinessPhoneNumber.text != ""){
            businessPhoneNumber = cleanString(string: txtfieldBusinessPhoneNumber.text!, replaceCharacters: ["(", ")", " ", "-"], replaceWith: "")
        }
        let startDateComponent = txtfieldBusinessStartDate.text!.components(separatedBy: "/")
        if (startDateComponent.count == 3){
            startDate = "\(startDateComponent[2])-\(startDateComponent[0])-\(startDateComponent[1])"
        }
        if (txtfieldJobTitle.text! != ""){
            jobTitle = txtfieldJobTitle.text!
        }
        if (txtfieldNetAnnualIncome.text! != ""){
            if let value = Int(cleanString(string: txtfieldNetAnnualIncome.text!, replaceCharacters: ["$  |  ",","], replaceWith: "")){
                annualIncome = value
            }
        }
        if (txtfieldOwnershipPercentage.text! != ""){
            if let ownership = Int(cleanString(string: txtfieldOwnershipPercentage.text!, replaceCharacters: ["%  |  ",","], replaceWith: "")){
                ownershipInterest = ownership
            }
        }
        
        let params = ["loanApplicationId": loanApplicationId,
                      "id": isForAdd ? NSNull() : businessDetail.id,
                      "borrowerId": borrowerId,
                      "incomeTypeId": incomeTypeId,
                      "businessName": businessName,
                      "businessPhone": businessPhoneNumber,
                      "startDate": "\(startDate)T00:00:00",
                      "jobTitle": jobTitle,
                      "ownershipPercentage": ownershipInterest,
                      "address": savedAddress,
                      "annualIncome": annualIncome] as [String: Any]
        
        APIRouter.sharedInstance.executeAPI(type: .addUpdateBusiness, method: .post, params: params) { status, result, message in
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

extension AddBusinessViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        deleteIncome()
    }
}

extension AddBusinessViewController: CurrentEmployerAddressViewControllerDelegate{
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
    }
}

//
//  RealEstateViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit
import SwiftyJSON

class RealEstateViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var addressView: UIView!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var addAddressView: UIView!
    @IBOutlet weak var txtfieldPropertyType: ColabaTextField!
    @IBOutlet weak var txtfieldOccupancyType: ColabaTextField!
    @IBOutlet weak var txtfieldCurrentRentalIncome: ColabaTextField!
    @IBOutlet weak var txtfieldRentalIncomeTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldRentalIncomeHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldPropertyStatus: ColabaTextField!
    @IBOutlet weak var txtfieldHomeOwnerAssociationDues: ColabaTextField!
    @IBOutlet weak var txtfieldPropertyValue: ColabaTextField!
    @IBOutlet weak var txtfieldAnnualPropertyTax: ColabaTextField!
    @IBOutlet weak var txtfieldAnnualHomeOwnerInsurance: ColabaTextField!
    @IBOutlet weak var txtfieldAnnualFloodInsurance: ColabaTextField!
    @IBOutlet weak var firstMortgageMainView: UIView!
    @IBOutlet weak var firstMortgageMainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var firstMortgageYesStackView: UIStackView!
    @IBOutlet weak var btnFirstMortgageYes: UIButton!
    @IBOutlet weak var lblFirstMortgageYes: UILabel!
    @IBOutlet weak var firstMortgageNoStackView: UIStackView!
    @IBOutlet weak var btnFirstMortgageNo: UIButton!
    @IBOutlet weak var lblFirstMortgageNo: UILabel!
    @IBOutlet weak var firstMortgageView: UIView!
    @IBOutlet weak var lblFirstMortgagePayment: UILabel!
    @IBOutlet weak var lblFirstMortgageBalance: UILabel!
    @IBOutlet weak var secondMortgageMainView: UIView!
    @IBOutlet weak var secondMortgageMainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var secondMortgageYesStackView: UIStackView!
    @IBOutlet weak var btnSecondMortgageYes: UIButton!
    @IBOutlet weak var lblSecondMortgageYes: UILabel!
    @IBOutlet weak var secondMortgageNoStackView: UIStackView!
    @IBOutlet weak var btnSecondMortgageNo: UIButton!
    @IBOutlet weak var lblSecondMortgageNo: UILabel!
    @IBOutlet weak var secondMortgageView: UIView!
    @IBOutlet weak var lblSecondMortgagePayment: UILabel!
    @IBOutlet weak var lblSecondMortgageBalance: UILabel!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var borrowerPropertyId = 0
    var borrowerFullName = ""
    
    var propertyTypeArray = [DropDownModel]()
    var occupancyTypeArray = [DropDownModel]()
    var propertyStatusArray = [DropDownModel]()
    var realEstateDetail = RealEstateDetailModel()
    
    var isFirstMortgage = false
    var isSecondMortgage = false
    
    var savedAddress: Any = NSNull()
    var savedFirstMortgage: Any = NSNull()
    var savedSecondMortgage: Any = NSNull()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
        btnDelete.isHidden = isForAdd
        addressView.isHidden = isForAdd
        addAddressView.isHidden = !isForAdd
        getPropertyTypeDropDown()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
    }

    //MARK:- Methods and Actions
    
    func setupTextFields(){
        
        lblUsername.text = borrowerFullName.uppercased()
        
        addressView.layer.cornerRadius = 6
        addressView.layer.borderWidth = 1
        addressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addressView.dropShadowToCollectionViewCell()
        addressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        addAddressView.layer.cornerRadius = 6
        addAddressView.layer.borderWidth = 1
        addAddressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addAddressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        txtfieldPropertyType.setTextField(placeholder: "Property Type", controller: self, validationType: .required)
        txtfieldPropertyType.type = .dropdown
        
        txtfieldOccupancyType.setTextField(placeholder: "Occupancy Type", controller: self, validationType: .required)
        txtfieldOccupancyType.type = .dropdown
        
        txtfieldCurrentRentalIncome.setTextField(placeholder: "Current Rental Income", controller: self, validationType: .required)
        txtfieldCurrentRentalIncome.type = .amount
        
        txtfieldPropertyStatus.setTextField(placeholder: "Property Status", controller: self, validationType: .required)
        txtfieldPropertyStatus.type = .dropdown
        
        txtfieldHomeOwnerAssociationDues.setTextField(placeholder: "Homeowner’s Association Dues", controller: self, validationType: .required)
        txtfieldHomeOwnerAssociationDues.type = .amount
        
        txtfieldPropertyValue.setTextField(placeholder: "Property Value", controller: self, validationType: .required)
        txtfieldPropertyValue.type = .amount
        
        txtfieldAnnualPropertyTax.setTextField(placeholder: "Annual Property Taxes", controller: self, validationType: .required)
        txtfieldAnnualPropertyTax.type = .amount
        
        txtfieldAnnualHomeOwnerInsurance.setTextField(placeholder: "Annual Homeowner's Insurance", controller: self, validationType: .required)
        txtfieldAnnualHomeOwnerInsurance.type = .amount
        
        txtfieldAnnualFloodInsurance.setTextField(placeholder: "Annual Flood Insurance", controller: self, validationType: .required)
        txtfieldAnnualFloodInsurance.type = .amount
        
        firstMortgageYesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(firstMortgageYesStackViewTapped)))
        firstMortgageNoStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(firstMortgageNoStackViewTapped)))
        firstMortgageView.layer.cornerRadius = 6
        firstMortgageView.layer.borderWidth = 1
        firstMortgageView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        firstMortgageView.dropShadowToCollectionViewCell()
        firstMortgageView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(firstMortgageViewTapped)))
        
        secondMortgageYesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(secondMortgageYesStackViewTapped)))
        secondMortgageNoStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(secondMortgageNoStackViewTapped)))
        secondMortgageView.layer.cornerRadius = 6
        secondMortgageView.layer.borderWidth = 1
        secondMortgageView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        secondMortgageView.dropShadowToCollectionViewCell()
        secondMortgageView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(secondMortgageViewTapped)))
        
    }
    
    func setRealEstateDetail(){
        if let subjectPropertyAddress = self.realEstateDetail.address{
            if (subjectPropertyAddress.street != ""){
                lblAddress.text = "\(subjectPropertyAddress.street) \(subjectPropertyAddress.unit),\n\(subjectPropertyAddress.city), \(subjectPropertyAddress.stateName) \(subjectPropertyAddress.zipCode)"
            }
        }
        
        addressView.isHidden = self.realEstateDetail.address?.street == ""
        addAddressView.isHidden = self.realEstateDetail.address?.street != ""
        
        if let propertyType = self.propertyTypeArray.filter({$0.optionId == self.realEstateDetail.propertyTypeId}).first{
            self.txtfieldPropertyType.setTextField(text: propertyType.optionName)
        }
        if let occupancyType = self.occupancyTypeArray.filter({$0.optionId == self.realEstateDetail.occupancyTypeId}).first{
            self.txtfieldOccupancyType.setTextField(text: occupancyType.optionName)
        }
        if let propertyStatus = self.propertyStatusArray.filter({$0.optionId == self.realEstateDetail.propertyStatus}).first{
            self.txtfieldPropertyStatus.setTextField(text: propertyStatus.optionName)
        }
        
        txtfieldCurrentRentalIncome.setTextField(text: String(format: "%.0f", self.realEstateDetail.rentalIncome.rounded()))
        txtfieldHomeOwnerAssociationDues.setTextField(text: String(format: "%.0f", self.realEstateDetail.homeOwnerDues.rounded()))
        txtfieldPropertyValue.setTextField(text: String(format: "%.0f", self.realEstateDetail.propertyValue.rounded()))
        txtfieldAnnualPropertyTax.setTextField(text: String(format: "%.0f", self.realEstateDetail.annualPropertyTax.rounded()))
        txtfieldAnnualHomeOwnerInsurance.setTextField(text: String(format: "%.0f", self.realEstateDetail.annualHomeInsurance.rounded()))
        txtfieldAnnualFloodInsurance.setTextField(text: String(format: "%.0f", self.realEstateDetail.annualFloodInsurance.rounded()))
        isFirstMortgage = self.realEstateDetail.hasFirstMortgage
        isSecondMortgage = self.realEstateDetail.hasSecondMortgage
        
        if let firstMortgage = self.realEstateDetail.firstMortgage{
            lblFirstMortgagePayment.text = Int(firstMortgage.firstMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
            lblFirstMortgageBalance.text = Int(firstMortgage.unpaidFirstMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
        }
        
        if let secondMortgage = self.realEstateDetail.secondMortgage{
            lblSecondMortgagePayment.text = Int(secondMortgage.secondMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
            lblSecondMortgageBalance.text = Int(secondMortgage.unpaidSecondMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
        }
        
        showHideRentalIncome()
        changeMortgageStatus()
    }
    
    func setScreenHeight(){
        let firstMortgageViewHeight = self.firstMortgageMainView.frame.height
        let secondMortgageViewHeight = self.secondMortgageMainView.frame.height
        
        self.mainViewHeightConstraint.constant = firstMortgageViewHeight + secondMortgageViewHeight + 1000
        
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func addressViewTapped(){
        let vc = Utility.getCurrentEmployerAddressVC()
        vc.isForSubjectProperty = true
        vc.topTitle = "Subject Property Address"
        vc.searchTextFieldPlaceholder = "Search Property Address"
        vc.borrowerFullName = self.borrowerFullName
        vc.delegate = self
        if (!isForAdd){
            vc.selectedAddress = self.realEstateDetail.address
        }
        self.pushToVC(vc: vc)
    }
    
    @objc func showHideRentalIncome(){
        if (txtfieldOccupancyType.text == "Investment Property"){
            txtfieldRentalIncomeTopConstraint.constant = 30
            txtfieldRentalIncomeHeightConstraint.constant = 39
            txtfieldCurrentRentalIncome.isHidden = false
            txtfieldCurrentRentalIncome.resignFirstResponder()
        }
        else if ( (txtfieldOccupancyType.text == "Primary Residence") && (txtfieldPropertyType.text == "Duplex (2 Unit)" || txtfieldPropertyType.text == "Triplex (3 Unit)" || txtfieldPropertyType.text == "Quadplex (4 Unit)") ){
            txtfieldRentalIncomeTopConstraint.constant = 30
            txtfieldRentalIncomeHeightConstraint.constant = 39
            txtfieldCurrentRentalIncome.isHidden = false
            txtfieldCurrentRentalIncome.resignFirstResponder()
        }
        else{
            txtfieldRentalIncomeTopConstraint.constant = 0
            txtfieldRentalIncomeHeightConstraint.constant = 0
            txtfieldCurrentRentalIncome.isHidden = true
            txtfieldCurrentRentalIncome.resignFirstResponder()
        }
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
        
    }
    
    @objc func firstMortgageYesStackViewTapped(){
        isFirstMortgage = true
        let vc = Utility.getFirstMortgageFollowupQuestionsVC()
        vc.isForRealEstate = true
        vc.mortgageDetail = self.realEstateDetail.firstMortgage
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func firstMortgageNoStackViewTapped(){
        isFirstMortgage = false
        isSecondMortgage = false
        changeMortgageStatus()
    }
    
    @objc func firstMortgageViewTapped(){
        let vc = Utility.getFirstMortgageFollowupQuestionsVC()
        vc.isForRealEstate = true
        vc.loanApplicationId = self.loanApplicationId
        vc.borrowerPropertyId = self.borrowerPropertyId
        vc.mortgageDetail = self.realEstateDetail.firstMortgage
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func secondMortgageYesStackViewTapped(){
        isSecondMortgage = true
        let vc = Utility.getSecondMortgageFollowupQuestionsVC()
        vc.isForRealEstate = true
        vc.mortgageDetail = self.realEstateDetail.secondMortgage
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func secondMortgageNoStackViewTapped(){
        isSecondMortgage = false
        changeMortgageStatus()
    }
    
    @objc func secondMortgageViewTapped(){
        let vc = Utility.getSecondMortgageFollowupQuestionsVC()
        vc.isForRealEstate = true
        vc.loanApplicationId = self.loanApplicationId
        vc.borrowerPropertyId = self.borrowerPropertyId
        vc.mortgageDetail = self.realEstateDetail.secondMortgage
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    func changeMortgageStatus(){
        if (!isFirstMortgage && !isSecondMortgage){
            
            btnFirstMortgageYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblFirstMortgageYes.font = Theme.getRubikRegularFont(size: 15)
            btnFirstMortgageNo.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblFirstMortgageNo.font = Theme.getRubikMediumFont(size: 15)
            firstMortgageMainViewHeightConstraint.constant = 145
            firstMortgageView.isHidden = true
            secondMortgageMainViewHeightConstraint.constant = 0
            secondMortgageMainView.isHidden = true
            secondMortgageView.isHidden = true
        }
        else if (isFirstMortgage && !isSecondMortgage){
            
            btnFirstMortgageYes.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblFirstMortgageYes.font = Theme.getRubikMediumFont(size: 15)
            btnFirstMortgageNo.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblFirstMortgageNo.font = Theme.getRubikRegularFont(size: 15)
            firstMortgageMainViewHeightConstraint.constant = 350
            firstMortgageView.isHidden = false
            secondMortgageMainView.isHidden = false
            secondMortgageView.isHidden = true
            btnSecondMortgageYes.setImage(UIImage(named: isSecondMortgage ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblSecondMortgageYes.font = isSecondMortgage ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
            btnSecondMortgageNo.setImage(UIImage(named: !isSecondMortgage ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblSecondMortgageNo.font = !isSecondMortgage ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
            secondMortgageMainViewHeightConstraint.constant = isSecondMortgage ? 350 : 145
            secondMortgageView.isHidden = !isSecondMortgage
        }
        else if (isSecondMortgage){
            btnFirstMortgageYes.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblFirstMortgageYes.font = Theme.getRubikMediumFont(size: 15)
            btnFirstMortgageNo.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblFirstMortgageNo.font = Theme.getRubikRegularFont(size: 15)
            firstMortgageMainViewHeightConstraint.constant = 350
            firstMortgageView.isHidden = false
            secondMortgageMainView.isHidden = false
            
            secondMortgageView.isHidden = false
            btnSecondMortgageYes.setImage(UIImage(named: isSecondMortgage ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblSecondMortgageYes.font = isSecondMortgage ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
            btnSecondMortgageNo.setImage(UIImage(named: !isSecondMortgage ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblSecondMortgageNo.font = !isSecondMortgage ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
            secondMortgageMainViewHeightConstraint.constant = isSecondMortgage ? 350 : 145
            secondMortgageView.isHidden = !isSecondMortgage
        }

        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        let vc = Utility.getDeleteAddressPopupVC()
        vc.popupTitle = "Are you sure you want to delete this property?"
        vc.screenType = 4
        vc.delegate = self
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate(){
            addUpdateRealEstate()
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldPropertyType.validate()
        isValidate = txtfieldOccupancyType.validate() && isValidate
        isValidate = txtfieldPropertyStatus.validate() && isValidate
        if !txtfieldCurrentRentalIncome.isHidden {
            isValidate = txtfieldCurrentRentalIncome.validate() && isValidate
        }
        isValidate = txtfieldHomeOwnerAssociationDues.validate() && isValidate
        isValidate = txtfieldPropertyValue.validate() && isValidate
        isValidate = txtfieldAnnualPropertyTax.validate() && isValidate
        isValidate = txtfieldAnnualHomeOwnerInsurance.validate() && isValidate
        isValidate = txtfieldAnnualFloodInsurance.validate() && isValidate
        return isValidate
    }
    
    //MARK:- API's
    
    func getPropertyTypeDropDown(){
        
        self.propertyTypeArray.removeAll()
        self.occupancyTypeArray.removeAll()
        self.propertyStatusArray.removeAll()
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllPropertyTypeDrowpDown, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option)
                        self.propertyTypeArray.append(model)
                    }
                    self.txtfieldPropertyType.setDropDownDataSource(self.propertyTypeArray.map{$0.optionName})
                    self.getOccupancyTypeDropDown()
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
        
    }
    
    func getOccupancyTypeDropDown(){
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllOccupancyTypeDropDown, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option)
                        self.occupancyTypeArray.append(model)
                    }
                    self.txtfieldOccupancyType.setDropDownDataSource(self.occupancyTypeArray.map{$0.optionName})
                    Utility.showOrHideLoader(shouldShow: false)
                    self.getPropertyStatusDropDown()
                    
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
    }
    
    func getPropertyStatusDropDown(){
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllPropertyStatus, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option)
                        self.propertyStatusArray.append(model)
                    }
                    self.txtfieldPropertyStatus.setDropDownDataSource(self.propertyStatusArray.map{$0.optionName})
                    if (!self.isForAdd){
                        self.getRealEstateDetail()
                    }
                    else{
                        Utility.showOrHideLoader(shouldShow: false)
                    }
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
        
    }
    
    func getRealEstateDetail(){
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerPropertyId=\(borrowerPropertyId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getRealEstateDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    
                    let realEstateModel = RealEstateDetailModel()
                    realEstateModel.updateModelWithJSON(json: result["data"])
                    self.realEstateDetail = realEstateModel
                    self.setRealEstateDetail()
                    if (result["data"]["address"] == JSON.null){
                        self.savedAddress = NSNull()
                    }
                    else{
                        self.savedAddress = ["street": result["data"]["address"]["street"] == JSON.null ? NSNull() : result["data"]["address"]["street"].stringValue,
                                             "unit": result["data"]["address"]["unit"] == JSON.null ? NSNull() : result["data"]["address"]["unit"].stringValue,
                                             "city": result["data"]["address"]["city"] == JSON.null ? NSNull() : result["data"]["address"]["city"].stringValue,
                                             "stateId": result["data"]["address"]["stateId"] == JSON.null ? NSNull() : result["data"]["address"]["stateId"].intValue,
                                             "zipCode": result["data"]["address"]["zipCode"] == JSON.null ? NSNull() : result["data"]["address"]["zipCode"].stringValue,
                                             "countryId": result["data"]["address"]["countryId"] == JSON.null ? NSNull() : result["data"]["address"]["countryId"].intValue,
                                             "countryName": result["data"]["address"]["countryName"] == JSON.null ? NSNull() : result["data"]["address"]["countryName"].stringValue,
                                             "stateName": result["data"]["address"]["stateName"] == JSON.null ? NSNull() : result["data"]["address"]["stateName"].stringValue,
                                             "countyId": result["data"]["address"]["countyId"] == JSON.null ? NSNull() : result["data"]["address"]["countyId"].stringValue,
                                             "countyName": result["data"]["address"]["countyName"] == JSON.null ? NSNull() : result["data"]["address"]["countyName"].stringValue] as [String: Any]
                    }
                    
                    if (result["data"]["firstMortgageModel"] == JSON.null){
                        self.savedFirstMortgage = NSNull()
                    }
                    else{
                        self.savedFirstMortgage = ["propertyTaxesIncludeinPayment": result["data"]["firstMortgageModel"]["propertyTaxesIncludeinPayment"].boolValue,
                                                   "homeOwnerInsuranceIncludeinPayment": result["data"]["firstMortgageModel"]["homeOwnerInsuranceIncludeinPayment"].boolValue,
                                                   "floodInsuranceIncludeinPayment": result["data"]["firstMortgageModel"]["floodInsuranceIncludeinPayment"].boolValue,
                                                   "paidAtClosing": result["data"]["firstMortgageModel"]["paidAtClosing"].boolValue,
                                                   "firstMortgagePayment": result["data"]["firstMortgageModel"]["firstMortgagePayment"].doubleValue,
                                                   "unpaidFirstMortgagePayment": result["data"]["firstMortgageModel"]["unpaidFirstMortgagePayment"].doubleValue,
                                                   "helocCreditLimit": result["data"]["firstMortgageModel"]["helocCreditLimit"].doubleValue,
                                                   "isHeloc": result["data"]["firstMortgageModel"]["isHeloc"].boolValue] as [String: Any]
                    }
                    
                    if (result["data"]["secondMortgageModel"] == JSON.null){
                        self.savedSecondMortgage = NSNull()
                    }
                    else{
                        self.savedSecondMortgage = ["secondMortgagePayment": result["data"]["secondMortgageModel"]["secondMortgagePayment"].doubleValue,
                                                    "unpaidSecondMortgagePayment": result["data"]["secondMortgageModel"]["unpaidSecondMortgagePayment"].doubleValue,
                                                    "helocCreditLimit": result["data"]["secondMortgageModel"]["helocCreditLimit"].doubleValue,
                                                    "isHeloc": result["data"]["secondMortgageModel"]["isHeloc"].boolValue,
                                                    "paidAtClosing": result["data"]["secondMortgageModel"]["paidAtClosing"].boolValue] as [String: Any]
                    }
                    
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
    }
    
    func addUpdateRealEstate(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        var propertyTypeId: Any = NSNull()
        var occupancyTypeId: Any = NSNull()
        var rentalIncome: Any = NSNull()
        var propertyStatus: Any = NSNull()
        var hoaDues: Any = NSNull()
        var propertyValue: Any = NSNull()
        var propertyTax: Any = NSNull()
        var homeOwnerInsurance: Any = NSNull()
        var floodInsurance: Any = NSNull()
        
        if let selectedProperty = propertyTypeArray.filter({$0.optionName.localizedCaseInsensitiveContains(txtfieldPropertyType.text!)}).first{
            propertyTypeId = selectedProperty.optionId
        }
        if let selectedOccupancy = occupancyTypeArray.filter({$0.optionName.localizedCaseInsensitiveContains(txtfieldOccupancyType.text!)}).first{
            occupancyTypeId = selectedOccupancy.optionId
        }
        if let selectedStatus = propertyStatusArray.filter({$0.optionName.localizedCaseInsensitiveContains(txtfieldPropertyStatus.text!)}).first{
            propertyStatus = selectedStatus.optionId
        }
        if (txtfieldCurrentRentalIncome.text != ""){
            if let value = Double(cleanString(string: txtfieldCurrentRentalIncome.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                rentalIncome = value
            }
        }
        if (txtfieldHomeOwnerAssociationDues.text != ""){
            if let value = Double(cleanString(string: txtfieldHomeOwnerAssociationDues.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                hoaDues = value
            }
        }
        if (txtfieldPropertyValue.text != ""){
            if let value = Double(cleanString(string: txtfieldPropertyValue.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                propertyValue = value
            }
        }
        if (txtfieldAnnualPropertyTax.text != ""){
            if let value = Double(cleanString(string: txtfieldAnnualPropertyTax.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                propertyTax = value
            }
        }
        if (txtfieldAnnualHomeOwnerInsurance.text != ""){
            if let value = Double(cleanString(string: txtfieldAnnualHomeOwnerInsurance.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                homeOwnerInsurance = value
            }
        }
        
        if (txtfieldAnnualFloodInsurance.text != ""){
            if let value = Double(cleanString(string: txtfieldAnnualFloodInsurance.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                floodInsurance = value
            }
        }
        
        let params = ["propertyInfoId": isForAdd ? NSNull() : realEstateDetail.propertyInfoId,
                      "borrowerPropertyId": isForAdd ? NSNull() : borrowerPropertyId,
                      "borrowerId": borrowerId,
                      "loanApplicationId": loanApplicationId,
                      "propertyTypeId": propertyTypeId,
                      "occupancyTypeId": occupancyTypeId,
                      "propertyStatus": propertyStatus,
                      "hoaDues": hoaDues,
                      "appraisedPropertyValue": propertyValue,
                      "rentalIncome": rentalIncome,
                      "hasFirstMortgage": isFirstMortgage,
                      "hasSecondMortgage": isSecondMortgage,
                      "propertyTax": propertyTax,
                      "floodInsurance": floodInsurance,
                      "homeOwnerInsurance": homeOwnerInsurance,
                      "address": savedAddress,
                      "firstMortgageModel": savedFirstMortgage,
                      "secondMortgageModel": savedSecondMortgage] as [String: Any]
        
        APIRouter.sharedInstance.executeAPI(type: .addUpdateRealEstate, method: .post, params: params) { status, result, message in
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
    
    func deleteRealEstate(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "BorrowerPropertyId=\(realEstateDetail.borrowerPropertyId)"
        
        APIRouter.sharedInstance.executeAPI(type: .deleteRealEstate, method: .delete, params: nil, extraData: extraData) { status, result, message in
            
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

extension RealEstateViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldPropertyType || textField == txtfieldOccupancyType {
            showHideRentalIncome()
        }
    }
}

extension RealEstateViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        deleteRealEstate()
    }
}

extension RealEstateViewController: CurrentEmployerAddressViewControllerDelegate{
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
            realEstateDetail.address?.stateId = stateId
        }
        if let countryId = address["countryId"] as? Int{
            realEstateDetail.address?.countryId = countryId
        }
        if let countryName = address["countryName"] as? String{
            realEstateDetail.address?.countryName = countryName
        }
        if let countyId = address["countyId"] as? Int{
            realEstateDetail.address?.countyId = countyId
        }
        if let countyName = address["countyName"] as? String{
            realEstateDetail.address?.countyName = countyName
        }
        realEstateDetail.address?.street = street
        realEstateDetail.address?.unit = unit
        realEstateDetail.address?.city = city
        realEstateDetail.address?.stateName = stateName
        realEstateDetail.address?.zipCode = zipCode
    }
}

extension RealEstateViewController: FirstMortgageFollowupQuestionsViewControllerDelegate{
    func saveFirstMortageObject(firstMortgage: [String : Any]) {
        self.savedFirstMortgage = firstMortgage
        isFirstMortgage = true
        changeMortgageStatus()
        
        var mortgagePayment: Double = 0.0, unpaidMortgagePayment: Double = 0.0, helocCreditLimit: Double = 0.0, propertyTaxesIncludeinPayment: Bool = false, homeOwnerInsuranceIncludeinPayment: Bool = false, floodInsuranceIncludeinPayment: Bool = false, paidAtClosing: Bool = false, isHeloc: Bool = false
        if let firstMortgagePayment = firstMortgage["firstMortgagePayment"] as? Double{
            mortgagePayment = firstMortgagePayment
            self.realEstateDetail.firstMortgage?.firstMortgagePayment = mortgagePayment
        }
        if let unpaidFirstMortgagePayment = firstMortgage["unpaidFirstMortgagePayment"] as? Double{
            unpaidMortgagePayment = unpaidFirstMortgagePayment
            self.realEstateDetail.firstMortgage?.unpaidFirstMortgagePayment = unpaidMortgagePayment
        }
        if let creditLimit = firstMortgage["helocCreditLimit"] as? Double{
            helocCreditLimit = creditLimit
            self.realEstateDetail.firstMortgage?.helocCreditLimit = helocCreditLimit
        }
        if let isPropertyTax = firstMortgage["propertyTaxesIncludeinPayment"] as? Bool{
            propertyTaxesIncludeinPayment = isPropertyTax
            self.realEstateDetail.firstMortgage?.propertyTaxesIncludeinPayment = propertyTaxesIncludeinPayment
        }
        if let isHomeOwner = firstMortgage["homeOwnerInsuranceIncludeinPayment"] as? Bool{
            homeOwnerInsuranceIncludeinPayment = isHomeOwner
            self.realEstateDetail.firstMortgage?.homeOwnerInsuranceIncludeinPayment = homeOwnerInsuranceIncludeinPayment
        }
        if let isFlood = firstMortgage["floodInsuranceIncludeinPayment"] as? Bool{
            floodInsuranceIncludeinPayment = isFlood
            self.realEstateDetail.firstMortgage?.floodInsuranceIncludeinPayment = floodInsuranceIncludeinPayment
        }
        if let isPaid = firstMortgage["paidAtClosing"] as? Bool{
            paidAtClosing = isPaid
            self.realEstateDetail.firstMortgage?.paidAtClosing = paidAtClosing
        }
        if let isHelocInclude = firstMortgage["isHeloc"] as? Bool{
            isHeloc = isHelocInclude
            self.realEstateDetail.firstMortgage?.isHeloc = isHeloc
        }
        
        lblFirstMortgagePayment.text = Int(mortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
        lblFirstMortgageBalance.text = Int(unpaidMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
    }
}

extension RealEstateViewController: SecondMortgageFollowupQuestionsViewControllerDelegate{
    func saveSecondMortageObject(secondMortgage: [String : Any]) {
        self.savedSecondMortgage = secondMortgage
        isSecondMortgage = true
        changeMortgageStatus()
        
        var mortgagePayment: Double = 0.0, unpaidMortgagePayment: Double = 0.0, helocCreditLimit: Double = 0.0, isHeloc: Bool = false, paidAtClosing: Bool = false, wasSmTaken: Bool = false, combineWithNewFirstMortgage: Bool = false
        if let secondMortgagePayment = secondMortgage["secondMortgagePayment"] as? Double{
            mortgagePayment = secondMortgagePayment
            self.realEstateDetail.secondMortgage?.secondMortgagePayment = mortgagePayment
        }
        if let unpaidSecondMortgagePayment = secondMortgage["unpaidSecondMortgagePayment"] as? Double{
            unpaidMortgagePayment = unpaidSecondMortgagePayment
            self.realEstateDetail.secondMortgage?.unpaidSecondMortgagePayment = unpaidMortgagePayment
        }
        if let creditLimit = secondMortgage["helocCreditLimit"] as? Double{
            helocCreditLimit = creditLimit
            self.realEstateDetail.secondMortgage?.helocCreditLimit = helocCreditLimit
        }
        if let isHelocInclude = secondMortgage["isHeloc"] as? Bool{
            isHeloc = isHelocInclude
            self.realEstateDetail.secondMortgage?.isHeloc = isHeloc
        }
        if let isPaid = secondMortgage["paidAtClosing"] as? Bool{
            paidAtClosing = isPaid
            self.realEstateDetail.secondMortgage?.paidAtClosing = paidAtClosing
        }
        if let wasSm = secondMortgage["wasSmTaken"] as? Bool{
            wasSmTaken = wasSm
            self.realEstateDetail.secondMortgage?.wasSmTaken = wasSmTaken
        }
        if let isCombine = secondMortgage["combineWithNewFirstMortgage"] as? Bool{
            combineWithNewFirstMortgage = isCombine
            self.realEstateDetail.secondMortgage?.combineWithNewFirstMortgage = combineWithNewFirstMortgage
        }
        
        lblSecondMortgagePayment.text = Int(mortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
        lblSecondMortgageBalance.text = Int(unpaidMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
    }
}

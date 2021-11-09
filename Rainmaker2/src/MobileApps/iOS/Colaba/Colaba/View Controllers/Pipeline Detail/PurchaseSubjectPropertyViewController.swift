//
//  PurchaseSubjectPropertyViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/09/2021.
//

import UIKit
import SwiftyJSON

class PurchaseSubjectPropertyViewController: BaseViewController {
    
    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    @IBOutlet weak var subjectPropertyTBDView: UIView!
    @IBOutlet weak var lblSubjectPropertyTBD: UILabel!
    @IBOutlet weak var btnSubjectPropertyTBD: UIButton!
    @IBOutlet weak var subjectPropertyAddressView: UIView!
    @IBOutlet weak var subjectPropertyAddressViewHeightConstraint: NSLayoutConstraint! //50 or 110
    @IBOutlet weak var lblSubjectPropertyAddress: UILabel!
    @IBOutlet weak var btnSubjectPropertyAddress: UIButton!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var txtfieldPropertyType: ColabaTextField!
    @IBOutlet weak var txtfieldOccupancyType: ColabaTextField!
    @IBOutlet weak var propertyView: UIView!
    @IBOutlet weak var propertyViewHeightConstraint: NSLayoutConstraint! //203 or 347
    @IBOutlet weak var lblUsePropertyQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var propertyDetailView: UIView!
    @IBOutlet weak var lblPropertyUseDetail: UILabel!
    @IBOutlet weak var txtfieldAppraisedPropertyValue: ColabaTextField!
    @IBOutlet weak var txtfieldTax: ColabaTextField!
    @IBOutlet weak var txtfieldHomeOwnerInsurance: ColabaTextField!
    @IBOutlet weak var txtfieldFloodInsurance: ColabaTextField!
    @IBOutlet weak var occupancyStatusView: UIView!
    @IBOutlet weak var lblCoBorrowerName: UILabel!
    @IBOutlet weak var occupyingStackView: UIStackView!
    @IBOutlet weak var btnOccupying: UIButton!
    @IBOutlet weak var lblOccupying: UILabel!
    @IBOutlet weak var nonOccupyingStackView: UIStackView!
    @IBOutlet weak var btnNonOccupying: UIButton!
    @IBOutlet weak var lblNonOccupying: UILabel!
    
    var loanApplicationId = 0
    
    var propertyTypeArray = [DropDownModel]()
    var occupancyTypeArray = [DropDownModel]()
    var subjectPropertyDetail = SubjectPropertyModel()
    var coBorrowerOccupancyArray = [CoBorrowerOccupancyModel]()
    
    var isTBDProperty = true
    var isMixedUseProperty: Bool?
    var isOccupying: Bool?
    var savedAddress: Any = NSNull()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
        setViews()
        getPropertyTypeDropDown()
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Property Type Text Field
        txtfieldPropertyType.setTextField(placeholder: "Property Type" , controller: self, validationType: .noValidation)
        txtfieldPropertyType.type = .dropdown
        txtfieldPropertyType.setDropDownDataSource(kPropertyTypeArray)
        
        ///Occupancy Type Text Field
        txtfieldOccupancyType.setTextField(placeholder: "Occupancy Type" , controller: self, validationType: .noValidation)
        txtfieldOccupancyType.type = .dropdown
        txtfieldOccupancyType.setDropDownDataSource(kOccupancyTypeArray)
        
        ///Appraised Property Value Text Field
        txtfieldAppraisedPropertyValue.setTextField(placeholder: "Appraised Property Value" , controller: self, validationType: .noValidation)
        txtfieldAppraisedPropertyValue.type = .amount
        
        ///Annual Property Taxes Text Field
        txtfieldTax.setTextField(placeholder: "Annual Property Taxes", controller: self, validationType: .noValidation)
        txtfieldTax.type = .amount
        
        ///Annual Homeowner's Insurance Text Field
        txtfieldHomeOwnerInsurance.setTextField(placeholder: "Annual Homeowner's Insurance", controller: self, validationType: .noValidation)
        txtfieldHomeOwnerInsurance.type = .amount
        
        ///Annual Flood Insurance Text Field
        txtfieldFloodInsurance.setTextField(placeholder: "Annual Flood Insurance", controller: self, validationType: .noValidation)
        txtfieldFloodInsurance.type = .amount
    }
    
    func setViews(){
        
        subjectPropertyTBDView.layer.cornerRadius = 8
        subjectPropertyTBDView.layer.borderWidth = 1
        subjectPropertyTBDView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        subjectPropertyTBDView.dropShadowToCollectionViewCell()
        subjectPropertyTBDView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(tbdViewTapped)))
        
        subjectPropertyAddressView.layer.cornerRadius = 8
        subjectPropertyAddressView.layer.borderWidth = 1
        subjectPropertyAddressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        subjectPropertyAddressView.dropShadowToCollectionViewCell()
        subjectPropertyAddressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        propertyDetailView.layer.cornerRadius = 6
        propertyDetailView.layer.borderWidth = 1
        propertyDetailView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        propertyDetailView.dropShadowToCollectionViewCell()
        propertyDetailView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(propertyDetailViewTapped)))
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        
        occupyingStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(occupyingStackViewTapped)))
        nonOccupyingStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(nonOccupyingStackViewTapped)))
        
        btnYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblYes.font = Theme.getRubikRegularFont(size: 14)
        propertyDetailView.isHidden = true
        propertyViewHeightConstraint.constant = 203
        setScreenHeight()
        
        btnOccupying.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblOccupying.font = Theme.getRubikRegularFont(size: 14)
    }
    
    func setSubjectPropertyData(){
        isTBDProperty = self.subjectPropertyDetail.address == nil
        if let subjectPropertyAddress = self.subjectPropertyDetail.address{
            lblAddress.text = "\(subjectPropertyAddress.street) \(subjectPropertyAddress.unit),\n\(subjectPropertyAddress.city), \(subjectPropertyAddress.stateName) \(subjectPropertyAddress.zipCode)"
        }
        
        if let propertyType = self.propertyTypeArray.filter({$0.optionId == self.subjectPropertyDetail.propertyTypeId}).first{
            self.txtfieldPropertyType.setTextField(text: propertyType.optionName)
        }
        if let occupancyType = self.occupancyTypeArray.filter({$0.optionId == self.subjectPropertyDetail.occupancyTypeId}).first{
            self.txtfieldOccupancyType.setTextField(text: occupancyType.optionName)
        }
        isMixedUseProperty = self.subjectPropertyDetail.isMixedUseProperty
        lblPropertyUseDetail.text = self.subjectPropertyDetail.mixedUsePropertyExplanation
        txtfieldAppraisedPropertyValue.setTextField(text: String(format: "%.0f", self.subjectPropertyDetail.appraisedPropertyValue.rounded()))
        txtfieldTax.setTextField(text: String(format: "%.0f", self.subjectPropertyDetail.propertyTax.rounded()))
        txtfieldHomeOwnerInsurance.setTextField(text: String(format: "%.0f", self.subjectPropertyDetail.homeOwnerInsurance.rounded()))
        txtfieldFloodInsurance.setTextField(text: String(format: "%.0f", self.subjectPropertyDetail.floodInsurance.rounded()))
        isOccupying = self.coBorrowerOccupancyArray.count > 0
        lblCoBorrowerName.text = self.coBorrowerOccupancyArray.map{$0.borrowerFullName}.joined(separator: ", ")
        changeOccupyingStatus()
        changeMixedUseProperty()
        changedSubjectPropertyType()
    }
    
    func setScreenHeight(){
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func tbdViewTapped(){
        isTBDProperty = true
        changedSubjectPropertyType()
    }
    
    @objc func addressViewTapped(){
        let vc = Utility.getSubjectPropertyAddressVC()
        vc.selectedAddress = self.subjectPropertyDetail.address
        vc.delegate = self
        self.presentVC(vc: vc)
        isTBDProperty = false
        changedSubjectPropertyType()
    }
    
    @objc func changedSubjectPropertyType(){
        btnSubjectPropertyTBD.setImage(UIImage(named: isTBDProperty ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblSubjectPropertyTBD.font = isTBDProperty ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnSubjectPropertyAddress.setImage(UIImage(named: isTBDProperty ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
        lblSubjectPropertyAddress.font = isTBDProperty ?  Theme.getRubikRegularFont(size: 15) : Theme.getRubikMediumFont(size: 15)
        subjectPropertyAddressViewHeightConstraint.constant = isTBDProperty ? 50 : 110
        lblAddress.isHidden = isTBDProperty
        setScreenHeight()
    }
    
    @objc func yesStackViewTapped(){
        let vc = Utility.getMixPropertyDetailFollowUpVC()
        vc.detail = lblPropertyUseDetail.text!
        vc.delegate = self
        self.presentVC(vc: vc)
        isMixedUseProperty = true
        changeMixedUseProperty()
    }
    
    @objc func noStackViewTapped(){
        isMixedUseProperty = false
        changeMixedUseProperty()
    }
    
    @objc func changeMixedUseProperty(){
        if let mixUseProperty = isMixedUseProperty{
            btnYes.setImage(UIImage(named: mixUseProperty ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblYes.font = mixUseProperty ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            btnNo.setImage(UIImage(named: mixUseProperty ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
            lblNo.font = mixUseProperty ?  Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
            propertyDetailView.isHidden = !mixUseProperty
            propertyViewHeightConstraint.constant = mixUseProperty ? 347 : 203
            setScreenHeight()
        }
        
    }
    
    @objc func propertyDetailViewTapped(){
        let vc = Utility.getMixPropertyDetailFollowUpVC()
        vc.detail = lblPropertyUseDetail.text!
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func occupyingStackViewTapped(){
        isOccupying = true
        changeOccupyingStatus()
    }
    
    @objc func nonOccupyingStackViewTapped(){
        isOccupying = false
        changeOccupyingStatus()
    }
    
    @objc func changeOccupyingStatus(){
        if let occupying = isOccupying{
            btnOccupying.setImage(UIImage(named: occupying ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblOccupying.font = occupying ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            btnNonOccupying.setImage(UIImage(named: occupying ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
            lblNonOccupying.font = occupying ?  Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
        }
        
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        updateSubjectProperty()
    }
    
    //MARK:- API's
    
    func getPropertyTypeDropDown(){
        
        propertyTypeArray.removeAll()
        occupancyTypeArray.removeAll()
        coBorrowerOccupancyArray.removeAll()
        
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
                    self.getPurchaseSubjectProperty()
                    
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
    
    func getPurchaseSubjectProperty(){
        
        let extraData = "loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getPurchaseSubjectPropertyDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                if (status == .success){
                    
                    let subjectPropertyModel = SubjectPropertyModel()
                    subjectPropertyModel.updateModelWithJSON(json: result["data"])
                    self.subjectPropertyDetail = subjectPropertyModel
                    self.getCoBorrowersOccupancyStatus()
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
    
    func getCoBorrowersOccupancyStatus(){
        let extraData = "loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getCoBorrowersOccupancyStatus, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    
                    let coBorrowers = result["data"].arrayValue
                    for borrower in coBorrowers{
                        let model = CoBorrowerOccupancyModel()
                        model.updateModelWithJSON(json: borrower)
                        self.coBorrowerOccupancyArray.append(model)
                    }
                    self.setSubjectPropertyData()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
    }
    
    func updateSubjectProperty(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        var propertyTypeId: Any = NSNull()
        var occupancyTypeId: Any = NSNull()
        var appraisedPropertyValue: Any = NSNull()
        var propertyTax: Any = NSNull()
        var homeOwnerInsurance: Any = NSNull()
        var floodInsurance: Any = NSNull()
        var mixUsedProperty: Any = NSNull()
        var mixUsedPropertyDetail: Any = NSNull()
        
        if txtfieldAppraisedPropertyValue.text != ""{
            if let value = Double(cleanString(string: txtfieldAppraisedPropertyValue.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                appraisedPropertyValue = value
            }
        }
        if txtfieldTax.text != ""{
            if let value = Double(cleanString(string: txtfieldTax.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                propertyTax = value
            }
        }
        if txtfieldHomeOwnerInsurance.text != ""{
            if let value = Double(cleanString(string: txtfieldHomeOwnerInsurance.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                homeOwnerInsurance = value
            }
        }
        if txtfieldFloodInsurance.text != ""{
            if let value = Double(cleanString(string: txtfieldFloodInsurance.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                floodInsurance = value
            }
        }
        
        if let selectedProperty = propertyTypeArray.filter({$0.optionName == txtfieldPropertyType.text}).first{
            propertyTypeId = selectedProperty.optionId
        }
        if let selectedOccupancy = occupancyTypeArray.filter({$0.optionName == txtfieldOccupancyType.text}).first{
            occupancyTypeId = selectedOccupancy.optionId
        }
        
        if let mixUse = isMixedUseProperty{
            mixUsedProperty = mixUse
        }
        
        if lblPropertyUseDetail.text != ""{
            mixUsedPropertyDetail = lblPropertyUseDetail.text!
        }
        
        let params = ["loanApplicationId": loanApplicationId,
                      "propertyTypeId": propertyTypeId,
                      "occupancyTypeId": occupancyTypeId,
                      "appraisedPropertyValue": appraisedPropertyValue,
                      "propertyTax": propertyTax,
                      "homeOwnerInsurance": homeOwnerInsurance,
                      "floodInsurance": floodInsurance,
                      "address": isTBDProperty ? NSNull() : savedAddress,
                      "isMixedUseProperty": mixUsedProperty,
                      "mixedUsePropertyExplanation": mixUsedPropertyDetail,
                      "subjectPropertyTbd": isTBDProperty] as [String: Any]
        
        print(params)
        
        APIRouter.sharedInstance.executeAPI(type: .updatePurchaseSubjectProperty, method: .post, params: params) { status, result, message in
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    self.showPopup(message: "Subject property updated sucessfully", popupState: .success, popupDuration: .custom(5)) { dismiss in
                        
                    }
                    self.getPurchaseSubjectProperty()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
        }
    }
    
}

extension PurchaseSubjectPropertyViewController: SubjectPropertyAddressViewControllerDelegate{
    func saveAddressObject(address: [String : Any]) {
        savedAddress = address
        
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

extension PurchaseSubjectPropertyViewController: MixPropertyDetailFollowUpViewControllerDelegate{
    func updateDetails(detail: String) {
        lblPropertyUseDetail.text = detail
    }
}

//
//  RealEstateViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit

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
    
    var loanApplicationId = 0
    var borrowerPropertyId = 0
    var borrowerFullName = ""
    
    var propertyTypeArray = [DropDownModel]()
    var occupancyTypeArray = [DropDownModel]()
    var propertyStatusArray = [DropDownModel]()
    var realEstateDetail = RealEstateDetailModel()
    
    var isFirstMortgage = false
    var isSecondMortgage = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        getPropertyTypeDropDown()
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
        txtfieldPropertyType.setDropDownDataSource(kPropertyTypeArray)
        
        txtfieldOccupancyType.setTextField(placeholder: "Occupancy Type", controller: self, validationType: .required)
        txtfieldOccupancyType.type = .dropdown
        txtfieldOccupancyType.setDropDownDataSource(kOccupancyTypeArray)
        
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
            lblAddress.text = "\(subjectPropertyAddress.street) \(subjectPropertyAddress.unit),\n\(subjectPropertyAddress.city), \(subjectPropertyAddress.stateName) \(subjectPropertyAddress.zipCode)"
        }
        
        if let propertyType = self.propertyTypeArray.filter({$0.optionId == self.realEstateDetail.propertyTypeId}).first{
            self.txtfieldPropertyType.setTextField(text: propertyType.optionName)
        }
        if let occupancyType = self.occupancyTypeArray.filter({$0.optionId == self.realEstateDetail.occupancyTypeId}).first{
            self.txtfieldOccupancyType.setTextField(text: occupancyType.optionName)
        }
        
        txtfieldPropertyStatus.setTextField(text: self.realEstateDetail.propertyStatus)
        txtfieldCurrentRentalIncome.setTextField(text: String(format: "%.0f", self.realEstateDetail.rentalIncome.rounded()))
        txtfieldHomeOwnerAssociationDues.setTextField(text: String(format: "%.0f", self.realEstateDetail.homeOwnerDues.rounded()))
        txtfieldPropertyValue.setTextField(text: String(format: "%.0f", self.realEstateDetail.propertyValue.rounded()))
        txtfieldAnnualPropertyTax.setTextField(text: String(format: "%.0f", self.realEstateDetail.annualPropertyTax.rounded()))
        txtfieldAnnualHomeOwnerInsurance.setTextField(text: String(format: "%.0f", self.realEstateDetail.annualHomeInsurance.rounded()))
        txtfieldAnnualFloodInsurance.setTextField(text: String(format: "%.0f", self.realEstateDetail.annualFloodInsurance.rounded()))
        isFirstMortgage = self.realEstateDetail.hasFirstMortgage
        isSecondMortgage = self.realEstateDetail.hasSecondMortgage
        lblFirstMortgagePayment.text = Int(self.realEstateDetail.firstMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
        lblFirstMortgageBalance.text = Int(self.realEstateDetail.firstMortgageBalance).withCommas().replacingOccurrences(of: ".00", with: "")
        lblSecondMortgagePayment.text = Int(self.realEstateDetail.secondMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
        lblSecondMortgageBalance.text = Int(self.realEstateDetail.secondMortgageBalance).withCommas().replacingOccurrences(of: ".00", with: "")
        
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
        vc.selectedAddress = self.realEstateDetail.address
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
        let vc = Utility.getFirstMortgageFollowupQuestionsVC()
        vc.isForRealEstate = true
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
        self.presentVC(vc: vc)
    }
    
    @objc func secondMortgageYesStackViewTapped(){
        let vc = Utility.getSecondMortgageFollowupQuestionsVC()
        vc.isForRealEstate = true
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
        else if (isFirstMortgage){
            
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
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate(){
            self.dismissVC()
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
                    if (self.loanApplicationId > 0){
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
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
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

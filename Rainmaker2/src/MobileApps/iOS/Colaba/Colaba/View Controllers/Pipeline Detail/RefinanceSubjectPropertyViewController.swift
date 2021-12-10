//
//  RefinanceSubjectPropertyViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/09/2021.
//

import UIKit
import SwiftyJSON

class RefinanceSubjectPropertyViewController: BaseViewController {
    
    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
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
    @IBOutlet weak var txtfieldRentalIncome: ColabaTextField!
    @IBOutlet weak var txtfieldRentalIncomeTopConstraint: NSLayoutConstraint! //30 or 0
    @IBOutlet weak var txtfieldRentalIncomeHeightConstraint: NSLayoutConstraint! // 39 or 0
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
    @IBOutlet weak var txtfieldHomePurchaseDate: ColabaTextField!
    @IBOutlet weak var txtfieldHomeOwnerAssociationDues: ColabaTextField!
    @IBOutlet weak var txtfieldTax: ColabaTextField!
    @IBOutlet weak var txtfieldHomeOwnerInsurance: ColabaTextField!
    @IBOutlet weak var txtfieldFloodInsurance: ColabaTextField!
    @IBOutlet weak var tableViewOccupancyStatus: UITableView!
    @IBOutlet weak var tableViewOccupancyStatusHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var firstMortgageMainView: UIView!
    @IBOutlet weak var firstMortgageMainViewTopConstraint: NSLayoutConstraint!
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
    
    var loanApplicationId = 0
    
    var isTBDProperty = true
    var isMixedUseProperty: Bool?
    var isFirstMortgage = false
    var isSecondMortgage = false
    
    var propertyTypeArray = [DropDownModel]()
    var occupancyTypeArray = [DropDownModel]()
    var subjectPropertyDetail = RefinanceSubjectPropertyModel()
    var coBorrowerOccupancyArray = [CoBorrowerOccupancyModel]()
    var savedAddress: Any = NSNull()
    var savedFirstMortgage: Any = NSNull()
    var savedSecondMortgage: Any = NSNull()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setViews()
        setTextFields()
        getPropertyTypeDropDown()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
    }
    
    //MARK:- Methods and Actions
    func setViews() {
        
        btnYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblYes.font = Theme.getRubikRegularFont(size: 14)
        propertyDetailView.isHidden = true
        propertyViewHeightConstraint.constant = 203
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
        
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
        
        tableViewOccupancyStatus.register(UINib(nibName: "OccupancyStatusTableViewCell", bundle: nil), forCellReuseIdentifier: "OccupancyStatusTableViewCell")
        
    }
    
    func setTextFields(){

        txtfieldPropertyType.setTextField(placeholder: "Property Type" , controller: self, validationType: .required)
        txtfieldPropertyType.type = .dropdown
        
        ///Occupancy Type Text Field
        txtfieldOccupancyType.setTextField(placeholder: "Occupancy Type" , controller: self, validationType: .required)
        txtfieldOccupancyType.type = .dropdown
        
        ///Rental Income Text Field
        txtfieldRentalIncome.setTextField(placeholder: "Rental Income" , controller: self, validationType: .noValidation)
        txtfieldRentalIncome.type = .amount
        
        ///Property Value Text Field
        txtfieldAppraisedPropertyValue.setTextField(placeholder: "Property Value" , controller: self, validationType: .noValidation)
        txtfieldAppraisedPropertyValue.type = .amount
        
        ///Property Value Text Field
        txtfieldHomePurchaseDate.setTextField(placeholder: "Date of Home Purchase (MM/YYYY)", controller: self, validationType: .noValidation)
        txtfieldHomePurchaseDate.type = .monthlyDatePicker
        
        ///Annual Homeowner's Associations Due Text Field
        txtfieldHomeOwnerAssociationDues.setTextField(placeholder: "Annual Homeowner's Associations Due", controller: self, validationType: .noValidation)
        txtfieldHomeOwnerAssociationDues.type = .amount
        
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
    
    func setSubjectPropertyData(){
        
        isTBDProperty = self.subjectPropertyDetail.subjectPropertyTbd
        if let subjectPropertyAddress = self.subjectPropertyDetail.address{
            lblAddress.text = "\(subjectPropertyAddress.street) \(subjectPropertyAddress.unit),\n\(subjectPropertyAddress.city), \(subjectPropertyAddress.stateName) \(subjectPropertyAddress.zipCode)"
        }
        
        if let propertyType = self.propertyTypeArray.filter({$0.optionId == self.subjectPropertyDetail.propertyTypeId}).first{
            self.txtfieldPropertyType.setTextField(text: propertyType.optionName)
        }
        if let occupancyType = self.occupancyTypeArray.filter({$0.optionId == self.subjectPropertyDetail.propertyUsageId}).first{
            self.txtfieldOccupancyType.setTextField(text: occupancyType.optionName)
        }
        
        txtfieldRentalIncome.setTextField(text: String(format: "%.0f", self.subjectPropertyDetail.rentalIncome.rounded()))
        isMixedUseProperty = self.subjectPropertyDetail.isMixedUseProperty
        lblPropertyUseDetail.text = self.subjectPropertyDetail.mixedUsePropertyExplanation
        txtfieldAppraisedPropertyValue.setTextField(text: String(format: "%.0f", self.subjectPropertyDetail.propertyValue.rounded()))
        txtfieldHomePurchaseDate.setTextField(text: Utility.getMonthYear(self.subjectPropertyDetail.dateAcquired))
        txtfieldHomeOwnerAssociationDues.setTextField(text: String(format: "%.0f", self.subjectPropertyDetail.hoaDues.rounded()))
        txtfieldTax.setTextField(text: String(format: "%.0f", self.subjectPropertyDetail.propertyTax.rounded()))
        txtfieldHomeOwnerInsurance.setTextField(text: String(format: "%.0f", self.subjectPropertyDetail.homeOwnerInsurance.rounded()))
        txtfieldFloodInsurance.setTextField(text: String(format: "%.0f", self.subjectPropertyDetail.floodInsurance.rounded()))
        tableViewOccupancyStatus.reloadData()
        if (coBorrowerOccupancyArray.count == 0){
            firstMortgageMainViewTopConstraint.constant = 0
        }
        isFirstMortgage = self.subjectPropertyDetail.hasFirstMortgage
        isSecondMortgage = self.subjectPropertyDetail.hasSecondMortgage
        
        if let firstMortgage = self.subjectPropertyDetail.firstMortgage{
            lblFirstMortgagePayment.text = Int(firstMortgage.firstMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
            lblFirstMortgageBalance.text = Int(firstMortgage.unpaidFirstMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
        }
        
        if let secondMortgage = self.subjectPropertyDetail.secondMortgage{
            lblSecondMortgagePayment.text = Int(secondMortgage.secondMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
            lblSecondMortgageBalance.text = Int(secondMortgage.unpaidSecondMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
        }
        
        changeMortgageStatus()
        showHideRentalIncome()
        changeMixedUseProperty()
        changedSubjectPropertyType()
    }
    
    func setScreenHeight(){
        let firstMortgageViewHeight = self.firstMortgageMainView.frame.height
        let secondMortgageViewHeight = self.secondMortgageMainView.frame.height
        let occupancyStatusTableViewHeight = self.tableViewOccupancyStatus.contentSize.height
        self.tableViewOccupancyStatusHeightConstraint.constant = occupancyStatusTableViewHeight
        self.mainViewHeightConstraint.constant = firstMortgageViewHeight + secondMortgageViewHeight + occupancyStatusTableViewHeight + 1500
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
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @objc func showHideRentalIncome(){
        if (txtfieldOccupancyType.text == "Investment Property"){
            txtfieldRentalIncomeTopConstraint.constant = 30
            txtfieldRentalIncomeHeightConstraint.constant = 39
            txtfieldRentalIncome.isHidden = false
            txtfieldRentalIncome.resignFirstResponder()
        }
        else if ( (txtfieldOccupancyType.text == "Primary Residence") && (txtfieldPropertyType.text == "Duplex (2 Unit)" || txtfieldPropertyType.text == "Triplex (3 Unit)" || txtfieldPropertyType.text == "Quadplex (4 Unit)") ){
            txtfieldRentalIncomeTopConstraint.constant = 30
            txtfieldRentalIncomeHeightConstraint.constant = 39
            txtfieldRentalIncome.isHidden = false
            txtfieldRentalIncome.resignFirstResponder()
        }
        else{
            txtfieldRentalIncomeTopConstraint.constant = 0
            txtfieldRentalIncomeHeightConstraint.constant = 0
            txtfieldRentalIncome.isHidden = true
            txtfieldRentalIncome.resignFirstResponder()
            txtfieldRentalIncome.text = ""
            txtfieldRentalIncome.textInsetsPreset = .none
            txtfieldRentalIncome.placeholderHorizontalOffset = 0
        }
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
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
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                self.setScreenHeight()
            }
        }
        
    }
    
    @objc func propertyDetailViewTapped(){
        let vc = Utility.getMixPropertyDetailFollowUpVC()
        vc.detail = lblPropertyUseDetail.text!
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func firstMortgageYesStackViewTapped(){
        isFirstMortgage = true
        let vc = Utility.getFirstMortgageFollowupQuestionsVC()
        vc.mortgageDetail = self.subjectPropertyDetail.firstMortgage
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
        vc.mortgageDetail = self.subjectPropertyDetail.firstMortgage
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func secondMortgageYesStackViewTapped(){
        isSecondMortgage = true
        let vc = Utility.getSecondMortgageFollowupQuestionsVC()
        vc.mortgageDetail = self.subjectPropertyDetail.secondMortgage
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func secondMortgageNoStackViewTapped(){
        isSecondMortgage = false
        changeMortgageStatus()
    }
    
    @objc func secondMortgageViewTapped(){
        let vc = Utility.getSecondMortgageFollowupQuestionsVC()
        vc.mortgageDetail = self.subjectPropertyDetail.secondMortgage
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
    
    func validate() -> Bool{
        var isValidate = txtfieldPropertyType.validate()
        isValidate = txtfieldOccupancyType.validate() && isValidate
        return isValidate
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        if (validate()){
            updateSubjectProperty()
        }
    }
    
    //MARK:- API's
    
    func getPropertyTypeDropDown(){
        
        self.propertyTypeArray.removeAll()
        self.occupancyTypeArray.removeAll()
        self.coBorrowerOccupancyArray.removeAll()
        
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
                    self.getRefinanceSubjectProperty()
                    
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
    
    func getRefinanceSubjectProperty(){
        
        let extraData = "loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getRefinanceSubjectPropertyDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                if (status == .success){
                    let refinancePropertyModel = RefinanceSubjectPropertyModel()
                    refinancePropertyModel.updateModelWithJSON(json: result["data"])
                    self.subjectPropertyDetail = refinancePropertyModel
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
                                                    "combineWithNewFirstMortgage": result["data"]["secondMortgageModel"]["combineWithNewFirstMortgage"].boolValue,
                                                    "wasSmTaken": result["data"]["secondMortgageModel"]["wasSmTaken"].boolValue] as [String: Any]
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
        var homeOwnerAssosiationDues: Any = NSNull()
        var homeOwnerInsurance: Any = NSNull()
        var floodInsurance: Any = NSNull()
        var rentalIncome: Any = NSNull()
        var mixUsedProperty: Any = NSNull()
        var mixUsedPropertyDetail: Any = NSNull()
        var dateAcquired: Any = NSNull()
        
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
        if txtfieldHomeOwnerAssociationDues.text != ""{
            if let value = Double(cleanString(string: txtfieldHomeOwnerAssociationDues.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                homeOwnerAssosiationDues = value
            }
        }
        if txtfieldFloodInsurance.text != ""{
            if let value = Double(cleanString(string: txtfieldFloodInsurance.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                floodInsurance = value
            }
        }
        if txtfieldRentalIncome.text != ""{
            if let value = Double(cleanString(string: txtfieldRentalIncome.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                rentalIncome = value
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
        
        let dateComponent = txtfieldHomePurchaseDate.text!.components(separatedBy: "/")
        if (dateComponent.count == 2){
            dateAcquired = "\(dateComponent[1])-\(dateComponent[0])-01T00:00:00"
        }
        
        let params = ["loanApplicationId": loanApplicationId,
                      "subjectPropertyTbd": isTBDProperty,
                      "address": isTBDProperty ? NSNull() : savedAddress,
                      "propertyTypeId": propertyTypeId,
                      "propertyUsageId": occupancyTypeId,
                      "isMixedUseProperty": mixUsedProperty,
                      "mixedUsePropertyExplanation": mixUsedPropertyDetail,
                      "propertyValue": appraisedPropertyValue,
                      "dateAcquired": dateAcquired,
                      "hoaDues": homeOwnerAssosiationDues,
                      "propertyTax": propertyTax,
                      "homeOwnerInsurance": homeOwnerInsurance,
                      "floodInsurance": floodInsurance,
                      "rentalIncome": rentalIncome,
                      "hasFirstMortgage": isFirstMortgage,
                      "hasSecondMortgage": isSecondMortgage,
                      "isSameAsPropertyAddress": subjectPropertyDetail.isSameAsPropertyAddress,
                      "firstMortgageModel": savedFirstMortgage,
                      "secondMortgageModel": savedSecondMortgage] as [String: Any]
        
        print(params)
        
        APIRouter.sharedInstance.executeAPI(type: .updateRefinanceSubjectProperty, method: .post, params: params) { status, result, message in
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
//                    self.showPopup(message: "Subject property updated sucessfully", popupState: .success, popupDuration: .custom(5)) { dismiss in
//                        self.goBack()
//                    }
                    self.goBack()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
        }
    }
}

extension RefinanceSubjectPropertyViewController: ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldPropertyType || textField == txtfieldOccupancyType {
            showHideRentalIncome()
        }
    }
}
extension RefinanceSubjectPropertyViewController: SubjectPropertyAddressViewControllerDelegate{
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
        
        if let stateId = address["stateId"] as? Int{
            subjectPropertyDetail.address?.stateId = stateId
        }
        if let countryId = address["countryId"] as? Int{
            subjectPropertyDetail.address?.countryId = countryId
        }
        if let countryName = address["countryName"] as? String{
            subjectPropertyDetail.address?.countryName = countryName
        }
        if let countyId = address["countyId"] as? Int{
            subjectPropertyDetail.address?.countyId = countyId
        }
        if let countyName = address["countyName"] as? String{
            subjectPropertyDetail.address?.countyName = countyName
        }
        subjectPropertyDetail.address?.street = street
        subjectPropertyDetail.address?.unit = unit
        subjectPropertyDetail.address?.city = city
        subjectPropertyDetail.address?.stateName = stateName
        subjectPropertyDetail.address?.zipCode = zipCode
    }
}

extension RefinanceSubjectPropertyViewController: MixPropertyDetailFollowUpViewControllerDelegate{
    func updateDetails(detail: String) {
        lblPropertyUseDetail.text = detail
    }
}

extension RefinanceSubjectPropertyViewController: FirstMortgageFollowupQuestionsViewControllerDelegate{
    func saveFirstMortageObject(firstMortgage: [String : Any]) {
        self.savedFirstMortgage = firstMortgage
        isFirstMortgage = true
        changeMortgageStatus()
        
        var mortgagePayment: Double = 0.0, unpaidMortgagePayment: Double = 0.0, helocCreditLimit: Double = 0.0, propertyTaxesIncludeinPayment: Bool = false, homeOwnerInsuranceIncludeinPayment: Bool = false, floodInsuranceIncludeinPayment: Bool = false, paidAtClosing: Bool = false, isHeloc: Bool = false
        
        if let firstMortgagePayment = firstMortgage["firstMortgagePayment"] as? Double{
            mortgagePayment = firstMortgagePayment
            self.subjectPropertyDetail.firstMortgage?.firstMortgagePayment = mortgagePayment
        }
        if let unpaidFirstMortgagePayment = firstMortgage["unpaidFirstMortgagePayment"] as? Double{
            unpaidMortgagePayment = unpaidFirstMortgagePayment
            self.subjectPropertyDetail.firstMortgage?.unpaidFirstMortgagePayment = unpaidMortgagePayment
        }
        if let creditLimit = firstMortgage["helocCreditLimit"] as? Double{
            helocCreditLimit = creditLimit
            self.subjectPropertyDetail.firstMortgage?.helocCreditLimit = helocCreditLimit
        }
        if let isPropertyTax = firstMortgage["propertyTaxesIncludeinPayment"] as? Bool{
            propertyTaxesIncludeinPayment = isPropertyTax
            self.subjectPropertyDetail.firstMortgage?.propertyTaxesIncludeinPayment = propertyTaxesIncludeinPayment
        }
        if let isHomeOwner = firstMortgage["homeOwnerInsuranceIncludeinPayment"] as? Bool{
            homeOwnerInsuranceIncludeinPayment = isHomeOwner
            self.subjectPropertyDetail.firstMortgage?.homeOwnerInsuranceIncludeinPayment = homeOwnerInsuranceIncludeinPayment
        }
        if let isFlood = firstMortgage["floodInsuranceIncludeinPayment"] as? Bool{
            floodInsuranceIncludeinPayment = isFlood
            self.subjectPropertyDetail.firstMortgage?.floodInsuranceIncludeinPayment = floodInsuranceIncludeinPayment
        }
        if let isPaid = firstMortgage["paidAtClosing"] as? Bool{
            paidAtClosing = isPaid
            self.subjectPropertyDetail.firstMortgage?.paidAtClosing = paidAtClosing
        }
        if let isHelocInclude = firstMortgage["isHeloc"] as? Bool{
            isHeloc = isHelocInclude
            self.subjectPropertyDetail.firstMortgage?.isHeloc = isHeloc
        }
        
        lblFirstMortgagePayment.text = Int(mortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
        lblFirstMortgageBalance.text = Int(unpaidMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
    }
}

extension RefinanceSubjectPropertyViewController: SecondMortgageFollowupQuestionsViewControllerDelegate{
    func saveSecondMortageObject(secondMortgage: [String : Any]) {
        self.savedSecondMortgage = secondMortgage
        isSecondMortgage = true
        changeMortgageStatus()
        
        var mortgagePayment: Double = 0.0, unpaidMortgagePayment: Double = 0.0, helocCreditLimit: Double = 0.0, isHeloc: Bool = false, paidAtClosing: Bool = false, wasSmTaken: Bool = false, combineWithNewFirstMortgage: Bool = false
        if let secondMortgagePayment = secondMortgage["secondMortgagePayment"] as? Double{
            mortgagePayment = secondMortgagePayment
            self.subjectPropertyDetail.secondMortgage?.secondMortgagePayment = mortgagePayment
        }
        if let unpaidSecondMortgagePayment = secondMortgage["unpaidSecondMortgagePayment"] as? Double{
            unpaidMortgagePayment = unpaidSecondMortgagePayment
            self.subjectPropertyDetail.secondMortgage?.unpaidSecondMortgagePayment = unpaidMortgagePayment
        }
        if let creditLimit = secondMortgage["helocCreditLimit"] as? Double{
            helocCreditLimit = creditLimit
            self.subjectPropertyDetail.secondMortgage?.helocCreditLimit = helocCreditLimit
        }
        if let isHelocInclude = secondMortgage["isHeloc"] as? Bool{
            isHeloc = isHelocInclude
            self.subjectPropertyDetail.secondMortgage?.isHeloc = isHeloc
        }
        if let isPaid = secondMortgage["paidAtClosing"] as? Bool{
            paidAtClosing = isPaid
            self.subjectPropertyDetail.secondMortgage?.paidAtClosing = paidAtClosing
        }
        if let wasSm = secondMortgage["wasSmTaken"] as? Bool{
            wasSmTaken = wasSm
            self.subjectPropertyDetail.secondMortgage?.wasSmTaken = wasSmTaken
        }
        if let isCombine = secondMortgage["combineWithNewFirstMortgage"] as? Bool{
            combineWithNewFirstMortgage = isCombine
            self.subjectPropertyDetail.secondMortgage?.combineWithNewFirstMortgage = combineWithNewFirstMortgage
        }
        
        lblSecondMortgagePayment.text = Int(mortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
        lblSecondMortgageBalance.text = Int(unpaidMortgagePayment).withCommas().replacingOccurrences(of: ".00", with: "")
    }
}

extension RefinanceSubjectPropertyViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return coBorrowerOccupancyArray.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "OccupancyStatusTableViewCell", for: indexPath) as! OccupancyStatusTableViewCell
        cell.borrower = coBorrowerOccupancyArray[indexPath.row]
        cell.setData()
        return cell
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        if (indexPath.row == coBorrowerOccupancyArray.count - 1){
            return 130
        }
        else{
            return 160
        }
    }
}

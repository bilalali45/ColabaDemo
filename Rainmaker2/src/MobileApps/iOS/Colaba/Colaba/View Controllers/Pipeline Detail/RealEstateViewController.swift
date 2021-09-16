//
//  RealEstateViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit
import DropDown

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
    @IBOutlet weak var txtfieldPropertyType: ColabaTextField!
    @IBOutlet weak var btnPropertyTypeDropDown: UIButton!
    @IBOutlet weak var propertyTypeDropDownAnchorView: UIView!
    @IBOutlet weak var txtfieldOccupancyType: ColabaTextField!
    @IBOutlet weak var btnOccupancyTypeDropDown: UIButton!
    @IBOutlet weak var occupancyTypeDropDownAnchorView: UIView!
    @IBOutlet weak var txtfieldCurrentRentalIncome: ColabaTextField!
    @IBOutlet weak var txtfieldRentalIncomeTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldRentalIncomeHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldPropertyStatus: ColabaTextField!
    @IBOutlet weak var btnPropertyStatusDropDown: UIButton!
    @IBOutlet weak var propertyStatusDropDownAnchorView: UIView!
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
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let propertyTypeDropDown = DropDown()
    let occupancyTypeDropDown = DropDown()
    var isFirstMortgage = false
    var isSecondMortgage = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
    }

    //MARK:- Methods and Actions
    
    func setupTextFields(){
        
        addressView.layer.cornerRadius = 6
        addressView.layer.borderWidth = 1
        addressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addressView.dropShadowToCollectionViewCell()
        addressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        txtfieldPropertyType.setTextField(placeholder: "Property Type")
        txtfieldPropertyType.setDelegates(controller: self)
        txtfieldPropertyType.setValidation(validationType: .required)
        txtfieldPropertyType.setTextField(keyboardType: .asciiCapable)
        txtfieldPropertyType.setIsValidateOnEndEditing(validate: true)
        //txtfieldPropertyType.type = .dropdown
        txtfieldPropertyType.addTarget(self, action: #selector(txtfieldPropertyTypeStartEditing), for: .editingDidBegin)
        
        txtfieldOccupancyType.setTextField(placeholder: "Occupancy Type")
        txtfieldOccupancyType.setDelegates(controller: self)
        txtfieldOccupancyType.setValidation(validationType: .required)
        txtfieldOccupancyType.setTextField(keyboardType: .asciiCapable)
        txtfieldOccupancyType.setIsValidateOnEndEditing(validate: true)
        //txtfieldOccupancyType.type = .dropdown
        txtfieldOccupancyType.addTarget(self, action: #selector(txtfieldOccupancyTypeStartEditing), for: .editingDidBegin)
        
        txtfieldCurrentRentalIncome.setTextField(placeholder: "Current Rental Income")
        txtfieldCurrentRentalIncome.setDelegates(controller: self)
        txtfieldCurrentRentalIncome.setTextField(keyboardType: .numberPad)
        txtfieldCurrentRentalIncome.setIsValidateOnEndEditing(validate: true)
        txtfieldCurrentRentalIncome.setValidation(validationType: .required)
        txtfieldCurrentRentalIncome.type = .amount
        
        txtfieldPropertyStatus.setTextField(placeholder: "Property Status")
        txtfieldPropertyStatus.setDelegates(controller: self)
        txtfieldPropertyStatus.setValidation(validationType: .required)
        txtfieldPropertyStatus.setTextField(keyboardType: .asciiCapable)
        txtfieldPropertyStatus.setIsValidateOnEndEditing(validate: true)
        //txtfieldPropertyStatus.type = .dropdown
        //txtfieldPropertyStatus.addTarget(self, action: #selector(txtfieldOccupancyTypeStartEditing), for: .editingDidBegin)
        
        txtfieldHomeOwnerAssociationDues.setTextField(placeholder: "Homeowner’s Association Dues")
        txtfieldHomeOwnerAssociationDues.setDelegates(controller: self)
        txtfieldHomeOwnerAssociationDues.setTextField(keyboardType: .numberPad)
        txtfieldHomeOwnerAssociationDues.setIsValidateOnEndEditing(validate: true)
        txtfieldHomeOwnerAssociationDues.setValidation(validationType: .required)
        txtfieldHomeOwnerAssociationDues.type = .amount
        
        txtfieldPropertyValue.setTextField(placeholder: "Property Value")
        txtfieldPropertyValue.setDelegates(controller: self)
        txtfieldPropertyValue.setTextField(keyboardType: .numberPad)
        txtfieldPropertyValue.setIsValidateOnEndEditing(validate: true)
        txtfieldPropertyValue.setValidation(validationType: .required)
        txtfieldPropertyValue.type = .amount
        
        txtfieldAnnualPropertyTax.setTextField(placeholder: "Annual Property Taxes")
        txtfieldAnnualPropertyTax.setDelegates(controller: self)
        txtfieldAnnualPropertyTax.setTextField(keyboardType: .numberPad)
        txtfieldAnnualPropertyTax.setIsValidateOnEndEditing(validate: true)
        txtfieldAnnualPropertyTax.setValidation(validationType: .required)
        txtfieldAnnualPropertyTax.type = .amount
        
        txtfieldAnnualHomeOwnerInsurance.setTextField(placeholder: "Annual Homeowner's Insurance")
        txtfieldAnnualHomeOwnerInsurance.setDelegates(controller: self)
        txtfieldAnnualHomeOwnerInsurance.setTextField(keyboardType: .numberPad)
        txtfieldAnnualHomeOwnerInsurance.setIsValidateOnEndEditing(validate: true)
        txtfieldAnnualHomeOwnerInsurance.setValidation(validationType: .required)
        txtfieldAnnualHomeOwnerInsurance.type = .amount
        
        txtfieldAnnualFloodInsurance.setTextField(placeholder: "Annual Flood Insurance")
        txtfieldAnnualFloodInsurance.setDelegates(controller: self)
        txtfieldAnnualFloodInsurance.setTextField(keyboardType: .numberPad)
        txtfieldAnnualFloodInsurance.setIsValidateOnEndEditing(validate: true)
        txtfieldAnnualFloodInsurance.setValidation(validationType: .required)
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
        
        propertyTypeDropDown.dismissMode = .onTap
        propertyTypeDropDown.anchorView = propertyTypeDropDownAnchorView
        propertyTypeDropDown.dataSource = kPropertyTypeArray
        propertyTypeDropDown.cancelAction = .some({
            self.btnPropertyTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            self.txtfieldPropertyType.dividerColor = Theme.getSeparatorNormalColor()
            self.txtfieldPropertyType.resignFirstResponder()
        })
        propertyTypeDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnPropertyTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldPropertyType.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldPropertyType.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldPropertyType.text = item
            txtfieldPropertyType.resignFirstResponder()
            txtfieldPropertyType.detail = ""
            propertyTypeDropDown.hide()
            showHideRentalIncome()
        }
        
        occupancyTypeDropDown.dismissMode = .onTap
        occupancyTypeDropDown.anchorView = occupancyTypeDropDownAnchorView
        occupancyTypeDropDown.dataSource = kOccupancyTypeArray
        occupancyTypeDropDown.cancelAction = .some({
            self.btnOccupancyTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            self.txtfieldOccupancyType.dividerColor = Theme.getSeparatorNormalColor()
            self.txtfieldOccupancyType.resignFirstResponder()
        })
        occupancyTypeDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnOccupancyTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldOccupancyType.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldOccupancyType.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldOccupancyType.text = item
            txtfieldOccupancyType.resignFirstResponder()
            txtfieldOccupancyType.detail = ""
            occupancyTypeDropDown.hide()
            showHideRentalIncome()
        }
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
    }
    
    func setScreenHeight(){
        let firstMortgageViewHeight = self.firstMortgageMainView.frame.height
        let secondMortgageViewHeight = self.secondMortgageMainView.frame.height
        
        self.mainViewHeightConstraint.constant = firstMortgageViewHeight + secondMortgageViewHeight + 1000
        
        UIView.animate(withDuration: 0.5) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func addressViewTapped(){
        let vc = Utility.getCurrentEmployerAddressVC()
        vc.isForSubjectProperty = true
        vc.topTitle = "Subject Property Address"
        vc.searchTextFieldPlaceholder = "Search Property Address"
        self.pushToVC(vc: vc)
    }
    
    @objc func txtfieldPropertyTypeStartEditing(){
        txtfieldPropertyType.resignFirstResponder()
        txtfieldPropertyType.dividerColor = Theme.getButtonBlueColor()
        btnPropertyTypeDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
        propertyTypeDropDown.show()
    }

    @objc func txtfieldOccupancyTypeStartEditing(){
        txtfieldOccupancyType.resignFirstResponder()
        txtfieldOccupancyType.dividerColor = Theme.getButtonBlueColor()
        btnOccupancyTypeDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
        occupancyTypeDropDown.show()
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
        isFirstMortgage = true
        changeMortgageStatus()
    }
    
    @objc func firstMortgageNoStackViewTapped(){
        isFirstMortgage = false
        isSecondMortgage = false
        changeMortgageStatus()
    }
    
    @objc func firstMortgageViewTapped(){
        let vc = Utility.getFirstMortgageFollowupQuestionsVC()
        vc.isForRealEstate = true
        self.presentVC(vc: vc)
    }
    
    @objc func secondMortgageYesStackViewTapped(){
        let vc = Utility.getSecondMortgageFollowupQuestionsVC()
        vc.isForRealEstate = true
        self.presentVC(vc: vc)
        isSecondMortgage = true
        changeMortgageStatus()
    }
    
    @objc func secondMortgageNoStackViewTapped(){
        isSecondMortgage = false
        changeMortgageStatus()
    }
    
    @objc func secondMortgageViewTapped(){
        let vc = Utility.getSecondMortgageFollowupQuestionsVC()
        vc.isForRealEstate = true
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
    
    func validate() -> Bool {

        if (!txtfieldPropertyType.validate()) {
            return false
        }
        if (!txtfieldOccupancyType.validate()) {
            return false
        }
        if (!txtfieldCurrentRentalIncome.validate() && !txtfieldCurrentRentalIncome.isHidden){
            return false
        }
        if (!txtfieldPropertyStatus.validate()){
            return false
        }
        if (!txtfieldHomeOwnerAssociationDues.validate()){
            return false
        }
        if (!txtfieldPropertyValue.validate()){
            return false
        }
        if (!txtfieldAnnualPropertyTax.validate()){
            return false
        }
        if (!txtfieldAnnualHomeOwnerInsurance.validate()){
            return false
        }
        if (!txtfieldAnnualFloodInsurance.validate()){
            return false
        }

        return true
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
        
        txtfieldPropertyType.validate()
        txtfieldOccupancyType.validate()
        if (!txtfieldCurrentRentalIncome.isHidden){
            txtfieldCurrentRentalIncome.validate()
        }
        txtfieldPropertyStatus.validate()
        txtfieldHomeOwnerAssociationDues.validate()
        txtfieldPropertyValue.validate()
        txtfieldAnnualPropertyTax.validate()
        txtfieldAnnualHomeOwnerInsurance.validate()
        txtfieldAnnualFloodInsurance.validate()
        
        if validate(){
            self.dismissVC()
        }
    }
    
}

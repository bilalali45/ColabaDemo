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
        
        addAddressView.layer.cornerRadius = 6
        addAddressView.layer.borderWidth = 1
        addAddressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addAddressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        txtfieldPropertyType.setTextField(placeholder: "Property Type")
        txtfieldPropertyType.setDelegates(controller: self)
        txtfieldPropertyType.setValidation(validationType: .required)
        txtfieldPropertyType.type = .dropdown
        txtfieldPropertyType.setDropDownDataSource(kPropertyTypeArray)
        
        txtfieldOccupancyType.setTextField(placeholder: "Occupancy Type")
        txtfieldOccupancyType.setDelegates(controller: self)
        txtfieldOccupancyType.setValidation(validationType: .required)
        txtfieldOccupancyType.type = .dropdown
        txtfieldOccupancyType.setDropDownDataSource(kOccupancyTypeArray)
        
        txtfieldCurrentRentalIncome.setTextField(placeholder: "Current Rental Income")
        txtfieldCurrentRentalIncome.setDelegates(controller: self)
        txtfieldCurrentRentalIncome.setTextField(keyboardType: .numberPad)
        txtfieldCurrentRentalIncome.setIsValidateOnEndEditing(validate: true)
        txtfieldCurrentRentalIncome.setValidation(validationType: .required)
        txtfieldCurrentRentalIncome.type = .amount
        
        txtfieldPropertyStatus.setTextField(placeholder: "Property Status")
        txtfieldPropertyStatus.setDelegates(controller: self)
        txtfieldPropertyStatus.setValidation(validationType: .required)
        txtfieldPropertyStatus.type = .dropdown
        txtfieldPropertyStatus.setDropDownDataSource(kPropertyStatusArray)
        
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
        isValidate = txtfieldOccupancyType.validate()
        isValidate = txtfieldPropertyStatus.validate()
        if !txtfieldCurrentRentalIncome.isHidden {
            isValidate = txtfieldCurrentRentalIncome.validate()
        }
        isValidate = txtfieldHomeOwnerAssociationDues.validate()
        isValidate = txtfieldPropertyValue.validate()
        isValidate = txtfieldAnnualPropertyTax.validate()
        isValidate = txtfieldAnnualHomeOwnerInsurance.validate()
        isValidate = txtfieldAnnualFloodInsurance.validate()
        return isValidate
    }
}

extension RealEstateViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldPropertyType || textField == txtfieldOccupancyType {
            showHideRentalIncome()
        }
    }
}

//
//  RefinanceSubjectPropertyViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/09/2021.
//

import UIKit
import Material
import MonthYearPicker
import DropDown

class RefinanceSubjectPropertyViewController: BaseViewController {
    
    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var btnSaveChanges: UIButton!
    @IBOutlet weak var subjectPropertyTBDView: UIView!
    @IBOutlet weak var lblSubjectPropertyTBD: UILabel!
    @IBOutlet weak var btnSubjectPropertyTBD: UIButton!
    @IBOutlet weak var subjectPropertyAddressView: UIView!
    @IBOutlet weak var subjectPropertyAddressViewHeightConstraint: NSLayoutConstraint! //50 or 110
    @IBOutlet weak var lblSubjectPropertyAddress: UILabel!
    @IBOutlet weak var btnSubjectPropertyAddress: UIButton!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var txtfieldPropertyType: TextField!
    @IBOutlet weak var btnPropertyTypeDropDown: UIButton!
    @IBOutlet weak var propertyTypeDropDownAnchorView: UIView!
    @IBOutlet weak var txtfieldOccupancyType: TextField!
    @IBOutlet weak var btnOccupancyTypeDropDown: UIButton!
    @IBOutlet weak var occupancyTypeDropDownAnchorView: UIView!
    @IBOutlet weak var txtfieldRentalIncome: TextField!
    @IBOutlet weak var txtfieldRentalIncomeTopConstraint: NSLayoutConstraint! //30 or 0
    @IBOutlet weak var txtfieldRentalIncomeHeightConstraint: NSLayoutConstraint! // 39 or 0
    @IBOutlet weak var rentalIncomeDollarView: UIView!
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
    @IBOutlet weak var txtfieldAppraisedPropertyValue: TextField!
    @IBOutlet weak var appraisedPropertyValueDollarView: UIView!
    @IBOutlet weak var txtfieldHomePurchaseDate: TextField!
    @IBOutlet weak var txtfieldHomeOwnerAssociationDues: TextField!
    @IBOutlet weak var homeOwnerAssociationDollarView: UIView!
    @IBOutlet weak var txtfieldTax: TextField!
    @IBOutlet weak var taxDollarView: UIView!
    @IBOutlet weak var txtfieldHomeOwnerInsurance: TextField!
    @IBOutlet weak var homeOwnerInsuranceDollarView: UIView!
    @IBOutlet weak var txtfieldFloodInsurance: TextField!
    @IBOutlet weak var floodInsuranceDollarView: UIView!
    @IBOutlet weak var occupancyStatusView: UIView!
    @IBOutlet weak var lblCoBorrowerName: UILabel!
    @IBOutlet weak var occupyingStackView: UIStackView!
    @IBOutlet weak var btnOccupying: UIButton!
    @IBOutlet weak var lblOccupying: UILabel!
    @IBOutlet weak var nonOccupyingStackView: UIStackView!
    @IBOutlet weak var btnNonOccupying: UIButton!
    @IBOutlet weak var lblNonOccupying: UILabel!
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
    
    let propertyTypeDropDown = DropDown()
    let occupancyTypeDropDown = DropDown()
    var isTBDProperty = true
    var isMixedUseProperty = true
    var isOccupying = true
    let homePurchaseDateFormatter = DateFormatter()
    var isFirstMortgage = false
    var isSecondMortgage = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldPropertyType, txtfieldOccupancyType,txtfieldRentalIncome, txtfieldAppraisedPropertyValue, txtfieldHomePurchaseDate, txtfieldHomeOwnerAssociationDues, txtfieldTax, txtfieldHomeOwnerInsurance, txtfieldFloodInsurance])
    }
    
    //MARK:- Methods and Actions
    
    func setMaterialTextFieldsAndViews(textfields: [TextField]){
        for textfield in textfields{
            textfield.dividerActiveColor = Theme.getButtonBlueColor()
            textfield.dividerColor = Theme.getSeparatorNormalColor()
            textfield.placeholderActiveColor = Theme.getAppGreyColor()
            textfield.delegate = self
            textfield.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
            textfield.detailLabel.font = Theme.getRubikRegularFont(size: 12)
            textfield.detailColor = .red
            textfield.detailVerticalOffset = 4
            textfield.placeholderVerticalOffset = 8
            textfield.textColor = Theme.getAppBlackColor()
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
        
        txtfieldRentalIncome.addTarget(self, action: #selector(txtfieldRentalIncomeChanged), for: .editingChanged)
        txtfieldAppraisedPropertyValue.addTarget(self, action: #selector(txtfieldAppraisedValueChanged), for: .editingChanged)
        txtfieldHomeOwnerAssociationDues.addTarget(self, action: #selector(txtfieldAssociationDuesChanged), for: .editingChanged)
        txtfieldTax.addTarget(self, action: #selector(txtfieldTaxChanged), for: .editingChanged)
        txtfieldHomeOwnerInsurance.addTarget(self, action: #selector(txtfieldHomeInsuranceChanged), for: .editingChanged)
        txtfieldFloodInsurance.addTarget(self, action: #selector(txtfieldFloodInsuranceChanged), for: .editingChanged)
        
        homePurchaseDateFormatter.dateStyle = .medium
        homePurchaseDateFormatter.dateFormat = "MM/yyyy"
        txtfieldHomePurchaseDate.addInputViewMonthYearDatePicker(target: self, selector: #selector(dateChanged))
        
        occupyingStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(occupyingStackViewTapped)))
        nonOccupyingStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(nonOccupyingStackViewTapped)))
        
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
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
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
        
    }
    
    func setPlaceholderLabelColorAfterTextFilled(selectedTextField: UITextField, allTextFields: [TextField]){
        for allTextField in allTextFields{
            if (allTextField == selectedTextField){
                if (allTextField.text == ""){
                    allTextField.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                }
                else{
                    allTextField.placeholderLabel.textColor = Theme.getAppGreyColor()
                }
            }
        }
    }
    
    func setScreenHeight(){
        let firstMortgageViewHeight = self.firstMortgageMainView.frame.height
        let secondMortgageViewHeight = self.secondMortgageMainView.frame.height
        
        self.mainViewHeightConstraint.constant = firstMortgageViewHeight + secondMortgageViewHeight + 1500
        
        UIView.animate(withDuration: 0.5) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func tbdViewTapped(){
        isTBDProperty = true
        changedSubjectPropertyType()
    }
    
    @objc func addressViewTapped(){
        let vc = Utility.getSubjectPropertyAddressVC()
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
            rentalIncomeDollarView.isHidden = true
            txtfieldRentalIncome.isHidden = true
            txtfieldRentalIncome.resignFirstResponder()
            txtfieldRentalIncome.text = ""
            txtfieldRentalIncome.textInsetsPreset = .none
            txtfieldRentalIncome.placeholderHorizontalOffset = 0
        }
        setScreenHeight()
    }
    
    @objc func yesStackViewTapped(){
        let vc = Utility.getMixPropertyDetailFollowUpVC()
        self.presentVC(vc: vc)
        isMixedUseProperty = true
        changeMixedUseProperty()
    }
    
    @objc func noStackViewTapped(){
        isMixedUseProperty = false
        changeMixedUseProperty()
    }
    
    @objc func changeMixedUseProperty(){
        btnYes.setImage(UIImage(named: isMixedUseProperty ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isMixedUseProperty ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnNo.setImage(UIImage(named: isMixedUseProperty ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
        lblNo.font = isMixedUseProperty ?  Theme.getRubikRegularFont(size: 15) : Theme.getRubikMediumFont(size: 15)
        propertyDetailView.isHidden = !isMixedUseProperty
        propertyViewHeightConstraint.constant = isMixedUseProperty ? 347 : 203
        setScreenHeight()
    }
    
    @objc func propertyDetailViewTapped(){
        let vc = Utility.getMixPropertyDetailFollowUpVC()
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
        btnOccupying.setImage(UIImage(named: isOccupying ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblOccupying.font = isOccupying ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnNonOccupying.setImage(UIImage(named: isOccupying ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
        lblNonOccupying.font = isOccupying ?  Theme.getRubikRegularFont(size: 15) : Theme.getRubikMediumFont(size: 15)
    }
    
    @objc func firstMortgageYesStackViewTapped(){
        let vc = Utility.getFirstMortgageFollowupQuestionsVC()
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
        self.presentVC(vc: vc)
    }
    
    @objc func secondMortgageYesStackViewTapped(){
        let vc = Utility.getSecondMortgageFollowupQuestionsVC()
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
    
    @objc func txtfieldRentalIncomeChanged(){
        if let amount = Int(txtfieldRentalIncome.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldRentalIncome.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func txtfieldAppraisedValueChanged(){
        if let amount = Int(txtfieldAppraisedPropertyValue.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldAppraisedPropertyValue.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func txtfieldAssociationDuesChanged(){
        if let amount = Int(txtfieldHomeOwnerAssociationDues.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldHomeOwnerAssociationDues.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func txtfieldTaxChanged(){
        if let amount = Int(txtfieldTax.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldTax.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func txtfieldHomeInsuranceChanged(){
        if let amount = Int(txtfieldHomeOwnerInsurance.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldHomeOwnerInsurance.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func txtfieldFloodInsuranceChanged(){
        if let amount = Int(txtfieldFloodInsurance.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldFloodInsurance.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func dateChanged() {
        if let  datePicker = self.txtfieldHomePurchaseDate.inputView as? MonthYearPickerView {
            self.txtfieldHomePurchaseDate.text = homePurchaseDateFormatter.string(from: datePicker.date)
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        self.goBack()
    }
}

extension RefinanceSubjectPropertyViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        
        if (textField == txtfieldPropertyType){
            textField.endEditing(true)
            txtfieldPropertyType.dividerColor = Theme.getButtonBlueColor()
            btnPropertyTypeDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            propertyTypeDropDown.show()
        }
        
        if (textField == txtfieldOccupancyType){
            textField.endEditing(true)
            txtfieldOccupancyType.dividerColor = Theme.getButtonBlueColor()
            btnOccupancyTypeDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            occupancyTypeDropDown.show()
        }
        
        if (textField == txtfieldRentalIncome){
            txtfieldRentalIncome.textInsetsPreset = .horizontally5
            txtfieldRentalIncome.placeholderHorizontalOffset = -24
            rentalIncomeDollarView.isHidden = false
        }
        
        if (textField == txtfieldAppraisedPropertyValue){
            txtfieldAppraisedPropertyValue.textInsetsPreset = .horizontally5
            txtfieldAppraisedPropertyValue.placeholderHorizontalOffset = -24
            appraisedPropertyValueDollarView.isHidden = false
        }
        
        if (textField == txtfieldHomePurchaseDate){
            dateChanged()
        }
        
        if (textField == txtfieldHomeOwnerAssociationDues){
            txtfieldHomeOwnerAssociationDues.textInsetsPreset = .horizontally5
            txtfieldHomeOwnerAssociationDues.placeholderHorizontalOffset = -24
            homeOwnerAssociationDollarView.isHidden = false
        }
        
        if (textField == txtfieldTax){
            txtfieldTax.textInsetsPreset = .horizontally5
            txtfieldTax.placeholderHorizontalOffset = -24
            taxDollarView.isHidden = false
        }
        
        if (textField == txtfieldHomeOwnerInsurance){
            txtfieldHomeOwnerInsurance.textInsetsPreset = .horizontally5
            txtfieldHomeOwnerInsurance.placeholderHorizontalOffset = -24
            homeOwnerInsuranceDollarView.isHidden = false
        }
        
        if (textField == txtfieldFloodInsurance){
            txtfieldFloodInsurance.textInsetsPreset = .horizontally5
            txtfieldFloodInsurance.placeholderHorizontalOffset = -24
            floodInsuranceDollarView.isHidden = false
        }
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldRentalIncome && txtfieldRentalIncome.text == ""){
            txtfieldRentalIncome.textInsetsPreset = .none
            txtfieldRentalIncome.placeholderHorizontalOffset = 0
            rentalIncomeDollarView.isHidden = true
        }
        
        if (textField == txtfieldAppraisedPropertyValue && txtfieldAppraisedPropertyValue.text == ""){
            txtfieldAppraisedPropertyValue.textInsetsPreset = .none
            txtfieldAppraisedPropertyValue.placeholderHorizontalOffset = 0
            appraisedPropertyValueDollarView.isHidden = true
        }
        
        if (textField == txtfieldHomeOwnerAssociationDues && txtfieldHomeOwnerAssociationDues.text == ""){
            txtfieldHomeOwnerAssociationDues.textInsetsPreset = .none
            txtfieldHomeOwnerAssociationDues.placeholderHorizontalOffset = 0
            homeOwnerAssociationDollarView.isHidden = true
        }
        
        if (textField == txtfieldTax && txtfieldTax.text == ""){
            txtfieldTax.textInsetsPreset = .none
            txtfieldTax.placeholderHorizontalOffset = 0
            taxDollarView.isHidden = true
        }
        
        if (textField == txtfieldHomeOwnerInsurance && txtfieldHomeOwnerInsurance.text == ""){
            txtfieldHomeOwnerInsurance.textInsetsPreset = .none
            txtfieldHomeOwnerInsurance.placeholderHorizontalOffset = 0
            homeOwnerInsuranceDollarView.isHidden = true
        }
        
        if (textField == txtfieldFloodInsurance && txtfieldFloodInsurance.text == ""){
            txtfieldFloodInsurance.textInsetsPreset = .none
            txtfieldFloodInsurance.placeholderHorizontalOffset = 0
            floodInsuranceDollarView.isHidden = true
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldPropertyType, txtfieldOccupancyType,txtfieldRentalIncome, txtfieldAppraisedPropertyValue, txtfieldHomePurchaseDate, txtfieldHomeOwnerAssociationDues, txtfieldTax, txtfieldHomeOwnerInsurance, txtfieldFloodInsurance])
    }
    
}

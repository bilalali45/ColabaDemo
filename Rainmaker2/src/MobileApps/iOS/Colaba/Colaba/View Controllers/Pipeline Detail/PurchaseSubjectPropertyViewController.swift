//
//  PurchaseSubjectPropertyViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/09/2021.
//

import UIKit
import Material
import DropDown

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
    @IBOutlet weak var txtfieldPropertyType: TextField!
    @IBOutlet weak var propertyTypeDropDownAnchorView: UIView!
    @IBOutlet weak var btnPropertyTypeDropDown: UIButton!
    @IBOutlet weak var txtfieldOccupancyType: TextField!
    @IBOutlet weak var occupancyTypeDropDownAnchorView: UIView!
    @IBOutlet weak var btnOccupancyTypeDropDown: UIButton!
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
    
    let propertyTypeDropDown = DropDown()
    let occupancyTypeDropDown = DropDown()
    var isTBDProperty = true
    var isMixedUseProperty: Bool?
    var isOccupying: Bool?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldPropertyType, txtfieldOccupancyType, txtfieldAppraisedPropertyValue, txtfieldTax, txtfieldHomeOwnerInsurance, txtfieldFloodInsurance])
        btnYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblYes.font = Theme.getRubikRegularFont(size: 14)
        propertyDetailView.isHidden = true
        propertyViewHeightConstraint.constant = 203
        setScreenHeight()
        
        btnOccupying.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblOccupying.font = Theme.getRubikRegularFont(size: 14)
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
        
        txtfieldAppraisedPropertyValue.addTarget(self, action: #selector(txtfieldAppraisedValueChanged), for: .editingChanged)
        txtfieldTax.addTarget(self, action: #selector(txtfieldTaxChanged), for: .editingChanged)
        txtfieldHomeOwnerInsurance.addTarget(self, action: #selector(txtfieldHomeInsuranceChanged), for: .editingChanged)
        txtfieldFloodInsurance.addTarget(self, action: #selector(txtfieldFloodInsuranceChanged), for: .editingChanged)
        
        occupyingStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(occupyingStackViewTapped)))
        nonOccupyingStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(nonOccupyingStackViewTapped)))
        

        
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
            lblOccupying.font = occupying ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 14)
            btnNonOccupying.setImage(UIImage(named: occupying ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
            lblNonOccupying.font = occupying ?  Theme.getRubikRegularFont(size: 15) : Theme.getRubikMediumFont(size: 14)
        }
        
    }
    
    @objc func txtfieldAppraisedValueChanged(){
        if let amount = Int(txtfieldAppraisedPropertyValue.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldAppraisedPropertyValue.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
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
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        self.goBack()
    }
}

extension PurchaseSubjectPropertyViewController: UITextFieldDelegate{
    
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
        
        if (textField == txtfieldAppraisedPropertyValue){
            txtfieldAppraisedPropertyValue.textInsetsPreset = .horizontally5
            txtfieldAppraisedPropertyValue.placeholderHorizontalOffset = -24
            appraisedPropertyValueDollarView.isHidden = false
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
        
        if (textField == txtfieldAppraisedPropertyValue && txtfieldAppraisedPropertyValue.text == ""){
            txtfieldAppraisedPropertyValue.textInsetsPreset = .none
            txtfieldAppraisedPropertyValue.placeholderHorizontalOffset = 0
            appraisedPropertyValueDollarView.isHidden = true
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
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldPropertyType, txtfieldOccupancyType, txtfieldAppraisedPropertyValue, txtfieldTax, txtfieldHomeOwnerInsurance, txtfieldFloodInsurance])
    }
    
}

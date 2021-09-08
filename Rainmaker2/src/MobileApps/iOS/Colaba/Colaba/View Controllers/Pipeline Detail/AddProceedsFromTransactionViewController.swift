//
//  AddProceedsFromTransactionViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/09/2021.
//

import UIKit
import Material
import DropDown
import MaterialComponents

class AddProceedsFromTransactionViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldTransactionType: TextField!
    @IBOutlet weak var btnTransactionTypeDropDown: UIButton!
    @IBOutlet weak var transactionTypeDropDownAnchorView: UIView!
    @IBOutlet weak var txtfieldExpectedProceeds: TextField!
    @IBOutlet weak var expectedProceedsDollarView: UIView!
    @IBOutlet weak var loanSecureView: UIView!
    @IBOutlet weak var loanSecureViewTopConstraint: NSLayoutConstraint! // 40 or 0
    @IBOutlet weak var loanSecureViewHeightConstraint: NSLayoutConstraint! // 140 or 0
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var txtfieldAssetsType: TextField!
    @IBOutlet weak var txtfieldAssetsTypeTopConstraint: NSLayoutConstraint! // 20 or 0
    @IBOutlet weak var txtFieldAssetsTypeHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var btnAssetsTypeDropDown: UIButton!
    @IBOutlet weak var assetsTypeDropDownAnchorView: UIView!
    @IBOutlet weak var assetsDescriptionTextViewContainer: UIView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let transactionTypeDropDown = DropDown()
    var isLoanSecureByAnAsset = false
    let assetsTypeDropDown = DropDown()
    var txtViewAssetsDescription = MDCFilledTextArea()
    private let validation: Validation
    
    init(validation: Validation) {
        self.validation = validation
        super.init(nibName: nil, bundle: nil)
    }
    
    required init?(coder: NSCoder) {
        self.validation = Validation()
        super.init(coder: coder)
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldTransactionType, txtfieldExpectedProceeds, txtfieldAssetsType])
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
        
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        
        let estimatedFrame = assetsDescriptionTextViewContainer.frame
        txtViewAssetsDescription = MDCFilledTextArea(frame: estimatedFrame)
        txtViewAssetsDescription.isHidden = true
        txtViewAssetsDescription.label.text = "Asset Description"
        txtViewAssetsDescription.textView.text = ""
        txtViewAssetsDescription.leadingAssistiveLabel.text = ""
        txtViewAssetsDescription.setFilledBackgroundColor(.clear, for: .normal)
        txtViewAssetsDescription.setFilledBackgroundColor(.clear, for: .disabled)
        txtViewAssetsDescription.setFilledBackgroundColor(.clear, for: .editing)
        txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
        txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .disabled)
        txtViewAssetsDescription.setUnderlineColor(Theme.getButtonBlueColor(), for: .editing)
        txtViewAssetsDescription.leadingEdgePaddingOverride = 0
        txtViewAssetsDescription.setFloatingLabel(Theme.getAppGreyColor(), for: .normal)
        txtViewAssetsDescription.setFloatingLabel(Theme.getAppGreyColor(), for: .disabled)
        txtViewAssetsDescription.setFloatingLabel(Theme.getAppGreyColor(), for: .editing)
        txtViewAssetsDescription.label.font = Theme.getRubikRegularFont(size: 13)
        txtViewAssetsDescription.setNormalLabel(Theme.getButtonGreyTextColor(), for: .normal)
        txtViewAssetsDescription.setNormalLabel(Theme.getButtonGreyTextColor(), for: .editing)
        txtViewAssetsDescription.setNormalLabel(Theme.getButtonGreyTextColor(), for: .disabled)
        txtViewAssetsDescription.setTextColor(Theme.getAppBlackColor(), for: .normal)
        txtViewAssetsDescription.setTextColor(Theme.getAppBlackColor(), for: .editing)
        txtViewAssetsDescription.setTextColor(Theme.getAppBlackColor(), for: .disabled)
        txtViewAssetsDescription.textView.font = Theme.getRubikRegularFont(size: 15)
        txtViewAssetsDescription.leadingAssistiveLabel.font = Theme.getRubikRegularFont(size: 12)
        txtViewAssetsDescription.setLeadingAssistiveLabel(.red, for: .normal)
        txtViewAssetsDescription.setLeadingAssistiveLabel(.red, for: .editing)
        txtViewAssetsDescription.setLeadingAssistiveLabel(.red, for: .disabled)
        txtViewAssetsDescription.textView.textColor = .black
        txtViewAssetsDescription.textView.delegate = self
        mainView.addSubview(txtViewAssetsDescription)
        
        txtfieldTransactionType.textInsets = UIEdgeInsets(top: 0, left: 0, bottom: 0, right: 24)
        transactionTypeDropDown.dismissMode = .onTap
        transactionTypeDropDown.anchorView = transactionTypeDropDownAnchorView
        transactionTypeDropDown.dataSource = kTransactionTypeArray
        transactionTypeDropDown.cancelAction = .some({
            self.btnTransactionTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            self.txtfieldTransactionType.dividerColor = Theme.getSeparatorNormalColor()
        })
        transactionTypeDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnTransactionTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldTransactionType.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldTransactionType.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldTransactionType.text = item
            txtfieldTransactionType.detail = ""
            transactionTypeDropDown.hide()
            txtfieldExpectedProceeds.isHidden = false
            if (item == "Proceeds From A Loan"){
                loanSecureView.isHidden = false
                loanSecureViewTopConstraint.constant = 40
                loanSecureViewHeightConstraint.constant = 140
                txtfieldAssetsType.isHidden = true
                btnAssetsTypeDropDown.isHidden = true
                txtfieldAssetsTypeTopConstraint.constant = 0
                txtFieldAssetsTypeHeightConstraint.constant = 0
                assetsDescriptionTextViewContainer.isHidden = true
                txtViewAssetsDescription.isHidden = true
                txtViewAssetsDescription.frame = assetsDescriptionTextViewContainer.frame
            }
            else{
                loanSecureView.isHidden = true
                loanSecureViewTopConstraint.constant = 0
                loanSecureViewHeightConstraint.constant = 0
                txtfieldAssetsType.isHidden = true
                btnAssetsTypeDropDown.isHidden = true
                txtfieldAssetsTypeTopConstraint.constant = 0
                txtFieldAssetsTypeHeightConstraint.constant = 0
                assetsDescriptionTextViewContainer.isHidden = false
                txtViewAssetsDescription.isHidden = false
                txtViewAssetsDescription.frame = assetsDescriptionTextViewContainer.frame
            }
            
            setScreenHeight()
        }
        
        assetsTypeDropDown.dismissMode = .onTap
        assetsTypeDropDown.anchorView = assetsTypeDropDownAnchorView
        assetsTypeDropDown.dataSource = kAssetsTypeArray
        assetsTypeDropDown.cancelAction = .some({
            self.btnAssetsTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            self.txtfieldAssetsType.dividerColor = Theme.getSeparatorNormalColor()
        })
        assetsTypeDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnAssetsTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldAssetsType.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldAssetsType.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldAssetsType.text = item
            txtfieldAssetsType.detail = ""
            assetsTypeDropDown.hide()
            assetsDescriptionTextViewContainer.isHidden = item != "Other"
            txtViewAssetsDescription.isHidden = item != "Other"
            txtViewAssetsDescription.frame = assetsDescriptionTextViewContainer.frame
            setScreenHeight()
        }
        
        txtfieldExpectedProceeds.addTarget(self, action: #selector(textfieldExpectedProceedsChanged), for: .editingChanged)
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
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
        UIView.animate(withDuration: 0.5) {
            self.view.layoutSubviews()
        }
    }
    
    @objc func textfieldExpectedProceedsChanged(){
        if let amount = Int(txtfieldExpectedProceeds.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldExpectedProceeds.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func yesStackViewTapped(){
        isLoanSecureByAnAsset = true
        txtfieldAssetsType.text = ""
        assetsDescriptionTextViewContainer.isHidden = true
        txtViewAssetsDescription.isHidden = true
        txtViewAssetsDescription.textView.text = ""
        changeLoanSecureStatus()
    }
    
    @objc func noStackViewTapped(){
        isLoanSecureByAnAsset = false
        txtfieldAssetsType.text = ""
        assetsDescriptionTextViewContainer.isHidden = true
        txtViewAssetsDescription.isHidden = true
        txtViewAssetsDescription.textView.text = ""
        changeLoanSecureStatus()
    }
    
    func changeLoanSecureStatus(){
        btnYes.setImage(UIImage(named: isLoanSecureByAnAsset ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isLoanSecureByAnAsset ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnNo.setImage(UIImage(named: !isLoanSecureByAnAsset ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = !isLoanSecureByAnAsset ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        txtfieldAssetsType.isHidden = !isLoanSecureByAnAsset
        btnAssetsTypeDropDown.isHidden = !isLoanSecureByAnAsset
        txtfieldAssetsTypeTopConstraint.constant = isLoanSecureByAnAsset ? 20 : 0
        txtFieldAssetsTypeHeightConstraint.constant = isLoanSecureByAnAsset ? 39 : 0
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        do{
            let transactionType = try validation.validateTransactionType(txtfieldTransactionType.text)
            DispatchQueue.main.async {
                self.txtfieldTransactionType.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldTransactionType.detail = ""
            }
            
        }
        catch{
            self.txtfieldTransactionType.dividerColor = .red
            self.txtfieldTransactionType.detail = error.localizedDescription
        }
  
        do{
            let expectedProceeds = try validation.validateExpectedProceeds(txtfieldExpectedProceeds.text)
            DispatchQueue.main.async {
                self.txtfieldExpectedProceeds.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldExpectedProceeds.detail = ""
            }
            
        }
        catch{
            self.txtfieldExpectedProceeds.dividerColor = .red
            self.txtfieldExpectedProceeds.detail = error.localizedDescription
        }
        
        do{
            let assetDescription = try validation.validateAssetDescription(txtViewAssetsDescription.textView.text)
            DispatchQueue.main.async {
                self.txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewAssetsDescription.leadingAssistiveLabel.text = ""
            }

        }
        catch{
            self.txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewAssetsDescription.leadingAssistiveLabel.text = error.localizedDescription
        }
    
        if (isLoanSecureByAnAsset){
            do{
                let assetsType = try validation.validateAssetsType(txtfieldAssetsType.text)
                DispatchQueue.main.async {
                    self.txtfieldAssetsType.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldAssetsType.detail = ""
                }
                
            }
            catch{
                self.txtfieldAssetsType.dividerColor = .red
                self.txtfieldAssetsType.detail = error.localizedDescription
            }
        }
        
        if (txtfieldTransactionType.text == "Proceeds From A Loan"){
            if (isLoanSecureByAnAsset){
                if (txtfieldAssetsType.text == "Other"){
                    if (txtfieldTransactionType.text != "" && txtfieldExpectedProceeds.text != "" && txtViewAssetsDescription.textView.text != ""){
                        self.dismissVC()
                    }
                }
                else{
                    if (txtfieldTransactionType.text != "" && txtfieldExpectedProceeds.text != "" && txtfieldAssetsType.text != ""){
                        self.dismissVC()
                    }
                }
            }
            else{
                if (txtfieldTransactionType.text != "" && txtfieldExpectedProceeds.text != ""){
                    self.dismissVC()
                }
            }
        }
        else{
            if (txtfieldTransactionType.text != "" && txtfieldExpectedProceeds.text != "" && txtViewAssetsDescription.textView.text != ""){
                self.dismissVC()
            }
        }
        
        
    }
}

extension AddProceedsFromTransactionViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        
        if (textField == txtfieldTransactionType){
            textField.endEditing(true)
            txtfieldTransactionType.dividerColor = Theme.getButtonBlueColor()
            btnTransactionTypeDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            transactionTypeDropDown.show()
        }
        
        if (textField == txtfieldExpectedProceeds){
            txtfieldExpectedProceeds.textInsetsPreset = .horizontally5
            txtfieldExpectedProceeds.placeholderHorizontalOffset = -24
            expectedProceedsDollarView.isHidden = false
        }
        
        if (textField == txtfieldAssetsType){
            textField.endEditing(true)
            txtfieldAssetsType.dividerColor = Theme.getButtonBlueColor()
            btnAssetsTypeDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            assetsTypeDropDown.show()
        }
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldTransactionType){
            if !(kTransactionTypeArray.contains(txtfieldTransactionType.text!)){
                txtfieldTransactionType.text = ""
                txtfieldTransactionType.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                transactionTypeDropDown.hide()
            }
            
            btnTransactionTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            do{
                let transactionType = try validation.validateTransactionType(txtfieldTransactionType.text)
                DispatchQueue.main.async {
                    self.txtfieldTransactionType.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldTransactionType.detail = ""
                }
                
            }
            catch{
                self.txtfieldTransactionType.dividerColor = .red
                self.txtfieldTransactionType.detail = error.localizedDescription
            }

        }
        
        if (textField == txtfieldExpectedProceeds){
            do{
                let expectedProceeds = try validation.validateExpectedProceeds(txtfieldExpectedProceeds.text)
                DispatchQueue.main.async {
                    self.txtfieldExpectedProceeds.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldExpectedProceeds.detail = ""
                }
                
            }
            catch{
                self.txtfieldExpectedProceeds.dividerColor = .red
                self.txtfieldExpectedProceeds.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldAssetsType){
            if !(kAssetsTypeArray.contains(txtfieldAssetsType.text!)){
                txtfieldAssetsType.text = ""
                txtfieldAssetsType.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                assetsTypeDropDown.hide()
            }
            
            btnAssetsTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            do{
                let assetsType = try validation.validateAssetsType(txtfieldAssetsType.text)
                DispatchQueue.main.async {
                    self.txtfieldAssetsType.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldAssetsType.detail = ""
                }
                
            }
            catch{
                self.txtfieldAssetsType.dividerColor = .red
                self.txtfieldAssetsType.detail = error.localizedDescription
            }

        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldTransactionType, txtfieldExpectedProceeds, txtfieldAssetsType])
    }
    
}

extension AddProceedsFromTransactionViewController: UITextViewDelegate{
    
    func textViewDidEndEditing(_ textView: UITextView) {
        do{
            let assetDescription = try validation.validateAssetDescription(txtViewAssetsDescription.textView.text)
            DispatchQueue.main.async {
                self.txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewAssetsDescription.leadingAssistiveLabel.text = ""
            }

        }
        catch{
            self.txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewAssetsDescription.leadingAssistiveLabel.text = error.localizedDescription
        }
    }
    
}

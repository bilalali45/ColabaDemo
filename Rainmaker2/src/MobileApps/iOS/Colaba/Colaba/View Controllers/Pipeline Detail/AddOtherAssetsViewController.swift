//
//  AddOtherAssetsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/09/2021.
//

import UIKit
import Material
import DropDown
import MaterialComponents

class AddOtherAssetsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldAssetType: TextField!
    @IBOutlet weak var btnassetTypeDropDown: UIButton!
    @IBOutlet weak var assetTypeDropDownAnchorView: UIView!
    @IBOutlet weak var txtfieldFinancialInstitution: TextField!
    @IBOutlet weak var txtfieldFinancialInstitutionTopConstraint: NSLayoutConstraint! //30 or 0
    @IBOutlet weak var txtfieldFinancialInsitutionHeightConstraint: NSLayoutConstraint! //39 or 0
    @IBOutlet weak var txtfieldAccountNumber: TextField!
    @IBOutlet weak var txtfieldAccountNumberTopConstraint: NSLayoutConstraint! //30 or 0
    @IBOutlet weak var txtfieldAccountNumberHeightConstraint: NSLayoutConstraint! //39 or 0
    @IBOutlet weak var btnEye: UIButton!
    @IBOutlet weak var txtfieldCashValue: TextField!
    @IBOutlet weak var cashValueDollarView: UIView!
    @IBOutlet weak var assetsDescriptionTextViewContainer: UIView!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isShowAccountNumber = false
    let assetTypeDropDown = DropDown()
    private let validation: Validation
    var txtViewAssetsDescription = MDCFilledTextArea()
    
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
        setMaterialTextFieldsAndViews(textfields: [txtfieldAssetType, txtfieldFinancialInstitution, txtfieldAccountNumber, txtfieldCashValue])
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
        
        let estimatedFrame = assetsDescriptionTextViewContainer.frame
        txtViewAssetsDescription = MDCFilledTextArea(frame: estimatedFrame)
        txtViewAssetsDescription.label.text = "Asset Description"
        txtViewAssetsDescription.isHidden = true
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
        
        txtfieldAssetType.textInsets = UIEdgeInsets(top: 0, left: 0, bottom: 0, right: 24)
        
        assetTypeDropDown.dismissMode = .onTap
        assetTypeDropDown.anchorView = assetTypeDropDownAnchorView
        assetTypeDropDown.dataSource = kOtherAssetsTypeArray
        assetTypeDropDown.cancelAction = .some({
            self.btnassetTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            self.txtfieldAssetType.dividerColor = Theme.getSeparatorNormalColor()
            self.txtfieldAssetType.resignFirstResponder()
        })
        assetTypeDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnassetTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldAssetType.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldAssetType.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldAssetType.text = item
            txtfieldAssetType.resignFirstResponder()
            txtfieldAssetType.detail = ""
            assetTypeDropDown.hide()
            
            if (item == "Trust Account" || item == "Bridge Loan Proceeds" || item == "Individual Development Account (IDA)" || item == "Cash Value of Life Insurance"){
                
                txtfieldFinancialInstitutionTopConstraint.constant = 30
                txtfieldFinancialInsitutionHeightConstraint.constant = 39
                txtfieldAccountNumberTopConstraint.constant = 30
                txtfieldAccountNumberHeightConstraint.constant = 39
                txtfieldFinancialInstitution.isHidden = false
                txtfieldAccountNumber.isHidden = false
                btnEye.isHidden = false
                txtfieldCashValue.isHidden = false
                txtfieldCashValue.text = ""
                txtfieldCashValue.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                txtfieldCashValue.textInsetsPreset = .none
                txtfieldCashValue.placeholderHorizontalOffset = 0
                cashValueDollarView.isHidden = true
                txtfieldCashValue.placeholder = item == "Cash Value of Life Insurance" ? "Market Value" : "Cash or Market Value"
                assetsDescriptionTextViewContainer.isHidden = true
                txtViewAssetsDescription.isHidden = true
                
            }
            else if (item == "Employer Assistance" || item == "Relocation Funds" || item == "Rent Credit" || item == "Lot Equity" || item == "Sweat Equity" || item == "Trade Equity"){
                
                txtfieldFinancialInstitutionTopConstraint.constant = 0
                txtfieldFinancialInsitutionHeightConstraint.constant = 0
                txtfieldAccountNumberTopConstraint.constant = 0
                txtfieldAccountNumberHeightConstraint.constant = 0
                txtfieldFinancialInstitution.isHidden = true
                txtfieldAccountNumber.isHidden = true
                btnEye.isHidden = true
                txtfieldCashValue.isHidden = false
                txtfieldCashValue.text = ""
                txtfieldCashValue.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                txtfieldCashValue.textInsetsPreset = .none
                txtfieldCashValue.placeholderHorizontalOffset = 0
                cashValueDollarView.isHidden = true
                txtfieldCashValue.placeholder = (item == "Rent Credit" || item == "Lot Equity" || item == "Sweat Equity" || item == "Trade Equity") ? "Market Value of Equity" : "Cash Value"
                assetsDescriptionTextViewContainer.isHidden = true
                txtViewAssetsDescription.isHidden = true
                
                
            }
            else if (item == "Other"){
                txtfieldFinancialInstitution.isHidden = true
                txtfieldFinancialInstitutionTopConstraint.constant = 0
                txtfieldFinancialInsitutionHeightConstraint.constant = 0
                txtfieldAccountNumber.isHidden = true
                txtfieldAccountNumberTopConstraint.constant = 0
                txtfieldAccountNumberHeightConstraint.constant = 0
                txtfieldCashValue.isHidden = false
                txtfieldCashValue.placeholder = "Cash or Market Value"
                assetsDescriptionTextViewContainer.isHidden = false
                DispatchQueue.main.asyncAfter(deadline: .now() + 0.01) {
                    txtViewAssetsDescription.frame = assetsDescriptionTextViewContainer.frame
                }
                txtViewAssetsDescription.isHidden = false
            }
            
            setScreenHeight()
            
            
        }
        
        txtfieldCashValue.addTarget(self, action: #selector(textfieldAnnualBaseSalaryChanged), for: .editingChanged)
        

        
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
            self.view.layoutSubviews()
        }
    }
    
    @objc func textfieldAnnualBaseSalaryChanged(){
        if let amount = Int(txtfieldCashValue.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldCashValue.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnEyeTapped(_ sender: UIButton){
        isShowAccountNumber = !isShowAccountNumber
        txtfieldAccountNumber.isSecureTextEntry = !isShowAccountNumber
        btnEye.setImage(UIImage(named: isShowAccountNumber ? "hide" : "eyeIcon"), for: .normal)
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        
        do{
            let assetType = try validation.validateAccountType(txtfieldAssetType.text)
            DispatchQueue.main.async {
                self.txtfieldAssetType.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldAssetType.detail = ""
            }
            
        }
        catch{
            self.txtfieldAssetType.dividerColor = .red
            self.txtfieldAssetType.detail = error.localizedDescription
        }
        
        if (!txtfieldFinancialInstitution.isHidden){
            do{
                let financialInstitution = try validation.validateFinancialInstitution(txtfieldFinancialInstitution.text)
                DispatchQueue.main.async {
                    self.txtfieldFinancialInstitution.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldFinancialInstitution.detail = ""
                }
                
            }
            catch{
                self.txtfieldFinancialInstitution.dividerColor = .red
                self.txtfieldFinancialInstitution.detail = error.localizedDescription
            }
        }
        if (!txtfieldAccountNumber.isHidden){
            do{
                let accountNumber = try validation.validateAccountNumber(txtfieldAccountNumber.text)
                DispatchQueue.main.async {
                    self.txtfieldAccountNumber.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldAccountNumber.detail = ""
                }
                
            }
            catch{
                self.txtfieldAccountNumber.dividerColor = .red
                self.txtfieldAccountNumber.detail = error.localizedDescription
            }
        }
        if (!txtfieldCashValue.isHidden){
            do{
                let cashValue = try validation.validateCashValue(txtfieldCashValue.text)
                DispatchQueue.main.async {
                    self.txtfieldCashValue.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldCashValue.detail = ""
                }
                
            }
            catch{
                self.txtfieldCashValue.dividerColor = .red
                self.txtfieldCashValue.detail = error.localizedDescription
            }
        }
        if (!txtViewAssetsDescription.isHidden){
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
        
        if (txtfieldAssetType.text != "" && !txtfieldFinancialInstitution.isHidden && txtfieldFinancialInstitution.text != "" && !txtfieldAccountNumber.isHidden && txtfieldAccountNumber.text != "" && txtfieldCashValue.text != ""){
            self.dismissVC()
        }
        else if (txtfieldAssetType.text != "" && txtfieldFinancialInstitution.isHidden && txtfieldAccountNumber.isHidden && txtfieldCashValue.text != "" && txtViewAssetsDescription.isHidden){
            self.dismissVC()
        }
        else if (txtfieldAssetType.text != "" && txtfieldFinancialInstitution.isHidden && txtfieldAccountNumber.isHidden && txtfieldCashValue.text != "" && !txtViewAssetsDescription.isHidden && txtViewAssetsDescription.textView.text != ""){
            self.dismissVC()
        }
    }
}

extension AddOtherAssetsViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        
        if (textField == txtfieldAssetType){
            textField.endEditing(true)
            txtfieldAssetType.dividerColor = Theme.getButtonBlueColor()
            btnassetTypeDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            assetTypeDropDown.show()
        }
        
        if (textField == txtfieldCashValue){
            txtfieldCashValue.textInsetsPreset = .horizontally5
            txtfieldCashValue.placeholderHorizontalOffset = -24
            cashValueDollarView.isHidden = false
        }
        
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldAssetType){
            if !(kOtherAssetsTypeArray.contains(txtfieldAssetType.text!)){
                txtfieldAssetType.text = ""
                txtfieldAssetType.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                assetTypeDropDown.hide()
            }
            
            btnassetTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            do{
                let assetType = try validation.validateAccountType(txtfieldAssetType.text)
                DispatchQueue.main.async {
                    self.txtfieldAssetType.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldAssetType.detail = ""
                }
                
            }
            catch{
                self.txtfieldAssetType.dividerColor = .red
                self.txtfieldAssetType.detail = error.localizedDescription
            }

        }
        
        if (textField == txtfieldFinancialInstitution){
            do{
                let financialInstitution = try validation.validateFinancialInstitution(txtfieldFinancialInstitution.text)
                DispatchQueue.main.async {
                    self.txtfieldFinancialInstitution.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldFinancialInstitution.detail = ""
                }
                
            }
            catch{
                self.txtfieldFinancialInstitution.dividerColor = .red
                self.txtfieldFinancialInstitution.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldAccountNumber){
            do{
                let accountNumber = try validation.validateAccountNumber(txtfieldAccountNumber.text)
                DispatchQueue.main.async {
                    self.txtfieldAccountNumber.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldAccountNumber.detail = ""
                }
                
            }
            catch{
                self.txtfieldAccountNumber.dividerColor = .red
                self.txtfieldAccountNumber.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldCashValue){
            do{
                let cashValue = try validation.validateCashValue(txtfieldCashValue.text)
                DispatchQueue.main.async {
                    self.txtfieldCashValue.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldCashValue.detail = ""
                }
                
            }
            catch{
                self.txtfieldCashValue.dividerColor = .red
                self.txtfieldCashValue.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldCashValue && txtfieldCashValue.text == ""){
            txtfieldCashValue.textInsetsPreset = .none
            txtfieldCashValue.placeholderHorizontalOffset = 0
            cashValueDollarView.isHidden = true
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldAssetType, txtfieldFinancialInstitution, txtfieldAccountNumber, txtfieldCashValue])
    }
    
}

extension AddOtherAssetsViewController: UITextViewDelegate{
    
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

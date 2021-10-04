//
//  AddOtherAssetsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/09/2021.
//

import UIKit
import MaterialComponents

class AddOtherAssetsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldAssetType: ColabaTextField!
    @IBOutlet weak var txtfieldFinancialInstitution: ColabaTextField!
    @IBOutlet weak var txtfieldFinancialInstitutionTopConstraint: NSLayoutConstraint! //30 or 0
    @IBOutlet weak var txtfieldFinancialInsitutionHeightConstraint: NSLayoutConstraint! //39 or 0
    @IBOutlet weak var txtfieldAccountNumber: ColabaTextField!
    @IBOutlet weak var txtfieldAccountNumberTopConstraint: NSLayoutConstraint! //30 or 0
    @IBOutlet weak var txtfieldAccountNumberHeightConstraint: NSLayoutConstraint! //39 or 0
    @IBOutlet weak var txtfieldCashValue: ColabaTextField!
    @IBOutlet weak var assetsDescriptionTextViewContainer: UIView!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var txtViewAssetsDescription = MDCFilledTextArea()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews()
    }
    
    //MARK:- Methods and Actions
    func setMaterialTextFieldsAndViews(){
        
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
        
        setTextFields()
    }
    
    func setTextFields() {
        ///Account Type Text Field
        txtfieldAssetType.setTextField(placeholder: "Asset Type", controller: self, validationType: .required)
        txtfieldAssetType.type = .dropdown
        txtfieldAssetType.setDropDownDataSource(kOtherAssetsTypeArray)
        
        ///Financial Institution Text Field
        txtfieldFinancialInstitution.setTextField(placeholder: "Financial Institution", controller: self, validationType: .required)
        
        ///Account Number Text Field
        txtfieldAccountNumber.setTextField(placeholder: "Account Number", controller: self, validationType: .required, keyboardType: .numberPad)
        txtfieldAccountNumber.type = .password
        
        ///Cash Value Text Field
        txtfieldCashValue.setTextField(placeholder: "Cash or Market Value", controller: self, validationType: .required)
        txtfieldCashValue.type = .amount
    }

    
    func setScreenHeight(){
        UIView.animate(withDuration: 0.0) {
            self.view.layoutSubviews()
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        
        if validate() {
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
    
    func validate() -> Bool {
        var isValidate = txtfieldAssetType.validate()
        if !txtfieldFinancialInstitution.isHidden {
            isValidate = txtfieldFinancialInstitution.validate() && isValidate
        }
        if !txtfieldAccountNumber.isHidden {
            isValidate = txtfieldAccountNumber.validate() && isValidate
        }
        if !txtfieldCashValue.isHidden {
            isValidate = txtfieldCashValue.validate() && isValidate
        }
        if !txtViewAssetsDescription.isHidden {
            isValidate = validateTextView() && isValidate
        }
        return isValidate
    }
}

extension AddOtherAssetsViewController: UITextViewDelegate{
    
    func textViewDidEndEditing(_ textView: UITextView) {
        _ = validateTextView()
    }
    
    func validateTextView() -> Bool {
        do{
            let response = try txtViewAssetsDescription.textView.text.validate(type: .required)
            DispatchQueue.main.async {
                self.txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewAssetsDescription.leadingAssistiveLabel.text = ""
            }
            return response
        }
        catch{
            self.txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewAssetsDescription.leadingAssistiveLabel.text = error.localizedDescription
            return false
        }
    }
}

extension AddOtherAssetsViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldAssetType {
            if (option == "Trust Account" || option == "Bridge Loan Proceeds" || option == "Individual Development Account (IDA)" || option == "Cash Value of Life Insurance"){
                
                txtfieldFinancialInstitutionTopConstraint.constant = 30
                txtfieldFinancialInsitutionHeightConstraint.constant = 39
                txtfieldAccountNumberTopConstraint.constant = 30
                txtfieldAccountNumberHeightConstraint.constant = 39
                txtfieldFinancialInstitution.isHidden = false
                txtfieldAccountNumber.isHidden = false
                txtfieldCashValue.isHidden = false
                txtfieldCashValue.text = ""
                txtfieldCashValue.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                txtfieldCashValue.textInsetsPreset = .none
                txtfieldCashValue.placeholderHorizontalOffset = 0
                txtfieldCashValue.placeholder = option == "Cash Value of Life Insurance" ? "Market Value" : "Cash or Market Value"
                assetsDescriptionTextViewContainer.isHidden = true
                txtViewAssetsDescription.isHidden = true
            }
            else if (option == "Employer Assistance" || option == "Relocation Funds" || option == "Rent Credit" || option == "Lot Equity" || option == "Sweat Equity" || option == "Trade Equity"){
                
                txtfieldFinancialInstitutionTopConstraint.constant = 0
                txtfieldFinancialInsitutionHeightConstraint.constant = 0
                txtfieldAccountNumberTopConstraint.constant = 0
                txtfieldAccountNumberHeightConstraint.constant = 0
                txtfieldFinancialInstitution.isHidden = true
                txtfieldAccountNumber.isHidden = true
                txtfieldCashValue.isHidden = false
                txtfieldCashValue.text = ""
                txtfieldCashValue.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                txtfieldCashValue.textInsetsPreset = .none
                txtfieldCashValue.placeholderHorizontalOffset = 0
                txtfieldCashValue.placeholder = (option == "Rent Credit" || option == "Lot Equity" || option == "Sweat Equity" || option == "Trade Equity") ? "Market Value of Equity" : "Cash Value"
                assetsDescriptionTextViewContainer.isHidden = true
                txtViewAssetsDescription.isHidden = true
            }
            else if (option == "Other"){
                txtfieldFinancialInstitution.isHidden = true
                txtfieldFinancialInstitutionTopConstraint.constant = 0
                txtfieldFinancialInsitutionHeightConstraint.constant = 0
                txtfieldAccountNumber.isHidden = true
                txtfieldAccountNumberTopConstraint.constant = 0
                txtfieldAccountNumberHeightConstraint.constant = 0
                txtfieldCashValue.isHidden = false
                txtfieldCashValue.placeholder = "Cash or Market Value"
                assetsDescriptionTextViewContainer.isHidden = false
                DispatchQueue.main.asyncAfter(deadline: .now() + 0.01) { [weak self] in
                    self?.txtViewAssetsDescription.frame = self?.assetsDescriptionTextViewContainer.frame ?? CGRect(x: 0, y: 0, width: 0, height: 0)
                }
                txtViewAssetsDescription.isHidden = false
            }
            setScreenHeight()
        }
    }
}

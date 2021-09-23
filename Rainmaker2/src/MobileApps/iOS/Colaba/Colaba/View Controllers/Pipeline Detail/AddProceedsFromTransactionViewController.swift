//
//  AddProceedsFromTransactionViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/09/2021.
//

import UIKit
import MaterialComponents

class AddProceedsFromTransactionViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldTransactionType: ColabaTextField!
    @IBOutlet weak var txtfieldExpectedProceeds: ColabaTextField!
    @IBOutlet weak var loanSecureView: UIView!
    @IBOutlet weak var loanSecureViewTopConstraint: NSLayoutConstraint! // 40 or 0
    @IBOutlet weak var loanSecureViewHeightConstraint: NSLayoutConstraint! // 140 or 0
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var txtfieldAssetsType: ColabaTextField!
    @IBOutlet weak var txtfieldAssetsTypeTopConstraint: NSLayoutConstraint! // 20 or 0
    @IBOutlet weak var txtFieldAssetsTypeHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var assetsDescriptionTextViewContainer: UIView!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isLoanSecureByAnAsset = false
    var txtViewAssetsDescription = MDCFilledTextArea()

    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews()
    }
   
    //MARK:- Methods and Actions
    func setMaterialTextFieldsAndViews(){
        
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
        
        setTextFields()
    }
    
    func setTextFields() {

        ///Transaction Type  Text Field
        txtfieldTransactionType.setTextField(placeholder: "Transaction Type")
        txtfieldTransactionType.setDelegates(controller: self)
        txtfieldTransactionType.setValidation(validationType: .required)
        txtfieldTransactionType.type = .dropdown
        txtfieldTransactionType.setDropDownDataSource(kTransactionTypeArray)
        
        ///Expected Proceeds Text Field
        txtfieldExpectedProceeds.setTextField(placeholder: "Expected Proceeds")
        txtfieldExpectedProceeds.setDelegates(controller: self)
        txtfieldExpectedProceeds.setTextField(keyboardType: .numberPad)
        txtfieldExpectedProceeds.setValidation(validationType: .required)
        txtfieldExpectedProceeds.type = .amount
        
        ///Assets Type Text Field
        txtfieldAssetsType.setTextField(placeholder: "Which Asset?")
        txtfieldAssetsType.setDelegates(controller: self)
        txtfieldAssetsType.setValidation(validationType: .required)
        txtfieldAssetsType.type = .dropdown
        txtfieldAssetsType.setDropDownDataSource(kAssetsTypeArray)
    }
    
    func setScreenHeight(){
        UIView.animate(withDuration: 0.0) {
            self.view.layoutSubviews()
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
        txtfieldAssetsTypeTopConstraint.constant = isLoanSecureByAnAsset ? 20 : 0
        txtFieldAssetsTypeHeightConstraint.constant = isLoanSecureByAnAsset ? 39 : 0
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {

        if validate() {
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
    
    func validate() -> Bool {
        var isValidate = txtfieldTransactionType.validate()
        if isLoanSecureByAnAsset && !txtfieldAssetsType.isHidden{
            isValidate = txtfieldAssetsType.validate() && isValidate
        }
        if !txtViewAssetsDescription.isHidden{
            isValidate = validateTextView() && isValidate
        }
        
        isValidate = txtfieldExpectedProceeds.validate() && isValidate
        return isValidate
    }
}

extension AddProceedsFromTransactionViewController: UITextViewDelegate{
    
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

extension AddProceedsFromTransactionViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldTransactionType {
            txtfieldExpectedProceeds.isHidden = false
            if (option == "Proceeds From A Loan"){
                loanSecureView.isHidden = false
                loanSecureViewTopConstraint.constant = 40
                loanSecureViewHeightConstraint.constant = 140
                txtfieldAssetsType.isHidden = true
                txtfieldAssetsTypeTopConstraint.constant = 0
                txtFieldAssetsTypeHeightConstraint.constant = 0
                assetsDescriptionTextViewContainer.isHidden = true
                txtViewAssetsDescription.isHidden = true
                DispatchQueue.main.asyncAfter(deadline: .now() + 0.1) { [weak self] in
                    self?.txtViewAssetsDescription.frame = self?.assetsDescriptionTextViewContainer.frame ?? CGRect(x: 0, y: 0, width: 0, height: 0)
                }
            }
            else{
                loanSecureView.isHidden = true
                loanSecureViewTopConstraint.constant = 0
                loanSecureViewHeightConstraint.constant = 0
                txtfieldAssetsType.isHidden = true
                txtfieldAssetsTypeTopConstraint.constant = 0
                txtFieldAssetsTypeHeightConstraint.constant = 0
                assetsDescriptionTextViewContainer.isHidden = false
                txtViewAssetsDescription.isHidden = false
                DispatchQueue.main.asyncAfter(deadline: .now() + 0.1) { [weak self] in
                    self?.txtViewAssetsDescription.frame = self?.assetsDescriptionTextViewContainer.frame ?? CGRect(x: 0, y: 0, width: 0, height: 0)
                }
            }
            
            setScreenHeight()
        }
        
        if textField == txtfieldAssetsType {
            assetsDescriptionTextViewContainer.isHidden = option != "Other"
            txtViewAssetsDescription.isHidden = option != "Other"
            txtViewAssetsDescription.frame = assetsDescriptionTextViewContainer.frame
            setScreenHeight()
        }
    }
}

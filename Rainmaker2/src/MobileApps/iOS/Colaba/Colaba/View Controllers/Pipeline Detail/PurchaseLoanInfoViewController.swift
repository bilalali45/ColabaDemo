//
//  PurchaseLoanInfoViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 30/08/2021.
//

import UIKit
import Material
import MonthYearPicker
import DropDown

class PurchaseLoanInfoViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldLoanStage: TextField!
    @IBOutlet weak var btnLoanStageDropDown: UIButton!
    @IBOutlet weak var loanStageDropDownAnchorView: UIView!
    @IBOutlet weak var txtfieldPurchasePrice: TextField!
    @IBOutlet weak var purchasePriceDollarView: UIView!
    @IBOutlet weak var txtfieldLoanAmount: TextField!
    @IBOutlet weak var loanAmountDollarView: UIView!
    @IBOutlet weak var txtfieldDownPayment: TextField!
    @IBOutlet weak var downPaymentDollarView: UIView!
    @IBOutlet weak var txtfieldPercentage: TextField!
    @IBOutlet weak var percentageView: UIView!
    @IBOutlet weak var txtfieldClosingDate: TextField!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let loanStageDropDown = DropDown()
    let closingDateFormatter = DateFormatter()
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
        setMaterialTextFieldsAndViews(textfields: [txtfieldLoanStage, txtfieldPurchasePrice, txtfieldLoanAmount, txtfieldDownPayment, txtfieldPercentage, txtfieldClosingDate])
        
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
        
        txtfieldPurchasePrice.addTarget(self, action: #selector(txtfieldPurchasePriceChanged), for: .editingChanged)
        txtfieldLoanAmount.addTarget(self, action: #selector(txtfieldLoanAmountChanged), for: .editingChanged)
        txtfieldDownPayment.addTarget(self, action: #selector(txtfieldDownPaymentChanged), for: .editingChanged)
        txtfieldPercentage.addTarget(self, action: #selector(txtfieldPercentageChanged), for: .editingChanged)
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
        closingDateFormatter.dateStyle = .medium
        closingDateFormatter.dateFormat = "MM/yyyy"
        txtfieldClosingDate.addInputViewMonthYearDatePicker(target: self, selector: #selector(dateChanged))
        
        loanStageDropDown.dismissMode = .onTap
        loanStageDropDown.anchorView = loanStageDropDownAnchorView
        loanStageDropDown.dataSource = kLoanStageArray
        loanStageDropDown.cancelAction = .some({
            self.btnLoanStageDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            self.txtfieldLoanStage.dividerColor = self.txtfieldLoanStage.text == "" ? Theme.getSeparatorErrorColor() : Theme.getSeparatorNormalColor()
        })
        loanStageDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnLoanStageDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldLoanStage.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldLoanStage.detail = ""
            txtfieldLoanStage.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldLoanStage.text = item
            loanStageDropDown.hide()
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
    
    @objc func dateChanged() {
        if let  datePicker = self.txtfieldClosingDate.inputView as? MonthYearPickerView {
            self.txtfieldClosingDate.text = closingDateFormatter.string(from: datePicker.date)
        }
    }
    
    @objc func txtfieldPurchasePriceChanged(){
        if let amount = Int(txtfieldPurchasePrice.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldPurchasePrice.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
            let purchaseAmount = Double(amount)
            let downPayment = Int(purchaseAmount * 0.2)
            self.txtfieldDownPayment.textInsetsPreset = .horizontally5
            self.txtfieldDownPayment.placeholderHorizontalOffset = -24
            self.txtfieldDownPayment.dividerColor = Theme.getSeparatorNormalColor()
            self.txtfieldDownPayment.detail = ""
            downPaymentDollarView.isHidden = false
            
            self.txtfieldDownPayment.text = downPayment.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
            self.txtfieldPercentage.text = "20"
            self.txtfieldPercentage.textInsetsPreset = .horizontally5
            self.txtfieldPercentage.placeholderHorizontalOffset = -24
            self.txtfieldPercentage.dividerColor = Theme.getSeparatorNormalColor()
            self.txtfieldPercentage.detail = ""
            percentageView.isHidden = false
        }
    }
    
    @objc func txtfieldLoanAmountChanged(){
        if let amount = Int(txtfieldLoanAmount.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldLoanAmount.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func txtfieldDownPaymentChanged(){
        if (txtfieldPurchasePrice.text == ""){
            txtfieldDownPayment.text = "\(0)"
        }
        else{
            if let amount = Int(txtfieldDownPayment.text!.replacingOccurrences(of: ",", with: "")){
                txtfieldDownPayment.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
                if let purchaseAmount = Int(txtfieldPurchasePrice.text!.replacingOccurrences(of: ",", with: "")){
                    let doubleAmount = Double(amount)
                    let doublePuchaseAmount = Double(purchaseAmount)
                    var downPaymentPercentage = doubleAmount / doublePuchaseAmount
                    downPaymentPercentage = downPaymentPercentage * 100.0
                    self.txtfieldPercentage.text = "\(Int(downPaymentPercentage.rounded()))"
                    self.txtfieldPercentage.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldPercentage.detail = ""
                }
            }
        }
        
    }
    
    @objc func txtfieldPercentageChanged(){
        if (txtfieldPurchasePrice.text == ""){
            txtfieldPercentage.text = "0"
        }
        else{
            if let percentage = Double(txtfieldPercentage.text!), let purchaseAmount = Double(txtfieldPurchasePrice.text!.replacingOccurrences(of: ",", with: "")){
                let downPaymentPercentage = percentage / 100
                let downPayment = Int(purchaseAmount * downPaymentPercentage)
                self.txtfieldDownPayment.text = downPayment.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
                self.txtfieldDownPayment.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldDownPayment.detail = ""
            }
            else{
                txtfieldPercentage.text = ""
                txtfieldDownPayment.text = "0"
            }
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        
        do{
            let loanStage = try validation.validateLoanStage(txtfieldLoanStage.text)
            DispatchQueue.main.async {
                self.txtfieldLoanStage.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldLoanStage.detail = ""
            }
            
        }
        catch{
            self.txtfieldLoanStage.dividerColor = .red
            self.txtfieldLoanStage.detail = error.localizedDescription
        }
        
        do{
            let purchasePrice = try validation.validatePurchasePrice(txtfieldPurchasePrice.text)
            DispatchQueue.main.async {
                self.txtfieldPurchasePrice.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldPurchasePrice.detail = ""
            }
            
        }
        catch{
            self.txtfieldPurchasePrice.dividerColor = .red
            self.txtfieldPurchasePrice.detail = error.localizedDescription
        }
        
        do{
            let loanAmount = try validation.validateLoanAmount(txtfieldLoanAmount.text)
            DispatchQueue.main.async {
                self.txtfieldLoanAmount.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldLoanAmount.detail = ""
            }
            
        }
        catch{
            self.txtfieldLoanAmount.dividerColor = .red
            self.txtfieldLoanAmount.detail = error.localizedDescription
        }
        
        do{
            let downPayment = try validation.validateDownPayment(txtfieldDownPayment.text)
            DispatchQueue.main.async {
                self.txtfieldDownPayment.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldDownPayment.detail = ""
            }
            
        }
        catch{
            self.txtfieldDownPayment.dividerColor = .red
            self.txtfieldDownPayment.detail = error.localizedDescription
        }
        
        do{
            let downPaymentPercentage = try validation.validateDownPaymentPercentage(txtfieldPercentage.text)
            DispatchQueue.main.async {
                self.txtfieldPercentage.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldPercentage.detail = ""
            }
            
        }
        catch{
            self.txtfieldPercentage.dividerColor = .red
            self.txtfieldPercentage.detail = error.localizedDescription
        }
        
        do{
            let closingDate = try validation.validateClosingDate(txtfieldClosingDate.text)
            DispatchQueue.main.async {
                self.txtfieldClosingDate.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldClosingDate.detail = ""
            }
            
        }
        catch{
            self.txtfieldClosingDate.dividerColor = .red
            self.txtfieldClosingDate.detail = error.localizedDescription
        }
        
        if (txtfieldLoanStage.text != "" && txtfieldPurchasePrice.text != "" && txtfieldLoanAmount.text != "" && txtfieldDownPayment.text != "" && txtfieldPercentage.text != "" && txtfieldClosingDate.text != ""){
            self.goBack()
        }
        
    }
    
}

extension PurchaseLoanInfoViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        
        if (textField == txtfieldLoanStage){
            textField.endEditing(true)
            btnLoanStageDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            loanStageDropDown.show()
            txtfieldLoanStage.dividerColor = Theme.getButtonBlueColor()
            
        }
        
        if (textField == txtfieldPurchasePrice){
            txtfieldPurchasePrice.textInsetsPreset = .horizontally5
            txtfieldPurchasePrice.placeholderHorizontalOffset = -24
            purchasePriceDollarView.isHidden = false
        }
        
        if (textField == txtfieldLoanAmount){
            txtfieldLoanAmount.textInsetsPreset = .horizontally5
            txtfieldLoanAmount.placeholderHorizontalOffset = -24
            loanAmountDollarView.isHidden = false
        }
        
        if (textField == txtfieldDownPayment){
            txtfieldDownPayment.textInsetsPreset = .horizontally5
            txtfieldDownPayment.placeholderHorizontalOffset = -24
            downPaymentDollarView.isHidden = false
        }
        
        if (textField == txtfieldPercentage){
            txtfieldPercentage.textInsetsPreset = .horizontally5
            txtfieldPercentage.placeholderHorizontalOffset = -24
            percentageView.isHidden = false
        }
        
        if (textField == txtfieldClosingDate){
            dateChanged()
        }

    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldPurchasePrice && txtfieldPurchasePrice.text == ""){
            txtfieldPurchasePrice.textInsetsPreset = .none
            txtfieldPurchasePrice.placeholderHorizontalOffset = 0
            purchasePriceDollarView.isHidden = true
        }
        
        if (textField == txtfieldLoanAmount && txtfieldLoanAmount.text == ""){
            txtfieldLoanAmount.textInsetsPreset = .none
            txtfieldLoanAmount.placeholderHorizontalOffset = 0
            loanAmountDollarView.isHidden = true
        }
        
        if (textField == txtfieldDownPayment && txtfieldDownPayment.text == ""){
            txtfieldDownPayment.textInsetsPreset = .none
            txtfieldDownPayment.placeholderHorizontalOffset = 0
            downPaymentDollarView.isHidden = true
        }
        
        if (textField == txtfieldPercentage && txtfieldPercentage.text == ""){
            txtfieldPercentage.textInsetsPreset = .none
            txtfieldPercentage.placeholderHorizontalOffset = 0
            percentageView.isHidden = true
        }
        
        if (textField == txtfieldLoanStage){
            btnLoanStageDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            do{
                let loanStage = try validation.validateLoanStage(txtfieldLoanStage.text)
                DispatchQueue.main.async {
                    self.txtfieldLoanStage.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldLoanStage.detail = ""
                }
                
            }
            catch{
                self.txtfieldLoanStage.dividerColor = .red
                self.txtfieldLoanStage.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldPurchasePrice){
            do{
                let purchasePrice = try validation.validatePurchasePrice(txtfieldPurchasePrice.text)
                DispatchQueue.main.async {
                    self.txtfieldPurchasePrice.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldPurchasePrice.detail = ""
                }
                
            }
            catch{
                self.txtfieldPurchasePrice.dividerColor = .red
                self.txtfieldPurchasePrice.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldLoanAmount){
            do{
                let loanAmount = try validation.validateLoanAmount(txtfieldLoanAmount.text)
                DispatchQueue.main.async {
                    self.txtfieldLoanAmount.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldLoanAmount.detail = ""
                }
                
            }
            catch{
                self.txtfieldLoanAmount.dividerColor = .red
                self.txtfieldLoanAmount.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldDownPayment){
            do{
                let downPayment = try validation.validateDownPayment(txtfieldDownPayment.text)
                DispatchQueue.main.async {
                    self.txtfieldDownPayment.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldDownPayment.detail = ""
                }
                
            }
            catch{
                self.txtfieldDownPayment.dividerColor = .red
                self.txtfieldDownPayment.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldPercentage){
            do{
                let downPaymentPercentage = try validation.validateDownPaymentPercentage(txtfieldPercentage.text)
                DispatchQueue.main.async {
                    self.txtfieldPercentage.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldPercentage.detail = ""
                }
                
            }
            catch{
                self.txtfieldPercentage.dividerColor = .red
                self.txtfieldPercentage.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldClosingDate){
            do{
                let closingDate = try validation.validateClosingDate(txtfieldClosingDate.text)
                DispatchQueue.main.async {
                    self.txtfieldClosingDate.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldClosingDate.detail = ""
                }
                
            }
            catch{
                self.txtfieldClosingDate.dividerColor = .red
                self.txtfieldClosingDate.detail = error.localizedDescription
            }
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldLoanStage, txtfieldPurchasePrice, txtfieldLoanAmount, txtfieldDownPayment, txtfieldPercentage, txtfieldClosingDate])
    }
}

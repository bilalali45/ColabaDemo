//
//  AddGiftFundsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/09/2021.
//

import UIKit
import Material
import DropDown

class AddGiftFundsViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldGiftSource: TextField!
    @IBOutlet weak var btnGiftSourceDropDown: UIButton!
    @IBOutlet weak var giftSourceDropDownAnchorView: UIView!
    @IBOutlet weak var giftTypeView: UIView!
    @IBOutlet weak var cashGiftStackView: UIStackView!
    @IBOutlet weak var btnCashGift: UIButton!
    @IBOutlet weak var lblCashGift: UILabel!
    @IBOutlet weak var giftOfEquityStackView: UIStackView!
    @IBOutlet weak var btnGiftOfEquity: UIButton!
    @IBOutlet weak var lblGiftOfEquity: UILabel!
    @IBOutlet weak var txtfieldCashValue: TextField!
    @IBOutlet weak var cashValueDollarView: UIView!
    @IBOutlet weak var giftDepositView: UIView!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var txtfieldDate: TextField!
    @IBOutlet weak var btnCalendar: UIButton!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let giftSourceDropDown = DropDown()
    var isCashGift = false
    var isGiftDeposit = false
    let dateOfTransferDateFormatter = DateFormatter()
    
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
        setMaterialTextFieldsAndViews(textfields: [txtfieldGiftSource, txtfieldCashValue, txtfieldDate])
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
        
        cashGiftStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(cashGiftStackViewTapped)))
        giftOfEquityStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(giftOfEquityStackViewTapped)))
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        
        txtfieldGiftSource.textInsets = UIEdgeInsets(top: 0, left: 0, bottom: 0, right: 24)
        giftSourceDropDown.dismissMode = .onTap
        giftSourceDropDown.anchorView = giftSourceDropDownAnchorView
        giftSourceDropDown.dataSource = kGiftSourceArray
        giftSourceDropDown.cancelAction = .some({
            self.btnGiftSourceDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            self.txtfieldGiftSource.dividerColor = Theme.getSeparatorNormalColor()
            self.txtfieldGiftSource.resignFirstResponder()
        })
        giftSourceDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnGiftSourceDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldGiftSource.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldGiftSource.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldGiftSource.text = item
            txtfieldGiftSource.resignFirstResponder()
            txtfieldGiftSource.detail = ""
            giftSourceDropDown.hide()
            giftTypeView.isHidden = false
            lblGiftOfEquity.text = (item == "Relative" || item == "Unmarried Partner") ? "Gift Of Equity" : "Grant"
        }
        
        txtfieldCashValue.addTarget(self, action: #selector(textfieldCashValueChanged), for: .editingChanged)
        
        dateOfTransferDateFormatter.dateStyle = .medium
        dateOfTransferDateFormatter.dateFormat = "MM/dd/yyyy"
        txtfieldDate.addInputViewDatePicker(target: self, selector: #selector(dateChanged))
        
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
    
    @objc func cashGiftStackViewTapped(){
        isCashGift = true
        changeGiftType()
    }
    
    @objc func giftOfEquityStackViewTapped(){
        isCashGift = false
        changeGiftType()
        txtfieldDate.isHidden = true
        btnCalendar.isHidden = true
    }
    
    func changeGiftType(){
        btnCashGift.setImage(UIImage(named: isCashGift ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblCashGift.font = isCashGift ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnGiftOfEquity.setImage(UIImage(named: !isCashGift ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblGiftOfEquity.font = !isCashGift ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        
        txtfieldCashValue.isHidden = false
        txtfieldCashValue.placeholder = isCashGift ? "Cash Value" : "Market Value"
        giftDepositView.isHidden = !isCashGift
        
    }
    
    @objc func yesStackViewTapped(){
        isGiftDeposit = true
        changeGiftDepositType()
    }
    
    @objc func noStackViewTapped(){
        isGiftDeposit = false
        changeGiftDepositType()
    }
    
    func changeGiftDepositType(){
        btnYes.setImage(UIImage(named: isGiftDeposit ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isGiftDeposit ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnNo.setImage(UIImage(named: !isGiftDeposit ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = !isGiftDeposit ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        
        txtfieldDate.isHidden = false
        btnCalendar.isHidden = false
        txtfieldDate.placeholder = isGiftDeposit ? "Date of Transfer" : "Expected Date of Transfer"
    }
    
    @objc func textfieldCashValueChanged(){
        if let amount = Int(txtfieldCashValue.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldCashValue.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func dateChanged() {
        if let  datePicker = self.txtfieldDate.inputView as? UIDatePicker {
            self.txtfieldDate.text = dateOfTransferDateFormatter.string(from: datePicker.date)
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        
        do{
            let giftSource = try validation.validateGiftSource(txtfieldGiftSource.text)
            DispatchQueue.main.async {
                self.txtfieldGiftSource.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldGiftSource.detail = ""
            }

        }
        catch{
            self.txtfieldGiftSource.dividerColor = .red
            self.txtfieldGiftSource.detail = error.localizedDescription
        }
        
        if !(txtfieldCashValue.isHidden){
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
        if !(txtfieldDate.isHidden){
            do{
                let dateOfTransfer = try validation.validateDateOfTransfer(txtfieldDate.text)
                DispatchQueue.main.async {
                    self.txtfieldDate.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldDate.detail = ""
                }
                
            }
            catch{
                self.txtfieldDate.dividerColor = .red
                self.txtfieldDate.detail = error.localizedDescription
            }
        }
        
        if (txtfieldGiftSource.text != "" && txtfieldCashValue.isHidden && txtfieldDate.isHidden){
            self.dismissVC()
        }
        else if (txtfieldGiftSource.text != "" && !txtfieldCashValue.isHidden && txtfieldCashValue.text != "" && txtfieldDate.isHidden){
            self.dismissVC()
        }
        else if (txtfieldGiftSource.text != "" && !txtfieldCashValue.isHidden && txtfieldCashValue.text != "" && !txtfieldDate.isHidden && txtfieldDate.text != ""){
            self.dismissVC()
        }
    }
  
}

extension AddGiftFundsViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        
        if (textField == txtfieldGiftSource){
            textField.endEditing(true)
            txtfieldGiftSource.dividerColor = Theme.getButtonBlueColor()
            btnGiftSourceDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            giftSourceDropDown.show()
        }
        
        if (textField == txtfieldCashValue){
            txtfieldCashValue.textInsetsPreset = .horizontally5
            txtfieldCashValue.placeholderHorizontalOffset = -24
            cashValueDollarView.isHidden = false
        }
        
        if (textField == txtfieldDate){
            dateChanged()
        }
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldGiftSource){
            if !(kGiftSourceArray.contains(txtfieldGiftSource.text!)){
                txtfieldGiftSource.text = ""
                txtfieldGiftSource.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                giftSourceDropDown.hide()
            }
            
            btnGiftSourceDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            do{
                let giftSource = try validation.validateGiftSource(txtfieldGiftSource.text)
                DispatchQueue.main.async {
                    self.txtfieldGiftSource.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldGiftSource.detail = ""
                }

            }
            catch{
                self.txtfieldGiftSource.dividerColor = .red
                self.txtfieldGiftSource.detail = error.localizedDescription
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
        
        if (textField == txtfieldDate){
            do{
                let dateOfTransfer = try validation.validateDateOfTransfer(txtfieldDate.text)
                DispatchQueue.main.async {
                    self.txtfieldDate.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldDate.detail = ""
                }
                
            }
            catch{
                self.txtfieldDate.dividerColor = .red
                self.txtfieldDate.detail = error.localizedDescription
            }
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldGiftSource, txtfieldCashValue, txtfieldDate])
    }
}

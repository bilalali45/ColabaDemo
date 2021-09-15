//
//  AddBankAccountViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 07/09/2021.
//

import UIKit
import Material
import DropDown

class AddBankAccountViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldAccountType: TextField!
    @IBOutlet weak var btnAccountTypeDropDown: UIButton!
    @IBOutlet weak var accountTypeDropDownAnchorView: UIView!
    @IBOutlet weak var txtfieldFinancialInstitution: TextField!
    @IBOutlet weak var txtfieldAccountNumber: TextField!
    @IBOutlet weak var btnEye: UIButton!
    @IBOutlet weak var txtfieldAnnualBaseSalary: TextField!
    @IBOutlet weak var annualBaseSalaryDollarView: UIView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    var isShowAccountNumber = false
    let accountTypeDropDown = DropDown()
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
        setMaterialTextFieldsAndViews(textfields: [txtfieldAccountType, txtfieldFinancialInstitution, txtfieldAccountNumber, txtfieldAnnualBaseSalary])
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
        
        accountTypeDropDown.dismissMode = .onTap
        accountTypeDropDown.anchorView = accountTypeDropDownAnchorView
        accountTypeDropDown.dataSource = kAccountTypeArray
        accountTypeDropDown.cancelAction = .some({
            self.btnAccountTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            self.txtfieldAccountType.dividerColor = Theme.getSeparatorNormalColor()
            self.txtfieldAccountType.resignFirstResponder()
        })
        accountTypeDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnAccountTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldAccountType.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldAccountType.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldAccountType.text = item
            txtfieldAccountType.resignFirstResponder()
            txtfieldAccountType.detail = ""
            accountTypeDropDown.hide()
        }
        
        txtfieldAnnualBaseSalary.addTarget(self, action: #selector(textfieldAnnualBaseSalaryChanged), for: .editingChanged)
        
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
    
    @objc func textfieldAnnualBaseSalaryChanged(){
        if let amount = Int(txtfieldAnnualBaseSalary.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldAnnualBaseSalary.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
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
            let accountType = try validation.validateAccountType(txtfieldAccountType.text)
            DispatchQueue.main.async {
                self.txtfieldAccountType.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldAccountType.detail = ""
            }
            
        }
        catch{
            self.txtfieldAccountType.dividerColor = .red
            self.txtfieldAccountType.detail = error.localizedDescription
        }
  
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
    
        do{
            let baseSalary = try validation.validateAnnualBaseSalary(txtfieldAnnualBaseSalary.text)
            DispatchQueue.main.async {
                self.txtfieldAnnualBaseSalary.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldAnnualBaseSalary.detail = ""
            }
            
        }
        catch{
            self.txtfieldAnnualBaseSalary.dividerColor = .red
            self.txtfieldAnnualBaseSalary.detail = error.localizedDescription
        }
        
        if (txtfieldAccountType.text != "" && txtfieldFinancialInstitution.text != "" && txtfieldAccountNumber.text != "" && txtfieldAnnualBaseSalary.text != ""){
            self.dismissVC()
        }
    }
}

extension AddBankAccountViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        
        if (textField == txtfieldAccountType){
            textField.endEditing(true)
            txtfieldAccountType.dividerColor = Theme.getButtonBlueColor()
            btnAccountTypeDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            accountTypeDropDown.show()
        }
        
        if (textField == txtfieldAnnualBaseSalary){
            txtfieldAnnualBaseSalary.textInsetsPreset = .horizontally5
            txtfieldAnnualBaseSalary.placeholderHorizontalOffset = -24
            annualBaseSalaryDollarView.isHidden = false
        }
        
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldAccountType){
            if !(kAccountTypeArray.contains(txtfieldAccountType.text!)){
                txtfieldAccountType.text = ""
                txtfieldAccountType.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                accountTypeDropDown.hide()
            }
            
            btnAccountTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            do{
                let accountType = try validation.validateAccountType(txtfieldAccountType.text)
                DispatchQueue.main.async {
                    self.txtfieldAccountType.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldAccountType.detail = ""
                }
                
            }
            catch{
                self.txtfieldAccountType.dividerColor = .red
                self.txtfieldAccountType.detail = error.localizedDescription
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
        
        if (textField == txtfieldAnnualBaseSalary){
            do{
                let baseSalary = try validation.validateAnnualBaseSalary(txtfieldAnnualBaseSalary.text)
                DispatchQueue.main.async {
                    self.txtfieldAnnualBaseSalary.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldAnnualBaseSalary.detail = ""
                }
                
            }
            catch{
                self.txtfieldAnnualBaseSalary.dividerColor = .red
                self.txtfieldAnnualBaseSalary.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldAnnualBaseSalary && txtfieldAnnualBaseSalary.text == ""){
            txtfieldAnnualBaseSalary.textInsetsPreset = .none
            txtfieldAnnualBaseSalary.placeholderHorizontalOffset = 0
            annualBaseSalaryDollarView.isHidden = true
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldAccountType, txtfieldFinancialInstitution, txtfieldAccountNumber, txtfieldAnnualBaseSalary])
    }
    
}

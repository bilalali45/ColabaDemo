//
//  RefinanceLoanInfoViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 30/08/2021.
//

import UIKit
import Material

class RefinanceLoanInfoViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldLoanStage: TextField!
    @IBOutlet weak var txtfieldAdditionalCashoutAmount: TextField!
    @IBOutlet weak var additionalCashAmountDollarView: UIView!
    @IBOutlet weak var txtfieldLoanAmount: TextField!
    @IBOutlet weak var loanAmountDollarView: UIView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldLoanStage, txtfieldAdditionalCashoutAmount, txtfieldLoanAmount])
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
        txtfieldAdditionalCashoutAmount.addTarget(self, action: #selector(txtfieldAdditionalCashAmountChanged), for: .editingChanged)
        txtfieldLoanAmount.addTarget(self, action: #selector(txtfieldLoanAmountChanged), for: .editingChanged)
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
    
    @objc func txtfieldAdditionalCashAmountChanged(){
        if let amount = Int(txtfieldAdditionalCashoutAmount.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldAdditionalCashoutAmount.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @objc func txtfieldLoanAmountChanged(){
        if let amount = Int(txtfieldLoanAmount.text!.replacingOccurrences(of: ",", with: "")){
            txtfieldLoanAmount.text = amount.withCommas().replacingOccurrences(of: "$", with: "").replacingOccurrences(of: ".00", with: "")
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.goBack()
    }
    
}

extension RefinanceLoanInfoViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        
        if (textField == txtfieldAdditionalCashoutAmount){
            txtfieldAdditionalCashoutAmount.textInsetsPreset = .horizontally5
            txtfieldAdditionalCashoutAmount.placeholderHorizontalOffset = -24
            additionalCashAmountDollarView.isHidden = false
        }
        
        if (textField == txtfieldLoanAmount){
            txtfieldLoanAmount.textInsetsPreset = .horizontally5
            txtfieldLoanAmount.placeholderHorizontalOffset = -24
            loanAmountDollarView.isHidden = false
        }
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldAdditionalCashoutAmount && txtfieldAdditionalCashoutAmount.text == ""){
            txtfieldAdditionalCashoutAmount.textInsetsPreset = .none
            txtfieldAdditionalCashoutAmount.placeholderHorizontalOffset = 0
            additionalCashAmountDollarView.isHidden = true
        }
        
        if (textField == txtfieldLoanAmount && txtfieldLoanAmount.text == ""){
            txtfieldLoanAmount.textInsetsPreset = .none
            txtfieldLoanAmount.placeholderHorizontalOffset = 0
            loanAmountDollarView.isHidden = true
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldLoanStage, txtfieldAdditionalCashoutAmount, txtfieldLoanAmount])
    }
}

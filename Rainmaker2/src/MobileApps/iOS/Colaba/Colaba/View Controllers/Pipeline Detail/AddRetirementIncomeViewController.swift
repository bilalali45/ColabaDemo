//
//  AddRetirementIncomeViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit
import DropDown

class AddRetirementIncomeViewController: BaseViewController {

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldRetirementIncomeType: ColabaTextField!
    @IBOutlet weak var btnRetirementIncomeTypeDropDown: UIButton!
    @IBOutlet weak var retirementIncomeTypeDropDownAnchorView: UIView!
    @IBOutlet weak var txtfieldEmployerName: ColabaTextField!
    @IBOutlet weak var txtfieldEmployerNameHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldEmployerNameTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldMonthlyIncome: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let retirementIncomeTypeDropDown = DropDown()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
    }
        
    //MARK:- Methods and Actions
    
    func setupTextFields(){
        
        txtfieldRetirementIncomeType.setTextField(placeholder: "Retirement Income Type")
        txtfieldRetirementIncomeType.setDelegates(controller: self)
        txtfieldRetirementIncomeType.setValidation(validationType: .required)
        txtfieldRetirementIncomeType.setTextField(keyboardType: .asciiCapable)
        txtfieldRetirementIncomeType.setIsValidateOnEndEditing(validate: true)
        //txtfieldRetirementIncomeType.type = .dropdown
        txtfieldRetirementIncomeType.addTarget(self, action: #selector(txtfieldRetirementIncomeTypeBeginEditing), for: .editingDidBegin)
        
        txtfieldEmployerName.setTextField(placeholder: "Employer Name")
        txtfieldEmployerName.setDelegates(controller: self)
        txtfieldEmployerName.setValidation(validationType: .required)
        txtfieldEmployerName.setTextField(keyboardType: .asciiCapable)
        txtfieldEmployerName.setIsValidateOnEndEditing(validate: true)
        
        txtfieldMonthlyIncome.setTextField(placeholder: "Monthly Income")
        txtfieldMonthlyIncome.setDelegates(controller: self)
        txtfieldMonthlyIncome.setValidation(validationType: .required)
        txtfieldMonthlyIncome.setTextField(keyboardType: .numberPad)
        txtfieldMonthlyIncome.setIsValidateOnEndEditing(validate: true)
        txtfieldMonthlyIncome.type = .amount
        
        retirementIncomeTypeDropDown.dismissMode = .automatic
        retirementIncomeTypeDropDown.anchorView = retirementIncomeTypeDropDownAnchorView
        retirementIncomeTypeDropDown.dataSource = kRetirementIncomeTypeArray
        retirementIncomeTypeDropDown.cancelAction = .some({
            self.btnRetirementIncomeTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            self.txtfieldRetirementIncomeType.dividerColor = Theme.getSeparatorNormalColor()
        })
        retirementIncomeTypeDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnRetirementIncomeTypeDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldRetirementIncomeType.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldRetirementIncomeType.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldRetirementIncomeType.text = item
            txtfieldRetirementIncomeType.detail = ""
            retirementIncomeTypeDropDown.hide()
            if (index == 0 || index == 2){
                txtfieldEmployerName.isHidden = true
                txtfieldEmployerNameTopConstraint.constant = 0
                txtfieldEmployerNameHeightConstraint.constant = 0
                txtfieldMonthlyIncome.isHidden = false
                txtfieldMonthlyIncome.placeholder = index == 0 ? "Monthly Income" : "Monthly Withdrawal"
                self.view.layoutSubviews()
            }
            else{
                txtfieldEmployerName.isHidden = false
                txtfieldEmployerNameTopConstraint.constant = 30
                txtfieldEmployerNameHeightConstraint.constant = 39
                txtfieldEmployerName.placeholder = index == 1 ? "Employer Name" : "Description"
                txtfieldMonthlyIncome.isHidden = false
                txtfieldMonthlyIncome.placeholder = "Monthly Income"
            }
        }
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
    }
    
    @objc func txtfieldRetirementIncomeTypeBeginEditing(){
        txtfieldRetirementIncomeType.resignFirstResponder()
        txtfieldRetirementIncomeType.dividerColor = Theme.getButtonBlueColor()
        btnRetirementIncomeTypeDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
        retirementIncomeTypeDropDown.show()
    }
    
    func validate() -> Bool {

        if (!txtfieldRetirementIncomeType.validate()) {
            return false
        }
        else if (!txtfieldEmployerName.validate() && !txtfieldEmployerName.isHidden){
            return false
        }
        else if (!txtfieldMonthlyIncome.validate()){
            return false
        }

        return true
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        txtfieldRetirementIncomeType.validate()
        txtfieldEmployerName.validate()
        txtfieldMonthlyIncome.validate()
        
        if validate(){
            self.dismissVC()
        }
    }
}

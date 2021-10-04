//
//  AddOtherIncomeViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit

class AddOtherIncomeViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldIncomeType: ColabaTextField!
    @IBOutlet weak var txtfieldDescription: ColabaTextField!
    @IBOutlet weak var txtfieldDescriptionTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldDescriptionHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldMonthlyIncome: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
    }
        
    //MARK:- Methods and Actions
    func setupTextFields(){
        ///Income Type Text Field
        txtfieldIncomeType.setTextField(placeholder: "Income Type", controller: self, validationType: .required)
        txtfieldIncomeType.type = .dropdown
        txtfieldIncomeType.setDropDownDataSource(kOtherIncomeTypeArray)
        
        ///Description Text Field
        txtfieldDescription.setTextField(placeholder: "Description", controller: self, validationType: .required)
        
        ///Monthly Income Text Field
        txtfieldMonthlyIncome.setTextField(placeholder: "Monthly Income", controller: self, validationType: .required)
        txtfieldMonthlyIncome.type = .amount
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate(){
            self.dismissVC()
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldIncomeType.validate()
        if (!txtfieldDescription.isHidden){
            isValidate = txtfieldDescription.validate() && isValidate
        }
        isValidate = txtfieldMonthlyIncome.validate() && isValidate
        return isValidate
    }
}

extension AddOtherIncomeViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldIncomeType {
            txtfieldMonthlyIncome.isHidden = false
            txtfieldMonthlyIncome.placeholder = (option == "Capital Gains" || option == "Interest / Dividends" || option == "Other Income Source") ? "Annual Income" : "Monthly Income"
            txtfieldDescription.isHidden = !(option == "Annuity" || option == "Other Income Source")
            txtfieldDescriptionTopConstraint.constant = (option == "Annuity" || option == "Other Income Source") ? 30 : 0
            txtfieldDescriptionHeightConstraint.constant = (option == "Annuity" || option == "Other Income Source") ? 39 : 0
            self.view.layoutSubviews()
        }
    }
}

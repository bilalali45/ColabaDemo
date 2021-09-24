//
//  AddRetirementIncomeViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit

class AddRetirementIncomeViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldRetirementIncomeType: ColabaTextField!
    @IBOutlet weak var txtfieldEmployerName: ColabaTextField!
    @IBOutlet weak var txtfieldEmployerNameHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldEmployerNameTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldMonthlyIncome: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
    }
        
    //MARK:- Methods and Actions
    func setupTextFields(){
        
        txtfieldRetirementIncomeType.setTextField(placeholder: "Retirement Income Type", controller: self, validationType: .required)
        txtfieldRetirementIncomeType.type = .dropdown
        txtfieldRetirementIncomeType.setDropDownDataSource(kRetirementIncomeTypeArray)
        
        txtfieldEmployerName.setTextField(placeholder: "Employer Name", controller: self, validationType: .required)
        
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
        var isValidate = txtfieldRetirementIncomeType.validate()
        isValidate = txtfieldEmployerName.validate() && isValidate
        isValidate = txtfieldMonthlyIncome.validate() && isValidate
        return isValidate
    }
}

extension AddRetirementIncomeViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldRetirementIncomeType {
            if (atIndex == 0 || atIndex == 2){
                txtfieldEmployerName.isHidden = true
                txtfieldEmployerNameTopConstraint.constant = 0
                txtfieldEmployerNameHeightConstraint.constant = 0
                txtfieldMonthlyIncome.isHidden = false
                txtfieldMonthlyIncome.placeholder = atIndex == 0 ? "Monthly Income" : "Monthly Withdrawal"
                self.view.layoutSubviews()
            }
            else {
                txtfieldEmployerName.isHidden = false
                txtfieldEmployerNameTopConstraint.constant = 30
                txtfieldEmployerNameHeightConstraint.constant = 39
                txtfieldEmployerName.placeholder = atIndex == 1 ? "Employer Name" : "Description"
                txtfieldMonthlyIncome.isHidden = false
                txtfieldMonthlyIncome.placeholder = "Monthly Income"
            }
        }
    }
}

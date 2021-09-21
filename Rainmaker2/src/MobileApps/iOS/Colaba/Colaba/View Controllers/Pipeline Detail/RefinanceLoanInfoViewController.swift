//
//  RefinanceLoanInfoViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 30/08/2021.
//

import UIKit
import Material
import DropDown

class RefinanceLoanInfoViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldLoanStage: ColabaTextField!
    @IBOutlet weak var txtfieldAdditionalCashoutAmount: ColabaTextField!
    @IBOutlet weak var txtfieldLoanAmount: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
    }
    
    func setTextFields() {
        ///Loan Stage Text Field
        txtfieldLoanStage.setTextField(placeholder: "Loan Stage")
        txtfieldLoanStage.setDelegates(controller: self)
        txtfieldLoanStage.setValidation(validationType: .required)
        txtfieldLoanStage.type = .dropdown
        txtfieldLoanStage.setDropDownDataSource(kLoanStageArray)
        
        ///Additional Cash Out Text Field
        txtfieldAdditionalCashoutAmount.setTextField(placeholder: "Additional Cash Out Amount")
        txtfieldAdditionalCashoutAmount.setDelegates(controller: self)
        txtfieldAdditionalCashoutAmount.setValidation(validationType: .noValidation)
        txtfieldAdditionalCashoutAmount.type = .amount
        
        ///Loan Amount Text Field
        txtfieldLoanAmount.setTextField(placeholder: "Loan Amount")
        txtfieldLoanAmount.setDelegates(controller: self)
        txtfieldLoanAmount.setValidation(validationType: .required)
        txtfieldLoanAmount.type = .amount
        

        
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
            if (txtfieldLoanStage.text != "" && txtfieldLoanAmount.text != ""){
                self.goBack()
            }
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldLoanStage.validate()
        isValidate = txtfieldLoanAmount.validate()
        return isValidate
    }
}

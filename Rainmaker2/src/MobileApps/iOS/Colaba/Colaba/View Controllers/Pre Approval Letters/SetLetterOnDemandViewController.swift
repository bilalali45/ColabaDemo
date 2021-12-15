//
//  SetLetterOnDemandViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 15/12/2021.
//

import UIKit

class SetLetterOnDemandViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var loanView: UIView!
    @IBOutlet weak var lblMessage: UILabel!
    @IBOutlet weak var txtfieldLoanAmount: ColabaTextField!
    @IBOutlet weak var txtfieldDownPayment: ColabaTextField!
    @IBOutlet weak var txtfieldExpirationDate: ColabaTextField!
    @IBOutlet weak var purchaseView: UIView!
    @IBOutlet weak var purchaseViewHeightConstraint: NSLayoutConstraint! // 260 or 150
    @IBOutlet weak var lblPurchasePrice: UILabel!
    @IBOutlet weak var btnNext: ColabaButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
    }
    
    //MARK:- Methods and Actions
    
    func setupViews(){
        ///TextField Loan Amount.
        txtfieldLoanAmount.setTextField(placeholder: "Max Loan Amount", controller: self, validationType: .required, keyboardType: .numberPad)
        txtfieldLoanAmount.type = .amount
        
        ///TextField Down Payment
        txtfieldDownPayment.setTextField(placeholder: "Max Down Payment", controller: self, validationType: .required, keyboardType: .numberPad)
        txtfieldDownPayment.type = .amount
        
        ///TextField Expiration Date
        txtfieldExpirationDate.setTextField(placeholder: "Max Expiration Date", controller: self, validationType: .required)
        txtfieldExpirationDate.type = .datePicker
        
        btnNext.setButton(image: UIImage(named: "NextIcon")!)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    @IBAction func btnNextTapped(_ sender: UIButton){
        
    }

}

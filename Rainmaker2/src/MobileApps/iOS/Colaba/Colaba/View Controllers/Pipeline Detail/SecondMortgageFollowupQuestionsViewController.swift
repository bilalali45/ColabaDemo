//
//  SecondMortgageFollowupQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 03/09/2021.
//

import UIKit
import Material

class SecondMortgageFollowupQuestionsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldMortgagePayment: ColabaTextField!
    @IBOutlet weak var txtfieldMortgageBalance: ColabaTextField!
    @IBOutlet weak var homeEquityStackView: UIStackView!
    @IBOutlet weak var switchHomeEquity: UISwitch!
    @IBOutlet weak var lblHomeEquity: UILabel!
    @IBOutlet weak var txtfieldCreditLimit: ColabaTextField!
    @IBOutlet weak var mortgageCombinedView: UIView!
    @IBOutlet weak var mortgageCombinedViewTopConstraint: NSLayoutConstraint! //140 or 50
    @IBOutlet weak var mortgageCombinedViewHeightConstarint: NSLayoutConstraint!
    @IBOutlet weak var lblMortgageCombinedQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var propertyPurchaseView: UIView!
    @IBOutlet weak var propertyPurchaseViewTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblPropertyPurchaseQuestion: UILabel!
    @IBOutlet weak var yesStackView2: UIStackView!
    @IBOutlet weak var btnYes2: UIButton!
    @IBOutlet weak var lblYes2: UILabel!
    @IBOutlet weak var noStackView2: UIStackView!
    @IBOutlet weak var btnNo2: UIButton!
    @IBOutlet weak var lblNo2: UILabel!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isMortgageCombine: Int?
    var isMortgageTakenWhenPropertyPurchase: Int?
    
    var isForRealEstate = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews()
        if (isForRealEstate){
            lblUsername.text = "5919 TRUSSVILLE CROSSINGS PKWY"
            mortgageCombinedView.isHidden = true
            mortgageCombinedViewHeightConstarint.constant = 0
            propertyPurchaseViewTopConstraint.constant = 0
        }
        changeMortgageCombineStatus()
        changeMortgageTakenWhenPropertyPurchaseStatus()
    }
    
    //MARK:- Methods and Actions
    
    func setMaterialTextFieldsAndViews(){
        
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        
        yesStackView2.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackView2Tapped)))
        noStackView2.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackView2Tapped)))
        

        
        setTextFields()
    }
    
    func setTextFields() {
        ///Second Mortgage Payment Text Field
        txtfieldMortgagePayment.setTextField(placeholder: "Second Mortgage Payment")
        txtfieldMortgagePayment.setDelegates(controller: self)
        txtfieldMortgagePayment.setValidation(validationType: .noValidation)
        txtfieldMortgagePayment.type = .amount
        
        ///Unpaid Second Mortgage Balance Text Field
        txtfieldMortgageBalance.setTextField(placeholder: "Unpaid Second Mortgage Balance")
        txtfieldMortgageBalance.setDelegates(controller: self)
        txtfieldMortgageBalance.setValidation(validationType: .noValidation)
        txtfieldMortgageBalance.type = .amount
        
        ///Credit Limit Text Field
        txtfieldCreditLimit.setTextField(placeholder: "Credit Limit")
        txtfieldCreditLimit.setDelegates(controller: self)
        txtfieldCreditLimit.setValidation(validationType: .noValidation)
        txtfieldCreditLimit.type = .amount
    }
    
    @objc func yesStackViewTapped(){
        isMortgageCombine = 1
        changeMortgageCombineStatus()
    }
    
    @objc func noStackViewTapped(){
        isMortgageCombine = 0
        changeMortgageCombineStatus()
    }
    
    func changeMortgageCombineStatus(){
        btnYes.setImage(UIImage(named: isMortgageCombine == 1 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isMortgageCombine == 1 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo.setImage(UIImage(named: isMortgageCombine == 0 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = isMortgageCombine == 0 ?  Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    @objc func yesStackView2Tapped(){
        isMortgageTakenWhenPropertyPurchase = 1
        changeMortgageTakenWhenPropertyPurchaseStatus()
    }
    
    @objc func noStackView2Tapped(){
        isMortgageTakenWhenPropertyPurchase = 0
        changeMortgageTakenWhenPropertyPurchaseStatus()
    }
    
    func changeMortgageTakenWhenPropertyPurchaseStatus(){
        btnYes2.setImage(UIImage(named: isMortgageTakenWhenPropertyPurchase == 1 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes2.font = isMortgageTakenWhenPropertyPurchase == 1 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo2.setImage(UIImage(named: isMortgageTakenWhenPropertyPurchase == 0 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo2.font = isMortgageTakenWhenPropertyPurchase == 0 ?  Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func switchHomeEquityChanged(_ sender: UISwitch) {
        lblHomeEquity.font = sender.isOn ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        txtfieldCreditLimit.text = ""
        txtfieldCreditLimit.isHidden = !sender.isOn
        mortgageCombinedViewTopConstraint.constant = sender.isOn ? 140 : 50
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.dismissVC()
    }
}

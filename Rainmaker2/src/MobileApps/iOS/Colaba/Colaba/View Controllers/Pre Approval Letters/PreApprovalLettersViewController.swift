//
//  PreApprovalLettersViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/12/2021.
//

import UIKit

class PreApprovalLettersViewController: BaseViewController {

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
    @IBOutlet weak var letterOnDemandView: UIView!
    @IBOutlet weak var letterOnDemandViewHeightConstraint: NSLayoutConstraint! // 168 or 60
    @IBOutlet weak var switchLetterOnDemand: UISwitch!
    @IBOutlet weak var lblLetterOnDemand: UILabel!
    @IBOutlet weak var letterOnDemandSeparatorView: UIView!
    @IBOutlet weak var letterOnDemandSeparatorViewTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var letterOnDemandLoanView: UIView!
    @IBOutlet weak var lblMaxLoanAmount: UILabel!
    @IBOutlet weak var lblLoanToValue: UILabel!
    @IBOutlet weak var lblChangeParameters: UILabel!
    @IBOutlet weak var iconRight: UIImageView!
    @IBOutlet weak var btnChanageParameters: UIButton!
    @IBOutlet weak var purchaseViewSeparator: UIView!
    @IBOutlet weak var purchaseSeparatorTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var optionalElementsView: UIView!
    @IBOutlet weak var loanTypeView: UIView!
    @IBOutlet weak var loanTypeViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var loanTypeStackView: UIStackView!
    @IBOutlet weak var lblLoanTypeHeading: UILabel!
    @IBOutlet weak var btnLoanType: UIButton!
    @IBOutlet weak var loanTypeSeparator: UIView!
    @IBOutlet weak var lblLoanType: UILabel!
    @IBOutlet weak var propertyTypeView: UIView!
    @IBOutlet weak var propertyTypeViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var propertyTypeStackView: UIStackView!
    @IBOutlet weak var lblPropertyTypeHeading: UILabel!
    @IBOutlet weak var btnPropertyType: UIButton!
    @IBOutlet weak var propertyTypeSeparator: UIView!
    @IBOutlet weak var lblPropertyType: UILabel!
    @IBOutlet weak var propertyLocationView: UIView!
    @IBOutlet weak var propertyLocationViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var propertyLocationStackView: UIStackView!
    @IBOutlet weak var lblPropertyLocationHeading: UILabel!
    @IBOutlet weak var btnPropertyLocation: UIButton!
    @IBOutlet weak var propertyLocationSeparator: UIView!
    @IBOutlet weak var stateStackView: UIStackView!
    @IBOutlet weak var lblState: UILabel!
    @IBOutlet weak var countyStackView: UIStackView!
    @IBOutlet weak var lblCounty: UILabel!
    @IBOutlet weak var btnNext: ColabaButton!
    
    var isLoanType = false
    var isPropertyType = false
    var isPropertyLocation = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
    }

    //MARK:- Methods and Actions
    
    func setupViews(){
        ///TextField Loan Amount.
        txtfieldLoanAmount.setTextField(placeholder: "Loan Amount", controller: self, validationType: .required, keyboardType: .numberPad)
        txtfieldLoanAmount.type = .amount
        
        ///TextField Down Payment
        txtfieldDownPayment.setTextField(placeholder: "Down Payment", controller: self, validationType: .required, keyboardType: .numberPad)
        txtfieldDownPayment.type = .amount
        
        ///TextField Expiration Date
        txtfieldExpirationDate.setTextField(placeholder: "Expiration Date", controller: self, validationType: .required)
        txtfieldExpirationDate.type = .datePicker
        
        letterOnDemandView.layer.cornerRadius = 8
        letterOnDemandView.layer.borderWidth = 1
        letterOnDemandView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        loanTypeView.layer.cornerRadius = 8
        loanTypeView.layer.borderWidth = 1
        loanTypeView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        loanTypeStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(loanTypeStackViewTapped)))
        
        propertyTypeView.layer.cornerRadius = 8
        propertyTypeView.layer.borderWidth = 1
        propertyTypeView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        propertyTypeStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(propertyTypeStackViewTapped)))
        
        propertyLocationView.layer.cornerRadius = 8
        propertyLocationView.layer.borderWidth = 1
        propertyLocationView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        propertyLocationStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(propertyLocationStackViewTapped)))
        
        btnNext.setButton(image: UIImage(named: "NextIcon")!)
    }
    
    @objc func loanTypeStackViewTapped(){
        isLoanType = !isLoanType
        loanTypeView.layer.borderColor = isLoanType ? Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor : Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        loanTypeViewHeightConstraint.constant = isLoanType ? 111 : 50
        lblLoanTypeHeading.textColor = isLoanType ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        btnLoanType.setImage(UIImage(named: isLoanType ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        loanTypeSeparator.isHidden = !isLoanType
        lblLoanType.isHidden = !isLoanType
    }
    
    @objc func propertyTypeStackViewTapped(){
        isPropertyType = !isPropertyType
        propertyTypeView.layer.borderColor = isPropertyType ? Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor : Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        propertyTypeViewHeightConstraint.constant = isPropertyType ? 111 : 50
        lblPropertyTypeHeading.textColor = isPropertyType ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        btnPropertyType.setImage(UIImage(named: isPropertyType ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        propertyTypeSeparator.isHidden = !isPropertyType
        lblPropertyType.isHidden = !isPropertyType
    }
    
    @objc func propertyLocationStackViewTapped(){
        isPropertyLocation = !isPropertyLocation
        propertyLocationView.layer.borderColor = isPropertyLocation ? Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor : Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        propertyLocationViewHeightConstraint.constant = isPropertyLocation ? 198 : 50
        lblPropertyLocationHeading.textColor = isPropertyLocation ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        btnPropertyLocation.setImage(UIImage(named: isPropertyLocation ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        propertyLocationSeparator.isHidden = !isPropertyLocation
        stateStackView.isHidden = !isPropertyLocation
        countyStackView.isHidden = !isPropertyLocation
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func letterOnDemandSwitchChanged(_ sender: UISwitch) {
        purchaseViewHeightConstraint.constant = sender.isOn ? 260 : 150
        letterOnDemandViewHeightConstraint.constant = sender.isOn ? 178 : 60
        letterOnDemandView.layer.borderColor = sender.isOn ? Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor : Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        lblLetterOnDemand.textColor = sender.isOn ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        letterOnDemandSeparatorView.isHidden = !sender.isOn
        letterOnDemandSeparatorViewTopConstraint.constant = sender.isOn ? 16 : 30
        letterOnDemandLoanView.isHidden = !sender.isOn
        lblChangeParameters.isHidden = !sender.isOn
        iconRight.isHidden = !sender.isOn
        btnChanageParameters.isHidden = !sender.isOn
        purchaseSeparatorTopConstraint.constant = sender.isOn ? 20 : 10
        self.view.layoutIfNeeded()
    }
    
    @IBAction func btnChangeParamtersTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnNextTapped(_ sender: UIButton){
        
    }
}

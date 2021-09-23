//
//  StartNewApplicationViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 21/09/2021.
//

import UIKit

class StartNewApplicationViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var findBorrowerView: UIView!
    @IBOutlet weak var findBorrowerViewHeightConstraint: NSLayoutConstraint! //50 110 144
    @IBOutlet weak var stackViewFindBorrower: UIStackView!
    @IBOutlet weak var btnFindBorrower: UIButton!
    @IBOutlet weak var lblFindBorrower: UILabel!
    @IBOutlet weak var searchView: UIView!
    @IBOutlet weak var searchIcon: UIImageView!
    @IBOutlet weak var txtfieldSearch: UITextField!
    @IBOutlet weak var borrowerInfoView: UIView!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var lblBorrowerEmailAndPhone: UILabel!
    @IBOutlet weak var createContactView: UIView!
    @IBOutlet weak var createContactViewHeightConstarint: NSLayoutConstraint! //50 or 335
    @IBOutlet weak var stackViewCreateContact: UIStackView!
    @IBOutlet weak var btnCreateContact: UIButton!
    @IBOutlet weak var lblCreateContact: UILabel!
    @IBOutlet weak var txtfieldFirstName: ColabaTextField!
    @IBOutlet weak var txtfieldLastName: ColabaTextField!
    @IBOutlet weak var txtfieldEmail: ColabaTextField!
    @IBOutlet weak var txtfieldPhone: ColabaTextField!
    @IBOutlet weak var loanPurposeView: UIView!
    @IBOutlet weak var stackViewPurchase: UIStackView!
    @IBOutlet weak var btnPurchase: UIButton!
    @IBOutlet weak var lblPurchase: UILabel!
    @IBOutlet weak var stackViewRefinance: UIStackView!
    @IBOutlet weak var btnRefinance: UIButton!
    @IBOutlet weak var lblRefinance: UILabel!
    @IBOutlet weak var loanGoalView: UIView!
    @IBOutlet weak var stackViewLowerPayments: UIStackView!
    @IBOutlet weak var btnLowerPayment: UIButton!
    @IBOutlet weak var lblLowerPayment: UILabel!
    @IBOutlet weak var stackViewCashOut: UIStackView!
    @IBOutlet weak var btnCashOut: UIButton!
    @IBOutlet weak var lblCashOut: UILabel!
    @IBOutlet weak var stackViewDebt: UIStackView!
    @IBOutlet weak var btnDebt: UIButton!
    @IBOutlet weak var lblDebt: UILabel!
    @IBOutlet weak var assignLoanOfficerView: UIView!
    @IBOutlet weak var btnCreateApplication: UIButton!
    
    var isCreateNewContact = false
    var loanPurpose: Int?
    var loanGoal: Int?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViewsAndTextfields()
        stackViewFindBorrower.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(findBorrowerStackViewTapped)))
        stackViewCreateContact.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(createContactStackViewTapped)))
        stackViewPurchase.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(purchaseStackViewTapped)))
        stackViewRefinance.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(refinanceStackViewTapped)))
        stackViewLowerPayments.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(lowerPaymentsStackViewTapped)))
        stackViewCashOut.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(cashOutStackViewTapped)))
        stackViewDebt.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(debtStackViewTapped)))
    }
    
    //MARK:- Methods and Actions
    
    func setupViewsAndTextfields(){
        findBorrowerView.layer.cornerRadius = 8
        findBorrowerView.layer.borderWidth = 1
        findBorrowerView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        findBorrowerView.dropShadowToCollectionViewCell()
        
        searchView.layer.cornerRadius = 5
        searchView.layer.borderWidth = 1
        searchView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        
        borrowerInfoView.layer.cornerRadius = 5
        borrowerInfoView.layer.borderWidth = 1
        borrowerInfoView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        
        let bororwerEmailAndPhone = "richard.glenn@gmail.com   ·    (121) 353 1343"
        let bororwerEmailAndPhoneAttributedText = NSMutableAttributedString(string: bororwerEmailAndPhone)
        let range1 = bororwerEmailAndPhone.range(of: "·")
        bororwerEmailAndPhoneAttributedText.addAttributes([NSAttributedString.Key.font: Theme.getRubikBoldFont(size: 20), NSAttributedString.Key.foregroundColor : Theme.getButtonBlueColor()], range: bororwerEmailAndPhone.nsRange(from: range1!))
        self.lblBorrowerEmailAndPhone.attributedText = bororwerEmailAndPhoneAttributedText
        
        createContactView.layer.cornerRadius = 8
        createContactView.layer.borderWidth = 1
        createContactView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        //createContactView.dropShadowToCollectionViewCell()
        
        txtfieldFirstName.setTextField(placeholder: "First Name")
        txtfieldFirstName.setDelegates(controller: self)
        txtfieldFirstName.setValidation(validationType: .required)
        txtfieldFirstName.setTextField(keyboardType: .asciiCapable)
        txtfieldFirstName.setIsValidateOnEndEditing(validate: true)
        
        txtfieldLastName.setTextField(placeholder: "Last Name")
        txtfieldLastName.setDelegates(controller: self)
        txtfieldLastName.setValidation(validationType: .required)
        txtfieldLastName.setTextField(keyboardType: .asciiCapable)
        txtfieldLastName.setIsValidateOnEndEditing(validate: true)
        
        txtfieldEmail.setTextField(placeholder: "Email Address")
        txtfieldEmail.setDelegates(controller: self)
        txtfieldEmail.setValidation(validationType: .email)
        txtfieldEmail.setTextField(keyboardType: .emailAddress)
        txtfieldEmail.setIsValidateOnEndEditing(validate: true)
        
        txtfieldPhone.setTextField(placeholder: "Mobile Number")
        txtfieldPhone.setDelegates(controller: self)
        txtfieldPhone.setValidation(validationType: .phoneNumber)
        txtfieldPhone.setTextField(keyboardType: .phonePad)
        txtfieldPhone.setIsValidateOnEndEditing(validate: true)
        
        assignLoanOfficerView.layer.cornerRadius = 8
        assignLoanOfficerView.layer.borderWidth = 1
        assignLoanOfficerView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        
        btnCreateApplication.layer.cornerRadius = 5
        btnCreateApplication.isEnabled = false
    }
    
    func setScreenHeight(){
        let findBorrowerHeight = findBorrowerView.frame.height
        let createNewContactHeight = createContactView.frame.height
        self.mainViewHeightConstraint.constant = findBorrowerHeight + createNewContactHeight + 450
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func findBorrowerStackViewTapped(){
        isCreateNewContact = false
        changeBorrowerContactType()
    }
    
    @objc func createContactStackViewTapped(){
        isCreateNewContact = true
        changeBorrowerContactType()
    }
    
    func changeBorrowerContactType(){
        findBorrowerViewHeightConstraint.constant = isCreateNewContact ? 50 : 110
        findBorrowerView.backgroundColor = isCreateNewContact ? .clear : .white
        btnFindBorrower.setImage(UIImage(named: isCreateNewContact ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
        lblFindBorrower.font = isCreateNewContact ? Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
        searchView.isHidden = isCreateNewContact
        createContactViewHeightConstarint.constant = isCreateNewContact ? 335 : 50
        btnCreateContact.setImage(UIImage(named: isCreateNewContact ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblCreateContact.font = isCreateNewContact ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        createContactView.backgroundColor = isCreateNewContact ? .white : .clear
        txtfieldFirstName.isHidden = !isCreateNewContact
        txtfieldLastName.isHidden = !isCreateNewContact
        txtfieldEmail.isHidden = !isCreateNewContact
        txtfieldPhone.isHidden = !isCreateNewContact
        
        if (isCreateNewContact){
            createContactView.dropShadowToCollectionViewCell()
            findBorrowerView.removeShadow()
        }
        else{
            findBorrowerView.dropShadowToCollectionViewCell()
            createContactView.removeShadow()
        }
        
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @objc func purchaseStackViewTapped(){
        loanPurpose = 0
        changeLoanPurpose()
    }
    
    @objc func refinanceStackViewTapped(){
        loanPurpose = 1
        changeLoanPurpose()
    }
    
    func changeLoanPurpose(){
        btnPurchase.setImage(UIImage(named: loanPurpose == 0 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblPurchase.font = loanPurpose == 0 ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnRefinance.setImage(UIImage(named: loanPurpose == 1 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblRefinance.font = loanPurpose == 1 ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnCreateApplication.backgroundColor = Theme.getButtonBlueColor()
        btnCreateApplication.setTitleColor(.white, for: .normal)
        btnCreateApplication.isEnabled = true
    }
    
    @objc func lowerPaymentsStackViewTapped(){
        loanGoal = 0
        changeLoanGoal()
    }
    
    @objc func cashOutStackViewTapped(){
        loanGoal = 1
        changeLoanGoal()
    }
    
    @objc func debtStackViewTapped(){
        loanGoal = 2
        changeLoanGoal()
    }
    
    func changeLoanGoal(){
        btnLowerPayment.setImage(UIImage(named: loanGoal == 0 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblLowerPayment.font = loanGoal == 0 ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnCashOut.setImage(UIImage(named: loanGoal == 1 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblCashOut.font = loanGoal == 1 ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnDebt.setImage(UIImage(named: loanGoal == 2 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblDebt.font = loanGoal == 2 ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnCreateApplication.backgroundColor = Theme.getButtonBlueColor()
        btnCreateApplication.setTitleColor(.white, for: .normal)
        btnCreateApplication.isEnabled = true
    }
    
    func validate() -> Bool{
        if (isCreateNewContact){
            if (!txtfieldFirstName.validate()) {
                return false
            }
            if (!txtfieldLastName.validate()) {
                return false
            }
            if (!txtfieldEmail.validate()) {
                return false
            }
            if (!txtfieldPhone.validate()) {
                return false
            }
            return true
        }
        return true
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnBorrowerInfoCloseTapped(_ sender: UIButton){
        
    }
    
    @IBAction func btnCreateApplicationTapped(_ sender: UIButton) {
        if (isCreateNewContact){
            txtfieldFirstName.validate()
            txtfieldLastName.validate()
            txtfieldEmail.validate()
            txtfieldPhone.validate()
        }
        if (validate()){
            self.dismissVC()
        }
    }
}

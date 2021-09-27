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
    @IBOutlet weak var contactListView: UIView!
    @IBOutlet weak var tableViewContactList: UITableView!
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
    @IBOutlet weak var loanGoalViewHeightConstraint: NSLayoutConstraint!
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
    @IBOutlet weak var loanOfficerView: UIView!
    @IBOutlet weak var loanOfficerImage: UIImageView!
    @IBOutlet weak var lblLoanOfficerName: UILabel!
    @IBOutlet weak var lblLoanOfficerTenant: UILabel!
    
    @IBOutlet weak var btnCreateApplication: UIButton!
    
    var isCreateNewContact = false
    var loanPurpose: Int?
    var loanGoal: Int?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViewsAndTextfields()
        tableViewContactList.register(UINib(nibName: "ContactListTableViewCell", bundle: nil), forCellReuseIdentifier: "ContactListTableViewCell")
        tableViewContactList.rowHeight = 71
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
        txtfieldSearch.addTarget(self, action: #selector(txtfieldSearchTextChanged), for: .editingChanged)
        
        borrowerInfoView.layer.cornerRadius = 5
        borrowerInfoView.layer.borderWidth = 1
        borrowerInfoView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        
        contactListView.roundOnlyBottomCorners(radius: 5)
        contactListView.layer.borderWidth = 1
        contactListView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        
        let bororwerEmailAndPhone = "richard.glenn@gmail.com  ·  (121) 353 1343"
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
        
        txtfieldLastName.setTextField(placeholder: "Last Name")
        txtfieldLastName.setDelegates(controller: self)
        txtfieldLastName.setValidation(validationType: .required)
        txtfieldLastName.setTextField(keyboardType: .asciiCapable)
        
        txtfieldEmail.setTextField(placeholder: "Email Address")
        txtfieldEmail.setDelegates(controller: self)
        txtfieldEmail.setValidation(validationType: .email)
        txtfieldEmail.setTextField(keyboardType: .emailAddress)
        
        txtfieldPhone.setTextField(placeholder: "Mobile Number")
        txtfieldPhone.setDelegates(controller: self)
        txtfieldPhone.setValidation(validationType: .phoneNumber)
        txtfieldPhone.setTextField(keyboardType: .phonePad)
        
        assignLoanOfficerView.layer.cornerRadius = 8
        assignLoanOfficerView.layer.borderWidth = 1
        assignLoanOfficerView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        assignLoanOfficerView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(assignLoanOfficerViewTapped)))
        
        loanOfficerView.layer.cornerRadius = 8
        loanOfficerView.layer.borderWidth = 1
        loanOfficerView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        btnCreateApplication.layer.cornerRadius = 5
        btnCreateApplication.isEnabled = false
    }
    
    func setScreenHeight(){
        let findBorrowerHeight = findBorrowerView.frame.height
        let createNewContactHeight = createContactView.frame.height
        let loanGoalViewHeight = loanGoalView.frame.height
        self.mainViewHeightConstraint.constant = findBorrowerHeight + createNewContactHeight + loanGoalViewHeight + 310
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func findBorrowerStackViewTapped(){
        isCreateNewContact = false
        changeBorrowerContactType()
        searchView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        searchView.layer.cornerRadius = 5
        borrowerInfoView.isHidden = true
        contactListView.isHidden = true
    }
    
    @objc func createContactStackViewTapped(){
        isCreateNewContact = true
        changeBorrowerContactType()
        searchView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        searchView.layer.cornerRadius = 5
        borrowerInfoView.isHidden = true
        contactListView.isHidden = true
    }
    
    @objc func txtfieldSearchTextChanged(){
        searchView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        searchView.roundOnlyTopCorners(radius: 5)
        contactListView.isHidden = false
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
        loanGoalView.isHidden = false
        loanGoalViewHeightConstraint.constant = loanPurpose == 0 ? 100 : 140
        stackViewDebt.isHidden = loanPurpose == 0
        lblLowerPayment.text = loanPurpose == 0 ? "Pre-Approval" : "Lower Payments or Term"
        lblCashOut.text = loanPurpose == 0 ? "Property Under Contract" : "Cash-Out"
        lblDebt.text = "Debt Consolidation"
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
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
    
    @objc func assignLoanOfficerViewTapped(){
        let vc = Utility.getAssignLoanOfficerPopupVC()
        self.present(vc, animated: false, completion: nil)
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
        findBorrowerStackViewTapped()
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

extension StartNewApplicationViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return 6
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = tableView.dequeueReusableCell(withIdentifier: "ContactListTableViewCell", for: indexPath) as! ContactListTableViewCell
        
        let borrowerName = indexPath.row % 2 == 0 ? "Richard Glenn Randall" : "Arnold Richard"
        let attributedBorrowerName = NSMutableAttributedString(string: borrowerName)
        let range = borrowerName.range(of: indexPath.row % 2 == 0 ? "Richard" : "Richard")
        attributedBorrowerName.addAttributes([NSAttributedString.Key.foregroundColor : Theme.getAppBlackColor(), NSAttributedString.Key.font : Theme.getRubikMediumFont(size: 15)], range: borrowerName.nsRange(from: range!))
        cell.lblName.attributedText = attributedBorrowerName
        
        let borrowerEmailAndPhone = indexPath.row % 2 == 0 ? "richard.glenn@gmail.com  ·  (121) 353 1343" : "arnold634@gmail.com  ·  (121) 353 1343"
        let attributedBorrowerEmailAndPhone = NSMutableAttributedString(string: borrowerEmailAndPhone)
        let rangeOfName = borrowerEmailAndPhone.range(of: indexPath.row % 2 == 0 ? "richard" : " ")
        let range2 = borrowerEmailAndPhone.range(of: "·")
        attributedBorrowerEmailAndPhone.addAttributes([NSAttributedString.Key.foregroundColor : Theme.getAppBlackColor(), NSAttributedString.Key.font : Theme.getRubikMediumFont(size: 13)], range: borrowerEmailAndPhone.nsRange(from: rangeOfName!))
        attributedBorrowerEmailAndPhone.addAttributes([NSAttributedString.Key.font: Theme.getRubikBoldFont(size: 20), NSAttributedString.Key.foregroundColor : Theme.getButtonBlueColor()], range: borrowerEmailAndPhone.nsRange(from: range2!))
        cell.lblEmailPhoneNumber.attributedText = attributedBorrowerEmailAndPhone
        
        return cell
        
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        contactListView.isHidden = true
        searchView.isHidden = true
        searchView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        searchView.layer.cornerRadius = 5
        borrowerInfoView.isHidden = false
        findBorrowerViewHeightConstraint.constant = 140
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
}

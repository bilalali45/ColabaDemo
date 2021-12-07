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
    @IBOutlet weak var segmentLoanPurpose: UISegmentedControl!
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
    var borrowerContacts = [BorrowerContactModel]()
    var selectedContactModel = BorrowerContactModel()
    var selectedLoanGoal = 0
    var selectedLoanOfficerId = 0
    var selectedLoanOfficerBranchId = 0
    
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
        NotificationCenter.default.addObserver(self, selector: #selector(seeMoreTapped), name: NSNotification.Name(rawValue: kNotificationLoanOfficerSeeMoreTapped), object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(loanOfficerSelected(notification:)), name: NSNotification.Name(rawValue: kNotificationLoanOfficerSelected), object: nil)
        purchaseStackViewTapped()
    }
    
    //MARK:- Methods and Actions
    
    func setupViewsAndTextfields(){
        findBorrowerView.layer.cornerRadius = 8
        findBorrowerView.layer.borderWidth = 1
        findBorrowerView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        findBorrowerView.dropShadowToCollectionViewCell()
        
        searchView.layer.cornerRadius = 5
        searchView.layer.borderWidth = 1
        searchView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        txtfieldSearch.addTarget(self, action: #selector(txtfieldSearchTextChanged), for: .editingChanged)
        txtfieldSearch.placeholder = "Search by Name, Email, or Phone"
        
        borrowerInfoView.layer.cornerRadius = 5
        borrowerInfoView.layer.borderWidth = 1
        borrowerInfoView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        
        contactListView.roundOnlyBottomCorners(radius: 5)
        contactListView.layer.borderWidth = 1
        contactListView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        
        createContactView.layer.cornerRadius = 8
        createContactView.layer.borderWidth = 1
        createContactView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        //createContactView.dropShadowToCollectionViewCell()
        
        segmentLoanPurpose.setTitleTextAttributes([NSAttributedString.Key.foregroundColor: Theme.getAppGreyColor(), NSAttributedString.Key.font: Theme.getRubikRegularFont(size: 14)], for: .normal)
        segmentLoanPurpose.setTitleTextAttributes([NSAttributedString.Key.foregroundColor: Theme.getButtonBlueColor(), NSAttributedString.Key.font: Theme.getRubikMediumFont(size: 14)], for: .selected)
        
        stackViewLowerPayments.layer.cornerRadius = 5
        stackViewLowerPayments.layer.borderWidth = 1
        stackViewLowerPayments.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        
        stackViewCashOut.layer.cornerRadius = 5
        stackViewCashOut.layer.borderWidth = 1
        stackViewCashOut.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        
        stackViewDebt.layer.cornerRadius = 5
        stackViewDebt.layer.borderWidth = 1
        stackViewDebt.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        
        ///First Name Text Field
        txtfieldFirstName.setTextField(placeholder: "First Name", controller: self, validationType: .required)
        ///Last Name Text Field
        txtfieldLastName.setTextField(placeholder: "Last Name", controller: self, validationType: .required)
        ///Email Address Text Field
        txtfieldEmail.setTextField(placeholder: "Email Address", controller: self, validationType: .email, keyboardType: .emailAddress)
        ///Mobile Number Text Field
        txtfieldPhone.setTextField(placeholder: "Mobile Number", controller: self, validationType: .phoneNumber, keyboardType: .phonePad)
        
        assignLoanOfficerView.layer.cornerRadius = 8
        assignLoanOfficerView.layer.borderWidth = 1
        assignLoanOfficerView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        assignLoanOfficerView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(assignLoanOfficerViewTapped)))
        
        loanOfficerView.layer.cornerRadius = 8
        loanOfficerView.layer.borderWidth = 1
        loanOfficerView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        loanOfficerView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(assignLoanOfficerViewTapped)))
        loanOfficerImage.layer.cornerRadius = loanOfficerImage.frame.height / 2
        
        btnCreateApplication.layer.cornerRadius = 5
        btnCreateApplication.isEnabled = false
    }
    
    func setScreenHeight(){
        let findBorrowerHeight = findBorrowerView.frame.height
        let createNewContactHeight = createContactView.frame.height
        let loanGoalViewHeight = loanGoalView.frame.height
        self.mainViewHeightConstraint.constant = findBorrowerHeight + createNewContactHeight + loanGoalViewHeight + 350
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
        selectedContactModel = BorrowerContactModel()
        changeCreateApplicationButtonState()
    }
    
    @objc func createContactStackViewTapped(){
        isCreateNewContact = true
        changeBorrowerContactType()
        searchView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        searchView.layer.cornerRadius = 5
        borrowerInfoView.isHidden = true
        contactListView.isHidden = true
        selectedContactModel = BorrowerContactModel()
        changeCreateApplicationButtonState()
    }
    
    @objc func txtfieldSearchTextChanged(){
        searchView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        searchView.roundOnlyTopCorners(radius: 5)
        findBorrowerWith(keyword: txtfieldSearch.text!)
    }
    
    func changeBorrowerContactType(){
        findBorrowerViewHeightConstraint.constant = isCreateNewContact ? 50 : 110
 //       findBorrowerView.backgroundColor = isCreateNewContact ? .clear : .white
        findBorrowerView.backgroundColor = .white
        btnFindBorrower.setImage(UIImage(named: isCreateNewContact ? "radioUnslected" : "RadioButtonSelected"), for: .normal)
        lblFindBorrower.font = isCreateNewContact ? Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
        searchView.isHidden = isCreateNewContact
        createContactViewHeightConstarint.constant = isCreateNewContact ? 335 : 50
        btnCreateContact.setImage(UIImage(named: isCreateNewContact ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblCreateContact.font = isCreateNewContact ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
//        createContactView.backgroundColor = isCreateNewContact ? .white : .clear
        createContactView.backgroundColor = .white
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
        loanGoal = nil
        changeLoanPurpose()
        changeCreateApplicationButtonState()
    }
    
    @objc func refinanceStackViewTapped(){
        loanPurpose = 1
        loanGoal = nil
        changeLoanPurpose()
        changeCreateApplicationButtonState()
    }
    
    func changeLoanPurpose(){
        btnPurchase.setImage(UIImage(named: loanPurpose == 0 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblPurchase.font = loanPurpose == 0 ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnRefinance.setImage(UIImage(named: loanPurpose == 1 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblRefinance.font = loanPurpose == 1 ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        loanGoalView.isHidden = false
        loanGoalViewHeightConstraint.constant = loanPurpose == 0 ? 130 : 190
        stackViewDebt.isHidden = loanPurpose == 0
        lblLowerPayment.text = loanPurpose == 0 ? "Pre-Approval" : "Lower Payments or Term"
        lblCashOut.text = loanPurpose == 0 ? "Property Under Contract" : "Cash-Out"
        lblDebt.text = "Debt Consolidation"
        changeLoanGoal()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @objc func lowerPaymentsStackViewTapped(){
        loanGoal = 0
        changeLoanGoal()
        changeCreateApplicationButtonState()
    }
    
    @objc func cashOutStackViewTapped(){
        loanGoal = 1
        changeLoanGoal()
        changeCreateApplicationButtonState()
    }
    
    @objc func debtStackViewTapped(){
        loanGoal = 2
        changeLoanGoal()
        changeCreateApplicationButtonState()
    }
    
    func changeLoanGoal(){
        btnLowerPayment.setImage(UIImage(named: loanGoal == 0 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblLowerPayment.font = loanGoal == 0 ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnCashOut.setImage(UIImage(named: loanGoal == 1 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblCashOut.font = loanGoal == 1 ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnDebt.setImage(UIImage(named: loanGoal == 2 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblDebt.font = loanGoal == 2 ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
    }
    
    @objc func assignLoanOfficerViewTapped(){
        let vc = Utility.getAssignLoanOfficerPopupVC()
        self.present(vc, animated: false, completion: nil)
    }
    
    @objc func seeMoreTapped(){
        let vc = Utility.getAssignLoanOfficerVC()
        self.presentVC(vc: vc)
    }
    
    @objc func loanOfficerSelected(notification: Notification){
        assignLoanOfficerView.isHidden = true
        loanOfficerView.isHidden = false
        changeCreateApplicationButtonState()
        if let mcu = notification.userInfo{
            if let userId = mcu["userId"] as? Int{
                selectedLoanOfficerId = userId
            }
            if let branchId = mcu["branchId"] as? Int{
                selectedLoanOfficerBranchId = branchId
            }
            if let userName = mcu["fullName"] as? String{
                lblLoanOfficerName.text = userName
            }
            if let branchName = mcu["branchName"] as? String{
                lblLoanOfficerTenant.text = branchName
            }
            if let userImage = mcu["profileimageurl"] as? String{
                loanOfficerImage.sd_setImage(with: URL(string: userImage), completed: nil)
            }
        }
//        if (isCreateNewContact || !borrowerInfoView.isHidden){
//            btnCreateApplication.backgroundColor = Theme.getButtonBlueColor()
//            btnCreateApplication.setTitleColor(.white, for: .normal)
//            btnCreateApplication.isEnabled = true
//        }
//        else{
//            btnCreateApplication.backgroundColor = Theme.getButtonGreyColor()
//            btnCreateApplication.setTitleColor(Theme.getButtonGreyTextColor(), for: .normal)
//            btnCreateApplication.isEnabled = false
//        }
    }
    
    func changeCreateApplicationButtonState(){
        
        if (isCreateNewContact){
            if (txtfieldFirstName.text! != "" && txtfieldLastName.text! != "" && txtfieldEmail.text! != "" && loanGoal != nil && !loanOfficerView.isHidden){
                btnCreateApplication.backgroundColor = Theme.getButtonBlueColor()
                btnCreateApplication.setTitleColor(.white, for: .normal)
                btnCreateApplication.isEnabled = true
            }
            else{
                btnCreateApplication.backgroundColor = Theme.getButtonGreyColor()
                btnCreateApplication.setTitleColor(Theme.getButtonGreyTextColor(), for: .normal)
                btnCreateApplication.isEnabled = false
            }
        }
        else{
            if (!borrowerInfoView.isHidden && loanGoal != nil && !loanOfficerView.isHidden){
                btnCreateApplication.backgroundColor = Theme.getButtonBlueColor()
                btnCreateApplication.setTitleColor(.white, for: .normal)
                btnCreateApplication.isEnabled = true
            }
            else{
                btnCreateApplication.backgroundColor = Theme.getButtonGreyColor()
                btnCreateApplication.setTitleColor(Theme.getButtonGreyTextColor(), for: .normal)
                btnCreateApplication.isEnabled = false
            }
        }
    }
    
    @objc func loanApplicationCreated(){
        self.dismissVC()
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
            if (txtfieldPhone.text != ""){
                if (!txtfieldPhone.validate()) {
                    return false
                }
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
        changeCreateApplicationButtonState()
    }
    
    @IBAction func segmentLoanPurposeChanged(_ sender: UISegmentedControl) {
        if (sender.selectedSegmentIndex == 0){
            purchaseStackViewTapped()
        }
        else{
            refinanceStackViewTapped()
        }
    }
    
    @IBAction func btnCreateApplicationTapped(_ sender: UIButton) {
        if (isCreateNewContact){
            txtfieldFirstName.validate()
            txtfieldLastName.validate()
            txtfieldEmail.validate()
            if (txtfieldPhone.text != ""){
                txtfieldPhone.validate()
            }
            if(validate()){
                checkBorrowerAlreadyExist()
            }
        }
        else{
            createNewApplication()
        }
        
    }
    
    //MARK:- API's
    
    func findBorrowerWith(keyword: String){
        
        let extraData = "keyword=\(keyword)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .findBorrowerContact, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                if (status == .success){
                    self.borrowerContacts.removeAll()
                    let contacts = result.arrayValue
                    for contact in contacts{
                        let model = BorrowerContactModel()
                        model.updateModelWithJSON(json: contact)
                        self.borrowerContacts.append(model)
                    }
                    self.contactListView.isHidden = self.borrowerContacts.count == 0
                    self.tableViewContactList.reloadData()
                }
                
                else{
                    self.contactListView.isHidden = true
                }
            }
            
        }
        
    }
    
    func checkBorrowerAlreadyExist(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let phoneNumber = cleanString(string: txtfieldPhone.text!, replaceCharacters: ["(", ")", " ", "-"], replaceWith: "")
        
        let extraData = "email=\(txtfieldEmail.text!)&phone=\(phoneNumber)"
        
        APIRouter.sharedInstance.executeAPI(type: .checkBorrowerAlreadyExist, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = BorrowerContactModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.selectedContactModel = model
                    
                    if (self.selectedContactModel.contactId == 0){
                        self.createNewApplication()
                    }
                    else{
                        let vc = Utility.getDuplicateContactPopupVC()
                        vc.selectedContact = self.selectedContactModel
                        vc.delegate = self
                        self.present(vc, animated: false, completion: nil)
                    }
                }
                
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(2)) { reason in
                        
                    }
                }
            }
        }
    }
    
    func createNewApplication(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        var loanGoalId = 0
        
        if (segmentLoanPurpose.selectedSegmentIndex == 0){
            if (loanGoal == 0){ //Pre Approval
                loanGoalId = 3
            }
            else if (loanGoal == 1){ //Property Under Contract
                 loanGoalId = 4
            }
        }
        else{
            if (loanGoal == 0){ //Lower Payment
                loanGoalId = 5
            }
            else if (loanGoal == 1){ //Cash Out
                 loanGoalId = 6
            }
            else if (loanGoal == 2){ //Debt Conso
                loanGoalId = 7
            }
        }
        
        let params = ["contactId": selectedContactModel.contactId == 0 ? NSNull() : selectedContactModel.contactId,
                      "FirstName": isCreateNewContact ? txtfieldFirstName.text! : selectedContactModel.firstName,
                      "LastName": isCreateNewContact ? txtfieldLastName.text! : selectedContactModel.lastName,
                      "MobileNumber":isCreateNewContact ? cleanString(string: txtfieldPhone.text!, replaceCharacters: ["(", ")", " ", "-"], replaceWith: "") : selectedContactModel.mobileNumber,
                      "EmailAddress": isCreateNewContact ? txtfieldEmail.text! : selectedContactModel.emailAddress,
                      "LoanGoal": loanGoalId,
                      "LoanPurpose": segmentLoanPurpose.selectedSegmentIndex == 0 ? 1 : 2,
                      "branchId": selectedLoanOfficerBranchId,
                      "LoanOfficerUserId": selectedLoanOfficerId] as [String: Any]
        
        print(params)
        
        APIRouter.sharedInstance.executeAPI(type: .createNewApplication, method: .post, params: params) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    self.showPopup(message: "New application has been created", popupState: .success, popupDuration: .custom(2)) { reason in
                        
                        let vc = Utility.getLoanDetailVC()
                        vc.isAfterCreateNewApplication = true
                        vc.loanApplicationId = result["data"]["loanApplicationId"].intValue
                        vc.borrowerName = "\(result["data"]["firstName"].stringValue) \(result["data"]["lastName"].stringValue)"
                        vc.loanPurpose = self.segmentLoanPurpose.selectedSegmentIndex == 0 ? "Purchase" : "Refinance"
                        vc.phoneNumber = result["data"]["mobileNumber"].stringValue
                        vc.email = result["data"]["emailAddress"].stringValue
                        let navVC = UINavigationController(rootViewController: vc)
                        navVC.navigationBar.isHidden = true
                        navVC.modalPresentationStyle = .fullScreen
                        self.presentVC(vc: navVC)
                    }
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(2)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
}

extension StartNewApplicationViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return borrowerContacts.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = tableView.dequeueReusableCell(withIdentifier: "ContactListTableViewCell", for: indexPath) as! ContactListTableViewCell
        
        let contact = borrowerContacts[indexPath.row]
        let borrowerName = "\(contact.firstName) \(contact.lastName)"
        let attributedBorrowerName = NSMutableAttributedString(string: borrowerName)
        let range = borrowerName.range(of: txtfieldSearch.text!, options: .caseInsensitive)
        if (range == nil){
            cell.lblName.text = borrowerName
        }
        else{
            attributedBorrowerName.addAttributes([NSAttributedString.Key.foregroundColor : Theme.getAppBlackColor(), NSAttributedString.Key.font : Theme.getRubikMediumFont(size: 15)], range: borrowerName.nsRange(from: range!))
            cell.lblName.attributedText = attributedBorrowerName
        }
        
        let mobileNumber = formatNumber(with: "(XXX) XXX-XXXX", number: contact.mobileNumber)
        let borrowerEmailAndPhone = "\(contact.emailAddress)  ·  \(mobileNumber)"
        let attributedBorrowerEmailAndPhone = NSMutableAttributedString(string: borrowerEmailAndPhone)
        let rangeOfName = borrowerEmailAndPhone.range(of: txtfieldSearch.text!, options: .caseInsensitive)
        let range2 = borrowerEmailAndPhone.range(of: "·")
        if (rangeOfName == nil || range2 == nil){
            cell.lblEmailPhoneNumber.text = borrowerEmailAndPhone
        }
        else{
            attributedBorrowerEmailAndPhone.addAttributes([NSAttributedString.Key.foregroundColor : Theme.getAppBlackColor(), NSAttributedString.Key.font : Theme.getRubikMediumFont(size: 13)], range: borrowerEmailAndPhone.nsRange(from: rangeOfName!))
            attributedBorrowerEmailAndPhone.addAttributes([NSAttributedString.Key.font: Theme.getRubikBoldFont(size: 20), NSAttributedString.Key.foregroundColor : Theme.getButtonBlueColor()], range: borrowerEmailAndPhone.nsRange(from: range2!))
            cell.lblEmailPhoneNumber.attributedText = attributedBorrowerEmailAndPhone
        }
        
        return cell
        
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        let selectedContact = borrowerContacts[indexPath.row]
        selectedContactModel = selectedContact
        contactListView.isHidden = true
        searchView.isHidden = true
        searchView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        searchView.layer.cornerRadius = 5
        borrowerInfoView.isHidden = false
        findBorrowerViewHeightConstraint.constant = 140
        let mobileNumber = formatNumber(with: "(XXX) XXX-XXXX", number: selectedContact.mobileNumber)
        let bororwerEmailAndPhone = "\(selectedContact.emailAddress)  ·  \(mobileNumber)"
        let bororwerEmailAndPhoneAttributedText = NSMutableAttributedString(string: bororwerEmailAndPhone)
        let range1 = bororwerEmailAndPhone.range(of: "·")
        bororwerEmailAndPhoneAttributedText.addAttributes([NSAttributedString.Key.font: Theme.getRubikBoldFont(size: 20), NSAttributedString.Key.foregroundColor : Theme.getButtonBlueColor()], range: bororwerEmailAndPhone.nsRange(from: range1!))
        self.lblBorrowerName.text = "\(selectedContact.firstName.capitalized) \(selectedContact.lastName.capitalized)"
        self.lblBorrowerEmailAndPhone.attributedText = bororwerEmailAndPhoneAttributedText
        txtfieldSearch.text = ""
        changeCreateApplicationButtonState()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
        
    }
}

extension StartNewApplicationViewController: ColabaTextFieldDelegate{
    func textFieldEndEditing(_ textField: ColabaTextField) {
        changeCreateApplicationButtonState()
    }
}

extension StartNewApplicationViewController: DuplicateContactPopupViewControllerDelegate{
    
    func createLoanApplicationWithExistingContact() {
        createNewApplication()
    }
    
}

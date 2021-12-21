//
//  SendDocumentRequestViewController.swift
//  Colaba
//
//  Created by Salman ~Khan on 04/10/2021.
//

import UIKit
import MaterialComponents

class SendDocumentRequestViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldRequestEmailTemplate: ColabaTextField!
    @IBOutlet weak var tableViewRequestTemplate: UITableView!
    @IBOutlet weak var txtfieldTo: ColabaTextField!
    @IBOutlet weak var txtfieldCC: ColabaTextField!
    @IBOutlet weak var subjectLineContainer: UIView!
    @IBOutlet weak var emailBodyContainer: UIView!
    @IBOutlet weak var textviewBody: UITextView!
    @IBOutlet weak var btnSendRequest: UIButton!
    @IBOutlet weak var collectionViewTo: UICollectionView!
    
    @IBOutlet weak var toChipView: UIView!
    @IBOutlet weak var toAlphabetView: UIView!
    @IBOutlet weak var lblToAlphabet: UILabel!
    @IBOutlet weak var lblToEmail: UILabel!
    @IBOutlet weak var lblToCloseIcon: UIImageView!
    
    
    var loanApplicationId = 0
    var borrowerName = ""
    var selectedDocs = [Doc]()
    var emailTemplatesList = [EmailTemplateModel]()
    var selectedEmailTemplate = EmailTemplateModel()
    var emailBody = ""
    
    var txtViewSubject = MDCFilledTextArea()
    var selectedTemplateIndex = IndexPath(row: 1, section: 0)
//    var chipField: MDCChipField!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFieldsAndTextViews()
        txtfieldRequestEmailTemplate.addTarget(self, action: #selector(textfieldRequestTemplateStartEditing), for: .editingDidBegin)
        //txtfieldTo.isHidden = true
        collectionViewTo.isHidden = true
        getEmailTemplatesList()
        
//        chipFieldTo.textField.placeholderLabel.text = "Hello Everyone"
//        chipFieldTo.delegate = self
//        chipFieldTo.showChipsDeleteButton = true
//        chipFieldTo.sizeToFit()
        
//        collectionViewTo.register(MDCChipCollectionViewCell.self, forCellWithReuseIdentifier: "identifier")
//        collectionViewTo.dataSource = self
//        collectionViewTo.delegate = self
        
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
//        chipField = MDCChipField()
//        chipField.backgroundColor = .clear
//        chipField.textField.placeholderLabel.dividerContentEdgeInsetsPreset = .horizontally5
//        chipField.frame = txtfieldTo.frame
//        chipField.textField.underline?.color = Theme.getSeparatorNormalColor()
//        chipField.delegate = self
//        chipField.textField.placeholderLabel.text = ""
//        chipField.showChipsDeleteButton = true
//        chipField.dividerThickness = 1
//        chipField.dividerColor = Theme.getSeparatorNormalColor()
//        chipField.contentEdgeInsets = UIEdgeInsets(top: -10, left: -5, bottom: 0, right: 0)
//        chipField.chipHeight = 40
//        chipField.heightPreset = .xxlarge
//        //chipField.minTextFieldWidth = self.txtfieldTo.frame.width
//        //chipField.sizeToFit()
//        mainView.addSubview(chipField)
        
        
//        let chipView = MDCChipView()
//        chipView.titleLabel.text = "ali@rainsoftfn.com"
//        chipView.setTitleColor(UIColor.red, for: .selected)
//        chipView.sizeToFit()
//        //chipView.addTarget(self, action: #selector(tap), for: .touchUpInside)
//        self.mainView.addSubview(chipView)
        
    }
   
    //MARK:- Methods and Actions
    
    @objc func chipTap(){
        
    }
    
    func setupTextFieldsAndTextViews(){
        txtfieldRequestEmailTemplate.setTextField(placeholder: "Request Email Template", controller: self, validationType: .required)
        txtfieldTo.setTextField(placeholder: "To", controller: self, validationType: .email, keyboardType: .emailAddress)
        txtfieldCC.setTextField(placeholder: "Cc", controller: self, validationType: .email, keyboardType: .emailAddress)
        
        let estimatedFrame = subjectLineContainer.frame
        txtViewSubject = MDCFilledTextArea(frame: estimatedFrame)
        txtViewSubject.label.text = "Subject Line"
        txtViewSubject.textView.text = ""
        txtViewSubject.leadingAssistiveLabel.text = ""
        txtViewSubject.setFilledBackgroundColor(.clear, for: .normal)
        txtViewSubject.setFilledBackgroundColor(.clear, for: .disabled)
        txtViewSubject.setFilledBackgroundColor(.clear, for: .editing)
        txtViewSubject.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
        txtViewSubject.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .disabled)
        txtViewSubject.setUnderlineColor(Theme.getButtonBlueColor(), for: .editing)
        txtViewSubject.leadingEdgePaddingOverride = 0
        txtViewSubject.setFloatingLabel(Theme.getAppGreyColor(), for: .normal)
        txtViewSubject.setFloatingLabel(Theme.getAppGreyColor(), for: .disabled)
        txtViewSubject.setFloatingLabel(Theme.getAppGreyColor(), for: .editing)
        txtViewSubject.label.font = Theme.getRubikRegularFont(size: 13)
        txtViewSubject.setNormalLabel(Theme.getButtonGreyTextColor(), for: .normal)
        txtViewSubject.setNormalLabel(Theme.getButtonGreyTextColor(), for: .editing)
        txtViewSubject.setNormalLabel(Theme.getButtonGreyTextColor(), for: .disabled)
        txtViewSubject.setTextColor(Theme.getAppBlackColor(), for: .normal)
        txtViewSubject.setTextColor(Theme.getAppBlackColor(), for: .editing)
        txtViewSubject.setTextColor(Theme.getAppBlackColor(), for: .disabled)
        txtViewSubject.textView.font = Theme.getRubikRegularFont(size: 15)
        txtViewSubject.leadingAssistiveLabel.font = Theme.getRubikRegularFont(size: 12)
        txtViewSubject.setLeadingAssistiveLabel(.red, for: .normal)
        txtViewSubject.setLeadingAssistiveLabel(.red, for: .editing)
        txtViewSubject.setLeadingAssistiveLabel(.red, for: .disabled)
        txtViewSubject.textView.textColor = .black
        txtViewSubject.textView.delegate = self
        mainView.addSubview(txtViewSubject)
        
        emailBodyContainer.layer.cornerRadius = 6
        emailBodyContainer.layer.borderWidth = 1
        emailBodyContainer.layer.borderColor = Theme.getButtonGreyColor().cgColor
        textviewBody.delegate = self
        //textviewBody.text = "Hi Richard,\n\n\nAs we discussed, I’m adding addition To continue your application, we need some more information.\n\n· Tax Returns with schedules (Personals - Two Years)\n· Credit Report\n· Earnest Money Deposit\n· Financial Statement\n· Form 1099\n· Government-issued ID\n· Letter of Explanation - General\n· Mortgage Statement\n· Paystubs\n\nComplete these items as soon as possible so we can continue reviewing your application.\n\n\n\nThanks & Regards,\nJohn Doe"
        
        tableViewRequestTemplate.register(UINib(nibName: "EmailTemplateTableViewCell", bundle: nil), forCellReuseIdentifier: "EmailTemplateTableViewCell")
        tableViewRequestTemplate.rowHeight = 80
        tableViewRequestTemplate.layer.cornerRadius = 6
        tableViewRequestTemplate.layer.borderWidth = 1
        tableViewRequestTemplate.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        tableViewRequestTemplate.clipsToBounds = false
        tableViewRequestTemplate.layer.masksToBounds = false
        tableViewRequestTemplate.dropShadowToCollectionViewCell(shadowColor: UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.12).cgColor, shadowRadius: 1, shadowOpacity: 1)
        
        toChipView.layer.cornerRadius = toChipView.frame.height / 2
        toChipView.layer.borderWidth = 1
        toChipView.layer.borderColor = Theme.getChipBorderColor().cgColor
        toChipView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(toChipViewTapped)))
        toAlphabetView.layer.cornerRadius = toAlphabetView.frame.height / 2
        btnSendRequest.layer.cornerRadius = 5
    }
    
    func setScreenHeight(){
        let emailBodyHeight = textviewBody.contentSize.height
        self.mainViewHeightConstraint.constant = emailBodyHeight + 600
        
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func textfieldRequestTemplateStartEditing(){
        self.view.endEditing(true)
        tableViewRequestTemplate.isHidden = false
        subjectLineContainer.isHidden = true
        txtViewSubject.isHidden = true
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldRequestEmailTemplate.validate()
        isValidate = txtfieldTo.validate() && isValidate
        if (txtfieldCC.text != ""){
            isValidate = txtfieldCC.validate() && isValidate
        }
        isValidate = validateSubjectTextView() && isValidate
        isValidate = validateBodyTextView() && isValidate
        return isValidate
    }
    
    func validateSubjectTextView() -> Bool {
        do{
            let response = try txtViewSubject.textView.text.validate(type: .required)
            DispatchQueue.main.async {
                self.txtViewSubject.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewSubject.leadingAssistiveLabel.text = ""
            }
            return response
        }
        catch{
            self.txtViewSubject.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewSubject.leadingAssistiveLabel.text = error.localizedDescription
            return false
        }
    }
    
    func validateBodyTextView() -> Bool{
        return textviewBody.text != ""
    }
    
    func setEmailBody(){
        
        txtfieldTo.setTextField(text: selectedEmailTemplate.toAddress)
        setToChipField()
        
        txtfieldCC.setTextField(text: selectedEmailTemplate.ccAddress)
        
        txtViewSubject.textView.text = selectedEmailTemplate.subject
        txtViewSubject.sizeToFit()
        
        let documentsList = selectedDocs.map({$0.docType})
        emailBody = selectedEmailTemplate.emailBody
        
        var bulletList = ""
        for document in documentsList{
            bulletList = bulletList + "<li>\(document)</li>"
        }
        
        emailBody = emailBody.replacingOccurrences(of: "###RequestDocumentList###", with: "<ul style=\"text-align:left;\"><span style=\"color: rgb(78,78,78);font-size: 14px;font-family: Rubik, sans-serif;\">\(bulletList)</ul>").replacingOccurrences(of: "background-color: rgb(255,255,255);", with: "")
        textviewBody.attributedText = emailBody.htmlToAttributedString
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
        
    }
    
    func setToChipField(){
        if (txtfieldTo.validate()){
            toChipView.isUserInteractionEnabled = true
            toChipView.isHidden = false
            toAlphabetView.isHidden = false
            lblToAlphabet.isHidden = false
            lblToEmail.isHidden = false
            lblToCloseIcon.isHidden = false
            let index = txtfieldTo.text!.index(txtfieldTo.text!.startIndex, offsetBy: 1)
            lblToAlphabet.text = String(txtfieldTo.text!.prefix(upTo: index))
            lblToEmail.text = txtfieldTo.text!
        }
        else{
            toChipView.isUserInteractionEnabled = false
            toChipView.isHidden = true
            toAlphabetView.isHidden = true
            lblToAlphabet.isHidden = true
            lblToEmail.isHidden = true
            lblToCloseIcon.isHidden = true
            lblToAlphabet.text = ""
            lblToEmail.text = ""
        }
    }
    
    @objc func toChipViewTapped(){
        toChipView.isUserInteractionEnabled = false
        toChipView.isHidden = true
        toAlphabetView.isHidden = true
        lblToAlphabet.isHidden = true
        lblToEmail.isHidden = true
        lblToCloseIcon.isHidden = true
        txtfieldTo.setTextField(text: "")
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnSendRequestTapped(_ sender: UIButton){
        if validate(){
            self.saveAndSendRequest()
        }
    }
    
    //MARK:- API's
    
    func getEmailTemplatesList(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getEmailTemplateList, method: .get) { status, result, message in
            DispatchQueue.main.async {
                
                if (status == .success){
                    
                    let emailTemplates = result.arrayValue
                    for template in emailTemplates{
                        let model = EmailTemplateModel()
                        model.updateModelWithJSON(json: template)
                        self.emailTemplatesList.append(model)
                    }
                    self.tableViewRequestTemplate.reloadData()
                    if (self.emailTemplatesList.count > 0){
                        self.getEmailBodyFromTemplate(templateId: self.emailTemplatesList.first!.id)
                        self.txtfieldRequestEmailTemplate.setTextField(text: self.emailTemplatesList.first!.templateName)
                    }
                    
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
        }
        
    }
    
    func getEmailBodyFromTemplate(templateId: String){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanapplicationid=\(loanApplicationId)&Id=\(templateId)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getRenderEmailTemplate, method: .get, params: nil, extraData: extraData) { status, result, message in
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    
                    let model = EmailTemplateModel()
                    model.updateModelWithJSON(json: result)
                    self.selectedEmailTemplate = model
                    self.setEmailBody()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
        }
    }
    
    func saveAndSendRequest(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        var requests = [[String: Any]]()
        
        var requestDic = [String: Any]()
        
        let emailTemplate = ["emailTemplateId": selectedEmailTemplate.id,
                             "toAddress": txtfieldTo.text!,
                             "fromAddress": selectedEmailTemplate.fromAddress,
                             "ccAddress": txtfieldCC.text!,
                             "subject": txtViewSubject.textView.text!,
                             "emailBody": emailBody] as [String: Any]
        requestDic["email"] = emailTemplate
        
        var documents = [[String: Any]]()
        
        for document in selectedDocs{
            let doc = [
                "docTypeId":document.docTypeId,
                "docType": document.docType,
                "docMessage": document.docMessage]
            documents.append(doc)
        }
        
        requestDic["documents"] = documents
            
        requests.append(requestDic)
        
        let params = ["loanApplicationId": loanApplicationId,
                      "requests": requests] as [String: Any]
        
        APIRouter.sharedInstance.executeAPI(type: .saveAndSendEmailRequest, method: .post, params: params) { status, result, message in
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let vc = Utility.getDocumentRequestSentVC()
                    vc.borrowerName = self.borrowerName
                    self.pushToVC(vc: vc)
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
        }
        
    }
    
}

extension SendDocumentRequestViewController: ColabaTextFieldDelegate{

    func textFieldDidChange(_ textField: ColabaTextField) {
        if (textField == txtfieldTo){
            toChipView.isUserInteractionEnabled = false
            toChipView.isHidden = true
            toAlphabetView.isHidden = true
            lblToAlphabet.isHidden = true
            lblToEmail.isHidden = true
            lblToCloseIcon.isHidden = true
            lblToAlphabet.text = ""
            lblToEmail.text = ""
        }
    }
    
    func textFieldEndEditing(_ textField: ColabaTextField) {
        if (textField == txtfieldTo){
            setToChipField()
        }
    }
}

extension SendDocumentRequestViewController: UITextViewDelegate{
    
    func textViewDidEndEditing(_ textView: UITextView) {
        
        if (textView == txtViewSubject.textView){
            validateSubjectTextView()
        }
        
        if (textView == textviewBody){
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.3) {
                self.setScreenHeight()
            }
        }
    }
    
}

extension SendDocumentRequestViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return emailTemplatesList.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "EmailTemplateTableViewCell", for: indexPath) as! EmailTemplateTableViewCell
        let template = emailTemplatesList[indexPath.row]
        cell.mainView.backgroundColor = self.txtfieldRequestEmailTemplate.text == template.templateName ? Theme.getButtonBlueColor().withAlphaComponent(0.1) : .clear
        cell.lblTitle.font = self.txtfieldRequestEmailTemplate.text == template.templateName ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        cell.lblTitle.text = template.templateName
        cell.lblDetail.text = template.templateDescription
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        let selectedTemplate = emailTemplatesList[indexPath.row]
        txtfieldRequestEmailTemplate.setTextField(text: selectedTemplate.templateName)
        self.tableViewRequestTemplate.reloadData()
        tableViewRequestTemplate.isHidden = true
        subjectLineContainer.isHidden = false
        txtViewSubject.isHidden = false
        self.getEmailBodyFromTemplate(templateId: selectedTemplate.id)
    }
    
}

//NSNotification.Name(rawValue: kNotificationLoanApplicationCreated)

//extension SendDocumentRequestViewController: UICollectionViewDataSource, UICollectionViewDelegate{
//
//    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
//        return 3
//    }
//
//    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
//
//        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "identifier", for: indexPath) as! MDCChipCollectionViewCell
//        let chipView = cell.chipView
//        chipView.titleLabel.text = "ali@rainsoftfn.com"
//        chipView.setTitleColor(UIColor.red, for: .selected)
//        chipView.imageView.image = Utility.getChipImage(name: "a")
//        chipView.sizeToFit()
//        return cell
//    }
//
//}

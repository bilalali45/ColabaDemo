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
    
    var txtViewSubject = MDCFilledTextArea()
    var selectedTemplateIndex = IndexPath(row: 1, section: 0)
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFieldsAndTextViews()
        txtfieldRequestEmailTemplate.addTarget(self, action: #selector(textfieldRequestTemplateStartEditing), for: .editingDidBegin)
        txtfieldRequestEmailTemplate.text = "Default Document Request"
        txtfieldTo.text = "richard.glenn@gmail.com"
        txtfieldCC.text = "ali@rainsoftfn.com"
        
    }
   
    //MARK:- Methods and Actions
    
    func setupTextFieldsAndTextViews(){
        txtfieldRequestEmailTemplate.setTextField(placeholder: "Request Email Template", controller: self, validationType: .required)
        txtfieldTo.setTextField(placeholder: "To", controller: self, validationType: .required, keyboardType: .emailAddress)
        txtfieldCC.setTextField(placeholder: "Cc", controller: self, validationType: .required, keyboardType: .emailAddress)
        
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
        txtViewSubject.textView.text = "You have new tasks to complete, for your AHC loan application"
        mainView.addSubview(txtViewSubject)
        
        emailBodyContainer.layer.cornerRadius = 6
        emailBodyContainer.layer.borderWidth = 1
        emailBodyContainer.layer.borderColor = Theme.getButtonGreyColor().cgColor
        textviewBody.delegate = self
        textviewBody.text = "Hi Richard,\n\n\nAs we discussed, I’m adding addition To continue your application, we need some more information.\n\n· Tax Returns with schedules (Personals - Two Years)\n· Credit Report\n· Earnest Money Deposit\n· Financial Statement\n· Form 1099\n· Government-issued ID\n· Letter of Explanation - General\n· Mortgage Statement\n· Paystubs\n\nComplete these items as soon as possible so we can continue reviewing your application.\n\n\n\nThanks & Regards,\nJohn Doe"
        
        tableViewRequestTemplate.register(UINib(nibName: "EmailTemplateTableViewCell", bundle: nil), forCellReuseIdentifier: "EmailTemplateTableViewCell")
        tableViewRequestTemplate.rowHeight = 80
        tableViewRequestTemplate.layer.cornerRadius = 6
        tableViewRequestTemplate.layer.borderWidth = 1
        tableViewRequestTemplate.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        tableViewRequestTemplate.clipsToBounds = false
        tableViewRequestTemplate.layer.masksToBounds = false
        tableViewRequestTemplate.dropShadowToCollectionViewCell(shadowColor: UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.12).cgColor, shadowRadius: 1, shadowOpacity: 1)
        
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
        isValidate = txtfieldCC.validate() && isValidate
        isValidate = validateSubjectTextView() && isValidate
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
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnSendRequestTapped(_ sender: UIButton){
        if validate(){
            let vc = Utility.getDocumentRequestSentVC()
            self.pushToVC(vc: vc)
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
        return kRequestPaymentTemplates.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "EmailTemplateTableViewCell", for: indexPath) as! EmailTemplateTableViewCell
        cell.mainView.backgroundColor = selectedTemplateIndex == indexPath ? Theme.getButtonBlueColor().withAlphaComponent(0.1) : .clear
        cell.lblTitle.font = selectedTemplateIndex == indexPath ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        cell.lblTitle.text = kRequestPaymentTemplates[indexPath.row].title
        cell.lblDetail.text = kRequestPaymentTemplates[indexPath.row].description
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        selectedTemplateIndex = indexPath
        txtfieldRequestEmailTemplate.text = kRequestPaymentTemplates[indexPath.row].title
        self.tableViewRequestTemplate.reloadData()
        tableViewRequestTemplate.isHidden = true
        subjectLineContainer.isHidden = false
        txtViewSubject.isHidden = false

    }
    
}

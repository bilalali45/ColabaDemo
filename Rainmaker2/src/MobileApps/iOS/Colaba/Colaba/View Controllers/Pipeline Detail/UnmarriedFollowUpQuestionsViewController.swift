//
//  UnmarriedFollowUpQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/08/2021.
//

import UIKit
import Material
import DropDown
import MaterialComponents

class UnmarriedFollowUpQuestionsViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var txtfieldTypeOfRelation: TextField!
    @IBOutlet weak var relationshipTypeDropDownAnchorView: UIView!
    @IBOutlet weak var btnTypeOfRelationDropDown: UIButton!
    @IBOutlet weak var txtfieldState: TextField!
    @IBOutlet weak var stateDropDownAnchorView: UIView!
    @IBOutlet weak var btnStateDropDown: UIButton!
    @IBOutlet weak var relationshipDetailTextViewContainer: UIView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    var isNonLegalSpouse = 2 // 1 for yes 2 for no
    let relationshipTypeDropDown = DropDown()
    let stateDropDown = DropDown()
    var txtViewRelationshipDetail = MDCFilledTextArea()
    private let validation: Validation
    
    init(validation: Validation) {
        self.validation = validation
        super.init(nibName: nil, bundle: nil)
    }
    
    required init?(coder: NSCoder) {
        self.validation = Validation()
        super.init(coder: coder)
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldTypeOfRelation, txtfieldState])
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        txtfieldState.addTarget(self, action: #selector(txtfieldStateTextChanged), for: .editingChanged)
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        
    }

    //MARK:- Methods and Actions
    
    func setMaterialTextFieldsAndViews(textfields: [TextField]){
        for textfield in textfields{
            textfield.dividerActiveColor = Theme.getButtonBlueColor()
            textfield.dividerColor = Theme.getSeparatorNormalColor()
            textfield.placeholderActiveColor = Theme.getAppGreyColor()
            textfield.delegate = self
            textfield.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
            textfield.detailLabel.font = Theme.getRubikRegularFont(size: 12)
            textfield.detailColor = .red
            textfield.detailVerticalOffset = 4
            textfield.placeholderVerticalOffset = 8
            textfield.textColor = Theme.getAppBlackColor()
        }
        
        let estimatedFrame = relationshipDetailTextViewContainer.frame
        txtViewRelationshipDetail = MDCFilledTextArea(frame: estimatedFrame)
        txtViewRelationshipDetail.isHidden = true
        txtViewRelationshipDetail.label.text = "Relationship Details"
        txtViewRelationshipDetail.textView.text = ""
        txtViewRelationshipDetail.leadingAssistiveLabel.text = ""
        txtViewRelationshipDetail.setFilledBackgroundColor(.clear, for: .normal)
        txtViewRelationshipDetail.setFilledBackgroundColor(.clear, for: .disabled)
        txtViewRelationshipDetail.setFilledBackgroundColor(.clear, for: .editing)
        txtViewRelationshipDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
        txtViewRelationshipDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .disabled)
        txtViewRelationshipDetail.setUnderlineColor(Theme.getButtonBlueColor(), for: .editing)
        txtViewRelationshipDetail.leadingEdgePaddingOverride = 0
        txtViewRelationshipDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .normal)
        txtViewRelationshipDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .disabled)
        txtViewRelationshipDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .editing)
        txtViewRelationshipDetail.label.font = Theme.getRubikRegularFont(size: 13)
        txtViewRelationshipDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .normal)
        txtViewRelationshipDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .editing)
        txtViewRelationshipDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .disabled)
        txtViewRelationshipDetail.setTextColor(Theme.getAppBlackColor(), for: .normal)
        txtViewRelationshipDetail.setTextColor(Theme.getAppBlackColor(), for: .editing)
        txtViewRelationshipDetail.setTextColor(Theme.getAppBlackColor(), for: .disabled)
        txtViewRelationshipDetail.textView.font = Theme.getRubikRegularFont(size: 15)
        txtViewRelationshipDetail.leadingAssistiveLabel.font = Theme.getRubikRegularFont(size: 12)
        txtViewRelationshipDetail.setLeadingAssistiveLabel(.red, for: .normal)
        txtViewRelationshipDetail.setLeadingAssistiveLabel(.red, for: .editing)
        txtViewRelationshipDetail.setLeadingAssistiveLabel(.red, for: .disabled)
        txtViewRelationshipDetail.textView.textColor = .black
        txtViewRelationshipDetail.textView.delegate = self
        mainView.addSubview(txtViewRelationshipDetail)
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
        relationshipTypeDropDown.dismissMode = .onTap
        relationshipTypeDropDown.anchorView = relationshipTypeDropDownAnchorView
        relationshipTypeDropDown.dataSource = kRelationshipTypeArray
        relationshipTypeDropDown.cancelAction = .some({
            self.btnTypeOfRelationDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
        })
        relationshipTypeDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnTypeOfRelationDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldTypeOfRelation.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldTypeOfRelation.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldTypeOfRelation.text = item
            txtfieldTypeOfRelation.detail = ""
            relationshipTypeDropDown.hide()
            relationshipDetailTextViewContainer.isHidden = item != "Other"
            txtViewRelationshipDetail.isHidden = item != "Other"

        }
        
        stateDropDown.dismissMode = .onTap
        stateDropDown.anchorView = stateDropDownAnchorView
        stateDropDown.direction = .any
        stateDropDown.dataSource = kUSAStatesArray
        stateDropDown.cancelAction = .some({
            self.btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
        })
        stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldState.text = item
            txtfieldState.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldState.detail = ""
            stateDropDown.hide()
        }
    }
    
    func setPlaceholderLabelColorAfterTextFilled(selectedTextField: UITextField, allTextFields: [TextField]){
        for allTextField in allTextFields{
            if (allTextField == selectedTextField){
                if (allTextField.text == ""){
                    allTextField.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                }
                else{
                    allTextField.placeholderLabel.textColor = Theme.getAppGreyColor()
                }
            }
        }
    }
    
    @objc func yesStackViewTapped(){
        isNonLegalSpouse = 1
        changeNonLegalSpouseStatus()
    }
    
    @objc func noStackViewTapped(){
        isNonLegalSpouse = 2
        changeNonLegalSpouseStatus()
    }
    
    func changeNonLegalSpouseStatus(){
        btnYes.setImage(UIImage(named: isNonLegalSpouse == 1 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isNonLegalSpouse == 1 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo.setImage(UIImage(named: isNonLegalSpouse == 2 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = isNonLegalSpouse == 2 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        txtfieldTypeOfRelation.isHidden = isNonLegalSpouse != 1
        btnTypeOfRelationDropDown.isHidden = isNonLegalSpouse != 1
        txtfieldState.isHidden = isNonLegalSpouse != 1
        btnStateDropDown.isHidden = isNonLegalSpouse != 1
        if (isNonLegalSpouse == 1){
//            txtviewRelationshipDetail.isHidden = txtfieldTypeOfRelation.text != "Other"
//            lblRelationshipDetail.isHidden = txtfieldTypeOfRelation.text != "Other"
            relationshipDetailTextViewContainer.isHidden = txtfieldTypeOfRelation.text != "Other"
            txtViewRelationshipDetail.isHidden = txtfieldTypeOfRelation.text != "Other"
        }
        else{
//            txtviewRelationshipDetail.isHidden = true
//            lblRelationshipDetail.isHidden = true
            relationshipDetailTextViewContainer.isHidden = true
            txtViewRelationshipDetail.isHidden = true
        }
    }
    
    @objc func txtfieldStateTextChanged(){
        btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
        if (txtfieldState.text == ""){
            stateDropDown.dataSource = kUSAStatesArray
            stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
                txtfieldState.text = item
                txtfieldState.dividerColor = Theme.getSeparatorNormalColor()
                txtfieldState.detail = ""
                btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        else{
            let filterStates = kUSAStatesArray.filter{$0.contains(txtfieldState.text!)}
            stateDropDown.dataSource = filterStates
            stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
                txtfieldState.text = item
                txtfieldState.dividerColor = Theme.getSeparatorNormalColor()
                txtfieldState.detail = ""
                btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        
        stateDropDown.show()
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnRelationDropDownTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnStateDropDownTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        
        if (isNonLegalSpouse == 1){
            do{
                let typeOfRelationship = try validation.validateTypeOfRelationship(txtfieldTypeOfRelation.text)
                DispatchQueue.main.async {
                    self.txtfieldTypeOfRelation.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldTypeOfRelation.detail = ""
                }
                
            }
            catch{
                self.txtfieldTypeOfRelation.dividerColor = .red
                self.txtfieldTypeOfRelation.detail = error.localizedDescription
            }
            
            do{
                let relationshipState = try validation.validateState(txtfieldState.text)
                DispatchQueue.main.async {
                    self.txtfieldState.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldState.detail = ""
                }
                
            }
            catch{
                self.txtfieldState.dividerColor = .red
                self.txtfieldState.detail = error.localizedDescription
            }
            
            if (txtfieldTypeOfRelation.text == "Other"){
                do{
                    let relationshipDetail = try validation.validateRelationshipDetail(txtViewRelationshipDetail.textView.text)
                    DispatchQueue.main.async {
//                        self.lblRelationshipDetailError.isHidden = true
//                        self.txtviewRelationshipDetail.dividerColor = Theme.getSeparatorNormalColor()
                        self.txtViewRelationshipDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                        self.txtViewRelationshipDetail.leadingAssistiveLabel.text = ""
                    }

                }
                catch{
//                    self.lblRelationshipDetailError.isHidden = false
//                    self.lblRelationshipDetailError.text = error.localizedDescription
//                    self.txtviewRelationshipDetail.dividerColor = Theme.getSeparatorErrorColor()
                    self.txtViewRelationshipDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
                    self.txtViewRelationshipDetail.leadingAssistiveLabel.text = error.localizedDescription
                }
            }

            if (txtfieldTypeOfRelation.text != "" && txtfieldState.text != ""){
                if (txtfieldTypeOfRelation.text == "Other" && txtViewRelationshipDetail.textView.text != ""){
                    self.dismissVC()
                }
                else if (txtfieldTypeOfRelation.text != "Other"){
                    self.dismissVC()
                }
            }
        }
        else{
            self.dismissVC()
        }
    }
}

extension UnmarriedFollowUpQuestionsViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        if (textField == txtfieldTypeOfRelation){
            textField.endEditing(true)
            txtfieldTypeOfRelation.dividerColor = Theme.getButtonBlueColor()
            btnTypeOfRelationDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            relationshipTypeDropDown.show()
        }
        
        if (textField == txtfieldState){
            //textField.endEditing(true)
            //btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            //stateDropDown.show()
        }
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldTypeOfRelation){
            btnTypeOfRelationDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldTypeOfRelation.dividerColor = Theme.getSeparatorNormalColor()
            
            do{
                let typeOfRelationship = try validation.validateTypeOfRelationship(txtfieldTypeOfRelation.text)
                DispatchQueue.main.async {
                    self.txtfieldTypeOfRelation.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldTypeOfRelation.detail = ""
                }
                
            }
            catch{
                self.txtfieldTypeOfRelation.dividerColor = .red
                self.txtfieldTypeOfRelation.detail = error.localizedDescription
            }
            
        }
        
        if (textField == txtfieldState){
            
            if !(kUSAStatesArray.contains(txtfieldState.text!)){
                txtfieldState.text = ""
                txtfieldState.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                stateDropDown.hide()
            }
            
            btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            do{
                let state = try validation.validateState(txtfieldState.text)
                DispatchQueue.main.async {
                    self.txtfieldState.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldState.detail = ""
                }
                
            }
            catch{
                self.txtfieldState.dividerColor = .red
                self.txtfieldState.detail = error.localizedDescription
            }
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldTypeOfRelation, txtfieldState])
    }
    
}

extension UnmarriedFollowUpQuestionsViewController: UITextViewDelegate{
    
//    func textViewDidBeginEditing(_ textView: UITextView) {
//        txtviewRelationshipDetail.dividerThickness = 2
//        txtviewRelationshipDetail.dividerColor = Theme.getButtonBlueColor()
//    }
    
    func textViewDidEndEditing(_ textView: UITextView) {
        //txtviewRelationshipDetail.dividerThickness = 1
        //txtviewRelationshipDetail.dividerColor = Theme.getSeparatorNormalColor()
        
        do{
            let relationshipDetail = try validation.validateRelationshipDetail(txtViewRelationshipDetail.textView.text)
            DispatchQueue.main.async {
                //self.lblRelationshipDetailError.isHidden = true
                //self.txtviewRelationshipDetail.dividerColor = Theme.getSeparatorNormalColor()
                self.txtViewRelationshipDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewRelationshipDetail.leadingAssistiveLabel.text = ""
            }

        }
        catch{
//            self.lblRelationshipDetailError.isHidden = false
//            self.lblRelationshipDetailError.text = error.localizedDescription
           // self.txtviewRelationshipDetail.dividerColor = Theme.getSeparatorErrorColor()
            self.txtViewRelationshipDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewRelationshipDetail.leadingAssistiveLabel.text = error.localizedDescription
        }
        
    }
    
}

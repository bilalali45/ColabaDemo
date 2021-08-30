//
//  NonPermanentResidenceFollowUpQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/08/2021.
//

import UIKit
import Material
import DropDown
import MaterialComponents

class NonPermanentResidenceFollowUpQuestionsViewController: UIViewController {
    
    //MARK:- Outlets and Properties

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var separatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldVisaStatus: TextField!
    @IBOutlet weak var visaStatusDropDownAnchorView: UIView!
    @IBOutlet weak var btnVisaStatusDropDown: UIButton!
    @IBOutlet weak var statusDetailTextViewContainer: UIView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let visaStatusDropDown = DropDown()
    var txtViewStatusDetail = MDCFilledTextArea()
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
        setMaterialTextFieldsAndViews(textfields: [txtfieldVisaStatus])
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        txtfieldVisaStatus.becomeFirstResponder()
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
        txtfieldVisaStatus.textInsets = UIEdgeInsets(top: 0, left: 0, bottom: 0, right: 25)
        
        let estimatedFrame = statusDetailTextViewContainer.frame
        txtViewStatusDetail = MDCFilledTextArea(frame: estimatedFrame)
        txtViewStatusDetail.isHidden = true
        txtViewStatusDetail.label.text = "Status Details"
        txtViewStatusDetail.textView.text = ""
        txtViewStatusDetail.leadingAssistiveLabel.text = ""
        txtViewStatusDetail.setFilledBackgroundColor(.clear, for: .normal)
        txtViewStatusDetail.setFilledBackgroundColor(.clear, for: .disabled)
        txtViewStatusDetail.setFilledBackgroundColor(.clear, for: .editing)
        txtViewStatusDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
        txtViewStatusDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .disabled)
        txtViewStatusDetail.setUnderlineColor(Theme.getButtonBlueColor(), for: .editing)
        txtViewStatusDetail.leadingEdgePaddingOverride = 0
        txtViewStatusDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .normal)
        txtViewStatusDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .disabled)
        txtViewStatusDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .editing)
        txtViewStatusDetail.label.font = Theme.getRubikRegularFont(size: 13)
        txtViewStatusDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .normal)
        txtViewStatusDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .editing)
        txtViewStatusDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .disabled)
        txtViewStatusDetail.setTextColor(Theme.getAppBlackColor(), for: .normal)
        txtViewStatusDetail.setTextColor(Theme.getAppBlackColor(), for: .editing)
        txtViewStatusDetail.setTextColor(Theme.getAppBlackColor(), for: .disabled)
        txtViewStatusDetail.textView.font = Theme.getRubikRegularFont(size: 15)
        txtViewStatusDetail.leadingAssistiveLabel.font = Theme.getRubikRegularFont(size: 12)
        txtViewStatusDetail.setLeadingAssistiveLabel(.red, for: .normal)
        txtViewStatusDetail.setLeadingAssistiveLabel(.red, for: .editing)
        txtViewStatusDetail.setLeadingAssistiveLabel(.red, for: .disabled)
        txtViewStatusDetail.textView.textColor = .black
        txtViewStatusDetail.textView.delegate = self
        mainView.addSubview(txtViewStatusDetail)
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
        visaStatusDropDown.dismissMode = .onTap
        visaStatusDropDown.anchorView = visaStatusDropDownAnchorView
        visaStatusDropDown.dataSource = kVisaStatusArray
        visaStatusDropDown.cancelAction = .some({
            self.btnVisaStatusDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
        })
        visaStatusDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnVisaStatusDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldVisaStatus.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldVisaStatus.text = item
            visaStatusDropDown.hide()
            statusDetailTextViewContainer.isHidden = item != "Other"
            txtViewStatusDetail.isHidden = item != "Other"
            txtfieldVisaStatus.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldVisaStatus.detail = ""
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
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        
        do{
            let visaStatus = try validation.validateVisaStatus(txtfieldVisaStatus.text)
            DispatchQueue.main.async {
                self.txtfieldVisaStatus.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldVisaStatus.detail = ""
            }
            
        }
        catch{
            self.txtfieldVisaStatus.dividerColor = .red
            self.txtfieldVisaStatus.detail = error.localizedDescription
        }
        
        if (txtfieldVisaStatus.text == "Other"){
            do{
                let visaStatusDetail = try validation.validateVisaStatusDetail(txtViewStatusDetail.textView.text)
                DispatchQueue.main.async {
                    self.txtViewStatusDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                    self.txtViewStatusDetail.leadingAssistiveLabel.text = ""
                }

            }
            catch{
                self.txtViewStatusDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
                self.txtViewStatusDetail.leadingAssistiveLabel.text = error.localizedDescription
            }
        }
        
        if (txtfieldVisaStatus.text != ""){
            if (txtfieldVisaStatus.text == "Other" && txtViewStatusDetail.textView.text != ""){
                self.dismissVC()
            }
            else if (txtfieldVisaStatus.text != "Other"){
                self.dismissVC()
            }
        }
        
        
    }
    
}

extension NonPermanentResidenceFollowUpQuestionsViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        
        if (textField == txtfieldVisaStatus){
            textField.endEditing(true)
            btnVisaStatusDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            visaStatusDropDown.show()
            txtfieldVisaStatus.dividerColor = Theme.getButtonBlueColor()
            
            do{
                let visaStatus = try validation.validateVisaStatus(txtfieldVisaStatus.text)
                DispatchQueue.main.async {
                    self.txtfieldVisaStatus.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldVisaStatus.detail = ""
                }
                
            }
            catch{
                self.txtfieldVisaStatus.dividerColor = .red
                self.txtfieldVisaStatus.detail = error.localizedDescription
            }
            
        }
        
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldVisaStatus){
            btnVisaStatusDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldVisaStatus.dividerColor = Theme.getSeparatorNormalColor()
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldVisaStatus])
    }
    
}

extension NonPermanentResidenceFollowUpQuestionsViewController: UITextViewDelegate{
    
//    func textViewDidBeginEditing(_ textView: UITextView) {
//        txtviewStatusDetail.dividerThickness = 2
//        txtviewStatusDetail.dividerColor = Theme.getButtonBlueColor()
//    }
    
    func textViewDidEndEditing(_ textView: UITextView) {
        //txtviewStatusDetail.dividerThickness = 1
        //txtviewStatusDetail.dividerColor = Theme.getSeparatorNormalColor()
        
        do{
            let relationshipDetail = try validation.validateRelationshipDetail(txtViewStatusDetail.textView.text)
            DispatchQueue.main.async {
                self.txtViewStatusDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewStatusDetail.leadingAssistiveLabel.text = ""
            }

        }
        catch{
            self.txtViewStatusDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewStatusDetail.leadingAssistiveLabel.text = error.localizedDescription
        }
        
    }
    
}

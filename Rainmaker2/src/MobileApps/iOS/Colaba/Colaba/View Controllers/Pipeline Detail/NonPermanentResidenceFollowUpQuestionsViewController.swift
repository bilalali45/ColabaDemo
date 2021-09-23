//
//  NonPermanentResidenceFollowUpQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/08/2021.
//

import UIKit
import MaterialComponents

class NonPermanentResidenceFollowUpQuestionsViewController: BaseViewController {
    
    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var separatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldVisaStatus: ColabaTextField!
    @IBOutlet weak var statusDetailTextViewContainer: UIView!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var txtViewStatusDetail = MDCFilledTextArea()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        _ = txtfieldVisaStatus.becomeFirstResponder()
    }
    
    //MARK:- Methods and Actions
    func setMaterialTextFieldsAndViews(){
        
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
        

        
        setTextFields()
    }
    
    func setTextFields() {
        ///Visa Status Text Field
        txtfieldVisaStatus.setTextField(placeholder: "Visa Status")
        txtfieldVisaStatus.setDelegates(controller: self)
        txtfieldVisaStatus.setValidation(validationType: .required)
        txtfieldVisaStatus.type = .dropdown
        txtfieldVisaStatus.setDropDownDataSource(kVisaStatusArray)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
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
    
    func validate() -> Bool {
        var isValidate = txtfieldVisaStatus.validate()
        if (txtfieldVisaStatus.text == "Other") {
            isValidate = validateTextView() && isValidate
        }
        return isValidate
    }
}

extension NonPermanentResidenceFollowUpQuestionsViewController : ColabaTextFieldDelegate {

    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        statusDetailTextViewContainer.isHidden = (option != "Other")
        txtViewStatusDetail.isHidden = (option != "Other")
    }
}

extension NonPermanentResidenceFollowUpQuestionsViewController: UITextViewDelegate{
    
    func textViewDidEndEditing(_ textView: UITextView) {
        _ = validateTextView()
    }
    
    func validateTextView() -> Bool {
        do{
            let response = try txtViewStatusDetail.textView.text.validate(type: .required)
            DispatchQueue.main.async {
                self.txtViewStatusDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewStatusDetail.leadingAssistiveLabel.text = ""
            }
            return response
        }
        catch{
            self.txtViewStatusDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewStatusDetail.leadingAssistiveLabel.text = error.localizedDescription
            return false
        }
    }
}

//
//  NonPermanentResidenceFollowUpQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/08/2021.
//

import UIKit
import Material
import DropDown

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
    @IBOutlet weak var lblStatusDetail: UILabel!
    @IBOutlet weak var txtviewStatusDetail: UITextView!
    @IBOutlet weak var lblStatusDetailError: UILabel!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let visaStatusDropDown = DropDown()
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
        }
        txtfieldVisaStatus.textInsets = UIEdgeInsets(top: 0, left: 0, bottom: 0, right: 25)
//        txtviewStatusDetail.dividerThickness = 1
//        txtviewStatusDetail.isDividerHidden = false
//        txtviewStatusDetail.dividerColor = Theme.getSeparatorNormalColor()
//        txtviewStatusDetail.delegate = self
//        txtviewStatusDetail.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
        txtviewStatusDetail.layer.cornerRadius = 5
        txtviewStatusDetail.layer.borderColor = Theme.getButtonGreyColor().cgColor
        txtviewStatusDetail.layer.borderWidth = 1
        txtviewStatusDetail.delegate = self
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
        visaStatusDropDown.dismissMode = .manual
        visaStatusDropDown.anchorView = visaStatusDropDownAnchorView
        visaStatusDropDown.dataSource = kVisaStatusArray
        visaStatusDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnVisaStatusDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldVisaStatus.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldVisaStatus.text = item
            visaStatusDropDown.hide()
            txtviewStatusDetail.isHidden = item != "Other"
            lblStatusDetail.isHidden = item != "Other"
            lblStatusDetailError.isHidden = item != "Other"
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
                let visaStatusDetail = try validation.validateVisaStatusDetail(txtviewStatusDetail.text)
                DispatchQueue.main.async {
                    self.lblStatusDetailError.isHidden = true
                    self.txtviewStatusDetail.dividerColor = Theme.getSeparatorNormalColor()
                }
                
            }
            catch{
                self.lblStatusDetailError.isHidden = false
                self.lblStatusDetailError.text = error.localizedDescription
                self.txtviewStatusDetail.dividerColor = Theme.getSeparatorErrorColor()
            }
        }
        
        if (txtfieldVisaStatus.text != ""){
            if (txtfieldVisaStatus.text == "Other" && txtviewStatusDetail.text != ""){
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
            let visaStatusDetail = try validation.validateVisaStatusDetail(txtviewStatusDetail.text)
            DispatchQueue.main.async {
                self.lblStatusDetailError.isHidden = true
                //self.txtviewStatusDetail.dividerColor = Theme.getSeparatorNormalColor()
            }
            
        }
        catch{
            self.lblStatusDetailError.isHidden = false
            self.lblStatusDetailError.text = error.localizedDescription
            //self.txtviewStatusDetail.dividerColor = Theme.getSeparatorErrorColor()
        }
        
    }
    
}

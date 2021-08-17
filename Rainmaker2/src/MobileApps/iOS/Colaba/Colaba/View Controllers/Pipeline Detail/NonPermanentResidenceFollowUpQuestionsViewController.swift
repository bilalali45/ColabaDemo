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
    @IBOutlet weak var txtviewStatusDetail: TextView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let visaStatusDropDown = DropDown()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setMaterialTextFieldsAndViews(textfields: [txtfieldVisaStatus])
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
        }
        txtviewStatusDetail.dividerThickness = 1
        txtviewStatusDetail.isDividerHidden = false
        //txtviewRelationshipDetail.dividerActiveColor = Theme.getButtonBlueColor()
        txtviewStatusDetail.dividerColor = Theme.getSeparatorNormalColor()
        //txtviewRelationshipDetail.placeholderActiveColor = Theme.getAppGreyColor()
        //txtviewRelationshipDetail.delegate = self
        txtviewStatusDetail.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
        //txtviewRelationshipDetail.detailLabel.font = Theme.getRubikRegularFont(size: 12)
        //txtviewRelationshipDetail.detailColor = .red
        
        btnSaveChanges.layer.cornerRadius = 5
        btnSaveChanges.dropShadowToCollectionViewCell()
        
        visaStatusDropDown.dismissMode = .manual
        visaStatusDropDown.anchorView = visaStatusDropDownAnchorView
        visaStatusDropDown.dataSource = kVisaStatusArray
        visaStatusDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnVisaStatusDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldVisaStatus.text = item
            visaStatusDropDown.hide()
            txtviewStatusDetail.isHidden = item != "Other"
            txtfieldVisaStatus.dividerColor = Theme.getSeparatorNormalColor()
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
        self.dismissVC()
    }
    
}

extension NonPermanentResidenceFollowUpQuestionsViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        
        if (textField == txtfieldVisaStatus){
            textField.endEditing(true)
            btnVisaStatusDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            visaStatusDropDown.show()
            txtfieldVisaStatus.dividerColor = Theme.getButtonBlueColor()
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

//
//  UnmarriedFollowUpQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/08/2021.
//

import UIKit
import Material
import DropDown

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
    @IBOutlet weak var txtviewRelationshipDetail: TextView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    var isNonLegalSpouse = 0 // 1 for yes 2 for no
    let relationshipTypeDropDown = DropDown()
    let stateDropDown = DropDown()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldTypeOfRelation, txtfieldState])
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        txtfieldState.addTarget(self, action: #selector(txtfieldStateTextChanged), for: .editingChanged)
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
        txtviewRelationshipDetail.dividerThickness = 1
        txtviewRelationshipDetail.isDividerHidden = false
        //txtviewRelationshipDetail.dividerActiveColor = Theme.getButtonBlueColor()
        txtviewRelationshipDetail.dividerColor = Theme.getSeparatorNormalColor()
        //txtviewRelationshipDetail.placeholderActiveColor = Theme.getAppGreyColor()
        //txtviewRelationshipDetail.delegate = self
        txtviewRelationshipDetail.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
        //txtviewRelationshipDetail.detailLabel.font = Theme.getRubikRegularFont(size: 12)
        //txtviewRelationshipDetail.detailColor = .red
        
        btnSaveChanges.layer.cornerRadius = 5
        btnSaveChanges.dropShadowToCollectionViewCell()
        
        relationshipTypeDropDown.dismissMode = .manual
        relationshipTypeDropDown.anchorView = relationshipTypeDropDownAnchorView
        relationshipTypeDropDown.dataSource = kRelationshipTypeArray
        relationshipTypeDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnTypeOfRelationDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldTypeOfRelation.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldTypeOfRelation.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldTypeOfRelation.text = item
            relationshipTypeDropDown.hide()
            txtviewRelationshipDetail.isHidden = item != "Other"
        }
        
        stateDropDown.dismissMode = .manual
        stateDropDown.anchorView = stateDropDownAnchorView
        stateDropDown.direction = .top
        stateDropDown.dataSource = kUSAStatesArray
        
        stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldState.text = item
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
            txtviewRelationshipDetail.isHidden = txtfieldTypeOfRelation.text != "Other"
        }
        else{
            txtviewRelationshipDetail.isHidden = true
        }
    }
    
    @objc func txtfieldStateTextChanged(){
        if (txtfieldState.text == ""){
            stateDropDown.dataSource = kUSAStatesArray
            stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
                txtfieldState.text = item
                btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        else{
            let filterStates = kUSAStatesArray.filter{$0.contains(txtfieldState.text!)}
            stateDropDown.dataSource = filterStates
            stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
                txtfieldState.text = item
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
        self.dismissVC()
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
            btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            stateDropDown.show()
        }
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldTypeOfRelation){
            btnTypeOfRelationDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldTypeOfRelation.dividerColor = Theme.getSeparatorNormalColor()
        }
        
        if (textField == txtfieldState){
            btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldTypeOfRelation, txtfieldState])
    }
    
}

//
//  UnmarriedFollowUpQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/08/2021.
//

import UIKit
import Material

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
    @IBOutlet weak var NoStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var txtfieldTypeOfRelation: TextField!
    @IBOutlet weak var btnTypeOfRelationDropDown: UIButton!
    @IBOutlet weak var txtfieldState: TextField!
    @IBOutlet weak var btnStateDropDown: UIButton!
    @IBOutlet weak var txtviewRelationshipDetail: TextView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldTypeOfRelation, txtfieldState])
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
    
    @IBAction func btnRelationDropDownTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnStateDropDownTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        self.dismissVC()
    }
}

extension UnmarriedFollowUpQuestionsViewController: UITextFieldDelegate{
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldTypeOfRelation, txtfieldState])
    }
    
}

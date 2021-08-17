//
//  ActiveDutyPersonnelViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/08/2021.
//

import UIKit
import Material
import MonthYearPicker

class ActiveDutyPersonnelFollowUpQuestionViewController: UIViewController {

    //MARK:- Outlets and Properties

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var separatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldLastDate: TextField!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let lastDateOfServiceDateFormatter = DateFormatter()
    
    override func viewDidLoad() {
        super.viewDidLoad()

        setMaterialTextFieldsAndViews(textfields: [txtfieldLastDate])
        lastDateOfServiceDateFormatter.dateStyle = .medium
        lastDateOfServiceDateFormatter.dateFormat = "MM/yyyy"
        txtfieldLastDate.addInputViewMonthYearDatePicker(target: self, selector: #selector(dateChanged))
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
        }
        
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
    
    @objc func dateChanged() {
        if let  datePicker = self.txtfieldLastDate.inputView as? MonthYearPickerView {
            self.txtfieldLastDate.text = lastDateOfServiceDateFormatter.string(from: datePicker.date)
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
}

extension ActiveDutyPersonnelFollowUpQuestionViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        dateChanged()
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldLastDate])
    }
    
}

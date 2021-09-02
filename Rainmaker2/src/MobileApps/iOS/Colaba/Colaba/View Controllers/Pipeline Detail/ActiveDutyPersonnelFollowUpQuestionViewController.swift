//
//  ActiveDutyPersonnelViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/08/2021.
//

import UIKit
import Material
import MonthYearPicker

class ActiveDutyPersonnelFollowUpQuestionViewController: BaseViewController {

    //MARK:- Outlets and Properties

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var separatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldLastDate: TextField!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let lastDateOfServiceDateFormatter = DateFormatter()
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

        setMaterialTextFieldsAndViews(textfields: [txtfieldLastDate])
        lastDateOfServiceDateFormatter.dateStyle = .medium
        lastDateOfServiceDateFormatter.dateFormat = "MM/yyyy"
        txtfieldLastDate.addInputViewMonthYearDatePicker(target: self, selector: #selector(dateChanged))
        txtfieldLastDate.becomeFirstResponder()
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
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
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
            self.txtfieldLastDate.dividerColor = Theme.getSeparatorNormalColor()
            self.txtfieldLastDate.detail = ""
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        
        do{
            let lastDateOfService = try validation.validateLastDateOfService(txtfieldLastDate.text)
            DispatchQueue.main.async {
                self.txtfieldLastDate.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldLastDate.detail = ""
            }
            
        }
        catch{
            self.txtfieldLastDate.dividerColor = .red
            self.txtfieldLastDate.detail = error.localizedDescription
        }
        
        if (txtfieldLastDate.text != ""){
            self.dismissVC()
        }
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

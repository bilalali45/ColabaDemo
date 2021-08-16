//
//  AddMailingAddressViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/08/2021.
//

import UIKit
import Material

class AddMailingAddressViewController: UIViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint! //100 and 700
    @IBOutlet weak var txtfieldHomeAddress: TextField!
    @IBOutlet weak var btnSearch: UIButton!
    @IBOutlet weak var btnDropDown: UIButton!
    @IBOutlet weak var txtfieldStreetAddress: TextField!
    @IBOutlet weak var txtfieldUnitNo: TextField!
    @IBOutlet weak var txtfieldCity: TextField!
    @IBOutlet weak var txtfieldCounty: TextField!
    @IBOutlet weak var txtfieldState: TextField!
    @IBOutlet weak var btnStateDropDown: UIButton!
    @IBOutlet weak var txtfieldZipCode: TextField!
    @IBOutlet weak var txtfieldCountry: TextField!
    @IBOutlet weak var btnCountryDropDown: UIButton!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let moveInDateFormatter = DateFormatter()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldHomeAddress, txtfieldStreetAddress, txtfieldUnitNo, txtfieldCity, txtfieldCounty, txtfieldState, txtfieldZipCode, txtfieldCountry])
        NotificationCenter.default.addObserver(self, selector: #selector(goBackAfterDelete), name: NSNotification.Name(rawValue: kNotificationDeleteMailingAddressAndDismiss), object: nil)
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
    
    func showAllFields(){
        mainViewHeightConstraint.constant = 700
        txtfieldStreetAddress.isHidden = false
        txtfieldUnitNo.isHidden = false
        txtfieldCity.isHidden = false
        txtfieldCounty.isHidden = false
        txtfieldState.isHidden = false
        btnStateDropDown.isHidden = false
        txtfieldZipCode.isHidden = false
        txtfieldCountry.isHidden = false
        btnCountryDropDown.isHidden = false
        
        UIView.animate(withDuration: 0.5) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func goBackAfterDelete(){
        self.goBack()
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        let vc = Utility.getDeleteAddressPopupVC()
        vc.popupTitle = "Are you sure you want to delete Richard's Mailing Address?"
        vc.screenType = 3
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnSearchTapped(_ sender: UIButton){
        showAllFields()
    }
    
    @IBAction func btnDropdownTapped(_ sender: UIButton){
        showAllFields()
    }
    
    @IBAction func btnStateDropDownTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnCountryDropDownTapped(_ sender: UIButton) {
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.goBack()
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowMailingAddress), object: nil)
    }
    
}

extension AddMailingAddressViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        if (textField == txtfieldHomeAddress){
            txtfieldHomeAddress.placeholder = "Search Home Address"
            if txtfieldHomeAddress.text == ""{
                txtfieldHomeAddress.text = "       "
            }
        }
        
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldHomeAddress){
            if (txtfieldHomeAddress.text == "       "){
                txtfieldHomeAddress.text = ""
                txtfieldHomeAddress.placeholder = "       Search Home Address"
            }
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldHomeAddress, txtfieldStreetAddress, txtfieldUnitNo, txtfieldCity, txtfieldCounty, txtfieldState, txtfieldZipCode, txtfieldCountry])
    }
    
    func textField(_ textField: UITextField, shouldChangeCharactersIn range: NSRange, replacementString string: String) -> Bool {
        if (textField == txtfieldHomeAddress){
            if (txtfieldHomeAddress.text == "       " && string == ""){
                return false
            }
            else{
                return true
            }
        }
        return true
    }
}

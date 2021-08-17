//
//  AddMailingAddressViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/08/2021.
//

import UIKit
import Material
import DropDown

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
    @IBOutlet weak var stateDropDownAnchorView: UIView!
    @IBOutlet weak var btnStateDropDown: UIButton!
    @IBOutlet weak var txtfieldZipCode: TextField!
    @IBOutlet weak var txtfieldCountry: TextField!
    @IBOutlet weak var countryDropDownAnchorView: UIView!
    @IBOutlet weak var btnCountryDropDown: UIButton!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    let moveInDateFormatter = DateFormatter()
    let countryDropDown = DropDown()
    let stateDropDown = DropDown()
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
        setMaterialTextFieldsAndViews(textfields: [txtfieldHomeAddress, txtfieldStreetAddress, txtfieldUnitNo, txtfieldCity, txtfieldCounty, txtfieldState, txtfieldZipCode, txtfieldCountry])
        NotificationCenter.default.addObserver(self, selector: #selector(goBackAfterDelete), name: NSNotification.Name(rawValue: kNotificationDeleteMailingAddressAndDismiss), object: nil)
        txtfieldCountry.addTarget(self, action: #selector(txtfieldCountryTextChanged), for: .editingChanged)
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
            textfield.detailVerticalOffset = 4
        }
        btnSaveChanges.layer.cornerRadius = 5
        btnSaveChanges.dropShadowToCollectionViewCell()
        
        countryDropDown.dismissMode = .manual
        countryDropDown.anchorView = countryDropDownAnchorView
        countryDropDown.direction = .top
        countryDropDown.dataSource = kCountryListArray
        
        countryDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnCountryDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldCountry.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldCountry.detail = ""
            txtfieldCountry.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldCountry.text = item
            countryDropDown.hide()
        }
        
        stateDropDown.dismissMode = .manual
        stateDropDown.anchorView = stateDropDownAnchorView
        stateDropDown.direction = .top
        stateDropDown.dataSource = kUSAStatesArray
        
        stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldState.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldState.detail = ""
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
    
    @objc func txtfieldCountryTextChanged(){
        
        if (txtfieldCountry.text == ""){
            countryDropDown.dataSource = kCountryListArray
            countryDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldCountry.placeholderLabel.textColor = Theme.getAppGreyColor()
                txtfieldCountry.dividerColor = Theme.getSeparatorNormalColor()
                txtfieldCountry.detail = ""
                txtfieldCountry.text = item
                btnCountryDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        else{
            let filterCountries = kCountryListArray.filter{$0.contains(txtfieldCountry.text!)}
            countryDropDown.dataSource = filterCountries
            countryDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldCountry.dividerColor = Theme.getSeparatorNormalColor()
                txtfieldCountry.detail = ""
                txtfieldCountry.placeholderLabel.textColor = Theme.getAppGreyColor()
                txtfieldCountry.text = item
                btnCountryDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        
        countryDropDown.show()
    }
    
    @objc func txtfieldStateTextChanged(){
        if (txtfieldState.text == ""){
            stateDropDown.dataSource = kUSAStatesArray
            stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldState.dividerColor = Theme.getSeparatorNormalColor()
                txtfieldState.detail = ""
                txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
                txtfieldState.text = item
                btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        else{
            let filterStates = kUSAStatesArray.filter{$0.contains(txtfieldState.text!)}
            stateDropDown.dataSource = filterStates
            stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldState.dividerColor = Theme.getSeparatorNormalColor()
                txtfieldState.detail = ""
                txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
                txtfieldState.text = item
                btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        
        stateDropDown.show()
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
        
        do{
            let searchHomeAddress = try validation.validateSearchHomeAddress(txtfieldHomeAddress.text)
            DispatchQueue.main.async {
                self.txtfieldHomeAddress.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldHomeAddress.detail = ""
            }
            
        }
        catch{
            self.txtfieldHomeAddress.dividerColor = .red
            self.txtfieldHomeAddress.detail = error.localizedDescription
        }
        
        do{
            let streetAddress = try validation.validateStreetAddressHomeAddress(txtfieldStreetAddress.text)
            DispatchQueue.main.async {
                self.txtfieldStreetAddress.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldStreetAddress.detail = ""
            }
            
        }
        catch{
            self.txtfieldStreetAddress.dividerColor = .red
            self.txtfieldStreetAddress.detail = error.localizedDescription
        }
        
        do{
            let city = try validation.validateCity(txtfieldCity.text)
            DispatchQueue.main.async {
                self.txtfieldCity.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldCity.detail = ""
            }
            
        }
        catch{
            self.txtfieldCity.dividerColor = .red
            self.txtfieldCity.detail = error.localizedDescription
        }
        
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
        
        do{
            let zipCode = try validation.validateZipcode(txtfieldZipCode.text)
            DispatchQueue.main.async {
                self.txtfieldZipCode.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldZipCode.detail = ""
            }
            
        }
        catch{
            self.txtfieldZipCode.dividerColor = .red
            self.txtfieldZipCode.detail = error.localizedDescription
        }
        
        do{
            let country = try validation.validateCountry(txtfieldCountry.text)
            DispatchQueue.main.async {
                self.txtfieldCountry.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldCountry.detail = ""
            }
            
        }
        catch{
            self.txtfieldCountry.dividerColor = .red
            self.txtfieldCountry.detail = error.localizedDescription
        }
        
        
        if (self.txtfieldHomeAddress.text != "" && txtfieldStreetAddress.text != "" && txtfieldCity.text != "" && txtfieldState.text != "" && txtfieldZipCode.text != "" && txtfieldCountry.text != ""){
            self.goBack()
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowMailingAddress), object: nil)
        }
        
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
        
        if (textField == txtfieldCountry){
            //textField.endEditing(true)
            btnCountryDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            countryDropDown.show()
        }
        
        if (textField == txtfieldState){
            //textField.endEditing(true)
            btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            stateDropDown.show()
        }
        
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        
        if (textField == txtfieldHomeAddress){
            if (txtfieldHomeAddress.text == "       "){
                txtfieldHomeAddress.text = ""
                txtfieldHomeAddress.placeholder = "       Search Home Address"
            }
            
            do{
                let searchHomeAddress = try validation.validateSearchHomeAddress(txtfieldHomeAddress.text!.replacingOccurrences(of: "       ", with: ""))
                DispatchQueue.main.async {
                    self.txtfieldHomeAddress.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldHomeAddress.detail = ""
                }
                
            }
            catch{
                self.txtfieldHomeAddress.dividerColor = .red
                self.txtfieldHomeAddress.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldState){
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
        
        if (textField == txtfieldCountry){
            btnCountryDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            do{
                let country = try validation.validateCountry(txtfieldCountry.text)
                DispatchQueue.main.async {
                    self.txtfieldCountry.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldCountry.detail = ""
                }
                
            }
            catch{
                self.txtfieldCountry.dividerColor = .red
                self.txtfieldCountry.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldStreetAddress){
            do{
                let streetAddress = try validation.validateStreetAddressHomeAddress(txtfieldStreetAddress.text)
                DispatchQueue.main.async {
                    self.txtfieldStreetAddress.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldStreetAddress.detail = ""
                }
                
            }
            catch{
                self.txtfieldStreetAddress.dividerColor = .red
                self.txtfieldStreetAddress.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldCity){
            do{
                let city = try validation.validateCity(txtfieldCity.text)
                DispatchQueue.main.async {
                    self.txtfieldCity.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldCity.detail = ""
                }
                
            }
            catch{
                self.txtfieldCity.dividerColor = .red
                self.txtfieldCity.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldZipCode){
            do{
                let zipCode = try validation.validateZipcode(txtfieldZipCode.text)
                DispatchQueue.main.async {
                    self.txtfieldZipCode.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldZipCode.detail = ""
                }
                
            }
            catch{
                self.txtfieldZipCode.dividerColor = .red
                self.txtfieldZipCode.detail = error.localizedDescription
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
        if (textField == txtfieldZipCode){
            return string == "" ? true : (txtfieldZipCode.text!.count < 5)
        }
        return true
    }
}

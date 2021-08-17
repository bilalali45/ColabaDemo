//
//  AddResidenceViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 09/08/2021.
//

import UIKit
import Material
import MonthYearPicker
import DropDown

class AddResidenceViewController: UIViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint! //350 and 1100
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
    @IBOutlet weak var txtfieldMoveInDate: TextField!
    @IBOutlet weak var txtfieldMoveInDateTopConstraint: NSLayoutConstraint! //583 and 30
    @IBOutlet weak var btnCalendar: UIButton!
    @IBOutlet weak var btnCalendarTopConstraint: NSLayoutConstraint! //589 and 36
    @IBOutlet weak var txtfieldHousingStatus: TextField!
    @IBOutlet weak var housingStatusDropDownAnchorView: UIView!
    @IBOutlet weak var btnHousingStatusDropDown: UIButton!
    @IBOutlet weak var txtfieldMonthlyRent: TextField!
    @IBOutlet weak var txtfieldMonthlyRentTopConstraint: NSLayoutConstraint! //30 or 0
    @IBOutlet weak var txtfieldMonthlyRentHeightConstraint: NSLayoutConstraint! //49 or 0
    @IBOutlet weak var addMailingAddressStackView: UIStackView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    @IBOutlet weak var tblViewMailingAddress: UITableView!
    
    let moveInDateFormatter = DateFormatter()
    let housingStatusDropDown = DropDown()
    let countryDropDown = DropDown()
    let stateDropDown = DropDown()
    var numberOfMailingAddress = 1
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews(textfields: [txtfieldHomeAddress, txtfieldStreetAddress, txtfieldUnitNo, txtfieldCity, txtfieldCounty, txtfieldState, txtfieldZipCode, txtfieldCountry, txtfieldMoveInDate, txtfieldHousingStatus, txtfieldMonthlyRent])
        NotificationCenter.default.addObserver(self, selector: #selector(showMailingAddress), name: NSNotification.Name(rawValue: kNotificationShowMailingAddress), object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(dismissAddressVC), name: NSNotification.Name(rawValue: kNotificationSaveAddressAndDismiss), object: nil)
        
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
        
        housingStatusDropDown.dismissMode = .manual
        housingStatusDropDown.anchorView = housingStatusDropDownAnchorView
        housingStatusDropDown.dataSource = kHousingStatusArray
        housingStatusDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnHousingStatusDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldHousingStatus.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldHousingStatus.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldHousingStatus.text = item
            housingStatusDropDown.hide()
            txtfieldMonthlyRent.isHidden = item != "Rent"
            txtfieldMonthlyRentTopConstraint.constant = item == "Rent" ? 30 : 0
            txtfieldMonthlyRentHeightConstraint.constant = item == "Rent" ? 49 : 0
            UIView.animate(withDuration: 0.5) {
                self.view.layoutSubviews()
            }
        }
        
        countryDropDown.dismissMode = .manual
        countryDropDown.anchorView = countryDropDownAnchorView
        countryDropDown.direction = .top
        countryDropDown.dataSource = kCountryListArray
        
        countryDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnCountryDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
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
            txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldState.text = item
            stateDropDown.hide()
        }
        
        addMailingAddressStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addMailingAddressStackViewTapped)))
        btnSaveChanges.layer.cornerRadius = 5
        btnSaveChanges.dropShadowToCollectionViewCell()
        
        moveInDateFormatter.dateStyle = .medium
        moveInDateFormatter.dateFormat = "MM/yyyy"
        txtfieldMoveInDate.addInputViewMonthYearDatePicker(target: self, selector: #selector(dateChanged))
        
        txtfieldCountry.addTarget(self, action: #selector(txtfieldCountryTextChanged), for: .editingChanged)
        txtfieldState.addTarget(self, action: #selector(txtfieldStateTextChanged), for: .editingChanged)
        
        tblViewMailingAddress.register(UINib(nibName: "BorrowerAddressInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerAddressInfoTableViewCell")
        
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
        mainViewHeightConstraint.constant = 1100
        txtfieldMoveInDateTopConstraint.constant = 583
        btnCalendarTopConstraint.constant = 589
        txtfieldStreetAddress.isHidden = false
        txtfieldUnitNo.isHidden = false
        txtfieldCity.isHidden = false
        txtfieldCounty.isHidden = false
        txtfieldState.isHidden = false
        btnStateDropDown.isHidden = false
        txtfieldZipCode.isHidden = false
        txtfieldCountry.isHidden = false
        btnCountryDropDown.isHidden = false
        addMailingAddressStackView.isHidden = false
        tblViewMailingAddress.isHidden = true
        
        UIView.animate(withDuration: 0.5) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func showMailingAddress(){
        self.showPopup(message: "Mailing address has been added", popupState: .success, popupDuration: .custom(5)) { reason in
            
        }
        self.tblViewMailingAddress.isHidden = false
        self.addMailingAddressStackView.isHidden = true
    }
    
    @objc func addMailingAddressStackViewTapped(){
        let vc = Utility.getAddMailingAddressVC()
        self.pushToVC(vc: vc)
    }
    
    @objc func dateChanged() {
        if let  datePicker = self.txtfieldMoveInDate.inputView as? MonthYearPickerView {
            self.txtfieldMoveInDate.text = moveInDateFormatter.string(from: datePicker.date)
        }
    }
    
    @objc func dismissAddressVC(){
        self.dismissVC()
    }
    
    @objc func txtfieldCountryTextChanged(){
        
        if (txtfieldCountry.text == ""){
            countryDropDown.dataSource = kCountryListArray
            countryDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldCountry.text = item
                txtfieldCountry.placeholderLabel.textColor = Theme.getAppGreyColor()
                btnCountryDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        else{
            let filterCountries = kCountryListArray.filter{$0.contains(txtfieldCountry.text!)}
            countryDropDown.dataSource = filterCountries
            countryDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldCountry.text = item
                txtfieldCountry.placeholderLabel.textColor = Theme.getAppGreyColor()
                btnCountryDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        
        countryDropDown.show()
    }
    
    @objc func txtfieldStateTextChanged(){
        if (txtfieldState.text == ""){
            stateDropDown.dataSource = kUSAStatesArray
            stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldState.text = item
                txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
                btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        else{
            let filterStates = kUSAStatesArray.filter{$0.contains(txtfieldState.text!)}
            stateDropDown.dataSource = filterStates
            stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldState.text = item
                txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
                btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        
        stateDropDown.show()
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        let vc = Utility.getSaveAddressPopupVC()
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        let vc = Utility.getDeleteAddressPopupVC()
        vc.popupTitle = "Are you sure you want to delete Richard's Current Residence?"
        vc.screenType = 2
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
    
    @IBAction func btnCalendarTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnHousingDropDownTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
}

extension AddResidenceViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return numberOfMailingAddress
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerAddressInfoTableViewCell", for: indexPath) as! BorrowerAddressInfoTableViewCell
        cell.addressIcon.isHidden = true
        cell.lblHeading.isHidden = true
        cell.lblRent.isHidden = true
        cell.lblAddressTopConstraint.constant = 15
        cell.lblAddress.text = "4101  Oak Tree Avenue  LN # 222, Chicago, MD 60605"
        cell.lblDate.text = "Mailing Address"
        cell.mainView.layer.cornerRadius = 6
        cell.mainView.layer.borderWidth = 1
        cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        cell.mainView.dropShadowToCollectionViewCell()
        cell.mainView.updateConstraintsIfNeeded()
        cell.mainView.layoutSubviews()
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        let vc = Utility.getAddMailingAddressVC()
        self.pushToVC(vc: vc)
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return UITableView.automaticDimension
    }
    
    func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        return true
    }
    
    func tableView(_ tableView: UITableView, trailingSwipeActionsConfigurationForRowAt indexPath: IndexPath) -> UISwipeActionsConfiguration? {
        
        let deleteAction = UIContextualAction(style: .normal, title: "") { action, actionView, bool in
            let vc = Utility.getDeleteAddressPopupVC()
            vc.popupTitle = "Are you sure you want to delete Richard's Mailing Address?"
            vc.screenType = 1
            vc.indexPath = indexPath
            vc.delegate = self
            self.present(vc, animated: false, completion: nil)
        }
        deleteAction.backgroundColor = Theme.getDashboardBackgroundColor()
        deleteAction.image = UIImage(named: "AddressDeleteIconBig")
        return UISwipeActionsConfiguration(actions: [deleteAction])
        
    }
    
}

extension AddResidenceViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        numberOfMailingAddress = 0
        self.tblViewMailingAddress.deleteRows(at: [indexPath], with: .left)
        self.tblViewMailingAddress.reloadData()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.tblViewMailingAddress.isHidden = true
            self.addMailingAddressStackView.isHidden = false
        }
    }
}

extension AddResidenceViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        if (textField == txtfieldHomeAddress){
            txtfieldHomeAddress.placeholder = "Search Home Address"
            if txtfieldHomeAddress.text == ""{
                txtfieldHomeAddress.text = "       "
            }
        }
        if (textField == txtfieldMoveInDate){
            dateChanged()
        }
        if (textField == txtfieldHousingStatus){
            textField.endEditing(true)
            txtfieldHousingStatus.dividerColor = Theme.getButtonBlueColor()
            btnHousingStatusDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
            housingStatusDropDown.show()
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
        }
        
        if (textField == txtfieldState){
            btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
        }
        
        if (textField == txtfieldCountry){
            btnCountryDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
        }
        
        if (textField == txtfieldHousingStatus){
            btnHousingStatusDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldHousingStatus.dividerColor = Theme.getSeparatorNormalColor()
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldHomeAddress, txtfieldStreetAddress, txtfieldUnitNo, txtfieldCity, txtfieldCounty, txtfieldState, txtfieldZipCode, txtfieldCountry, txtfieldMoveInDate, txtfieldHousingStatus, txtfieldMonthlyRent])
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

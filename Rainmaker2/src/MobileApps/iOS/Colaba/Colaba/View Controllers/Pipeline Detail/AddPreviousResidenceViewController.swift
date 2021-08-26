//
//  AddPreviousResidenceViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 26/08/2021.
//

import UIKit
import Material
import MonthYearPicker
import DropDown
import GooglePlaces

class AddPreviousResidenceViewController: UIViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint! //330 and 1050
    @IBOutlet weak var txtfieldHomeAddress: TextField!
    @IBOutlet weak var btnSearch: UIButton!
    @IBOutlet weak var btnSearchTopConstraint: NSLayoutConstraint! //34 or 36
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
    @IBOutlet weak var txtfieldMoveInDateTopConstraint: NSLayoutConstraint! //513 and 30
    @IBOutlet weak var btnCalendar: UIButton!
    @IBOutlet weak var btnCalendarTopConstraint: NSLayoutConstraint! //589 and 36
    @IBOutlet weak var txtfieldMoveOutDate: TextField!
    @IBOutlet weak var txtfieldHousingStatus: TextField!
    @IBOutlet weak var housingStatusDropDownAnchorView: UIView!
    @IBOutlet weak var btnHousingStatusDropDown: UIButton!
    @IBOutlet weak var txtfieldMonthlyRent: TextField!
    @IBOutlet weak var txtfieldMonthlyRentTopConstraint: NSLayoutConstraint! //30 or 0
    @IBOutlet weak var txtfieldMonthlyRentHeightConstraint: NSLayoutConstraint! //39 or 0
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    @IBOutlet weak var tblViewPlaces: UITableView!
    var placesData=[GMSAutocompletePrediction]()
    var fetcher: GMSAutocompleteFetcher?
    
    let moveInDateFormatter = DateFormatter()
    let housingStatusDropDown = DropDown()
    let countryDropDown = DropDown()
    let stateDropDown = DropDown()
    var numberOfMailingAddress = 1
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
        setMaterialTextFieldsAndViews(textfields: [txtfieldHomeAddress, txtfieldStreetAddress, txtfieldUnitNo, txtfieldCity, txtfieldCounty, txtfieldState, txtfieldZipCode, txtfieldCountry, txtfieldMoveInDate, txtfieldMoveOutDate, txtfieldHousingStatus, txtfieldMonthlyRent])
        NotificationCenter.default.addObserver(self, selector: #selector(dismissAddressVC), name: NSNotification.Name(rawValue: kNotificationSaveAddressAndDismiss), object: nil)
        
        let filter = GMSAutocompleteFilter()
        filter.type = .address

        // Create the fetcher.
        fetcher = GMSAutocompleteFetcher(filter: filter)
        fetcher?.delegate = self as GMSAutocompleteFetcherDelegate

        txtfieldHomeAddress.addTarget(self, action: #selector(txtfieldHomeAddressTextChanged), for: UIControl.Event.editingChanged)

        tblViewPlaces.delegate = self
        tblViewPlaces.dataSource = self

        tblViewPlaces.reloadData()
        tblViewPlaces.dropShadowToCollectionViewCell()
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
        
        housingStatusDropDown.dismissMode = .manual
        housingStatusDropDown.anchorView = housingStatusDropDownAnchorView
        housingStatusDropDown.dataSource = kHousingStatusArray
        housingStatusDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
            btnHousingStatusDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldHousingStatus.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldHousingStatus.detail = ""
            txtfieldHousingStatus.dividerColor = Theme.getSeparatorNormalColor()
            txtfieldHousingStatus.placeholderLabel.textColor = Theme.getAppGreyColor()
            txtfieldHousingStatus.text = item
            housingStatusDropDown.hide()
            txtfieldMonthlyRent.isHidden = item != "Rent"
            txtfieldMonthlyRentTopConstraint.constant = item == "Rent" ? 30 : 0
            txtfieldMonthlyRentHeightConstraint.constant = item == "Rent" ? 39 : 0
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
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
        
        moveInDateFormatter.dateStyle = .medium
        moveInDateFormatter.dateFormat = "MM/yyyy"
        txtfieldMoveInDate.addInputViewMonthYearDatePicker(target: self, selector: #selector(dateChanged))
        txtfieldMoveOutDate.addInputViewMonthYearDatePicker(target: self, selector: #selector(moveOutDateChanged))
        
        txtfieldCountry.addTarget(self, action: #selector(txtfieldCountryTextChanged), for: .editingChanged)
        txtfieldState.addTarget(self, action: #selector(txtfieldStateTextChanged), for: .editingChanged)
        
        
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
    
    @objc func txtfieldHomeAddressTextChanged(){
        btnDropDown.setImage(UIImage(named: "textfield-dropdownIconUp"), for: .normal)
        tblViewPlaces.isHidden = txtfieldHomeAddress.text == "       "
        fetcher?.sourceTextHasChanged(txtfieldHomeAddress.text!.replacingOccurrences(of: "       ", with: ""))
    }
    
    func showAutoCompletePlaces(){
        let autocompleteController = GMSAutocompleteViewController()
            autocompleteController.delegate = self

//            // Specify the place data types to return.
//            let fields: GMSPlaceField = GMSPlaceField(rawValue: UInt(GMSPlaceField.name.rawValue) |
//                                                        UInt(GMSPlaceField.placeID.rawValue))
//            autocompleteController.placeFields = fields

            // Specify a filter.
            let filter = GMSAutocompleteFilter()
            filter.type = .address
            autocompleteController.autocompleteFilter = filter

            // Display the autocomplete view controller.
            self.present(autocompleteController, animated: true, completion: nil)
    }
    
    func showAllFields(){
        mainViewHeightConstraint.constant = 950
        txtfieldMoveInDateTopConstraint.constant = 513
        btnCalendarTopConstraint.constant = 517
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
    
    func getAddressFromLatLon(pdblLatitude: String, withLongitude pdblLongitude: String) {
            var center : CLLocationCoordinate2D = CLLocationCoordinate2D()
            let lat: Double = Double("\(pdblLatitude)")!
            //21.228124
            let lon: Double = Double("\(pdblLongitude)")!
            //72.833770
            let ceo: CLGeocoder = CLGeocoder()
            center.latitude = lat
            center.longitude = lon

            let loc: CLLocation = CLLocation(latitude:center.latitude, longitude: center.longitude)
            ceo.reverseGeocodeLocation(loc, completionHandler:
            {(placemarks, error) in
                if (error != nil)
                {
                    print("reverse geodcode fail: \(error!.localizedDescription)")
                }
                else{
                    if let pm = placemarks?.first {
                        if let city = pm.locality{
                            self.txtfieldCity.text = city
                        }
                        if let county = pm.subAdministrativeArea{
                            self.txtfieldCounty.text = county
                        }
                        if let state = pm.administrativeArea{
                            self.txtfieldState.text = state
                        }
                        if let zipCode = pm.postalCode{
                            self.txtfieldZipCode.text = zipCode
                        }
                        if let country = pm.country{
                            self.txtfieldCountry.text = country
                        }
                        
                  }
                }
        })
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
    
    @objc func moveOutDateChanged() {
        if let  datePicker = self.txtfieldMoveOutDate.inputView as? MonthYearPickerView {
            self.txtfieldMoveOutDate.text = moveInDateFormatter.string(from: datePicker.date)
        }
    }
    
    @objc func dismissAddressVC(){
        self.dismissVC()
    }
    
    @objc func txtfieldCountryTextChanged(){
        
        if (txtfieldCountry.text == ""){
            countryDropDown.dataSource = kCountryListArray
            countryDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldCountry.dividerColor = Theme.getSeparatorNormalColor()
                txtfieldCountry.detail = ""
                txtfieldCountry.text = item
                txtfieldCountry.placeholderLabel.textColor = Theme.getAppGreyColor()
                btnCountryDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        else{
            let filterCountries = kCountryListArray.filter{$0.contains(txtfieldCountry.text!)}
            countryDropDown.dataSource = filterCountries
            countryDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldCountry.dividerColor = Theme.getSeparatorNormalColor()
                txtfieldCountry.detail = ""
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
                txtfieldState.dividerColor = Theme.getSeparatorNormalColor()
                txtfieldState.detail = ""
                txtfieldState.text = item
                txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
                btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        else{
            let filterStates = kUSAStatesArray.filter{$0.contains(txtfieldState.text!)}
            stateDropDown.dataSource = filterStates
            stateDropDown.selectionAction = { [unowned self] (index: Int, item: String) in
                txtfieldState.dividerColor = Theme.getSeparatorNormalColor()
                txtfieldState.detail = ""
                txtfieldState.text = item
                txtfieldState.placeholderLabel.textColor = Theme.getAppGreyColor()
                btnStateDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            }
        }
        
        stateDropDown.show()
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        let vc = Utility.getSaveAddressPopupVC()
        vc.isForPrevAddress = true
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        let vc = Utility.getDeleteAddressPopupVC()
        vc.popupTitle = "Are you sure you want to delete Richard's Current Residence?"
        vc.screenType = 2
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnSearchTapped(_ sender: UIButton){
        //showAllFields()
        //getAddressFromLatLon(pdblLatitude: "21.228124", withLongitude: "72.833770")
//        let autocompleteController = GMSAutocompleteViewController()
//            autocompleteController.delegate = self
//
//            // Specify the place data types to return.
//            let fields: GMSPlaceField = GMSPlaceField(rawValue: UInt(GMSPlaceField.name.rawValue) |
//                                                        UInt(GMSPlaceField.placeID.rawValue))
//            autocompleteController.placeFields = fields
//
//            // Specify a filter.
//            let filter = GMSAutocompleteFilter()
//            filter.type = .address
//            autocompleteController.autocompleteFilter = filter
//
//            // Display the autocomplete view controller.
//        self.present(autocompleteController, animated: true, completion: nil)
    }
    
    @IBAction func btnDropdownTapped(_ sender: UIButton){
       // showAllFields()
//        let autocompleteController = GMSAutocompleteViewController()
//            autocompleteController.delegate = self
//
//            // Specify the place data types to return.
//            let fields: GMSPlaceField = GMSPlaceField(rawValue: UInt(GMSPlaceField.name.rawValue) |
//                                                        UInt(GMSPlaceField.placeID.rawValue))
//            autocompleteController.placeFields = fields
//
//            // Specify a filter.
//            let filter = GMSAutocompleteFilter()
//            filter.type = .address
//            autocompleteController.autocompleteFilter = filter
//
//            // Display the autocomplete view controller.
//            present(autocompleteController, animated: true, completion: nil)
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
        
        do{
            let moveInDate = try validation.validateMoveInDate(txtfieldMoveInDate.text)
            DispatchQueue.main.async {
                self.txtfieldMoveInDate.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldMoveInDate.detail = ""
            }
            
        }
        catch{
            self.txtfieldMoveInDate.dividerColor = .red
            self.txtfieldMoveInDate.detail = error.localizedDescription
        }
        
        do{
            let housingStatus = try validation.validateHousingStatus(txtfieldHousingStatus.text)
            DispatchQueue.main.async {
                self.txtfieldHousingStatus.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldHousingStatus.detail = ""
            }
            
        }
        catch{
            self.txtfieldHousingStatus.dividerColor = .red
            self.txtfieldHousingStatus.detail = error.localizedDescription
        }
        
        if (txtfieldHousingStatus.text == "Rent"){
            do{
                let rent = try validation.validateMonthlyRent(txtfieldMonthlyRent.text)
                DispatchQueue.main.async {
                    self.txtfieldMonthlyRent.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldMonthlyRent.detail = ""
                }
                
            }
            catch{
                self.txtfieldMonthlyRent.dividerColor = .red
                self.txtfieldMonthlyRent.detail = error.localizedDescription
            }
        }
        
        if (self.txtfieldHomeAddress.text != "" && txtfieldStreetAddress.text != "" && txtfieldCity.text != "" && txtfieldState.text != "" && txtfieldZipCode.text != "" && txtfieldCountry.text != "" && txtfieldMoveInDate.text != "" && txtfieldHousingStatus.text != ""){
            if (txtfieldHousingStatus.text == "Rent" && txtfieldMonthlyRent.text != ""){
                self.dismissVC()
            }
            else if (txtfieldHousingStatus.text != "Rent"){
                self.dismissVC()
            }
        }
        
    }
    
}

extension AddPreviousResidenceViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        if (tableView == tblViewPlaces){
            return placesData.count
        }
        else{
            return numberOfMailingAddress
        }
        
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        if (tableView == tblViewPlaces){
            let cell: UITableViewCell = UITableViewCell()

            cell.selectionStyle =  .default
            cell.backgroundColor = UIColor.white
            cell.contentView.backgroundColor = UIColor.clear
            cell.textLabel?.textAlignment = NSTextAlignment.left
            cell.textLabel?.textColor = Theme.getAppBlackColor()
            cell.textLabel?.font = Theme.getRubikMediumFont(size: 14)
            cell.textLabel?.text = placesData[indexPath.row].attributedFullText.string
            return cell
            
        }
        else{
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
        
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        tblViewPlaces.isHidden = true
        btnDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
        self.txtfieldStreetAddress.text = placesData[indexPath.row].attributedPrimaryText.string
        GMSPlacesClient.shared().fetchPlace(fromPlaceID: placesData[indexPath.row].placeID, placeFields: .all, sessionToken: nil) { place, error in
            if let formattedAddress = place?.formattedAddress{
                self.txtfieldHomeAddress.text = "       \(formattedAddress)"
                self.txtfieldHomeAddress.placeholder = "Search Previous Home Address"
                self.txtfieldHomeAddress.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldHomeAddress.detail = ""
            }
            self.showAllFields()
            if let latitude = place?.coordinate.latitude, let longitude = place?.coordinate.longitude{
                self.getAddressFromLatLon(pdblLatitude: "\(latitude)", withLongitude: "\(longitude)")
            }
            
        }
        tableView.deselectRow(at: indexPath, animated: true)
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 40
    }
    
}

extension AddPreviousResidenceViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        if (textField == txtfieldHomeAddress){
            //showAutoCompletePlaces()
            btnSearchTopConstraint.constant = 37
            self.view.layoutSubviews()
            txtfieldHomeAddress.placeholder = "Search Previous Home Address"
            if txtfieldHomeAddress.text == ""{
                txtfieldHomeAddress.text = "       "
            }
            
        }
        if (textField == txtfieldMoveInDate){
            dateChanged()
        }
        if (textField == txtfieldMoveOutDate){
            moveOutDateChanged()
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
            btnDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            if (txtfieldHomeAddress.text == "       "){
                txtfieldHomeAddress.text = ""
                txtfieldHomeAddress.placeholder = "       Search Previous Home Address"
                btnSearchTopConstraint.constant = 34
                self.view.layoutSubviews()
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
            
            if !(kUSAStatesArray.contains(txtfieldState.text!)){
                txtfieldState.text = ""
                txtfieldState.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                stateDropDown.hide()
            }
            
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
            
            if !(kCountryListArray.contains(txtfieldCountry.text!)){
                txtfieldCountry.text = ""
                txtfieldCountry.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                countryDropDown.hide()
            }
            
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
        
        if (textField == txtfieldHousingStatus){
            btnHousingStatusDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            txtfieldHousingStatus.dividerColor = Theme.getSeparatorNormalColor()
            do{
                let housingStatus = try validation.validateHousingStatus(txtfieldHousingStatus.text)
                DispatchQueue.main.async {
                    self.txtfieldHousingStatus.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldHousingStatus.detail = ""
                }
                
            }
            catch{
                self.txtfieldHousingStatus.dividerColor = .red
                self.txtfieldHousingStatus.detail = error.localizedDescription
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
        
        if (textField == txtfieldMoveInDate){
            do{
                let moveInDate = try validation.validateMoveInDate(txtfieldMoveInDate.text)
                DispatchQueue.main.async {
                    self.txtfieldMoveInDate.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldMoveInDate.detail = ""
                }
                
            }
            catch{
                self.txtfieldMoveInDate.dividerColor = .red
                self.txtfieldMoveInDate.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldMonthlyRent){
            if (txtfieldHousingStatus.text == "Rent"){
                do{
                    let rent = try validation.validateMonthlyRent(txtfieldMonthlyRent.text)
                    DispatchQueue.main.async {
                        self.txtfieldMonthlyRent.dividerColor = Theme.getSeparatorNormalColor()
                        self.txtfieldMonthlyRent.detail = ""
                    }
                    
                }
                catch{
                    self.txtfieldMonthlyRent.dividerColor = .red
                    self.txtfieldMonthlyRent.detail = error.localizedDescription
                }
            }
        }
        
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldHomeAddress, txtfieldStreetAddress, txtfieldUnitNo, txtfieldCity, txtfieldCounty, txtfieldState, txtfieldZipCode, txtfieldCountry, txtfieldMoveInDate, txtfieldMoveOutDate, txtfieldHousingStatus, txtfieldMonthlyRent])
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

extension AddPreviousResidenceViewController: GMSAutocompleteViewControllerDelegate {

  // Handle the user's selection.
  func viewController(_ viewController: GMSAutocompleteViewController, didAutocompleteWith place: GMSPlace) {
    if let formattedAddress = place.formattedAddress{
        txtfieldHomeAddress.text = "       \(formattedAddress)"
        txtfieldHomeAddress.placeholder = "Search Previous Home Address"
        txtfieldHomeAddress.dividerColor = Theme.getSeparatorNormalColor()
        txtfieldHomeAddress.detail = ""
    }
    if let shortName = place.addressComponents?.first?.shortName{
        txtfieldStreetAddress.text = shortName
    }
    showAllFields()
    getAddressFromLatLon(pdblLatitude: "\(place.coordinate.latitude)", withLongitude: "\(place.coordinate.longitude)")
    dismiss(animated: true, completion: nil)
  }

  func viewController(_ viewController: GMSAutocompleteViewController, didFailAutocompleteWithError error: Error) {
    // TODO: handle the error.
    print("Error: ", error.localizedDescription)
  }

  // User canceled the operation.
  func wasCancelled(_ viewController: GMSAutocompleteViewController) {
    dismiss(animated: true, completion: nil)
  }

  // Turn the network activity indicator on and off again.
  func didRequestAutocompletePredictions(_ viewController: GMSAutocompleteViewController) {
    UIApplication.shared.isNetworkActivityIndicatorVisible = true
  }

  func didUpdateAutocompletePredictions(_ viewController: GMSAutocompleteViewController) {
    UIApplication.shared.isNetworkActivityIndicatorVisible = false
  }

}

extension AddPreviousResidenceViewController: GMSAutocompleteFetcherDelegate {
    
    func didAutocomplete(with predictions: [GMSAutocompletePrediction]) {
        placesData.removeAll()
        placesData = predictions

        tblViewPlaces.reloadData()
    }

    func didFailAutocompleteWithError(_ error: Error) {
        print(error.localizedDescription)
    }
    
}

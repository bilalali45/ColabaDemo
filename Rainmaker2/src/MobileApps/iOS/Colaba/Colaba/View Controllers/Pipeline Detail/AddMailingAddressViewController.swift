//
//  AddMailingAddressViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/08/2021.
//

import UIKit
import Material
import GooglePlaces

class AddMailingAddressViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint! //100 and 700
    @IBOutlet weak var txtfieldHomeAddress: TextField!
    @IBOutlet weak var btnSearchTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var btnDropDown: UIButton!
    @IBOutlet weak var txtfieldStreetAddress: ColabaTextField!
    @IBOutlet weak var txtfieldUnitNo: ColabaTextField!
    @IBOutlet weak var txtfieldCity: ColabaTextField!
    @IBOutlet weak var txtfieldCounty: ColabaTextField!
    @IBOutlet weak var txtfieldState: ColabaTextField!
    @IBOutlet weak var txtfieldZipCode: ColabaTextField!
    @IBOutlet weak var txtfieldCountry: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    @IBOutlet weak var tblViewPlaces: UITableView!
    var placesData=[GMSAutocompletePrediction]()
    var fetcher: GMSAutocompleteFetcher?
    var selectedAddress = BorrowerAddress()
    var borrowerFirstName = ""
    var borrowerLastName = ""
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
        setPlacePickerTextField()
        lblBorrowerName.text = "\(borrowerFirstName.uppercased()) \(borrowerLastName.uppercased())"
        setAddressData()
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Street Address Text Field
        txtfieldStreetAddress.setTextField(placeholder: "Street Address", controller: self, validationType: .required)
        
        ///Unit Number Text Field
        txtfieldUnitNo.setTextField(placeholder: "Unit Or Apt. Number (Optional)", controller: self, validationType: .noValidation)
        
        ///City Text Field
        txtfieldCity.setTextField(placeholder: "City", controller: self, validationType: .required)
        
        ///County Text Field
        txtfieldCounty.setTextField(placeholder: "County", controller: self, validationType: .noValidation)
        
        ///State Text Field
        txtfieldState.setTextField(placeholder: "State", controller: self, validationType: .required)
        txtfieldState.type = .editableDropdown
        txtfieldState.setDropDownDataSource(kUSAStatesArray)
        
        ///Zip Code Text Field
        txtfieldZipCode.setTextField(placeholder: "Zip Code", controller: self, validationType: .required, keyboardType: .numberPad)
        
        ///Country Text Field
        txtfieldCountry.setTextField(placeholder: "Country", controller: self, validationType: .required)
        txtfieldCountry.type = .editableDropdown
        txtfieldCountry.setDropDownDataSource(kCountryListArray)
    }
    
    func setAddressData(){
        if (selectedAddress.id > 0){
            let address = selectedAddress.isMailingAddressDifferent ? selectedAddress.mailingAddressModel : selectedAddress.addressModel
            showAllFields()
            txtfieldHomeAddress.text = address.street
            txtfieldStreetAddress.setTextField(text: address.street)
            txtfieldUnitNo.setTextField(text: address.unit)
            txtfieldCity.setTextField(text: address.city)
            txtfieldCounty.setTextField(text: address.countyName)
            txtfieldState.setTextField(text: address.stateName)
            txtfieldZipCode.setTextField(text: address.zipCode)
            txtfieldCountry.setTextField(text: address.countryName)
        }
    }
    
    func setPlacePickerTextField() {
        txtfieldHomeAddress.dividerActiveColor = Theme.getButtonBlueColor()
        txtfieldHomeAddress.dividerColor = Theme.getSeparatorNormalColor()
        txtfieldHomeAddress.placeholderActiveColor = Theme.getAppGreyColor()
        txtfieldHomeAddress.delegate = self
        txtfieldHomeAddress.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
        txtfieldHomeAddress.detailLabel.font = Theme.getRubikRegularFont(size: 12)
        txtfieldHomeAddress.detailColor = .red
        txtfieldHomeAddress.detailVerticalOffset = 4
        txtfieldHomeAddress.placeholderVerticalOffset = 8
        txtfieldHomeAddress.textColor = Theme.getAppBlackColor()
        txtfieldHomeAddress.textInsetsPreset = .horizontally5
        
        NotificationCenter.default.addObserver(self, selector: #selector(goBackAfterDelete), name: NSNotification.Name(rawValue: kNotificationDeleteMailingAddressAndDismiss), object: nil)
        
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
        tblViewPlaces.isHidden = txtfieldHomeAddress.text == ""
        fetcher?.sourceTextHasChanged(txtfieldHomeAddress.text!)
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
        mainViewHeightConstraint.constant = 640
        txtfieldStreetAddress.isHidden = false
        txtfieldUnitNo.isHidden = false
        txtfieldCity.isHidden = false
        txtfieldCounty.isHidden = false
        txtfieldState.isHidden = false
        txtfieldZipCode.isHidden = false
        txtfieldCountry.isHidden = false
        
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    func changedDeleteButton(){
        let deleteIcon = UIImage(named: "AddressDeleteIcon")?.withRenderingMode(.alwaysTemplate)
        btnDelete.setImage(deleteIcon, for: .normal)
        btnDelete.tintColor = .red
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
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
            if (self.txtfieldHomeAddress.text != "" && txtfieldStreetAddress.text != "" && txtfieldCity.text != "" && txtfieldState.text != "" && txtfieldZipCode.text != "" && txtfieldCountry.text != ""){
                self.goBack()
                NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowMailingAddress), object: nil)
            }
        }
    }
    
    func validate() -> Bool {
        var isValidate = validateHomeAddress()
        isValidate = txtfieldStreetAddress.validate() && isValidate
        isValidate = txtfieldCity.validate() && isValidate
        isValidate = txtfieldState.validate() && isValidate
        isValidate = txtfieldZipCode.validate() && isValidate
        isValidate = txtfieldCountry.validate() && isValidate
        return isValidate
    }
}

extension AddMailingAddressViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return placesData.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell: UITableViewCell = UITableViewCell(style: UITableViewCell.CellStyle.default, reuseIdentifier:"addCategoryCell")
        cell.selectionStyle =  .default
        cell.backgroundColor = UIColor.white
        cell.contentView.backgroundColor = UIColor.clear
        cell.textLabel?.textAlignment = NSTextAlignment.left
        cell.textLabel?.textColor = Theme.getAppBlackColor()
        cell.textLabel?.font = Theme.getRubikMediumFont(size: 14)
        cell.textLabel?.text = placesData[indexPath.row].attributedFullText.string
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        tblViewPlaces.isHidden = true
        btnDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
        self.txtfieldStreetAddress.text = placesData[indexPath.row].attributedPrimaryText.string
        GMSPlacesClient.shared().fetchPlace(fromPlaceID: placesData[indexPath.row].placeID, placeFields: .all, sessionToken: nil) { place, error in
            if let formattedAddress = place?.formattedAddress{
                self.txtfieldHomeAddress.text = "\(formattedAddress)"
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

extension AddMailingAddressViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        if (textField == txtfieldHomeAddress){
            //showAutoCompletePlaces()
            btnSearchTopConstraint.constant = 37
            self.view.layoutSubviews()
            txtfieldHomeAddress.placeholderHorizontalOffset = -24
        }
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        if (textField == txtfieldHomeAddress){
            btnDropDown.setImage(UIImage(named: "textfield-dropdownIcon"), for: .normal)
            if (txtfieldHomeAddress.text == ""){
                txtfieldHomeAddress.placeholderHorizontalOffset = 0
                btnSearchTopConstraint.constant = 34
                self.view.layoutSubviews()
            }
            _ = validateHomeAddress()
            setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldHomeAddress, txtfieldStreetAddress, txtfieldUnitNo, txtfieldCity, txtfieldCounty, txtfieldState, txtfieldZipCode, txtfieldCountry])
        }
    }
    
    func validateHomeAddress() -> Bool {
        do{
            let response = try txtfieldHomeAddress.text?.validate(type: .required)
            DispatchQueue.main.async {
                self.txtfieldHomeAddress.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldHomeAddress.detail = ""
            }
            return response ?? false
        }
        catch{
            self.txtfieldHomeAddress.dividerColor = .red
            self.txtfieldHomeAddress.detail = error.localizedDescription
            return false
        }
    }
}

extension AddMailingAddressViewController: GMSAutocompleteViewControllerDelegate {
    
    // Handle the user's selection.
    func viewController(_ viewController: GMSAutocompleteViewController, didAutocompleteWith place: GMSPlace) {
        if let formattedAddress = place.formattedAddress{
            txtfieldHomeAddress.text = "       \(formattedAddress)"
            txtfieldHomeAddress.placeholder = "Search Mailing Address"
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

extension AddMailingAddressViewController: GMSAutocompleteFetcherDelegate {
    
    func didAutocomplete(with predictions: [GMSAutocompletePrediction]) {
        placesData.removeAll()
        placesData = predictions
        tblViewPlaces.reloadData()
    }
    
    func didFailAutocompleteWithError(_ error: Error) {
        print(error.localizedDescription)
    }
}

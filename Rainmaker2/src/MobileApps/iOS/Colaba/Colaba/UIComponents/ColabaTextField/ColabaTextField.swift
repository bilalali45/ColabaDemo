//
//  ColabaTextField.swift
//  Colaba
//
//  Created by Salman Khan on 31/08/2021.
//

import UIKit
import LocalAuthentication
import Material
import MonthYearPicker
import DropDown

enum PrefixType: String {
    case amount     = "$  |  "
    case percentage = "%  |  "
}

enum ColabaTextFieldType {
    case password
    case dropdown
    case editableDropdown
    case datePicker
    case monthlyDatePicker
    case delete
    case amount
    case percentage
    case defaultType
}

protocol ColabaTextFieldDelegate {
    func deleteButtonClicked()
    func selectedOption(option: String, atIndex : Int, textField: ColabaTextField)
    func selectedDate(date:Date)
    func textFieldDidChange(_ textField : ColabaTextField)
    func textFieldEndEditing(_ textField : ColabaTextField)
    func dismiss()
}

extension ColabaTextFieldDelegate {
    func deleteButtonClicked(){}
    func selectedOption(option: String, atIndex : Int, textField: ColabaTextField) {}
    func selectedDate(date:Date){}
    func biometricClicked(){}
    func passwordClicked(){}
    func textFieldDidChange(_ textField : ColabaTextField) {}
    func textFieldEndEditing(_ textField : ColabaTextField) {}
    func dismiss(){}
}

class ColabaTextField: TextField {
    
    //MARK: Outlets and Private Properties
    private var button: UIButton!
    private var imagePicker: UIImagePickerController?
    private var maximumDate:Date?
    private var minimumDate:Date?
    private var isValidateOnEndEditing: Bool!
    private var validationType: ValidationType!
    private var dropDown: DropDown!
    private var dropDownDataSource: [String] = []
    
    //MARK: Public Properties
    public var colabaDelegate: ColabaTextFieldDelegate?
    
    private var prefix: String?
    private var attributedPrefix: NSMutableAttributedString? // "$│ "
    private var fieldMinLength = 0
    private var fieldMaxLength = 20
    private var allowedCharacters : CharacterSet = .alphanumerics
    internal var regex: String?
    
    
    public var type: ColabaTextFieldType = .defaultType {
        didSet {
            switch type {
            case .password:
                self.isButtonHidden(false)
                isSecureText(true)
                toggleButtonImage()
            case .dropdown:
                isButtonHidden(false)
                self.isUserInteractionEnabled = true
                self.setButton(image: UIImage(named: "textfield-dropdownIcon")!)
                setDropDown()
            case .editableDropdown:
                isButtonHidden(false)
                self.isUserInteractionEnabled = true
                self.setButton(image: UIImage(named: "textfield-dropdownIcon")!)
                setDropDown()
            case .delete:
                button.contentHorizontalAlignment = .center
                self.setButton(image: UIImage(named: "DeleteDependent"))
            case .amount:
                prefix = PrefixType.amount.rawValue
                attributedPrefix = createAttributedPrefix(prefix: prefix!)
                setTextField(keyboardType: .numberPad)
            case .percentage:
                prefix = PrefixType.percentage.rawValue
                attributedPrefix = createAttributedPrefix(prefix: prefix!)
                setTextField(keyboardType: .numberPad)
            case .datePicker:
                isButtonHidden(false)
                self.isUserInteractionEnabled = true
                self.tintColor = .clear
                setButton(image: UIImage(named: "CalendarIcon")!)
            case .monthlyDatePicker:
                isButtonHidden(false)
                self.isUserInteractionEnabled = true
                self.tintColor = .clear
                setButton(image: UIImage(named: "CalendarIcon")!)
            case .defaultType:
                self.setButton(image: nil)
            }
        }
    }
    
    //MARK: Initializers
    
    public override init(frame: CGRect) {
        super.init(frame: frame)
        self.setupView()
    }
    
    //initWithCode to init view from xib or storyboard
    public required init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
        self.setupView()
    }
    
    private func setupView() {
        
        let tap = UITapGestureRecognizer(target: self, action: #selector(self.handleTap(_:)))
        self.addGestureRecognizer(tap)
        setTextField(textColor: Theme.getAppBlackColor())
        setTextField(font: Theme.getRubikRegularFont(size: 15))
        setTextField(dividerActiveColor: Theme.getButtonBlueColor())
        setTextField(dividerColor: Theme.getSeparatorNormalColor())
        setTextField(dividerNormalHeight: 1.0)
        setTextField(dividerThickness: 1.0)
        setTextField(placeholderActiveColor: Theme.getAppGreyColor())
        setTextField(placeholderLabelColor: Theme.getButtonGreyTextColor())
        setTextField(placeholderVerticalOffset: 8)
        setTextField(detailLabelFont: Theme.getRubikRegularFont(size: 12))
        setTextField(detailLabelColor: .red)
        setTextField(detailLabelVerticalOffset: 4)
        setIsValidateOnEndEditing(validate: true)
        self.addTarget(self, action: #selector(textFieldDidChange(_:)), for: .editingChanged)
        self.backgroundColor = .clear
        addButton()
    }
    
    func addButton() {
        
        button = UIButton()
        button.contentHorizontalAlignment = .trailing
        button.addTarget(self, action: #selector(colabaTextFieldButtonClicked(_:)), for: .touchUpInside)
        self.addSubview(button)
        
        button.translatesAutoresizingMaskIntoConstraints = false
        let horizontalConstraint = button.trailingAnchor.constraint(greaterThanOrEqualTo: self.trailingAnchor, constant: -2)
        let verticalConstraint = button.topAnchor.constraint(greaterThanOrEqualTo: self.topAnchor, constant: 5)
//        let widthConstraint = button.widthAnchor.constraint(equalToConstant: 32)
//        let heightConstraint = button.heightAnchor.constraint(equalToConstant: 32)
        self.addConstraints([horizontalConstraint, verticalConstraint])
        isButtonHidden(true)
    }
    
    @objc func handleTap(_ sender: UITapGestureRecognizer? = nil) {
        if type == .datePicker {
            if self.isUserInteractionEnabled {
                datePickerButtonClicked()
            }
            return
        }
        if type == .monthlyDatePicker {
            if self.isUserInteractionEnabled {
                monthlyDatePickerButtonClicked()
            }
            return
        }
        if type == .dropdown {
            dropDownButtonClicked()
            return
        }
        _ = self.becomeFirstResponder()
    }
    
    //MARK: Button Action
    @objc func colabaTextFieldButtonClicked(_ sender: UIButton) {
        switch type {
        case .password:
            self.isSecureTextEntry = !self.isSecureTextEntry
            toggleButtonImage()
            colabaDelegate?.passwordClicked()
        case .dropdown:
            dropDownButtonClicked()
        case .editableDropdown:
            print("No any action on editable dropdown")
        case .datePicker:
            datePickerButtonClicked()
        case .monthlyDatePicker:
            monthlyDatePickerButtonClicked()
        case .delete:
            colabaDelegate?.deleteButtonClicked()
        case .amount:
            print("button not available in amount type")
        case .percentage:
            print("button not available in percentage type")
        case .defaultType:
            print("button not available in default type")
        }
    }
    
    private func dropDownButtonClicked() {
        setButton(image: UIImage(named: "textfield-dropdownIconUp"))
        dropDown.show()
    }
    
    private func datePickerButtonClicked() {
        setDatePicker()
    }
    
    private func monthlyDatePickerButtonClicked() {
        setMonthlyDatePicker()
    }
    
    private func setDropDown() {
        dropDown = DropDown()
        dropDown.anchorView = self
        setDropDownDirection()
        setDropDownDismissMode()
        dropDown.cancelAction = .some({ [weak self] in
            self?.setButton(image: UIImage(named: "textfield-dropdownIcon"))
            self?.resignFirstResponder()
        })
        dropDown.selectionAction = { [weak self] (index: Int, item: String) in
            self?.setButton(image: UIImage(named: "textfield-dropdownIcon"))
            self?.dropDown.hide()
            self?.text = item
            self?.resignFirstResponder()
            self?.colabaDelegate?.selectedOption(option: item, atIndex: index, textField: self!)
            _ = self?.validate()
        }
    }
    
    public func setDropDownDismissMode(_ dimsissMode : DropDown.DismissMode = .automatic) {
        dropDown.dismissMode = dimsissMode
    }
    
    public func setDropDownDataSource(_ dataSource : [String]) {
        dropDown.dataSource = dataSource
        dropDownDataSource = dataSource // Datasource in case we are filtering datasource
    }
    public func setDropDownDirection(_ direction : DropDown.Direction = .any) {
        if direction == .top {
            dropDown.topOffset = CGPoint(x: 0, y:-(dropDown.anchorView?.plainView.bounds.height)!)
        } else {
            dropDown.bottomOffset = CGPoint(x: 0, y:(dropDown.anchorView?.plainView.bounds.height)!)
        }
    }
}

//MARK: Public functions
extension ColabaTextField {
    
    //MARK: Textfield
    public func setTextField(placeholder: String, controller: UIViewController, validationType: ValidationType, keyboardType: UIKeyboardType = .asciiCapable) {
        setTextField(placeholder: placeholder)
        setDelegates(controller: controller)
        setValidation(validationType: validationType)
        setTextField(keyboardType: keyboardType)
    }
    
    public func setTextField(textColor: UIColor) {
        self.tintColor = textColor
        self.textColor = textColor
    }
    
    public func setTextField(placeholder: String) {
        self.placeholder = placeholder
    }
    
    public func setTextField(keyboardType: UIKeyboardType) {
        self.keyboardType = keyboardType
    }
    
    public func setTextField(font: UIFont) {
        self.font = font
    }
    
    public func setTextField(dividerActiveColor: UIColor) {
        self.dividerActiveColor = dividerActiveColor
    }
    
    public func setTextField(dividerColor: UIColor) {
        self.dividerColor = dividerColor
    }
    
    public func setTextField(dividerNormalHeight : CGFloat) {
        self.dividerNormalHeight = dividerNormalHeight
    }
    
    public func setTextField(dividerThickness : CGFloat) {
        self.dividerThickness = dividerThickness
    }
    
    public func setTextField(placeholderActiveColor: UIColor) {
        self.placeholderActiveColor = placeholderActiveColor
    }
    
    public func setTextField(placeholderLabelColor: UIColor) {
        self.placeholderLabel.textColor = placeholderLabelColor
    }
    
    public func setTextField(placeholderVerticalOffset: CGFloat) {
        self.placeholderVerticalOffset = placeholderVerticalOffset
    }
    
    public func setTextField(detail: String) {
        self.detail = detail
    }
    
    public func setTextField(detailLabelFont: UIFont) {
        self.detailLabel.font = detailLabelFont
    }
    
    public func setTextField(detailLabelColor: UIColor) {
        self.detailColor = detailLabelColor
    }
    
    public func setTextField(detailLabelVerticalOffset: CGFloat) {
        self.detailVerticalOffset = detailLabelVerticalOffset
    }
    
    public func setTextField(minLength: Int) {
        fieldMinLength = minLength
    }
    
    public func setTextField(maxLength: Int) {
        fieldMaxLength = maxLength
    }
    
    public func setTextField(acceptableCharacters: CharacterSet = .alphanumerics) {
        self.allowedCharacters = acceptableCharacters
    }
    
    public func setTextField(text: String){
        if (type == .amount){
            prefix = PrefixType.amount.rawValue
            attributedPrefix = createAttributedPrefix(prefix: prefix!)
            self.attributedText = NSAttributedString(string: "\(attributedPrefix!.string)\(text)")
            if let amount = Int(cleanString(string: self.attributedText!.string, replaceCharacters: [prefix!,",", " "], replaceWith: "")){
                //With Commas function create String like $10,000.00, now remove $ and 0.00 from string and return to field
                let amountWithComma = cleanString(string: amount.withCommas(), replaceCharacters: ["$",".00"], replaceWith: "")
                self.attributedText = createAttributedTextWithPrefix(prefix: prefix!, string: amountWithComma)
            }
        }
        else if (type == .percentage){
            prefix = PrefixType.percentage.rawValue
            attributedPrefix = createAttributedPrefix(prefix: prefix!)
            self.attributedText = NSAttributedString(string: "\(attributedPrefix!.string)\(text)")
            let string = Int(cleanString(string: self.attributedText!.string, replaceCharacters: [prefix!], replaceWith: ""))?.description
            self.attributedText = createAttributedTextWithPrefix(prefix: prefix!, string: string ?? "0" )
        }
        else{
            self.text = text
        }
        if (text == ""){
            self.placeholderLabel.textColor = Theme.getAppGreyColor()
        }
    }
    
    public func isSecureText(_ secure: Bool = false) {
        self.isSecureTextEntry = secure
    }
    
    public func setValidation(validationType : ValidationType) {
        self.validationType = validationType
    }
    
    public func setIsValidateOnEndEditing(validate: Bool) {
        isValidateOnEndEditing = validate
    }
    
    public func getTextField() -> TextField {
        return self
    }
    
    public func validate() -> Bool {
        do {
            let response = try self.text?.validate(type: validationType)
            DispatchQueue.main.async {
                self.setTextField(dividerColor: Theme.getSeparatorNormalColor())
                self.setTextField(detail: "")
            }
            return response ?? false
        }
        catch {
            self.setTextField(dividerColor: .red)
            self.setTextField(detail: error.localizedDescription)
            return false
        }
    }
    
    public override func becomeFirstResponder() -> Bool {
        super.becomeFirstResponder()
    }
    
    public func setTag(tag: Int) {
        self.tag = tag
    }
    
    //MARK: Button
    public func setButton(_ title : String?) {
        button.setTitle(title, for: .normal)
    }
    
    public func setButton(image: UIImage?) {
        button.setImage(image, for: .normal)
    }
    
    private func toggleButtonTitle() {
        setButton(self.isSecureTextEntry ? "Show" : "Hide")
    }
    
    private func toggleButtonImage() {
        setButton(image: UIImage(named: self.isSecureTextEntry ? "eyeIcon" : "hide"))
    }
    
    public func isButtonHidden(_ hidden: Bool = true) {
        self.textInsets = UIEdgeInsets(top: 0, left: 0, bottom: 0, right: 36)
        button.isHidden = hidden
    }
    
    public func setMaxDate(date:Date) {
        self.maximumDate = date
    }
    
    public func setMinDate(date:Date) {
        self.minimumDate = date
    }
    
    public func setMaxDate(string: String) {
        let dateFormater = DateFormatter()
        dateFormater.dateStyle = .medium
        dateFormater.dateFormat = "MM/yyyy"
        dateFormater.timeZone = TimeZone(abbreviation: "UTC")
        self.maximumDate = dateFormater.date(from: string)
    }
    
    public func setMinDate(string: String) {
        let dateFormater = DateFormatter()
        dateFormater.dateStyle = .medium
        dateFormater.dateFormat = "MM/yyyy"
        dateFormater.timeZone = TimeZone(abbreviation: "UTC")
        self.minimumDate = dateFormater.date(from: string)
    }
}

//MARK: Delegates
extension ColabaTextField: UITextFieldDelegate {
    public func setDelegates(controller: UIViewController) {
        self.delegate = self
        self.colabaDelegate = controller as? ColabaTextFieldDelegate
    }
    
    public func setDelegates(collectionViewCell: UICollectionViewCell) {
        self.delegate = self
        self.colabaDelegate = collectionViewCell as? ColabaTextFieldDelegate
    }
    
    public func textField(_ textField: UITextField, shouldChangeCharactersIn range: NSRange, replacementString string: String) -> Bool {
        
        if validationType == .phoneNumber {
            guard let text = textField.text else { return false }
            let newString = (text as NSString).replacingCharacters(in: range, with: string)
            textField.text = formatNumber(with: "(XXX) XXX-XXXX", number: newString)
            return false
        }
        if validationType == .socialSecurityNumber {
            guard let text = textField.text else { return false }
            let newString = (text as NSString).replacingCharacters(in: range, with: string)
            textField.text = formatNumber(with: "XXX-XX-XXXX", number: newString)
            return false

        }
        if validationType == .verificationCode {
            if (string == "" || textField.text!.count < 6){
                return true
            }
            else{
                return false
            }
        }
        if type == .amount {
            return (self.text == prefix && range.location == prefix!.count - 1 && range.length == 1) ? false : true
        }
        if type == .percentage {
            return (self.text == prefix && range.location == prefix!.count - 1 && range.length == 1) ? false : true
        }
        return true
    }
    
    //MARK: Textfield Did Change
    @objc func textFieldDidChange(_ textField: UITextField) {
        if type == .amount {
            //First Remove , from Amount like 10,000 to 10000 and convert to integer
            if let amount = Int(cleanString(string: self.attributedText!.string, replaceCharacters: [prefix!,",", " "], replaceWith: "")){
                //With Commas function create String like $10,000.00, now remove $ and 0.00 from string and return to field
                let amountWithComma = cleanString(string: amount.withCommas(), replaceCharacters: ["$",".00"], replaceWith: "")
                self.attributedText = createAttributedTextWithPrefix(prefix: prefix!, string: amountWithComma)
            }
        }
        if type == .percentage {
            let string = Int(cleanString(string: self.attributedText!.string, replaceCharacters: [prefix!], replaceWith: ""))?.description
            self.attributedText = createAttributedTextWithPrefix(prefix: prefix!, string: string ?? "0" )
        }
        if type == .editableDropdown {
            if textField.text == "" {
                dropDown.dataSource = dropDownDataSource
                dropDownButtonClicked()
            } else {
                let filteredDataSource = dropDownDataSource.filter{$0.localizedCaseInsensitiveContains(self.text!)}
                dropDown.dataSource = filteredDataSource
                dropDownButtonClicked()
            }
        }
        colabaDelegate?.textFieldDidChange(self)
    }
    
    //To hide error text when textField begin editing
    public func textFieldDidBeginEditing(_ textField: UITextField) {
        if type == .delete {
            isButtonHidden(false)
        }
        if type == .amount && !attributedText!.string.contains("$  |  ") {
            self.attributedText = attributedPrefix
        }
        if type == .percentage && !attributedText!.string.contains("%  |  ") {
            self.attributedText = attributedPrefix
        }
        if type == .datePicker {
            setDatePicker()
            if let datePicker = self.inputView as? UIDatePicker {
                self.colabaDelegate?.selectedDate(date: datePicker.date)
                self.text = getFormattedDate(datePicker: datePicker)
            }
        }
        if type == .monthlyDatePicker {
            setMonthlyDatePicker()
            if let datePicker = self.inputView as? MonthYearPickerView {
                self.colabaDelegate?.selectedDate(date: datePicker.date)
                self.text = getMonthFormattedDate(datePicker: datePicker)
            }
        }
        if type == .dropdown {
            dropDownButtonClicked()
        }
    }
    
    public func textFieldDidEndEditing(_ textField: UITextField) {
        if (self.text == ""){
            self.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
        }
        else{
            self.placeholderLabel.textColor = Theme.getAppGreyColor()
        }
        if type == .delete {
            isButtonHidden(true)
        }
        if type == .amount && self.text == prefix {
            self.text = ""
        }
        if type == .percentage && self.text == prefix {
            self.text = ""
        }
        if type == .dropdown {
            setButton(image: UIImage(named: "textfield-dropdownIcon"))
        }
        if type == .editableDropdown {
            if !(dropDownDataSource.contains(self.text!)){
                self.text = ""
                dropDown.hide()
            }
            setButton(image: UIImage(named: "textfield-dropdownIcon"))
        }
        
        if isValidateOnEndEditing {
            _ = validate()
        }
        
        colabaDelegate?.textFieldEndEditing(self)
    }
    
    public func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        textField.resignFirstResponder()
        return true
    }
}

//MARK: Date Picker
extension ColabaTextField {
    
    func setDatePicker(){
        let datePicker = UIDatePicker()
        datePicker.datePickerMode = .date
        if #available(iOS 13.4, *) {
            datePicker.preferredDatePickerStyle = .wheels
        }
        if let maxDate = self.maximumDate{
            datePicker.maximumDate = maxDate
        }
        if let minDate = self.minimumDate{
            datePicker.minimumDate = minDate
        }
        
        datePicker.addTarget(self, action: #selector(datePickerValueChanged(datePicker:)), for: .valueChanged)
        self.inputView = datePicker
        
        //Tool Bar
        let toolbar = UIToolbar(frame: CGRect(x: 0, y: 0, width: self.frame.width, height: 44))
        let cancelButton = UIBarButtonItem(title: "Cancel", style: .plain, target: self, action: #selector(cancelDatePicker))
        let spacer = UIBarButtonItem(barButtonSystemItem: .flexibleSpace, target: nil, action: nil)
        let doneButton = UIBarButtonItem(title: "Done", style: .done, target: self, action: #selector(doneDatePicker))
        toolbar.items = [cancelButton,spacer, doneButton]
        self.isUserInteractionEnabled = true
        self.inputAccessoryView = toolbar
        if !self.isFirstResponder {
            _ = self.becomeFirstResponder()
        }
    }
    
    func setMonthlyDatePicker() {
        
        let datePicker = MonthYearPickerView(frame: CGRect(x: 0, y: 0, width: self.frame.width, height: 216))
        datePicker.addTarget(self, action: #selector(monthlyDatePickerValueChanged(datePicker:)), for: .valueChanged)
        self.inputView = datePicker
        
        if let maxDate = self.maximumDate{
            datePicker.maximumDate = maxDate
        }
        if let minDate = self.minimumDate{
            datePicker.minimumDate = minDate
        }
        
        //Tool Bar
        let toolbar = UIToolbar(frame: CGRect(x: 0, y: 0, width: self.frame.width, height: 44))
        let cancelButton = UIBarButtonItem(title: "Cancel", style: .plain, target: self, action: #selector(cancelDatePicker))
        let spacer = UIBarButtonItem(barButtonSystemItem: .flexibleSpace, target: nil, action: nil)
        let doneButton = UIBarButtonItem(title: "Done", style: .done, target: self, action: #selector(doneMonthlyDatePicker))
        toolbar.items = [cancelButton,spacer, doneButton]
        self.isUserInteractionEnabled = true
        self.inputAccessoryView = toolbar
        if !self.isFirstResponder {
            _ = self.becomeFirstResponder()
        }
    }

    @objc func cancelDatePicker() {
        self.isUserInteractionEnabled = true
        self.resignFirstResponder()
    }
    
    @objc func doneDatePicker(datePicker: UIDatePicker) {
        if let datePicker = self.inputView as? UIDatePicker{
            self.colabaDelegate?.selectedDate(date: datePicker.date)
            self.text = getFormattedDate(datePicker: datePicker)
        }
        self.resignFirstResponder()
        self.isUserInteractionEnabled = true
    }
    
    @objc func doneMonthlyDatePicker(datePicker: MonthYearPickerView) {
        if let datePicker = self.inputView as? MonthYearPickerView {
            self.colabaDelegate?.selectedDate(date: datePicker.date)
            self.text = getMonthFormattedDate(datePicker: datePicker)
        }
        self.resignFirstResponder()
        self.isUserInteractionEnabled = true
    }
    
    @objc func datePickerValueChanged(datePicker: UIDatePicker) {
        self.text = getFormattedDate(datePicker: datePicker)
        self.colabaDelegate?.selectedDate(date: datePicker.date)
    }
    
    @objc func monthlyDatePickerValueChanged(datePicker: MonthYearPickerView) {
        self.text = getMonthFormattedDate(datePicker: datePicker)
        self.colabaDelegate?.selectedDate(date: datePicker.date)
    }
    
    func getFormattedDate(datePicker: UIDatePicker) -> String {
        let dateFormater = DateFormatter()
        dateFormater.dateStyle = .medium
        dateFormater.dateFormat = "MM/dd/yyyy"
        return dateFormater.string(from: datePicker.date)
    }
    
    func getMonthFormattedDate(datePicker: MonthYearPickerView) -> String {
        let dateFormater = DateFormatter()
        dateFormater.dateStyle = .medium
        dateFormater.dateFormat = "MM/yyyy"
        return dateFormater.string(from: datePicker.date)
    }
}

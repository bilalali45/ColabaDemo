//
//  ColabaTextField.swift
//  Colaba
//
//  Created by Salman Khan on 31/08/2021.
//

import UIKit
import LocalAuthentication
import Material

enum ColabaTextFieldButtonType {
    case password
    case dropdown
    case datePicker
    case none
}

protocol ColabaTextFieldDelegate {
    func dropDownClicked(alert: UIAlertController)
    func dropDownClicked(alert: UIAlertController, withTag: Int)
    func selectedOption(option: String, alert: UIAlertController)
    func selectedDate(date:Date)
    func textFieldEndEditing(_ textField : TextField)
    func dismiss()
}

extension ColabaTextFieldDelegate {
    func dropDownClicked(alert: UIAlertController){}
    func dropDownClicked(alert: UIAlertController, withTag: Int){}
    func selectedOption(option: String, alert: UIAlertController){}
    func selectedDate(date:Date){}
    func biometricClicked(){}
    func passwordClicked(){}
    func textFieldEndEditing(_ textField : UITextField) {}
    func dismiss(){}
}

class ColabaTextField: TextField {
    
    //MARK: Outlets and Private Properties
//    @IBOutlet private weak var textField: TextField!
    private var button: UIButton!
    
    private var selectionList : [String]?
    private var alert: UIAlertController?
    private var imagePicker: UIImagePickerController?
    private var maximumDate:Date?
    private var minimumDate:Date?
    private var validationType: ValidationType!
    
    //MARK: Public Properties
    public var colabaDelegate: ColabaTextFieldDelegate?
    
    private var fieldMinLength = 0
    private var fieldMaxLength = 20
    private var selectedCountryCallingCode = ""
    private var allowedCharacters : CharacterSet = .alphanumerics
    internal var regex: String?
    
    public var type: ColabaTextFieldButtonType = .none {
        didSet {
            switch type {
            case .password:
                self.isButtonHidden(false)
                isSecureText(true)
                toggleButtonImage()
            case .dropdown:
                isButtonHidden(false)
                self.isUserInteractionEnabled = false
                self.setButton(image: UIImage(named: "dropdown")!)
            case .datePicker:
                isButtonHidden(false)
                self.isUserInteractionEnabled = false
                self.tintColor = .clear
                setButton(image: UIImage(named: "calender")!)
            case .none:
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
        self.backgroundColor = .clear
        addButton()
    }
    
    func addButton() {

        button = UIButton()
        button.contentHorizontalAlignment = .trailing
        button.addTarget(self, action: #selector(colabaTextFieldButtonClicked(_:)), for: .touchUpInside)
        self.addSubview(button)
        
        button.translatesAutoresizingMaskIntoConstraints = false
        let horizontalConstraint = button.trailingAnchor.constraint(equalTo: self.trailingAnchor)
        let verticalConstraint = button.centerYAnchor.constraint(equalTo: self.centerYAnchor)
        let widthConstraint = button.widthAnchor.constraint(equalToConstant: 32)
        let heightConstraint = button.heightAnchor.constraint(equalToConstant: 32)
        self.addConstraints([horizontalConstraint, verticalConstraint, widthConstraint, heightConstraint])
        button.isHidden = true
    }
    
    @objc func testClick() {
        print("Test Click")
    }
        
    @objc func handleTap(_ sender: UITapGestureRecognizer? = nil) {
        self.endEditing(true)
        if type == .datePicker {
            if self.isUserInteractionEnabled {
                datePickerButtonClicked()
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
        self.endEditing(true)
        switch type {
        case .password:
            self.isSecureTextEntry = !self.isSecureTextEntry
            toggleButtonImage()
            colabaDelegate?.passwordClicked()
        case .dropdown:
            dropDownButtonClicked()
        case .datePicker:
            datePickerButtonClicked()
        case .none:
            print("None")
        }
    }
    
    private func dropDownButtonClicked() {
        colabaDelegate?.dropDownClicked(alert: alert ?? UIAlertController(title: "", message: "", preferredStyle: .actionSheet), withTag: self.tag)
    }
    
    private func datePickerButtonClicked() {
        setDatePicker()
    }
}



//MARK: Public Functions -- ColabaTextField Values
extension ColabaTextField {

    
    public func setUserInteractionDisabled(){
//        self.self.isUserInteractionEnabled = false
//        self.button.isHidden = true
//        setTextField( textColor: UIColor.Palette.formLabel)
    }
    
    public func setDatePickerDisabled() {
        self.isUserInteractionEnabled = false
        button.isHidden = false
        button.isUserInteractionEnabled = false
//        setTextField( textColor: UIColor.Palette.formLabel)
    }
}

//MARK: Public functions
extension ColabaTextField {
    
    //MARK: Textfield
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
    
    public func isSecureText(_ secure: Bool = false) {
        self.isSecureTextEntry = secure
    }
    
    public func setValidation(validationType : ValidationType) {
        self.validationType = validationType
    }
    
    public func getTextField() -> TextField{
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
        catch{
            self.setTextField(dividerColor: .red)
            self.setTextField(detail: error.localizedDescription)
            return false
        }
    }
    
    public override func becomeFirstResponder() -> Bool {
        super.becomeFirstResponder()
    }
    
    public func setTag(tag: Int){
        self.tag = tag
    }
    
    public func setDropDown(_ title: String, _ list: [String], showText: Bool = true) {
        alert = UIAlertController(title: title, message: "", preferredStyle: .actionSheet)
        for actionTitle in list {
            
            let action = UIAlertAction(title: actionTitle, style: .default) { [weak self] _ in
                if showText {
                    self?.text = actionTitle
                }
                self?.colabaDelegate?.selectedOption(option: actionTitle, alert: (self?.alert!)!)
            }
            
            alert?.addAction(action)
        }
        let cancel = UIAlertAction(title: "Cancel", style: .cancel) { [weak self] _ in
            self?.colabaDelegate?.selectedOption(option: "cancelled", alert: (self?.alert!)!)
        }
        alert?.addAction(cancel)
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
    
    public func setMaxDate(date:Date){
        self.maximumDate = date
    }
    
    public func setMinDate(date:Date){
        self.minimumDate = date
    }
}

//MARK: Delegates
extension ColabaTextField: UITextFieldDelegate {
    public func setDelegates(controller: UIViewController) {
        self.self.delegate = self
        self.colabaDelegate = controller as? ColabaTextFieldDelegate
    }

    public func textField(_ textField: UITextField, shouldChangeCharactersIn range: NSRange, replacementString string: String) -> Bool {
        return true
//        return InputValidation.getAcceptablecharacters(type: allowedCharacters, inputString: string, range: range, maxLenght: fieldMaxLength) ? true : false
    }

    //To hide error text when textField begin editing
    public func textFieldDidBeginEditing(_ textField: UITextField) {
        
    }
    
    public func textFieldDidEndEditing(_ textField: UITextField) {
        if (self.text == ""){
            self.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
        }
        else{
            self.placeholderLabel.textColor = Theme.getAppGreyColor()
        }
        colabaDelegate?.textFieldEndEditing(textField)
    }
    
    public func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        textField.resignFirstResponder()
        return true
    }
}

//MARK: Date Picker
extension ColabaTextField{
    
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
        self.becomeFirstResponder()
    }
    
    @objc func cancelDatePicker(){
        self.isUserInteractionEnabled = false
        self.text = nil
    }
    
    @objc func doneDatePicker(datePicker: UIDatePicker){
        if let datePicker = self.inputView as? UIDatePicker{
            self.colabaDelegate?.selectedDate(date: datePicker.date)
            self.text = getFormattedDate(datePicker: datePicker)
        }
        self.isUserInteractionEnabled = false
    }
    
    @objc func datePickerValueChanged(datePicker: UIDatePicker){
        self.text = getFormattedDate(datePicker: datePicker)
        self.colabaDelegate?.selectedDate(date: datePicker.date)
    }
    
    func getFormattedDate(datePicker: UIDatePicker) -> String{
        let dateFormater = DateFormatter()
        dateFormater.dateStyle = .medium
        dateFormater.dateFormat = "MM/dd/yyyy"
        return dateFormater.string(from: datePicker.date)
    }
}

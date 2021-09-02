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
    case biometric
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

class ColabaTextField: BaseView {
    
    //MARK: Outlets and Private Properties
    @IBOutlet private weak var textField: TextField!
    @IBOutlet private weak var button: UIButton!
    
    private var selectionList : [String]?
    private var alert: UIAlertController?
    private var imagePicker: UIImagePickerController?
    private var maximumDate:Date?
    private var minimumDate:Date?
    
    //MARK: Public Properties
    public var delegate: ColabaTextFieldDelegate?
    
    private var fieldMinLength = 0
    private var fieldMaxLength = 20
    private var selectedCountryCallingCode = ""
    private var allowedCharacters : CharacterSet = .alphanumerics
    internal var regex: String?
    
    public var type: ColabaTextFieldButtonType = .none {
        didSet {
            switch type {
            case .biometric:
                self.isButtonHidden(false)
                isSecureText(false)
                
//                let biometric = BiometricAuth()
//                biometric.canEvaluate { canEvaluate, _, _ in
//                    guard canEvaluate else {return}
//                }
//                if Defaults.isBiometricEnabled ?? false{
//                    let imageIcon = LAContext().biometryType == .faceID ? "faceid" : "thumb"
//                    self.setButton(image: UIImage(named: imageIcon)!)
//                }else{
//                    self.setButton(image: nil)
//                }
            case .password:
                isSecureText(true)
                toggleButtonImage()
            case .dropdown:
                self.isButtonHidden(false)
                self.textField.isUserInteractionEnabled = false
                self.setButton(image: UIImage(named: "dropdown")!)
            case .datePicker:
                self.isButtonHidden(false)
                self.textField.isUserInteractionEnabled = false
                self.textField.tintColor = .clear
                self.setButton(image: UIImage(named: "calender")!)
                
            case .none:
                self.setButton(image: nil)
                print("None")
            }
        }
    }
    
    public var text: String? {
        set {
            textField.text = newValue
        }
        get {
            return textField.text
        }
    }
    
    //MARK: Initializers
    
    public override init(frame: CGRect) {
        super.init(frame: frame)
        setupFromNib()
    }
    
    //initWithCode to init view from xib or storyboard
    public required init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
        setupFromNib()
    }

    public override func awakeFromNib() {
        super.awakeFromNib()
        self.setupView()
    }
    
    func setupFromNib() {
        super.nibName = String(describing: Self.self)
        super.setupFromNib()
    }

    private func setupView() {
        
        let tap = UITapGestureRecognizer(target: self, action: #selector(self.handleTap(_:)))
        self.addGestureRecognizer(tap)
        
        setTextField(textColor: Theme.getAppBlackColor())
        setTextField(font: Theme.getRubikRegularFont(size: 15))
        setTextField(dividerActiveColor: Theme.getButtonBlueColor())
        setTextField(dividerColor: Theme.getSeparatorNormalColor())
        setTextField(placeholderActiveColor: Theme.getAppGreyColor())
        setTextField(placeholderLabelColor: Theme.getButtonGreyTextColor())
        setTextField(placeholderVerticalOffset: 8)
        setTextField(detailLabelFont: Theme.getRubikRegularFont(size: 12))
        setTextField(detailLabelColor: .red)
        setTextField(detailLabelVerticalOffset: 4)
        
    }
        
    @objc func handleTap(_ sender: UITapGestureRecognizer? = nil) {
        self.endEditing(true)
        if type == .datePicker {
            if textField.isUserInteractionEnabled {
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
    @IBAction func ColabaTextFieldButtonClicked(_ sender: UIButton) {
        self.endEditing(true)
        switch type {
        case .biometric:
            delegate?.biometricClicked()
        case .password:
            textField.isSecureTextEntry = !textField.isSecureTextEntry
            toggleButtonImage()
            delegate?.passwordClicked()
        case .dropdown:
            dropDownButtonClicked()
        case .datePicker:
            datePickerButtonClicked()
        case .none:
            print("None")
        }
    }
    
    private func dropDownButtonClicked() {
        delegate?.dropDownClicked(alert: alert ?? UIAlertController(title: "", message: "", preferredStyle: .actionSheet), withTag: self.tag)
    }
    
    private func datePickerButtonClicked() {
        setDatePicker()
    }
}



//MARK: Public Functions -- ColabaTextField Values
extension ColabaTextField {

    
    public func setUserInteractionDisabled(){
//        self.textField.isUserInteractionEnabled = false
//        self.button.isHidden = true
//        setTextField( textColor: UIColor.Palette.formLabel)
    }
    
    public func setDatePickerDisabled() {
        self.textField.isUserInteractionEnabled = false
        self.button.isHidden = false
        self.button.isUserInteractionEnabled = false
//        setTextField( textColor: UIColor.Palette.formLabel)
    }
}

//MARK: Public functions
extension ColabaTextField {
    
    //MARK: Textfield
    public func setTextField(textColor: UIColor) {
        textField.tintColor = textColor
        textField.textColor = textColor
    }
    
    public func setTextField(keyboardType: UIKeyboardType) {
        textField.keyboardType = keyboardType
    }
    
    public func setTextField(font: UIFont) {
        textField.font = font
    }
    
    public func setTextField(dividerActiveColor: UIColor) {
        textField.dividerActiveColor = dividerActiveColor
    }
    
    public func setTextField(dividerColor: UIColor) {
        textField.dividerColor = dividerColor
    }
    
    public func setTextField(placeholderActiveColor: UIColor) {
        textField.placeholderActiveColor = placeholderActiveColor
    }
    
    public func setTextField(placeholderLabelColor: UIColor) {
        textField.placeholderLabel.textColor = placeholderLabelColor
    }
    
    public func setTextField(placeholderVerticalOffset: CGFloat) {
        textField.detailVerticalOffset = placeholderVerticalOffset
    }
    
    public func setTextField(detailLabelFont: UIFont) {
        textField.detailLabel.font = detailLabelFont
    }
    
    public func setTextField(detailLabelColor: UIColor) {
        textField.detailColor = detailLabelColor
    }
    
    public func setTextField(detailLabelVerticalOffset: CGFloat) {
        textField.detailVerticalOffset = detailLabelVerticalOffset
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
        textField.isSecureTextEntry = secure
    }
    
    public func getTextField() -> UITextField{
        self.textField
    }
    
    public override func becomeFirstResponder() -> Bool {
        textField.becomeFirstResponder()
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
                self?.delegate?.selectedOption(option: actionTitle, alert: (self?.alert!)!)
            }
            
            alert?.addAction(action)
        }
        let cancel = UIAlertAction(title: "Cancel", style: .cancel) { [weak self] _ in
            self?.delegate?.selectedOption(option: "cancelled", alert: (self?.alert!)!)
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
        if textField.isSecureTextEntry {
            setButton("Show")
        } else {
            setButton("Hide")
        }
    }
    private func toggleButtonImage() {
        if textField.isSecureTextEntry{
            setButton(image: UIImage(named: "hide"))
        } else {
            setButton(image: UIImage(named: "show"))
        }
    }
    
    public func isButtonHidden(_ hidden: Bool = true) {
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
        self.textField.delegate = self
        self.delegate = controller as? ColabaTextFieldDelegate
    }

    public func textField(_ textField: UITextField, shouldChangeCharactersIn range: NSRange, replacementString string: String) -> Bool {
        return true
//        return InputValidation.getAcceptablecharacters(type: allowedCharacters, inputString: string, range: range, maxLenght: fieldMaxLength) ? true : false
    }

    //To hide error text when textField begin editing
    public func textFieldDidBeginEditing(_ textField: UITextField) {
        
    }
    
    public func textFieldDidEndEditing(_ textField: UITextField) {
        if (self.textField.text == ""){
            self.textField.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
        }
        else{
            self.textField.placeholderLabel.textColor = Theme.getAppGreyColor()
        }
        delegate?.textFieldEndEditing(textField)
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
        textField.inputView = datePicker
        
        //Tool Bar
        let toolbar = UIToolbar(frame: CGRect(x: 0, y: 0, width: self.frame.width, height: 44))
        let cancelButton = UIBarButtonItem(title: "Cancel", style: .plain, target: self, action: #selector(cancelDatePicker))
        let spacer = UIBarButtonItem(barButtonSystemItem: .flexibleSpace, target: nil, action: nil)
        let doneButton = UIBarButtonItem(title: "Done", style: .done, target: self, action: #selector(doneDatePicker))
        toolbar.items = [cancelButton,spacer, doneButton]
        self.textField.isUserInteractionEnabled = true
        textField.inputAccessoryView = toolbar
        textField.becomeFirstResponder()
    }
    
    @objc func cancelDatePicker(){
        self.textField.isUserInteractionEnabled = false
        textField.text = nil
    }
    
    @objc func doneDatePicker(datePicker: UIDatePicker){
        if let datePicker = textField.inputView as? UIDatePicker{
            self.delegate?.selectedDate(date: datePicker.date)
            self.text = getFormattedDate(datePicker: datePicker)
        }
        self.textField.isUserInteractionEnabled = false
    }
    
    @objc func datePickerValueChanged(datePicker: UIDatePicker){
        self.text = getFormattedDate(datePicker: datePicker)
        self.delegate?.selectedDate(date: datePicker.date)
    }
    
    func getFormattedDate(datePicker: UIDatePicker) -> String{
        let dateFormater = DateFormatter()
        dateFormater.dateStyle = .medium
        dateFormater.dateFormat = "MM/dd/yyyy"
        return dateFormater.string(from: datePicker.date)
    }
}

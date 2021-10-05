//
//  CustomDocumentViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 05/10/2021.
//

import UIKit
import MaterialComponents

class CustomDocumentViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldDocumentName: ColabaTextField!
    @IBOutlet weak var messageTextViewContainer: UIView!
    @IBOutlet weak var btnNext: ColabaButton!
    
    var txtViewMessage = MDCFilledTextArea()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFieldAndTextView()
        NotificationCenter.default.addObserver(self, selector: #selector(deleteDocumentTapped), name: NSNotification.Name(rawValue: kNotificationDeleteDocument), object: nil)
    }

    //MARK:- Methods and Actions
    
    func setupTextFieldAndTextView(){
        
        txtfieldDocumentName.setTextField(placeholder: "Custom Document Name", controller: self, validationType: .required)
        
        let estimatedFrame = messageTextViewContainer.frame
        txtViewMessage = MDCFilledTextArea(frame: estimatedFrame)
        txtViewMessage.label.text = "Include a message to the borrower"
        txtViewMessage.textView.text = ""
        txtViewMessage.leadingAssistiveLabel.text = ""
        txtViewMessage.setFilledBackgroundColor(.clear, for: .normal)
        txtViewMessage.setFilledBackgroundColor(.clear, for: .disabled)
        txtViewMessage.setFilledBackgroundColor(.clear, for: .editing)
        txtViewMessage.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
        txtViewMessage.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .disabled)
        txtViewMessage.setUnderlineColor(Theme.getButtonBlueColor(), for: .editing)
        txtViewMessage.leadingEdgePaddingOverride = 0
        txtViewMessage.setFloatingLabel(Theme.getAppGreyColor(), for: .normal)
        txtViewMessage.setFloatingLabel(Theme.getAppGreyColor(), for: .disabled)
        txtViewMessage.setFloatingLabel(Theme.getAppGreyColor(), for: .editing)
        txtViewMessage.label.font = Theme.getRubikRegularFont(size: 13)
        txtViewMessage.setNormalLabel(Theme.getButtonGreyTextColor(), for: .normal)
        txtViewMessage.setNormalLabel(Theme.getButtonGreyTextColor(), for: .editing)
        txtViewMessage.setNormalLabel(Theme.getButtonGreyTextColor(), for: .disabled)
        txtViewMessage.setTextColor(Theme.getAppBlackColor(), for: .normal)
        txtViewMessage.setTextColor(Theme.getAppBlackColor(), for: .editing)
        txtViewMessage.setTextColor(Theme.getAppBlackColor(), for: .disabled)
        txtViewMessage.textView.font = Theme.getRubikRegularFont(size: 15)
        txtViewMessage.leadingAssistiveLabel.font = Theme.getRubikRegularFont(size: 12)
        txtViewMessage.setLeadingAssistiveLabel(.red, for: .normal)
        txtViewMessage.setLeadingAssistiveLabel(.red, for: .editing)
        txtViewMessage.setLeadingAssistiveLabel(.red, for: .disabled)
        txtViewMessage.textView.textColor = .black
        txtViewMessage.textView.delegate = self
        mainView.addSubview(txtViewMessage)
        btnNext.setButton(image: UIImage(named: "NextIcon")!)
    }
    
    @objc func deleteDocumentTapped(){
        self.dismissVC()
    }
    
    func validate() -> Bool {
        let isValidate = txtfieldDocumentName.validate()
        return isValidate
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        let vc = Utility.getDeleteDocumentPopupVC()
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnNextTapped(_ sender: UIButton) {
        if validate(){
            self.dismissVC()
        }
    }
}

extension CustomDocumentViewController: UITextViewDelegate{
    
}

//
//  MixPropertyDetailFollowUpViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 02/09/2021.
//

import UIKit
import MaterialComponents

class MixPropertyDetailFollowUpViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var detailTextViewContainer: UIView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    var txtViewDetail = MDCFilledTextArea()
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
        setupMaterialTextView()
    }
    
    //MARK:- Methods and Actions
    
    func setupMaterialTextView(){
        let estimatedFrame = detailTextViewContainer.frame
        txtViewDetail = MDCFilledTextArea(frame: estimatedFrame)
        txtViewDetail.label.text = "Details"
        txtViewDetail.textView.text = ""
        txtViewDetail.leadingAssistiveLabel.text = ""
        txtViewDetail.setFilledBackgroundColor(.clear, for: .normal)
        txtViewDetail.setFilledBackgroundColor(.clear, for: .disabled)
        txtViewDetail.setFilledBackgroundColor(.clear, for: .editing)
        txtViewDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
        txtViewDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .disabled)
        txtViewDetail.setUnderlineColor(Theme.getButtonBlueColor(), for: .editing)
        txtViewDetail.leadingEdgePaddingOverride = 0
        txtViewDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .normal)
        txtViewDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .disabled)
        txtViewDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .editing)
        txtViewDetail.label.font = Theme.getRubikRegularFont(size: 13)
        txtViewDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .normal)
        txtViewDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .editing)
        txtViewDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .disabled)
        txtViewDetail.setTextColor(Theme.getAppBlackColor(), for: .normal)
        txtViewDetail.setTextColor(Theme.getAppBlackColor(), for: .editing)
        txtViewDetail.setTextColor(Theme.getAppBlackColor(), for: .disabled)
        txtViewDetail.textView.font = Theme.getRubikRegularFont(size: 15)
        txtViewDetail.leadingAssistiveLabel.font = Theme.getRubikRegularFont(size: 12)
        txtViewDetail.setLeadingAssistiveLabel(.red, for: .normal)
        txtViewDetail.setLeadingAssistiveLabel(.red, for: .editing)
        txtViewDetail.setLeadingAssistiveLabel(.red, for: .disabled)
        txtViewDetail.textView.textColor = .black
        txtViewDetail.textView.delegate = self
        mainView.addSubview(txtViewDetail)
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        
        do{
            let details = try validation.validatePropertyDetail(txtViewDetail.textView.text)
            DispatchQueue.main.async {
                self.txtViewDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewDetail.leadingAssistiveLabel.text = ""
            }

        }
        catch{
            self.txtViewDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewDetail.leadingAssistiveLabel.text = error.localizedDescription
        }
        
        if (txtViewDetail.textView.text != ""){
            self.dismissVC()
        }
        
    }
    
}

extension MixPropertyDetailFollowUpViewController: UITextViewDelegate{
    
    func textViewDidEndEditing(_ textView: UITextView) {
        
        do{
            let details = try validation.validatePropertyDetail(txtViewDetail.textView.text)
            DispatchQueue.main.async {
                self.txtViewDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewDetail.leadingAssistiveLabel.text = ""
            }

        }
        catch{
            self.txtViewDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewDetail.leadingAssistiveLabel.text = error.localizedDescription
        }
        
    }
    
}

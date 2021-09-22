//
//  BankruptcyFollowupViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 17/09/2021.
//

import UIKit
import MaterialComponents

class BankruptcyFollowupViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var chapter7StackView: UIStackView!
    @IBOutlet weak var btnChapter7: UIButton!
    @IBOutlet weak var lblChapter7: UILabel!
    @IBOutlet weak var chapter11StackView: UIStackView!
    @IBOutlet weak var btnChapter11: UIButton!
    @IBOutlet weak var lblChapter11: UILabel!
    @IBOutlet weak var chapter12StackView: UIStackView!
    @IBOutlet weak var btnChapter12: UIButton!
    @IBOutlet weak var lblChapter12: UILabel!
    @IBOutlet weak var chapter13StackView: UIStackView!
    @IBOutlet weak var btnChapter13: UIButton!
    @IBOutlet weak var lblChapter13: UILabel!
    @IBOutlet weak var txtviewDetailContainerView: UIView!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var txtViewDetail = MDCFilledTextArea()
    var isChapter7 = false
    var isChapter11 = false
    var isChapter12 = false
    var isChapter13 = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextView()
        chapter7StackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(chapter7StackViewTapped)))
        chapter11StackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(chapter11StackViewTapped)))
        chapter12StackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(chapter12StackViewTapped)))
        chapter13StackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(chapter13StackViewTapped)))
    }
    
    //MARK:- Methods and Actions
    
    func setupTextView(){
        let estimatedFrame = txtviewDetailContainerView.frame
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
        //txtViewDetail.textView.delegate = self
        mainView.addSubview(txtViewDetail)
        

    }
    
    @objc func chapter7StackViewTapped(){
        isChapter7 = !isChapter7
        btnChapter7.setImage(UIImage(named: isChapter7 ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblChapter7.font = isChapter7 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    @objc func chapter11StackViewTapped(){
        isChapter11 = !isChapter11
        btnChapter11.setImage(UIImage(named: isChapter11 ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblChapter11.font = isChapter11 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    @objc func chapter12StackViewTapped(){
        isChapter12 = !isChapter12
        btnChapter12.setImage(UIImage(named: isChapter12 ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblChapter12.font = isChapter12 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    @objc func chapter13StackViewTapped(){
        isChapter13 = !isChapter13
        btnChapter13.setImage(UIImage(named: isChapter13 ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblChapter13.font = isChapter13 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        
//        do{
//            let assetDescription = try validation.validateAssetDescription(txtViewDetail.textView.text)
//            DispatchQueue.main.async {
//                self.txtViewDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
//                self.txtViewDetail.leadingAssistiveLabel.text = ""
//            }
//
//        }
//        catch{
//            self.txtViewDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
//            self.txtViewDetail.leadingAssistiveLabel.text = error.localizedDescription
//        }
        
        //if validate(){
            self.dismissVC()
        //}
    }
}

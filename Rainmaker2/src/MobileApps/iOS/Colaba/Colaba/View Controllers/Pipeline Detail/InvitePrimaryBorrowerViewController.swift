//
//  InvitePrimaryBorrowerViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/12/2021.
//

import UIKit

class InvitePrimaryBorrowerViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var emailBodyContainer: UIView!
    @IBOutlet weak var textviewBody: UITextView!
    @IBOutlet weak var btnSendInvite: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
    }
    
    //MARK:- Methods and Actions
    
    func setupViews(){
        emailBodyContainer.layer.cornerRadius = 6
        emailBodyContainer.layer.borderWidth = 1
        emailBodyContainer.layer.borderColor = Theme.getButtonGreyColor().cgColor
        textviewBody.delegate = self
        textviewBody.addHyperLinksToText(originalText: "Hi Richard,\n\nLorem ipsum dolor sit amet, consectetur adipiscing elit. In commodo laoreet lectus sit amet vulputate. Ut malesuada arcu eget porttitor laoreet. In commodo dui et tristique consequat. Aliquam cursus mi nec lectus pretium, quis tincidunt erat consequat. Mauris blandit velit non velit laoreet efficitur. Integer non arcu sodales, rutrum lectus ac, auctor lectus. Integer in hendrerit arcu, at pellentesque ligula.\n\nhttps://colaba.com/?=sdjkhdfgn_dsfgdf\n\nIn commodo dui et tristique consequat. Aliquam cursus mi nec lectus pretium, quis tincidunt erat consequat.\n\n\n\nThanks & Regards,\nJohn Doe\nLoan Officer", hyperLinks: ["https://colaba.com/?=sdjkhdfgn_dsfgdf": "https://qaapplytx.rainsoftfn.com/texas/app/signup"])
        //textviewBody.text = "Hi Richard,\n\nLorem ipsum dolor sit amet, consectetur adipiscing elit. In commodo laoreet lectus sit amet vulputate. Ut malesuada arcu eget porttitor laoreet. In commodo dui et tristique consequat. Aliquam cursus mi nec lectus pretium, quis tincidunt erat consequat. Mauris blandit velit non velit laoreet efficitur. Integer non arcu sodales, rutrum lectus ac, auctor lectus. Integer in hendrerit arcu, at pellentesque ligula.\n\nhttps://colaba.com/?=sdjkhdfgn_dsfgdf\n\nIn commodo dui et tristique consequat. Aliquam cursus mi nec lectus pretium, quis tincidunt erat consequat.\n\n\n\nThanks & Regards,\nJohn Doe\nLoan Officer"
        btnSendInvite.layer.cornerRadius = 5
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    func setScreenHeight(){
        let emailBodyHeight = textviewBody.contentSize.height
        self.mainViewHeightConstraint.constant = emailBodyHeight + 100
        self.view.layoutSubviews()
//        UIView.animate(withDuration: 0.0) {
//            self.view.layoutIfNeeded()
//        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.dismissVC()
    }
    
    @IBAction func btnSendInviteTapped(_ sender: UIButton){
        self.dismissVC()
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowInviteSendPopup), object: nil)
    }

}

extension InvitePrimaryBorrowerViewController: UITextViewDelegate{
    
    func textViewDidEndEditing(_ textView: UITextView) {
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.3) {
            self.setScreenHeight()
        }
    }
    
}

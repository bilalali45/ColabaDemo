//
//  DocumentRequestSentViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 05/10/2021.
//

import UIKit

class DocumentRequestSentViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var lblRequestSent: UILabel!
    @IBOutlet weak var btnBackToDocument: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        let successMessage = "Document Request has been sent to Richard Glenn Randall"
        let attributedSuccessMessage = NSMutableAttributedString(string: successMessage)
        let range = successMessage.range(of: "Richard Glenn Randall")
        attributedSuccessMessage.addAttribute(NSAttributedString.Key.font, value: Theme.getRubikMediumFont(size: 17), range: successMessage.nsRange(from: range!))
        lblRequestSent.attributedText = attributedSuccessMessage
        btnBackToDocument.layer.cornerRadius = 5
    }
    
    //MARK:- Methods and Actions
    
    @IBAction func btnBackToDocumentTapped(_ sender: UIButton){
        self.dismissVC()
    }
}

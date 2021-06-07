//
//  FingerPrintViewController.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit

class FingerPrintViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var logo: UIImageView!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var fingerImage: UIImageView!
    @IBOutlet weak var btnSignInWithPassword: UIButton!
    @IBOutlet weak var btnAnotherAccount: UIButton!
   
    private let biometricIDAuth = BiometricIDAuth()
    
    //MARK:- View Controller Life Cycle
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        fingerImage.isUserInteractionEnabled = true
        fingerImage.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(fingerPrintImageTapped)))
        if let user = UserModel.getCurrentUser(){
            lblUsername.text = user.userName
        }
    }

    //MARK:- Actions and Methods
    
    @objc func fingerPrintImageTapped(){
        biometricIDAuth.canEvaluate { (canEvaluate, _, canEvaluateError) in
            guard canEvaluate else {
                // Face ID/Touch ID may not be available or configured
                return
            }
            
            biometricIDAuth.evaluate { [weak self] (success, error) in
                guard success else {
                    // Face ID/Touch ID may not be configured
                    return
                }
                self!.showPopup(message: "Your finger print recognized successfully.", popupState: .success, popupDuration: .custom(2)) { (reason) in
                }
                // You are successfully verified
            }
        }
    }
    
    @IBAction func btnLoginWithPasswordTapped(_ sender: UIButton) {
        self.goToRoot()
    }
    
    @IBAction func btnAnotherAccountTapped(_ sender: UIButton) {
        self.goToRoot()
    }
    
}

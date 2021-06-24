//
//  FaceRecognitionViewController.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit

class FaceRecognitionViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var logo: UIImageView!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var faceImage: UIImageView!
    @IBOutlet weak var btnSignInWithPassword: UIButton!
    @IBOutlet weak var btnAnotherAccount: UIButton!
    
    private let biometricIDAuth = BiometricIDAuth()

    //MARK:- View Controller Life Cycle
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        faceImage.isUserInteractionEnabled = true
        faceImage.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(faceImageTapped)))
        if let user = UserModel.getCurrentUser(){
            lblUsername.text = "\(user.firstName) \(user.lastName)"
        }
        
    }

    //MARK:- Methods and Actions
    
    @objc func faceImageTapped(){
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
                UserDefaults.standard.set(kYes, forKey: kIsUserRegisteredWithBiometric)
                self?.goToDashboard()
            }
        }
    }
    
    func showResetPopup(){
        
        let alert = UIAlertController(title: "Alert", message: kFaceIdResetPopup, preferredStyle: .alert)
        let yesAction = UIAlertAction(title: "Yes", style: .default) { action in
            DispatchQueue.main.async {
                //Reset User defaults setting
                UserDefaults.standard.set(kNo, forKey: kIsUserRegisteredWithBiometric)
                let vc = Utility.getLoginVC()
                self.pushToVC(vc: vc)
            }
        }
        let noAction = UIAlertAction(title: "No", style: .destructive, handler: nil)
        alert.addAction(yesAction)
        alert.addAction(noAction)
        self.presentVC(vc: alert)
        
    }
    
    @IBAction func btnLoginWithPasswordTapped(_ sender: UIButton) {
        isBiometricAllow = false
        UserDefaults.standard.set(kNo, forKey: kIsUserRegisteredWithBiometric)
        let vc = Utility.getLoginVC()
        self.pushToVC(vc: vc)
    }
    
    @IBAction func btnAnotherAccountTapped(_ sender: UIButton) {
        isBiometricAllow = false
        UserDefaults.standard.set(kNo, forKey: kIsUserRegisteredWithBiometric)
        let vc = Utility.getLoginVC()
        self.pushToVC(vc: vc)
    }
    
}

//
//  FaceRecognitionViewController.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit
import RealmSwift

class FaceRecognitionViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var logo: UIImageView!
    @IBOutlet weak var lblGreeting: UILabel!
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
        lblGreeting.text = Utility.getGreetingMessage()
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
                
                if (Utility.getIsTokenExpire(tokenValidityDate: Utility.getTokenValidityDate())) == true{
                    self?.refreshAccessTokenWithRequest()
                }
                else{
                    UserDefaults.standard.set(kYes, forKey: kIsUserRegisteredWithBiometric)
                    self?.goToDashboard()
                }
                
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
    
    //MARK:- API's
    
    func refreshAccessTokenWithRequest(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let params = ["Token": Utility.getUserAccessToken(),
                      "RefreshToken": Utility.getUserRefreshToken()]

        APIRouter.sharedInstance.executeAPI(type: .refreshAccessToken, method: .post, params: params) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    
                    let realm = try! Realm()
                    realm.beginWrite()
                    realm.deleteAll()
                    let model = UserModel()
                    model.updateModelWithJSON(json: result["data"])
                    realm.add(model)
                    try! realm.commitWrite()
                    UserDefaults.standard.set(kYes, forKey: kIsUserRegisteredWithBiometric)
                    self.goToDashboard()
                    
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
    
}

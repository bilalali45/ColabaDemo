//
//  CreateNewPopupViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 21/06/2021.
//

import UIKit

class CreateNewPopupViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var bottomView: UIView!
    @IBOutlet weak var applicationView: UIView!
    @IBOutlet weak var contactView: UIView!
    @IBOutlet weak var btnClose: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        applicationView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(applicationViewTapped)))
        contactView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(contactViewTapped)))
        self.view.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(applicationViewTapped)))
        btnClose.roundButtonWithShadow()
        btnClose.rotate(angle: 45)
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(true)
        
        UIView.transition(with: self.bottomView, duration: 0.5, options: .transitionCrossDissolve, animations: {
            self.bottomView.isHidden = false
            self.btnClose.isHidden = false
        })
    }
    
    //MARK:- Methods and Actions
    
    @objc func applicationViewTapped(){
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationPopupViewCloseTapped), object: nil)
        self.dismiss(animated: false, completion: nil)
    }
    
    @objc func contactViewTapped(){
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationPopupViewCloseTapped), object: nil)
        self.dismiss(animated: false, completion: nil)
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton){
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationPopupViewCloseTapped), object: nil)
        self.dismiss(animated: false, completion: nil)
    }

}

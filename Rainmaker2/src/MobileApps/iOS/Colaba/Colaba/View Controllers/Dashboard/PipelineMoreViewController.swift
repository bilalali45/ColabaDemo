//
//  PiplineMoreViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 15/06/2021.
//

import UIKit

class PipelineMoreViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var lblMoreUsers: UILabel!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var emailView: UIView!
    @IBOutlet weak var callView: UIView!
    @IBOutlet weak var messageView: UIView!
    @IBOutlet weak var applicationView: UIView!
    @IBOutlet weak var documentsView: UIView!
    @IBOutlet weak var conversationsView: UIView!
    @IBOutlet weak var archiveView: UIView!
    @IBOutlet weak var bottomViewHeightConstraint: NSLayoutConstraint!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        mainView.roundOnlyTopCorners(radius: 20)
        emailView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(emailViewTapped)))
        callView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(callViewTapped)))
        messageView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(messageViewTapped)))
        applicationView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(applicationViewTapped)))
        documentsView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(documentsViewTapped)))
        conversationsView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(conversationViewTapped)))
        archiveView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(archiveViewTapped)))
        self.view.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(backgroundTapped)))
        let swipeDownGesture = UISwipeGestureRecognizer(target: self, action: #selector(backgroundTapped))
        swipeDownGesture.direction = .down
        self.view.addGestureRecognizer(swipeDownGesture)
        
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        
        self.bottomViewHeightConstraint.constant = 0
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
        UIView.animate(withDuration: 0.30, animations: {
            self.view.backgroundColor = UIColor(red: 0, green: 0, blue: 0, alpha: 0.08)
            
        }, completion: nil)
        
    }
    
    
    
    //MARK:- Methods and Actions
    
    func dismissPopup(){
        
        self.bottomViewHeightConstraint.constant = -340
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
        UIView.animate(withDuration: 0.30) {
            self.view.backgroundColor = .clear
        } completion: { _ in
            self.dismissVC()
        }
        
    }
    
    @objc func emailViewTapped(){
        dismissPopup()
    }
    
    @objc func callViewTapped(){
        dismissPopup()
    }
    
    @objc func messageViewTapped(){
        dismissPopup()
    }
    
    @objc func applicationViewTapped(){
        dismissPopup()
    }
    
    @objc func documentsViewTapped(){
        dismissPopup()
    }
    
    @objc func conversationViewTapped(){
        dismissPopup()
    }
    
    @objc func archiveViewTapped(){
        dismissPopup()
    }
    
    @objc func backgroundTapped(){
        dismissPopup()
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        dismissPopup()
    }
    

}

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
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        mainView.layer.cornerRadius = 20
        emailView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(emailViewTapped)))
        callView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(callViewTapped)))
        messageView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(messageViewTapped)))
        applicationView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(applicationViewTapped)))
        documentsView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(documentsViewTapped)))
        conversationsView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(conversationViewTapped)))
        archiveView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(archiveViewTapped)))
        
    }
    
    //MARK:- Methods and Actions
    
    @objc func emailViewTapped(){
        self.dismissVC()
    }
    
    @objc func callViewTapped(){
        self.dismissVC()
    }
    
    @objc func messageViewTapped(){
        self.dismissVC()
    }
    
    @objc func applicationViewTapped(){
        self.dismissVC()
    }
    
    @objc func documentsViewTapped(){
        self.dismissVC()
    }
    
    @objc func conversationViewTapped(){
        self.dismissVC()
    }
    
    @objc func archiveViewTapped(){
        self.dismissVC()
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    

}

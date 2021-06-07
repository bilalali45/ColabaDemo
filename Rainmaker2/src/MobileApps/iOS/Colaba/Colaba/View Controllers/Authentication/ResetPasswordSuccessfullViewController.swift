//
//  ResetPasswordSuccessfullViewController.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import UIKit

class ResetPasswordSuccessfullViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnLogin: UIButton!
    
    //MARK:- View Controller Life Cycle
    
    override func viewDidLoad() {
        super.viewDidLoad()
        btnLogin.layer.cornerRadius = 5
    }
    
    //MARK:- Actions and Methods
    
    @IBAction func btnLoginTapped(_sender: UIButton){
        self.goToRoot()
    }

}

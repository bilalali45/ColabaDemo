//
//  PiplineMoreViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 15/06/2021.
//

import UIKit

class PipelineMoreViewController: UIViewController {

    //MARK:- Outlets and Properties
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

        // Do any additional setup after loading the view.
    }
    
    //MARK:- Methods and Actions
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    

}

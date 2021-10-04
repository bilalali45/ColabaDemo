//
//  AssignLoanOfficerViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 28/09/2021.
//

import UIKit

class AssignLoanOfficerViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var containerView: UIView!
    
    var loanOfficerMainVC = LoanOfficerMainViewController()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        loanOfficerMainVC = Utility.getLoanOfficerMainVC()
        add(viewController: loanOfficerMainVC)
    }
    
    //MARK:- Methods and Actions
    
    func add(viewController: UIViewController){
        addChild(viewController)
        containerView.addSubview(viewController.view)
        viewController.view.frame = containerView.bounds
        viewController.view.autoresizingMask = [.flexibleHeight, .flexibleWidth]
        viewController.didMove(toParent: self)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.dismissVC()
    }
    
}

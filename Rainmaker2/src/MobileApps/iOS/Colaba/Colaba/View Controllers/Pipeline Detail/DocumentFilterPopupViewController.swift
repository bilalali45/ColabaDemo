//
//  DocumentFilterPopupViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 07/01/2022.
//

import UIKit

class DocumentFilterPopupViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewBottomConstraint: NSLayoutConstraint!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var allView: UIView!
    @IBOutlet weak var manuallyAddedView: UIView!
    @IBOutlet weak var borrowerToDoView: UIView!
    @IBOutlet weak var startedView: UIView!
    @IBOutlet weak var pendingView: UIView!
    @IBOutlet weak var completedView: UIView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        mainView.roundOnlyTopCorners(radius: 20)
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        
        self.mainViewBottomConstraint.constant = 0
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
        UIView.animate(withDuration: 0.30, animations: {
            self.view.backgroundColor = UIColor(red: 0, green: 0, blue: 0, alpha: 0.08)
            
        }, completion: nil)
        
    }
    
    //MARK:- Actions and Methods
    
    func dismissPopup(){
        
        self.mainViewBottomConstraint.constant = -363
        
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
        UIView.animate(withDuration: 0.30) {
            self.view.backgroundColor = .clear
        } completion: { _ in
            self.dismissVC()
        }
        
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        dismissPopup()
    }
    
}

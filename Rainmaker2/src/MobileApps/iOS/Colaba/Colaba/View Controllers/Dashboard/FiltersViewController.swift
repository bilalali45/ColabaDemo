//
//  FiltersViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 15/06/2021.
//

import UIKit

protocol FiltersViewControllerDelegate: AnyObject {
    func getOrderby(orderBy: Int)
}

class FiltersViewController: BaseViewController {

    //MARK:- Outlets and properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var pendingActivityView: UIView!
    @IBOutlet weak var recentActivityView: UIView!
    @IBOutlet weak var aToZView: UIView!
    @IBOutlet weak var zToAView: UIView!
    @IBOutlet weak var bottomViewHeightConstraint: NSLayoutConstraint!
    
    weak var delegate: FiltersViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        mainView.roundOnlyTopCorners(radius: 20)
        pendingActivityView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(pendingViewTapped)))
        recentActivityView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(recentViewTapped)))
        aToZView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(aToZViewTapped)))
        zToAView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(zToAViewTapped)))
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
        
        self.bottomViewHeightConstraint.constant = -294
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
        UIView.animate(withDuration: 0.30) {
            self.view.backgroundColor = .clear
        } completion: { _ in
            self.dismissVC()
        }
        
    }
    
    @objc func pendingViewTapped(){
        self.delegate?.getOrderby(orderBy: 0)
        dismissPopup()
    }
    
    @objc func recentViewTapped(){
        self.delegate?.getOrderby(orderBy: 1)
        dismissPopup()
    }
    
    @objc func aToZViewTapped(){
        self.delegate?.getOrderby(orderBy: 2)
        dismissPopup()
    }
    
    @objc func zToAViewTapped(){
        self.delegate?.getOrderby(orderBy: 3)
        dismissPopup()
    }
    
    @objc func backgroundTapped(){
        dismissPopup()
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        dismissPopup()
    }
}

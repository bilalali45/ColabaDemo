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
    
    
    //MARK:- Methods and Actions
    
    @objc func pendingViewTapped(){
        self.delegate?.getOrderby(orderBy: 0)
        self.dismissVC()
    }
    
    @objc func recentViewTapped(){
        self.delegate?.getOrderby(orderBy: 1)
        self.dismissVC()
    }
    
    @objc func aToZViewTapped(){
        self.delegate?.getOrderby(orderBy: 2)
        self.dismissVC()
    }
    
    @objc func zToAViewTapped(){
        self.delegate?.getOrderby(orderBy: 3)
        self.dismissVC()
    }
    
    @objc func backgroundTapped(){
        self.dismissVC()
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        self.dismissVC()
    }
}

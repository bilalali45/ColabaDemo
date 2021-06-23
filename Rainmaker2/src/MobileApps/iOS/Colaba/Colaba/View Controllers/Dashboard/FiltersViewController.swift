//
//  FiltersViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 15/06/2021.
//

import UIKit

class FiltersViewController: BaseViewController {

    //MARK:- Outlets and properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var pendingActivityView: UIView!
    @IBOutlet weak var recentActivityView: UIView!
    @IBOutlet weak var aToZView: UIView!
    @IBOutlet weak var zToAView: UIView!
    
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
        self.dismissVC()
    }
    
    @objc func recentViewTapped(){
        self.dismissVC()
    }
    
    @objc func aToZViewTapped(){
        self.dismissVC()
    }
    
    @objc func zToAViewTapped(){
        self.dismissVC()
    }
    
    @objc func backgroundTapped(){
        self.dismissVC()
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        self.dismissVC()
    }
}

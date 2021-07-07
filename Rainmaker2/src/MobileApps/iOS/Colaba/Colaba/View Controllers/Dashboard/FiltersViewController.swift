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
    @IBOutlet weak var iconRecentActivity: UIImageView!
    @IBOutlet weak var lblMostRecentActivity: UILabel!
    @IBOutlet weak var iconPendingActions: UIImageView!
    @IBOutlet weak var lblPendingActions: UILabel!
    @IBOutlet weak var iconAtoZ: UIImageView!
    @IBOutlet weak var lblAtoZ: UILabel!
    @IBOutlet weak var iconZtoA: UIImageView!
    @IBOutlet weak var lblZtoA: UILabel!
    
    var imgRecentActivity = UIImage()
    var imgPendingAction = UIImage()
    var imgAToZ = UIImage()
    var imgZtoA = UIImage()
    
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
        
        imgRecentActivity = (UIImage(named: "most-recent-icon")?.withRenderingMode(.alwaysTemplate))!
        imgPendingAction = (UIImage(named: "pending-actions-icon")?.withRenderingMode(.alwaysTemplate))!
        imgAToZ = (UIImage(named: "borrower-sort-icon")?.withRenderingMode(.alwaysTemplate))!
        imgZtoA = (UIImage(named: "borrower-sort-icon")?.withRenderingMode(.alwaysTemplate))!
        
        iconRecentActivity.image = imgRecentActivity
        iconPendingActions.image = imgPendingAction
        iconAtoZ.image = imgAToZ
        iconZtoA.image = imgZtoA
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
        
        setSelectedFilter()
    }
    
    //MARK:- Methods and Actions
    
    func setSelectedFilter(){
        
        if (sortingFilter == 0){
            lblPendingActions.textColor = Theme.getButtonBlueColor()
            iconPendingActions.tintColor = Theme.getButtonBlueColor()
            
            lblMostRecentActivity.textColor = Theme.getAppGreyColor()
            iconRecentActivity.tintColor = Theme.getAppGreyColor()
            lblAtoZ.textColor = Theme.getAppGreyColor()
            iconAtoZ.tintColor = Theme.getAppGreyColor()
            lblZtoA.textColor = Theme.getAppGreyColor()
            iconZtoA.tintColor = Theme.getAppGreyColor()
        }
        else if (sortingFilter == 1){
            lblMostRecentActivity.textColor = Theme.getButtonBlueColor()
            iconRecentActivity.tintColor = Theme.getButtonBlueColor()
            
            lblPendingActions.textColor = Theme.getAppGreyColor()
            iconPendingActions.tintColor = Theme.getAppGreyColor()
            lblAtoZ.textColor = Theme.getAppGreyColor()
            iconAtoZ.tintColor = Theme.getAppGreyColor()
            lblZtoA.textColor = Theme.getAppGreyColor()
            iconZtoA.tintColor = Theme.getAppGreyColor()
        }
        else if (sortingFilter == 2){
            lblAtoZ.textColor = Theme.getButtonBlueColor()
            iconAtoZ.tintColor = Theme.getButtonBlueColor()
            
            lblPendingActions.textColor = Theme.getAppGreyColor()
            iconPendingActions.tintColor = Theme.getAppGreyColor()
            lblMostRecentActivity.textColor = Theme.getAppGreyColor()
            iconRecentActivity.tintColor = Theme.getAppGreyColor()
            lblZtoA.textColor = Theme.getAppGreyColor()
            iconZtoA.tintColor = Theme.getAppGreyColor()
        }
        else if (sortingFilter == 3){
            lblZtoA.textColor = Theme.getButtonBlueColor()
            iconZtoA.tintColor = Theme.getButtonBlueColor()
            
            lblPendingActions.textColor = Theme.getAppGreyColor()
            iconPendingActions.tintColor = Theme.getAppGreyColor()
            lblMostRecentActivity.textColor = Theme.getAppGreyColor()
            iconRecentActivity.tintColor = Theme.getAppGreyColor()
            lblAtoZ.textColor = Theme.getAppGreyColor()
            iconAtoZ.tintColor = Theme.getAppGreyColor()
        }
    }
    
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
        sortingFilter = 0
        setSelectedFilter()
        self.delegate?.getOrderby(orderBy: sortingFilter)
        dismissPopup()
    }
    
    @objc func recentViewTapped(){
        sortingFilter = 1
        setSelectedFilter()
        self.delegate?.getOrderby(orderBy: sortingFilter)
        dismissPopup()
    }
    
    @objc func aToZViewTapped(){
        sortingFilter = 2
        setSelectedFilter()
        self.delegate?.getOrderby(orderBy: sortingFilter)
        dismissPopup()
    }
    
    @objc func zToAViewTapped(){
        sortingFilter = 3
        setSelectedFilter()
        self.delegate?.getOrderby(orderBy: sortingFilter)
        dismissPopup()
    }
    
    @objc func backgroundTapped(){
        dismissPopup()
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        dismissPopup()
    }
}

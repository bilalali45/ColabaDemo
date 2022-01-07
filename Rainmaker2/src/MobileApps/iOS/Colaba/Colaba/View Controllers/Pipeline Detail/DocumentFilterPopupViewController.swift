//
//  DocumentFilterPopupViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 07/01/2022.
//

import UIKit

protocol DocumentFilterPopupViewControllerDelegate: AnyObject {
    func filterViewTapped(selectedTag: Int)
}

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
    
    var delegate: DocumentFilterPopupViewControllerDelegate?
    
    var selectedTag = 1
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        mainView.roundOnlyTopCorners(radius: 20)
        setSelectedFilter(filterViews: [allView, manuallyAddedView, borrowerToDoView, startedView, pendingView, completedView])
        allView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(allViewTapped)))
        manuallyAddedView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(manuallyAddedViewTapped)))
        borrowerToDoView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(borrowerToDoViewTapped)))
        startedView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(startedViewTapped)))
        pendingView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(pendingViewTapped)))
        completedView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(completedViewTapped)))
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
    
    @objc func allViewTapped(){
        selectedTag = 1
        setSelectedFilter(filterViews: [allView, manuallyAddedView, borrowerToDoView, startedView, pendingView, completedView])
        dismissPopup()
        self.delegate?.filterViewTapped(selectedTag: selectedTag)
    }
    
    @objc func manuallyAddedViewTapped(){
        selectedTag = 2
        setSelectedFilter(filterViews: [allView, manuallyAddedView, borrowerToDoView, startedView, pendingView, completedView])
        dismissPopup()
        self.delegate?.filterViewTapped(selectedTag: selectedTag)
    }
    
    @objc func borrowerToDoViewTapped(){
        selectedTag = 3
        setSelectedFilter(filterViews: [allView, manuallyAddedView, borrowerToDoView, startedView, pendingView, completedView])
        dismissPopup()
        self.delegate?.filterViewTapped(selectedTag: selectedTag)
    }
    
    @objc func startedViewTapped(){
        selectedTag = 4
        setSelectedFilter(filterViews: [allView, manuallyAddedView, borrowerToDoView, startedView, pendingView, completedView])
        dismissPopup()
        self.delegate?.filterViewTapped(selectedTag: selectedTag)
    }
    
    @objc func pendingViewTapped(){
        selectedTag = 5
        setSelectedFilter(filterViews: [allView, manuallyAddedView, borrowerToDoView, startedView, pendingView, completedView])
        dismissPopup()
        self.delegate?.filterViewTapped(selectedTag: selectedTag)
    }
    
    @objc func completedViewTapped(){
        selectedTag = 6
        setSelectedFilter(filterViews: [allView, manuallyAddedView, borrowerToDoView, startedView, pendingView, completedView])
        dismissPopup()
        self.delegate?.filterViewTapped(selectedTag: selectedTag)
    }
    
    func setSelectedFilter(filterViews: [UIView]){
        for filterView in filterViews{
            for subView in filterView.subviews{
                if (subView.isKind(of: UILabel.self)){
                    (subView as! UILabel).font = selectedTag == filterView.tag ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
                    (subView as! UILabel).textColor = selectedTag == filterView.tag ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
                }
            }
        }
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        dismissPopup()
    }
    
}

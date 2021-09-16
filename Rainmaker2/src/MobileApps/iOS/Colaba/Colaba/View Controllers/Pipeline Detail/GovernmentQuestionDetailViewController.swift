//
//  GovernmentQuestionDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit

class GovernmentQuestionDetailViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var tabsScrollView: UIScrollView!
    @IBOutlet weak var tabsMainView: UIView!
    @IBOutlet weak var unDisclosedView: UIView!
    @IBOutlet weak var ownershipInterestView: UIView!
    @IBOutlet weak var priorityLiensView: UIView!
    @IBOutlet weak var undisclosedMortgageApplicationsView: UIView!
    @IBOutlet weak var undisclosedCreditApplicationView: UIView!
    @IBOutlet weak var debtCoSignerView: UIView!
    @IBOutlet weak var outstandingJudgementsView: UIView!
    @IBOutlet weak var fedralDebtView: UIView!
    @IBOutlet weak var partyToLawsuitView: UIView!
    @IBOutlet weak var titleConveyanceView: UIView!
    @IBOutlet weak var preForceClosureView: UIView!
    @IBOutlet weak var foreClosuredPropertyView: UIView!
    @IBOutlet weak var bankruptcyView: UIView!
    @IBOutlet weak var childSupportView: UIView!
    @IBOutlet weak var demographicView: UIView!
    @IBOutlet weak var containerView: UIView!
    
    var undisclosedVC: UndisclosedBorrowerFundsViewController!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        undisclosedVC = Utility.getUndisclosedBorrowerFundsVC()
        
        roundAllFilterViews(filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
        filterViewTapped(selectedFilterView: unDisclosedView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
        
        unDisclosedView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(unDisclosedViewTapped)))
        ownershipInterestView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(ownershipInterestViewTapped)))
        priorityLiensView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(priorityLiensViewTapped)))
        undisclosedMortgageApplicationsView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(undisclosedMortgageApplicationsViewTapped)))
        undisclosedCreditApplicationView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(undisclosedCreditApplicationViewTapped)))
        debtCoSignerView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(debtCoSignerViewTapped)))
        outstandingJudgementsView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(outstandingJudgementsViewTapped)))
        fedralDebtView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(fedralDebtViewTapped)))
        partyToLawsuitView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(partyToLawsuitViewTapped)))
        titleConveyanceView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(titleConveyanceViewTapped)))
        preForceClosureView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(preForceClosureViewTapped)))
        foreClosuredPropertyView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(foreClosuredPropertyViewTapped)))
        bankruptcyView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(bankruptcyViewTapped)))
        childSupportView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(childSupportViewTapped)))
        demographicView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(demographicViewTapped)))
    }
    
    //MARK:- Methods and Actions
    
    func roundAllFilterViews(filterViews: [UIView]){
        for filterView in filterViews{
            filterView.layer.cornerRadius = 15
        }
    }
    
    func filterViewTapped(selectedFilterView: UIView, filterViews: [UIView]){
        for filterView in filterViews{
            if (filterView == selectedFilterView){
                filterView.backgroundColor = .white
                filterView.layer.borderWidth = 1
                filterView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
                for subview in filterView.subviews{
                    if (subview.isKind(of: UILabel.self)){
                        (subview as! UILabel).textColor = Theme.getButtonBlueColor()
                    }
                }
            }
            else{
                filterView.backgroundColor = Theme.getButtonGreyColor().withAlphaComponent(0.5)
                filterView.layer.borderWidth = 0
                for subview in filterView.subviews{
                    if (subview.isKind(of: UILabel.self)){
                        (subview as! UILabel).textColor = Theme.getButtonGreyTextColor()
                    }
                }
            }
        }
        if (selectedFilterView == unDisclosedView){
            add(viewController: undisclosedVC)
        }
    }
    
    func add(viewController: UIViewController){
        addChild(viewController)
        containerView.addSubview(viewController.view)
        viewController.view.frame = containerView.bounds
        viewController.view.autoresizingMask = [.flexibleHeight, .flexibleWidth]
        viewController.didMove(toParent: self)
    }
    
    func remove(viewControllers: [UIViewController]){
        for viewController in viewControllers{
            viewController.willMove(toParent: nil)
            viewController.view.removeFromSuperview()
            viewController.removeFromParent()
        }
    }
    
    @objc func unDisclosedViewTapped(){
        filterViewTapped(selectedFilterView: unDisclosedView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func ownershipInterestViewTapped(){
        filterViewTapped(selectedFilterView: ownershipInterestView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func priorityLiensViewTapped(){
        filterViewTapped(selectedFilterView: priorityLiensView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func undisclosedMortgageApplicationsViewTapped(){
        filterViewTapped(selectedFilterView: undisclosedMortgageApplicationsView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func undisclosedCreditApplicationViewTapped(){
        filterViewTapped(selectedFilterView: undisclosedCreditApplicationView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func debtCoSignerViewTapped(){
        filterViewTapped(selectedFilterView: debtCoSignerView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func outstandingJudgementsViewTapped(){
        filterViewTapped(selectedFilterView: outstandingJudgementsView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func fedralDebtViewTapped(){
        filterViewTapped(selectedFilterView: fedralDebtView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func partyToLawsuitViewTapped(){
        filterViewTapped(selectedFilterView: partyToLawsuitView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func titleConveyanceViewTapped(){
        filterViewTapped(selectedFilterView: titleConveyanceView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func preForceClosureViewTapped(){
        filterViewTapped(selectedFilterView: preForceClosureView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func foreClosuredPropertyViewTapped(){
        filterViewTapped(selectedFilterView: foreClosuredPropertyView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func bankruptcyViewTapped(){
        filterViewTapped(selectedFilterView: bankruptcyView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func childSupportViewTapped(){
        filterViewTapped(selectedFilterView: childSupportView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func demographicViewTapped(){
        filterViewTapped(selectedFilterView: demographicView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }

}

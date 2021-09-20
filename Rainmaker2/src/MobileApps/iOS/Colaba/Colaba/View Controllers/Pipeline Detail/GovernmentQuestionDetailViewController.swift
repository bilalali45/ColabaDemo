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
    var ownershipInterestVC: OwnershipInterestInPropertyViewController!
    var priorityLiensVC: PriorityLiensViewController!
    var undisclosedMortgageApplicationVC: UndisclosedMortgageApplicationViewController!
    var undisclosedCreditApplicationVC: UndisclosedCreditApplicationViewController!
    var debtCoSignerVC: DebtCoSignerViewController!
    var outstandingJudgementsVC: OutstandingJudgementsViewController!
    var fedralDebtVC: FedralDebtViewController!
    var partyToLawsuitVC: PartyToLawsuitViewController!
    var titleConveyanceVC: TitleConveyanceViewController!
    var preForceClosureVC: PreForceClosureViewController!
    var forceClosedPropertyVC: ForceClosedPropertyViewController!
    var bankruptcyVC: BankruptcyViewController!
    var childSupportVC: ChildSupportViewController!
    var demographicInfoVC: DemographicInformationViewController!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        undisclosedVC = Utility.getUndisclosedBorrowerFundsVC()
        ownershipInterestVC = Utility.getOwnershipInterestInPropertyVC()
        priorityLiensVC = Utility.getPriorityLiensViewController()
        undisclosedMortgageApplicationVC = Utility.getUndisclosedMortgageApplicationVC()
        undisclosedCreditApplicationVC = Utility.getUndisclosedCreditApplicationVC()
        debtCoSignerVC = Utility.getDebtCoSignerVC()
        outstandingJudgementsVC = Utility.getOutstandingJudgementsVC()
        fedralDebtVC =  Utility.getFedralDebtVC()
        partyToLawsuitVC = Utility.getPartyToLawsuitVC()
        titleConveyanceVC = Utility.getTitleConveyanceVC()
        preForceClosureVC = Utility.getPreForceClosureVC()
        forceClosedPropertyVC = Utility.getForceClosedPropertyVC()
        bankruptcyVC = Utility.getBankruptcyVC()
        childSupportVC = Utility.getChildSupportVC()
        demographicInfoVC = Utility.getDemographicInformationVC()
        
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
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: undisclosedVC)
            add(viewController: undisclosedVC)
        }
        else if (selectedFilterView == ownershipInterestView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: ownershipInterestVC)
            add(viewController: ownershipInterestVC)
        }
        else if (selectedFilterView == priorityLiensView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: priorityLiensVC)
            add(viewController: priorityLiensVC)
        }
        else if (selectedFilterView == undisclosedMortgageApplicationsView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: undisclosedMortgageApplicationVC)
            add(viewController: undisclosedMortgageApplicationVC)
        }
        else if (selectedFilterView == undisclosedCreditApplicationView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: undisclosedCreditApplicationVC)
            add(viewController: undisclosedCreditApplicationVC)
        }
        else if (selectedFilterView == debtCoSignerView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: debtCoSignerVC)
            add(viewController: debtCoSignerVC)
        }
        else if (selectedFilterView == outstandingJudgementsView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: outstandingJudgementsVC)
            add(viewController: outstandingJudgementsVC)
        }
        else if (selectedFilterView == fedralDebtView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: fedralDebtVC)
            add(viewController: fedralDebtVC)
        }
        else if (selectedFilterView == partyToLawsuitView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: partyToLawsuitVC)
            add(viewController: partyToLawsuitVC)
        }
        else if (selectedFilterView == titleConveyanceView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: titleConveyanceVC)
            add(viewController: titleConveyanceVC)
        }
        else if (selectedFilterView == preForceClosureView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: preForceClosureVC)
            add(viewController: preForceClosureVC)
        }
        else if (selectedFilterView == foreClosuredPropertyView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: forceClosedPropertyVC)
            add(viewController: forceClosedPropertyVC)
        }
        else if (selectedFilterView == bankruptcyView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: bankruptcyVC)
            add(viewController: bankruptcyVC)
        }
        else if (selectedFilterView == childSupportView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: childSupportVC)
            add(viewController: childSupportVC)
        }
        else if (selectedFilterView == demographicView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: demographicInfoVC)
            add(viewController: demographicInfoVC)
        }
    }
    
    func add(viewController: UIViewController){
        addChild(viewController)
        containerView.addSubview(viewController.view)
        viewController.view.frame = containerView.bounds
        viewController.view.autoresizingMask = [.flexibleHeight, .flexibleWidth]
        viewController.didMove(toParent: self)
    }
    
    func remove(viewControllers: [UIViewController], selectedVC: UIViewController){
        for viewController in viewControllers{
            if (viewController != selectedVC){
                viewController.willMove(toParent: nil)
                viewController.view.removeFromSuperview()
                viewController.removeFromParent()
            }
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

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
    @IBOutlet weak var familyOrBusinessAffiliationView: UIView!
    //@IBOutlet weak var priorityLiensView: UIView!
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
    //var priorityLiensVC: PriorityLiensViewController!
    var familyOrBusinessAffiliationVC: FamilyOrBusinessAffliationViewController!
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
    
    var loanApplicationId = 0
    var borrowerId = 0
    var ownTypeId = 0
    var governmentQuestions = [GovernmentQuestionModel]()
    
    override func viewDidLoad() {
        super.viewDidLoad()

        roundAllFilterViews(filterViews: [unDisclosedView, ownershipInterestView, familyOrBusinessAffiliationView,/*priorityLiensView,*/ undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
        getGovernmentQuestions()
        
        unDisclosedView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(unDisclosedViewTapped)))
        ownershipInterestView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(ownershipInterestViewTapped)))
        //priorityLiensView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(priorityLiensViewTapped)))
        familyOrBusinessAffiliationView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(familyOrBusinessAffiliationViewTapped)))
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
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: undisclosedVC)
            add(viewController: undisclosedVC)
        }
        else if (selectedFilterView == ownershipInterestView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: ownershipInterestVC)
            add(viewController: ownershipInterestVC)
        }
        else if (selectedFilterView == familyOrBusinessAffiliationView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: familyOrBusinessAffiliationVC)
            add(viewController: familyOrBusinessAffiliationVC)
        }
//        else if (selectedFilterView == priorityLiensView){
//            remove(viewControllers: [undisclosedVC, ownershipInterestVC, priorityLiensVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: priorityLiensVC)
//            add(viewController: priorityLiensVC)
//        }
        else if (selectedFilterView == undisclosedMortgageApplicationsView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: undisclosedMortgageApplicationVC)
            add(viewController: undisclosedMortgageApplicationVC)
        }
        else if (selectedFilterView == undisclosedCreditApplicationView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: undisclosedCreditApplicationVC)
            add(viewController: undisclosedCreditApplicationVC)
        }
        else if (selectedFilterView == debtCoSignerView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: debtCoSignerVC)
            add(viewController: debtCoSignerVC)
        }
        else if (selectedFilterView == outstandingJudgementsView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: outstandingJudgementsVC)
            add(viewController: outstandingJudgementsVC)
        }
        else if (selectedFilterView == fedralDebtView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: fedralDebtVC)
            add(viewController: fedralDebtVC)
        }
        else if (selectedFilterView == partyToLawsuitView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: partyToLawsuitVC)
            add(viewController: partyToLawsuitVC)
        }
        else if (selectedFilterView == titleConveyanceView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: titleConveyanceVC)
            add(viewController: titleConveyanceVC)
        }
        else if (selectedFilterView == preForceClosureView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: preForceClosureVC)
            add(viewController: preForceClosureVC)
        }
        else if (selectedFilterView == foreClosuredPropertyView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: forceClosedPropertyVC)
            add(viewController: forceClosedPropertyVC)
        }
        else if (selectedFilterView == bankruptcyView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: bankruptcyVC)
            add(viewController: bankruptcyVC)
        }
        else if (selectedFilterView == childSupportView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: childSupportVC)
            add(viewController: childSupportVC)
        }
        else if (selectedFilterView == demographicView){
            remove(viewControllers: [undisclosedVC, ownershipInterestVC, /*priorityLiensVC,*/familyOrBusinessAffiliationVC, undisclosedMortgageApplicationVC, undisclosedCreditApplicationVC , debtCoSignerVC, outstandingJudgementsVC, fedralDebtVC, partyToLawsuitVC, titleConveyanceVC, preForceClosureVC, forceClosedPropertyVC, bankruptcyVC, childSupportVC, demographicInfoVC], selectedVC: demographicInfoVC)
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
    
    func setGovernmentQuestion(){

        undisclosedVC = Utility.getUndisclosedBorrowerFundsVC()
        if let undisclosedQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Undisclosed Borrowered Funds")}).first{
            undisclosedVC.questionModel = undisclosedQuestion
            if let subQuestion = governmentQuestions.filter({$0.parentQuestionId == undisclosedQuestion.id}).first{
                undisclosedVC.subQuestionModel = subQuestion
            }
        }
        
        ownershipInterestVC = Utility.getOwnershipInterestInPropertyVC()
        if let ownershipQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Ownership Interest in Property")}).first{
            ownershipInterestVC.questionModel = ownershipQuestion
            let subQuestions = governmentQuestions.filter({$0.parentQuestionId == ownershipQuestion.id})
            ownershipInterestVC.subQuestions = subQuestions
            
        }
        
        familyOrBusinessAffiliationVC = Utility.getFamilyOrBusinessAffliationVC()
        if let familyOrBusinessAffiliationQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Family or Business affiliation")}).first{
            familyOrBusinessAffiliationVC.questionModel = familyOrBusinessAffiliationQuestion
        }
        
        //priorityLiensVC = Utility.getPriorityLiensViewController()
        
        undisclosedMortgageApplicationVC = Utility.getUndisclosedMortgageApplicationVC()
        
        undisclosedCreditApplicationVC = Utility.getUndisclosedCreditApplicationVC()
        
        debtCoSignerVC = Utility.getDebtCoSignerVC()
        if let debtCoQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Debt Co-Signer or Guarantor")}).first{
            debtCoSignerVC.questionModel = debtCoQuestion
        }
        
        outstandingJudgementsVC = Utility.getOutstandingJudgementsVC()
        if let outstandingJudgementQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Outstanding Judgements")}).first{
            outstandingJudgementsVC.questionModel = outstandingJudgementQuestion
        }
        
        fedralDebtVC =  Utility.getFedralDebtVC()
        if let fedralDebtQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Federal Debt Deliquency")}).first{
            fedralDebtVC.questionModel = fedralDebtQuestion
        }
        
        partyToLawsuitVC = Utility.getPartyToLawsuitVC()
        if let partyToLawsuitQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Party to Lawsuit")}).first{
            partyToLawsuitVC.questionModel = partyToLawsuitQuestion
        }
        
        titleConveyanceVC = Utility.getTitleConveyanceVC()
        if let titleConveyanceQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Title Conveyance")}).first{
            titleConveyanceVC.questionModel = titleConveyanceQuestion
        }
        
        preForceClosureVC = Utility.getPreForceClosureVC()
        if let preForceClosureQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Pre-Foreclosureor Short Sale")}).first{
            preForceClosureVC.questionModel = preForceClosureQuestion
        }
        
        forceClosedPropertyVC = Utility.getForceClosedPropertyVC()
        if let forceClosureQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Foreclosured Property")}).first{
            forceClosedPropertyVC.questionModel = forceClosureQuestion
        }
        
        bankruptcyVC = Utility.getBankruptcyVC()
        if let bankruptcyQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Bankruptcy")}).first{
            bankruptcyVC.questionModel = bankruptcyQuestion
            if let subQuestion = governmentQuestions.filter({$0.parentQuestionId == bankruptcyQuestion.id}).first{
                bankruptcyVC.subQuestionModel = subQuestion
            }
        }
        
        childSupportVC = Utility.getChildSupportVC()
        if let childSupportQuestion = governmentQuestions.filter({$0.headerText.localizedCaseInsensitiveContains("Child Support, Alimony, etc.")}).first{
            childSupportVC.questionModel = childSupportQuestion
        }
        
        demographicInfoVC = Utility.getDemographicInformationVC()
        
        
        filterViewTapped(selectedFilterView: unDisclosedView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func unDisclosedViewTapped(){
        filterViewTapped(selectedFilterView: unDisclosedView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func ownershipInterestViewTapped(){
        filterViewTapped(selectedFilterView: ownershipInterestView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
//    @objc func priorityLiensViewTapped(){
//        filterViewTapped(selectedFilterView: priorityLiensView, filterViews: [unDisclosedView, ownershipInterestView, priorityLiensView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
//    }
    
    @objc func familyOrBusinessAffiliationViewTapped(){
        filterViewTapped(selectedFilterView: familyOrBusinessAffiliationView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func undisclosedMortgageApplicationsViewTapped(){
        filterViewTapped(selectedFilterView: undisclosedMortgageApplicationsView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func undisclosedCreditApplicationViewTapped(){
        filterViewTapped(selectedFilterView: undisclosedCreditApplicationView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func debtCoSignerViewTapped(){
        filterViewTapped(selectedFilterView: debtCoSignerView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func outstandingJudgementsViewTapped(){
        filterViewTapped(selectedFilterView: outstandingJudgementsView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func fedralDebtViewTapped(){
        filterViewTapped(selectedFilterView: fedralDebtView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func partyToLawsuitViewTapped(){
        filterViewTapped(selectedFilterView: partyToLawsuitView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func titleConveyanceViewTapped(){
        filterViewTapped(selectedFilterView: titleConveyanceView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func preForceClosureViewTapped(){
        filterViewTapped(selectedFilterView: preForceClosureView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func foreClosuredPropertyViewTapped(){
        filterViewTapped(selectedFilterView: foreClosuredPropertyView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func bankruptcyViewTapped(){
        filterViewTapped(selectedFilterView: bankruptcyView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func childSupportViewTapped(){
        filterViewTapped(selectedFilterView: childSupportView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    @objc func demographicViewTapped(){
        filterViewTapped(selectedFilterView: demographicView, filterViews: [unDisclosedView, ownershipInterestView, /*priorityLiensView,*/familyOrBusinessAffiliationView, undisclosedMortgageApplicationsView, undisclosedCreditApplicationView, debtCoSignerView, outstandingJudgementsView, fedralDebtView, partyToLawsuitView, titleConveyanceView, preForceClosureView, foreClosuredPropertyView, bankruptcyView, childSupportView, demographicView])
    }
    
    //MARK:- API
    
    func getGovernmentQuestions(){
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)&ownTypeId=\(ownTypeId)&borrowerId=\(self.borrowerId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getGovernmentQuestions, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    
                    self.governmentQuestions.removeAll()
                    let questions = result["data"].arrayValue
                    for question in questions{
                        let model = GovernmentQuestionModel()
                        model.updateModelWithJSON(json: question)
                        self.governmentQuestions.append(model)
                    }
                    self.setGovernmentQuestion()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
            
        }
        
    }

}

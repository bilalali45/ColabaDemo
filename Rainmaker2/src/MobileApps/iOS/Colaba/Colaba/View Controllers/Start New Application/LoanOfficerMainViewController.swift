//
//  LoanOfficerMainViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 27/09/2021.
//

import UIKit
import CarbonKit

class LoanOfficerMainViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var searchView: UIView!
    @IBOutlet weak var txtfieldSearch: UITextField!
    @IBOutlet weak var searchIcon: UIImageView!
    @IBOutlet weak var tabView: UIView!
    
    var isForPopup = false
    var loanOfficers = [LoanOfficersModel]()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        searchView.layer.cornerRadius = 5
        searchView.layer.borderWidth = 1
        searchView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        txtfieldSearch.delegate = self
        txtfieldSearch.returnKeyType = .search
        txtfieldSearch.addTarget(self, action: #selector(searchOfficers), for: .editingChanged)
        getAllLoanOfficers()
        if (isForPopup){
            self.view.backgroundColor = .clear
        }
    }
    
    //MARK:- Methods and Actions
    
    func setupHeader(){
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.03) {
            let tabItems = ["Loan Officer", "Loan Coordinator", "Pre Processor", "Loan Processor"]
            let carbonTabSwipeNavigation = CarbonTabSwipeNavigation(items: tabItems, delegate: self)
            
            carbonTabSwipeNavigation.carbonSegmentedControl?.backgroundColor = self.isForPopup ? .white : Theme.getDashboardBackgroundColor()
            carbonTabSwipeNavigation.setTabBarHeight(50)
            carbonTabSwipeNavigation.setIndicatorColor(nil)
            carbonTabSwipeNavigation.setIndicatorHeight(4)
            carbonTabSwipeNavigation.setNormalColor(Theme.getAppGreyColor(), font: Theme.getRubikRegularFont(size: 13))
            carbonTabSwipeNavigation.setSelectedColor(Theme.getButtonBlueColor(), font: Theme.getRubikRegularFont(size: 13))
            carbonTabSwipeNavigation.carbonSegmentedControl?.imageNormalColor = .clear
            carbonTabSwipeNavigation.carbonSegmentedControl?.imageSelectedColor = .clear
            
            let segmentWidth = (self.tabView.frame.width / 3.5)
            
            let indicator = carbonTabSwipeNavigation.carbonSegmentedControl?.indicator
            let subView = UIView()
            subView.backgroundColor = Theme.getButtonBlueColor()
            subView.roundOnlyTopCorners(radius: 4)
            indicator?.addSubview(subView)
            subView.translatesAutoresizingMaskIntoConstraints = false
            subView.widthAnchor.constraint(equalToConstant: segmentWidth * 0.8).isActive = true
            subView.centerXAnchor.constraint(equalTo: indicator!.centerXAnchor, constant: 0).isActive = true
            subView.topAnchor.constraint(equalTo: indicator!.topAnchor, constant: 0).isActive = true
            subView.bottomAnchor.constraint(equalTo: indicator!.bottomAnchor, constant: 0).isActive = true
            carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 0)
            carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 1)
            carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 2)
            carbonTabSwipeNavigation.insert(intoRootViewController: self, andTargetView: self.tabView)
            
        }
    }
    
    @objc func searchOfficers(){
        let searchUserInfo = ["searchText": txtfieldSearch.text!]
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationSearchOfficers), object: nil, userInfo: searchUserInfo)
    }
    
    //MARK:- API
    
    func getAllLoanOfficers(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "filterLoanOfficer=true"
        
        APIRouter.sharedInstance.executeAPI(type: .getLoanOfficers, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    let officers = result["data"]["roles"].arrayValue
                    for officer in officers{
                        let model = LoanOfficersModel()
                        model.updateModelWithJSON(json: officer)
                        self.loanOfficers.append(model)
                    }
                    self.setupHeader()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(2)) { reason in
                        
                    }
                }
                
            }
            
        }
    }

}

extension LoanOfficerMainViewController: CarbonTabSwipeNavigationDelegate{
    
    func carbonTabSwipeNavigation(_ carbonTabSwipeNavigation: CarbonTabSwipeNavigation, viewControllerAt index: UInt) -> UIViewController {
        let vc = Utility.getLoanOfficerListVC()
        vc.isForPopup = isForPopup
        vc.selectedRole = index == 0 ? loanOfficers.filter({$0.roleName.localizedCaseInsensitiveContains("Loan Officer")}).first! : LoanOfficersModel()
        return vc
    }
    
}

extension LoanOfficerMainViewController: UITextFieldDelegate{
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        if (isForPopup){
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationLoanOfficerSeeMoreTapped), object: nil)
        }
        else{
            searchOfficers()
        }
        return true
    }
    
}

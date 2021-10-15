//
//  AssetsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 06/09/2021.
//

import UIKit
import CarbonKit

class AssetsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var tabView: UIView!
    @IBOutlet weak var lblTotalAssets: UILabel!
    
    var loanApplicationId = 0
    var borrowersArray = [BorrowerInfoModel]()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupHeaderAndFooter()
    }
    
    //MARK:- Methods and Actions
    func setupHeaderAndFooter(){
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.03) {
            let carbonTabSwipeNavigation = CarbonTabSwipeNavigation(items: self.borrowersArray.map{$0.borrowerFullName}, delegate: self)
            
            carbonTabSwipeNavigation.carbonSegmentedControl?.backgroundColor = Theme.getDashboardBackgroundColor()
            carbonTabSwipeNavigation.setTabBarHeight(50)
            carbonTabSwipeNavigation.setIndicatorColor(nil)
            carbonTabSwipeNavigation.setIndicatorHeight(4)
            carbonTabSwipeNavigation.setNormalColor(Theme.getAppGreyColor(), font: Theme.getRubikRegularFont(size: 15))
            carbonTabSwipeNavigation.setSelectedColor(Theme.getButtonBlueColor(), font: Theme.getRubikRegularFont(size: 15))
            carbonTabSwipeNavigation.carbonSegmentedControl?.imageNormalColor = .clear
            carbonTabSwipeNavigation.carbonSegmentedControl?.imageSelectedColor = .clear
            
            let segmentWidth = self.borrowersArray.count > 1 ? self.tabView.frame.width / 2 : self.tabView.frame.width
            
            let indicator = carbonTabSwipeNavigation.carbonSegmentedControl?.indicator
            let subView = UIView()
            subView.backgroundColor = Theme.getButtonBlueColor()
            subView.roundOnlyTopCorners(radius: 4)
            indicator?.addSubview(subView)
            subView.translatesAutoresizingMaskIntoConstraints = false
            if (self.borrowersArray.count > 1){
                subView.widthAnchor.constraint(equalToConstant: segmentWidth * 0.8).isActive = true
            }
            else{
                subView.widthAnchor.constraint(equalToConstant: segmentWidth * 0.9).isActive = true
            }
            subView.centerXAnchor.constraint(equalTo: indicator!.centerXAnchor, constant: 0).isActive = true
            subView.topAnchor.constraint(equalTo: indicator!.topAnchor, constant: 0).isActive = true
            subView.bottomAnchor.constraint(equalTo: indicator!.bottomAnchor, constant: 0).isActive = true
            for i in 0..<self.borrowersArray.count{
                carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: i)
            }
            carbonTabSwipeNavigation.insert(intoRootViewController: self, andTargetView: self.tabView)
            
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.goBack()
    }
    
}

extension AssetsViewController: CarbonTabSwipeNavigationDelegate{
    
    func carbonTabSwipeNavigation(_ carbonTabSwipeNavigation: CarbonTabSwipeNavigation, viewControllerAt index: UInt) -> UIViewController {
        let vc = Utility.getAssetsDetailVC()
        vc.loanApplicationId = self.loanApplicationId
        vc.borrowerId = borrowersArray[Int(index)].borrowerId
        vc.delegate = self
        return vc
    }
    
}

extension AssetsViewController: AssetsDetailViewControllerDelegate{
    func getBorrowerTotalAssets(totalAssets: String) {
        self.lblTotalAssets.text = totalAssets
    }
}

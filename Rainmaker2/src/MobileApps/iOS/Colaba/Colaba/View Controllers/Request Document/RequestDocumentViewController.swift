//
//  RequestDocumentViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/10/2021.
//

import UIKit
import CarbonKit

class RequestDocumentViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var tabView: UIView!
    @IBOutlet weak var btnNext: ColabaButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupHeaderAndFooter()
    }
    
    //MARK:- Methods and Actions
    
    func setupHeaderAndFooter(){
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.03) {
            let tabItems = ["Document Templates", "Document List"]
            let carbonTabSwipeNavigation = CarbonTabSwipeNavigation(items: tabItems, delegate: self)
            
            carbonTabSwipeNavigation.carbonSegmentedControl?.backgroundColor = Theme.getDashboardBackgroundColor()
            carbonTabSwipeNavigation.setTabBarHeight(50)
            carbonTabSwipeNavigation.setIndicatorColor(nil)
            carbonTabSwipeNavigation.setIndicatorHeight(4)
            carbonTabSwipeNavigation.setNormalColor(Theme.getAppGreyColor(), font: Theme.getRubikRegularFont(size: 15))
            carbonTabSwipeNavigation.setSelectedColor(Theme.getButtonBlueColor(), font: Theme.getRubikRegularFont(size: 15))
            carbonTabSwipeNavigation.carbonSegmentedControl?.imageNormalColor = .clear
            carbonTabSwipeNavigation.carbonSegmentedControl?.imageSelectedColor = .clear
            
            let segmentWidth = (self.tabView.frame.width / 2)
            
            let indicator = carbonTabSwipeNavigation.carbonSegmentedControl?.indicator
            let subView = UIView()
            subView.backgroundColor = Theme.getButtonBlueColor()
            subView.roundOnlyTopCorners(radius: 4)
            indicator?.addSubview(subView)
            subView.translatesAutoresizingMaskIntoConstraints = false
            subView.widthAnchor.constraint(equalToConstant: segmentWidth * 0.85).isActive = true
            subView.centerXAnchor.constraint(equalTo: indicator!.centerXAnchor, constant: 0).isActive = true
            subView.topAnchor.constraint(equalTo: indicator!.topAnchor, constant: 0).isActive = true
            subView.bottomAnchor.constraint(equalTo: indicator!.bottomAnchor, constant: 0).isActive = true
            carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 0)
            carbonTabSwipeNavigation.carbonSegmentedControl?.setWidth(segmentWidth, forSegmentAt: 1)
            carbonTabSwipeNavigation.insert(intoRootViewController: self, andTargetView: self.tabView)
        }
        btnNext.setButton(image: UIImage(named: "NextIcon")!)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.dismissVC()
    }
    
    @IBAction func btnNextTapped(_ sender: UIButton){
        let vc = Utility.getDocumentsTypeVC()
        self.pushToVC(vc: vc)
    }
    
}

extension RequestDocumentViewController: CarbonTabSwipeNavigationDelegate{
    
    func carbonTabSwipeNavigation(_ carbonTabSwipeNavigation: CarbonTabSwipeNavigation, viewControllerAt index: UInt) -> UIViewController {
        return index == 0 ? Utility.getDocumentTemplatesVC() : Utility.getDocumentsListVC()
    }
    
}

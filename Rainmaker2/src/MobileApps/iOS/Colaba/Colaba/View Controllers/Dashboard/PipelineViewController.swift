//
//  PipelineViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/06/2021.
//

import UIKit

class PipelineViewController: BaseViewController {

    //MARK:- Outlets and properties
    @IBOutlet weak var assignToMeSwitch: UISwitch!
    @IBOutlet weak var btnFilters: UIButton!
    @IBOutlet weak var tblView: UITableView!
    
    var expandableCellsIndex = [Int]()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        tblView.register(UINib(nibName: "PipelineTableViewCell", bundle: nil), forCellReuseIdentifier: "PipelineTableViewCell")
        tblView.register(UINib(nibName: "PipelineDetailTableViewCell", bundle: nil), forCellReuseIdentifier: "PipelineDetailTableViewCell")
    }
    
    //MARK:- Methods and Actions
    @IBAction func btnFilterTapped(_ sender: UIButton) {
        let vc = Utility.getFiltersVC()
        self.presentVC(vc: vc)
    }
    
    @IBAction func assignToMeSwitchChanged(_ sender: UISwitch) {
        
    }
    
}

extension PipelineViewController: UITableViewDataSource, UITableViewDelegate{
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return 10
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        if expandableCellsIndex.contains(section){
            return 2
        }
        else{
            return 1
        }
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        if (indexPath.row == 0){
            let cell = tableView.dequeueReusableCell(withIdentifier: "PipelineTableViewCell", for: indexPath) as! PipelineTableViewCell
            
            cell.mainViewHeightConstraint.constant = indexPath.section == 2 ? 123 : 145
            cell.btnArrowBottomConstraint.constant = indexPath.section == 2 ? 15 : 11
            cell.updateConstraintsIfNeeded()
            cell.layoutSubviews()
            cell.mainView.layer.cornerRadius = 8
            cell.mainView.dropShadow()
            cell.indexPath = indexPath
            cell.delegate = self
            cell.lblUsername.text = indexPath.section % 2 == 0 ? "Richard Glenn Randal" : "Jenifer Moore"
            cell.lblMoreUsers.text = indexPath.section % 2 == 0 ? "+2" : ""
            cell.lblTime.text = indexPath.section % 2 == 0 ? "1 min ago" : "23 hours ago"
            cell.typeIcon.image = UIImage(named: indexPath.section % 2 == 0 ? "RefinanceIcon" : "PurchaseIcon")
            cell.lblType.text = indexPath.section % 2 == 0 ? "Refinance" : "Purchase"
            cell.lblStatus.text = indexPath.section % 2 == 0 ? "Application Started" : "Approved with Conditions"
            cell.lblDocuments.text = indexPath.section % 2 == 0 ? "7 Documents to Review" : "2 Documents to Review"
            cell.lblDocuments.isHidden = indexPath.section == 2
            cell.bntArrow.setImage(UIImage(named: expandableCellsIndex.contains(indexPath.section) ? "ArrowUp" : "ArrowDown"), for: .normal)
            cell.emptyView.isHidden = !expandableCellsIndex.contains(indexPath.section)
            return cell
        }
        else{
            let cell = tableView.dequeueReusableCell(withIdentifier: "PipelineDetailTableViewCell", for: indexPath) as! PipelineDetailTableViewCell
            cell.mainView.layer.cornerRadius = 8
            cell.mainView.dropShadow()
            return cell
        }
        
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (indexPath.section == 2){
            if (indexPath.row == 0){
                return expandableCellsIndex.contains(indexPath.section) ? 140 : 145
            }
            else{
                return 165
            }
        }
        else{
            if (indexPath.row == 0){
                return expandableCellsIndex.contains(indexPath.section) ? 165 : 170
            }
            else{
                return 165
            }
        }
        
    }
}

extension PipelineViewController: PipelineTableViewCellDelegate{
    
    func btnOptionsTapped(indexPath: IndexPath) {
        let vc = Utility.getPipelineMoreVC()
        self.presentVC(vc: vc)
    }
    
    func btnArrowTapped(indexPath: IndexPath) {
        
        if (expandableCellsIndex.contains(indexPath.section)){
            if let index = expandableCellsIndex.firstIndex(of: indexPath.section){
                expandableCellsIndex.remove(at: index)
            }
        }
        else{
            expandableCellsIndex.append(indexPath.section)
        }
        self.tblView.reloadData()
    }
}

//
//  PipelineViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/06/2021.
//

import UIKit

class PipelineViewController: UIViewController {

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
            
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                if (self.expandableCellsIndex.contains(indexPath.section)){
                   // cell.mainView.roundOnlyTopCorners(radius: 8)
                    cell.mainView.layer.cornerRadius = 8
                    cell.mainView.addShadow()
                }
                else{
                    cell.mainView.layer.cornerRadius = 8
                    cell.mainView.addShadow()
                }
            }
            
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
            return cell
        }
        else{
            let cell = tableView.dequeueReusableCell(withIdentifier: "PipelineDetailTableViewCell", for: indexPath) as! PipelineDetailTableViewCell
            return cell
        }
        
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (indexPath.section == 2){
            
            return indexPath.row == 0 ? 140 : 153
        }
        else{
            if (indexPath.row == 0){
                return 165
            }
            else{
                return 153
            }
        }
        
    }
}

extension PipelineViewController: PipelineTableViewCellDelegate{
    
    func btnOptionsTapped(indexPath: IndexPath) {
//        let vc = Utility.getPipelineMoreVC()
//        self.presentVC(vc: vc)
    }
    
    func btnArrowTapped(indexPath: IndexPath) {
        
//        if (expandableCellsIndex.contains(indexPath.section)){
//            if let index = expandableCellsIndex.firstIndex(of: indexPath.section){
//                expandableCellsIndex.remove(at: index)
//            }
//        }
//        else{
//            expandableCellsIndex.append(indexPath.section)
//        }
//        self.tblView.reloadData()
    }
}

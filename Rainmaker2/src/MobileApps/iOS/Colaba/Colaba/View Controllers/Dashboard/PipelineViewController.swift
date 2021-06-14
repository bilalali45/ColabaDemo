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
    @IBOutlet weak var btnNew: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
       
        btnNew.roundButtonWithShadow()
        tblView.register(UINib(nibName: "PipelineTableViewCell", bundle: nil), forCellReuseIdentifier: "PipelineTableViewCell")
        
    }
    
    //MARK:- Methods and Actions
    @IBAction func btnFilterTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func assignToMeSwitchChanged(_ sender: UISwitch) {
        
    }
    
    @IBAction func btnNewTapped(_ sender: UIButton){
        
    }
    
}

extension PipelineViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return 10
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "PipelineTableViewCell", for: indexPath) as! PipelineTableViewCell
        cell.lblUsername.text = indexPath.row % 2 == 0 ? "Richard Glenn Randal" : "Jenifer Moore"
        cell.lblMoreUsers.text = indexPath.row % 2 == 0 ? "+2" : ""
        cell.lblTime.text = indexPath.row % 2 == 0 ? "1 min ago" : "23 hours ago"
        cell.typeIcon.image = UIImage(named: indexPath.row % 2 == 0 ? "RefinanceIcon" : "PurchaseIcon")
        cell.lblType.text = indexPath.row % 2 == 0 ? "Refinance" : "Purchase"
        cell.lblStatus.text = indexPath.row % 2 == 0 ? "Application Started" : "Approved with Conditions"
        cell.lblDocuments.text = indexPath.row % 2 == 0 ? "7 Documents to Review" : "2 Documents to Review"
        cell.lblDocuments.isHidden = indexPath.row == 2
        return cell
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        if (indexPath.row == 2){
            return 140
        }
        else{
            return 165
        }
        
    }
}

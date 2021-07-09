//
//  OverviewViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/07/2021.
//

import UIKit

class OverviewViewController: BaseViewController {

    @IBOutlet weak var tableViewOverView: UITableView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        tableViewOverView.register(UINib(nibName: "BorrowerOverviewTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerOverviewTableViewCell")
        tableViewOverView.register(UINib(nibName: "BorrowerAddressTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerAddressTableViewCell")
        tableViewOverView.register(UINib(nibName: "BorrowerLoanInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerLoanInfoTableViewCell")
        
    }
    
}

extension OverviewViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return 3
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        if (indexPath.row == 0){
            let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerOverviewTableViewCell", for: indexPath) as! BorrowerOverviewTableViewCell
            return cell
        }
        else if (indexPath.row == 1){
            let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerAddressTableViewCell", for: indexPath) as! BorrowerAddressTableViewCell
            return cell
        }
        else{
            let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerLoanInfoTableViewCell", for: indexPath) as! BorrowerLoanInfoTableViewCell
            return cell
        }
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (indexPath.row == 2){
            return 107
        }
        else{
            return UITableView.automaticDimension
        }
        
    }
}

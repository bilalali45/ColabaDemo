//
//  BorrowerOverviewTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 09/07/2021.
//

import UIKit
import MarqueeLabel

protocol BorrowerOverviewTableViewCellDelegate: AnyObject {
    func applicationStatusViewTapped()
}

class BorrowerOverviewTableViewCell: UITableViewCell {

    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var lblCoBorrowerName: MarqueeLabel!
    @IBOutlet weak var lblCoBorrowerHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblCoBorrowerTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var applicationStatusView: UIView!
    @IBOutlet weak var applicationStatusViewHeightConstraint: NSLayoutConstraint! //116 and 61
    @IBOutlet weak var lblApplicationStatus: UILabel!
    @IBOutlet weak var applicationStatusSeparatorView: UIView!
    @IBOutlet weak var lblLoanNo: UILabel!
    @IBOutlet weak var iconByte: UIImageView!
    @IBOutlet weak var lblByte: UILabel!
    
    weak var delegate: BorrowerOverviewTableViewCellDelegate?
    
    override func awakeFromNib() {
        super.awakeFromNib()
        
        applicationStatusView.layer.cornerRadius = 6
        applicationStatusView.layer.borderWidth = 1
        applicationStatusView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
        applicationStatusView.backgroundColor = .white
        applicationStatusView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(applicationStatusTapped)))
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
    @objc func applicationStatusTapped(){
        self.delegate?.applicationStatusViewTapped()
    }
    
}

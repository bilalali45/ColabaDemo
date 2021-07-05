//
//  PipelineDetailTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/06/2021.
//

import UIKit

class PipelineDetailTableViewCell: UITableViewCell {

    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var addressIcon: UIImageView!
    @IBOutlet weak var lblPropertyAddress: UILabel!
    @IBOutlet weak var loanIcon: UIImageView!
    @IBOutlet weak var lblLoanAmount: UILabel!
    @IBOutlet weak var propertyIcon: UIImageView!
    @IBOutlet weak var lblPropertyValue: UILabel!
    @IBOutlet weak var bottomSeperator: UIView!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        
        let renderRect = CGRect(origin: .zero, size: CGSize(width: 90, height: 90))
        let renderer = UIGraphicsImageRenderer(size: renderRect.size)
        
        bottomSeperator.layer.cornerRadius = bottomSeperator.frame.height / 2
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}

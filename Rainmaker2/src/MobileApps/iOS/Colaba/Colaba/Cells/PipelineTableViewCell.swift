//
//  PipelineTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/06/2021.
//

import UIKit

class PipelineTableViewCell: UITableViewCell {

    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var lblMoreUsers: UILabel!
    @IBOutlet weak var lblTime: UILabel!
    @IBOutlet weak var btnOptions: UIButton!
    @IBOutlet weak var typeIcon: UIImageView!
    @IBOutlet weak var lblType: UILabel!
    @IBOutlet weak var lblStatus: UILabel!
    @IBOutlet weak var lblCheck: UILabel!
    @IBOutlet weak var lblDocuments: UILabel!
    @IBOutlet weak var bntArrow: UIButton!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
        mainView.layer.cornerRadius = 8
        mainView.addShadow()
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
    @IBAction func btnMoreTapped(_ sender: UIButton) {
    }
    
    @IBAction func btnArrowTapped(_ sender: UIButton) {
    }
    
}

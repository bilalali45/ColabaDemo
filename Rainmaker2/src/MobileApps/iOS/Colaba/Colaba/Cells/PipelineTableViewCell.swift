//
//  PipelineTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/06/2021.
//

import UIKit

protocol PipelineTableViewCellDelegate: AnyObject {
    func btnOptionsTapped(indexPath: IndexPath)
    func btnArrowTapped(indexPath: IndexPath)
}

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
    
    weak var delegate: PipelineTableViewCellDelegate?
    var indexPath = IndexPath()
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
        
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
    @IBAction func btnMoreTapped(_ sender: UIButton) {
        self.delegate?.btnOptionsTapped(indexPath: indexPath)
    }
    
    @IBAction func btnArrowTapped(_ sender: UIButton) {
        self.delegate?.btnArrowTapped(indexPath: indexPath)
    }
    
}

//
//  GovernmentQuestionsCollectionViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 20/07/2021.
//

import UIKit

class GovernmentQuestionsCollectionViewCell: UICollectionViewCell {

    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblQuestionHeading: UILabel!
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var iconAns1: UIImageView!
    @IBOutlet weak var lblAns1: UILabel!
    @IBOutlet weak var lblUser1: UILabel!
    @IBOutlet weak var iconAns2: UIImageView!
    @IBOutlet weak var lblAns2: UILabel!
    @IBOutlet weak var lblUser2: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

}
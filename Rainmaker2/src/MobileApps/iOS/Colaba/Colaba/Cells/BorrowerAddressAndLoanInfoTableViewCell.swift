//
//  BorrowerAddressAndLoanInfoTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 26/07/2021.
//

import UIKit

class BorrowerAddressAndLoanInfoTableViewCell: UITableViewCell {

    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblLoanPurpose: UILabel!
    @IBOutlet weak var lblLoanPurposeTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblLoanType: UILabel!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var lblPropertyType: UILabel!
    @IBOutlet weak var loanDetailStackView: UIStackView!
    @IBOutlet weak var propertyValueView: UIView!
    @IBOutlet weak var lblPropertyValue: UILabel!
    @IBOutlet weak var loanAmountView: UIView!
    @IBOutlet weak var lblLoanAmount: UILabel!
    @IBOutlet weak var downPaymentView: UIView!
    @IBOutlet weak var lblDownPayment: UILabel!
    @IBOutlet weak var lblDownPaymentPercentage: UILabel!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var bottomDownPaymentView: UIView!
    @IBOutlet weak var lblBottomDownPayment: UILabel!
    @IBOutlet weak var lblBottomDownPaymentPercentage: UILabel!
    @IBOutlet weak var mapIconTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblAddressTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var emptyStateLoanPurposeView: UIView!
    @IBOutlet weak var lblEmptyStateLoanPurpose: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}

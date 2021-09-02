//
//  BaseView.swift
//  Colaba
//
//  Created by Salman ~Khan on 31/08/2021.
//

import UIKit

public class BaseView: UIView, NibLoadable {
    var nibName: String!
    override init(frame: CGRect) {
        super.init(frame: frame)
      }
    
    //initWithCode to init view from xib or storyboard
    required init?(coder aDecoder: NSCoder) {
      super.init(coder: aDecoder)
    }
}

protocol NibLoadable {
    var nibName: String! { get set }
}

extension NibLoadable where Self: BaseView {

    var nib: UINib {
        return UINib(nibName: nibName, bundle: nil)
    }

    func setupFromNib() {
        guard let view = nib.instantiate(withOwner: self, options: nil).first as? UIView else { fatalError("Error loading \(self) from nib") }
        addSubview(view)
        view.translatesAutoresizingMaskIntoConstraints = false
        view.leadingAnchor.constraint(equalTo: self.safeAreaLayoutGuide.leadingAnchor, constant: 0).isActive = true
        view.topAnchor.constraint(equalTo: self.safeAreaLayoutGuide.topAnchor, constant: 0).isActive = true
        view.trailingAnchor.constraint(equalTo: self.safeAreaLayoutGuide.trailingAnchor, constant: 0).isActive = true
        view.bottomAnchor.constraint(equalTo: self.safeAreaLayoutGuide.bottomAnchor, constant: 0).isActive = true
    }
}

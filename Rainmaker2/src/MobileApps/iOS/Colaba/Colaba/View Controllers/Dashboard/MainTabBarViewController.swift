//
//  MainTabBarViewController.swift
//  Colaba
//
//  Created by Murtaza on 11/06/2021.
//

import UIKit

class MainTabBarViewController: UITabBarController {

    override func viewDidLoad() {
        super.viewDidLoad()
        self.tabBar.items![1].badgeValue = "1"
    }
   
}

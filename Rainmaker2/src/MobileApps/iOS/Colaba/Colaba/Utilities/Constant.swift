//
//  Constant.swift
//  Colaba
//
//  Created by Murtaza on 19/05/2021.
//

import Foundation

let kDeviceNotRegistered = "Device is not registered with Biometric login. Please setup it from settings."
let kFaceID = "FaceID"
let kTouchID = "TouchID"
let kYes = "YES"
let kNo = "NO"
let kFaceIdResetPopup = "This will reset your face Id verification. Do you want to continue?"
let kFingerIdResetPopup = "This will reset your finger print verification. Do you want to continue?"
let BASEURL = "https://devmobilegateway.rainsoftfn.com:5002/api/mcu/mobile/"
let kResendEnableTimeStamp = "kResendEnableTimeStamp"
var isBiometricAllow = false
var shouldShowSkipButton = false
var totalCodeLimit = 5
let kIsUserRegisteredWithBiometric = "UserRegisteredWithBiometric"
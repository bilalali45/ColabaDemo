package com.rnsoft.colabademo

class GovtScreenUpdateEvent(val detailTitle: String = "Detail", val detailDescription:String = " ")

class UndisclosedBorrowerFundUpdateEvent(val detailTitle: String , val detailDescription:String)

class OwnershipInterestUpdateEvent(val question1: String  , val answer1:String , val index1:Int,  val question2: String  , val answer2:String, val index2:Int  )

class ChildSupportUpdateEvent(val childAnswerList:ArrayList<ChildAnswerData>)

class BankruptcyUpdateEvent(val detailTitle: String = "Which Type?", val detailDescription:String )


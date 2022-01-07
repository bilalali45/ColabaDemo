package com.rnsoft.colabademo.activities.details.model

data class Address(
    val city: String,
    val countryId: Int,
    val countryName: String,
    val countyId: Any,
    val countyName: String,
    val stateId: Any,
    val stateName: String,
    val street: String,
    val unit: Any,
    val zipCode: String
)
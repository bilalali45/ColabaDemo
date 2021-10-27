package com.rnsoft.colabademo

/**
 * Created by Anita Kiran on 10/26/2021.
 */
data class EthinicityResponseModel(
    val ethnicityDetails: List<EthinicityDetails>
)

data class EthinicityDetails(
    val id: Int?,
    val isOther : Boolean?,
    val name: String?,
    val otherPlaceHolder: String?,
)


package com.rnsoft.colabademo

import com.rnsoft.colabademo.Result

class WebServiceErrorEvent(val errorResult: Result.Error?=null, val isInternetError:Boolean = false)
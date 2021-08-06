package com.rnsoft.colabademo

import android.content.Context
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.widget.TextView
import androidx.annotation.IdRes
import com.google.android.material.bottomnavigation.BottomNavigationItemView
import com.google.android.material.bottomnavigation.BottomNavigationView
import java.text.DecimalFormat
import java.text.SimpleDateFormat
import java.util.*
import kotlin.math.roundToInt


object AppSetting {

    var biometricEnabled: Boolean = false
    var loanApiDateTime: String = ""
    var activeloanApiDateTime: String = ""
    var nonActiveloanApiDateTime: String = ""

    // check in Repo to query Room database....
    var hasLoanApiDataLoaded = false
    var hasActiveLoanApiDataLoaded = false
    var hasNonActiveLoanApiDataLoaded = false

    fun returnGreetingString(): String {
        val currentTimeAgain: String =
            SimpleDateFormat("HH:mm", Locale.getDefault()).format(Date())
        Log.e("currentTimeAgain-time-", currentTimeAgain)

        var greetingString = ""

        val timeInArray = currentTimeAgain.split(":").map { it.toInt() }
        if (timeInArray[0] < 12)
            greetingString = "Good Morning"
        else if (timeInArray[0] in 13..16)
            greetingString = "Good Afternoon"
        else
            greetingString = "Good Evening"

        return greetingString
    }

    fun getUploadedDate(fileUploaded: String, docName: String) : String{

        var output: String = ""
        var trim: String
        if (fileUploaded.contains(":Z")) {
            trim = fileUploaded.substring(0, fileUploaded.length - 2)
        } else
            trim = fileUploaded.substring(0, fileUploaded.length - 4)

        Log.e(" Doc Uploaded Date: ", trim)

        val formatter = SimpleDateFormat("yyyy-MM-dd HH:mm:ss")
        val firstDate: Date? = formatter.parse(trim)

        //var currentTime = Date()
        //val currentDate : String = formatter.format(currentTime)
        //val secondDate : Date? = formatter.parse(currentDate)
        //Log.e("first Date", ""+firstDate)
        //Log.e("second Date", ""+secondDate)
        //val difference : Long = Date(firstDate - secondDate)
        //val cal = Calendar.getInstance()
        //cal.time = diff


        return output

    }

    fun getDocumentUploadedDate(fileUploaded: String, docName: String): String {

        var output: String = ""
        var trim: String
        if (fileUploaded.contains(":Z")) {
            trim = fileUploaded.substring(0, fileUploaded.length - 2)
        } else
            trim = fileUploaded.substring(0, fileUploaded.length - 4)

        Log.e(" Doc Uploaded Date: ", trim)

        /*val formatter = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.getDefault())
        val firstDate: Date? = formatter.parse(trim)

        var currentTime = Date()
        val currentDate : String = formatter.format(currentTime)
        val secondDate : Date? = formatter.parse(currentDate)
        Log.e("first Date", ""+firstDate)
        Log.e("second Date", ""+secondDate)
        //val difference : Long = Date(firstDate - secondDate)
        val cal = Calendar.getInstance()
        //cal.time = diff */
        val format1 = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.US)
        val dt1 :Date = format1.parse(trim)

        val cal = Calendar.getInstance()
        cal.time= dt1
        var uploadedDay = cal[Calendar.DAY_OF_MONTH]
        var uploadedMonth = cal[Calendar.MONTH]
        var uploadedYear = cal[Calendar.YEAR]
        var uploadedMinutes = cal[Calendar.MINUTE]
        var uploadedHour = cal[Calendar.HOUR]
        var uploadedSec = cal[Calendar.SECOND]
        var uploadedWeek = cal[Calendar.WEEK_OF_YEAR]

        Log.e(
            docName,
            "Uploaded min: " + uploadedMinutes + " Hour: " + uploadedHour + " uploadedSec: " + uploadedSec + " days: " +
                    "Day: " + uploadedDay + " Month: " + uploadedMonth + " Year: " + uploadedYear + " Week: " + uploadedWeek
        )

        val now = Calendar.getInstance()
        //var now : LocalDateTime =LocalDateTime.now()
        val currentHour = now[Calendar.HOUR]
        val currentMinutes = now[Calendar.MINUTE]
        val currentSeconds = now[Calendar.SECOND]
        val currentDay = now[Calendar.DAY_OF_MONTH]
        val currentMonth = now[Calendar.MONTH]
        val currentYear = now[Calendar.YEAR]

        Log.e(
            " CurrentHour",
            "" + currentHour + " CurrentMinute: " + currentMinutes + " Seconds: " + currentSeconds + " CurrentDay: " + currentDay +
                    " CurrentMonth: " + currentMonth + "  CurrentYear: " + currentYear)

        val day = uploadedDay - now[Calendar.DAY_OF_MONTH]
        val year = uploadedYear - now[Calendar.YEAR]
        val month = uploadedMonth - now[Calendar.MONTH]
        val hour = uploadedHour - now[Calendar.HOUR]
        val min = uploadedMinutes - now[Calendar.MINUTE]
        val sec = uploadedSec - now[Calendar.SECOND]

        if (Math.abs(year) >= 2) {
            output = Math.abs(year).toString().plus(" years ago")
            return output
        }
        if (Math.abs(year) >= 1) {
            output = "Last year"
            return output
        }
        if (Math.abs(month + 1) >= 2) {
            output = Math.abs(month + 1).toString().plus(" months ago")
            return output
        }
        if (Math.abs(month + 1) >= 1) {
            output = "Last month"
            return output
        }
        if (Math.abs(day) >= 2) {
            output = Math.abs(day).toString().plus(" days ago")
        }
        if (Math.abs(day) <= 1) {
            output = "Yesterday"
        }
        if (Math.abs(hour) >= 2) {
            output = Math.abs(hour).toString().plus(" hours ago")
            return output
        }
        if (Math.abs(hour) >= 1) {
            output = "1 hour ago"
            return output
        }
        if (Math.abs(min) >= 2) {
            output = Math.abs(min).toString().plus(" minutes ago")
            return output
        }
        if (Math.abs(min) >= 1) {
            output = "1 minute ago"
            return output
        }
        if (Math.abs(sec) >= 3) {
            output = Math.abs(sec).toString().plus("seconds ago")
            return output
        }
        if (Math.abs(sec) < 3) {
            output = "Just now"
            return output
        }

        return output
    }

    fun returnLongTimeNow(input: String): String {

        var lastSeen = input

        if (input.contains(":Z"))
            lastSeen = input.substring(0, input.length - 2).toString()
        else
            lastSeen = input.substring(0, input.length - 4)

        //Log.e("input-time--", lastSeen)

        //val formatter = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.US)
        val formatter = SimpleDateFormat("yyyy-MM-dd'T'HH:mm", Locale.US)
        val oldDate: Date? = formatter.parse(lastSeen)


        val oldMillis = oldDate?.time

        oldMillis?.let {
            //Log.e("oldMillis", "Date in milli :: FOR API >= 26 >>> $oldMillis")
            //Log.e("lastseen", "Date in milli :: FOR API >= 26 >>>"+ lastseen(oldMillis))
            lastSeen = lastseen(oldMillis)
        }

        return lastSeen

    }

    fun returnAmountFormattedString(amount: Double): String {
        val df2 = DecimalFormat()
        df2.maximumFractionDigits = 0
        Log.e("new-deci-format", df2.format(amount).toString())
        return df2.format(amount).toString()
    }

    fun returnNotificationTime(input: String): String {
        var lastSeen = input
        // "2021-07-02T11:43:07.205Z"
        val formatter = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.US)
        val oldDate: Date? = formatter.parse(input)
        val oldMillis = oldDate?.time
        oldMillis?.let {
            lastSeen = lastseen(oldMillis)
        }

        return lastSeen
    }

    fun documentDetailDateTimeFormat(input: String): String {
        var receivedTimeString = input
        receivedTimeString = if (input.contains(":Z"))
            input.substring(0, input.length - 2).toString()
        else
            input.substring(0, input.length - 4)

        val formatter = SimpleDateFormat("yyyy-MM-dd'T'HH:mm", Locale.US)
        val oldDate: Date? = formatter.parse(receivedTimeString)
        val oldMillis = oldDate?.time
        var finalTimeInFormat = ""

        //val dateFormatter = SimpleDateFormat("E MMM dd,yyyy hh:mm a");
        // System.out.println("Format 2:   " + dateFormatter.format(now));
        oldMillis?.let {
            finalTimeInFormat = getDate(it, "E MMM dd,yyyy hh:mm a")
        }

        return finalTimeInFormat
    }

    fun getDate(milliSeconds: Long, dateFormat: String?): String {
        // Create a DateFormatter object for displaying date in specified format.
        val formatter = SimpleDateFormat(dateFormat)

        // Create a calendar object that will convert the date and time value in milliseconds to date.
        val calendar = Calendar.getInstance()
        calendar.timeInMillis = milliSeconds
        return formatter.format(calendar.time)
    }

    fun lastseen(time: Long): String {
        val difference = (System.currentTimeMillis() - time) / 1000

        return if (difference < 60) {
            "just now"
        } else if (difference < 60 * 2) {
            "1 minute ago"
        } else if (difference < 60 * 60) {
            val minutes = (difference / 60.0).roundToInt()
            "$minutes minutes ago"
        } else if (difference < 60 * 60 * 2) {
            "1 hour ago"
        } else if (difference < 60 * 60 * 24) {
            val hours = (difference / (60.0 * 60.0)).roundToInt()
            "$hours hours ago"
        } else if (difference < 60 * 60 * 48) {
            "1 day ago"
        } else {
            val days = (difference / (60.0 * 60.0 * 24.0)).roundToInt()
            "$days days ago"
        }
    }

    fun showBadge(
        context: Context?,
        bottomNavigationView: BottomNavigationView,
        @IdRes itemId: Int,
        value: String?
    ) {
        removeBadge(bottomNavigationView, itemId)
        val itemView: BottomNavigationItemView = bottomNavigationView.findViewById(itemId)
        val badge: View = LayoutInflater.from(context)
            .inflate(R.layout.layout_news_badge, bottomNavigationView, false)
        val text: TextView = badge.findViewById(R.id.badge_text_view)
        text.text = value
        itemView.addView(badge)
    }

    fun removeBadge(bottomNavigationView: BottomNavigationView, @IdRes itemId: Int) {
        val itemView: BottomNavigationItemView = bottomNavigationView.findViewById(itemId)
        if (itemView.childCount == 3) {
            itemView.removeViewAt(2)
        }
    }
/*
    fun isDateInCurrentWeek(date: Date?): Boolean {
        val currentCalendar = Calendar.getInstance()
        val week = currentCalendar[Calendar.WEEK_OF_YEAR]
        val year = currentCalendar[Calendar.YEAR]
        val targetCalendar = Calendar.getInstance()
        targetCalendar.time = date
        val targetWeek = targetCalendar[Calendar.WEEK_OF_YEAR]
        val targetYear = targetCalendar[Calendar.YEAR]
        return week == targetWeek && year == targetYear
    }


    fun getMilliFromDate(dateFormat: String?): Long {
        var date = Date()
        val formatter = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'")
        try {
            date = formatter.parse(dateFormat)
        } catch (e: Exception) {
            e.printStackTrace()
        }
        println("Today is $date")
        return date.time
    }



    //1 minute = 60 seconds
    //1 hour = 60 x 60 = 3600
    //1 day = 3600 x 24 = 86400
    fun getTimeDifference(startDate: Long, endDate: Long): String? {
        //milliseconds
        var different = endDate - startDate

        //        System.out.println("startDate : " + startDate);
        //        System.out.println("endDate : " + endDate);
        //        System.out.println("different : " + different);
        val secondsInMilli: Long = 1000
        val minutesInMilli = secondsInMilli * 60
        val hoursInMilli = minutesInMilli * 60
        val daysInMilli = hoursInMilli * 24
        val elapsedDays = different / daysInMilli
        different = different % daysInMilli
        val elapsedHours = different / hoursInMilli
        different = different % hoursInMilli
        val elapsedMinutes = different / minutesInMilli
        different = different % minutesInMilli
        val elapsedSeconds = different / secondsInMilli

        //        System.out.printf("%d days, %d hours, %d minutes, %d seconds%n",
        //                elapsedDays, elapsedHours, elapsedMinutes, elapsedSeconds);
        return if (elapsedDays > 0) {
            MessageFormat.format(
                "{0}d {1}h {2}m {3}s",
                elapsedDays,
                elapsedHours,
                elapsedMinutes,
                if (elapsedSeconds > 0) elapsedSeconds else 0
            )
        } else if (elapsedHours > 0) {
            MessageFormat.format(
                "{0}h {1}m {2}s",
                elapsedHours,
                elapsedMinutes,
                if (elapsedSeconds > 0) elapsedSeconds else 0
            )
        } else if (elapsedMinutes > 0) {
            MessageFormat.format(
                "{0}m {1}s",
                elapsedMinutes,
                if (elapsedSeconds > 0) elapsedSeconds else 0
            )
        } else {
            MessageFormat.format("{0} s", if (elapsedSeconds > 0) elapsedSeconds else 0)
        }
    }

     */

}
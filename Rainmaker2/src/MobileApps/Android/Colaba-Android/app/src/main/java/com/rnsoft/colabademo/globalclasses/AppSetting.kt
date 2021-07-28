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

    var biometricEnabled:Boolean = false

    var loanApiDateTime:String = ""
    var activeloanApiDateTime:String = ""
    var nonActiveloanApiDateTime:String = ""



    //////////////////////////////////////////////
    // check in Repo to query Room database....
    var hasLoanApiDataLoaded = false
    var hasActiveLoanApiDataLoaded = false
    var hasNonActiveLoanApiDataLoaded = false

    fun returnGreetingString():String{
        val currentTimeAgain: String =
            SimpleDateFormat("HH:mm", Locale.getDefault()).format(Date())
        Log.e("currentTimeAgain-time-", currentTimeAgain)

        var greetingString = ""

        val timeInArray = currentTimeAgain.split(":").map { it.toInt() }
        if(timeInArray[0]<12 )
            greetingString = "Good Morning"
        else if(timeInArray[0] in 13..16)
            greetingString = "Good Afternoon"
        else
            greetingString = "Good Evening"

        return greetingString
    }




    fun returnLongTimeNow(input:String):String{

        var lastSeen = input

        val formatter = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'", Locale.US)
        val oldDate: Date? = formatter.parse(input)


        val oldMillis = oldDate?.time

        oldMillis?.let {
            //Log.e("oldMillis", "Date in milli :: FOR API >= 26 >>> $oldMillis")
            //Log.e("lastseen", "Date in milli :: FOR API >= 26 >>>"+ lastseen(oldMillis))
            lastSeen = lastseen(oldMillis)
        }

        return lastSeen

    }

    fun returnAmountFormattedString(amount:Double){

        val df2 = DecimalFormat("###,###,###,###")
        val dd = 200100.2397
        val dd2dec: Double = df2.format(dd).toDouble()
        Log.e("new-deci-format", dd2dec.toString())
    }

    fun returnNotificationTime(input:String):String{
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

    fun lastseen( time: Long): String {
        val difference = (System.currentTimeMillis() - time) / 1000

        return  if (difference < 60) {
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
    private const val SHORT_DATE_FLAGS = (DateUtils.FORMAT_SHOW_DATE
            or DateUtils.FORMAT_NO_YEAR or DateUtils.FORMAT_ABBREV_ALL)
    private const val FULL_DATE_FLAGS = (DateUtils.FORMAT_SHOW_TIME
            or DateUtils.FORMAT_ABBREV_ALL or DateUtils.FORMAT_SHOW_DATE)

    fun readableTimeDifference(context: Context, time: Long): String? {
        return readableTimeDifference(context, time, false)
    }

    fun readableTimeDifferenceFull(context: Context, time: Long): String? {
        return readableTimeDifference(context, time, true)
    }

    fun readableTimeDifference(context: Context, time: Long, fullDate: Boolean): String? {
        if (time == 0L) {
            return context.getString(R.string.just_now)
        }
        val date = Date(time)
        return if (fullDate) {
            if (today(date)) {
                DateFormat.format("hh:mm a", date).toString()
            } else {
                DateUtils.formatDateTime(context, date.time, SHORT_DATE_FLAGS)
            }
        } else {
            DateFormat.format("hh:mm a", date).toString()
        }
    }

    fun getReadableTimeDifference(context: Context, time: Long): String? {
        if (time == 0L) {
            return context.getString(R.string.just_now)
        }
        val date = Date(time)
        return if (today(date)) {
            DateFormat.format("hh:mm a", date).toString()
        } else if (isDateInCurrentWeek(date)) {
            getWeekName(time) + " - " + DateFormat.format("hh:mm a", date).toString()
        } else {
            (DateUtils.formatDateTime(context, date.time, SHORT_DATE_FLAGS) + " - "
                    + DateFormat.format("hh:mm a", date).toString())
        }
    }

    private fun today(date: Date): Boolean {
        return sameDay(date, Date(System.currentTimeMillis()))
    }

    fun today(date: Long): Boolean {
        return sameDay(date, System.currentTimeMillis())
    }

    fun sameDay(a: Long, b: Long): Boolean {
        return sameDay(Date(a), Date(b))
    }

    private fun sameDay(a: Date, b: Date): Boolean {
        val cal1 = Calendar.getInstance()
        val cal2 = Calendar.getInstance()
        cal1.time = a
        cal2.time = b
        return (cal1[Calendar.YEAR] == cal2[Calendar.YEAR]
                && cal1[Calendar.DAY_OF_YEAR] == cal2[Calendar.DAY_OF_YEAR])
    }

    private fun yesterday(date: Date): Boolean {
        return yesterday(date.time)
    }

    fun yesterday(date: Long): Boolean {
        val cal1 = Calendar.getInstance()
        val cal2 = Calendar.getInstance()
        cal1.add(Calendar.DAY_OF_YEAR, -1)
        cal2.time = Date(date)
        return (cal1[Calendar.YEAR] == cal2[Calendar.YEAR]
                && cal1[Calendar.DAY_OF_YEAR] == cal2[Calendar.DAY_OF_YEAR])
    }

    private fun getWeekName(date: Long): String {
        val c = Calendar.getInstance()
        c.timeInMillis = date
        val dayOfWeek = c[Calendar.DAY_OF_WEEK]
        var weekDay = ""
        if (Calendar.MONDAY == dayOfWeek) {
            weekDay = "Mon"
        } else if (Calendar.TUESDAY == dayOfWeek) {
            weekDay = "Tue"
        } else if (Calendar.WEDNESDAY == dayOfWeek) {
            weekDay = "Wed"
        } else if (Calendar.THURSDAY == dayOfWeek) {
            weekDay = "Thu"
        } else if (Calendar.FRIDAY == dayOfWeek) {
            weekDay = "Fri"
        } else if (Calendar.SATURDAY == dayOfWeek) {
            weekDay = "Sat"
        } else if (Calendar.SUNDAY == dayOfWeek) {
            weekDay = "Sun"
        }
        return weekDay
    }

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
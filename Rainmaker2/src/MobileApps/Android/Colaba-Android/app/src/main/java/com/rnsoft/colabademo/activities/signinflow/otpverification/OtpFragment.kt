package com.rnsoft.colabademo

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.os.CountDownTimer
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import androidx.activity.addCallback
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import com.google.gson.Gson
import com.rnsoft.colabademo.activities.signinflow.phone.events.OtpSentEvent
import dagger.hilt.android.AndroidEntryPoint
import org.greenrobot.eventbus.EventBus
import org.greenrobot.eventbus.Subscribe
import org.greenrobot.eventbus.ThreadMode
import javax.inject.Inject


@AndroidEntryPoint
class OtpFragment: Fragment() {

    private lateinit var root:View
    @Inject
    lateinit var sharedPreferences: SharedPreferences

    @Inject
    lateinit var spEditor: SharedPreferences.Editor

    private val signUpFlowActivity: SignUpFlowViewModel by activityViewModels() // Shared Repo.....

    private val otpViewModel: OtpViewModel by activityViewModels() // Shared Repo.....

    private lateinit var resendLink:TextView
    private lateinit var nearToResetTextView:TextView
    private lateinit var verifyButton:Button
    private lateinit var otpEditText:EditText
    private lateinit var cTimer:CountDownTimer
    private lateinit var insideTimerLayout:ConstraintLayout
    private lateinit var otpLoader:ProgressBar
    private lateinit var minuteTextView:TextView
    private lateinit var secondTextView:TextView
    private lateinit var otpMessageTextView:TextView
    private lateinit var notAskChekBox:CheckBox

    private var minutes:Int = 0
    private var seconds:Int = 0

    private var attemptLeft:Int = 0

    private var restoreBtnState:Boolean = false
    private var otpTextFieldState:Boolean = false

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        setHasOptionsMenu(true)
        root = inflater.inflate(R.layout.otp_screen_layout, container, false)
        verifyButton = root.findViewById<Button>(R.id.verifyBtn)
        resendLink = root.findViewById(R.id.resendTextView)
        nearToResetTextView = root.findViewById(R.id.nearToResetTextView)
        otpEditText = root.findViewById(R.id.otpCodeEditText)
        insideTimerLayout = root.findViewById(R.id.inside_timer_constraintlayout)
        otpLoader = root.findViewById(R.id.loader_otp_screen)
        minuteTextView = root.findViewById(R.id.minuteTextView)
        secondTextView = root.findViewById(R.id.secondTextView)
        otpMessageTextView = root.findViewById(R.id.timerMessageTextView)
        notAskChekBox = root.findViewById(R.id.permission_checkbox)

        //////////////////////////////////////////////////////////////////////////////////////////////
        otpEditText.addTextChangedListener(object : TextWatcher {
            override fun onTextChanged(s: CharSequence, start: Int, before: Int, count: Int) {}
            override fun beforeTextChanged(s: CharSequence, start: Int, count: Int, after: Int) {}
            override fun afterTextChanged(s: Editable) {
                val str: String = otpEditText.text.toString()
                //verifyButton.isEnabled = str.length == 6
                if(str.length == 6) {
                    otpEditText.isEnabled = false
                    disabledButtons()
                    val otpVal = otpEditText.text.toString()
                    otpViewModel.verifyOtp(otpVal.toInt())
                }
            }
        })

        resendLink.setOnClickListener {
            sharedPreferences.getString(ColabaConstant.phoneNumber,"")?.let { phoneNum->
                sharedPreferences.getString(ColabaConstant.token,"")?.let { intermediateToken ->
                    restoreBtnState = verifyButton.isEnabled
                    otpTextFieldState = otpEditText.isEnabled
                    disabledButtons()
                    signUpFlowActivity.sendOtpToPhone(intermediateToken, phoneNum)
                }
            }
        }

        verifyButton.setOnClickListener {
            if(notAskChekBox.isChecked) {
                sharedPreferences.getString(ColabaConstant.token,"")?.let {
                    disabledButtons()
                    otpViewModel.notAskForOtp(it)
                }
            }
            else
                navigateToDashBoardScreen()
        }

        checkForTimer()
        updateResendCount()

        return root
    }

    override fun onStart() {
        super.onStart()
        EventBus.getDefault().register(this)
    }

    override fun onStop() {
        super.onStop()
        EventBus.getDefault().unregister(this)
    }



    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onOtpReceivedOnPhone(event: OtpSentEvent) {
        otpLoader.visibility = View.INVISIBLE

        otpEditText.isEnabled = otpTextFieldState
        verifyButton.isEnabled = restoreBtnState

        val otpSentResponse =event.otpSentResponse
        resendLink.isEnabled = true
        updateResendCount()
        Log.e("otp-sent", otpSentResponse.toString())
        if (otpSentResponse.code == "400" && otpSentResponse.otpData!=null) {
            checkForTimer()
        }
        else
            otpSentResponse.message?.let { showToast(it) }

    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun onOtpVerificationCompleteEvent(event: OtpVerificationEvent) {
        otpLoader.visibility = View.INVISIBLE
        resendLink.isEnabled = true

        val verificationResponse = event.otpVerificationResponse
        if(verificationResponse.code == "200" &&  verificationResponse.data != null) {
            verifyButton.isEnabled = true
            requireActivity().onBackPressedDispatcher.addCallback(viewLifecycleOwner) {}
        }
        else if(verificationResponse.message!=null) {
                showToast(verificationResponse.message)
                otpEditText.isEnabled = true
        }
        else {
                showToast("Response contains no message...")
                otpEditText.isEnabled = true
        }



    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    fun notAskForOtpEventReceived(event: NotAskForOtpEvent) {
        otpLoader.visibility = View.INVISIBLE


        val notAskForOtpResponse =event.notAskForOtpResponse
        Log.e("notAskForOtpResponse==", notAskForOtpResponse.toString())
        if (notAskForOtpResponse.code == "200" && notAskForOtpResponse.status=="OK") {
            navigateToDashBoardScreen()
        }
        else {
            verifyButton.isEnabled = true
            resendLink.isEnabled = true
            notAskForOtpResponse.message?.let { showToast(it) }
        }
    }

    private fun showToast(toastMessage: String) = Toast.makeText(requireActivity().applicationContext, toastMessage, Toast.LENGTH_LONG).show()

    private fun navigateToDashBoardScreen(){
        startActivity(Intent(requireActivity(), DashBoardActivity::class.java))
        requireActivity().finish()
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private fun checkForTimer(){
        sharedPreferences.getString(ColabaConstant.otpDataJson, "").let {
            val test = sharedPreferences.getString(ColabaConstant.otpDataJson, "")
            val obj = Gson().fromJson(test, OtpData::class.java)

            if(obj!=null) {
               if (obj.remainingTimeoutInSeconds != null && obj.remainingTimeoutInSeconds>3) {
                    setUpTimerInitial(obj.remainingTimeoutInSeconds)
                    toggleTimerView(true)
                }
                else
                    toggleTimerView(false)
            }
            else
                toggleTimerView(false)
        }
    }

    private fun toggleTimerView(bool:Boolean) {
        if(bool) {
            insideTimerLayout.visibility = View.VISIBLE
            nearToResetTextView.visibility = View.GONE
            resendLink.visibility = View.GONE
        }
        else {
            insideTimerLayout.visibility = View.GONE
            nearToResetTextView.visibility = View.VISIBLE
            resendLink.visibility = View.VISIBLE
        }
        updateResendCount()
    }

    private fun updateResendCount(){
        sharedPreferences.getString(ColabaConstant.otpDataJson, "")?.let { otpDataReceived->
            val obj = Gson().fromJson(otpDataReceived, OtpData::class.java)
            if (obj != null) {
                sharedPreferences.getInt(ColabaConstant.maxOtpSendAllowed, 5).let {
                    attemptLeft = it - obj.attemptsCount
                }
            }
        }
        val attemptsLeftString = resources.getString(R.string.resend_code) + " ($attemptLeft left)"
        resendLink.text = attemptsLeftString
    }

    private fun setUpTimerInitial(remainingSeconds:Int){
        sharedPreferences.getString(ColabaConstant.otp_message , resources.getString(R.string.dummy_otp_message))?.let{ otpMessage ->
            otpMessageTextView.text  =  otpMessage
        }
        if(remainingSeconds>60){
            minutes =  (remainingSeconds / 60)
            seconds = (remainingSeconds - (minutes * 60))
            val totalSeconds:Long = (remainingSeconds * 1000).toLong()
            Log.e("all - ", "$remainingSeconds becomes $minutes min $seconds seconds")
            startTimer(totalSeconds)
        }
        else{
            minutes = 0
            seconds = remainingSeconds
            startTimer((remainingSeconds * 1000).toLong())
        }
    }

    private fun startTimer(totalSeconds:Long){
        cTimer = object : CountDownTimer(totalSeconds, 1000) {
            override fun onTick(millisUntilFinished: Long) {
                Log.e("millisUntilFinished-", millisUntilFinished.toString())
                Log.e("Minutes - ", "$minutes  seconds - $seconds")
                seconds--
                if(seconds == 0 && minutes > 0) {
                    Log.e("MinutesNow--", "Decreased")
                    minutes -= 1
                    seconds = 60
                }
                else
                if(minutes <= 0 && seconds <= 0){
                    Log.e("TimerStop", "StopNow")
                    toggleTimerView(false)
                    cancelTimer()
                }
                minuteTextView.text = "0"+minutes
                secondTextView.text = ": "+seconds.toString()
            }
            override fun onFinish() { Log.e("Timer Finished-", "Completed...") }
        }
        cTimer.start()
    }

    private fun cancelTimer() {
        cTimer.let {
            cTimer.cancel();
        }
    }


    private fun disabledButtons(){
        resendLink.isEnabled = false
        verifyButton.isEnabled = false
        otpLoader.visibility = View.VISIBLE
    }



}

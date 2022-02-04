//package com.rnsoft.colabademo.Notification
//
//import android.app.NotificationManager
//
//import android.app.NotificationChannel
//
//import android.os.Build
//
//import android.media.RingtoneManager
//
//import android.R
//import android.app.Notification
//
//import android.app.PendingIntent
//import android.content.Context
//
//import android.content.Intent
//
//import android.media.MediaPlayer
//import android.provider.Settings
//import android.util.Log
//import android.window.SplashScreen
//import androidx.annotation.RequiresApi
//import androidx.compose.ui.graphics.Color
//import com.google.firebase.messaging.FirebaseMessagingService
//import com.google.firebase.messaging.RemoteMessage
//import okhttp3.Response
//import java.lang.Exception
//import java.util.*
//import androidx.core.app.NotificationCompat;
//import com.rnsoft.colabademo.SplashActivity
//
//
//class MyFirebaseMessagingService : FirebaseMessagingService() {
//    private val TAG = "Fcm"
//    var CHANNEL_ID = "Colaba_channel" // The id of the channel.
//    override fun onNewToken(s: String) {
//        super.onNewToken(s)
//        Log.d(TAG, "onNewToken: $s")
//    }
//
//    override fun onCreate() {
//        super.onCreate()
//    }
//
//    @RequiresApi(Build.VERSION_CODES.S)
//    override fun onMessageReceived(remoteMessage: RemoteMessage) {
//        if (remoteMessage.getData() != null) {
//            val intent = Intent(this, SplashActivity::class.java)
//            handleNotification(
//                remoteMessage.getData().get("title")!!,
//                remoteMessage.getData().get("body")!!,
//                intent
//            )
//        }
//    }
//
//    /*
//    private void showChatNotification(RemoteMessage remoteMessage, Intent intent) {
//        NotificationManager notificationManager = (NotificationManager) BaseApplication.Companion.getApp().getSystemService(Context.NOTIFICATION_SERVICE);
//        Random ran = new Random() {
//            @Override
//            public int nextBits(int i) {
//                return 0;
//            }
//        };
//        int notificationID = ran.nextInt();
//        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
//        PendingIntent pendingIntent = PendingIntent.getActivity(BaseApplication.Companion.getApp(), 0,
//                intent, PendingIntent.FLAG_ONE_SHOT);
//        NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(BaseApplication.Companion.getApp(), CHANNEL_ID)
//                .setSmallIcon(R.drawable.ic_notification)
//                .setContentTitle(remoteMessage.getData().get("title"))
//                .setContentText(remoteMessage.getData().get("body"))
//                .setAutoCancel(true)
//                .setSound(RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION))
//                .setContentIntent(pendingIntent);
//        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
//            CharSequence channelName = "New notification";
//            String channelDescription = "Device to device notification";
//            NotificationChannel channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationManager.IMPORTANCE_HIGH);
//            channel.setDescription(channelDescription);
//            channel.enableLights(true);
//            channel.setLightColor(Color.GREEN);
//            channel.enableVibration(true);
//            notificationManager.createNotificationChannel(channel);
//        }
//        notificationManager.notify(notificationID, notificationBuilder.build());
//    }
//*/
//    private fun handleNotification(title: String, message: String, intentToOpen: Intent) {
//        playNotificationSound()
//        handleDataMessage(title, message, intentToOpen)
//    }
//
//    private fun playNotificationSound() {
//        try {
//            val player: MediaPlayer = MediaPlayer.create(
//                this, Settings.System.DEFAULT_NOTIFICATION_URI
//            )
//            player.start()
//        } catch (e: Exception) {
//            e.printStackTrace()
//        }
//    }
//
//    private fun handleDataMessage(title: String, message: String, intentToOpen: Intent) {
//        intentToOpen.flags = Intent.FLAG_ACTIVITY_CLEAR_TOP or Intent.FLAG_ACTIVITY_SINGLE_TOP
//        intentToOpen.flags = Intent.FLAG_ACTIVITY_CLEAR_TASK or Intent.FLAG_ACTIVITY_NEW_TASK
//        showNotification(
//            this,
//            title, message,
//            intentToOpen
//        )
//    }
//
//    fun showNotification(context: Context?, title: String?, message: String?, intent: Intent) {
//        val notificationManager =
//            this.getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
//        val ran: Random = object : Random() {
//            fun nextBits(i: Int): Int {
//                return 0
//            }
//        }
//        val notificationID: Int = ran.nextInt()
//        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)
//        val pendingIntent = PendingIntent.getActivity(
//            this, 0,
//            intent, PendingIntent.FLAG_ONE_SHOT
//
//        )
//        val notificationBuilder: Notification.Builder = Notification.Builder(
//            this,
//            CHANNEL_ID
//        )
//
//            .setContentTitle(title)
//            .setContentText(message)
//            .setAutoCancel(true)
//            .setSound(RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION))
//            .setContentIntent(pendingIntent)
//        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
//            val channelName: CharSequence = "New notification"
//            val channelDescription = "Device to device notification"
//            val channel =
//                NotificationChannel(CHANNEL_ID, channelName, NotificationManager.IMPORTANCE_HIGH)
//            channel.description = channelDescription
//            channel.enableLights(true)
//            channel.enableVibration(true)
//            notificationManager.createNotificationChannel(channel)
//        }
//        notificationManager.notify(notificationID, notificationBuilder.build())
//    }
//}
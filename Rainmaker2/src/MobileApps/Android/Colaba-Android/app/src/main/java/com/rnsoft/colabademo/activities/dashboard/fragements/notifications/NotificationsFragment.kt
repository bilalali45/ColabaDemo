package com.rnsoft.colabademo

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.ecommerce.testapp.NewNotificationListAdapter
import com.rnsoft.colabademo.databinding.FragmentNotificationsBinding


class NotificationsFragment : Fragment(), NotificationClickListener {


    private lateinit var notificationsViewModel: NotificationsViewModel
    private var _binding: FragmentNotificationsBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    private var notificationRecycleView: RecyclerView? = null

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        notificationsViewModel =
            ViewModelProvider(this).get(NotificationsViewModel::class.java)

        _binding = FragmentNotificationsBinding.inflate(inflater, container, false)
        val root: View = binding.root

        notificationRecycleView = root.findViewById<RecyclerView>(R.id.notification_recycle_view)
        notificationRecycleView?.apply {
            // set a LinearLayoutManager to handle Android
            // RecyclerView behavior
            this.layoutManager = LinearLayoutManager(activity)
            //(this.layoutManager as LinearLayoutManager).isMeasurementCacheEnabled = false
            this.setHasFixedSize(true)
            // set the custom adapter to the RecyclerView
            val notificationList = NotificationModel.sampleNotificationList(requireContext())
            this.adapter = NewNotificationListAdapter(notificationList, this@NotificationsFragment)

        }
        binding.newNotificationButton.transformationMethod = null
        binding.newNotificationButton.setOnClickListener {
            //val layoutManager = notificationRecycleView?.layoutManager
            //layoutManager?.scrollToPosition(0)
            //layoutManager?.smoothScrollToPosition(notificationRecycleView,RecyclerView.State, 0)
            notificationRecycleView?.smoothScrollToPosition(0)
        }

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    override fun onItemClick(view:View) {

    }


}
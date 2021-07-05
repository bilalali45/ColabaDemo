package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.ecommerce.testapp.NewNotificationListAdapter
import com.rnsoft.colabademo.databinding.FragmentNotificationsBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject

@AndroidEntryPoint
class NotificationsFragment : Fragment(), NotificationClickListener {


   // private lateinit var notificationsViewModel: NotificationsViewModel
    private var _binding: FragmentNotificationsBinding? = null

    private val dashBoardViewModel: DashBoardViewModel by activityViewModels()


    @Inject
    lateinit var sharedPreferences: SharedPreferences

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    private lateinit var notificationRecycleView: RecyclerView
    private var notificationArrayList: ArrayList<NotificationItem> = ArrayList()
    private var newNotificationAdapter:NewNotificationListAdapter = NewNotificationListAdapter(notificationArrayList, this@NotificationsFragment)

    /////////////////////////////////////////////////////////////////////////
    private var pageSize = 20
    //private var lastId = -1
    private var mediumId = 1
    private var lastNotificationId = -1


    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        //notificationsViewModel = ViewModelProvider(this).get(NotificationsViewModel::class.java)

        _binding = FragmentNotificationsBinding.inflate(inflater, container, false)
        val root: View = binding.root

        notificationRecycleView = root.findViewById<RecyclerView>(R.id.notification_recycle_view)
        val linearLayoutManager = LinearLayoutManager(activity)
        notificationRecycleView.apply {
            // set a LinearLayoutManager to handle Android
            // RecyclerView behavior
            this.layoutManager = linearLayoutManager
            //(this.layoutManager as LinearLayoutManager).isMeasurementCacheEnabled = false
            this.setHasFixedSize(true)
            // set the custom adapter to the RecyclerView
            //val notificationList = NotificationModel.sampleNotificationList(requireContext())
            newNotificationAdapter = NewNotificationListAdapter(notificationArrayList, this@NotificationsFragment)

            this.adapter = newNotificationAdapter



        }
        binding.newNotificationButton.transformationMethod = null
        binding.newNotificationButton.setOnClickListener {
            //val layoutManager = notificationRecycleView?.layoutManager
            //layoutManager?.scrollToPosition(0)
            //layoutManager?.smoothScrollToPosition(notificationRecycleView,RecyclerView.State, 0)
            notificationRecycleView.smoothScrollToPosition(0)
        }


        lifecycleScope.launchWhenResumed {
            sharedPreferences.getString(AppConstant.token, "")?.let {
                dashBoardViewModel.getNotificationListing(
                    token = AppConstant.fakeMubashirToken,
                    pageSize = pageSize, lastId = -1, mediumId = mediumId
                )
            }
        }

        dashBoardViewModel.notificationItemList.observe(viewLifecycleOwner, {
            if(it.size>0) {
                 Log.e("it-", it.size.toString() + "  " + it)
                notificationArrayList.addAll(it)
                newNotificationAdapter.notifyDataSetChanged()
                val seenIds:ArrayList<Int> = ArrayList()
                for (i in it){
                    i.id?.let {
                        seenIds.add(i.id)
                    }
                }

                 it[it.size-1].id?.let {  lastId->
                     lastNotificationId  = lastId
                 }
                // load the count notification service and other service and update count by LiveData...
                sharedPreferences.getString(AppConstant.token, "")?.let {
                      dashBoardViewModel.getNotificationCountT(AppConstant.fakeMubashirToken)
                    dashBoardViewModel.seenNotifications(AppConstant.fakeMubashirToken, seenIds)
                }
            }

        })

        val scrollListener = object : EndlessRecyclerViewScrollListener(linearLayoutManager) {
            override fun onLoadMore(page: Int, totalItemsCount: Int, view: RecyclerView?) {
                // Triggered only when new data needs to be appended to the list
                // Add whatever code is needed to append new items to the bottom of the list
                //rowLoading.visibility = View.VISIBLE
                loadFurtherNotifications()
            }
        }

        notificationRecycleView.addOnScrollListener(scrollListener)

        binding.newNotificationButton

        return root
    }

    private fun loadFurtherNotifications(){
        sharedPreferences.getString(AppConstant.token, "")?.let {
            dashBoardViewModel.getFurtherNotificationList(
                token = AppConstant.fakeMubashirToken,   pageSize = pageSize, lastId = lastNotificationId, mediumId = mediumId
            )
        }
    }


    override fun onStop() {
        super.onStop()
        sharedPreferences.getString(AppConstant.token,"")?.let {
            dashBoardViewModel.deleteNotifications(AppConstant.fakeMubashirToken, ArrayList())
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    override fun onItemClick(view:View) {
        Log.e("onClick - ", view.toString())
    }

    override fun getNotificationIndex(position: Int) {
        Log.e("position - ", position.toString())
    }


}
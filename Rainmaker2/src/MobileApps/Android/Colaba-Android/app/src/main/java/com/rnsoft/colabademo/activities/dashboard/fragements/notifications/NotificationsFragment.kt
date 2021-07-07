package com.rnsoft.colabademo

import android.content.SharedPreferences
import android.graphics.Color
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.coordinatorlayout.widget.CoordinatorLayout
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.ItemTouchHelper
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.ecommerce.testapp.NewNotificationListAdapter
import com.google.android.material.snackbar.Snackbar
import com.rnsoft.colabademo.databinding.FragmentNotificationsBinding
import dagger.hilt.android.AndroidEntryPoint
import javax.inject.Inject


@AndroidEntryPoint
class NotificationsFragment : Fragment(), NotificationClickListener, RecyclerItemTouchHelper.RecyclerItemTouchHelperListener {


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
    private var readArrayList: ArrayList<Int> = ArrayList()
    private var deleteArrayList: ArrayList<Int> = ArrayList()
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

        coordinatorLayout = root.findViewById<CoordinatorLayout>(R.id.coordinator_layout)

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

        /*
        val itemTouchHelperCallback1: ItemTouchHelper.SimpleCallback = object :
            ItemTouchHelper.SimpleCallback(0, ItemTouchHelper.LEFT or ItemTouchHelper.RIGHT or ItemTouchHelper.UP) {
            override fun onMove(recyclerView: RecyclerView, viewHolder: RecyclerView.ViewHolder, target: RecyclerView.ViewHolder): Boolean {
                return false
            }

            override fun onSwiped(viewHolder: RecyclerView.ViewHolder, direction: Int) {
                // Row is swiped from recycler view
                // remove it from adapter
            }

            override fun onChildDraw(c: Canvas, recyclerView: RecyclerView, viewHolder: RecyclerView.ViewHolder, dX: Float, dY: Float, actionState: Int, isCurrentlyActive: Boolean) {
                super.onChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive)
            }
        }

         */

        // attaching the touch helper to recycler view

        // attaching the touch helper to recycler view
        //ItemTouchHelper(itemTouchHelperCallback1).attachToRecyclerView(notificationRecycleView)


        notificationRecycleView.addOnScrollListener(scrollListener)




        /*
        val touchHelperCallback: ItemTouchHelper.SimpleCallback =
            object : ItemTouchHelper.SimpleCallback(0, ItemTouchHelper.LEFT ) {

                override fun onMove(recyclerView: RecyclerView, viewHolder: RecyclerView.ViewHolder, target: RecyclerView.ViewHolder): Boolean {
                    return false
                }

                private val background = ColorDrawable(Color.RED)

                override fun onSwiped(viewHolder: RecyclerView.ViewHolder, direction: Int) {
                    newNotificationAdapter.showMenu(viewHolder.adapterPosition);
                    listener.onSwiped(viewHolder, direction, viewHolder.adapterPosition)
                }

                override fun onChildDraw(c: Canvas, recyclerView: RecyclerView, viewHolder: RecyclerView.ViewHolder,
                    dX: Float, dY: Float, actionState: Int, isCurrentlyActive: Boolean) {
                    super.onChildDraw(
                        c, recyclerView, viewHolder,
                        dX, dY, actionState, isCurrentlyActive)
                    val itemView = viewHolder.itemView
                    if (dX > 0) {
                        background.setBounds(itemView.left, itemView.top, itemView.left + dX.toInt(), itemView.bottom)
                    } else if (dX < 0) {
                        background.setBounds(itemView.right + dX.toInt(), itemView.top, itemView.right, itemView.bottom)
                    } else {
                        background.setBounds(0, 0, 0, 0)
                    }
                    background.draw(c)
                }
            }


        val itemTouchHelper = ItemTouchHelper(touchHelperCallback)
        itemTouchHelper.attachToRecyclerView(notificationRecycleView)

         */

        val itemTouchHelperCallback: ItemTouchHelper.SimpleCallback =
            RecyclerItemTouchHelper(0, ItemTouchHelper.LEFT, this)
        ItemTouchHelper(itemTouchHelperCallback).attachToRecyclerView(notificationRecycleView)





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
        Log.e("readArrayList-", readArrayList.size.toString())
        Log.e("deleteArrayList-", deleteArrayList.size.toString())
        sharedPreferences.getString(AppConstant.token,"")?.let {
            if(readArrayList.size>0)
                dashBoardViewModel.readNotifications(AppConstant.fakeMubashirToken, readArrayList)

        }
        sharedPreferences.getString(AppConstant.token,"")?.let {
            //if(deleteArrayList.size>0)
                //dashBoardViewModel.deleteNotifications(AppConstant.fakeMubashirToken, deleteArrayList)
        }
        super.onStop()
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    override fun onItemClick(view:View) {
        Log.e("onClick - ", view.toString())
    }

    override fun onNotificationRead(position: Int) {
        Log.e("position - ", position.toString())
        val readId = notificationArrayList[position].id
        readId?.let {
            if (!readArrayList.contains(it))
                readArrayList.add(it)
        }
    }


    override fun onNotificationDelete(position: Int) {
        Log.e("position - ", position.toString())
        val deleteId = notificationArrayList[position].id
        deleteId?.let {
            if (!deleteArrayList.contains(it))
                deleteArrayList.add(it)
        }
    }

    override fun onSwiped(viewHolder: RecyclerView.ViewHolder?, direction: Int, position: Int) {
        if (viewHolder is NewNotificationListAdapter.ContentViewHolder) {
            // get the removed item name to display it in snack bar
            val name = notificationArrayList[viewHolder.getAdapterPosition()].id

            // backup of removed item for undo purpose
            val deletedItem = notificationArrayList[viewHolder.getAdapterPosition()]
            val deletedIndex = viewHolder.getAdapterPosition()

            val deleteId = deletedItem.id
            deleteId?.let {
                if (!deleteArrayList.contains(it))
                    deleteArrayList.add(it)
            }

            // remove the item from recycler view
            newNotificationAdapter.removeItem(viewHolder.getAdapterPosition())

            // showing snack bar with Undo option
            val snackbar = Snackbar
                .make(coordinatorLayout, "$name removed from Notifications!", Snackbar.LENGTH_LONG)
            snackbar.setAction("UNDO") { // undo is selected, restore the deleted item
                newNotificationAdapter.restoreItem(deletedItem, deletedIndex)
                val deleteId = deletedItem.id
                deleteId?.let {
                    if (deleteArrayList.contains(it))
                        deleteArrayList.remove(it)
                }
            }
            snackbar.setActionTextColor(Color.YELLOW)
            snackbar.show()
        }
    }

    private lateinit var coordinatorLayout: CoordinatorLayout


}


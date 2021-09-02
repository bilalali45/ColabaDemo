package com.rnsoft.colabademo.activities.borroweraddresses.info;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.rnsoft.colabademo.R;
import com.rnsoft.colabademo.activities.borroweraddresses.info.model.Address;

import java.util.ArrayList;
import java.util.List;

public class ResidenceAdapter extends RecyclerView.Adapter<ResidenceAdapter.MyViewHolder> {
    private Context mContext;
    private List<Address> taskList;
    private AddressClickListener clickListener;

    ResidenceAdapter(Context context, AddressClickListener clickListener){
        mContext = context;
        taskList = new ArrayList<Address>();
        this.clickListener = clickListener;
    }

    @Override
    public MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(mContext).inflate(R.layout.residence_item,parent,false);





        return new MyViewHolder(view);
    }
    @Override
    public void onBindViewHolder(MyViewHolder holder, int position) {
        Address address = taskList.get(position);
        holder.tvTaskName.setText(address.getAddressDesc());

        if(address.isCurrentAddress()){
            holder.tvHeading.setVisibility(View.VISIBLE);
            holder.tvRent.setVisibility(View.VISIBLE);
        } else {
            holder.tvHeading.setVisibility(View.GONE);
            holder.tvRent.setVisibility(View.GONE);
            holder.tvDate.setText("From Aug 2019 to Nov 2020");
        }

        /* holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.e("click", "click");
                //clickListener.onAddressClick(position);
            }
        }); */

    }
    @Override
    public int getItemCount() {
        return taskList.size();
    }
    public void setTaskList(List<Address> taskList) {
        this.taskList = taskList;
        notifyDataSetChanged();
    }


    public class MyViewHolder extends RecyclerView.ViewHolder {
        private TextView tvTaskName;
        private TextView tvHeading;
        private TextView tvRent;
        private TextView tvDate;
        public MyViewHolder(View itemView ) {
            super(itemView);
            tvTaskName = itemView.findViewById(R.id.tv_address);
            tvHeading = itemView.findViewById(R.id.tv_currentAddress_heading);
            tvRent = itemView.findViewById(R.id.tv_homerent);
            tvDate = itemView.findViewById(R.id.tv_residence_date);



            itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Log.e("click", "click");
                    clickListener.onAddressClick(getAdapterPosition());
                    Toast.makeText(mContext, "clicked",Toast.LENGTH_SHORT).show();
                }
            });
        }



    }
}
package com.rnsoft.colabademo.activities.info;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.rnsoft.colabademo.R;
import com.rnsoft.colabademo.activities.info.model.Address;

import java.util.ArrayList;
import java.util.List;

public class ResidenceAdapter extends RecyclerView.Adapter<ResidenceAdapter.MyViewHolder> {
    private Context mContext;
    private List<Address> taskList;
    ResidenceAdapter(Context context){
        mContext = context;
        taskList = new ArrayList<Address>();
    }
    @NonNull
    @Override
    public MyViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(mContext).inflate(R.layout.residence_item,parent,false);
        return new MyViewHolder(view);
    }
    @Override
    public void onBindViewHolder(@NonNull MyViewHolder holder, int position) {
        Address address = taskList.get(position);
        holder.tvTaskName.setText(address.getName());

        if(position ==0){
            holder.tvHeading.setVisibility(View.VISIBLE);
            holder.tvRent.setVisibility(View.VISIBLE);
        } else {
            holder.tvHeading.setVisibility(View.GONE);
            holder.tvRent.setVisibility(View.GONE);
        }

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
        public MyViewHolder(View itemView) {
            super(itemView);
            tvTaskName = itemView.findViewById(R.id.tv_address);
            tvHeading = itemView.findViewById(R.id.tv_currentAddress_heading);
            tvRent = itemView.findViewById(R.id.tv_homerent);
        }
    }
}
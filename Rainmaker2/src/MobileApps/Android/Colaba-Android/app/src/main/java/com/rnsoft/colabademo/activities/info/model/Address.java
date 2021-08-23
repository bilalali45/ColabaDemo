package com.rnsoft.colabademo.activities.info.model;

/**
 * Created by Anita Kiran on 8/20/2021.
 */
public class Address {

    private String name;
    private String desc;

    public Address(String name, String desc) {
        this.name = name;
        this.desc = desc;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDesc() {
        return desc;
    }

    public void setDesc(String desc) {
        this.desc = desc;
    }
}

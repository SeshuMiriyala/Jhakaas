package com.adp.jhakaas;

import java.io.Serializable;

public class Lead implements Serializable{
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private int id;
	private String firstName;
	private String lastName;
	private String avatar;
	
	public Lead(int id, String fName, String lName, String avatar){
		this.id = id;
		this.firstName = fName;
		this.lastName = lName;
		this.avatar = avatar;
	}

	public int getLeadId() {
		return id;
	}
	
	public String getFirstName() {
		return firstName;
	}

	public String getLastName() {
		return lastName;
	}

	public String getAvatar() {
		return avatar;
	}
}

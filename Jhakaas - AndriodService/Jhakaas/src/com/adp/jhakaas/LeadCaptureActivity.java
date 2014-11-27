package com.adp.jhakaas;

import java.util.ArrayList;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Toast;

public class LeadCaptureActivity extends Activity {
	
	public static ArrayList<Lead> LEADS;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_leads_list);
		
		if(null != LEADS)
			createRadioButton(LEADS);
	}
	
	@SuppressLint("ResourceAsColor")
	private void createRadioButton(ArrayList<Lead> leads) {
	    RadioGroup rg = (RadioGroup) findViewById(R.id.rg_leads);
	    for(int i = 0; i < leads.size(); i++){
	    	RadioButton rb = new RadioButton(this);
	        rb  = new RadioButton(this);
	        
	        rb.setText(leads.get(i).getFirstName() + " " + leads.get(i).getLastName());
	        rb.setTag(leads.get(i).getLeadId());
	        rb.setTextColor(R.color.blackColor);
	        
	        LinearLayout.LayoutParams layoutParams = new RadioGroup.LayoutParams(
	                RadioGroup.LayoutParams.WRAP_CONTENT,
	                RadioGroup.LayoutParams.WRAP_CONTENT );
	        rg.addView(rb, 0, layoutParams);
	    }
	    
	}
	
	public void SubmitPost(View view){
		RadioGroup rg = (RadioGroup) findViewById(R.id.rg_leads);
		int selectedOption = rg.getCheckedRadioButtonId();
		RadioButton selectedLead = (RadioButton) findViewById(selectedOption);
		
		Integer selectedLeadId = Integer.parseInt(selectedLead.getTag().toString());
		
		HttpHelper_PostActivity httpHelper = new HttpHelper_PostActivity();
		try{
			int result = httpHelper.execute("1", selectedLeadId.toString(), "testdrive").get();
			
			if(result > 0)
			{
				finish();
				Toast.makeText(LeadCaptureActivity.this,
					"Successfully posted the activity.", Toast.LENGTH_LONG).show();
			}
			else
				Toast.makeText(LeadCaptureActivity.this,
						"Failed to post the activity.", Toast.LENGTH_LONG).show();
		}
		catch(Exception e){
			Toast.makeText(LeadCaptureActivity.this,
					"Failed to post the activity.", Toast.LENGTH_LONG).show();
			Log.e(getLocalClassName(), e.getMessage());
		}

	}
}

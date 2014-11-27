package com.adp.jhakaas;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.ArrayList;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.os.AsyncTask;
import android.widget.Toast;

public class HttpHelper  extends AsyncTask<Integer, Integer, ArrayList<Lead>> {

	private ArrayList<Lead> leads = new ArrayList<Lead>();

	public ArrayList<Lead> doInBackground(Integer... params) {
		try {
			HttpGet httpget = new HttpGet("http://por-vd-miriyals.dslab.ad.adp.com/api/lead/getleadsbyuserid?userid=" + params[0]);
			HttpClient client = new DefaultHttpClient();
			
			// Execute HTTP Post Request
			HttpResponse httpresponse = client.execute(httpget);
	         HttpEntity entity = httpresponse.getEntity();
	         if(null != entity)
	         {
	        	//get the response content as a string
	             String response = EntityUtils.toString(entity);
	             //consume the entity
	             entity.consumeContent();

	             // When HttpClient instance is no longer needed, shut down the connection manager to ensure immediate deallocation of all system resources
	             client.getConnectionManager().shutdown();
	             convertStringToLeads(response);
	             
	             return leads;
	         }
		}
		catch(JSONException e){
			Toast.makeText(null, 
					e.getMessage(), Toast.LENGTH_LONG).show();
			e.printStackTrace();
		} catch (UnsupportedEncodingException e) {
			Toast.makeText(null, 
					e.getMessage(), Toast.LENGTH_LONG).show();
			e.printStackTrace();
		} catch (ClientProtocolException e) {
			Toast.makeText(null, 
					e.getMessage(), Toast.LENGTH_LONG).show();
			e.printStackTrace();
		} catch (IOException e) {
			Toast.makeText(null, 
					e.getMessage(), Toast.LENGTH_LONG).show();
			e.printStackTrace();
		}
		catch(Exception e){
			Toast.makeText(null, 
					e.getMessage(), Toast.LENGTH_LONG).show();
			e.printStackTrace();
		}
		return null;
	}
	@Override
	protected void onPostExecute(ArrayList<Lead> result) {
		super.onPostExecute(result);
		
		if(LeadCaptureActivity.LEADS != null && LeadCaptureActivity.LEADS.size() > 0)
			LeadCaptureActivity.LEADS.clear();
		LeadCaptureActivity.LEADS = result;
	}
	
	private void convertStringToLeads(String response)
			throws JSONException {
		
		JSONArray jArray = new JSONArray(response);
		
		if (jArray != null) { 
			   for (int i=0;i<jArray.length();i++){
				   
				   JSONObject object = jArray.getJSONObject(i);
				   int id = object.getInt("Id");
				   String firstName = object.getString("FirstName");
				   String lastName = object.getString("LastName");
				   String avatar = object.getString("Avatar");
				   
				   Lead lead = new Lead(id, firstName, lastName, avatar);
				   
				   leads.add(lead);
			   } 
		}
	}

}

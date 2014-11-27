package com.adp.jhakaas;

import java.io.IOException;
import java.io.UnsupportedEncodingException;

import org.apache.http.HttpEntity;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;

import android.os.AsyncTask;

public class HttpHelper_PostActivity extends
		AsyncTask<String, Integer, Integer> {

	@Override
	protected Integer doInBackground(String... params) {
		try {
			HttpGet httpget = new HttpGet("http://por-vd-miriyals.dslab.ad.adp.com/api/post/addnewpost?userid=" + params[0] + "&leadid=" + params[1] + "&activitycode=" + params[2] + "&refreshId=1/jQ5atV69WM6DdKOxanvmEb0p22ODoyC915WgODaReCg&channelId=18377579481682988412/ooggfcmfijlplgljchclakbkdkhckbfb");
			HttpClient client = new DefaultHttpClient();
			
			// Execute HTTP Post Request
	         HttpEntity entity = client.execute(httpget).getEntity();
	         if(null != entity)
	         {
	        	//get the response content as a string
	             String response = EntityUtils.toString(entity);
	             //consume the entity
	             entity.consumeContent();

	             // When HttpClient instance is no longer needed, shut down the connection manager to ensure immediate deallocation of all system resources
	             client.getConnectionManager().shutdown();
	             
	             return Integer.parseInt(response);
	         }
		}
		catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		} catch (ClientProtocolException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		catch(Exception e){
			e.printStackTrace();
		}
		return null;
	}

}

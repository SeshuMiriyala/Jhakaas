package com.adp.jhakaas;

import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.ArrayList;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.location.Location;
import android.location.LocationManager;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

public class LocationCaptureActivity extends Activity {

	private static final String POINT_LATITUDE_KEY = "POINT_LATITUDE_KEY";
	private static final String POINT_LONGITUDE_KEY = "POINT_LONGITUDE_KEY";

	private static final String PREFERENCE_NAME = "VEHICLE_LOT";

	private static final NumberFormat nf = new DecimalFormat("##.########");

	private LocationManager locationManager;

	private EditText latitudeEditText;
	private EditText longitudeEditText;

	private double latitude;
	private double longitude;
	
	public static ArrayList<Lead> LEADS;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_location_capture);

		locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);

		latitudeEditText = (EditText) findViewById(R.id.et_latitude);
		longitudeEditText = (EditText) findViewById(R.id.et_longitude);
	}

	public void CaptureCoordinates(View v) {
		Location location = null;
		try {
			location = locationManager
					.getLastKnownLocation(LocationManager.GPS_PROVIDER);
		} catch (Exception ex) {
			Log.e("Inside location manager.", ex.getMessage());
		}

		if (location == null) {
			try {
				location = locationManager
						.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
			} catch (Exception ex) {
				Log.e("Inside location manager.", ex.getMessage());
			}
			if (location == null) {
				Toast.makeText(this, "No last known location. Aborting...",
						Toast.LENGTH_LONG).show();
				return;
			}
		}

		latitude = location.getLatitude();
		longitude = location.getLongitude();

		latitudeEditText.setText(nf.format(latitude));
		longitudeEditText.setText(nf.format(longitude));
	}

	public void SaveCoordinates(View v) {

		if (latitudeEditText.getText() != null && longitudeEditText.getText() != null) {

			saveCoordinatesInPreferences(Float.parseFloat(latitudeEditText.getText().toString()), Float.parseFloat(longitudeEditText.getText().toString()));
		}

		Intent serviceIntent = new Intent(getApplicationContext(), LocationService.class);
		serviceIntent.setAction("com.adp.jhakaas.locationServiceClass");
		
		startService(serviceIntent);
		
        finish();
	}

	private void saveCoordinatesInPreferences(float latitude, float longitude) {
		SharedPreferences prefs = this.getSharedPreferences(PREFERENCE_NAME,
				Context.MODE_PRIVATE);
		SharedPreferences.Editor prefsEditor = prefs.edit();
		prefsEditor.putFloat(POINT_LATITUDE_KEY, latitude);
		prefsEditor.putFloat(POINT_LONGITUDE_KEY, longitude);
		prefsEditor.commit();
	}
}

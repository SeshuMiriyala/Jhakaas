package com.adp.jhakaas;

import android.app.PendingIntent;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.location.Location;
import android.location.LocationManager;
import android.os.IBinder;

public class LocationService extends Service {

	@Override
	public void onCreate() {
		super.onCreate();
	}

	@Override
	public void onDestroy() {
		super.onDestroy();
	}

	private static final String POINT_LATITUDE_KEY = "POINT_LATITUDE_KEY";
    private static final String POINT_LONGITUDE_KEY = "POINT_LONGITUDE_KEY";
    
    private static final String PREFERENCE_NAME = "VEHICLE_LOT";
    
    private static final long PROXIMITY_RADIUS = 1; // in Meters
    
    private static final String PROX_ALERT_INTENT = "com.adp.jhakaas.ProximityAlert";

    
    private LocationManager locationManager;
    
    @Override
	public int onStartCommand(Intent intent, int flags, int startId) {
		locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);

		Location pointLocation = retrieveLocationFromPreferences();
		
		Intent serviceIntent = new Intent(PROX_ALERT_INTENT);
		PendingIntent proximityIntent = PendingIntent.getBroadcast(getApplicationContext(), 
				0, serviceIntent, 0);

		IntentFilter filter = new IntentFilter(PROX_ALERT_INTENT); 
		registerReceiver(new ProximityIntentReceiver(), filter);
		
        locationManager.addProximityAlert(pointLocation.getLatitude(),
        		pointLocation.getLongitude(), PROXIMITY_RADIUS, -1, proximityIntent);
        
		return Service.START_STICKY;
	}

	@Override
	public IBinder onBind(Intent intent) {
		return null;
	}
	
	
	
	private Location retrieveLocationFromPreferences() {
        SharedPreferences prefs = 
           this.getSharedPreferences(PREFERENCE_NAME, Context.MODE_PRIVATE);
        Location location = new Location("POINT_LOCATION");
        location.setLatitude(prefs.getFloat(POINT_LATITUDE_KEY, 0));
        location.setLongitude(prefs.getFloat(POINT_LONGITUDE_KEY, 0));
        return location;
    }
}

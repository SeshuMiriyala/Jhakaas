package com.adp.jhakaas;

import android.annotation.SuppressLint;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.location.LocationManager;
import android.media.RingtoneManager;
import android.net.Uri;
import android.widget.Toast;

public class ProximityIntentReceiver extends BroadcastReceiver {
    
    private static final int NOTIFICATION_ID = 1000;

	@SuppressWarnings("deprecation")
	@Override
    public void onReceive(Context context, Intent intent) {
        
        String key = LocationManager.KEY_PROXIMITY_ENTERING;

        Boolean entering = intent.getBooleanExtra(key, false);
        
        if (entering) {
        	Toast.makeText(context, 
                    "Entering the proximity", Toast.LENGTH_LONG).show();
        }
        else {
        	Toast.makeText(context, 
                    "Exiting the proximity", Toast.LENGTH_LONG).show();
        }
        
        NotificationManager notificationManager = 
	            (NotificationManager) context.getSystemService(Context.NOTIFICATION_SERVICE);
		
		Intent activityIntent = new Intent(context, LeadCaptureActivity.class);
        
        HttpHelper httpHelper = new HttpHelper();
        
		try {
			httpHelper.execute(1);
		}catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		PendingIntent pendingIntent = PendingIntent.getActivity(context, 0, activityIntent, 0); 
		
		Notification notification = createNotification(context, 
        		"Proximity Alert!", 
        		"You are near your point of interest.",
        		pendingIntent);
		
        notification.setLatestEventInfo(context, 
            "Proximity Alert!", "You are near your point of interest.", pendingIntent);
        
        notificationManager.notify(NOTIFICATION_ID, notification);
        
    }
    
	@SuppressLint("NewApi")
	private Notification createNotification(Context context, String title, String text, PendingIntent intent) {
        Notification notification = new Notification.Builder(context)
        							.setContentTitle(title)
        							.setContentText(text)
        							.setContentIntent(intent)
        							.setContentInfo("Click to open")
        							.build();
        
        Uri soundUri = RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION);

        notification.sound = soundUri;
        
        notification.icon = R.drawable.ic_launcher;
        notification.when = System.currentTimeMillis();
        
        notification.flags |= Notification.FLAG_AUTO_CANCEL;
        notification.flags |= Notification.FLAG_SHOW_LIGHTS;
        
        notification.flags |= Notification.DEFAULT_SOUND;
        notification.flags |= Notification.FLAG_AUTO_CANCEL;
        
        notification.defaults |= Notification.DEFAULT_VIBRATE;
        notification.defaults |= Notification.DEFAULT_SOUND;
        notification.defaults |= Notification.DEFAULT_LIGHTS;
        
        notification.ledARGB = Color.WHITE;
        notification.ledOnMS = 1500;
        notification.ledOffMS = 1500;
        
        return notification;
    }
    
}

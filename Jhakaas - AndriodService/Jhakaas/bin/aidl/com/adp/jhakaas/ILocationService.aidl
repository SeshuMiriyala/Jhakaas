package com.adp.jhakaas;

interface ILocationService{

	boolean StartCapturingLocationAlerts(float latitude, float longitude);
	
	boolean StopCapturingLocationAlerts();
}
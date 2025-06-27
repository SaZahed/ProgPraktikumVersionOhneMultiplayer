using System;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float timeMultiplier;//um geschwindigkeit der zeit zu kontrollieren
    [SerializeField]
    private float startHour;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private Light sunLight;
    
    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    private DateTime currentTime; //einfacher für berechnung

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;




    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();

    }
    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        if (timeText !=null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }
    private void RotateSun()
    {
        float sunLightRotation;
        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);//wie viel zeit seit sonaufgang vergangen ist
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes; //prozentualer anteil der zeit seit sonnenaufgang

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage); //sonnenstand zwischen 0 und 180 Grad

        }
        else //für die nacht sunset bis sunrise
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes; //prozentualer anteil der zeit seit sonnenuntergang

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage); //sonnenstand zwischen 180 und 360 Grad
        }
        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);//rotieren um x achse

    }
    private TimeSpan CalculateTimeDifference(TimeSpan fromTime,TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;
        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromDays(1); // wert muss positiv sein, also wenn von 23:00 Uhr auf 01:00 Uhr
        }
        return difference;
    }
}

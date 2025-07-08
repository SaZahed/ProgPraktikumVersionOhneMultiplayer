using System;
using TMPro;
using UnityEngine;

//day and night cycle wurde mithilfe folgendes Tutorial umgzesetzt:
// https://youtu.be/L4t2c1_Szdk?si=ZBYCjewzvOpwI5dX
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

    private DateTime currentTime; //einfacher fuer berechnung

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    [SerializeField] private Material daySkybox; // Wird nicht genutzt: es bleibt die Standard-Skybox
    [SerializeField] private Material nightSkybox; // von mir erstellte galaxy skybox ueber folgender webseite: https://tools.wwwtyro.net/space-3d/index.html#animationSpeed=0.1&fov=107&nebulae=true&pointStars=true&resolution=1024&seed=6jcc4aml3a40&stars=true&sun=true 

    public static TimeController Instance { get; private set; }//notwenidig, um im Replay auf Zeit waehrend szene zuzugreifen 


    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);

    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateSkybox();

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
    private void UpdateSkybox()
    {
        bool isNight = currentTime.TimeOfDay <= sunriseTime || currentTime.TimeOfDay >= sunsetTime;

        if (isNight)
        {
            RenderSettings.skybox = nightSkybox;
        }
    }
    public TimeSpan GetCurrentTimeOfDay()
    {
        return currentTime.TimeOfDay;
    }
    public void SetTimeOfDay(TimeSpan newTime)
    {
        currentTime = currentTime.Date + newTime;
    }

}

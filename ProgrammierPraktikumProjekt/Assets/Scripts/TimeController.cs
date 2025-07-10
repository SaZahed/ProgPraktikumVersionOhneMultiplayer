using System;
using TMPro;
using UnityEngine;


/// <summary>
/// Die TimeController Klasse verwaltet den Tag- und Nachtzyklus in der Simulation.
/// Basierend auf der Uhrzeit wird die Tageszeit angepasst und die Sonne rotiert entsprechend.
/// </summary>
/// <remakrs>day and night cycle wurde mithilfe folgendes Tutorial umgzesetzt:  https://youtu.be/L4t2c1_Szdk?si=ZBYCjewzvOpwI5dX </remakrs>
public class TimeController : MonoBehaviour
{
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

    /// <summary>
    /// Initialisiert die aktuelle Uhrzeit und berechnet Sonnenauf-/untergang.
    /// </summary>

    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);

    }

    /// <summary>
    /// Stellt sicher, dass es nur eine Instanz gibt (Singleton-Pattern), da Zugriff fuer Replay-Funktionalitaet benoetigt wird.
    /// </summary>
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
    /// <summary>
    /// Aktualisiert die Uhrzeit basierend auf dem Zeitmultiplikator und zeigt sie im UI Dashbaord an.
    /// </summary>
    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        if (timeText !=null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }
    /// <summary>
    /// Methoden rotiert die Sonne basierend auf der aktuellen Uhrzeit.
    /// </summary>
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

    /// <summary>
    /// berechnet den Zeitunterschied von start und endzeit
    /// </summary>
    /// <param> name="fromTime" ist die Startzeit  </param>
    /// <param> name="toTime" ist die Endzeit </param>
    /// <returns> der Zeitunterschied wird zurueckgegeben </returns>
    private TimeSpan CalculateTimeDifference(TimeSpan fromTime,TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;
        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromDays(1); // wert muss positiv sein, also wenn von 23:00 Uhr auf 01:00 Uhr
        }
        return difference;
    }

    /// <summary>
    /// Sollte die Skybox basierend auf der Tageszeit (Tag/Nacht) aktualisieren
    /// </summary>
    /// <remarks> diese Methode funktioniert leider nicht </remarks> 
    private void UpdateSkybox()
    {
        bool isNight = currentTime.TimeOfDay <= sunriseTime || currentTime.TimeOfDay >= sunsetTime;

        if (isNight)
        {
            RenderSettings.skybox = nightSkybox;
        }
    }

    /// <summary>
    /// Getter und Setter um Zugriff auf die aktuellen Zeiten zu bekommen, da diese im Replay benoetigt werden.
    /// </summary>
    /// <returns></returns>
    public TimeSpan GetCurrentTimeOfDay()
    {
        return currentTime.TimeOfDay;
    }
    public void SetTimeOfDay(TimeSpan newTime)
    {
        currentTime = currentTime.Date + newTime;
    }

}

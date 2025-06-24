using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class WeatherManager : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] float RainIntensity;
    [SerializeField, Range(0f, 10f)] float SnowIntensity;
    [SerializeField, Range(0f, 10f)] float HailIntensity;
    [SerializeField, Range(0f, 10f)] float FogIntensity;


    [SerializeField] VisualEffect RainVFX;
    [SerializeField] VisualEffect SnowVFX;
    [SerializeField] VisualEffect HailVFX;
    [SerializeField] Volume FogVolume;



    float PreviousRainIntensity;
    float PreviousHailIntensity;
    float PreviousSnowIntensity;
    float PreviousFogIntensity;


    // Start is called before the first frame update
    void Start()
    {
        RainVFX.SetFloat("Intensity", RainIntensity);
        HailVFX.SetFloat("Intensity", HailIntensity);
        SnowVFX.SetFloat("Intensity", SnowIntensity);
        FogVolume.weight = FogIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (RainIntensity != PreviousRainIntensity)
        {
            PreviousRainIntensity = RainIntensity;
            RainVFX.SetFloat("Intensity", RainIntensity);
        }

        if (HailIntensity != PreviousHailIntensity)
        {
            PreviousHailIntensity = HailIntensity;
            HailVFX.SetFloat("Intensity", HailIntensity);
        }

        if (SnowIntensity != PreviousSnowIntensity)
        {
            PreviousSnowIntensity = SnowIntensity;
            SnowVFX.SetFloat("Intensity", SnowIntensity);
        }
        if (FogIntensity != PreviousFogIntensity)
        {
            PreviousFogIntensity = FogIntensity;
            FogVolume.weight = FogIntensity;    
        }
    }

    public void SetWetter(string type)
{
    RainIntensity = 0f;
    SnowIntensity = 0f;
    HailIntensity = 0f;

    switch (type)
    {
        case "Regen":
            RainIntensity = 1f;
            break;
        case "Schnee":
            SnowIntensity = 1f;
            break;
        case "Hagel":
            HailIntensity = 1f;
            break;
        case "Nebel":
             FogIntensity = 1f;
             break;

        case "Klares Wetter":
            break;
        }
}

}

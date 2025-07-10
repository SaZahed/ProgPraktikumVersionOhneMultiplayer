using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Repraesentiert ein vom Lehrer definiertes Szenario mit Name, Szenenbezeichnung, Wetterbedingung und ausgewaehltem Schiff.
/// </summary>
/// <remarks> die Speicherung von Szenarien wurde mithilfe von ChatGPT erstellt </remarks>

[System.Serializable]
public class SzenarioKlasse
{
    public string name;
    public string szene;
    public string wetter;
    public string schiff;

    public SzenarioKlasse(string name, string szene, string wetter, string schiff)
    {
        this.name = name;
        this.szene = szene;
        this.wetter = wetter;
        this.schiff = schiff;
    }
}

/// <summary>
/// Eine Liste von Szenarien zur Speicherung oder Verwaltung verschiedener Spielzustaende.
/// </summary>

[System.Serializable]
public class SzenarioListe
{
    public List<SzenarioKlasse> szenarien = new List<SzenarioKlasse>();
}


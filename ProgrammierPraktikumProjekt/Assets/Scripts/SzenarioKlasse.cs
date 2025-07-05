using System.Collections.Generic;
using UnityEngine;

//die Speicherung von Szenarien wurde mithilfe von ChatGPT erstellt
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

[System.Serializable]
public class SzenarioListe
{
    public List<SzenarioKlasse> szenarien = new List<SzenarioKlasse>();
}


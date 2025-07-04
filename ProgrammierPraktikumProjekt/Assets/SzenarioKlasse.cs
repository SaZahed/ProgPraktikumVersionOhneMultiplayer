using UnityEngine;

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

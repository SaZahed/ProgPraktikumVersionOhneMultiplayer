using UnityEngine;

public class SzenarioKlasse
{
    private string name;
    private string szene;
    private string wetter;
    private string schiff;

    public SzenarioKlasse(string name, string szene, string wetter, string schiff)
    {
        this.name = name;
        this.szene = szene;
        this.wetter = wetter;
        this.schiff = schiff;
    }
}

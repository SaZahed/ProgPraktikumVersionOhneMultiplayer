using UnityEngine;

/// <summary>
/// Die ActionReplayRecords Klasse speichert die Position, Rotation, aktive Kamera, aktuelles Wetter und die Replay-Zeit, um diese in der Replay
/// anzeige anzupassen
/// </summary>
/// <remarks>die Replay funktion wurde mithilfe von folgendem Tutorial erstellt: https://youtu.be/R8RinJDzhf8?si=HEylm_vra8GaieGY </remarks>


[System.Serializable]
public class ActionReplayRecords
{
    public Vector3 position;

    public Quaternion rotation; //weil auch rotation aufgezeichnet werden soll

    //im Video gab es nur die beiden zum speichern, in unserem Projekt sind es weitere Komponenten
    
    public int activeCameraIndex;
    public string currentWeather;
    public System.TimeSpan replayTime;


}

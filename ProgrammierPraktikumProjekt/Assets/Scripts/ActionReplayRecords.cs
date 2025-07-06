using UnityEngine;

//die Replay funktion wurde mithilfe von folgendem Tutorial erstellt: https://youtu.be/R8RinJDzhf8?si=HEylm_vra8GaieGY
[System.Serializable]
public class ActionReplayRecords
{
    public Vector3 position;

    public Quaternion rotation; //weil auch rotation aufgezeichnet werden soll

    //im Video gab es nur die beiden zum speichern, aber bei uns sind es mehr sachen also:
    
    public int activeCameraIndex;
    public string currentWeather; 


}

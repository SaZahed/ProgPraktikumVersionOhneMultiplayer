using UnityEngine;
using System.Collections.Generic;
//die Replay funktion wurde mithilfe von folgendem Tutorial erstellt: https://youtu.be/R8RinJDzhf8?si=HEylm_vra8GaieGY

public class ActionReplay : MonoBehaviour
{
    private bool isInReplayMode = false;
    private Rigidbody rb;
    private List<ActionReplayRecords> records = new List<ActionReplayRecords>();
    private int replayIndex = 0; //angepasst mit ChatGpt

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isInReplayMode = !isInReplayMode;
            replayIndex = 0;

            if (isInReplayMode)
            {
                rb.isKinematic = true;
            }
            else
            {
                rb.isKinematic = false;
                records.Clear();
            }
        }
    }

    void FixedUpdate()
    {
        if (isInReplayMode)
        {
            if (replayIndex < records.Count)
            {
                SetTransform(replayIndex);
                replayIndex++;
            }
        }
        else
        {
            records.Add(new ActionReplayRecords
            {
                position = transform.position,
                rotation = transform.rotation,
                
                activeCameraIndex = CameraSwitcher.Instance.GetCurrentCameraIndex(),
                currentWeather = WeatherManager.Instance.GetCurrentWeatherType(),
            });
        }
    }

    private void SetTransform(int index)
    {
        var record = records[index];
        transform.position = record.position;
        transform.rotation = record.rotation;

        CameraSwitcher.Instance.SetCameraByIndex(record.activeCameraIndex);
        WeatherManager.Instance.SetWetter(record.currentWeather);
    }
}

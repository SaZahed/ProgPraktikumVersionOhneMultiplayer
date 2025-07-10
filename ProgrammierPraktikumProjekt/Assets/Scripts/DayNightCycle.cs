using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

/// <summary>
/// Diese version wurde nicht fuer den day and night cycle verwendet und war nur ein erster ansatz
/// </summary>
//day and night cycle wurde mithilfe folgendes Tutorial umgzesetzt: https://youtu.be/L4t2c1_Szdk?si=ZBYCjewzvOpwI5dX

public class DayNightCycle : MonoBehaviour
{
    public Light light;
    public float rotationSpeed;
    public Light directionalLight;


    private void Update()
    {
        light.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
       
        //float dot = Vector3.Dot(directionalLight.transform.forward, Vector3.down);
        //directionalLight.intensity = Mathf.Clamp01(dot) * 100000f;
    }
}

using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

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

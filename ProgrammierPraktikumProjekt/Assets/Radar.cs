using UnityEngine;

public class Radar : MonoBehaviour
{
    private Transform sweepTrasform;
    private float rotationSpeed;
    private void Awake()
    {
        sweepTrasform = transform.Find("Sweep");
        rotationSpeed = 180f;
    }

    private void Update()
    {
        sweepTrasform.eulerAngles -= new Vector3(0, 0, rotationSpeed + Time.deltaTime);
    }
}

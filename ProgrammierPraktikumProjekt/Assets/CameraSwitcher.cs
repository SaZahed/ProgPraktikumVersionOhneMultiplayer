using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera bridgeCam;

    private bool bridgeCamOn = false;

    void Start()
    {
        mainCam.enabled = true; // zu Beginn ist die Main Camera aktiv
        bridgeCam.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }

    public void SwitchCamera()
    {
        bridgeCamOn = !bridgeCamOn; // bool wird umgedreht
        mainCam.enabled = !bridgeCamOn;
        bridgeCam.enabled = bridgeCamOn;
    }
}

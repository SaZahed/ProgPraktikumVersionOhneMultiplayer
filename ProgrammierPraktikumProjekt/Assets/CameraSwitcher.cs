using UnityEngine;
using UnityEngine.UIElements;

public class CameraSwitcher : MonoBehaviour
{
    private UIDocument uiDocument;

    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera bridgeCam;

    private bool bridgeCamOn = false;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        root.Q<Button>("CameraSwitch").clicked += () => SwitchCamera();
    }

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

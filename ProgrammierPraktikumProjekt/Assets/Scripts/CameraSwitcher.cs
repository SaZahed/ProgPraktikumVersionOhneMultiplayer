using UnityEngine;
using UnityEngine.UIElements;

public class CameraSwitcher : MonoBehaviour
{
    public static CameraSwitcher Instance { get; private set; }//fuer Replayfunktion hinzugefügt

    private UIDocument uiDocument;

    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera bridgeCam;



    private bool bridgeCamOn = false;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();

        // Singleton-Pattern: Stelle sicher, dass nur eine Instanz existiert
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
    public int GetCurrentCameraIndex()
    {
        return bridgeCamOn ? 1 : 0;
    }

    public void SetCameraByIndex(int index)
    {
        bridgeCamOn = (index == 1);
        mainCam.enabled = !bridgeCamOn;
        bridgeCam.enabled = bridgeCamOn;
    }
}

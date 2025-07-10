using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Diese Klasse implementiert die Logik hinter dem Wechseln der Kameras in Unity.
/// </summary>

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

    /// <summary>
    /// Ermoeglicht Kamerawechsel per Tastendruck von C
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }

    /// <summary>
    /// Wechselt zwischen der Hauptkamera und der Brückenkamera.
    /// </summary>
    public void SwitchCamera()
    {
        bridgeCamOn = !bridgeCamOn; // bool wird umgedreht
        mainCam.enabled = !bridgeCamOn;
        bridgeCam.enabled = bridgeCamOn;
    }
    /// <summary>
    /// Gibt den aktuellen Kamera-Index zurueck. Bei Main Camera ist der Index 0, bei Bridge Camera ist der Index 1.
    /// </summary>
    /// <returns>Index der aktiven Kamera</returns>
    public int GetCurrentCameraIndex()
    {
        return bridgeCamOn ? 1 : 0;
    }

    /// <summary>
    /// Aktiviert eine Kamera anhand des Indexes. Benutzt 0 für die Hauptkamera und 1 für die Brückenkamera.
    /// </summary>
    /// <param> name="index" Kameraindex (0 oder 1)</param>
    public void SetCameraByIndex(int index)
    {
        bridgeCamOn = (index == 1);
        mainCam.enabled = !bridgeCamOn;
        bridgeCam.enabled = bridgeCamOn;
    }
}

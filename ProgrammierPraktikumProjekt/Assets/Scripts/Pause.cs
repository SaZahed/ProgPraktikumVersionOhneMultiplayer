using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Diese Klasse verwaltet die Pause-Funktionalität in der Simulation.
/// Stellt die Verbindung zwischen der UI und der Spiel-Logik her.
/// </summary>
public class Pause : MonoBehaviour
{
    private UIDocument uiDocument;
    private Label pauseLabel;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    /// <summary>
    /// Initialisiert die UI-Elemente und registriert die Button-Events, wenn das Skript aktiviert wird.
    /// </summary>
    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        root.Q<Button>("Stop").clicked += () => PauseGame();

        pauseLabel = root.Q<Label>("Pause");
        pauseLabel.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// zustaendig fuer die eigentliche Steuerung der Spielpause.
    /// </summary>
    private void PauseGame()
    {
        //Time.timeScale = Time.timeScale == 0 ? 1 : 0; // Zeit anhalten beim ersten drücken, beim zweiten weiterfahren

        bool isPaused = Time.timeScale == 0;
        Time.timeScale = isPaused ? 1 : 0;
        pauseLabel.style.display = isPaused ? DisplayStyle.None : DisplayStyle.Flex; // Label anzeigen oder ausblenden
    }

    /// <summary>
    /// ruft die PauseGame-Methode auf, wenn die Taste "P" gedrückt wird.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
}

using UnityEngine;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    private UIDocument uiDocument;
    private Label pauseLabel;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }
    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        root.Q<Button>("Stop").clicked += () => PauseGame();

        pauseLabel = root.Q<Label>("Pause");
        pauseLabel.style.display = DisplayStyle.None;
    }
    private void PauseGame()
    {
        //Time.timeScale = Time.timeScale == 0 ? 1 : 0; // Zeit anhalten beim ersten drücken, beim zweiten weiterfahren

        bool isPaused = Time.timeScale == 0;
        Time.timeScale = isPaused ? 1 : 0;
        pauseLabel.style.display = isPaused ? DisplayStyle.None : DisplayStyle.Flex; // Label anzeigen oder ausblenden
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
}

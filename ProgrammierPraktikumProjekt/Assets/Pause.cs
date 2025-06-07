using UnityEngine;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    private UIDocument uiDocument;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }
    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        root.Q<Button>("Stop").clicked += () => PauseGame();
    }
    private void PauseGame()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0; // Zeit anhalten beim ersten drücken, beim zweiten weiterfahren
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class NeuesMainMenu : MonoBehaviour
{
    private UIDocument uiDocument;

    private VisualElement startSeiteContainer;
    private VisualElement studierendeContainer;
    private VisualElement lehrendeContainer;
    private VisualElement schulungsteilnehmerContainer;
   
    private VisualElement szenenErstellungContainer;


    private DropdownField studierendeDropdown;
    private DropdownField lehrendeDropdown;
    private DropdownField schulungsteilnehmerDropdown;
    private DropdownField szenenErstellungDropdown;


    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        // Container Zuweisen
        startSeiteContainer = root.Q<VisualElement>("StartSeiteContainer");
        studierendeContainer = root.Q<VisualElement>("StudierendeContainer");
        lehrendeContainer = root.Q<VisualElement>("LehrendeContainer");
        schulungsteilnehmerContainer = root.Q<VisualElement>("SchulungsteilnehmerContainer");

        szenenErstellungContainer = root.Q<VisualElement>("SzenenErstellungContainer");


        // Dropdowns zuweisen
        studierendeDropdown = root.Q<DropdownField>("StudierendeDropdown");
        lehrendeDropdown = root.Q<DropdownField>("LehrendeDropdown");
        schulungsteilnehmerDropdown = root.Q<DropdownField>("SchulungsteilnehmerDropdown");

        // Die Buttons auf der StartSeite
        root.Q<Button>("StudierendeButton").clicked += () => ShowPanel(studierendeContainer);
        root.Q<Button>("LehrendeButton").clicked += () => ShowPanel(lehrendeContainer);
        root.Q<Button>("SchulungsteilnehmerButton").clicked += () => ShowPanel(schulungsteilnehmerContainer);
        root.Q<Button>("OptionsButton").clicked += () => Debug.Log("Options clicked");
        root.Q<Button>("QuitButton").clicked += QuitGame;

        // Die Play Buttons bei den Containern
        root.Q<Button>("StudierendePlayButton").clicked += () => LoadScene(studierendeDropdown.value);
        root.Q<Button>("LehrendePlayButton").clicked += () => LoadScene(lehrendeDropdown.value);
        root.Q<Button>("SchulungsteilnehmerPlayButton").clicked += () => LoadScene(schulungsteilnehmerDropdown.value);

        // Die Zurück Buttons bei den Containern
        root.Q<Button>("StudierendeZurueckButton").clicked += () => ShowPanel(startSeiteContainer);
        root.Q<Button>("LehrendeZurueckButton").clicked += () => ShowPanel(startSeiteContainer);
        root.Q<Button>("SchulungsteilnehmerZurueckButton").clicked += () => ShowPanel(startSeiteContainer);
        root.Q<Button>("SzenenErstellungZurueckButton").clicked += () => ShowPanel(lehrendeContainer);

        root.Q<Button>("SzenarioErstellenButton").clicked += () => ShowPanel(szenenErstellungContainer);


    }

    private void ShowPanel(VisualElement targetPanel)
    {
        startSeiteContainer.style.display = DisplayStyle.None;
        studierendeContainer.style.display = DisplayStyle.None;
        lehrendeContainer.style.display = DisplayStyle.None;
        schulungsteilnehmerContainer.style.display = DisplayStyle.None;

        szenenErstellungContainer.style.display = DisplayStyle.None;

        targetPanel.style.display = DisplayStyle.Flex;
    }

    private void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Keine Szene ausgewählt!");
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
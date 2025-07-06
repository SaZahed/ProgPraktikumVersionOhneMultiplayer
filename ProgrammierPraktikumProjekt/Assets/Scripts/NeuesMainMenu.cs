using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class NeuesMainMenu : MonoBehaviour
{
    private UIDocument uiDocument;

    private VisualElement startSeiteContainer;
    private VisualElement studierendeContainer;
    private VisualElement lehrendeContainer;
    private VisualElement schulungsteilnehmerContainer;
    private VisualElement szenenErstellungContainer;


    private DropdownField szenarienDropdown;
    private DropdownField schulungsteilnehmerDropdown;
    private DropdownField szenenDropdown;
    private DropdownField wetterDropdown;
    private DropdownField schiffDropdown;

    private TextField szenarioName;
    private List<SzenarioKlasse> gespeicherteSzenarien = new List<SzenarioKlasse>();


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

        //Lehrende Container Buttons
        root.Q<Button>("SzenarioErstellenButton").clicked += () => ShowPanel(szenenErstellungContainer);

        // Dropdowns zuweisen
        szenarienDropdown = root.Q<DropdownField>("SzenarienDropdown");
        schulungsteilnehmerDropdown = root.Q<DropdownField>("SchulungsteilnehmerDropdown");

        // Die Buttons auf der StartSeite
        root.Q<Button>("StudierendeButton").clicked += () => ShowPanel(studierendeContainer);
        root.Q<Button>("LehrendeButton").clicked += () => ShowPanel(lehrendeContainer);
        root.Q<Button>("SchulungsteilnehmerButton").clicked += () => ShowPanel(schulungsteilnehmerContainer);
        root.Q<Button>("OptionsButton").clicked += () => Debug.Log("Options clicked");
        root.Q<Button>("QuitButton").clicked += QuitGame;

        gespeicherteSzenarien = LadeSzenarienAusJson();
        if (szenarienDropdown != null)
        {
            szenarienDropdown.choices = gespeicherteSzenarien.Select(s => s.name).ToList();
        }
        if (schulungsteilnehmerDropdown != null)
        {
            schulungsteilnehmerDropdown.choices = gespeicherteSzenarien.Select(s => s.name).ToList();
        }

        // Die Play Buttons bei den Containern
        //root.Q<Button>("StudierendePlayButton").clicked += () => LoadScene(studierendeDropdown.value);
        //root.Q<Button>("SchulungsteilnehmerPlayButton").clicked += () => LoadScene(schulungsteilnehmerDropdown.value);
        
        // Play-Button für Studierende
        root.Q<Button>("StudierendePlayButton").clicked += () =>
        {
            string ausgewaehlterName = szenarienDropdown.value;
            var szenario = gespeicherteSzenarien.Find(s => s.name == ausgewaehlterName);
            SzenarioDaten.Wetter = szenario.wetter;
            SzenarioDaten.Schiff = szenario.schiff; // Hier wird das Schiff gesetzt
            LoadScene(szenario.szene);
        };
        // Play-Button für Schulungsteilnehmer
        root.Q<Button>("SchulungsteilnehmerPlayButton").clicked += () =>
        {
            string ausgewaehlterName = schulungsteilnehmerDropdown.value;
            var szenario = gespeicherteSzenarien.Find(s => s.name == ausgewaehlterName);
            SzenarioDaten.Wetter = szenario.wetter;
            LoadScene(szenario.szene);
        };

        // Die Zurück Buttons bei den Containern
        root.Q<Button>("StudierendeZurueckButton").clicked += () => ShowPanel(startSeiteContainer);
        root.Q<Button>("LehrendeZurueckButton").clicked += () => ShowPanel(startSeiteContainer);
        root.Q<Button>("SchulungsteilnehmerZurueckButton").clicked += () => ShowPanel(startSeiteContainer);
        root.Q<Button>("SzenenErstellungZurueckButton").clicked += () => ShowPanel(lehrendeContainer);

        //Szenario Erstellen
        szenarioName = root.Q<TextField>("SzenarioTextField");
        szenenDropdown = root.Q<DropdownField>("SzenenDropdown");
        wetterDropdown = root.Q<DropdownField>("WetterDropdown");
        schiffDropdown = root.Q<DropdownField>("SchiffDropdown");
        //root.Q<Button>("ErstellenButton1").clicked += () => SpeichereSzenarioAlsJson(new SzenarioKlasse(szenarioName.value, szenenDropdown.value, wetterDropdown.value, schiffDropdown.value));
        //Debug.Log($"Name: {szenarioName.value}, Szene: {szenenDropdown.value}, Wetter: {wetterDropdown.value}, Schiff: {schiffDropdown.value}"); //wird nichts übergeben siehe konsole

        schiffDropdown.choices = new List<string> { "MS Diane Weiss", "MS Diane Schwarz "};//neu Ansatz
        
        var erstellenButton = root.Q<Button>("ErstellenButton1");

        if (erstellenButton == null)
        {
            Debug.LogError("ErstellenButton1 nicht gefunden! Überprüfe den Namen im UXML.");
        }
        else
        {
            erstellenButton.clicked += () =>
            {
                //Debug.Log("ErstellenButton1 wurde geklickt!");//es wird auf jeden fall geklickt
                
                    string name = szenarioName?.value;
                    string szene = szenenDropdown?.value;
                    string wetter = wetterDropdown?.value;
                    string schiff = schiffDropdown?.value;

                Debug.Log($"Name: {name}, Szene: {szene}, Wetter: {wetter}, Schiff: {schiff}");//hier sieht man, dass die param richtig aus Usereingabe genommen werden


                SpeichereSzenarioAlsJson(new SzenarioKlasse(name, szene, wetter, schiff));
            };
        }
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

    //public static void SpeichereSzenarioAlsJson(SzenarioKlasse SzenarioKlasse)
    //{
    //    //string dateiPfad = "Assets/szenario.json";
    //    string dateiPfad = Path.Combine(Application.dataPath, "szenario.json");

    //    // Serialisierung mit Formatierung
    //    string jsonString = JsonUtility.ToJson(SzenarioKlasse, true);

    //    // In Datei schreiben
    //    File.WriteAllText(dateiPfad, jsonString);
    //}
    public static void SpeichereSzenarioAlsJson(SzenarioKlasse neuesSzenario)
    {
        string dateiPfad = Path.Combine(Application.dataPath, "szenario.json");

        SzenarioListe szenarioListe = new SzenarioListe();

        if (File.Exists(dateiPfad))
        {
            string jsonAlt = File.ReadAllText(dateiPfad);
            szenarioListe = JsonUtility.FromJson<SzenarioListe>(jsonAlt) ?? new SzenarioListe();
        }

        szenarioListe.szenarien.Add(neuesSzenario);

        string jsonNeu = JsonUtility.ToJson(szenarioListe, true);
        File.WriteAllText(dateiPfad, jsonNeu);
    }

    private List<SzenarioKlasse> LadeSzenarienAusJson()
    {
        string dateiPfad = Path.Combine(Application.dataPath, "szenario.json");

        string json = File.ReadAllText(dateiPfad);
        SzenarioListe liste = JsonUtility.FromJson<SzenarioListe>(json);

        if (liste == null || liste.szenarien == null)
        {
            Debug.LogWarning("Szenarien konnten nicht geladen werden oder Liste ist leer.");
            return new List<SzenarioKlasse>();
        }

        return liste.szenarien;
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
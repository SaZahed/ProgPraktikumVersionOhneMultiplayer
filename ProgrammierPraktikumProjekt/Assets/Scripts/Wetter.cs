using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Die Wetter Klasse verknuepft die UI-Elemente (Dropdown und Slider) mit dem weatherManager
/// Benutzer:innen k�nnen dar�ber den Wettertyp und dessen Intensit�t in der Simulation �ndern.
/// </summary>

public class Wetter : MonoBehaviour
{
    private UIDocument uiDocument;
    private DropdownField WetterDropdown;
    private Slider IntensitaetsSlider;

    [SerializeField] private WeatherManager weatherManager;

    void Start()
    {
        if (!string.IsNullOrEmpty(SzenarioDaten.Wetter))
        {
            weatherManager.SetWetter(SzenarioDaten.Wetter);
        }
    }
    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        WetterDropdown = root.Q<DropdownField>("WetterDropdown");
        IntensitaetsSlider = root.Q<Slider>("IntensitaetsSlider");

        if (WetterDropdown != null)
        {
            WetterDropdown.RegisterValueChangedCallback(OnWetterChanged);
        }

        if (IntensitaetsSlider != null)
        {
            IntensitaetsSlider.RegisterValueChangedCallback(OnIntensitaetChanged);
        }
    }

    private void OnWetterChanged(ChangeEvent<string> evt)
    {
        weatherManager.SetWetter(evt.newValue);
    }

    private void OnIntensitaetChanged(ChangeEvent<float> evt)
    {
        if (WetterDropdown != null)
        {
            weatherManager.UpdateIntensity(WetterDropdown.value, evt.newValue);
        }
    }
}

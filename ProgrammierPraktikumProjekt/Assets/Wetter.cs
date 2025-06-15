using UnityEngine;
using UnityEngine.UIElements;

public class Wetter : MonoBehaviour
{
    private UIDocument uiDocument;
    private DropdownField WetterDropdown;
    [SerializeField] private WeatherManager weatherManager;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        WetterDropdown = root.Q<DropdownField>("WetterDropdown");

        if (WetterDropdown != null)
        {
            WetterDropdown.RegisterValueChangedCallback(OnWetterChanged);
        }
    }

    private void OnWetterChanged(ChangeEvent<string> evt)
    {
        weatherManager.SetWetter(evt.newValue);
    }
}


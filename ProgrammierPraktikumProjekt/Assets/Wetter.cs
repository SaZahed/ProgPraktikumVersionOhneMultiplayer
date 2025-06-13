using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Wetter : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement rainVFX;
    private VisualElement snowVFX;
    private DropdownField WetterDropdown;

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
        string selected = evt.newValue;

        switch (selected)
        {
            case "Regen":
                rainVFX.disablePlayModeTint = true;
                break;
            //case "Schnee":
               // regenVFX.Stop();
               // schneeVFX.Play();
              //  break;
        }
    }
}

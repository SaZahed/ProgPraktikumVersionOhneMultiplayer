using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class HostClientScript : MonoBehaviour
{
    private UIDocument uiDocument;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }
    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        root.Q<Button>("StartClient").clicked += () => NetworkManager.Singleton.StartClient();
        root.Q<Button>("StartHost").clicked += () => NetworkManager.Singleton.StartHost();
        root.Q<Button>("StopClient").clicked += () => NetworkManager.Singleton.Shutdown();
        root.Q<Button>("StopHost").clicked += () => NetworkManager.Singleton.Shutdown();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

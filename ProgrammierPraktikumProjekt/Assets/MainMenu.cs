using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startSimulation()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void switchToOptions()
    {
        SceneManager.LoadScene("Options");
    }
    public void quitSimulation()
    {
        Debug.Log("Die Simulation wurde beendeet...");
        Application.Quit();
    }
}

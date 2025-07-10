using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Steuerung fuer das Hauptmenue der Simulation.
/// Folgende Funktionen sind implementiert: starten, Optionen und beenden.
/// </summary>
public class MainMenu : MonoBehaviour
{
   
    public void startSimulation()
    {
        SceneManager.LoadScene("SzeneAuswahl");
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

using UnityEngine;

/// <summary>
/// Klasse wurde erstellt um die Schiffsauswahl zu verwalten indem vom menue ausgewaehlte diane geladen wird
/// </summary>
/// <remarks> die weitere Implementation konnte nicht umgesetzt weredn </remarks>
public class SchiffManager : MonoBehaviour
{
    public GameObject dianeWeiss;
    public GameObject dianeSchwarz;

    //dient zunaechst als Ansatz fuer Schiffauswahl

    void Start()
    {
        void Start()
        {
            switch (SzenarioDaten.Schiff)
            {
                case "Schiff A":
                    Instantiate(dianeWeiss, new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                case "Schiff B":
                    Instantiate(dianeSchwarz, new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                default:
                    Debug.LogWarning("Kein bekanntes Schiff ausgewählt.");
                    break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

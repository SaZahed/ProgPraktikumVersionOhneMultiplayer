using UnityEngine;

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

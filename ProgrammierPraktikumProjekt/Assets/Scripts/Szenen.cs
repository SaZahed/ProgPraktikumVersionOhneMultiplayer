using UnityEngine;
using UnityEngine.SceneManagement;

public class Szenen : MonoBehaviour
{
    public void ladeBremen()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void ladeBremerheven()
    {
        SceneManager.LoadSceneAsync("Bremerhaven");
    }

    public void ladeNordsee()
    {
        SceneManager.LoadSceneAsync("Nordsee");
    }
}

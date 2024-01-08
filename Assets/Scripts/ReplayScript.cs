using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayScript : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

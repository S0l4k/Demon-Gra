using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public int killed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TryLoadNextScene();
        }
    }

    void TryLoadNextScene()
    {
        if (DeathCount.Instance != null && DeathCount.Instance.GetDefeatedEnemies() >= killed)
        {
            LoadNextScene();
        }
        else
        {
            Debug.Log("Musisz pokonaæ wiêcej przeciwników, aby przejœæ dalej!");
        }
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
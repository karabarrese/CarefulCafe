using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentText : MonoBehaviour
{
    private static PersistentText instance;
    private static string savedScene = "";

    void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (instance != null)
        {
            if (currentScene == savedScene)
            {
                Destroy(gameObject); // Prevent duplicates if returning
            }
            else
            {
                gameObject.SetActive(false); // Hide background when switching scenes
            }
            return;
        }

        instance = this;
        savedScene = currentScene;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == savedScene)
        {
            gameObject.SetActive(true); // Show background when returning
        }
        else
        {
            gameObject.SetActive(false); // Hide background in new scenes
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObject : MonoBehaviour
{
    private static bool isInitialized = false; 
    private string initialScene;

    void Awake()
    {
        if (GameObject.Find(gameObject.name) != gameObject && isInitialized)
        {
            Destroy(gameObject);
            return;
        }

        isInitialized = true; 
        initialScene = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != initialScene)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}

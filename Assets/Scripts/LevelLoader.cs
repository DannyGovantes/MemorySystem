using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    public static void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
        {

            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.LogWarning("LEVEL LOADER load level error: invalid scene specified");
        }

    }

    public static void ReloadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        LoadLevel(currentScene.buildIndex);
    }

}

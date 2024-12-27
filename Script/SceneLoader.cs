using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject GamesPanel;
    public GameObject GameMenuPanel;
    private void Awake()
    {
        GamesPanel.SetActive(false);
        GameMenuPanel.SetActive(true);
    }
    // Method to be called by each button
    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is not provided or empty!");
        }
    }
    public void ActivePanel()
    {
           GamesPanel.SetActive(true);
        GameMenuPanel.SetActive(false);
    }
    public void gamepanel()
    {
        GamesPanel.SetActive(false);
        GameMenuPanel.SetActive(true);

    }
}

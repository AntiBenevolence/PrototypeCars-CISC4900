using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void OpenSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void OpenAboutUsScene()
    {
        SceneManager.LoadScene("AboutUsScene");
    }

    public void OpenCarSelectionScene()
    {
        SceneManager.LoadScene("BetaScene");
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
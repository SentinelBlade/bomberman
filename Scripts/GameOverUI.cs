using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverUI : MonoBehaviour
{
   
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

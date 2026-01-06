using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
